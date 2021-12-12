﻿using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace SourceGeneratorPower.Options
{
    [Generator]
    public class OptionsGenerator : ISourceGenerator
    {
        public void Initialize(GeneratorInitializationContext context)
        {
            context.RegisterForSyntaxNotifications(() => new OptionsSyntax());
        }

        public void Execute(GeneratorExecutionContext context)
        {
            if (!(context.SyntaxContextReceiver is OptionsSyntax receiver))
            {
                return;
            }

            INamedTypeSymbol attributeSymbol =
                context.Compilation.GetTypeByMetadataName("SourceGeneratorPower.Options.OptionAttribute");

            StringBuilder source = new StringBuilder($@"
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection
{{
    public static class ScanInjectOptions
    {{
        public static void AutoInjectOptions(this IServiceCollection services, IConfiguration configuration)
        {{
");
            foreach (ITypeSymbol typeSymbol in receiver.TypeSymbols)
            {
                source.Append(' ', 12);
                source.AppendLine(ProcessOptions(typeSymbol, attributeSymbol));
            }

            source.Append(' ', 8).AppendLine("}")
                .Append(' ', 4).AppendLine("}")
                .AppendLine("}");
            context.AddSource("Options.AutoGenerated.cs",
                SourceText.From(source.ToString(), Encoding.UTF8));
        }

        private string ProcessOptions(ISymbol typeSymbol, ISymbol attributeSymbol)
        {
            AttributeData attributeData = typeSymbol.GetAttributes()
                .Single(ad => ad.AttributeClass!.Equals(attributeSymbol, SymbolEqualityComparer.Default));
            TypedConstant path = attributeData.ConstructorArguments.First();
            return $@"services.Configure<{typeSymbol.ToDisplayString()}>(configuration.GetSection(""{path.Value}""));";
        }

        class OptionsSyntax : ISyntaxContextReceiver
        {
            public List<ITypeSymbol> TypeSymbols { get; set; } = new List<ITypeSymbol>();

            public void OnVisitSyntaxNode(GeneratorSyntaxContext context)
            {
                if (context.Node is ClassDeclarationSyntax cds && cds.AttributeLists.Count > 0)
                {
                    ITypeSymbol typeSymbol = context.SemanticModel.GetDeclaredSymbol(cds) as ITypeSymbol;
                    if (typeSymbol!.GetAttributes().Any(x =>
                            x.AttributeClass!.ToDisplayString() ==
                            "SourceGeneratorPower.Options.OptionAttribute"))
                    {
                        TypeSymbols.Add(typeSymbol);
                    }
                }
            }
        }
    }
}