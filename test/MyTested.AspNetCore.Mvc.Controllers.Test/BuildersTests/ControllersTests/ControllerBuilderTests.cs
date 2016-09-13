namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.ControllersTests
{
    using System;
    using Builders.Contracts.Actions;
    using Builders.Contracts.Base;
    using Exceptions;
    using Microsoft.AspNetCore.Mvc;
    using Setups;
    using Setups.Controllers;
    using Setups.Models;
    using Xunit;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc.Controllers;
    using Builders.Base;

    public class ControllerBuilderTests
    {
        [Fact]
        public void CallingShouldPopulateCorrectActionNameAndActionResultWithNormalActionCall()
        {
            var actionResultTestBuilder = MyController<MvcController>
                .Instance()
                .Calling(c => c.OkResultAction());

            this.CheckActionResultTestBuilder(actionResultTestBuilder, "OkResultAction");
        }

        [Fact]
        public void CallingShouldPopulateCorrectActionNameAndActionResultWithAsyncActionCall()
        {
            var actionResultTestBuilder = MyController<MvcController>
                .Instance()
                .Calling(c => c.AsyncOkResultAction());

            this.CheckActionResultTestBuilder(actionResultTestBuilder, "AsyncOkResultAction");
        }

        [Fact]
        public void CallingShouldPopulateCorrectActionNameWithNormalVoidActionCall()
        {
            var voidActionResultTestBuilder = MyController<MvcController>
                .Instance()
                .Calling(c => c.EmptyAction());

            this.CheckActionName(voidActionResultTestBuilder, "EmptyAction");
        }

        [Fact]
        public void CallingShouldPopulateCorrectActionNameWithTaskActionCall()
        {
            var voidActionResultTestBuilder = MyController<MvcController>
                .Instance()
                .Calling(c => c.EmptyActionAsync());

            this.CheckActionName(voidActionResultTestBuilder, "EmptyActionAsync");
        }

        [Fact]
        public void WithAuthenticatedNotCalledShouldNotHaveAuthorizedUser()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.AuthorizedAction())
                .ShouldReturn()
                .NotFound()
                .ShouldPassForThe<MvcController>(controller =>
                {
                    var controllerUser = controller.User;

                    Assert.Equal(false, controllerUser.IsInRole("Any"));
                    Assert.Equal(null, controllerUser.Identity.Name);
                    Assert.Equal(null, controllerUser.Identity.AuthenticationType);
                    Assert.Equal(false, controllerUser.Identity.IsAuthenticated);
                });
        }

        [Fact]
        public void PrepareControllerShouldSetCorrectPropertiesWithCustomSetup()
        {
            MyController<MvcController>
                .Instance()
                .WithSetup(c =>
                {
                    c.ControllerContext = new ControllerContext();
                })
                .ShouldPassForThe<MvcController>(controller =>
                {
                    Assert.NotNull(controller);
                    Assert.NotNull(controller.ControllerContext);
                    Assert.Null(controller.ControllerContext.HttpContext);
                });
        }

        [Fact]
        public void CallingShouldPopulateCorrectActionDescriptor()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.OkResultAction())
                .ShouldPassForThe<MvcController>(controller =>
                {
                    Assert.NotNull(controller);
                    Assert.NotNull((controller as Controller).ControllerContext);
                    Assert.NotNull((controller as Controller).ControllerContext.ActionDescriptor);
                    Assert.Equal("OkResultAction", (controller as Controller).ControllerContext.ActionDescriptor.ActionName);
                });
        }

        [Fact]
        public void UsingTryUpdateModelAsyncShouldWorkCorrectly()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.TryUpdateModelAction())
                .ShouldReturn()
                .Ok();
        }

        [Fact]
        public void UnresolvedRouteValuesShouldHaveFriendlyException()
        {
            Test.AssertException<InvalidOperationException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.UrlAction())
                        .ShouldReturn()
                        .Ok()
                        .WithModel("");
                },
                "Route values are not present in the method call but are needed for successful pass of this test case. Consider calling 'WithRouteData' on the component builder to resolve them from the provided lambda expression or set the HTTP request path by using 'WithHttpRequest'.");
        }

        [Fact]
        public void NormalIndexOutOfRangeExceptionShouldShowNormalMessage()
        {
            Assert.Throws<InvocationAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.IndexOutOfRangeException())
                        .ShouldReturn()
                        .Ok();
                });
        }

        [Fact]
        public void WithControllerContextShouldSetControllerContext()
        {
            var controllerContext = new ControllerContext
            {
                ActionDescriptor = new ControllerActionDescriptor
                {
                    ActionName = "Test"
                }
            };

            MyController<MvcController>
                .Instance()
                .WithControllerContext(controllerContext)
                .ShouldPassForThe<MvcController>(controller =>
                {
                    Assert.NotNull(controller);
                    Assert.NotNull(controller.ControllerContext);
                    Assert.Equal("Test", controller.ControllerContext.ActionDescriptor.ActionName);
                });
        }

        [Fact]
        public void WithControllerContextSetupShouldSetCorrectControllerContext()
        {
            MyController<MvcController>
                .Instance()
                .WithControllerContext(controllerContext =>
                {
                    controllerContext.RouteData.Values.Add("testkey", "testvalue");
                })
                .ShouldPassForThe<MvcController>(controller =>
                {
                    Assert.NotNull(controller);
                    Assert.NotNull(controller.ControllerContext);
                    Assert.True(controller.ControllerContext.RouteData.Values.ContainsKey("testkey"));
                });
        }

        [Fact]
        public void CallingShouldWorkCorrectlyWithFromServices()
        {
            MyApplication
                .IsUsingDefaultConfiguration()
                .WithServices(services =>
                {
                    services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
                });

            MyController<MvcController>
                .Instance()
                .Calling(c => c.WithService(From.Services<IHttpContextAccessor>()))
                .ShouldReturn()
                .Ok();

            MyApplication.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void InvalidControllerTypeShouldThrowException()
        {
            Test.AssertException<InvalidOperationException>(
                () =>
                {
                    MyController<RequestModel>.Instance();
                },
                "RequestModel is not a valid controller type.");
        }

        [Fact]
        public void PrepareControllerShouldSetCorrectPropertiesWithDefaultServices()
        {
            MyController<MvcController>
                .Instance()
                .ShouldPassForThe<MvcController>(controller =>
                {
                    Assert.NotNull(controller);
                    Assert.NotNull(controller.HttpContext);
                    Assert.NotNull(controller.HttpContext.RequestServices);
                    Assert.NotNull(controller.ControllerContext);
                    Assert.NotNull(controller.ControllerContext.HttpContext);
                    Assert.NotNull(controller.ViewBag);
                    Assert.NotNull(controller.ViewData);
                    Assert.NotNull(controller.Request);
                    Assert.NotNull(controller.Response);
                    Assert.NotNull(controller.MetadataProvider);
                    Assert.NotNull(controller.ModelState);
                    Assert.NotNull(controller.ObjectValidator);
                    Assert.NotNull(controller.Url);
                    Assert.NotNull(controller.User);
                    Assert.NotNull(controller.ControllerContext.ActionDescriptor);
                    Assert.NotNull(controller.ControllerContext.ValueProviderFactories);
                    Assert.Empty(controller.ControllerContext.ValueProviderFactories);
                    Assert.NotNull(controller.ControllerContext.ActionDescriptor.ActionConstraints);
                    Assert.Empty(controller.ControllerContext.ActionDescriptor.ActionConstraints);
                    Assert.NotNull(controller.ControllerContext.ActionDescriptor.AttributeRouteInfo);
                    Assert.NotNull(controller.ControllerContext.ActionDescriptor.BoundProperties);
                    Assert.Empty(controller.ControllerContext.ActionDescriptor.BoundProperties);
                    Assert.NotNull(controller.ControllerContext.ActionDescriptor.FilterDescriptors);
                    Assert.Empty(controller.ControllerContext.ActionDescriptor.FilterDescriptors);
                    Assert.NotNull(controller.ControllerContext.ActionDescriptor.Parameters);
                    Assert.Empty(controller.ControllerContext.ActionDescriptor.Parameters);
                    Assert.NotNull(controller.ControllerContext.ActionDescriptor.Properties);
                    Assert.Empty(controller.ControllerContext.ActionDescriptor.Properties);
                    Assert.NotNull(controller.ControllerContext.ActionDescriptor.RouteValues);
                    Assert.Empty(controller.ControllerContext.ActionDescriptor.RouteValues);
                });
        }

        private void CheckActionResultTestBuilder<TActionResult>(
            IActionResultTestBuilder<TActionResult> actionResultTestBuilder,
            string expectedActionName)
        {
            this.CheckActionName(actionResultTestBuilder, expectedActionName);

            actionResultTestBuilder
                .ShouldPassForThe<IActionResult>(actionResult =>
                {
                    Assert.NotNull(actionResult);
                    Assert.IsAssignableFrom<OkResult>(actionResult);
                });
        }

        private void CheckActionName(IBaseTestBuilderWithInvokedAction testBuilder, string expectedActionName)
        {
            var actionName = (testBuilder as BaseTestBuilderWithInvokedAction)?.ActionName;

            Assert.NotNull(actionName);
            Assert.NotEmpty(actionName);
            Assert.Equal(expectedActionName, actionName);
        }
    }
}
