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
    }
}
