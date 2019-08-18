namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.ActionResultsTests.LocalRedirectTests
{
    using Exceptions;
    using Setups;
    using Setups.Controllers;
    using Setups.Startups;
    using Xunit;

    public class LocalRedirectTestBuilderTests
    {
        [Fact]
        public void ToShouldWorkCorrectly()
        {
            MyApplication.StartsFrom<RoutingStartup>();

            Test.AssertException<RedirectResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.LocalRedirectActionWithCustomUrlHelper(null))
                        .ShouldReturn()
                        .LocalRedirect(localRedirect => localRedirect
                            .To<NoAttributesController>(c => c.WithParameter(1)));
                },
                "When calling LocalRedirectActionWithCustomUrlHelper action in MvcController expected local redirect result to have resolved location to '/api/Redirect/WithParameter?id=1', but in fact received '/api/test'.");

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void ToShouldWorkCorrectlyToAsyncAction()
        {
            MyApplication.StartsFrom<RoutingStartup>();

            MyController<MvcController>
                .Instance()
                .Calling(c => c.LocalRedirectActionWithCustomUrlHelper(null))
                .ShouldReturn()
                .LocalRedirect(localRedirect => localRedirect
                    .To<MvcController>(c => c.AsyncOkResultAction()));

            MyApplication.StartsFrom<DefaultStartup>();
        }
    }
}
