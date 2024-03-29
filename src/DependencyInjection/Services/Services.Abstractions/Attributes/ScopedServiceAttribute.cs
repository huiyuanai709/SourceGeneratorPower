﻿using System;

namespace SourceGeneratorPower.Services.Attributes
{
    /// <summary>
    /// Mark a class/interface as a scoped service
    /// </summary>
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