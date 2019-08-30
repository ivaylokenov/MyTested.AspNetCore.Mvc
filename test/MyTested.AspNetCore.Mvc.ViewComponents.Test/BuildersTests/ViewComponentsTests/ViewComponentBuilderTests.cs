namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.ViewComponentsTests
{
    using Builders.Base;
    using Builders.Contracts.Base;
    using Builders.Contracts.Invocations;
    using Exceptions;
    using Internal.ViewComponents;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewComponents;
    using Setups;
    using Setups.Models;
    using Setups.ViewComponents;
    using System;
    using System.Reflection;
    using Xunit;

    public class ViewComponentBuilderTests
    {
        [Fact]
        public void InvokedWithShouldPopulateCorrectInvokeNameAndInvocationResultWithNormalActionCall()
        {
            var testBuilder = MyViewComponent<NormalComponent>
                .InvokedWith(c => c.Invoke());

            this.CheckViewComponentResultTestBuilder(testBuilder, "Invoke");
        }

        [Fact]
        public void InvokedWithShouldPopulateCorrectActionNameAndActionResultWithAsyncActionCall()
        {
            var testBuilder = MyViewComponent<AsyncComponent>
                .InvokedWith(c => c.InvokeAsync());

            this.CheckViewComponentResultTestBuilder(testBuilder, "InvokeAsync");
        }
        
        [Fact]
        public void InvokedWithShouldHaveValidEmptyModelState()
        {
            MyViewComponent<NormalComponent>
                .InvokedWith(c => c.Invoke())
                .ShouldReturn()
                .Content()
                .ShouldPassForThe<NormalComponent>(viewComponent =>
                {
                    var modelState = viewComponent.ModelState;

                    Assert.True(modelState.IsValid);
                    Assert.Empty(modelState.Values);
                    Assert.Empty(modelState.Keys);
                });
        }
        
        [Fact]
        public void WithAuthenticatedNotCalledShouldNotHaveAuthorizedUser()
        {
            MyViewComponent<NormalComponent>
                .InvokedWith(c => c.Invoke())
                .ShouldReturn()
                .Content()
                .ShouldPassForThe<NormalComponent>(viewComponent =>
                {
                    var user = viewComponent.User;

                    Assert.False(user.IsInRole("Any"));
                    Assert.Null(user.Identity.Name);
                    Assert.Null(user.Identity.AuthenticationType);
                    Assert.False(user.Identity.IsAuthenticated);
                });
        }

        [Fact]
        public void PrepareControllerShouldSetCorrectPropertiesWithCustomSetup()
        {
            MyViewComponent<NormalComponent>
                .Instance()
                .WithSetup(vc =>
                {
                    vc.ViewComponentContext = new ViewComponentContext();
                })
                .ShouldPassForThe<NormalComponent>(viewComponent =>
                {
                    Assert.NotNull(viewComponent);
                    Assert.NotNull(viewComponent.ViewComponentContext);
                    Assert.NotNull(viewComponent.ViewComponentContext.ViewContext);
                    Assert.NotNull(viewComponent.ViewComponentContext.ViewComponentDescriptor);
                    Assert.Null(viewComponent.ViewComponentContext.ViewComponentDescriptor.MethodInfo);
                });
        }

        [Fact]
        public void InvokedWithShouldPopulateCorrectActionDescriptor()
        {
            MyViewComponent<NormalComponent>
                .InvokedWith(vc => vc.Invoke())
                .ShouldPassForThe<NormalComponent>(viewComponent =>
                {
                    Assert.NotNull(viewComponent);
                    Assert.NotNull(viewComponent.ViewComponentContext);
                    Assert.NotNull(viewComponent.ViewComponentContext.ViewComponentDescriptor);
                    Assert.Equal("Invoke", viewComponent.ViewComponentContext.ViewComponentDescriptor.MethodInfo.Name);
                });
        }
        
        [Fact]
        public void UnresolvedRouteValuesShouldHaveFriendlyException()
        {
            Test.AssertException<InvalidOperationException>(
                () =>
                {
                    MyViewComponent<UrlComponent>
                        .InvokedWith(c => c.Invoke())
                        .ShouldReturn()
                        .Content();
                },
                "Route values are not present in the method call but are needed for successful pass of this test case. Consider calling 'WithRouteData' on the component builder to resolve them from the provided lambda expression or set the HTTP request path by using 'WithHttpRequest'.");
        }

        [Fact]
        public void NormalIndexOutOfRangeExceptionShouldShowNormalMessage()
        {
            Assert.Throws<InvocationAssertionException>(
                () =>
                {
                    MyViewComponent<ExceptionComponent>
                        .InvokedWith(c => c.Invoke())
                        .ShouldReturn()
                        .Content();
                });
        }

        [Fact]
        public void WithControllerContextShouldSetControllerContext()
        {
            var viewComponentContext = new ViewComponentContext
            {
                ViewComponentDescriptor = new ViewComponentDescriptor
                {
                    TypeInfo = typeof(NormalComponent).GetTypeInfo()
                }
            };

            MyViewComponent<NormalComponent>
                .Instance()
                .WithViewComponentContext(viewComponentContext)
                .ShouldPassForThe<NormalComponent>(viewComponent =>
                {
                    Assert.NotNull(viewComponent);
                    Assert.NotNull(viewComponent.ViewComponentContext);
                    Assert.Equal(
                        typeof(NormalComponent).GetTypeInfo(),
                        viewComponent.ViewComponentContext.ViewComponentDescriptor.TypeInfo);
                });
        }

        [Fact]
        public void WithControllerContextSetupShouldSetCorrectControllerContext()
        {
            MyViewComponent<NormalComponent>
                .Instance()
                .WithViewComponentContext(viewComponentContext =>
                {
                    viewComponentContext.ViewContext.RouteData.Values.Add("testkey", "testvalue");
                })
                .ShouldPassForThe<NormalComponent>(viewComponent =>
                {
                    Assert.NotNull(viewComponent);
                    Assert.NotNull(viewComponent.ViewComponentContext);
                    Assert.True(viewComponent.ViewComponentContext.ViewContext.RouteData.Values.ContainsKey("testkey"));
                });
        }
        
        [Fact]
        public void InvalidViewComponentTypeShouldThrowException()
        {
            Test.AssertException<InvalidOperationException>(
                () =>
                {
                    MyViewComponent<RequestModel>.Instance();
                },
                "RequestModel is not recognized as a valid view component type. Classes decorated with 'NonViewComponentAttribute' are not considered as passable view components. Additionally, make sure the SDK is set to 'Microsoft.NET.Sdk.Web' in your test project's '.csproj' file in order to enable proper view component discovery. If your type is still not recognized, you may manually add it in the application part manager by using the 'AddMvc().PartManager.ApplicationParts.Add(applicationPart))' method.");
        }

        [Fact]
        public void PrepareControllerShouldSetCorrectPropertiesWithDefaultServices()
        {
            MyViewComponent<NormalComponent>
                .Instance()
                .ShouldPassForThe<NormalComponent>(viewComponent =>
                {
                    Assert.NotNull(viewComponent);
                    Assert.NotNull(viewComponent.HttpContext);
                    Assert.NotNull(viewComponent.HttpContext.RequestServices);
                    Assert.NotNull(viewComponent.ViewComponentContext);
                    Assert.IsAssignableFrom<ViewComponentContextMock>(viewComponent.ViewComponentContext);
                    Assert.NotNull(viewComponent.ViewBag);
                    Assert.NotNull(viewComponent.ViewData);
                    Assert.NotNull(viewComponent.Request);
                    Assert.NotNull(viewComponent.RouteData);
                    Assert.NotNull(viewComponent.ModelState);
                    Assert.NotNull(viewComponent.Url);
                    Assert.NotNull(viewComponent.User);
                    Assert.NotNull(viewComponent.ViewContext);
                    Assert.IsAssignableFrom<ViewContextMock>(viewComponent.ViewContext);
                    Assert.NotNull(viewComponent.ViewEngine);
                    Assert.NotNull(viewComponent.ViewComponentContext.Arguments);
                    Assert.Empty(viewComponent.ViewComponentContext.Arguments);
                    Assert.NotNull(viewComponent.ViewComponentContext.HtmlEncoder);
                    Assert.NotNull(viewComponent.ViewComponentContext.ViewComponentDescriptor);
                    Assert.NotNull(viewComponent.ViewComponentContext.ViewContext);
                    Assert.NotNull(viewComponent.ViewComponentContext.ViewData);
                    Assert.NotNull(viewComponent.ViewComponentContext.Writer);
                    Assert.NotNull(viewComponent.ViewContext.ActionDescriptor);
                    Assert.False(viewComponent.ViewContext.ClientValidationEnabled);
                    Assert.Null(viewComponent.ViewContext.ExecutingFilePath);
                    Assert.NotNull(viewComponent.ViewContext.FormContext);
                    Assert.Equal(Html5DateRenderingMode.Rfc3339, viewComponent.ViewContext.Html5DateRenderingMode);
                    Assert.NotNull(viewComponent.ViewContext.HttpContext);
                    Assert.NotNull(viewComponent.ViewContext.ModelState);
                    Assert.NotNull(viewComponent.ViewContext.RouteData);
                    Assert.NotNull(viewComponent.ViewContext.TempData);
                    Assert.Null(viewComponent.ViewContext.ValidationMessageElement);
                    Assert.Null(viewComponent.ViewContext.ValidationSummaryMessageElement);
                    Assert.NotNull(viewComponent.ViewContext.View);
                    Assert.NotNull(viewComponent.ViewContext.ViewBag);
                    Assert.NotNull(viewComponent.ViewContext.ViewData);
                    Assert.NotNull(viewComponent.ViewContext.Writer);
                });
        }
        
        [Fact]
        public void PrepareControllerShouldSetCorrectPropertiesWithDefaultServicesForPocoComponent()
        {
            MyViewComponent<PocoViewComponent>
                .Instance()
                .ShouldPassForThe<PocoViewComponent>(viewComponent =>
                {
                    Assert.NotNull(viewComponent);
                    Assert.NotNull(viewComponent.Context);
                    Assert.NotNull(viewComponent.Context.ViewContext);
                    Assert.NotNull(viewComponent.Context.ViewContext.HttpContext);
                    Assert.NotNull(viewComponent.Context.ViewContext.HttpContext.RequestServices);
                    Assert.NotNull(viewComponent.Context);
                    Assert.IsAssignableFrom<ViewComponentContextMock>(viewComponent.Context);
                    Assert.NotNull(viewComponent.Context.ViewData);
                    Assert.NotNull(viewComponent.Context.ViewContext.HttpContext.Request);
                    Assert.NotNull(viewComponent.Context.ViewContext.HttpContext.User);
                    Assert.IsAssignableFrom<ViewContextMock>(viewComponent.Context.ViewContext);
                    Assert.NotNull(viewComponent.Context.Arguments);
                    Assert.Empty(viewComponent.Context.Arguments);
                    Assert.NotNull(viewComponent.Context.HtmlEncoder);
                    Assert.NotNull(viewComponent.Context.ViewComponentDescriptor);
                    Assert.NotNull(viewComponent.Context.ViewContext);
                    Assert.NotNull(viewComponent.Context.ViewData);
                    Assert.NotNull(viewComponent.Context.Writer);
                    Assert.NotNull(viewComponent.Context.ViewContext.ActionDescriptor);
                    Assert.False(viewComponent.Context.ViewContext.ClientValidationEnabled);
                    Assert.Null(viewComponent.Context.ViewContext.ExecutingFilePath);
                    Assert.NotNull(viewComponent.Context.ViewContext.FormContext);
                    Assert.Equal(Html5DateRenderingMode.Rfc3339, viewComponent.Context.ViewContext.Html5DateRenderingMode);
                    Assert.NotNull(viewComponent.Context.ViewContext.HttpContext);
                    Assert.NotNull(viewComponent.Context.ViewContext.ModelState);
                    Assert.NotNull(viewComponent.Context.ViewContext.RouteData);
                    Assert.NotNull(viewComponent.Context.ViewContext.TempData);
                    Assert.Null(viewComponent.Context.ViewContext.ValidationMessageElement);
                    Assert.Null(viewComponent.Context.ViewContext.ValidationSummaryMessageElement);
                    Assert.NotNull(viewComponent.Context.ViewContext.View);
                    Assert.NotNull(viewComponent.Context.ViewContext.ViewBag);
                    Assert.NotNull(viewComponent.Context.ViewContext.ViewData);
                    Assert.NotNull(viewComponent.Context.ViewContext.Writer);
                });
        }

        [Fact]
        public void WithArgumentsShouldResolveCorrectly()
        {
            MyViewComponent<ArgumentsComponent>
                .InvokedWith(vc => vc.Invoke(1, new RequestModel { RequiredString = "Test" }))
                .ShouldReturn()
                .Content("1,Test")
                .ShouldPassForThe<ArgumentsComponent>(component => component.ViewComponentContext.Arguments.Count == 2);
        }
        
        [Fact]
        public void NonViewComponentShouldThrowException()
        {
            Test.AssertException<InvalidOperationException>(
                () =>
                {
                    MyViewComponent<NonViewComponent>.Instance();
                },
                "NonViewComponent is not recognized as a valid view component type. Classes decorated with 'NonViewComponentAttribute' are not considered as passable view components. Additionally, make sure the SDK is set to 'Microsoft.NET.Sdk.Web' in your test project's '.csproj' file in order to enable proper view component discovery. If your type is still not recognized, you may manually add it in the application part manager by using the 'AddMvc().PartManager.ApplicationParts.Add(applicationPart))' method.");
        }

        private void CheckViewComponentResultTestBuilder<TInvocationResult>(
            IViewComponentResultTestBuilder<TInvocationResult> testBuilder,
            string expectedActionName)
        {
            this.CheckActionName(testBuilder, expectedActionName);

            testBuilder
                .ShouldPassForThe<IViewComponentResult>(actionResult =>
                {
                    Assert.NotNull(actionResult);
                });
        }

        private void CheckActionName(IBaseTestBuilderWithViewComponent testBuilder, string expectedMethodName)
        {
            var methodName = (testBuilder as BaseTestBuilderWithViewComponent)?.TestContext.MethodName;

            Assert.NotNull(methodName);
            Assert.NotEmpty(methodName);
            Assert.Equal(expectedMethodName, methodName);
        }
    }
}
