namespace MyTested.AspNetCore.Mvc.Plugins
{
    using System;
    using Internal.Contracts;
    using Internal.TestContexts;
    using Microsoft.Extensions.DependencyInjection;
    using Utilities;
    using Microsoft.AspNetCore.Http;

    public class AbstractionsTestPlugin : BaseTestPlugin, IServiceRegistrationPlugin, IInitializationPlugin, IShouldPassForPlugin
    {
        private readonly Type baseHttpContextType = typeof(HttpContext);
        private readonly Type baseHttpRequestType = typeof(HttpRequest);

        public Action<IServiceCollection> ServiceRegistrationDelegate =>
            serviceCollection => serviceCollection.AddCoreTesting();

        // this call prepares all application conventions and fills the controller action descriptor cache
        public Action<IServiceProvider> InitializationDelegate => serviceProvider => serviceProvider.GetService<IControllerActionDescriptorCache>();

        public object TryGetValue(Type type, ComponentTestContext testContext)
            => Reflection.AreAssignable(baseHttpContextType, type) // HttpContext
                ? testContext.HttpContext
                : Reflection.AreAssignable(baseHttpRequestType, type) // HttpRequest
                ? (object)testContext.HttpRequest
                : null;
    }
}
