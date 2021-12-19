using System;

namespace SourceGeneratorPower.HttpClient
{
    [AttributeUsage(AttributeTargets.Interface, AllowMultiple = true)]
    public class RequiredServiceAttribute : Attribute
    {
        public Type ServiceType { get; }

        public string FieldName { get; }

        public RequiredServiceAttribute(Type serviceType, string fieldName)
        {
            ServiceType = serviceType;
            FieldName = fieldName;
        }
    }
}