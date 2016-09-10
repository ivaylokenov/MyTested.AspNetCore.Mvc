namespace MyTested.AspNetCore.Mvc.Test.InternalTests.ControllersTests
{
    using Internal;
    using Internal.Services;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Setups.Controllers;
    using Xunit;

    public class TempDataControllerPropertyHelperTests
    {
        [Fact]
        public void GetPropertiesShouldNotThrowExceptionForNormalController()
        {
            MyApplication.IsUsingDefaultConfiguration();

            var helper = TempDataPropertyHelper.GetTempDataProperties<MvcController>();

            var controller = new MvcController
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext
                    {
                        RequestServices = TestServiceProvider.Global
                    }
                }
            };
            
            var tempData = controller.TempData;
            var gotTempData = helper.TempDataGetter(controller);

            Assert.NotNull(gotTempData);
            Assert.Same(gotTempData, controller.TempData);
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

            var helper = TempDataPropertyHelper.GetTempDataProperties<FullPocoController>();

            var controllerContext = new ControllerContext();
            var controller = new FullPocoController
            {
                CustomControllerContext = controllerContext
            };
            
            var gotTempData = helper.TempDataGetter(controller);

            Assert.NotNull(gotTempData);
            Assert.Same(gotTempData, controller.CustomTempData);

            MyApplication.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void GetPropertiesShouldNotThrowExceptionForPrivateProperties()
        {
            MyApplication.IsUsingDefaultConfiguration();

            var helper = TempDataPropertyHelper.GetTempDataProperties<PrivatePocoController>();

            var controller = new PrivatePocoController(TestServiceProvider.Global);

            var gotTempData = helper.TempDataGetter(controller);
            Assert.NotNull(gotTempData);
        }
    }
}
