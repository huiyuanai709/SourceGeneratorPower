namespace SourceGeneratorPower.HttpClient.HttpMethod
{
    /// <summary>
    /// Identity a method send HTTP Delete request
    /// </summary>
    public class HttpDeleteAttribute : HttpMethodAttribute
    {
        /// <summary>
        /// Creates a new <see cref="HttpDeleteAttribute"/> with the given route template.
        /// </summary>
        /// <param name="template">route template</param>
        public HttpDeleteAttribute(string template) : base(template)
        {
        }
    }
}