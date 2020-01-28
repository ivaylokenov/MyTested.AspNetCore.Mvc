namespace MyTested.AspNetCore.Mvc.Plugins
{
    using System;
    using Microsoft.AspNetCore.Mvc.Internal;
    using Microsoft.Extensions.DependencyInjection;

    public abstract class BaseTestPlugin
    {
        private readonly Type defaultMvcMarkerServiceType = typeof(MvcMarkerService);

        public Func<ServiceDescriptor, bool> ServiceSelectorPredicate 
            => serviceDescriptor => serviceDescriptor.ServiceType == this.defaultMvcMarkerServiceType;
    }
}
