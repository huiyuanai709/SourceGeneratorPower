using System;

namespace SourceGeneratorPower.Services.Attributes
{
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