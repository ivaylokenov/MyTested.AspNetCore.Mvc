namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.ControllersTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using Builders.Contracts.Actions;
    using Builders.Contracts.Base;
    using Exceptions;
    using Internal.Http;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Controllers;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Microsoft.Extensions.Primitives;
    using Setups;
    using Setups.Controllers;
    using Setups.Models;
    using Setups.Services;
    using Xunit;

    public class ControllerBuilderTests
    {
        [Fact]
        public void WithResolvedDependencyForShouldWorkCorrectlyWithNullValues()
        {
            MyController<MvcController>
                .Instance()
                .WithServiceFor<IInjectedService>(null)
                .WithServiceFor<RequestModel>(null)
                .WithServiceFor<IAnotherInjectedService>(null)
                .Calling(c => c.DefaultView())
                .ShouldReturn()
                .View();
        }

        [Fact]
        public void WithResolvedDependencyForShouldNotThrowExceptionWithNullValuesAndMoreThanOneSuitableConstructor()
        {
            MyController<MvcController>
                .Instance()
                .WithServiceFor<IInjectedService>(null)
                .Calling(c => c.DefaultView())
                .ShouldReturn()
                .View();
        }

        [Fact]
        public void WithNoResolvedDependencyForShouldNotThrowException()
        {
            MyController<MvcController>
                .Instance()
                .WithNoServiceFor<IInjectedService>()
                .Calling(c => c.DefaultView())
                .ShouldReturn()
                .View();
        }

        [Fact]
        public void WithTempDataShouldPopulateTempDataCorrectly()
        {
            MyController<MvcController>
                .Instance()
                .WithTempData(tempData =>
                {
                    tempData.WithEntry("test", "value");
                })
                .Calling(c => c.TempDataAction())
                .ShouldReturn()
                .Ok();
        }

        [Fact]
        public void WithTempDataShouldPopulateTempData()
        {
            MyController<MvcController>
                .Instance()
                .WithTempData(tempData => tempData
                    .WithEntry("key", "value"))
                .ShouldPassFor()
                .TheController(controller =>
                {
                    Assert.Equal(1, controller.TempData.Count);
                });
        }

        [Fact]
        public void WithResolvedDependencyForShouldWorkCorrectlyWithNullValuesForPocoController()
        {
            MyController<FullPocoController>
                .Instance()
                .WithServiceFor<IInjectedService>(null)
                .WithServiceFor<RequestModel>(null)
                .WithServiceFor<IAnotherInjectedService>(null)
                .Calling(c => c.DefaultView())
                .ShouldReturn()
                .View();
        }

        [Fact]
        public void WithResolvedDependencyForShouldNotThrowExceptionWithNullValuesAndMoreThanOneSuitableConstructorForPocoController()
        {
            MyController<FullPocoController>
                .Instance()
                .WithServiceFor<IInjectedService>(null)
                .Calling(c => c.DefaultView())
                .ShouldReturn()
                .View();
        }

        [Fact]
        public void WithNoResolvedDependencyForShouldNotThrowExceptionForPocoController()
        {
            MyController<FullPocoController>
                .Instance()
                .WithNoServiceFor<IInjectedService>()
                .Calling(c => c.DefaultView())
                .ShouldReturn()
                .View();
        }

        [Fact]
        public void WithTempDataShouldPopulateTempDataCorrectlyForPocoController()
        {
            MyApplication
                .IsUsingDefaultConfiguration()
                .WithServices(services =>
                {
                    services.AddHttpContextAccessor();
                });

            MyController<FullPocoController>
                .Instance()
                .WithTempData(tempData =>
                {
                    tempData.WithEntry("test", "value");
                })
                .Calling(c => c.TempDataAction())
                .ShouldReturn()
                .Ok();

            MyApplication.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void WithTempDataShouldPopulateTempDataForPocoController()
        {
            MyApplication
                .IsUsingDefaultConfiguration()
                .WithServices(services =>
                {
                    services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
                });

            MyController<FullPocoController>
                .Instance()
                .WithTempData(tempData => tempData
                    .WithEntry("key", "value"))
                .ShouldPassFor()
                .TheController(controller =>
                {
                    Assert.Equal(1, controller.CustomTempData.Count);
                });

            MyApplication.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void RouteDataShouldBePopulatedWhenRequestAndPathAreProvided()
        {
            MyApplication.IsUsingDefaultConfiguration();

            MyController<MvcController>
                .Instance()
                .WithHttpRequest(req => req.WithPath("/Mvc/WithRouteData/1"))
                .Calling(c => c.WithRouteData(1))
                .ShouldReturn()
                .View();
        }

        [Fact]
        public void RouteDataShouldBePopulatedWhenRequestAndPathAreProvidedForPocoController()
        {
            MyApplication
                .IsUsingDefaultConfiguration()
                .WithServices(services =>
                {
                    services.AddHttpContextAccessor();
                });

            MyController<FullPocoController>
                .Instance()
                .WithHttpRequest(req => req.WithPath("/Mvc/WithRouteData/1"))
                .Calling(c => c.WithRouteData(1))
                .ShouldReturn()
                .View();

            MyApplication.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void CallingShouldPopulateCorrectActionDescriptorForPocoController()
        {
            MyApplication
                .IsUsingDefaultConfiguration()
                .WithServices(services =>
                {
                    services.AddHttpContextAccessor();
                });

            MyController<FullPocoController>
                .Instance()
                .Calling(c => c.OkResultAction())
                .ShouldPassFor()
                .TheController(controller =>
                {
                    Assert.NotNull(controller);
                    Assert.NotNull((controller as FullPocoController).CustomControllerContext);
                    Assert.NotNull((controller as FullPocoController).CustomControllerContext.ActionDescriptor);
                    Assert.Equal("OkResultAction", (controller as FullPocoController).CustomControllerContext.ActionDescriptor.ActionName);
                });

            MyApplication.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void WithoutValidationShouldNotValidateTheRequestModelForPocoController()
        {
            MyApplication
                .IsUsingDefaultConfiguration()
                .WithServices(services =>
                {
                    services.AddHttpContextAccessor();
                });

            MyController<FullPocoController>
                .Instance()
                .WithoutValidation()
                .Calling(c => c.ModelStateCheck(TestObjectFactory.GetRequestModelWithErrors()))
                .ShouldHave()
                .ValidModelState();

            MyApplication.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void CallingShouldHaveValidModelStateWhenThereAreNoModelErrorsForPocoController()
        {
            MyApplication
                .IsUsingDefaultConfiguration()
                .WithServices(services =>
                {
                    services.AddHttpContextAccessor();
                });

            var requestModel = TestObjectFactory.GetValidRequestModel();

            MyController<FullPocoController>
                .Instance()
                .Calling(c => c.OkResultActionWithRequestBody(1, requestModel))
                .ShouldReturn()
                .Ok()
                .ShouldPassFor()
                .TheController(controller =>
                {
                    var modelState = (controller as FullPocoController).CustomControllerContext.ModelState;

                    Assert.True(modelState.IsValid);
                    Assert.Equal(0, modelState.Values.Count());
                    Assert.Equal(0, modelState.Keys.Count());
                });

            MyApplication.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void NormalIndexOutOfRangeExceptionShouldShowNormalMessageForPocoController()
        {
            MyApplication
                .IsUsingDefaultConfiguration()
                .WithServices(services =>
                {
                    services.AddHttpContextAccessor();
                });

            Assert.Throws<ActionCallAssertionException>(
                () =>
                {
                    MyController<FullPocoController>
                        .Instance()
                        .Calling(c => c.IndexOutOfRangeException())
                        .ShouldReturn()
                        .Ok();
                });

            MyApplication.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void CallingShouldPopulateCorrectActionNameAndActionResultWithAsyncActionCallForPocoController()
        {
            MyApplication
                .IsUsingDefaultConfiguration()
                .WithServices(services =>
                {
                    services.AddHttpContextAccessor();
                });

            var actionResultTestBuilder = MyController<FullPocoController>
                .Instance()
                .Calling(c => c.AsyncOkResultAction());

            this.CheckActionResultTestBuilder(actionResultTestBuilder, "AsyncOkResultAction");

            MyApplication.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void CallingShouldPopulateCorrectActionNameAndActionResultWithNormalActionCallForPocoController()
        {
            MyApplication
                .IsUsingDefaultConfiguration()
                .WithServices(services =>
                {
                    services.AddHttpContextAccessor();
                });

            var actionResultTestBuilder = MyController<FullPocoController>
                .Instance()
                .Calling(c => c.OkResultAction());

            this.CheckActionResultTestBuilder(actionResultTestBuilder, "OkResultAction");

            MyApplication.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void CallingShouldPopulateCorrectActionNameWithNormalVoidActionCallForPocoController()
        {
            MyApplication
                .IsUsingDefaultConfiguration()
                .WithServices(services =>
                {
                    services.AddHttpContextAccessor();
                });

            var voidActionResultTestBuilder = MyController<FullPocoController>
                .Instance()
                .Calling(c => c.EmptyAction());

            this.CheckActionName(voidActionResultTestBuilder, "EmptyAction");

            MyApplication.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void CallingShouldPopulateCorrectActionNameWithTaskActionCallForPocoController()
        {
            MyApplication
                .IsUsingDefaultConfiguration()
                .WithServices(services =>
                {
                    services.AddHttpContextAccessor();
                });

            var voidActionResultTestBuilder = MyController<FullPocoController>
                .Instance()
                .Calling(c => c.EmptyActionAsync());

            this.CheckActionName(voidActionResultTestBuilder, "EmptyActionAsync");

            MyApplication.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void CallingShouldPopulateModelStateWhenThereAreModelErrorsForPocoController()
        {
            MyApplication
                .IsUsingDefaultConfiguration()
                .WithServices(services =>
                {
                    services.AddHttpContextAccessor();
                });

            var requestModel = TestObjectFactory.GetRequestModelWithErrors();

            MyController<FullPocoController>
                .Instance()
                .Calling(c => c.OkResultActionWithRequestBody(1, requestModel))
                .ShouldReturn()
                .Ok()
                .ShouldPassFor()
                .TheController(controller =>
                {
                    var modelState = (controller as FullPocoController).CustomControllerContext.ModelState;

                    Assert.False(modelState.IsValid);
                    Assert.Equal(2, modelState.Values.Count());
                    Assert.Equal("Integer", modelState.Keys.First());
                    Assert.Equal("RequiredString", modelState.Keys.Last());

                });

            MyApplication.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void WithAuthenticatedUserShouldPopulateUserPropertyWithDefaultValuesForPocoController()
        {
            MyApplication
                .IsUsingDefaultConfiguration()
                .WithServices(services =>
                {
                    services.AddHttpContextAccessor();
                });

            MyController<FullPocoController>
                .Instance()
                .WithAuthenticatedUser()
                .Calling(c => c.AuthorizedAction())
                .ShouldReturn()
                .Ok()
                .ShouldPassFor()
                .TheController(controller =>
                {
                    var controllerUser = (controller as FullPocoController).CustomHttpContext.User;

                    Assert.Equal(false, controllerUser.IsInRole("Any"));
                    Assert.Equal("TestUser", controllerUser.Identity.Name);
                    Assert.True(controllerUser.HasClaim(ClaimTypes.Name, "TestUser"));
                    Assert.Equal("Passport", controllerUser.Identity.AuthenticationType);
                    Assert.Equal(true, controllerUser.Identity.IsAuthenticated);
                });

            MyApplication.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void WithAuthenticatedUserShouldPopulateProperUserWhenUserWithUserBuilderForPocoController()
        {
            MyApplication
                .IsUsingDefaultConfiguration()
                .WithServices(services =>
                {
                    services.AddHttpContextAccessor();
                });

            MyController<FullPocoController>
                .Instance()
                .WithAuthenticatedUser(user => user
                    .WithUsername("NewUserName")
                    .WithAuthenticationType("Custom")
                    .InRole("NormalUser")
                    .AndAlso()
                    .InRoles("Moderator", "Administrator")
                    .InRoles(new[]
                    {
                        "SuperUser",
                        "MegaUser"
                    }))
                .Calling(c => c.AuthorizedAction())
                .ShouldReturn()
                .Ok()
                .ShouldPassFor()
                .TheController(controller =>
                {
                    var controllerUser = (controller as FullPocoController).CustomHttpContext.User;

                    Assert.Equal("NewUserName", controllerUser.Identity.Name);
                    Assert.Equal("Custom", controllerUser.Identity.AuthenticationType);
                    Assert.Equal(true, controllerUser.Identity.IsAuthenticated);
                    Assert.Equal(true, controllerUser.IsInRole("NormalUser"));
                    Assert.Equal(true, controllerUser.IsInRole("Moderator"));
                    Assert.Equal(true, controllerUser.IsInRole("Administrator"));
                    Assert.Equal(true, controllerUser.IsInRole("SuperUser"));
                    Assert.Equal(true, controllerUser.IsInRole("MegaUser"));
                    Assert.Equal(false, controllerUser.IsInRole("AnotherRole"));
                });

            MyApplication.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void WithAuthenticatedNotCalledShouldNotHaveAuthorizedUserForPocoController()
        {
            MyApplication
                .IsUsingDefaultConfiguration()
                .WithServices(services =>
                {
                    services.AddHttpContextAccessor();
                });

            MyController<FullPocoController>
                .Instance()
                .Calling(c => c.AuthorizedAction())
                .ShouldReturn()
                .NotFound()
                .ShouldPassFor()
                .TheController(controller =>
                {
                    var controllerUser = (controller as FullPocoController).CustomHttpContext.User;

                    Assert.Equal(false, controllerUser.IsInRole("Any"));
                    Assert.Equal(null, controllerUser.Identity.Name);
                    Assert.Equal(null, controllerUser.Identity.AuthenticationType);
                    Assert.Equal(false, controllerUser.Identity.IsAuthenticated);
                });

            MyApplication.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void WithResolvedDependencyForShouldChooseCorrectConstructorWithLessDependenciesForPocoController()
        {
            MyController<FullPocoController>
                .Instance()
                .WithServiceFor<IInjectedService>(new InjectedService())
                .ShouldPassFor()
                .TheController(controller =>
                {
                    Assert.NotNull(controller);
                    Assert.NotNull(controller.InjectedService);
                    Assert.Null(controller.AnotherInjectedService);
                    Assert.Null(controller.InjectedRequestModel);
                });

            MyApplication.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void WithResolvedDependencyForShouldChooseCorrectConstructorWithMoreDependenciesForPocoController()
        {
            MyController<FullPocoController>
                .Instance()
                .WithServiceFor<IAnotherInjectedService>(new AnotherInjectedService())
                .WithServiceFor<IInjectedService>(new InjectedService())
                .ShouldPassFor()
                .TheController(controller =>
                {
                    Assert.NotNull(controller);
                    Assert.NotNull(controller.InjectedService);
                    Assert.NotNull(controller.AnotherInjectedService);
                    Assert.Null(controller.InjectedRequestModel);
                });

            MyApplication.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void WithResolvedDependencyForShouldChooseCorrectConstructorWithAllDependenciesForPocoController()
        {
            MyController<FullPocoController>
                .Instance()
                .WithServiceFor<IAnotherInjectedService>(new AnotherInjectedService())
                .WithServiceFor<RequestModel>(new RequestModel())
                .WithServiceFor<IInjectedService>(new InjectedService())
                .ShouldPassFor()
                .TheController(controller =>
                {
                    Assert.NotNull(controller);
                    Assert.NotNull(controller.InjectedService);
                    Assert.NotNull(controller.AnotherInjectedService);
                    Assert.NotNull(controller.InjectedRequestModel);
                });

            MyApplication.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void WithResolvedDependencyForShouldContinueTheNormalExecutionFlowOfTestBuildersForPocoController()
        {
            MyController<FullPocoController>
                .Instance()
                .WithServiceFor(new RequestModel())
                .WithServiceFor(new AnotherInjectedService())
                .WithServiceFor(new InjectedService())
                .WithAuthenticatedUser()
                .Calling(c => c.OkResultAction())
                .ShouldReturn()
                .Ok();
        }

        [Fact]
        public void AndAlsoShouldContinueTheNormalExecutionFlowOfTestBuildersForPocoController()
        {
            MyController<FullPocoController>
                .Instance()
                .WithServiceFor(new RequestModel())
                .AndAlso()
                .WithServiceFor(new AnotherInjectedService())
                .AndAlso()
                .WithServiceFor(new InjectedService())
                .AndAlso()
                .WithAuthenticatedUser()
                .Calling(c => c.OkResultAction())
                .ShouldReturn()
                .Ok();
        }

        [Fact]
        public void WithResolvedDependenciesShouldWorkCorrectWithCollectionOfObjectsForPocoController()
        {
            MyController<FullPocoController>
                .Instance()
                .WithServices(new List<object> { new RequestModel(), new AnotherInjectedService(), new InjectedService() })
                .WithAuthenticatedUser()
                .Calling(c => c.OkResultAction())
                .ShouldReturn()
                .Ok();
        }

        [Fact]
        public void WithResolvedDependenciesShouldWorkCorrectWithParamsOfObjectsForPocoController()
        {
            MyController<FullPocoController>
                .Instance()
                .WithServices(new RequestModel(), new AnotherInjectedService(), new InjectedService())
                .WithAuthenticatedUser()
                .Calling(c => c.OkResultAction())
                .ShouldReturn()
                .Ok();
        }

        [Fact]
        public void WithResolvedDependencyForShouldThrowExceptionWhenSameDependenciesAreRegisteredForPocoController()
        {
            Test.AssertException<InvalidOperationException>(
                () =>
                {
                    MyController<FullPocoController>
                        .Instance()
                        .WithServiceFor<RequestModel>(new RequestModel())
                        .WithServiceFor<IAnotherInjectedService>(new AnotherInjectedService())
                        .WithServiceFor<IInjectedService>(new InjectedService())
                        .WithServiceFor<IAnotherInjectedService>(new AnotherInjectedService());
                },
                "Dependency AnotherInjectedService is already registered for FullPocoController controller.");
        }
        
        [Fact]
        public void PrepareControllerShouldSetCorrectPropertiesWithCustomSetupForPocoController()
        {
            MyApplication
                .IsUsingDefaultConfiguration()
                .WithServices(services =>
                {
                    services.AddHttpContextAccessor();
                });

            MyController<FullPocoController>
                .Instance()
                .WithSetup(c =>
                {
                    c.PublicProperty = new object();
                })
                .ShouldPassFor()
                .TheController(controller =>
                {
                    Assert.NotNull(controller);
                    Assert.NotNull(controller.PublicProperty);
                });

            MyApplication.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void WithHttpContextSessionShouldThrowExceptionIfSessionIsNotRegisteredForPocoController()
        {
            MyApplication
                .IsUsingDefaultConfiguration()
                .WithServices(services =>
                {
                    services.AddHttpContextAccessor();
                });

            var httpContext = new DefaultHttpContext();

            MyController<FullPocoController>
                .Instance()
                .WithHttpContext(httpContext)
                .ShouldPassFor()
                .TheHttpContext(context =>
                {
                    Assert.Throws<InvalidOperationException>(() => context.Session);
                });
        }

        [Fact]
        public void WithHttpContextShouldPopulateCustomHttpContextForPocoController()
        {
            MyApplication
                .IsUsingDefaultConfiguration()
                .WithServices(services =>
                {
                    services.AddHttpContextAccessor();
                });

            var httpContext = new DefaultHttpContext();
            httpContext.Request.Scheme = "Custom";
            httpContext.Response.StatusCode = 404;
            httpContext.Response.ContentType = ContentType.ApplicationJson;
            httpContext.Response.ContentLength = 100;

            MyController<FullPocoController>
                .Instance()
                .WithHttpContext(httpContext)
                .ShouldPassFor()
                .TheHttpContext(setHttpContext =>
                {
                    Assert.Equal("Custom", setHttpContext.Request.Scheme);
                    Assert.IsAssignableFrom<MockedHttpResponse>(setHttpContext.Response);
                    Assert.Same(httpContext.Response.Body, setHttpContext.Response.Body);
                    Assert.Equal(httpContext.Response.ContentLength, setHttpContext.Response.ContentLength);
                    Assert.Equal(httpContext.Response.ContentType, setHttpContext.Response.ContentType);
                    Assert.Equal(httpContext.Response.StatusCode, setHttpContext.Response.StatusCode);
                    Assert.Same(httpContext.Items, setHttpContext.Items);
                    Assert.Same(httpContext.Features, setHttpContext.Features);
                    Assert.Same(httpContext.RequestServices, setHttpContext.RequestServices);
                    Assert.Same(httpContext.TraceIdentifier, setHttpContext.TraceIdentifier);
                    Assert.Same(httpContext.User, setHttpContext.User);
                });

            MyController<FullPocoController>
                .Instance()
                .WithHttpContext(httpContext)
                .ShouldPassFor()
                .TheController(controller =>
                {
                    Assert.Equal("Custom", controller.CustomHttpContext.Request.Scheme);
                    Assert.IsAssignableFrom<MockedHttpResponse>(controller.CustomHttpContext.Response);
                    Assert.Same(httpContext.Response.Body, controller.CustomHttpContext.Response.Body);
                    Assert.Equal(httpContext.Response.ContentLength, controller.CustomHttpContext.Response.ContentLength);
                    Assert.Equal(httpContext.Response.ContentType, controller.CustomHttpContext.Response.ContentType);
                    Assert.Equal(httpContext.Response.StatusCode, controller.CustomHttpContext.Response.StatusCode);
                    Assert.Same(httpContext.Items, controller.CustomHttpContext.Items);
                    Assert.Same(httpContext.Features, controller.CustomHttpContext.Features);
                    Assert.Same(httpContext.RequestServices, controller.CustomHttpContext.RequestServices);
                    Assert.Same(httpContext.TraceIdentifier, controller.CustomHttpContext.TraceIdentifier);
                    Assert.Same(httpContext.User, controller.CustomHttpContext.User);
                });

            MyApplication.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void WithHttpContextSetupShouldPopulateContextPropertiesForPocoController()
        {
            MyApplication
                .IsUsingDefaultConfiguration()
                .WithServices(services =>
                {
                    services.AddHttpContextAccessor();
                });

            MyController<FullPocoController>
                .Instance()
                .WithHttpContext(httpContext =>
                {
                    httpContext.Request.ContentType = ContentType.ApplicationOctetStream;
                })
                .ShouldPassFor()
                .TheController(controller =>
                {
                    Assert.Equal(ContentType.ApplicationOctetStream, controller.CustomHttpContext.Request.ContentType);
                });

            MyApplication.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void WithRequestShouldNotWorkWithDefaultRequestActionForPocoController()
        {
            MyApplication
                .IsUsingDefaultConfiguration()
                .WithServices(services =>
                {
                    services.AddHttpContextAccessor();
                });

            MyController<FullPocoController>
                .Instance()
                .Calling(c => c.WithRequest())
                .ShouldReturn()
                .BadRequest();

            MyApplication.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void WithRequestShouldWorkWithSetRequestActionForPocoController()
        {
            MyApplication
                .IsUsingDefaultConfiguration()
                .WithServices(services =>
                {
                    services.AddHttpContextAccessor();
                });

            MyController<FullPocoController>
                .Instance()
                .WithHttpRequest(req => req.WithFormField("Test", "TestValue"))
                .Calling(c => c.WithRequest())
                .ShouldReturn()
                .Ok();

            MyApplication.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void WithRequestAsObjectShouldWorkWithSetRequestActionForPocoController()
        {
            MyApplication
                .IsUsingDefaultConfiguration()
                .WithServices(services =>
                {
                    services.AddHttpContextAccessor();
                });

            var httpContext = new MockedHttpContext();
            httpContext.Request.Form = new FormCollection(new Dictionary<string, StringValues> { ["Test"] = "TestValue" });

            MyController<FullPocoController>
                .Instance()
                .WithHttpRequest(httpContext.Request)
                .Calling(c => c.WithRequest())
                .ShouldReturn()
                .Ok();

            MyApplication.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void UsingUrlHelperInsideControllerShouldWorkCorrectlyForPocoController()
        {
            MyApplication
                .IsUsingDefaultConfiguration()
                .WithServices(services =>
                {
                    services.AddHttpContextAccessor();
                });

            MyController<FullPocoController>
                .Instance()
                .WithRouteData()
                .Calling(c => c.UrlAction())
                .ShouldReturn()
                .Ok()
                .WithModel("/FullPoco/UrlAction");

            MyApplication.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void UnresolvedRouteValuesShouldHaveFriendlyExceptionForPocoController()
        {
            MyApplication
                .IsUsingDefaultConfiguration()
                .WithServices(services =>
                {
                    services.AddHttpContextAccessor();
                });

            Test.AssertException<InvalidOperationException>(
                () =>
                {
                    MyController<FullPocoController>
                        .Instance()
                        .Calling(c => c.UrlAction())
                        .ShouldReturn()
                        .Ok()
                        .WithModel("");
                },
                "Route values are not present in the action call but are needed for successful pass of this test case. Consider calling 'WithResolvedRouteValues' on the controller builder to resolve them from the provided lambda expression or set the HTTP request path by using 'WithHttpRequest'.");

            MyApplication.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void PrepareControllerShouldSetCorrectPropertiesWithDefaultServicesForPocoController()
        {
            MyApplication
                .IsUsingDefaultConfiguration()
                .WithServices(services =>
                {
                    services.AddHttpContextAccessor();
                });

            MyController<FullPocoController>
                .Instance()
                .ShouldPassFor()
                .TheController(controller =>
                {
                    Assert.NotNull(controller);
                    Assert.NotNull(controller.CustomHttpContext);
                    Assert.NotNull(controller.CustomActionContext);
                    Assert.NotNull(controller.CustomControllerContext);
                    Assert.NotNull(controller.CustomControllerContext.HttpContext);
                    Assert.NotNull(controller.CustomHttpContext.Request);
                    Assert.NotNull(controller.CustomHttpContext.Response);
                    Assert.NotNull(controller.CustomControllerContext.ModelState);
                    Assert.NotNull(controller.CustomHttpContext.User);
                    Assert.Null(controller.CustomControllerContext.ActionDescriptor);
                });

            MyApplication.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void PrepareControllerShouldSetCorrectPropertiesWithDefaultServices()
        {
            MyController<MvcController>
                .Instance()
                .ShouldPassFor()
                .TheController(controller =>
                {
                    Assert.NotNull(controller);
                    Assert.NotNull(controller.HttpContext);
                    Assert.NotNull(controller.HttpContext.RequestServices);
                    Assert.NotNull(controller.ControllerContext);
                    Assert.NotNull(controller.ControllerContext.HttpContext);
                    Assert.NotNull(controller.ViewBag);
                    Assert.NotNull(controller.ViewData);
                    Assert.NotNull(controller.TempData);
                    Assert.NotNull(controller.Request);
                    Assert.NotNull(controller.Response);
                    Assert.NotNull(controller.MetadataProvider);
                    Assert.NotNull(controller.ModelState);
                    Assert.NotNull(controller.ObjectValidator);
                    Assert.NotNull(controller.Url);
                    Assert.NotNull(controller.User);
                    Assert.Null(controller.ControllerContext.ActionDescriptor);
                });
        }
        
        [Fact]
        public void WithControllerContextShouldSetControllerContextForPocoController()
        {
            MyApplication
                .IsUsingDefaultConfiguration()
                .WithServices(services =>
                {
                    services.AddHttpContextAccessor();
                });

            var controllerContext = new ControllerContext
            {
                ActionDescriptor = new ControllerActionDescriptor
                {
                    ActionName = "Test"
                }
            };

            MyController<FullPocoController>
                .Instance()
                .WithControllerContext(controllerContext)
                .ShouldPassFor()
                .TheController(controller =>
                {
                    Assert.NotNull(controller);
                    Assert.NotNull(controller.CustomControllerContext);
                    Assert.Equal("Test", controller.CustomControllerContext.ActionDescriptor.ActionName);
                });

            MyApplication.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void WithControllerContextSetupShouldSetCorrectControllerContextForPocoController()
        {
            MyApplication
                .IsUsingDefaultConfiguration()
                .WithServices(services =>
                {
                    services.AddHttpContextAccessor();
                });

            MyController<FullPocoController>
                .Instance()
                .WithControllerContext(controllerContext =>
                {
                    controllerContext.RouteData.Values.Add("testkey", "testvalue");
                })
                .ShouldPassFor()
                .TheController(controller =>
                {
                    Assert.NotNull(controller);
                    Assert.NotNull(controller.CustomControllerContext);
                    Assert.True(controller.CustomControllerContext.RouteData.Values.ContainsKey("testkey"));
                });

            MyApplication.IsUsingDefaultConfiguration();
        }

        private void CheckActionResultTestBuilder<TActionResult>(
            IActionResultTestBuilder<TActionResult> actionResultTestBuilder,
            string expectedActionName)
        {
            this.CheckActionName(actionResultTestBuilder, expectedActionName);

            actionResultTestBuilder
                .ShouldPassFor()
                .TheActionResult(actionResult =>
                {
                    Assert.NotNull(actionResult);
                    Assert.IsAssignableFrom<OkResult>(actionResult);
                });
        }

        private void CheckActionName(IBaseTestBuilderWithInvokedAction testBuilder, string expectedActionName)
        {
            testBuilder
                .ShouldPassFor()
                .TheAction(actionName =>
                {
                    Assert.NotNull(actionName);
                    Assert.NotEmpty(actionName);
                    Assert.Equal(expectedActionName, actionName);
                });
        }
    }
}
