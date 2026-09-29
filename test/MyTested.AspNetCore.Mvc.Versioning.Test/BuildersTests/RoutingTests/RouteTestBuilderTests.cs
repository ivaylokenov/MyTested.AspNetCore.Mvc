namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.RoutingTests
{
    using Asp.Versioning;
    using Exceptions;
    using Microsoft.Extensions.DependencyInjection;
    using Setups;
    using Setups.Controllers;
    using Setups.Routing;
    using Setups.Startups;
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

        [Fact]
        public void RouteAssertionShouldWorkCorrectlyWithDefaultQueryStringParameter()
        {
            MyRouting
                .Configuration()
                .ShouldMap("api/versioning?api-version=2.0")
                .To<QueryVersioningController>(c => c.Index());
        }

        [Fact]
        public void RouteAssertionShouldWorkCorrectlyWithDefaultQueryStringParameterWhichDoesNotExist()
        {
            MyRouting
                .Configuration()
                .ShouldMap("api/versioning?api-version=1.0")
                .ToNonExistingRoute();
        }

        [Fact]
        public void RouteAssertionShouldWorkCorrectlyWithDefaultQueryStringParameterAndCompetingV1()
        {
            MyRouting
                .Configuration()
                .ShouldMap("api/query-competing?api-version=1.0")
                .To<QueryCompetingV1Controller>(c => c.Index());
        }

        [Fact]
        public void RouteAssertionShouldWorkCorrectlyWithDefaultQueryStringParameterAndCompetingV2()
        {
            MyRouting
                .Configuration()
                .ShouldMap("api/query-competing?api-version=2.0")
                .To<QueryCompetingV2Controller>(c => c.Index());
        }

        [Fact]
        public void RouteAssertionShouldWorkCorrectlyWithQueryVersioningWithoutVersion()
        {
            MyRouting
                .Configuration()
                .ShouldMap("api/versioning")
                .ToNonExistingRoute();
        }

        [Fact]
        public void RouteAssertionShouldWorkCorrectlyWithQueryCompetingWithoutVersion()
        {
            MyRouting
                .Configuration()
                .ShouldMap("api/query-competing")
                .ToNonExistingRoute();
        }

        [Fact]
        public void RouteAssertionShouldWorkCorrectlyWithHeaderVersioningWithoutVersion()
        {
            MyRouting
                .Configuration()
                .ShouldMap("api/header-versioning")
                .ToNonExistingRoute();
        }

        [Fact]
        public void RouteAssertionShouldWorkCorrectlyWithMalformedQueryVersion()
        {
            MyRouting
                .Configuration()
                .ShouldMap("api/query-competing?v=abc")
                .ToNonExistingRoute();
        }

        [Fact]
        public void RouteAssertionShouldWorkCorrectlyWithMalformedHeaderVersion()
        {
            MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithLocation("api/header-competing")
                    .WithHeader("X-Custom-Version", "abc"))
                .ToNonExistingRoute();
        }

        [Fact]
        public void RouteAssertionShouldWorkCorrectlyWithMalformedUrlSegmentVersion()
        {
            MyRouting
                .Configuration()
                .ShouldMap("api/vabc/competing")
                .ToNonExistingRoute();
        }

        [Fact]
        public void RouteAssertionShouldWorkCorrectlyWithAmbiguousQueryVersions()
        {
            MyRouting
                .Configuration()
                .ShouldMap("api/query-competing?v=1.0&v=2.0")
                .ToNonExistingRoute();
        }

        [Fact]
        public void RouteAssertionShouldWorkCorrectlyWithConflictingQueryStringParameters()
        {
            MyRouting
                .Configuration()
                .ShouldMap("api/query-competing?api-version=1.0&v=2.0")
                .ToNonExistingRoute();
        }

        [Fact]
        public void RouteAssertionShouldWorkCorrectlyWithConflictingQueryAndHeaderVersions()
        {
            MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithLocation("api/query-competing?v=1.0")
                    .WithHeader("X-Custom-Version", "2.0"))
                .ToNonExistingRoute();
        }

        [Fact]
        public void RouteAssertionShouldWorkCorrectlyWithEqualQueryAndHeaderVersions()
        {
            MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithLocation("api/query-competing?v=2.0")
                    .WithHeader("X-Custom-Version", "2.0"))
                .To<QueryCompetingV2Controller>(c => c.Index());
        }

        [Fact]
        public void RouteAssertionShouldWorkCorrectlyWithCustomRouteParameterName()
        {
            MyRouting
                .Configuration()
                .ShouldMap("api/v1.0/custom-parameter")
                .To<CustomParameterVersioningController>(c => c.Index())
                .AndAlso()
                .ToRouteValue("ver", "1.0");
        }

        [Fact]
        public void RouteAssertionShouldWorkCorrectlyWithCustomRouteParameterNameWhichDoesNotExist()
        {
            MyRouting
                .Configuration()
                .ShouldMap("api/v2.0/custom-parameter")
                .ToNonExistingRoute();
        }

        [Fact]
        public void RouteAssertionShouldWorkCorrectlyWithDeprecatedVersion()
        {
            MyRouting
                .Configuration()
                .ShouldMap("api/deprecated-versioning?v=1.0")
                .To<DeprecatedVersioningController>(c => c.Index());
        }

        [Fact]
        public void RouteAssertionShouldWorkCorrectlyWithSupportedVersionNextToDeprecatedVersion()
        {
            MyRouting
                .Configuration()
                .ShouldMap("api/deprecated-versioning?v=2.0")
                .To<DeprecatedVersioningController>(c => c.Index());
        }

        [Fact]
        public void RouteAssertionShouldWorkCorrectlyWithDeprecatedVersioningWhichDoesNotExist()
        {
            MyRouting
                .Configuration()
                .ShouldMap("api/deprecated-versioning?v=3.0")
                .ToNonExistingRoute();
        }

        [Fact]
        public void RouteAssertionShouldWorkCorrectlyWithMediaTypeVersioningV1()
        {
            MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithLocation("api/query-competing")
                    .WithHeader("Accept", "application/json;v=1.0"))
                .To<QueryCompetingV1Controller>(c => c.Index());
        }

        [Fact]
        public void RouteAssertionShouldWorkCorrectlyWithMediaTypeVersioningV2()
        {
            MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithLocation("api/query-competing")
                    .WithHeader("Accept", "application/json;v=2.0"))
                .To<QueryCompetingV2Controller>(c => c.Index());
        }

        [Fact]
        public void RouteAssertionShouldWorkCorrectlyWithMediaTypeVersioningWhichDoesNotExist()
        {
            MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithLocation("api/query-competing")
                    .WithHeader("Accept", "application/json;v=3.0"))
                .ToNonExistingRoute();
        }

        [Fact]
        public void RouteAssertionShouldWorkCorrectlyWithImplicitlyMappedQueryVersion()
        {
            MyRouting
                .Configuration()
                .ShouldMap("api/mapped-versioning?v=1.0")
                .To<MappedVersioningController>(c => c.Index());
        }

        [Fact]
        public void RouteAssertionShouldWorkCorrectlyWithExplicitlyMappedQueryVersion()
        {
            MyRouting
                .Configuration()
                .ShouldMap("api/mapped-versioning?v=2.0")
                .To<MappedVersioningController>(c => c.SpecificVersion());
        }

        [Fact]
        public void RouteAssertionShouldWorkCorrectlyWithImplicitlyMappedHeaderVersion()
        {
            MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithLocation("api/mapped-versioning")
                    .WithHeader("X-Custom-Version", "1.0"))
                .To<MappedVersioningController>(c => c.Index());
        }

        [Fact]
        public void RouteAssertionShouldWorkCorrectlyWithExplicitlyMappedHeaderVersion()
        {
            MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithLocation("api/mapped-versioning")
                    .WithHeader("X-Custom-Version", "2.0"))
                .To<MappedVersioningController>(c => c.SpecificVersion());
        }

        [Fact]
        public void RouteAssertionShouldWorkCorrectlyWithMappedVersioningWhichDoesNotExist()
        {
            MyRouting
                .Configuration()
                .ShouldMap("api/mapped-versioning?v=3.0")
                .ToNonExistingRoute();
        }

        [Fact]
        public void RouteAssertionShouldWorkCorrectlyWithUnversionedController()
        {
            MyRouting
                .Configuration()
                .ShouldMap("/Home/Contact/1")
                .To<HomeController>(c => c.Contact(1));
        }

        [Fact]
        public void RouteAssertionShouldWorkCorrectlyWithUnversionedControllerAndRequestedVersion()
        {
            MyRouting
                .Configuration()
                .ShouldMap("/Home/Contact/1?v=2.0")
                .To<HomeController>(c => c.Contact(1));
        }

        [Fact]
        public void RouteAssertionShouldThrowExceptionWithIncorrectVersionedController()
        {
            Test.AssertException<RouteAssertionException>(
                () =>
                {
                    MyRouting
                        .Configuration()
                        .ShouldMap("api/query-competing?v=1.0")
                        .To<QueryCompetingV2Controller>(c => c.Index());
                },
                "Expected route '/api/query-competing' to match Index action in QueryCompetingV2Controller but instead matched QueryCompetingV1Controller.");
        }

        [Fact]
        public void RouteAssertionShouldThrowExceptionWithIncorrectMappedAction()
        {
            Test.AssertException<RouteAssertionException>(
                () =>
                {
                    MyRouting
                        .Configuration()
                        .ShouldMap("api/mapped-versioning?v=2.0")
                        .To<MappedVersioningController>(c => c.Index());
                },
                "Expected route '/api/mapped-versioning' to match Index action in MappedVersioningController but instead matched SpecificVersion action.");
        }

        [Fact]
        public void RouteAssertionShouldThrowExceptionWithUnspecifiedVersion()
        {
            Test.AssertException<RouteAssertionException>(
                () =>
                {
                    MyRouting
                        .Configuration()
                        .ShouldMap("api/query-competing")
                        .To<QueryCompetingV1Controller>(c => c.Index());
                },
                "Expected route '/api/query-competing' to match Index action in QueryCompetingV1Controller but action could not be matched.");
        }

        [Fact]
        public void RouteAssertionShouldWorkCorrectlyWithAssumedDefaultVersionAndQueryCompeting()
        {
            MyApplication.StartsFrom<AssumeDefaultVersionStartup>();

            MyRouting
                .Configuration()
                .ShouldMap("api/query-competing")
                .To<QueryCompetingV2Controller>(c => c.Index());

            MyApplication.StartsFrom<TestStartup>();
        }

        [Fact]
        public void RouteAssertionShouldWorkCorrectlyWithAssumedDefaultVersionAndHeaderCompeting()
        {
            MyApplication.StartsFrom<AssumeDefaultVersionStartup>();

            MyRouting
                .Configuration()
                .ShouldMap("api/header-competing")
                .To<HeaderCompetingV2Controller>(c => c.Index());

            MyApplication.StartsFrom<TestStartup>();
        }

        [Fact]
        public void RouteAssertionShouldWorkCorrectlyWithAssumedDefaultVersionAndExplicitlyMappedAction()
        {
            MyApplication.StartsFrom<AssumeDefaultVersionStartup>();

            MyRouting
                .Configuration()
                .ShouldMap("api/mapped-versioning")
                .To<MappedVersioningController>(c => c.SpecificVersion());

            MyApplication.StartsFrom<TestStartup>();
        }

        [Fact]
        public void RouteAssertionShouldWorkCorrectlyWithAssumedDefaultVersionAndRequestedVersion()
        {
            MyApplication.StartsFrom<AssumeDefaultVersionStartup>();

            MyRouting
                .Configuration()
                .ShouldMap("api/query-competing?v=1.0")
                .To<QueryCompetingV1Controller>(c => c.Index());

            MyApplication.StartsFrom<TestStartup>();
        }

        [Fact]
        public void RouteAssertionShouldWorkCorrectlyWithAssumedDefaultVersionWhichDoesNotExist()
        {
            MyApplication.StartsFrom<AssumeDefaultVersionStartup>();

            MyRouting
                .Configuration()
                .ShouldMap("api/header-versioning")
                .ToNonExistingRoute();

            MyApplication.StartsFrom<TestStartup>();
        }

        [Fact]
        public void RouteAssertionShouldWorkCorrectlyWithAssumedDefaultVersionAndMalformedVersion()
        {
            MyApplication.StartsFrom<AssumeDefaultVersionStartup>();

            MyRouting
                .Configuration()
                .ShouldMap("api/query-competing?v=abc")
                .ToNonExistingRoute();

            MyApplication.StartsFrom<TestStartup>();
        }

        [Fact]
        public void RouteAssertionShouldWorkCorrectlyWithAssumedDefaultVersionAndAmbiguousVersions()
        {
            MyApplication.StartsFrom<AssumeDefaultVersionStartup>();

            MyRouting
                .Configuration()
                .ShouldMap("api/query-competing?v=1.0&v=2.0")
                .ToNonExistingRoute();

            MyApplication.StartsFrom<TestStartup>();
        }

        [Fact]
        public void RouteAssertionShouldWorkCorrectlyWithCurrentImplementationApiVersionSelector()
        {
            MyApplication
                .StartsFrom<AssumeDefaultVersionStartup>()
                .WithServices(services => services
                    .Configure<ApiVersioningOptions>(options =>
                    {
                        options.ApiVersionSelector = new CurrentImplementationApiVersionSelector(options);
                    }));

            MyRouting
                .Configuration()
                .ShouldMap("api/header-versioning")
                .To<HeaderVersioningController>(c => c.Index());

            MyApplication.StartsFrom<TestStartup>();
        }

        [Fact]
        public void RouteAssertionShouldWorkCorrectlyWithLowestImplementedApiVersionSelector()
        {
            MyApplication
                .StartsFrom<AssumeDefaultVersionStartup>()
                .WithServices(services => services
                    .Configure<ApiVersioningOptions>(options =>
                    {
                        options.ApiVersionSelector = new LowestImplementedApiVersionSelector(options);
                    }));

            MyRouting
                .Configuration()
                .ShouldMap("api/query-competing")
                .To<QueryCompetingV1Controller>(c => c.Index());

            MyApplication.StartsFrom<TestStartup>();
        }
    }
}
