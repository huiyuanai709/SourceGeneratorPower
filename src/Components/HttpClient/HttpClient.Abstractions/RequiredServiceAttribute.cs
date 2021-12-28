using System;

namespace SourceGeneratorPower.HttpClient
{
    /// <summary>
    /// Mark a interface with a serviceType and fieldName which will be injected in Implement class
    /// </summary>
    [AttributeUsage(AttributeTargets.Interface, AllowMultiple = true)]
    public class RequiredServiceAttribute : Attribute
    {
        /// <summary>
        /// Inject service type
        /// </summary>
        public Type ServiceType { get; }

        /// <summary>
        /// Inject field name
        /// </summary>
        public string FieldName { get; }

        /// <summary>
        /// Create new <see cref="RequiredServiceAttribute"/> with given serviceType and fieldName
        /// </summary>
        /// <param name="serviceType"></param>
        /// <param name="fieldName"></param>
        public RequiredServiceAttribute(Type serviceType, string fieldName)
        {
            ServiceType = serviceType;
            FieldName = fieldName;
        }
    }
}