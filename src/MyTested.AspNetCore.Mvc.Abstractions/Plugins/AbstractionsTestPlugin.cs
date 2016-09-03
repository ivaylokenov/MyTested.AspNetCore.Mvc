namespace MyTested.AspNetCore.Mvc.Plugins
{
    using System;
    using Internal.Contracts;
    using Internal.TestContexts;
    using Microsoft.Extensions.DependencyInjection;
    using Utilities;

    public class AbstractionsTestPlugin : BaseTestPlugin, IServiceRegistrationPlugin, IInitializationPlugin, IShouldPassForPlugin
    {
        private readonly Type exceptionType = typeof(Exception);

        public Action<IServiceCollection> ServiceRegistrationDelegate =>
            serviceCollection => serviceCollection.AddCoreTesting();

        // this call prepares all application conventions and fills the controller action descriptor cache
        public Action<IServiceProvider> InitializationDelegate => serviceProvider => serviceProvider.GetService<IControllerActionDescriptorCache>();

        public object TryGetValue(Type type, ComponentTestContext testContext)
            => Reflection.AreAssignable(exceptionType, type) // Exception
                ? testContext.CaughtException
                : null;
    }
}
