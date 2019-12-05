namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.RoutingTests
{
    using Exceptions;
    using Setups;
    using Setups.Routing;
    using Xunit;

    public class RouteTestBuilderTests
    {
        [Fact]
        public void ShouldMapShouldExecuteActionFiltersAndShouldValidateRoutes()
        {
            MyPipeline
                .Configuration()
                .ShouldMap(request => request
                    .WithLocation("/Normal/FiltersAction")
                    .WithAntiForgeryToken())
                .To<NormalController>(c => c.FiltersAction());
        }

        [Fact]
        public void ShouldMapShouldExecuteActionFiltersAndShouldValidateRoutesAndModelBinding()
        {
            MyPipeline
                .Configuration()
                .ShouldMap(request => request
                    .WithLocation("/Normal/FiltersActionWithModelBinding/1")
                    .WithMethod(HttpMethod.Post)
                    .WithAntiForgeryToken()
                    .WithJsonBody(new
                    {
                        Integer = 1,
                        String = "Text"
                    }))
                .To<NormalController>(c => c.FiltersActionWithModelBinding(
                    1,
                    new RequestModel
                    {
                        Integer = 1,
                        String = "Text"
                    }));
        }

        [Fact]
        public void ShouldMapShouldThrowExceptionIfFiltersAreNotSet()
        {
            Test.AssertException<RouteAssertionException>(
                () =>
                {
                    MyPipeline
                        .Configuration()
                        .ShouldMap(request => request
                            .WithLocation("/Normal/FiltersAction"))
                        .To<NormalController>(c => c.FiltersAction());
                },
                "Expected route '/Normal/FiltersAction' to match FiltersAction action in NormalController but action could not be invoked because of the declared filters. You must set the request properties so that they will pass through the pipeline.");
        }
    }
}
