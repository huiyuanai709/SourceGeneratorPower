using Microsoft.CodeAnalysis;

namespace SourceGeneratorPower.Services;

public static class SymbolExtension
{
    public static INamedTypeSymbol CompatibleWithGenericType(this INamedTypeSymbol symbol)
    {
        return symbol.IsGenericType ? symbol.ConstructUnboundGenericType() : symbol;
    }
}