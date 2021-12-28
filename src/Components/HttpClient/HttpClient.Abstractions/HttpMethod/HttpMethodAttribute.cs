using System;

namespace SourceGeneratorPower.HttpClient.HttpMethod
{
    /// <summary>
    /// HTTP method abstract type for common encapsulation
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public abstract class HttpMethodAttribute : Attribute
    {
        /// <summary>
        /// Route template
        /// </summary>
        private string Template { get; }
        
        /// <summary>
        /// Creates a new <see cref="HttpMethodAttribute"/> with the given route template.
        /// </summary>
        /// <param name="template">route template</param>
        protected HttpMethodAttribute(string template)
        {
            Template = template;
        }
    }
}