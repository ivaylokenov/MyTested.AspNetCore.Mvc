namespace MyTested.AspNetCore.Mvc.Test
{
    using System.Linq;
    using Internal.Application;
    using Internal.Contracts;
    using Internal.Routing;
    using Microsoft.AspNetCore.Mvc.Abstractions;
    using Microsoft.AspNetCore.Mvc.Internal;
    using Microsoft.AspNetCore.Mvc.RazorPages.Internal;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Setups.Common;
    using Xunit;
    using Setups;
    using Setups.Startups;

    public class ServicesTests
    {
        [Fact]
        public void WithoutAdditionalServicesTheDefaultActionInvokersShouldBeSet()
        {
            MyApplication.StartsFrom<DefaultStartup>();

            var services = TestApplication.Services;
            var actionInvokerProviders = services.GetServices<IActionInvokerProvider>().ToList();
            var modelBindingActionInvokerFactory = services.GetService<IModelBindingActionInvokerFactory>();

            Assert.Equal(2, actionInvokerProviders.Count);
            Assert.Contains(actionInvokerProviders, a => a.GetType() == typeof(ControllerActionInvokerProvider));
            Assert.Contains(actionInvokerProviders, a => a.GetType() == typeof(PageActionInvokerProvider));
            Assert.Null(modelBindingActionInvokerFactory);

            var routeServices = TestApplication.RoutingServices;
            var routeActionInvokerProviders = routeServices.GetServices<IActionInvokerProvider>().ToList();
            var routeModelBindingActionInvokerFactory = routeServices.GetService<IModelBindingActionInvokerFactory>();

            Assert.Equal(3, routeActionInvokerProviders.Count);

            var routeActionInvokerProvidersList = routeActionInvokerProviders.OrderByDescending(r => r.Order).ToList();

            Assert.True(routeActionInvokerProvidersList[0].GetType() == typeof(ModelBindingActionInvokerProvider));
            Assert.NotNull(routeModelBindingActionInvokerFactory);
            Assert.IsAssignableFrom<ModelBindingActionInvokerFactory>(routeModelBindingActionInvokerFactory);

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void CallingAddRoutingTestingShouldSetActionInvokers()
        {
            MyApplication.StartsFrom<CustomStartupWithRouteTesting>();

            var services = TestApplication.Services;
            var actionInvokerProviders = services.GetServices<IActionInvokerProvider>().ToList();
            var modelBindingActionInvokerFactory = services.GetService<IModelBindingActionInvokerFactory>();

            Assert.Equal(2, actionInvokerProviders.Count);
            Assert.Contains(actionInvokerProviders, a => a.GetType() == typeof(ControllerActionInvokerProvider));
            Assert.Contains(actionInvokerProviders, a => a.GetType() == typeof(PageActionInvokerProvider));
            Assert.Null(modelBindingActionInvokerFactory);

            var routeServices = TestApplication.RoutingServices;
            var routeActionInvokerProviders = routeServices.GetServices<IActionInvokerProvider>().ToList();
            var routeModelBindingActionInvokerFactory = routeServices.GetService<IModelBindingActionInvokerFactory>();

            Assert.Equal(3, routeActionInvokerProviders.Count);

            var routeActionInvokerProvidersList = routeActionInvokerProviders.OrderByDescending(r => r.Order).ToList();

            Assert.True(routeActionInvokerProvidersList[0].GetType() == typeof(ModelBindingActionInvokerProvider));
            Assert.NotNull(routeModelBindingActionInvokerFactory);
            Assert.IsAssignableFrom<ModelBindingActionInvokerFactory>(routeModelBindingActionInvokerFactory);

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void WithCustomImplementationsForTheRouteTestingTheCorrectServicesShouldBeSet()
        {
            MyApplication
                .StartsFrom<DefaultStartup>()
                .WithServices(customServices =>
                {
                    customServices.TryAddEnumerable(
                        ServiceDescriptor.Transient<IActionInvokerProvider, CustomActionInvokerProvider>());
                    customServices.TryAddSingleton<IModelBindingActionInvokerFactory, CustomModelBindingActionInvokerFactory>();
                });

            var services = TestApplication.Services;
            var actionInvokerProviders = services.GetServices<IActionInvokerProvider>().ToList();
            var modelBindingActionInvokerFactory = services.GetService<IModelBindingActionInvokerFactory>();

            Assert.Equal(3, actionInvokerProviders.Count);
            Assert.Contains(actionInvokerProviders, a => a.GetType() == typeof(ControllerActionInvokerProvider));
            Assert.Contains(actionInvokerProviders, a => a.GetType() == typeof(PageActionInvokerProvider));
            Assert.Contains(actionInvokerProviders, a => a.GetType() == typeof(CustomActionInvokerProvider));
            Assert.NotNull(modelBindingActionInvokerFactory);

            var routeServices = TestApplication.RoutingServices;
            var routeActionInvokerProviders = routeServices.GetServices<IActionInvokerProvider>().ToList();
            var routeModelBindingActionInvokerFactory = routeServices.GetService<IModelBindingActionInvokerFactory>();

            Assert.Equal(3, routeActionInvokerProviders.Count);

            var routeActionInvokerProvidersList = routeActionInvokerProviders.OrderByDescending(r => r.Order).ToList();

            Assert.True(routeActionInvokerProvidersList[0].GetType() == typeof(CustomActionInvokerProvider));
            Assert.NotNull(routeModelBindingActionInvokerFactory);
            Assert.IsAssignableFrom<CustomModelBindingActionInvokerFactory>(routeModelBindingActionInvokerFactory);

            MyApplication.StartsFrom<DefaultStartup>();
        }
    }
}
