using System;

namespace SourceGeneratorPower.HttpClient
{
    /// <summary>
    /// Mark a interface or a Method with a KeyValuePair which will be add to request headers
    /// </summary>
    [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Method)]
    public class HeaderAttribute : Attribute
    {
        /// <summary>
        /// Header Key
        /// </summary>
        private string Key { get; }

        /// <summary>
        /// HeaderValue
        /// </summary>
        private string Value { get; }

        /// <summary>
        /// Create a new <see cref="HeaderAttribute"/> with given key, value
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public HeaderAttribute(string key, string value)
        {
            Key = key;
            Value = value;
        }
    }
}