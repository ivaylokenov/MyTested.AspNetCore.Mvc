namespace MyTested.AspNetCore.Mvc.Plugins
{
    using System;
    using Microsoft.Extensions.DependencyInjection;
    using Internal.TestContexts;
    using Utilities;

    public class ViewComponentsTestPlugin : BaseTestPlugin, IDefaultRegistrationPlugin, IServiceRegistrationPlugin, IShouldPassForPlugin
    {
        private readonly Type viewComponentAttributesType = typeof(ViewComponentAttributes);

        public long Priority => -1000;

        public Action<IServiceCollection> DefaultServiceRegistrationDelegate
            => serviceCollection => serviceCollection
                .AddMvcCore()
                .AddFormatterMappings()
                .AddViews()
                .AddDataAnnotations();
        
        public Action<IServiceCollection> ServiceRegistrationDelegate 
            => serviceCollection => serviceCollection.AddViewComponentsTesting();

        public object TryGetValue(Type type, ComponentTestContext testContext)
            => Reflection.AreAssignable(this.viewComponentAttributesType, type) // ViewComponentAttributes
                ? new ViewComponentAttributes(testContext.ComponentAttributes)
                : null;
    }
}
