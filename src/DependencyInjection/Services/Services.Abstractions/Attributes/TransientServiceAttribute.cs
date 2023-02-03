using System;

namespace SourceGeneratorPower.Services.Attributes
{
    public class TransientServiceAttribute : ServiceAttribute
    {
        public TransientServiceAttribute() : base(ServiceLifetime.Transient)
        {
        }

        public TransientServiceAttribute(params Type[] types) : base(ServiceLifetime.Transient, types)
        {
        }
    }
}