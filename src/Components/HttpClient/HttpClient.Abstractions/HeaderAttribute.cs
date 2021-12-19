using System;

namespace SourceGeneratorPower.HttpClient
{
    [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Method)]
    public class HeaderAttribute : Attribute
    {
        public string Key { get; }

        public string Value { get; }

        public HeaderAttribute(string key, string value)
        {
            Key = key;
            Value = value;
        }
    }
}