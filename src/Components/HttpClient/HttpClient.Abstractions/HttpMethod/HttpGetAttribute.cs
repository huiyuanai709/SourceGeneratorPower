namespace SourceGeneratorPower.HttpClient.HttpMethod
{
    /// <summary>
    /// Identity a method send HTTP Get request
    /// </summary>
    public class HttpGetAttribute : HttpMethodAttribute
    {
        /// <summary>
        /// Creates a new <see cref="HttpGetAttribute"/> with the given route template.
        /// </summary>
        /// <param name="template">route template</param>
        public HttpGetAttribute(string template) : base(template)
        {
        }
    }
}