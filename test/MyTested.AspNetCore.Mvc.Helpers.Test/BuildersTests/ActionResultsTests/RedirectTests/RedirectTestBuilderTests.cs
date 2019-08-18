namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.ActionResultsTests.RedirectTests
{
    using Exceptions;
    using Setups;
    using Setups.Controllers;
    using Setups.Startups;
    using Xunit;

    public class RedirectTestBuilderTests
    {
        [Fact]
        public void ToShouldWorkCorrectlyWithCorrectActionCall()
        {
            MyApplication.StartsFrom<RoutingStartup>();

            MyController<MvcController>
                .Instance()
                .Calling(c => c.RedirectToRouteAction())
                .ShouldReturn()
                .Redirect(redirect => redirect
                    .To<NoAttributesController>(c => c.WithParameter(1)));

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void ToShouldWorkCorrectlyWithWithAnyCall()
        {
            MyApplication.StartsFrom<RoutingStartup>();

            MyController<MvcController>
                .Instance()
                .Calling(c => c.RedirectToRouteAction())
                .ShouldReturn()
                .Redirect(redirect => redirect
                    .To<NoAttributesController>(c => c.WithParameter(With.Any<int>())));

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void ToShouldWorkCorrectlyWithCorrectVoidActionCall()
        {
            MyApplication.StartsFrom<RoutingStartup>();

            MyController<MvcController>
                .Instance()
                .Calling(c => c.RedirectToRouteVoidAction())
                .ShouldReturn()
                .Redirect(redirect => redirect
                    .To<NoAttributesController>(c => c.VoidAction()));

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void ToShouldWorkCorrectlyWithCorrectTaskActionCall()
        {
            MyApplication.StartsFrom<RoutingStartup>();

            Test.AssertException<RedirectResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.RedirectToRouteAction())
                        .ShouldReturn()
                        .Redirect(redirect => redirect
                            .To<MvcController>(c => c.AsyncOkResultAction()));
                },
                "When calling RedirectToRouteAction action in MvcController expected redirect result to have resolved location to '/api/test', but in fact received '/api/Redirect/WithParameter?id=1'.");

            MyApplication.StartsFrom<DefaultStartup>();
        }
    }
}
