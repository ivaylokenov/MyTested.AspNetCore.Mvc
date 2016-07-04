namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.ControllersTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using Builders.Contracts.Actions;
    using Builders.Contracts.Base;
    using Exceptions;
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
    using Microsoft.AspNetCore.Mvc.Controllers;

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
        public void CallingShouldHaveValidModelStateWhenThereAreNoModelErrors()
        {
            var requestModel = TestObjectFactory.GetValidRequestModel();

            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.OkResultActionWithRequestBody(1, requestModel))
                .ShouldReturn()
                .Ok()
                .ShouldPassFor()
                .TheController(controller =>
                {
                    var modelState = (controller as Controller).ModelState;

                    Assert.True(modelState.IsValid);
                    Assert.Equal(0, modelState.Values.Count());
                    Assert.Equal(0, modelState.Keys.Count());
                });
        }

        [Fact]
        public void WithAuthenticatedUserShouldPopulateUserPropertyWithDefaultValues()
        {
            MyMvc
                .Controller<MvcController>()
                .WithAuthenticatedUser()
                .Calling(c => c.AuthorizedAction())
                .ShouldReturn()
                .Ok()
                .ShouldPassFor()
                .TheController(controller =>
                {
                    var controllerUser = (controller as Controller).User;

                    Assert.Equal(false, controllerUser.IsInRole("Any"));
                    Assert.Equal("TestUser", controllerUser.Identity.Name);
                    Assert.True(controllerUser.HasClaim(ClaimTypes.Name, "TestUser"));
                    Assert.Equal("Passport", controllerUser.Identity.AuthenticationType);
                    Assert.Equal(true, controllerUser.Identity.IsAuthenticated);
                });
        }

        [Fact]
        public void WithAuthenticatedUserShouldPopulateProperUserWhenUserWithUserBuilder()
        {
            MyMvc
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
                    }))
                .Calling(c => c.AuthorizedAction())
                .ShouldReturn()
                .Ok()
                .ShouldPassFor()
                .TheController(controller =>
                {
                    var controllerUser = (controller as Controller).User;

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
        }

        [Fact]
        public void WithAuthenticatedNotCalledShouldNotHaveAuthorizedUser()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.AuthorizedAction())
                .ShouldReturn()
                .NotFound()
                .ShouldPassFor()
                .TheController(controller =>
                {
                    var controllerUser = (controller as Controller).User;

                    Assert.Equal(false, controllerUser.IsInRole("Any"));
                    Assert.Equal(null, controllerUser.Identity.Name);
                    Assert.Equal(null, controllerUser.Identity.AuthenticationType);
                    Assert.Equal(false, controllerUser.Identity.IsAuthenticated);
                });
        }
        
        [Fact]
        public void WithResolvedDependencyForShouldChooseCorrectConstructorWithLessDependencies()
        {
            MyMvc
                .Controller<MvcController>()
                .WithServiceFor<IInjectedService>(new InjectedService())
                .ShouldPassFor()
                .TheController(controller =>
                {
                    Assert.NotNull(controller);
                    Assert.NotNull(controller.InjectedService);
                    Assert.Null(controller.AnotherInjectedService);
                    Assert.Null(controller.InjectedRequestModel);
                });
        }

        [Fact]
        public void WithResolvedDependencyForShouldChooseCorrectConstructorWithMoreDependencies()
        {
            MyMvc
                .Controller<MvcController>()
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
        }

        [Fact]
        public void WithResolvedDependencyForShouldChooseCorrectConstructorWithAllDependencies()
        {
            MyMvc
                .Controller<MvcController>()
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
        }

        [Fact]
        public void WithResolvedDependencyForShouldContinueTheNormalExecutionFlowOfTestBuilders()
        {
            MyMvc
                .Controller<MvcController>()
                .WithServiceFor(new RequestModel())
                .WithServiceFor(new AnotherInjectedService())
                .WithServiceFor(new InjectedService())
                .WithAuthenticatedUser()
                .Calling(c => c.AuthorizedAction())
                .ShouldReturn()
                .Ok();
        }

        [Fact]
        public void AndAlsoShouldContinueTheNormalExecutionFlowOfTestBuilders()
        {
            MyMvc
                .Controller<MvcController>()
                .WithServiceFor(new RequestModel())
                .AndAlso()
                .WithServiceFor(new AnotherInjectedService())
                .AndAlso()
                .WithServiceFor(new InjectedService())
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
                .WithServices(new List<object> { new RequestModel(), new AnotherInjectedService(), new InjectedService() })
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
                .WithServices(new RequestModel(), new AnotherInjectedService(), new InjectedService())
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
                        .WithServiceFor<RequestModel>(new RequestModel())
                        .WithServiceFor<IAnotherInjectedService>(new AnotherInjectedService())
                        .WithServiceFor<IInjectedService>(new InjectedService())
                        .WithServiceFor<IAnotherInjectedService>(new AnotherInjectedService());
                },
                "Dependency AnotherInjectedService is already registered for MvcController controller.");
        }

        [Fact]
        public void WithResolvedDependencyForShouldThrowExceptionWhenNoConstructorExistsForDependencies()
        {
            Test.AssertException<UnresolvedServicesException>(
                () =>
                {
                    MyMvc
                        .Controller<NoParameterlessConstructorController>()
                        .WithServiceFor<RequestModel>(new RequestModel())
                        .WithServiceFor<IAnotherInjectedService>(new AnotherInjectedService())
                        .WithServiceFor<ResponseModel>(new ResponseModel())
                        .Calling(c => c.OkAction())
                        .ShouldReturn()
                        .Ok();
                },
                "NoParameterlessConstructorController could not be instantiated because it contains no constructor taking RequestModel, AnotherInjectedService, ResponseModel as parameters.");
        }

        [Fact]
        public void PrepareControllerShouldSetCorrectPropertiesWithCustomSetup()
        {
            MyMvc
                .Controller<MvcController>()
                .WithSetup(c =>
                {
                    c.ControllerContext = new ControllerContext();
                })
                .ShouldPassFor()
                .TheController(controller =>
                {
                    Assert.NotNull(controller);
                    Assert.NotNull(controller.ControllerContext);
                    Assert.Null(controller.ControllerContext.HttpContext);
                });
        }

        [Fact]
        public void CallingShouldPopulateCorrectActionDescriptor()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.OkResultAction())
                .ShouldPassFor()
                .TheController(controller =>
                {
                    Assert.NotNull(controller);
                    Assert.NotNull((controller as Controller).ControllerContext);
                    Assert.NotNull((controller as Controller).ControllerContext.ActionDescriptor);
                    Assert.Equal("OkResultAction", (controller as Controller).ControllerContext.ActionDescriptor.ActionName);
                });
        }

        [Fact]
        public void WithHttpContextSessionShouldThrowExceptionIfSessionIsNotRegistered()
        {
            var httpContext = new DefaultHttpContext();

            MyMvc
                .Controller<MvcController>()
                .WithHttpContext(httpContext)
                .ShouldPassFor()
                .TheHttpContext(context =>
                {
                    Assert.Throws<InvalidOperationException>(() => context.Session);
                });
        }

        [Fact]
        public void WithHttpContextShouldPopulateCustomHttpContext()
        {
            var httpContext = new DefaultHttpContext();
            httpContext.Request.Scheme = "Custom";
            httpContext.Response.StatusCode = 404;
            httpContext.Response.ContentType = ContentType.ApplicationJson;
            httpContext.Response.ContentLength = 100;

            MyMvc
                .Controller<MvcController>()
                .WithHttpContext(httpContext)
                .ShouldPassFor()
                .TheController(controller =>
                {
                    Assert.Equal("Custom", controller.HttpContext.Request.Scheme);
                    Assert.IsAssignableFrom<MockedHttpResponse>(controller.HttpContext.Response);
                    Assert.Same(httpContext.Response.Body, controller.HttpContext.Response.Body);
                    Assert.Equal(httpContext.Response.ContentLength, controller.HttpContext.Response.ContentLength);
                    Assert.Equal(httpContext.Response.ContentType, controller.HttpContext.Response.ContentType);
                    Assert.Equal(httpContext.Response.StatusCode, controller.HttpContext.Response.StatusCode);
                    Assert.Same(httpContext.Items, controller.HttpContext.Items);
                    Assert.Same(httpContext.Features, controller.HttpContext.Features);
                    Assert.Same(httpContext.RequestServices, controller.HttpContext.RequestServices);
                    Assert.Same(httpContext.TraceIdentifier, controller.HttpContext.TraceIdentifier);
                    Assert.Same(httpContext.User, controller.HttpContext.User);
                })
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
        }

        [Fact]
        public void WithHttpContextSetupShouldPopulateContextProperties()
        {
            MyMvc
                .Controller<MvcController>()
                .WithHttpContext(httpContext =>
                {
                    httpContext.Request.ContentType = ContentType.ApplicationOctetStream;
                })
                .ShouldPassFor()
                .TheController(controller =>
                {
                    Assert.Equal(ContentType.ApplicationOctetStream, controller.HttpContext.Request.ContentType);
                });
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
        public void UsingUrlHelperInsideControllerShouldWorkCorrectly()
        {
            MyMvc
                .Controller<MvcController>()
                .WithRouteData()
                .Calling(c => c.UrlAction())
                .ShouldReturn()
                .Ok()
                .WithResponseModel("/api/test");
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
            var controllerContext = new ControllerContext
            {
                ActionDescriptor = new ControllerActionDescriptor
                {
                    ActionName = "Test"
                }
            };

            MyMvc
                .Controller<MvcController>()
                .WithControllerContext(controllerContext)
                .ShouldPassFor()
                .TheController(controller =>
                {
                    Assert.NotNull(controller);
                    Assert.NotNull(controller.ControllerContext);
                    Assert.Equal("Test", controller.ControllerContext.ActionDescriptor.ActionName);
                });
        }

        [Fact]
        public void WithControllerContextSetupShouldSetCorrectControllerContext()
        {
            MyMvc
                .Controller<MvcController>()
                .WithControllerContext(controllerContext =>
                {
                    controllerContext.RouteData.Values.Add("testkey", "testvalue");
                })
                .ShouldPassFor()
                .TheController(controller =>
                {
                    Assert.NotNull(controller);
                    Assert.NotNull(controller.ControllerContext);
                    Assert.True(controller.ControllerContext.RouteData.Values.ContainsKey("testkey"));
                });
        }

        [Fact]
        public void WithResolvedDependencyForShouldThrowExceptionWhenNoConstructorExistsForDependenciesForPocoController()
        {
            Test.AssertException<UnresolvedServicesException>(
                () =>
                {
                    MyMvc
                        .Controller<NoParameterlessConstructorController>()
                        .WithServiceFor<RequestModel>(new RequestModel())
                        .WithServiceFor<IAnotherInjectedService>(new AnotherInjectedService())
                        .WithServiceFor<ResponseModel>(new ResponseModel())
                        .Calling(c => c.OkAction())
                        .ShouldReturn()
                        .Ok();
                },
                "NoParameterlessConstructorController could not be instantiated because it contains no constructor taking RequestModel, AnotherInjectedService, ResponseModel as parameters.");
        }

        [Fact]
        public void CallingShouldWorkCorrectlyWithFromServices()
        {
            MyMvc
                .IsUsingDefaultConfiguration()
                .WithServices(services =>
                {
                    services.AddHttpContextAccessor();
                });

            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.WithService(From.Services<IHttpContextAccessor>()))
                .ShouldReturn()
                .Ok();

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
        
        [Fact]
        public void PrepareControllerShouldSetCorrectPropertiesWithDefaultServices()
        {
            MyMvc
                .Controller<MvcController>()
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
        public void WithServiceSetupForShouldSetupScopedServiceCorrectly()
        {
            MyMvc
                .IsUsingDefaultConfiguration()
                .WithServices(services =>
                {
                    services.AddScoped<IScopedService, ScopedService>();
                });

            MyMvc
                .Controller<ServicesController>()
                .WithNoServiceFor<IScopedService>()
                .WithServiceSetupFor<IScopedService>(s => s.Value = "Custom")
                .Calling(c => c.DoNotSetValue())
                .ShouldReturn()
                .ResultOfType<string>()
                .Passing(r => r == "Custom");
            
            MyMvc
                .Controller<ServicesController>()
                .WithNoServiceFor<IScopedService>()
                .WithServiceSetupFor<IScopedService>(s => s.Value = "SecondCustom")
                .Calling(c => c.FromServices(From.Services<IScopedService>()))
                .ShouldReturn()
                .ResultOfType<string>()
                .Passing(r => r == "SecondCustom");

            MyMvc.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void WithServiceSetupForShouldSetupScopedServiceCorrectlyWithConstructorInjection()
        {
            MyMvc
                .IsUsingDefaultConfiguration()
                .WithServices(services =>
                {
                    services.AddScoped<IScopedService, ScopedService>();
                });

            MyMvc
                .Controller<ScopedServiceController>()
                .WithServiceSetupFor<IScopedService>(s => s.Value = "TestValue")
                .Calling(c => c.Index())
                .ShouldReturn()
                .Ok()
                .WithResponseModel("TestValue");

            MyMvc.IsUsingDefaultConfiguration();
        }
        
        [Fact]
        public void WithServiceSetupForShouldThrowExceptionWithNoScopedService()
        {
            MyMvc
                .IsUsingDefaultConfiguration()
                .WithServices(services =>
                {
                    services.AddSingleton<IScopedService, ScopedService>();
                });

            Test.AssertException<InvalidOperationException>(
                () =>
                {
                    MyMvc
                        .Controller<ScopedServiceController>()
                        .WithServiceSetupFor<IScopedService>(s => s.Value = "TestValue")
                        .Calling(c => c.Index())
                        .ShouldReturn()
                        .Ok()
                        .WithResponseModel("TestValue");
                },
                "The 'WithServiceSetupFor' method can be used only for services with scoped lifetime.");

            MyMvc.IsUsingDefaultConfiguration();
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
