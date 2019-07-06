namespace MyTested.AspNetCore.Mvc.Plugins
{
    using System;
    using Internal.TestContexts;
    using Utilities;

    public class DependencyInjectionTestPlugin : IShouldPassForPlugin
    {
        private readonly Type serviceProviderType = typeof(IServiceProvider);

        public object TryGetValue(Type type, ComponentTestContext testContext)
            => Reflection.AreAssignable(this.serviceProviderType, type) // ServiceProvider.
                ? testContext.HttpContext.RequestServices
                : null;
    }
}
