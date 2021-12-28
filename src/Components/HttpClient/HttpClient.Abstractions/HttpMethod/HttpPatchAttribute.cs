namespace SourceGeneratorPower.HttpClient.HttpMethod
{
    /// <summary>
    /// Identity a method send HTTP Patch request
    /// </summary>
    public class HttpPatchAttribute : HttpMethodAttribute
    {
        /// <summary>
        /// Creates a new <see cref="HttpPatchAttribute"/> with the given route template.
        /// </summary>
        /// <param name="template">route template</param>
        public HttpPatchAttribute(string template) : base(template)
        {
        }
    }
}