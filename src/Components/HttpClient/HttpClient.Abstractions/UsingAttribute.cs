using System;

namespace SourceGeneratorPower.HttpClient
{
    /// <summary>
    /// Mark a interface with a series of using directives which will be add to using block in Implement class
    /// </summary>
    [AttributeUsage(AttributeTargets.Interface, AllowMultiple = true)]
    public class UsingAttribute : Attribute
    {
        /// <summary>
        /// Using directives
        /// </summary>
        public string[] UsingDirectives { get; }

        /// <summary>
        /// Create new <see cref="UsingAttribute"/> with using directives
        /// </summary>
        /// <param name="usingDirectives"></param>
        public UsingAttribute(params string[] usingDirectives)
        {
            UsingDirectives = usingDirectives;
        }
    }
}