using System;

namespace SourceGeneratorPower.Services.Attributes
{
    /// <summary>
    /// Mark a class/interface as a singleton service
    /// </summary>
    public class SingletonServiceAttribute : ServiceAttribute
    {
        public SingletonServiceAttribute() : base(ServiceLifetime.Singleton)
        {
        }

        public SingletonServiceAttribute(params Type[] types) : base(ServiceLifetime.Singleton, types)
        {
        }
    }
}