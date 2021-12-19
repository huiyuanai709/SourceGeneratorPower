using System;

namespace SourceGeneratorPower.HttpClient
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Parameter)]
    public class IgnoreAttribute : Attribute
    {
    }
}