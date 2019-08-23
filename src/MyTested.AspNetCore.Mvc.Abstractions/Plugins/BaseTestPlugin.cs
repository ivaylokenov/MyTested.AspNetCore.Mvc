namespace MyTested.AspNetCore.Mvc.Plugins
{
    using System;
    using Internal;
    using Microsoft.Extensions.DependencyInjection;

    public abstract class BaseTestPlugin
    {
        private readonly Type defaultMvcMarkerServiceType = WebFramework.Internals.MvcMarkerService;

        public Func<ServiceDescriptor, bool> ServiceSelectorPredicate 
            => serviceDescriptor => serviceDescriptor.ServiceType == this.defaultMvcMarkerServiceType;
    }
}
