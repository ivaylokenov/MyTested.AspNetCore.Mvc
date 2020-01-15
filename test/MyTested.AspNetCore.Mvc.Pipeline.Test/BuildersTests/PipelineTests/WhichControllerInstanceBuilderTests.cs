namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.PipelineTests
{
    using Exceptions;
    using Microsoft.Extensions.DependencyInjection;
    using Setups;
    using Setups.Pipeline;
    using Setups.Routing;
    using Setups.Services;
    using Xunit;

    public class WhichControllerInstanceBuilderTests
    {
        [Fact]
        public void WhichShouldResolveCorrectSyncAction()
        {
            MyPipeline
                .Configuration()
                .ShouldMap("/Home/Contact/1")
                .To<HomeController>(c => c.Contact(1))
                .Which()
                .ShouldReturn()
                .Ok(ok => ok
                    .Passing(result => result
                        .Value
                        .Equals(1)));
        }

        [Fact]
        public void WhichShouldResolveCorrectAsyncAction()
        {
            MyPipeline
                .Configuration()
                .ShouldMap("/Home/AsyncMethod")
                .To<HomeController>(c => c.AsyncMethod())
                .Which()
                .ShouldReturn()
                .Ok(ok => ok
                    .Passing(result => result
                        .Value
                        .Equals("Test")));
        }

        [Fact]
        public void WhichShouldResolveCorrectEmptyAction()
        {
            MyPipeline
                .Configuration()
                .ShouldMap("/Home/Empty")
                .To<HomeController>(c => c.Empty())
                .Which()
                .ShouldReturnEmpty();
        }

        [Fact]
        public void WhichShouldResolveCorrectEmptyAsyncAction()
        {
            MyPipeline
                .Configuration()
                .ShouldMap("/Home/EmptyTask")
                .To<HomeController>(c => c.EmptyTask())
                .Which()
                .ShouldReturnEmpty();
        }

        [Fact]
        public void WhichShouldResolveCorrectAsyncActionWithSetup()
        {
            const string testData = "TestData";

            MyPipeline
                .Configuration()
                .ShouldMap("/Home/AsyncMethod")
                .To<HomeController>(c => c.AsyncMethod())
                .Which()
                .WithSetup(c => c.Data = testData)
                .ShouldReturn()
                .Ok(ok => ok
                    .Passing(result => result
                        .Value
                        .Equals(testData)));
        }

        [Fact]
        public void WhichShouldResolveCorrectAsyncActionWithControllerInstance()
        {
            const string testData = "TestData";

            MyPipeline
                .Configuration()
                .ShouldMap("/Home/AsyncMethod")
                .To<HomeController>(c => c.AsyncMethod())
                .Which(new HomeController
                {
                    Data = testData
                })
                .ShouldReturn()
                .Ok(ok => ok
                    .Passing(result => result
                        .Value
                        .Equals(testData)));
        }

        [Fact]
        public void WhichShouldResolveCorrectAsyncActionWithControllerConstructionFunc()
        {
            const string testData = "TestData";

            MyPipeline
                .Configuration()
                .ShouldMap("/Home/AsyncMethod")
                .To<HomeController>(c => c.AsyncMethod())
                .Which(() => new HomeController
                {
                    Data = testData
                })
                .ShouldReturn()
                .Ok(ok => ok
                    .Passing(result => result
                        .Value
                        .Equals(testData)));
        }

        [Fact]
        public void WhichShouldResolveCorrectAsyncActionWithInnerSetup()
        {
            const string testData = "TestData";

            MyPipeline
                .Configuration()
                .ShouldMap("/Home/AsyncMethod")
                .To<HomeController>(c => c.AsyncMethod())
                .Which(controller => controller
                    .WithSetup(c => c.Data = testData))
                .ShouldReturn()
                .Ok(ok => ok
                    .Passing(result => result
                        .Value
                        .Equals(testData)));
        }

        [Fact]
        public void WhichShouldNotResolveCorrectActionResultWhenFilterSetsIt()
        {
            Test.AssertException<RouteAssertionException>(
                () =>
                {
                    MyPipeline
                        .Configuration()
                        .ShouldMap("/Normal/CustomFiltersAction?result=true")
                        .To<NormalController>(c => c.CustomFiltersAction())
                        .Which()
                        .ShouldReturn()
                        .Ok();
                },
                "Expected route '/Normal/CustomFiltersAction' to match CustomFiltersAction action in NormalController but action could not be invoked because of the declared filters - CustomActionFilterAttribute (Action), UnsupportedContentTypeFilter (Global), SaveTempDataAttribute (Global), ControllerActionFilter (Controller). Either a filter is setting the response result before the action itself, or you must set the request properties so that they will pass through the pipeline.");
        }

        [Fact]
        public void WhichShouldResolveCorrectActionFilterLogic()
        {
            MyPipeline
                .Configuration()
                .ShouldMap("/Normal/CustomFiltersAction?controller=true")
                .To<NormalController>(c => c.CustomFiltersAction())
                .Which()
                .ShouldReturn()
                .Ok()
                .AndAlso()
                .ShouldPassForThe<NormalController>(controller =>
                {
                    Assert.Equal("ActionFilter", controller.Data);
                });
        }

        [Fact]
        public void WhichShouldResolveCorrectActionFilterLogicWithGlobalServices()
        {
            MyApplication
                .StartsFrom<DefaultStartup>()
                .WithServices(services => services
                    .AddTransient<IInjectedService, InjectedService>());

            MyPipeline
                .Configuration()
                .ShouldMap("/Pipeline/FilterAction?controller=true")
                .To<PipelineController>(c => c.FilterAction())
                .Which()
                .ShouldReturn()
                .Ok()
                .AndAlso()
                .ShouldPassForThe<PipelineController>(controller =>
                {
                    const string testValue = "ActionFilter";
                    Assert.Equal(testValue, controller.Data);
                    Assert.True(controller.RouteData.Values.ContainsKey(testValue));
                    Assert.True(controller.ControllerContext.ActionDescriptor.Properties.ContainsKey(testValue));
                    Assert.True(controller.ModelState.ContainsKey(testValue));
                    Assert.NotNull(controller.HttpContext.Features.Get<PipelineController>());
                });

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void WhichShouldResolveCorrectControllerFilterLogicWithGlobalServices()
        {
            MyApplication
                .StartsFrom<DefaultStartup>()
                .WithServices(services => services
                    .AddTransient<IInjectedService, InjectedService>());

            MyPipeline
                .Configuration()
                .ShouldMap("/Pipeline/Action?controller=true")
                .To<PipelineController>(c => c.Action())
                .Which()
                .ShouldReturn()
                .Ok()
                .AndAlso()
                .ShouldPassForThe<PipelineController>(controller =>
                {
                    const string testValue = "ControllerFilter";
                    Assert.Equal(testValue, controller.Data);
                    Assert.True(controller.RouteData.Values.ContainsKey(testValue));
                    Assert.True(controller.ControllerContext.ActionDescriptor.Properties.ContainsKey(testValue));
                    Assert.True(controller.ModelState.ContainsKey(testValue));
                    Assert.NotNull(controller.HttpContext.Features.Get<PipelineController>());
                });

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void WhichShouldResolveCorrectActionFilterLogicWithExplicitServices()
        {
            MyApplication
                .StartsFrom<DefaultStartup>()
                .WithServices(services => services
                    .AddTransient<IInjectedService, InjectedService>());

            MyPipeline
                .Configuration()
                .ShouldMap("/Pipeline/FilterAction?controller=true")
                .To<PipelineController>(c => c.FilterAction())
                .Which(controller => controller
                    .WithDependencies(new InjectedService()))
                .ShouldReturn()
                .Ok()
                .AndAlso()
                .ShouldPassForThe<PipelineController>(controller =>
                {
                    const string testValue = "ActionFilter";
                    Assert.Equal(testValue, controller.Data);
                    Assert.True(controller.RouteData.Values.ContainsKey(testValue));
                    Assert.True(controller.ControllerContext.ActionDescriptor.Properties.ContainsKey(testValue));
                    Assert.True(controller.ModelState.ContainsKey(testValue));
                    Assert.NotNull(controller.HttpContext.Features.Get<PipelineController>());
                });

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void WhichShouldResolveCorrectControllerFilterLogicWithExplicitServices()
        {
            MyApplication
                .StartsFrom<DefaultStartup>()
                .WithServices(services => services
                    .AddTransient<IInjectedService, InjectedService>());

            MyPipeline
                .Configuration()
                .ShouldMap("/Pipeline/Action?controller=true")
                .To<PipelineController>(c => c.Action())
                .Which(controller => controller
                    .WithDependencies(new InjectedService()))
                .ShouldReturn()
                .Ok()
                .AndAlso()
                .ShouldPassForThe<PipelineController>(controller =>
                {
                    const string testValue = "ControllerFilter";
                    Assert.Equal(testValue, controller.Data);
                    Assert.True(controller.RouteData.Values.ContainsKey(testValue));
                    Assert.True(controller.ControllerContext.ActionDescriptor.Properties.ContainsKey(testValue));
                    Assert.True(controller.ModelState.ContainsKey(testValue));
                    Assert.NotNull(controller.HttpContext.Features.Get<PipelineController>());
                });

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void WhichShouldNotResolveActionFilterLogicWithExplicitControllerInstance()
        {
            MyApplication
                .StartsFrom<DefaultStartup>()
                .WithServices(services => services
                    .AddTransient<IInjectedService, InjectedService>());

            var injectedService = new InjectedService();

            MyPipeline
                .Configuration()
                .ShouldMap("/Pipeline/FilterAction?controller=true")
                .To<PipelineController>(c => c.FilterAction())
                .Which(new PipelineController(injectedService))
                .ShouldReturn()
                .Ok()
                .AndAlso()
                .ShouldPassForThe<PipelineController>(controller =>
                {
                    Assert.Same(injectedService, controller.Service);
                    Assert.Null(controller.Data);
                });

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void WhichShouldNotResolveControllerFilterLogicWithExplicitControllerInstance()
        {
            MyApplication
                .StartsFrom<DefaultStartup>()
                .WithServices(services => services
                    .AddTransient<IInjectedService, InjectedService>());

            var injectedService = new InjectedService();

            MyPipeline
                .Configuration()
                .ShouldMap("/Pipeline/Action?controller=true")
                .To<PipelineController>(c => c.Action())
                .Which(new PipelineController(injectedService))
                .ShouldReturn()
                .Ok()
                .AndAlso()
                .ShouldPassForThe<PipelineController>(controller =>
                {
                    Assert.Same(injectedService, controller.Service);
                    Assert.Null(controller.Data);
                });

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void WhichShouldNotResolveActionFilterLogicWithControllerConstructionFunction()
        {
            MyApplication
                .StartsFrom<DefaultStartup>()
                .WithServices(services => services
                    .AddTransient<IInjectedService, InjectedService>());

            var injectedService = new InjectedService();

            MyPipeline
                .Configuration()
                .ShouldMap("/Pipeline/FilterAction?controller=true")
                .To<PipelineController>(c => c.FilterAction())
                .Which(() => new PipelineController(injectedService))
                .ShouldReturn()
                .Ok()
                .AndAlso()
                .ShouldPassForThe<PipelineController>(controller =>
                {
                    Assert.Same(injectedService, controller.Service);
                    Assert.Null(controller.Data);
                });

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void WhichShouldNotResolveControllerFilterLogicWithControllerConstructionFunction()
        {
            MyApplication
                .StartsFrom<DefaultStartup>()
                .WithServices(services => services
                    .AddTransient<IInjectedService, InjectedService>());

            var injectedService = new InjectedService();

            MyPipeline
                .Configuration()
                .ShouldMap("/Pipeline/Action?controller=true")
                .To<PipelineController>(c => c.Action())
                .Which(() => new PipelineController(injectedService))
                .ShouldReturn()
                .Ok()
                .AndAlso()
                .ShouldPassForThe<PipelineController>(controller =>
                {
                    Assert.Same(injectedService, controller.Service);
                    Assert.Null(controller.Data);
                });

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void WhichShouldNotResolveControllerContextWhenWithMethodsAreCalled()
        {
            MyApplication
                   .StartsFrom<DefaultStartup>()
                   .WithServices(services => services
                       .AddTransient<IInjectedService, InjectedService>());

            const string contextTestKey = "ControllerContext";
            const string contextTestValue = "Context Value";

            MyPipeline
                .Configuration()
                .ShouldMap("/Pipeline/Action?controller=true")
                .To<PipelineController>(c => c.Action())
                .Which()
                .WithHttpContext(context => context.Features.Set(new AnotherInjectedService()))
                .WithHttpRequest(request => request.WithHeader(contextTestKey, contextTestValue))
                .WithUser(user => user.WithUsername(contextTestKey))
                .WithRouteData(new { Id = contextTestKey })
                .WithControllerContext(context => context.RouteData.Values.Add(contextTestKey, contextTestValue))
                .WithActionContext(context => context.ModelState.AddModelError(contextTestKey, contextTestValue))
                .WithTempData(tempData => tempData.WithEntry(contextTestKey, contextTestValue))
                .WithSetup(controller => controller.HttpContext.Features.Set(new InjectedService()))
                .ShouldReturn()
                .Ok()
                .AndAlso()
                .ShouldPassForThe<PipelineController>(controller =>
                {
                    const string testValue = "ControllerFilter";
                    Assert.Equal(testValue, controller.Data);
                    Assert.True(controller.RouteData.Values.ContainsKey(testValue));
                    Assert.True(controller.RouteData.Values.ContainsKey(contextTestKey));
                    Assert.True(controller.RouteData.Values.ContainsKey("Id"));
                    Assert.True(controller.ControllerContext.ActionDescriptor.Properties.ContainsKey(testValue));
                    Assert.True(controller.ModelState.ContainsKey(testValue));
                    Assert.True(controller.ModelState.ContainsKey(contextTestKey));
                    Assert.NotNull(controller.HttpContext.Features.Get<PipelineController>());
                    Assert.NotNull(controller.HttpContext.Features.Get<InjectedService>());
                    Assert.NotNull(controller.HttpContext.Features.Get<AnotherInjectedService>());
                    Assert.True(controller.HttpContext.Request.Headers.ContainsKey(contextTestKey));
                    Assert.True(controller.TempData.ContainsKey(contextTestKey));
                    Assert.True(controller.User.Identity.Name == contextTestKey);
                });

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void WhichShouldNotResolveControllerContextWhenWithMethodsAreCalledInInnerBuilder()
        {
            MyApplication
                   .StartsFrom<DefaultStartup>()
                   .WithServices(services => services
                       .AddTransient<IInjectedService, InjectedService>());

            const string contextTestKey = "ControllerContext";
            const string contextTestValue = "Context Value";

            MyPipeline
                .Configuration()
                .ShouldMap("/Pipeline/Action?controller=true")
                .To<PipelineController>(c => c.Action())
                .Which(pipelineController => pipelineController
                    .WithHttpContext(context => context.Features.Set(new AnotherInjectedService()))
                    .WithHttpRequest(request => request.WithHeader(contextTestKey, contextTestValue))
                    .WithUser(user => user.WithUsername(contextTestKey))
                    .WithRouteData(new { Id = contextTestKey })
                    .WithControllerContext(context => context.RouteData.Values.Add(contextTestKey, contextTestValue))
                    .WithActionContext(context => context.ModelState.AddModelError(contextTestKey, contextTestValue))
                    .WithTempData(tempData => tempData.WithEntry(contextTestKey, contextTestValue))
                    .WithSetup(controller => controller.HttpContext.Features.Set(new InjectedService())))
                .ShouldReturn()
                .Ok()
                .AndAlso()
                .ShouldPassForThe<PipelineController>(controller =>
                {
                    const string testValue = "ControllerFilter";
                    Assert.Equal(testValue, controller.Data);
                    Assert.True(controller.RouteData.Values.ContainsKey(testValue));
                    Assert.True(controller.RouteData.Values.ContainsKey(contextTestKey));
                    Assert.True(controller.RouteData.Values.ContainsKey("Id"));
                    Assert.True(controller.ControllerContext.ActionDescriptor.Properties.ContainsKey(testValue));
                    Assert.True(controller.ModelState.ContainsKey(testValue));
                    Assert.True(controller.ModelState.ContainsKey(contextTestKey));
                    Assert.NotNull(controller.HttpContext.Features.Get<PipelineController>());
                    Assert.NotNull(controller.HttpContext.Features.Get<InjectedService>());
                    Assert.NotNull(controller.HttpContext.Features.Get<AnotherInjectedService>());
                    Assert.True(controller.HttpContext.Request.Headers.ContainsKey(contextTestKey));
                    Assert.True(controller.TempData.ContainsKey(contextTestKey));
                    Assert.True(controller.User.Identity.Name == contextTestKey);
                });

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void WhichShouldOverrideActionFilterValues()
        {
            MyApplication
                      .StartsFrom<DefaultStartup>()
                      .WithServices(services => services
                          .AddTransient<IInjectedService, InjectedService>());

            const string contextTestKey = "ControllerFilter";
            const string contextTestValue = "Context Value";

            MyPipeline
                .Configuration()
                .ShouldMap("/Pipeline/Action?controller=true")
                .To<PipelineController>(c => c.Action())
                .Which()
                .WithRouteData(new { ControllerFilter = contextTestValue })
                .WithControllerContext(context => context.ActionDescriptor.Properties[contextTestKey] = contextTestValue)
                .WithActionContext(context => context.ModelState.Clear())
                .ShouldReturn()
                .Ok()
                .AndAlso()
                .ShouldPassForThe<PipelineController>(controller =>
                {
                    Assert.Equal(contextTestKey, controller.Data);
                    Assert.True(controller.RouteData.Values.ContainsKey(contextTestKey));
                    Assert.Equal(contextTestValue, controller.RouteData.Values[contextTestKey]);
                    Assert.True(controller.ControllerContext.ActionDescriptor.Properties.ContainsKey(contextTestKey));
                    Assert.Equal(contextTestValue, controller.ControllerContext.ActionDescriptor.Properties[contextTestKey]);
                    Assert.Empty(controller.ModelState);
                });

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void WhichShouldOverrideActionFilterValuesInInnerBuilder()
        {
            MyApplication
                      .StartsFrom<DefaultStartup>()
                      .WithServices(services => services
                          .AddTransient<IInjectedService, InjectedService>());

            const string contextTestKey = "ControllerFilter";
            const string contextTestValue = "Context Value";

            MyPipeline
                .Configuration()
                .ShouldMap("/Pipeline/Action?controller=true")
                .To<PipelineController>(c => c.Action())
                .Which(controller => controller
                    .WithRouteData(new { ControllerFilter = contextTestValue })
                    .WithControllerContext(context => context.ActionDescriptor.Properties[contextTestKey] = contextTestValue)
                    .WithActionContext(context => context.ModelState.Clear()))
                .ShouldReturn()
                .Ok()
                .AndAlso()
                .ShouldPassForThe<PipelineController>(controller =>
                {
                    Assert.Equal(contextTestKey, controller.Data);
                    Assert.True(controller.RouteData.Values.ContainsKey(contextTestKey));
                    Assert.Equal(contextTestValue, controller.RouteData.Values[contextTestKey]);
                    Assert.True(controller.ControllerContext.ActionDescriptor.Properties.ContainsKey(contextTestKey));
                    Assert.Equal(contextTestValue, controller.ControllerContext.ActionDescriptor.Properties[contextTestKey]);
                    Assert.Empty(controller.ModelState);
                });

            MyApplication.StartsFrom<DefaultStartup>();
        }
    }
}
