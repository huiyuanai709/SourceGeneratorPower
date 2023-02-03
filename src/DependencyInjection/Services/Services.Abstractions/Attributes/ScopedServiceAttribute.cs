using System;

namespace SourceGeneratorPower.Services.Attributes
{
    public class ScopedServiceAttribute : ServiceAttribute
    {
        public ScopedServiceAttribute() : base(ServiceLifetime.Scope)
        {
        }

        public ScopedServiceAttribute(params Type[] types) : base(ServiceLifetime.Scope, types)
        {
        }
    }
}