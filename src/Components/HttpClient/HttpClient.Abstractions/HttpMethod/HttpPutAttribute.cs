namespace SourceGeneratorPower.HttpClient.HttpMethod
{
    /// <summary>
    /// Identity a method send HTTP Put request
    /// </summary>
    public class HttpPutAttribute : HttpMethodAttribute
    {
        /// <summary>
        /// Creates a new <see cref="HttpPutAttribute"/> with the given route template.
        /// </summary>
        /// <param name="template">route template</param>
        public HttpPutAttribute(string template) : base(template)
        {
        }
    }
}