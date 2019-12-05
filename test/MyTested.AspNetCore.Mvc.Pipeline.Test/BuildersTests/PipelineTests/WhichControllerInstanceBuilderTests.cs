namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.PipelineTests
{
    using Microsoft.Extensions.DependencyInjection;
    using MyTested.AspNetCore.Mvc.Exceptions;
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
                .ShouldPassForThe<PipelineController>(controller =>
                {
                    Assert.Equal("ActionFilter", controller.Data);
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
                .ShouldPassForThe<PipelineController>(controller =>
                {
                    Assert.Equal("ControllerFilter", controller.Data);
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
                .ShouldPassForThe<PipelineController>(controller =>
                {
                    Assert.Equal("ActionFilter", controller.Data);
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
                .ShouldPassForThe<PipelineController>(controller =>
                {
                    Assert.Equal("ControllerFilter", controller.Data);
                });

            MyApplication.StartsFrom<DefaultStartup>();
        }
    }
}
