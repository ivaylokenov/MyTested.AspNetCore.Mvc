namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.RoutingTests
{
    using Setups.Controllers;
    using Xunit;

    public class RouteTestBuilderTests
    {
        [Fact]
        public void RouteAssertionShouldWorkCorrectlyWithVersioning()
        {
            MyRouting
                .Configuration()
                .ShouldMap("api/v2.0/versioning")
                .To<VersioningController>(c => c.Index());
        }

        [Fact]
        public void RouteAssertionShouldWorkCorrectlyWithVersioningWhichDoesNotExist()
        {
            MyRouting
                .Configuration()
                .ShouldMap("api/v1.0/versioning")
                .ToNonExistingRoute();
        }

        [Fact]
        public void RouteAssertionShouldWorkCorrectlyWithQueryVersioning()
        {
            MyRouting
                .Configuration()
                .ShouldMap("api/versioning?api-version=2.0")
                .To<QueryVersioningController>(c => c.Index());
        }

        [Fact]
        public void RouteAssertionShouldWorkCorrectlyWithQueryVersioningWhichDoesNotExist()
        {
            MyRouting
                .Configuration()
                .ShouldMap("api/versioning?api-version=1.0")
                .ToNonExistingRoute();
        }

        [Fact]
        public void RouteAssertionShouldWorkCorrectlyWithActionVersioningWhichDoesNotExist()
        {
            MyRouting
                .Configuration()
                .ShouldMap("api/v3.0/versioning")
                .To<VersioningController>(c => c.SpecificVersion());
        }
    }
}
