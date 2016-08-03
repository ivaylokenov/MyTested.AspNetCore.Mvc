namespace MyTested.AspNetCore.Mvc.Test.InternalTests.ControllersTests
{
    using System;
    using Internal.Controllers;
    using Internal.Services;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.DependencyInjection;
    using Setups;
    using Setups.Controllers;
    using Xunit;

    public class ControllerPropertyHelperTests
    {
        [Fact]
        public void GetPropertiesShouldNotThrowExceptionForNormalController()
        {
            MyApplication.IsUsingDefaultConfiguration();

            var helper = ControllerPropertyHelper.GetProperties<MvcController>();

            var controller = new MvcController();
            var controllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    RequestServices = TestServiceProvider.Global
                }
            };

            controller.ControllerContext = controllerContext;

            Assert.NotNull(controller.ControllerContext);
            Assert.Same(controllerContext, controller.ControllerContext);

            var gotControllerContext = helper.ControllerContextGetter(controller);

            Assert.NotNull(gotControllerContext);
            Assert.Same(gotControllerContext, controller.ControllerContext);

            Test.AssertException<InvalidOperationException>(
                () =>
                {
                    var gotActionContext = helper.ActionContextGetter(controller);
                },
                "ActionContext could not be found on the provided MvcController. The property should be specified manually by providing controller instance or using the specified helper methods.");
        }

        [Fact]
        public void GetPropertiesShouldNotThrowExceptionForPocoController()
        {
            MyApplication
                .IsUsingDefaultConfiguration()
                .WithServices(services =>
                {
                    services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
                });

            var helper = ControllerPropertyHelper.GetProperties<FullPocoController>();

            var controllerContext = new ControllerContext();
            var controller = new FullPocoController
            {
                CustomControllerContext = controllerContext
            };

            Assert.NotNull(controller.CustomControllerContext);
            Assert.Same(controllerContext, controller.CustomControllerContext);

            var gotControllerContext = helper.ControllerContextGetter(controller);

            Assert.NotNull(gotControllerContext);
            Assert.Same(gotControllerContext, controller.CustomControllerContext);

            var actionContext = new ActionContext();

            controller.CustomActionContext = actionContext;

            Assert.NotNull(controller.CustomActionContext);
            Assert.Same(actionContext, controller.CustomActionContext);

            var gotActionContext = helper.ActionContextGetter(controller);

            Assert.NotNull(gotActionContext);
            Assert.Same(gotActionContext, controller.CustomActionContext);

            MyApplication.IsUsingDefaultConfiguration();
        }
    }
}
