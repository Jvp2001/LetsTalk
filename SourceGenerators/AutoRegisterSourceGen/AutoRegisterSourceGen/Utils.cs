using System.Collections;
using System.Collections.Generic;
using Microsoft.CodeAnalysis;

namespace AutoRegisterSourceGen;

public static class Utils
{
    public static IEnumerable<INamedTypeSymbol> GetAllClasses(INamespaceSymbol namespaceSymbol)
    {
        foreach (var member in namespaceSymbol.GetMembers())
        {
            if (member is INamespaceSymbol namespaceSymbol1)
            {
                foreach (var allClass in GetAllClasses(namespaceSymbol1))
                    yield return allClass;
            }
            else if (member is INamedTypeSymbol allClass1)
            {
                yield return allClass1;
            }
        }
    }

    public static IEnumerable<INamedTypeSymbol> FilterByParentType(this IEnumerable<INamedTypeSymbol> classes, INamedTypeSymbol parentType)
    {
        foreach (var @class in classes)
        {
            if (@class.BaseType.Equals(parentType, SymbolEqualityComparer.Default) == true)
                yield return @class;
        }
    }
}
