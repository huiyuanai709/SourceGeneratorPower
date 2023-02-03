using System;
using System.Collections.Generic;
using Microsoft.CodeAnalysis;

namespace SourceGeneratorPower.Services;

public class ServiceDescriptor : IEqualityComparer<ServiceDescriptor>
{
    public INamedTypeSymbol ServiceTypeSymbol { get; set; }
    
    public SortedSet<INamedTypeSymbol> ImplementTypeSymbols { get; set; }

    public AttributeData AttributeData { get; set; }

    public bool Equals(ServiceDescriptor x, ServiceDescriptor y)
    {
        if (x is null)
        {
            return y is null;
        }

        return SymbolEqualityComparer.Default.Equals(x.ServiceTypeSymbol, y?.ServiceTypeSymbol);
    }

    public int GetHashCode(ServiceDescriptor obj)
    {
#pragma warning disable RS1024
        return obj.ServiceTypeSymbol?.GetHashCode() ?? 0;
#pragma warning restore RS1024
    }
}