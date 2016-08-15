namespace MyTested.AspNetCore.Mvc.Test.InternalTests.ControllersTests
{
    using Internal.Services;
    using Internal.ViewFeatures;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Setups.Controllers;
    using Xunit;
    using Microsoft.Extensions.DependencyInjection.Extensions;

    public class ViewDataPropertyHelperTests
    {
        [Fact]
        public void GetPropertiesShouldNotThrowExceptionForNormalController()
        {
            MyApplication.IsUsingDefaultConfiguration();

            var helper = ViewDataPropertyHelper.GetViewDataProperties<MvcController>();

            var controller = new MvcController();
            var controllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    RequestServices = TestServiceProvider.Global
                }
            };

            var gotViewData = helper.ViewDataGetter(controller);

            Assert.NotNull(gotViewData);
            Assert.Same(gotViewData, controller.ViewData);
        }

        [Fact]
        public void GetPropertiesShouldNotThrowExceptionForPocoController()
        {
            MyApplication
                .IsUsingDefaultConfiguration()
                .WithServices(services =>
                {
                    services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
                });

            var helper = ViewDataPropertyHelper.GetViewDataProperties<FullPocoController>();

            var controllerContext = new ControllerContext();
            var controller = new FullPocoController
            {
                CustomControllerContext = controllerContext
            };
            
            var gotViewData = helper.ViewDataGetter(controller);

            Assert.NotNull(gotViewData);
            Assert.Same(gotViewData, controller.CustomViewData);
            
            MyApplication.IsUsingDefaultConfiguration();
        }
    }
}
