namespace MyTested.AspNetCore.Mvc.Plugins
{
    using System;
    using Microsoft.AspNetCore.Mvc.Infrastructure;
    using Microsoft.Extensions.DependencyInjection;

    public class RazorRuntimeCompilationTestPlugin : IServiceRegistrationPlugin
    {
        private readonly Type defaultActionDescriptorChangeProviderServiceType = typeof(IActionDescriptorChangeProvider);

        public Func<ServiceDescriptor, bool> ServiceSelectorPredicate
            => serviceDescriptor => serviceDescriptor.ServiceType == this.defaultActionDescriptorChangeProviderServiceType;

        public Action<IServiceCollection> ServiceRegistrationDelegate
            => serviceCollection => serviceCollection.ReplaceRazorRuntimeCompilation();
    }
}
