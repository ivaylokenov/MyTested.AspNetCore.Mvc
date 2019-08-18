namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.ActionResultsTests.CreatedTests
{
    using Exceptions;
    using Setups;
    using Setups.Controllers;
    using Setups.Startups;
    using Xunit;

    public class CreatedTestBuilderTests
    {
        [Fact]
        public void AtShouldWorkCorrectlyWithCorrectActionCall()
        {
            MyApplication.StartsFrom<RoutingStartup>();

            MyController<MvcController>
                .Instance()
                .Calling(c => c.CreatedAtRouteAction())
                .ShouldReturn()
                .Created(created => created
                    .At<NoAttributesController>(c => c.WithParameter(1)));

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void AtShouldWorkCorrectlyWithCorrectTaskActionCall()
        {
            MyApplication.StartsFrom<RoutingStartup>();

            Test.AssertException<CreatedResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.CreatedAtRouteAction())
                        .ShouldReturn()
                        .Created(created => created
                            .At<MvcController>(c => c.AsyncOkResultAction()));
                },
                "When calling CreatedAtRouteAction action in MvcController expected created result to have resolved location to '/api/test', but in fact received '/api/Redirect/WithParameter?id=1'.");

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void AtShouldWorkCorrectlyWithCorrectVoidActionCall()
        {
            MyApplication.StartsFrom<RoutingStartup>();

            MyController<MvcController>
                .Instance()
                .Calling(c => c.CreatedAtRouteVoidAction())
                .ShouldReturn()
                .Created(created => created
                    .At<NoAttributesController>(c => c.VoidAction()));

            MyApplication.StartsFrom<DefaultStartup>();
        }
    }
}
