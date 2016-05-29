namespace MyTested.Mvc.Internal.Application
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Extensions.DependencyInjection;

    public class ServiceLifetimeCollection : Dictionary<Type, ServiceLifetime>
    {
    }
}
