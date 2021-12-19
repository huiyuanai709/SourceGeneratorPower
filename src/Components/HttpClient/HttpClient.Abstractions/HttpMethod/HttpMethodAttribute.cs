using System;

namespace SourceGeneratorPower.HttpClient.HttpMethod
{
    [AttributeUsage(AttributeTargets.Method)]
    public class HttpMethodAttribute : Attribute
    {
        public string Template { get; }

        public HttpMethodAttribute(string template)
        {
            Template = template;
        }
    }
}