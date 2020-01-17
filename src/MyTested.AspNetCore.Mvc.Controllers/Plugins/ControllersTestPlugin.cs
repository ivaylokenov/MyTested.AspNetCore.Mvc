namespace MyTested.AspNetCore.Mvc.Plugins
{
    using System;
    using Microsoft.Extensions.DependencyInjection;
    using Internal.TestContexts;
    using Microsoft.AspNetCore.Mvc;
    using Utilities;

    public class ControllersTestPlugin : BaseTestPlugin, IDefaultRegistrationPlugin, IServiceRegistrationPlugin, IShouldPassForPlugin
    {
        private readonly Type controllerAttributesType = typeof(ControllerAttributes);
        private readonly Type actionAttributesType = typeof(ActionAttributes);

        public long Priority => -10000;

        public Action<IServiceCollection> DefaultServiceRegistrationDelegate 
            => serviceCollection => serviceCollection.AddMvcCore();
        
        public Action<IServiceCollection> ServiceRegistrationDelegate 
            => serviceCollection => serviceCollection.AddControllersCoreTesting();

        public object TryGetValue(Type type, ComponentTestContext testContext)
            => Reflection.AreAssignable(this.controllerAttributesType, type) // ControllerAttributes
                ? new ControllerAttributes(testContext.ComponentAttributes)
                : Reflection.AreAssignable(this.actionAttributesType, type) // ActionAttributes
                ? new ActionAttributes(testContext.MethodAttributes)
                : testContext.MethodResult is ObjectResult objectResult // ObjectResult Model
                    && Reflection.AreAssignable(type, objectResult.Value?.GetType())
                ? objectResult.Value
                : null;
    }
}
