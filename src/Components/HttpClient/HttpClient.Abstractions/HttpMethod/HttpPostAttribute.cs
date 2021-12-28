namespace SourceGeneratorPower.HttpClient.HttpMethod
{
    /// <summary>
    /// Identity a method send HTTP Post request
    /// </summary>
    public class HttpPostAttribute : HttpMethodAttribute
    {
        /// <summary>
        /// Creates a new <see cref="HttpPostAttribute"/> with the given route template.
        /// </summary>
        /// <param name="template">route template</param>
        public HttpPostAttribute(string template) : base(template)
        {
        }
    }
}