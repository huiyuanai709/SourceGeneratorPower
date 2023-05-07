using System;

namespace SourceGeneratorPower.Services.Attributes
{
    /// <summary>
    /// Mark a class/interface as a transient service
    /// </summary>
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