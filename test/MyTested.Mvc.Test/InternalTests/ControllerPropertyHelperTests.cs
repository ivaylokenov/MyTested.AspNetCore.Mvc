namespace MyTested.Mvc.Test.InternalTests
{
    using Internal;
    using Internal.Application;
    using Microsoft.AspNetCore.Http.Internal;
    using Microsoft.AspNetCore.Mvc;
    using Setups;
    using Setups.Controllers;
    using System;
    using Xunit;

    public class ControllerPropertyHelperTests
    {
        [Fact]
        public void GetPropertiesShouldNotThrowExceptionForNormalController()
        {
            MyMvc.IsUsingDefaultConfiguration();

            var helper = ControllerPropertyHelper.GetProperties<MvcController>();

            var controller = new MvcController();
            var controllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    RequestServices = TestServiceProvider.Global
                }
            };

            helper.ControllerContextSetter(controller, controllerContext);

            Assert.NotNull(controller.ControllerContext);
            Assert.Same(controllerContext, controller.ControllerContext);

            var gotControllerContext = helper.ControllerContextGetter(controller);

            Assert.NotNull(gotControllerContext);
            Assert.Same(gotControllerContext, controller.ControllerContext);

            var actionContext = new ActionContext();

            Test.AssertException<InvalidOperationException>(
                () =>
                {
                    helper.ActionContextSetter(controller, actionContext);
                }, "ActionContext could not be found on the provided MvcController. The property should be specified manually by providing controller instance or using the specified helper methods.");

            Test.AssertException<InvalidOperationException>(
                () =>
                {
                    var gotActionContext = helper.ActionContextGetter(controller);
                }, "ActionContext could not be found on the provided MvcController. The property should be specified manually by providing controller instance or using the specified helper methods.");
            
            var gotViewData = helper.ViewDataGetter(controller);

            Assert.NotNull(gotViewData);
            Assert.Same(gotViewData, controller.ViewData);

            var tempData = controller.TempData;
            var gotTempData = helper.TempDataGetter(controller);

            Assert.NotNull(gotTempData);
            Assert.Same(gotTempData, controller.TempData);
        }

        [Fact]
        public void GetPropertiesShouldNotThrowExceptionForPocoController()
        {
            MyMvc.IsUsingDefaultConfiguration();

            var helper = ControllerPropertyHelper.GetProperties<FullPocoController>();
            
            var controller = new FullPocoController(TestServiceProvider.Global);
            var controllerContext = new ControllerContext();

            helper.ControllerContextSetter(controller, controllerContext);

            Assert.NotNull(controller.CustomControllerContext);
            Assert.Same(controllerContext, controller.CustomControllerContext);

            var gotControllerContext = helper.ControllerContextGetter(controller);

            Assert.NotNull(gotControllerContext);
            Assert.Same(gotControllerContext, controller.CustomControllerContext);

            var actionContext = new ActionContext();
            
            helper.ActionContextSetter(controller, actionContext);

            Assert.NotNull(controller.CustomActionContext);
            Assert.Same(actionContext, controller.CustomActionContext);

            var gotActionContext = helper.ActionContextGetter(controller);

            Assert.NotNull(gotActionContext);
            Assert.Same(gotActionContext, controller.CustomActionContext);
            
            var gotViewData = helper.ViewDataGetter(controller);

            Assert.NotNull(gotViewData);
            Assert.Same(gotViewData, controller.CustomViewData);
            
            var gotTempData = helper.TempDataGetter(controller);

            Assert.NotNull(gotTempData);
            Assert.Same(gotTempData, controller.CustomTempData);
        }

        [Fact]
        public void GetPropertiesShouldNotThrowExceptionForPrivateProperties()
        {
            MyMvc.IsUsingDefaultConfiguration();

            var helper = ControllerPropertyHelper.GetProperties<PrivatePocoController>();

            var controller = new PrivatePocoController(TestServiceProvider.Global);

            var gotTempData = helper.TempDataGetter(controller);
            Assert.NotNull(gotTempData);
        }
    }
}
