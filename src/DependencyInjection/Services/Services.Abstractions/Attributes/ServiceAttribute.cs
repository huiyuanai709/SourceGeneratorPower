using System;

namespace SourceGeneratorPower.Services.Attributes
{
    [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class)]
    public class ServiceAttribute : Attribute
    {
        private ServiceLifetime _serviceLifetime;

        private Type[] _types;

        protected ServiceAttribute(ServiceLifetime serviceLifetime, params Type[] types)
        {
            _serviceLifetime = serviceLifetime;
            _types = types;
        }
    }
}