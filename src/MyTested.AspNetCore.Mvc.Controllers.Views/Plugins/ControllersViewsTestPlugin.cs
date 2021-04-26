namespace MyTested.AspNetCore.Mvc.Plugins
{
    using System;
    using Internal.TestContexts;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.DependencyInjection;

    public class ControllersViewsTestPlugin : IDefaultRegistrationPlugin, IShouldPassForPlugin
    {
        public long Priority => -1000;

        public Action<IServiceCollection> DefaultServiceRegistrationDelegate
            => serviceCollection => serviceCollection
                .AddMvcCore()
                .AddFormatterMappings()
                .AddViews()
                .AddDataAnnotations();

        public object TryGetValue(Type type, ComponentTestContext testContext)
            => testContext.MethodResult switch
            {
                JsonResult jsonResult => jsonResult.Value,
                ViewResult viewResult => viewResult.Model,
                PartialViewResult partialViewResult => partialViewResult.Model,
                ViewComponentResult viewComponentResult => viewComponentResult.Model,
                _ => null
            };
    }
}
