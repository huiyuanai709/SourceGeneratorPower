﻿using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace SourceGeneratorPower.Services
{
    [Generator]
    public class ServicesGenerator : ISourceGenerator
    {
        public void Initialize(GeneratorInitializationContext context)
        {
            context.RegisterForSyntaxNotifications(() => new ServicesSyntax());
        }

        public void Execute(GeneratorExecutionContext context)
        {
            if (context.SyntaxContextReceiver is not ServicesSyntax receiver)
            {
                return;
            }

            CollectReferencesAttributedTypeSymbol(context, receiver);

            var puredServiceTypes = new Dictionary<INamedTypeSymbol, AttributeData>(SymbolEqualityComparer.Default);

            StringBuilder source = new StringBuilder($@"
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection
{{
    public static class ScanInjectServices
    {{
        public static void AddAutoInjectServices(this IServiceCollection services)
        {{
");
            foreach (var symbol in receiver.TypeSymbols)
            {
                if (symbol.Key.TypeKind == TypeKind.Interface && symbol.Value.ConstructorArguments.IsDefaultOrEmpty)
                {
                    puredServiceTypes.Add(symbol.Key.CompatibleWithGenericType(), symbol.Value);
                    continue;
                }

                var serviceDescriptors = CollectServiceDescriptor(symbol.Key, symbol.Value);
                serviceDescriptors.ForEach(x => source.Append(' ', 12).Append(PrintServiceDescriptor(x)));
            }

            var serviceTypes = CollectServiceImplementDescriptor(context, puredServiceTypes);
            foreach (var serviceDescriptor in serviceTypes.Values)
            {
                source.Append(' ', 12).Append(PrintServiceDescriptor(serviceDescriptor));
            }


            source.Append(' ', 8).AppendLine("}")
                .Append(' ', 4).AppendLine("}")
                .AppendLine("}");
            context.AddSource("Services.AutoGenerated.cs",
                SourceText.From(source.ToString(), Encoding.UTF8));
        }

        private static ImmutableDictionary<INamedTypeSymbol, ServiceDescriptor> CollectServiceImplementDescriptor(GeneratorExecutionContext context,
            Dictionary<INamedTypeSymbol, AttributeData> puredServiceTypes)
        {
            var serviceAndImplementVisitor = new ServiceImplementTypeVisitor(puredServiceTypes);

            context.Compilation.SourceModule.ContainingAssembly.Accept(serviceAndImplementVisitor);

            foreach (var assemblySymbol in context.Compilation.SourceModule.ReferencedAssemblySymbols
                         .Where(x => x.Identity.PublicKeyToken == ImmutableArray<byte>.Empty))
            {
                assemblySymbol.Accept(serviceAndImplementVisitor);
            }

            return serviceAndImplementVisitor.GetServiceDescriptors();
        }

        private static void CollectReferencesAttributedTypeSymbol(GeneratorExecutionContext context, ServicesSyntax receiver)
        {
            var servicesVisitor = new ServicesVisitor();
            foreach (var assemblySymbol in context.Compilation.SourceModule.ReferencedAssemblySymbols
                         .Where(x => x.Identity.PublicKeyToken == ImmutableArray<byte>.Empty))
            {
                assemblySymbol.Accept(servicesVisitor);
            }

            foreach (var serviceType in servicesVisitor.GetServiceTypes())
            {
                receiver.TypeSymbols.Add(serviceType.Key, serviceType.Value);
            }
        }

        private static string PrintServiceDescriptor(ServiceDescriptor serviceDescriptor)
        {
            var attributeStr = serviceDescriptor.AttributeData.AttributeClass!.ToDisplayString();
            var stringBuilder = new StringBuilder();
            switch (attributeStr)
            {
                case "SourceGeneratorPower.Services.Attributes.TransientServiceAttribute":
                    foreach (var type in serviceDescriptor.ImplementTypeSymbols)
                    {
                        stringBuilder.AppendLine(
                            $"services.AddTransient(typeof(global::{serviceDescriptor.ServiceTypeSymbol.CompatibleWithGenericType().ToDisplayString()}), typeof(global::{type.CompatibleWithGenericType().ToDisplayString()}));");
                    }

                    break;
                case "SourceGeneratorPower.Services.Attributes.ScopedServiceAttribute":
                    foreach (var type in serviceDescriptor.ImplementTypeSymbols)
                    {
                        stringBuilder.AppendLine(
                            $"services.AddScoped(typeof(global::{serviceDescriptor.ServiceTypeSymbol.CompatibleWithGenericType().ToDisplayString()}), typeof(global::{type.CompatibleWithGenericType().ToDisplayString()}));");
                    }

                    break;
                case "SourceGeneratorPower.Services.Attributes.SingletonServiceAttribute":
                    foreach (var type in serviceDescriptor.ImplementTypeSymbols)
                    {
                        stringBuilder.AppendLine(
                            $"services.AddSingleton(typeof(global::{serviceDescriptor.ServiceTypeSymbol.CompatibleWithGenericType().ToDisplayString()}), typeof(global::{type.CompatibleWithGenericType().ToDisplayString()}));");
                    }

                    break;
            }

            return stringBuilder.ToString();
        }

        private List<ServiceDescriptor> CollectServiceDescriptor(INamedTypeSymbol symbol, AttributeData attributeData)
        {
            if (attributeData.ConstructorArguments.IsDefaultOrEmpty)
            {
                return symbol.AllInterfaces.Select(x => x.CompatibleWithGenericType()).Concat(new []{symbol}).Select(x => new ServiceDescriptor
                {
                    ServiceTypeSymbol = x,
                    ImplementTypeSymbols = new SortedSet<INamedTypeSymbol>
                    {
                        symbol
                    },
                    AttributeData = attributeData
                }).ToList();
            }

            var types = attributeData.ConstructorArguments.First().Values;
            if (symbol.TypeKind == TypeKind.Interface)
            {
                return new List<ServiceDescriptor>
                {
                    new()
                    {
                        ServiceTypeSymbol = symbol,
                        ImplementTypeSymbols =
                            new SortedSet<INamedTypeSymbol>(types.Select(x => (x.Value as INamedTypeSymbol))),
                        AttributeData = attributeData
                    }
                };
            }

            return types.Select(x => new ServiceDescriptor
            {
                ServiceTypeSymbol = x.Value as INamedTypeSymbol,
                ImplementTypeSymbols = new SortedSet<INamedTypeSymbol>
                {
                    symbol
                },
                AttributeData = attributeData
            }).ToList();
        }


        class ServicesSyntax : ISyntaxContextReceiver
        {
            public readonly Dictionary<INamedTypeSymbol, AttributeData> TypeSymbols = new(SymbolEqualityComparer.Default);

            public void OnVisitSyntaxNode(GeneratorSyntaxContext context)
            {
                if (context.Node is not ClassDeclarationSyntax &&
                    context.Node is not InterfaceDeclarationSyntax)
                {
                    return;
                }

                var typeSymbol = context.SemanticModel.GetDeclaredSymbol(context.Node);

                var attributeData = typeSymbol!.GetAttributes().FirstOrDefault(x =>
                    x.AttributeClass!.BaseType?.ToDisplayString() ==
                    "SourceGeneratorPower.Services.Attributes.ServiceAttribute");
                if (attributeData is not null)
                {
                    TypeSymbols.Add((INamedTypeSymbol)typeSymbol, attributeData);
                }
            }
        }

        class ServicesVisitor : SymbolVisitor
        {
            private readonly Dictionary<INamedTypeSymbol, AttributeData> _serviceTypeSymbols;

            public ServicesVisitor()
            {
                _serviceTypeSymbols = new Dictionary<INamedTypeSymbol, AttributeData>(SymbolEqualityComparer.Default);
            }

            public ImmutableDictionary<INamedTypeSymbol, AttributeData> GetServiceTypes() =>
                _serviceTypeSymbols.ToImmutableDictionary(SymbolEqualityComparer.Default);

            public override void VisitAssembly(IAssemblySymbol symbol)
            {
                symbol.GlobalNamespace.Accept(this);
            }

            public override void VisitNamespace(INamespaceSymbol symbol)
            {
                foreach (var namespaceOrTypeSymbol in symbol.GetMembers())
                {
                    namespaceOrTypeSymbol.Accept(this);
                }
            }

            public override void VisitNamedType(INamedTypeSymbol symbol)
            {
                if (symbol.DeclaredAccessibility != Accessibility.Public)
                {
                    return;
                }

                var attributeData = symbol.GetAttributes().FirstOrDefault(x =>
                    x.AttributeClass!.BaseType?.ToDisplayString() ==
                    "SourceGeneratorPower.Services.Attributes.ServiceAttribute");
                if (attributeData != null)
                {
                    _serviceTypeSymbols.Add(symbol, attributeData);
                }

                var nestedTypes = symbol.GetMembers();
                if (nestedTypes.IsDefaultOrEmpty)
                {
                    return;
                }

                foreach (var nestedType in nestedTypes)
                {
                    nestedType.Accept(this);
                }
            }
        }

        class ServiceImplementTypeVisitor : SymbolVisitor
        {
            private readonly Dictionary<INamedTypeSymbol, ServiceDescriptor> _serviceDescriptors;
            private readonly Dictionary<INamedTypeSymbol, AttributeData> _serviceSymbols;

            public ServiceImplementTypeVisitor(Dictionary<INamedTypeSymbol, AttributeData> serviceSymbols)
            {
                _serviceSymbols = serviceSymbols;
                _serviceDescriptors = new Dictionary<INamedTypeSymbol, ServiceDescriptor>(SymbolEqualityComparer.Default);
            }

            public ImmutableDictionary<INamedTypeSymbol, ServiceDescriptor> GetServiceDescriptors() =>
                _serviceDescriptors.ToImmutableDictionary(SymbolEqualityComparer.Default);

            public override void VisitAssembly(IAssemblySymbol symbol)
            {
                symbol.GlobalNamespace.Accept(this);
            }

            public override void VisitNamespace(INamespaceSymbol symbol)
            {
                foreach (var namespaceOrTypeSymbol in symbol.GetMembers())
                {
                    namespaceOrTypeSymbol.Accept(this);
                }
            }

            public override void VisitNamedType(INamedTypeSymbol symbol)
            {
                if (symbol.DeclaredAccessibility != Accessibility.Public)
                {
                    return;
                }

                foreach (var typeSymbol in symbol.AllInterfaces.Select(x => x.CompatibleWithGenericType()))
                {
                    if (!_serviceSymbols.TryGetValue(typeSymbol, out var attributeData))
                    {
                        continue;
                    }

                    CollectServiceDescriptor(symbol, typeSymbol, attributeData);
                }

                var nestedTypes = symbol.GetMembers();
                if (nestedTypes.IsDefaultOrEmpty)
                {
                    return;
                }

                foreach (var nestedType in nestedTypes)
                {
                    nestedType.Accept(this);
                }
            }

            private void CollectServiceDescriptor(INamedTypeSymbol symbol, INamedTypeSymbol typeSymbol, AttributeData attributeData)
            {
                if (_serviceDescriptors.TryGetValue(typeSymbol, out var serviceDescriptor))
                {
                    serviceDescriptor.ImplementTypeSymbols.Add(symbol);
                    return;
                }

                _serviceDescriptors.Add(typeSymbol, new ServiceDescriptor
                {
                    ServiceTypeSymbol = typeSymbol,
                    ImplementTypeSymbols = new SortedSet<INamedTypeSymbol>()
                    {
                        symbol
                    },
                    AttributeData = attributeData
                });
            }
        }
    }
}