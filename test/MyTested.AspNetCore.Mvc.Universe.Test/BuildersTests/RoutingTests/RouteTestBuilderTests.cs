namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.RoutingTests
{
    using Microsoft.AspNetCore.Builder;
    using Setups;
    using Setups.Routing;
    using Xunit;

    public class RouteTestBuilderTests
    {
        [Fact]
        public void RouteWithVersionValueShouldMapCorrectlyWithoutApiVersioning()
        {
            MyApplication
                .StartsFrom<TestStartup>()
                .WithRoutes(routes => routes
                    .MapRoute(
                        name: "versioned",
                        template: "api/{version}/{controller}/{action}/{id?}"));

            MyRouting
                .Configuration()
                .ShouldMap("/api/1.0/Home/Contact/1")
                .To<HomeController>(c => c.Contact(1))
                .AndAlso()
                .ToRouteValue("version", "1.0");

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void PipelineWithVersionValueShouldMapCorrectlyWithoutApiVersioning()
        {
            MyApplication
                .StartsFrom<TestStartup>()
                .WithRoutes(routes => routes
                    .MapRoute(
                        name: "versioned",
                        template: "api/{version}/{controller}/{action}/{id?}"));

            MyPipeline
                .Configuration()
                .ShouldMap("/api/v2/Home/Contact/1")
                .To<HomeController>(c => c.Contact(1))
                .Which()
                .ShouldReturn()
                .Ok(ok => ok
                    .WithModel(1));

            MyApplication.StartsFrom<DefaultStartup>();
        }
    }
}
