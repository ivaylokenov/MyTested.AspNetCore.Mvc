namespace MyTested.Mvc.Test.BuildersTests.ControllersTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using Builders.Contracts.Actions;
    using Builders.Contracts.Base;
    using Exceptions;
    using Microsoft.AspNetCore.Http.Internal;
    using Microsoft.AspNetCore.Mvc;
    using Setups;
    using Setups.Controllers;
    using Setups.Models;
    using Setups.Services;
    using Xunit;
    using Internal.Http;
    using Microsoft.Extensions.Primitives;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    public class ControllerBuilderTests
    {
        [Fact]
        public void CallingShouldPopulateCorrectActionNameAndActionResultWithNormalActionCall()
        {
            var actionResultTestBuilder = MyMvc
                .Controller<MvcController>()
                .Calling(c => c.OkResultAction());

            this.CheckActionResultTestBuilder(actionResultTestBuilder, "OkResultAction");
        }

        [Fact]
        public void CallingShouldPopulateCorrectActionNameAndActionResultWithAsyncActionCall()
        {
            var actionResultTestBuilder = MyMvc
                .Controller<MvcController>()
                .Calling(c => c.AsyncOkResultAction());

            this.CheckActionResultTestBuilder(actionResultTestBuilder, "AsyncOkResultAction");
        }

        [Fact]
        public void CallingShouldPopulateCorrectActionNameWithNormalVoidActionCall()
        {
            var voidActionResultTestBuilder = MyMvc
                .Controller<MvcController>()
                .Calling(c => c.EmptyAction());

            this.CheckActionName(voidActionResultTestBuilder, "EmptyAction");
        }

        [Fact]
        public void CallingShouldPopulateCorrectActionNameWithTaskActionCall()
        {
            var voidActionResultTestBuilder = MyMvc
                .Controller<MvcController>()
                .Calling(c => c.EmptyActionAsync());

            this.CheckActionName(voidActionResultTestBuilder, "EmptyActionAsync");
        }

        [Fact]
        public void CallingShouldPopulateModelStateWhenThereAreModelErrors()
        {
            var requestModel = TestObjectFactory.GetRequestModelWithErrors();

            var controller = MyMvc
                .Controller<MvcController>()
                .Calling(c => c.OkResultActionWithRequestBody(1, requestModel))
                .ShouldReturn()
                .Ok()
                .AndProvideTheController();

            var modelState = (controller as Controller).ModelState;

            Assert.False(modelState.IsValid);
            Assert.Equal(2, modelState.Values.Count);
            Assert.Equal("Integer", modelState.Keys.First());
            Assert.Equal("RequiredString", modelState.Keys.Last());
        }

        [Fact]
        public void CallingShouldHaveValidModelStateWhenThereAreNoModelErrors()
        {
            var requestModel = TestObjectFactory.GetValidRequestModel();

            var controller = MyMvc
                .Controller<MvcController>()
                .Calling(c => c.OkResultActionWithRequestBody(1, requestModel))
                .ShouldReturn()
                .Ok()
                .AndProvideTheController();

            var modelState = (controller as Controller).ModelState;

            Assert.True(modelState.IsValid);
            Assert.Equal(0, modelState.Values.Count);
            Assert.Equal(0, modelState.Keys.Count);
        }

        [Fact]
        public void WithoutValidationShouldNotValidateTheRequestModel()
        {
            MyMvc
                .Controller<MvcController>()
                .WithoutValidation()
                .Calling(c => c.ModelStateCheck(TestObjectFactory.GetRequestModelWithErrors()))
                .ShouldHave()
                .ValidModelState();
        }
        
        [Fact]
        public void WithAuthenticatedUserShouldPopulateUserPropertyWithDefaultValues()
        {
            var controllerBuilder = MyMvc
                .Controller<MvcController>()
                .WithAuthenticatedUser();

            controllerBuilder
                .Calling(c => c.AuthorizedAction())
                .ShouldReturn()
                .Ok();

            var controllerUser = controllerBuilder.AndProvideTheController().User;

            Assert.Equal(false, controllerUser.IsInRole("Any"));
            Assert.Equal("TestUser", controllerUser.Identity.Name);
            Assert.True(controllerUser.HasClaim(ClaimTypes.Name, "TestUser"));
            Assert.Equal("Passport", controllerUser.Identity.AuthenticationType);
            Assert.Equal(true, controllerUser.Identity.IsAuthenticated);
        }

        [Fact]
        public void WithAuthenticatedUserShouldPopulateProperUserWhenUserWithUserBuilder()
        {
            var controllerBuilder = MyMvc
                .Controller<MvcController>()
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
                    }));

            controllerBuilder
                .Calling(c => c.AuthorizedAction())
                .ShouldReturn()
                .Ok();

            var controllerUser = controllerBuilder.AndProvideTheController().User;

            Assert.Equal("NewUserName", controllerUser.Identity.Name);
            Assert.Equal("Custom", controllerUser.Identity.AuthenticationType);
            Assert.Equal(true, controllerUser.Identity.IsAuthenticated);
            Assert.Equal(true, controllerUser.IsInRole("NormalUser"));
            Assert.Equal(true, controllerUser.IsInRole("Moderator"));
            Assert.Equal(true, controllerUser.IsInRole("Administrator"));
            Assert.Equal(true, controllerUser.IsInRole("SuperUser"));
            Assert.Equal(true, controllerUser.IsInRole("MegaUser"));
            Assert.Equal(false, controllerUser.IsInRole("AnotherRole"));
        }

        [Fact]
        public void WithAuthenticatedNotCalledShouldNotHaveAuthorizedUser()
        {
            var controllerBuilder = MyMvc
                .Controller<MvcController>();

            controllerBuilder
                .Calling(c => c.AuthorizedAction())
                .ShouldReturn()
                .NotFound();

            var controllerUser = controllerBuilder.AndProvideTheController().User;

            Assert.Equal(false, controllerUser.IsInRole("Any"));
            Assert.Equal(null, controllerUser.Identity.Name);
            Assert.Equal(null, controllerUser.Identity.AuthenticationType);
            Assert.Equal(false, controllerUser.Identity.IsAuthenticated);
        }
        
        [Fact]
        public void WithResolvedDependencyForShouldChooseCorrectConstructorWithLessDependencies()
        {
            var controller = MyMvc
                .Controller<MvcController>()
                .WithResolvedDependencyFor<IInjectedService>(new InjectedService())
                .AndProvideTheController();

            Assert.NotNull(controller);
            Assert.NotNull(controller.InjectedService);
            Assert.Null(controller.AnotherInjectedService);
            Assert.Null(controller.InjectedRequestModel);
        }

        [Fact]
        public void WithResolvedDependencyForShouldChooseCorrectConstructorWithMoreDependencies()
        {
            var controller = MyMvc
                .Controller<MvcController>()
                .WithResolvedDependencyFor<IAnotherInjectedService>(new AnotherInjectedService())
                .WithResolvedDependencyFor<IInjectedService>(new InjectedService())
                .AndProvideTheController();

            Assert.NotNull(controller);
            Assert.NotNull(controller.InjectedService);
            Assert.NotNull(controller.AnotherInjectedService);
            Assert.Null(controller.InjectedRequestModel);
        }

        [Fact]
        public void WithResolvedDependencyForShouldChooseCorrectConstructorWithAllDependencies()
        {
            var controller = MyMvc
                .Controller<MvcController>()
                .WithResolvedDependencyFor<IAnotherInjectedService>(new AnotherInjectedService())
                .WithResolvedDependencyFor<RequestModel>(new RequestModel())
                .WithResolvedDependencyFor<IInjectedService>(new InjectedService())
                .AndProvideTheController();

            Assert.NotNull(controller);
            Assert.NotNull(controller.InjectedService);
            Assert.NotNull(controller.AnotherInjectedService);
            Assert.NotNull(controller.InjectedRequestModel);
        }

        [Fact]
        public void WithResolvedDependencyForShouldContinueTheNormalExecutionFlowOfTestBuilders()
        {
            MyMvc
                .Controller<MvcController>()
                .WithResolvedDependencyFor(new RequestModel())
                .WithResolvedDependencyFor(new AnotherInjectedService())
                .WithResolvedDependencyFor(new InjectedService())
                .WithAuthenticatedUser()
                .Calling(c => c.AuthorizedAction())
                .ShouldReturn()
                .Ok();
        }

        [Fact]
        public void WithResolvedDependencyForShouldWorkCorrectlyWithNullValues()
        {
            MyMvc
                .Controller<MvcController>()
                .WithResolvedDependencyFor<IInjectedService>(null)
                .WithResolvedDependencyFor<RequestModel>(null)
                .WithResolvedDependencyFor<IAnotherInjectedService>(null)
                .Calling(c => c.DefaultView())
                .ShouldReturn()
                .View();
        }

        [Fact]
        public void WithResolvedDependencyForShouldNotThrowExceptionWithNullValuesAndMoreThanOneSuitableConstructor()
        {
            MyMvc
                .Controller<MvcController>()
                .WithResolvedDependencyFor<IInjectedService>(null)
                .Calling(c => c.DefaultView())
                .ShouldReturn()
                .View();
        }

        [Fact]
        public void WithNoResolvedDependencyForShouldNotThrowException()
        {
            MyMvc
                .Controller<MvcController>()
                .WithNoResolvedDependencyFor<IInjectedService>()
                .Calling(c => c.DefaultView())
                .ShouldReturn()
                .View();
        }

        [Fact]
        public void AndAlsoShouldContinueTheNormalExecutionFlowOfTestBuilders()
        {
            MyMvc
                .Controller<MvcController>()
                .WithResolvedDependencyFor(new RequestModel())
                .AndAlso()
                .WithResolvedDependencyFor(new AnotherInjectedService())
                .AndAlso()
                .WithResolvedDependencyFor(new InjectedService())
                .AndAlso()
                .WithAuthenticatedUser()
                .Calling(c => c.AuthorizedAction())
                .ShouldReturn()
                .Ok();
        }

        [Fact]
        public void WithResolvedDependenciesShouldWorkCorrectWithCollectionOfObjects()
        {
            MyMvc
                .Controller<MvcController>()
                .WithResolvedDependencies(new List<object> { new RequestModel(), new AnotherInjectedService(), new InjectedService() })
                .WithAuthenticatedUser()
                .Calling(c => c.AuthorizedAction())
                .ShouldReturn()
                .Ok();
        }

        [Fact]
        public void WithResolvedDependenciesShouldWorkCorrectWithParamsOfObjects()
        {
            MyMvc
                .Controller<MvcController>()
                .WithResolvedDependencies(new RequestModel(), new AnotherInjectedService(), new InjectedService())
                .WithAuthenticatedUser()
                .Calling(c => c.AuthorizedAction())
                .ShouldReturn()
                .Ok();
        }

        [Fact]
        public void WithResolvedDependencyForShouldThrowExceptionWhenSameDependenciesAreRegistered()
        {
            Test.AssertException<InvalidOperationException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .WithResolvedDependencyFor<RequestModel>(new RequestModel())
                        .WithResolvedDependencyFor<IAnotherInjectedService>(new AnotherInjectedService())
                        .WithResolvedDependencyFor<IInjectedService>(new InjectedService())
                        .WithResolvedDependencyFor<IAnotherInjectedService>(new AnotherInjectedService());
                },
                "Dependency AnotherInjectedService is already registered for MvcController controller.");
        }

        [Fact]
        public void WithResolvedDependencyForShouldThrowExceptionWhenNoConstructorExistsForDependencies()
        {
            Test.AssertException<UnresolvedDependenciesException>(
                () =>
                {
                    MyMvc
                        .Controller<NoParameterlessConstructorController>()
                        .WithResolvedDependencyFor<RequestModel>(new RequestModel())
                        .WithResolvedDependencyFor<IAnotherInjectedService>(new AnotherInjectedService())
                        .WithResolvedDependencyFor<ResponseModel>(new ResponseModel())
                        .Calling(c => c.OkAction())
                        .ShouldReturn()
                        .Ok();
                },
                "NoParameterlessConstructorController could not be instantiated because it contains no constructor taking RequestModel, AnotherInjectedService, ResponseModel as parameters.");
        }

        [Fact]
        public void PrepareControllerShouldSetCorrectPropertiesWithDefaultServices()
        {
            var controller = MyMvc
                .Controller<MvcController>()
                .AndProvideTheController();

            Assert.NotNull(controller);
            Assert.NotNull(controller.HttpContext);
            Assert.NotNull(controller.ControllerContext);
            Assert.NotNull(controller.ControllerContext.HttpContext);
            Assert.NotNull(controller.ControllerContext.InputFormatters);
            Assert.NotEmpty(controller.ControllerContext.InputFormatters);
            Assert.NotNull(controller.ControllerContext.ModelBinders);
            Assert.NotEmpty(controller.ControllerContext.ModelBinders);
            Assert.NotNull(controller.ControllerContext.ValidatorProviders);
            Assert.NotEmpty(controller.ControllerContext.ValidatorProviders);
            Assert.NotNull(controller.ControllerContext.ValueProviders);
            Assert.NotNull(controller.ViewBag);
            Assert.NotNull(controller.ViewData);
            Assert.NotNull(controller.TempData);
            Assert.NotNull(controller.Resolver);
            Assert.NotNull(controller.Request);
            Assert.NotNull(controller.Response);
            Assert.NotNull(controller.MetadataProvider);
            Assert.NotNull(controller.ModelState);
            Assert.NotNull(controller.ObjectValidator);
            Assert.NotNull(controller.Url);
            Assert.NotNull(controller.User);
            Assert.Null(controller.ControllerContext.ActionDescriptor);
        }

        [Fact]
        public void PrepareControllerShouldSetCorrectPropertiesWithCustomSetup()
        {
            var controller = MyMvc
                .Controller<MvcController>()
                .WithSetup(c =>
                {
                    c.ControllerContext = new ControllerContext();
                })
                .AndProvideTheController();

            Assert.NotNull(controller);
            Assert.NotNull(controller.ControllerContext);
            Assert.Null(controller.ControllerContext.HttpContext);
            Assert.Empty(controller.ControllerContext.InputFormatters);
            Assert.Empty(controller.ControllerContext.ModelBinders);
            Assert.Empty(controller.ControllerContext.ValidatorProviders);
        }

        [Fact]
        public void CallingShouldPopulateCorrectActionDescriptor()
        {
            var controller = MyMvc
                .Controller<MvcController>()
                .Calling(c => c.OkResultAction())
                .AndProvideTheController();

            Assert.NotNull(controller);
            Assert.NotNull((controller as Controller).ControllerContext);
            Assert.NotNull((controller as Controller).ControllerContext.ActionDescriptor);
            Assert.Equal("OkResultAction", (controller as Controller).ControllerContext.ActionDescriptor.Name);
        }

        [Fact]
        public void WithHttpContextSessionShouldThrowExceptionIfSessionIsNotRegistered()
        {
            var httpContext = new DefaultHttpContext();

            var setHttpContext = MyMvc
                .Controller<MvcController>()
                .WithHttpContext(httpContext)
                .AndProvideTheHttpContext();

            Assert.Throws<InvalidOperationException>(() => setHttpContext.Session);
        }

        [Fact]
        public void WithHttpContextShouldPopulateCustomHttpContext()
        {
            MyMvc
                .IsUsingDefaultConfiguration()
                .WithServices(services =>
                {
                    services.AddMemoryCache();
                    services.AddDistributedMemoryCache();
                    services.AddSession();
                });

            var httpContext = new DefaultHttpContext();
            httpContext.Request.Scheme = "Custom";
            httpContext.Response.StatusCode = 404;
            httpContext.Response.ContentType = ContentType.ApplicationJson;
            httpContext.Response.ContentLength = 100;

            var controllerBuilder = MyMvc
                .Controller<MvcController>()
                .WithHttpContext(httpContext);

            var controller = controllerBuilder
                .AndProvideTheController();

            var setHttpContext = controllerBuilder
                .AndProvideTheHttpContext();

            Assert.Equal("Custom", controller.HttpContext.Request.Scheme);
            Assert.Equal("Custom", setHttpContext.Request.Scheme);
            Assert.IsAssignableFrom<MockedHttpResponse>(controller.HttpContext.Response);
            Assert.IsAssignableFrom<MockedHttpResponse>(setHttpContext.Response);
            Assert.Same(httpContext.Response.Body, controller.HttpContext.Response.Body);
            Assert.Same(httpContext.Response.Body, setHttpContext.Response.Body);
            Assert.Equal(httpContext.Response.ContentLength, controller.HttpContext.Response.ContentLength);
            Assert.Equal(httpContext.Response.ContentLength, setHttpContext.Response.ContentLength);
            Assert.Equal(httpContext.Response.ContentType, controller.HttpContext.Response.ContentType);
            Assert.Equal(httpContext.Response.ContentType, setHttpContext.Response.ContentType);
            Assert.Equal(httpContext.Response.StatusCode, controller.HttpContext.Response.StatusCode);
            Assert.Equal(httpContext.Response.StatusCode, setHttpContext.Response.StatusCode);
            Assert.Same(httpContext.Items, controller.HttpContext.Items);
            Assert.Same(httpContext.Items, setHttpContext.Items);
            Assert.Same(httpContext.Features, controller.HttpContext.Features);
            Assert.Same(httpContext.Features, setHttpContext.Features);
            Assert.Same(httpContext.RequestServices, controller.HttpContext.RequestServices);
            Assert.Same(httpContext.RequestServices, setHttpContext.RequestServices);
            Assert.Same(httpContext.Session, controller.HttpContext.Session);
            Assert.Same(httpContext.Session, setHttpContext.Session);
            Assert.Same(httpContext.TraceIdentifier, controller.HttpContext.TraceIdentifier);
            Assert.Same(httpContext.TraceIdentifier, setHttpContext.TraceIdentifier);
            Assert.Same(httpContext.User, controller.HttpContext.User);
            Assert.Same(httpContext.User, setHttpContext.User);

            MyMvc.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void WithHttpContextSetupShouldPopulateContextProperties()
        {
            var controller = MyMvc
                .Controller<MvcController>()
                .WithHttpContext(httpContext =>
                {
                    httpContext.Request.ContentType = ContentType.ApplicationOctetStream;
                })
                .AndProvideTheController();

            Assert.Equal(ContentType.ApplicationOctetStream, controller.HttpContext.Request.ContentType);
        }

        [Fact]
        public void WithRequestShouldNotWorkWithDefaultRequestAction()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.WithRequest())
                .ShouldReturn()
                .BadRequest();
        }

        [Fact]
        public void WithRequestShouldWorkWithSetRequestAction()
        {
            MyMvc
                .Controller<MvcController>()
                .WithHttpRequest(req => req.WithFormField("Test", "TestValue"))
                .Calling(c => c.WithRequest())
                .ShouldReturn()
                .Ok();
        }

        [Fact]
        public void WithRequestAsObjectShouldWorkWithSetRequestAction()
        {
            var httpContext = new MockedHttpContext();
            httpContext.Request.Form = new FormCollection(new Dictionary<string, StringValues> { ["Test"] = "TestValue" });

            MyMvc
                .Controller<MvcController>()
                .WithHttpRequest(httpContext.Request)
                .Calling(c => c.WithRequest())
                .ShouldReturn()
                .Ok();
        }

        [Fact]
        public void RouteDataShouldBePopulatedWhenRequestAndPathAreProvided()
        {
            MyMvc.IsUsingDefaultConfiguration();

            MyMvc
                .Controller<MvcController>()
                .WithHttpRequest(req => req.WithPath("/Mvc/WithRouteData/1"))
                .Calling(c => c.WithRouteData(1))
                .ShouldReturn()
                .View();
        }

        [Fact]
        public void UsingUrlHelperInsideControllerShouldWorkCorrectly()
        {
            MyMvc
                .Controller<MvcController>()
                .WithResolvedRouteData()
                .Calling(c => c.UrlAction())
                .ShouldReturn()
                .Ok()
                .WithResponseModel("/api/test");
        }

        [Fact]
        public void UsingTryValidateModelInsideControllerShouldWorkCorrectly()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.TryValidateModelAction())
                .ShouldReturn()
                .BadRequest();
        }

        [Fact]
        public void UsingTryUpdateModelAsyncShouldWorkCorrectly()
        {
            MyMvc
                .Controller<MvcController>()
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
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.UrlAction())
                        .ShouldReturn()
                        .Ok()
                        .WithResponseModel("");
                },
                "Route values are not present in the action call but are needed for successful pass of this test case. Consider calling 'WithResolvedRouteValues' on the controller builder to resolve them from the provided lambda expression or set the HTTP request path by using 'WithHttpRequest'.");
        }

        [Fact]
        public void NormalIndexOutOfRangeExceptionShouldShowNormalMessage()
        {
            Assert.Throws<ActionCallAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.IndexOutOfRangeException())
                        .ShouldReturn()
                        .Ok();
                });
        }

        [Fact]
        public void WithControllerContextShouldSetControllerContext()
        {
            var controllerContext = new ControllerContext();

            var controller = MyMvc
                .Controller<MvcController>()
                .WithControllerContext(controllerContext)
                .AndProvideTheController();

            Assert.NotNull(controller);
            Assert.Same(controllerContext, controller.ControllerContext);
        }

        [Fact]
        public void WithControllerContextSetupShouldSetCorrectControllerContext()
        {
            var controller = MyMvc
                .Controller<MvcController>()
                .WithControllerContext(controllerContext =>
                {
                    controllerContext.RouteData.Values.Add("testkey", "testvalue");
                })
                .AndProvideTheController();

            Assert.NotNull(controller);
            Assert.NotNull(controller.ControllerContext);
            Assert.True(controller.ControllerContext.RouteData.Values.ContainsKey("testkey"));
        }

        [Fact]
        public void WithTempDataShouldPopulateTempDataCorrectly()
        {
            MyMvc
                .Controller<MvcController>()
                .WithTempData(tempData =>
                {
                    tempData.WithEntry("test", "value");
                })
                .Calling(c => c.TempDataAction())
                .ShouldReturn()
                .Ok();
        }

        [Fact]
        public void WithSessionShouldPopulateSessionCorrectly()
        {
            MyMvc
                .IsUsingDefaultConfiguration()
                .WithServices(services =>
                {
                    services.AddMemoryCache();
                    services.AddDistributedMemoryCache();
                    services.AddSession();
                });

            MyMvc
                .Controller<MvcController>()
                .WithSession(session =>
                {
                    session.WithStringEntry("test", "value");
                })
                .Calling(c => c.SessionAction())
                .ShouldReturn()
                .Ok();

            MyMvc.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void WithSessionShouldThrowExceptionIfSessionIsNotSet()
        {
            Test.AssertException<InvalidOperationException>(
                () =>
                {
                    MyMvc
                       .Controller<MvcController>()
                       .WithSession(session =>
                       {
                           session.WithStringEntry("test", "value");
                       })
                       .Calling(c => c.SessionAction())
                       .ShouldReturn()
                       .Ok();
                },
                "Session has not been configured for this application or request.");
        }

        [Fact]
        public void WithTempDataShouldPopulateTempData()
        {
            var controller = MyMvc
                .Controller<MvcController>()
                .WithTempData(tempData => tempData
                    .WithEntry("key", "value"))
                .AndProvideTheController();

            Assert.Equal(1, controller.TempData.Count);
        }
        
        [Fact]
        public void CallingShouldPopulateCorrectActionNameAndActionResultWithNormalActionCallForPocoController()
        {
            MyMvc
                .IsUsingDefaultConfiguration()
                .WithServices(services =>
                {
                    services.AddHttpContextAccessor();
                });

            var actionResultTestBuilder = MyMvc
                .Controller<FullPocoController>()
                .Calling(c => c.OkResultAction());

            this.CheckActionResultTestBuilder(actionResultTestBuilder, "OkResultAction");

            MyMvc.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void CallingShouldPopulateCorrectActionNameAndActionResultWithAsyncActionCallForPocoController()
        {
            MyMvc
                .IsUsingDefaultConfiguration()
                .WithServices(services =>
                {
                    services.AddHttpContextAccessor();
                });

            var actionResultTestBuilder = MyMvc
                .Controller<FullPocoController>()
                .Calling(c => c.AsyncOkResultAction());

            this.CheckActionResultTestBuilder(actionResultTestBuilder, "AsyncOkResultAction");

            MyMvc.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void CallingShouldPopulateCorrectActionNameWithNormalVoidActionCallForPocoController()
        {
            MyMvc
                .IsUsingDefaultConfiguration()
                .WithServices(services =>
                {
                    services.AddHttpContextAccessor();
                });

            var voidActionResultTestBuilder = MyMvc
                .Controller<FullPocoController>()
                .Calling(c => c.EmptyAction());

            this.CheckActionName(voidActionResultTestBuilder, "EmptyAction");

            MyMvc.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void CallingShouldPopulateCorrectActionNameWithTaskActionCallForPocoController()
        {
            MyMvc
                .IsUsingDefaultConfiguration()
                .WithServices(services =>
                {
                    services.AddHttpContextAccessor();
                });

            var voidActionResultTestBuilder = MyMvc
                .Controller<FullPocoController>()
                .Calling(c => c.EmptyActionAsync());

            this.CheckActionName(voidActionResultTestBuilder, "EmptyActionAsync");

            MyMvc.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void CallingShouldPopulateModelStateWhenThereAreModelErrorsForPocoController()
        {
            MyMvc
                .IsUsingDefaultConfiguration()
                .WithServices(services =>
                {
                    services.AddHttpContextAccessor();
                });

            var requestModel = TestObjectFactory.GetRequestModelWithErrors();

            var controller = MyMvc
                .Controller<FullPocoController>()
                .Calling(c => c.OkResultActionWithRequestBody(1, requestModel))
                .ShouldReturn()
                .Ok()
                .AndProvideTheController();

            var modelState = (controller as FullPocoController).CustomControllerContext.ModelState;

            Assert.False(modelState.IsValid);
            Assert.Equal(2, modelState.Values.Count);
            Assert.Equal("Integer", modelState.Keys.First());
            Assert.Equal("RequiredString", modelState.Keys.Last());

            MyMvc.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void CallingShouldHaveValidModelStateWhenThereAreNoModelErrorsForPocoController()
        {
            MyMvc
                .IsUsingDefaultConfiguration()
                .WithServices(services =>
                {
                    services.AddHttpContextAccessor();
                });

            var requestModel = TestObjectFactory.GetValidRequestModel();

            var controller = MyMvc
                .Controller<FullPocoController>()
                .Calling(c => c.OkResultActionWithRequestBody(1, requestModel))
                .ShouldReturn()
                .Ok()
                .AndProvideTheController();

            var modelState = (controller as FullPocoController).CustomControllerContext.ModelState;

            Assert.True(modelState.IsValid);
            Assert.Equal(0, modelState.Values.Count);
            Assert.Equal(0, modelState.Keys.Count);

            MyMvc.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void WithoutValidationShouldNotValidateTheRequestModelForPocoController()
        {
            MyMvc
                .IsUsingDefaultConfiguration()
                .WithServices(services =>
                {
                    services.AddHttpContextAccessor();
                });

            MyMvc
                .Controller<FullPocoController>()
                .WithoutValidation()
                .Calling(c => c.ModelStateCheck(TestObjectFactory.GetRequestModelWithErrors()))
                .ShouldHave()
                .ValidModelState();

            MyMvc.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void WithAuthenticatedUserShouldPopulateUserPropertyWithDefaultValuesForPocoController()
        {
            MyMvc
                .IsUsingDefaultConfiguration()
                .WithServices(services =>
                {
                    services.AddHttpContextAccessor();
                });

            var controllerBuilder = MyMvc
                .Controller<FullPocoController>()
                .WithAuthenticatedUser();

            controllerBuilder
                .Calling(c => c.AuthorizedAction())
                .ShouldReturn()
                .Ok();

            var controllerUser = controllerBuilder.AndProvideTheController().CustomHttpContext.User;

            Assert.Equal(false, controllerUser.IsInRole("Any"));
            Assert.Equal("TestUser", controllerUser.Identity.Name);
            Assert.True(controllerUser.HasClaim(ClaimTypes.Name, "TestUser"));
            Assert.Equal("Passport", controllerUser.Identity.AuthenticationType);
            Assert.Equal(true, controllerUser.Identity.IsAuthenticated);

            MyMvc.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void WithAuthenticatedUserShouldPopulateProperUserWhenUserWithUserBuilderForPocoController()
        {
            MyMvc
                .IsUsingDefaultConfiguration()
                .WithServices(services =>
                {
                    services.AddHttpContextAccessor();
                });

            var controllerBuilder = MyMvc
                .Controller<FullPocoController>()
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
                    }));

            controllerBuilder
                .Calling(c => c.AuthorizedAction())
                .ShouldReturn()
                .Ok();

            var controllerUser = controllerBuilder.AndProvideTheController().CustomHttpContext.User;

            Assert.Equal("NewUserName", controllerUser.Identity.Name);
            Assert.Equal("Custom", controllerUser.Identity.AuthenticationType);
            Assert.Equal(true, controllerUser.Identity.IsAuthenticated);
            Assert.Equal(true, controllerUser.IsInRole("NormalUser"));
            Assert.Equal(true, controllerUser.IsInRole("Moderator"));
            Assert.Equal(true, controllerUser.IsInRole("Administrator"));
            Assert.Equal(true, controllerUser.IsInRole("SuperUser"));
            Assert.Equal(true, controllerUser.IsInRole("MegaUser"));
            Assert.Equal(false, controllerUser.IsInRole("AnotherRole"));

            MyMvc.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void WithAuthenticatedNotCalledShouldNotHaveAuthorizedUserForPocoController()
        {
            MyMvc
                .IsUsingDefaultConfiguration()
                .WithServices(services =>
                {
                    services.AddHttpContextAccessor();
                });

            var controllerBuilder = MyMvc
                .Controller<FullPocoController>();

            controllerBuilder
                .Calling(c => c.AuthorizedAction())
                .ShouldReturn()
                .NotFound();

            var controllerUser = controllerBuilder.AndProvideTheController().CustomHttpContext.User;

            Assert.Equal(false, controllerUser.IsInRole("Any"));
            Assert.Equal(null, controllerUser.Identity.Name);
            Assert.Equal(null, controllerUser.Identity.AuthenticationType);
            Assert.Equal(false, controllerUser.Identity.IsAuthenticated);

            MyMvc.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void WithResolvedDependencyForShouldChooseCorrectConstructorWithLessDependenciesForPocoController()
        {
            var controller = MyMvc
                .Controller<FullPocoController>()
                .WithResolvedDependencyFor<IInjectedService>(new InjectedService())
                .AndProvideTheController();

            Assert.NotNull(controller);
            Assert.NotNull(controller.InjectedService);
            Assert.Null(controller.AnotherInjectedService);
            Assert.Null(controller.InjectedRequestModel);

            MyMvc.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void WithResolvedDependencyForShouldChooseCorrectConstructorWithMoreDependenciesForPocoController()
        {
            var controller = MyMvc
                .Controller<FullPocoController>()
                .WithResolvedDependencyFor<IAnotherInjectedService>(new AnotherInjectedService())
                .WithResolvedDependencyFor<IInjectedService>(new InjectedService())
                .AndProvideTheController();

            Assert.NotNull(controller);
            Assert.NotNull(controller.InjectedService);
            Assert.NotNull(controller.AnotherInjectedService);
            Assert.Null(controller.InjectedRequestModel);

            MyMvc.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void WithResolvedDependencyForShouldChooseCorrectConstructorWithAllDependenciesForPocoController()
        {
            var controller = MyMvc
                .Controller<FullPocoController>()
                .WithResolvedDependencyFor<IAnotherInjectedService>(new AnotherInjectedService())
                .WithResolvedDependencyFor<RequestModel>(new RequestModel())
                .WithResolvedDependencyFor<IInjectedService>(new InjectedService())
                .AndProvideTheController();

            Assert.NotNull(controller);
            Assert.NotNull(controller.InjectedService);
            Assert.NotNull(controller.AnotherInjectedService);
            Assert.NotNull(controller.InjectedRequestModel);

            MyMvc.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void WithResolvedDependencyForShouldContinueTheNormalExecutionFlowOfTestBuildersForPocoController()
        {
            MyMvc
                .Controller<FullPocoController>()
                .WithResolvedDependencyFor(new RequestModel())
                .WithResolvedDependencyFor(new AnotherInjectedService())
                .WithResolvedDependencyFor(new InjectedService())
                .WithAuthenticatedUser()
                .Calling(c => c.OkResultAction())
                .ShouldReturn()
                .Ok();
        }

        [Fact]
        public void WithResolvedDependencyForShouldWorkCorrectlyWithNullValuesForPocoController()
        {
            MyMvc
                .Controller<FullPocoController>()
                .WithResolvedDependencyFor<IInjectedService>(null)
                .WithResolvedDependencyFor<RequestModel>(null)
                .WithResolvedDependencyFor<IAnotherInjectedService>(null)
                .Calling(c => c.DefaultView())
                .ShouldReturn()
                .View();
        }

        [Fact]
        public void WithResolvedDependencyForShouldNotThrowExceptionWithNullValuesAndMoreThanOneSuitableConstructorForPocoController()
        {
            MyMvc
                .Controller<FullPocoController>()
                .WithResolvedDependencyFor<IInjectedService>(null)
                .Calling(c => c.DefaultView())
                .ShouldReturn()
                .View();
        }

        [Fact]
        public void WithNoResolvedDependencyForShouldNotThrowExceptionForPocoController()
        {
            MyMvc
                .Controller<FullPocoController>()
                .WithNoResolvedDependencyFor<IInjectedService>()
                .Calling(c => c.DefaultView())
                .ShouldReturn()
                .View();
        }

        [Fact]
        public void AndAlsoShouldContinueTheNormalExecutionFlowOfTestBuildersForPocoController()
        {
            MyMvc
                .Controller<FullPocoController>()
                .WithResolvedDependencyFor(new RequestModel())
                .AndAlso()
                .WithResolvedDependencyFor(new AnotherInjectedService())
                .AndAlso()
                .WithResolvedDependencyFor(new InjectedService())
                .AndAlso()
                .WithAuthenticatedUser()
                .Calling(c => c.OkResultAction())
                .ShouldReturn()
                .Ok();
        }

        [Fact]
        public void WithResolvedDependenciesShouldWorkCorrectWithCollectionOfObjectsForPocoController()
        {
            MyMvc
                .Controller<FullPocoController>()
                .WithResolvedDependencies(new List<object> { new RequestModel(), new AnotherInjectedService(), new InjectedService() })
                .WithAuthenticatedUser()
                .Calling(c => c.OkResultAction())
                .ShouldReturn()
                .Ok();
        }

        [Fact]
        public void WithResolvedDependenciesShouldWorkCorrectWithParamsOfObjectsForPocoController()
        {
            MyMvc
                .Controller<FullPocoController>()
                .WithResolvedDependencies(new RequestModel(), new AnotherInjectedService(), new InjectedService())
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
                    MyMvc
                        .Controller<FullPocoController>()
                        .WithResolvedDependencyFor<RequestModel>(new RequestModel())
                        .WithResolvedDependencyFor<IAnotherInjectedService>(new AnotherInjectedService())
                        .WithResolvedDependencyFor<IInjectedService>(new InjectedService())
                        .WithResolvedDependencyFor<IAnotherInjectedService>(new AnotherInjectedService());
                },
                "Dependency AnotherInjectedService is already registered for FullPocoController controller.");
        }

        [Fact]
        public void WithResolvedDependencyForShouldThrowExceptionWhenNoConstructorExistsForDependenciesForPocoController()
        {
            Test.AssertException<UnresolvedDependenciesException>(
                () =>
                {
                    MyMvc
                        .Controller<NoParameterlessConstructorController>()
                        .WithResolvedDependencyFor<RequestModel>(new RequestModel())
                        .WithResolvedDependencyFor<IAnotherInjectedService>(new AnotherInjectedService())
                        .WithResolvedDependencyFor<ResponseModel>(new ResponseModel())
                        .Calling(c => c.OkAction())
                        .ShouldReturn()
                        .Ok();
                },
                "NoParameterlessConstructorController could not be instantiated because it contains no constructor taking RequestModel, AnotherInjectedService, ResponseModel as parameters.");
        }

        [Fact]
        public void PrepareControllerShouldSetCorrectPropertiesWithDefaultServicesForPocoController()
        {
            MyMvc
                .IsUsingDefaultConfiguration()
                .WithServices(services =>
                {
                    services.AddHttpContextAccessor();
                });

            var controller = MyMvc
                .Controller<FullPocoController>()
                .AndProvideTheController();

            Assert.NotNull(controller);
            Assert.NotNull(controller.CustomHttpContext);
            Assert.NotNull(controller.CustomActionContext);
            Assert.NotNull(controller.CustomControllerContext);
            Assert.NotNull(controller.CustomControllerContext.HttpContext);
            Assert.NotNull(controller.CustomControllerContext.InputFormatters);
            Assert.NotEmpty(controller.CustomControllerContext.InputFormatters);
            Assert.NotNull(controller.CustomControllerContext.ModelBinders);
            Assert.NotEmpty(controller.CustomControllerContext.ModelBinders);
            Assert.NotNull(controller.CustomControllerContext.ValidatorProviders);
            Assert.NotEmpty(controller.CustomControllerContext.ValidatorProviders);
            Assert.NotNull(controller.CustomControllerContext.ValueProviders);
            Assert.NotNull(controller.CustomHttpContext.Request);
            Assert.NotNull(controller.CustomHttpContext.Response);
            Assert.NotNull(controller.CustomControllerContext.ModelState);
            Assert.NotNull(controller.CustomHttpContext.User);
            Assert.Null(controller.CustomControllerContext.ActionDescriptor);

            MyMvc.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void PrepareControllerShouldSetCorrectPropertiesWithCustomSetupForPocoController()
        {
            MyMvc
                .IsUsingDefaultConfiguration()
                .WithServices(services =>
                {
                    services.AddHttpContextAccessor();
                });

            var controller = MyMvc
                .Controller<FullPocoController>()
                .WithSetup(c =>
                {
                    c.PublicProperty = new object();
                })
                .AndProvideTheController();

            Assert.NotNull(controller);
            Assert.NotNull(controller.PublicProperty);

            MyMvc.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void CallingShouldPopulateCorrectActionDescriptorForPocoController()
        {
            MyMvc
                .IsUsingDefaultConfiguration()
                .WithServices(services =>
                {
                    services.AddHttpContextAccessor();
                });

            var controller = MyMvc
                .Controller<FullPocoController>()
                .Calling(c => c.OkResultAction())
                .AndProvideTheController();

            Assert.NotNull(controller);
            Assert.NotNull((controller as FullPocoController).CustomControllerContext);
            Assert.NotNull((controller as FullPocoController).CustomControllerContext.ActionDescriptor);
            Assert.Equal("OkResultAction", (controller as FullPocoController).CustomControllerContext.ActionDescriptor.Name);

            MyMvc.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void WithHttpContextSessionShouldThrowExceptionIfSessionIsNotRegisteredForPocoController()
        {
            MyMvc
                .IsUsingDefaultConfiguration()
                .WithServices(services =>
                {
                    services.AddHttpContextAccessor();
                });

            var httpContext = new DefaultHttpContext();

            var setHttpContext = MyMvc
                .Controller<FullPocoController>()
                .WithHttpContext(httpContext)
                .AndProvideTheHttpContext();

            Assert.Throws<InvalidOperationException>(() => setHttpContext.Session);
        }

        [Fact]
        public void WithHttpContextShouldPopulateCustomHttpContextForPocoController()
        {
            MyMvc
                .IsUsingDefaultConfiguration()
                .WithServices(services =>
                {
                    services.AddMemoryCache();
                    services.AddDistributedMemoryCache();
                    services.AddSession();
                    services.AddHttpContextAccessor();
                });

            var httpContext = new DefaultHttpContext();
            httpContext.Request.Scheme = "Custom";
            httpContext.Response.StatusCode = 404;
            httpContext.Response.ContentType = ContentType.ApplicationJson;
            httpContext.Response.ContentLength = 100;

            var controllerBuilder = MyMvc
                .Controller<FullPocoController>()
                .WithHttpContext(httpContext);

            var controller = controllerBuilder
                .AndProvideTheController();

            var setHttpContext = controllerBuilder
                .AndProvideTheHttpContext();

            Assert.Equal("Custom", controller.CustomHttpContext.Request.Scheme);
            Assert.Equal("Custom", setHttpContext.Request.Scheme);
            Assert.IsAssignableFrom<MockedHttpResponse>(controller.CustomHttpContext.Response);
            Assert.IsAssignableFrom<MockedHttpResponse>(setHttpContext.Response);
            Assert.Same(httpContext.Response.Body, controller.CustomHttpContext.Response.Body);
            Assert.Same(httpContext.Response.Body, setHttpContext.Response.Body);
            Assert.Equal(httpContext.Response.ContentLength, controller.CustomHttpContext.Response.ContentLength);
            Assert.Equal(httpContext.Response.ContentLength, setHttpContext.Response.ContentLength);
            Assert.Equal(httpContext.Response.ContentType, controller.CustomHttpContext.Response.ContentType);
            Assert.Equal(httpContext.Response.ContentType, setHttpContext.Response.ContentType);
            Assert.Equal(httpContext.Response.StatusCode, controller.CustomHttpContext.Response.StatusCode);
            Assert.Equal(httpContext.Response.StatusCode, setHttpContext.Response.StatusCode);
            Assert.Same(httpContext.Items, controller.CustomHttpContext.Items);
            Assert.Same(httpContext.Items, setHttpContext.Items);
            Assert.Same(httpContext.Features, controller.CustomHttpContext.Features);
            Assert.Same(httpContext.Features, setHttpContext.Features);
            Assert.Same(httpContext.RequestServices, controller.CustomHttpContext.RequestServices);
            Assert.Same(httpContext.RequestServices, setHttpContext.RequestServices);
            Assert.Same(httpContext.Session, controller.CustomHttpContext.Session);
            Assert.Same(httpContext.Session, setHttpContext.Session);
            Assert.Same(httpContext.TraceIdentifier, controller.CustomHttpContext.TraceIdentifier);
            Assert.Same(httpContext.TraceIdentifier, setHttpContext.TraceIdentifier);
            Assert.Same(httpContext.User, controller.CustomHttpContext.User);
            Assert.Same(httpContext.User, setHttpContext.User);

            MyMvc.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void WithHttpContextSetupShouldPopulateContextPropertiesForPocoController()
        {
            MyMvc
                .IsUsingDefaultConfiguration()
                .WithServices(services =>
                {
                    services.AddHttpContextAccessor();
                });

            var controller = MyMvc
                .Controller<FullPocoController>()
                .WithHttpContext(httpContext =>
                {
                    httpContext.Request.ContentType = ContentType.ApplicationOctetStream;
                })
                .AndProvideTheController();

            Assert.Equal(ContentType.ApplicationOctetStream, controller.CustomHttpContext.Request.ContentType);

            MyMvc.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void WithRequestShouldNotWorkWithDefaultRequestActionForPocoController()
        {
            MyMvc
                .IsUsingDefaultConfiguration()
                .WithServices(services =>
                {
                    services.AddHttpContextAccessor();
                });

            MyMvc
                .Controller<FullPocoController>()
                .Calling(c => c.WithRequest())
                .ShouldReturn()
                .BadRequest();

            MyMvc.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void WithRequestShouldWorkWithSetRequestActionForPocoController()
        {
            MyMvc
                .IsUsingDefaultConfiguration()
                .WithServices(services =>
                {
                    services.AddHttpContextAccessor();
                });

            MyMvc
                .Controller<FullPocoController>()
                .WithHttpRequest(req => req.WithFormField("Test", "TestValue"))
                .Calling(c => c.WithRequest())
                .ShouldReturn()
                .Ok();

            MyMvc.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void WithRequestAsObjectShouldWorkWithSetRequestActionForPocoController()
        {
            MyMvc
                .IsUsingDefaultConfiguration()
                .WithServices(services =>
                {
                    services.AddHttpContextAccessor();
                });

            var httpContext = new MockedHttpContext();
            httpContext.Request.Form = new FormCollection(new Dictionary<string, StringValues> { ["Test"] = "TestValue" });

            MyMvc
                .Controller<FullPocoController>()
                .WithHttpRequest(httpContext.Request)
                .Calling(c => c.WithRequest())
                .ShouldReturn()
                .Ok();

            MyMvc.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void RouteDataShouldBePopulatedWhenRequestAndPathAreProvidedForPocoController()
        {
            MyMvc
                .IsUsingDefaultConfiguration()
                .WithServices(services =>
                {
                    services.AddHttpContextAccessor();
                });

            MyMvc
                .Controller<FullPocoController>()
                .WithHttpRequest(req => req.WithPath("/Mvc/WithRouteData/1"))
                .Calling(c => c.WithRouteData(1))
                .ShouldReturn()
                .View();

            MyMvc.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void UsingUrlHelperInsideControllerShouldWorkCorrectlyForPocoController()
        {
            MyMvc
                .IsUsingDefaultConfiguration()
                .WithServices(services =>
                {
                    services.AddHttpContextAccessor();
                });

            MyMvc
                .Controller<FullPocoController>()
                .WithResolvedRouteData()
                .Calling(c => c.UrlAction())
                .ShouldReturn()
                .Ok()
                .WithResponseModel("/FullPoco/UrlAction");

            MyMvc.IsUsingDefaultConfiguration();
        }
        
        [Fact]
        public void UnresolvedRouteValuesShouldHaveFriendlyExceptionForPocoController()
        {
            MyMvc
                .IsUsingDefaultConfiguration()
                .WithServices(services =>
                {
                    services.AddHttpContextAccessor();
                });

            Test.AssertException<InvalidOperationException>(
                () =>
                {
                    MyMvc
                        .Controller<FullPocoController>()
                        .Calling(c => c.UrlAction())
                        .ShouldReturn()
                        .Ok()
                        .WithResponseModel("");
                },
                "Route values are not present in the action call but are needed for successful pass of this test case. Consider calling 'WithResolvedRouteValues' on the controller builder to resolve them from the provided lambda expression or set the HTTP request path by using 'WithHttpRequest'.");
            
            MyMvc.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void NormalIndexOutOfRangeExceptionShouldShowNormalMessageForPocoController()
        {
            MyMvc
                .IsUsingDefaultConfiguration()
                .WithServices(services =>
                {
                    services.AddHttpContextAccessor();
                });

            Assert.Throws<ActionCallAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<FullPocoController>()
                        .Calling(c => c.IndexOutOfRangeException())
                        .ShouldReturn()
                        .Ok();
                });

            MyMvc.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void WithControllerContextShouldSetControllerContextForPocoController()
        {
            MyMvc
                .IsUsingDefaultConfiguration()
                .WithServices(services =>
                {
                    services.AddHttpContextAccessor();
                });

            var controllerContext = new ControllerContext();

            var controller = MyMvc
                .Controller<FullPocoController>()
                .WithControllerContext(controllerContext)
                .AndProvideTheController();

            Assert.NotNull(controller);
            Assert.Same(controllerContext, controller.CustomControllerContext);

            MyMvc.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void WithControllerContextSetupShouldSetCorrectControllerContextForPocoController()
        {
            MyMvc
                .IsUsingDefaultConfiguration()
                .WithServices(services =>
                {
                    services.AddHttpContextAccessor();
                });

            var controller = MyMvc
                .Controller<FullPocoController>()
                .WithControllerContext(controllerContext =>
                {
                    controllerContext.RouteData.Values.Add("testkey", "testvalue");
                })
                .AndProvideTheController();

            Assert.NotNull(controller);
            Assert.NotNull(controller.CustomControllerContext);
            Assert.True(controller.CustomControllerContext.RouteData.Values.ContainsKey("testkey"));

            MyMvc.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void WithTempDataShouldPopulateTempDataCorrectlyForPocoController()
        {
            MyMvc
                .IsUsingDefaultConfiguration()
                .WithServices(services =>
                {
                    services.AddHttpContextAccessor();
                });

            MyMvc
                .Controller<FullPocoController>()
                .WithTempData(tempData =>
                {
                    tempData.WithEntry("test", "value");
                })
                .Calling(c => c.TempDataAction())
                .ShouldReturn()
                .Ok();

            MyMvc.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void WithSessionShouldPopulateSessionCorrectlyForPocoController()
        {
            MyMvc
                .IsUsingDefaultConfiguration()
                .WithServices(services =>
                {
                    services.AddMemoryCache();
                    services.AddDistributedMemoryCache();
                    services.AddSession();
                    services.AddHttpContextAccessor();
                });

            MyMvc
                .Controller<FullPocoController>()
                .WithSession(session =>
                {
                    session.WithStringEntry("test", "value");
                })
                .Calling(c => c.SessionAction())
                .ShouldReturn()
                .Ok();

            MyMvc.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void WithSessionShouldThrowExceptionIfSessionIsNotSetForPocoController()
        {
            Test.AssertException<InvalidOperationException>(
                () =>
                {
                    MyMvc
                       .Controller<FullPocoController>()
                       .WithSession(session =>
                       {
                           session.WithStringEntry("test", "value");
                       })
                       .Calling(c => c.SessionAction())
                       .ShouldReturn()
                       .Ok();
                },
                "Session has not been configured for this application or request.");
        }

        [Fact]
        public void WithTempDataShouldPopulateTempDataForPocoController()
        {
            MyMvc
                .IsUsingDefaultConfiguration()
                .WithServices(services =>
                {
                    services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
                });

            var controller = MyMvc
                .Controller<FullPocoController>()
                .WithTempData(tempData => tempData
                    .WithEntry("key", "value"))
                .AndProvideTheController();

            Assert.Equal(1, controller.CustomTempData.Count);

            MyMvc.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void InvalidControllerTypeShouldThrowException()
        {
            Test.AssertException<InvalidOperationException>(
                () =>
                {
                    MyMvc.Controller<RequestModel>();
                },
                "RequestModel is not a valid controller type.");
        }

        private void CheckActionResultTestBuilder<TActionResult>(
            IActionResultTestBuilder<TActionResult> actionResultTestBuilder,
            string expectedActionName)
        {
            this.CheckActionName(actionResultTestBuilder, expectedActionName);
            var actionResult = actionResultTestBuilder.AndProvideTheActionResult();

            Assert.NotNull(actionResult);
            Assert.IsAssignableFrom<OkResult>(actionResult);
        }

        private void CheckActionName(IBaseTestBuilderWithInvokedAction testBuilder, string expectedActionName)
        {
            var actionName = testBuilder.AndProvideTheActionName();

            Assert.NotNull(actionName);
            Assert.NotEmpty(actionName);
            Assert.Equal(expectedActionName, actionName);
        }
    }
}
