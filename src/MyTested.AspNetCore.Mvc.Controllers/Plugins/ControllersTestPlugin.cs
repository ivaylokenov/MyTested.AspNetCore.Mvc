namespace MyTested.AspNetCore.Mvc.Plugins
{
    using System;
    using Microsoft.Extensions.DependencyInjection;
    using Internal.TestContexts;
    using Microsoft.AspNetCore.Http;
    using Utilities;

    public class ControllersTestPlugin : BaseTestPlugin, IDefaultRegistrationPlugin, IServiceRegistrationPlugin, IShouldPassForPlugin
    {
        private readonly Type controllerAttributesType = typeof(ControllerAttributes);
        private readonly Type actionAttributesType = typeof(ActionAttributes);
        private readonly Type exceptionType = typeof(Exception);
        private readonly Type baseHttpResponseType = typeof(HttpResponse);

        public long Priority => -10000;

        public Action<IServiceCollection> DefaultServiceRegistrationDelegate => 
            serviceCollection => serviceCollection.AddMvcCore();
        
        public Action<IServiceCollection> ServiceRegistrationDelegate => 
            serviceCollection => serviceCollection.AddControllersTesting();

        public object TryGetValue(Type type, ComponentTestContext testContext)
            => Reflection.AreAssignable(controllerAttributesType, type) // ControllerAttributes
                ? new ControllerAttributes(testContext.ComponentAttributes)
                : Reflection.AreAssignable(actionAttributesType, type) // ActionAttributes
                ? new ActionAttributes(testContext.MethodAttributes)
                : Reflection.AreAssignable(exceptionType, type) // Exception
                ? testContext.CaughtException
                : Reflection.AreAssignable(baseHttpResponseType, type) // HttpResponse
                ? (object)testContext.HttpResponse
                : null;
    }
}
