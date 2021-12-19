using System;

namespace SourceGeneratorPower.HttpClient
{
    /// <summary>
    /// Marked class with a name in IHttpClientFactory created
    /// </summary>
    [AttributeUsage(AttributeTargets.Interface)]
    public class HttpClientAttribute : Attribute
    {
        /// <summary>
        /// HttpClient Name
        /// </summary>
        public string Name { get; set; }

        public HttpClientAttribute()
        {
        }

        public HttpClientAttribute(string name)
        {
            Name = name;
        }
    }
}