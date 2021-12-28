using System;

namespace SourceGeneratorPower.HttpClient
{
    /// <summary>
    /// Identity a class which will be implemented by SourceGenerator
    /// </summary>
    [AttributeUsage(AttributeTargets.Interface)]
    public class HttpClientAttribute : Attribute
    {
        /// <summary>
        /// HttpClient name
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Create a new <see cref="HttpClientAttribute"/>
        /// </summary>
        public HttpClientAttribute()
        {
        }

        /// <summary>
        /// Create a new <see cref="HttpClientAttribute"/> with given name
        /// </summary>
        /// <param name="name"></param>
        public HttpClientAttribute(string name)
        {
            Name = name;
        }
    }
}