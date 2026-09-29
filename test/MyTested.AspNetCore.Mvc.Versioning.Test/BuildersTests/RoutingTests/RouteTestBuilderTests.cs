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
                .ShouldMap("api/versioning?v=2.0")
                .To<QueryVersioningController>(c => c.Index());
        }

        [Fact]
        public void RouteAssertionShouldWorkCorrectlyWithQueryVersioningWhichDoesNotExist()
        {
            MyRouting
                .Configuration()
                .ShouldMap("api/versioning?v=1.0")
                .ToNonExistingRoute();
        }

        [Fact]
        public void RouteAssertionShouldWorkCorrectlyWithActionVersioning()
        {
            MyRouting
                .Configuration()
                .ShouldMap("api/v3.0/versioning")
                .To<VersioningController>(c => c.SpecificVersion());
        }

        [Fact]
        public void RouteAssertionShouldWorkCorrectlyWithHeaderVersioning()
        {
            MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithLocation("api/header-versioning")
                    .WithHeader("X-Custom-Version", "1.0"))
                .To<HeaderVersioningController>(c => c.Index());
        }

        [Fact]
        public void RouteAssertionShouldWorkCorrectlyWithHeaderVersioningWhichDoesNotExist()
        {
            MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithLocation("api/header-versioning")
                    .WithHeader("X-Custom-Version", "2.0"))
                .ToNonExistingRoute();
        }

        [Fact]
        public void RouteAssertionShouldWorkCorrectlyWithVersionNeutralQueryController()
        {
            MyRouting
                .Configuration()
                .ShouldMap("api/neutral-query?v=99.0")
                .To<NeutralQueryController>(c => c.Index());
        }

        [Fact]
        public void RouteAssertionShouldWorkCorrectlyWithVersionNeutralQueryControllerWithoutVersion()
        {
            MyRouting
                .Configuration()
                .ShouldMap("api/neutral-query")
                .To<NeutralQueryController>(c => c.Index());
        }

        [Fact]
        public void RouteAssertionShouldWorkCorrectlyWithVersionNeutralHeaderController()
        {
            MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithLocation("api/neutral-header")
                    .WithHeader("X-Custom-Version", "99.0"))
                .To<NeutralHeaderController>(c => c.Index());
        }

        [Fact]
        public void RouteAssertionShouldWorkCorrectlyWithVersionNeutralHeaderControllerWithoutVersion()
        {
            MyRouting
                .Configuration()
                .ShouldMap("api/neutral-header")
                .To<NeutralHeaderController>(c => c.Index());
        }

        [Fact]
        public void RouteAssertionShouldWorkCorrectlyWithUrlSegmentCompetingV1()
        {
            MyRouting
                .Configuration()
                .ShouldMap("api/v1.0/competing")
                .To<UrlCompetingV1Controller>(c => c.Index());
        }

        [Fact]
        public void RouteAssertionShouldWorkCorrectlyWithUrlSegmentCompetingV2()
        {
            MyRouting
                .Configuration()
                .ShouldMap("api/v2.0/competing")
                .To<UrlCompetingV2Controller>(c => c.Index());
        }

        [Fact]
        public void RouteAssertionShouldWorkCorrectlyWithUrlSegmentCompetingNonExistentVersion()
        {
            MyRouting
                .Configuration()
                .ShouldMap("api/v3.0/competing")
                .ToNonExistingRoute();
        }

        [Fact]
        public void RouteAssertionShouldWorkCorrectlyWithQueryCompetingV1()
        {
            MyRouting
                .Configuration()
                .ShouldMap("api/query-competing?v=1.0")
                .To<QueryCompetingV1Controller>(c => c.Index());
        }

        [Fact]
        public void RouteAssertionShouldWorkCorrectlyWithQueryCompetingV2()
        {
            MyRouting
                .Configuration()
                .ShouldMap("api/query-competing?v=2.0")
                .To<QueryCompetingV2Controller>(c => c.Index());
        }

        [Fact]
        public void RouteAssertionShouldWorkCorrectlyWithQueryCompetingNonExistentVersion()
        {
            MyRouting
                .Configuration()
                .ShouldMap("api/query-competing?v=3.0")
                .ToNonExistingRoute();
        }

        [Fact]
        public void RouteAssertionShouldWorkCorrectlyWithHeaderCompetingV1()
        {
            MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithLocation("api/header-competing")
                    .WithHeader("X-Custom-Version", "1.0"))
                .To<HeaderCompetingV1Controller>(c => c.Index());
        }

        [Fact]
        public void RouteAssertionShouldWorkCorrectlyWithHeaderCompetingV2()
        {
            MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithLocation("api/header-competing")
                    .WithHeader("X-Custom-Version", "2.0"))
                .To<HeaderCompetingV2Controller>(c => c.Index());
        }

        [Fact]
        public void RouteAssertionShouldWorkCorrectlyWithHeaderCompetingNonExistentVersion()
        {
            MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithLocation("api/header-competing")
                    .WithHeader("X-Custom-Version", "3.0"))
                .ToNonExistingRoute();
        }
    }
}
