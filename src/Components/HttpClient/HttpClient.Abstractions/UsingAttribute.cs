using System;

namespace SourceGeneratorPower.HttpClient
{
    [AttributeUsage(AttributeTargets.Interface, AllowMultiple = true)]
    public class UsingAttribute : Attribute
    {
        public string[] UsingDirectives { get; set; }

        public UsingAttribute(params string[] usingDirectives)
        {
            UsingDirectives = usingDirectives;
        }
    }
}