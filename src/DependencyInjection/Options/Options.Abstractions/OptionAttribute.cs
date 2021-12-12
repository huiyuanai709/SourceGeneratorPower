using System;

namespace SourceGeneratorPower.Options
{
    /// <summary>
    /// Mark a class with a Key in IConfiguration which will be source generated in the DependencyInjection extension method
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class OptionAttribute : Attribute
    {
        /// <summary>
        /// The Key represent IConfiguration section
        /// </summary>
        public string Key { get; }

        public OptionAttribute(string key)
        {
            Key = key;
        }
    }
}
