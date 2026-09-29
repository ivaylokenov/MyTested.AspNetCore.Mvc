namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.PipelineTests
{
    using Asp.Versioning;
    using Exceptions;
    using Setups;
    using Setups.Controllers;
    using Setups.Startups;
    using Xunit;

    public class WhichControllerInstanceBuilderTests
    {
        [Fact]
        public void PipelineAssertionShouldWorkCorrectlyWithVersioning()
        {
            MyPipeline
                .Configuration()
                .ShouldMap("api/v2.0/versioning")
                .To<VersioningController>(c => c.Index())
                .Which()
                .ShouldReturn()
                .Ok();
        }

        [Fact]
        public void PipelineAssertionShouldWorkCorrectlyWithQueryVersioning()
        {
            MyPipeline
                .Configuration()
                .ShouldMap("api/versioning?v=2.0")
                .To<QueryVersioningController>(c => c.Index())
                .Which()
                .ShouldReturn()
                .Ok();
        }

        [Fact]
        public void PipelineAssertionShouldWorkCorrectlyWithActionVersioning()
        {
            MyPipeline
                .Configuration()
                .ShouldMap("api/v3.0/versioning")
                .To<VersioningController>(c => c.SpecificVersion())
                .Which()
                .ShouldReturn()
                .Ok();
        }

        [Fact]
        public void PipelineAssertionShouldWorkCorrectlyWithHeaderVersioning()
        {
            MyPipeline
                .Configuration()
                .ShouldMap(request => request
                    .WithLocation("api/header-versioning")
                    .WithHeader("X-Custom-Version", "1.0"))
                .To<HeaderVersioningController>(c => c.Index())
                .Which()
                .ShouldReturn()
                .Ok();
        }

        [Fact]
        public void PipelineAssertionShouldWorkCorrectlyWithVersionNeutralQueryController()
        {
            MyPipeline
                .Configuration()
                .ShouldMap("api/neutral-query?v=99.0")
                .To<NeutralQueryController>(c => c.Index())
                .Which()
                .ShouldReturn()
                .Ok();
        }

        [Fact]
        public void PipelineAssertionShouldWorkCorrectlyWithVersionNeutralHeaderController()
        {
            MyPipeline
                .Configuration()
                .ShouldMap(request => request
                    .WithLocation("api/neutral-header")
                    .WithHeader("X-Custom-Version", "99.0"))
                .To<NeutralHeaderController>(c => c.Index())
                .Which()
                .ShouldReturn()
                .Ok();
        }

        [Fact]
        public void PipelineAssertionShouldWorkCorrectlyWithUrlSegmentCompetingV1()
        {
            MyPipeline
                .Configuration()
                .ShouldMap("api/v1.0/competing")
                .To<UrlCompetingV1Controller>(c => c.Index())
                .Which()
                .ShouldReturn()
                .Ok();
        }

        [Fact]
        public void PipelineAssertionShouldWorkCorrectlyWithUrlSegmentCompetingV2()
        {
            MyPipeline
                .Configuration()
                .ShouldMap("api/v2.0/competing")
                .To<UrlCompetingV2Controller>(c => c.Index())
                .Which()
                .ShouldReturn()
                .Ok();
        }

        [Fact]
        public void PipelineAssertionShouldWorkCorrectlyWithQueryCompetingV1()
        {
            MyPipeline
                .Configuration()
                .ShouldMap("api/query-competing?v=1.0")
                .To<QueryCompetingV1Controller>(c => c.Index())
                .Which()
                .ShouldReturn()
                .Ok();
        }

        [Fact]
        public void PipelineAssertionShouldWorkCorrectlyWithQueryCompetingV2()
        {
            MyPipeline
                .Configuration()
                .ShouldMap("api/query-competing?v=2.0")
                .To<QueryCompetingV2Controller>(c => c.Index())
                .Which()
                .ShouldReturn()
                .Ok();
        }

        [Fact]
        public void PipelineAssertionShouldWorkCorrectlyWithHeaderCompetingV1()
        {
            MyPipeline
                .Configuration()
                .ShouldMap(request => request
                    .WithLocation("api/header-competing")
                    .WithHeader("X-Custom-Version", "1.0"))
                .To<HeaderCompetingV1Controller>(c => c.Index())
                .Which()
                .ShouldReturn()
                .Ok();
        }

        [Fact]
        public void PipelineAssertionShouldWorkCorrectlyWithHeaderCompetingV2()
        {
            MyPipeline
                .Configuration()
                .ShouldMap(request => request
                    .WithLocation("api/header-competing")
                    .WithHeader("X-Custom-Version", "2.0"))
                .To<HeaderCompetingV2Controller>(c => c.Index())
                .Which()
                .ShouldReturn()
                .Ok();
        }

        [Fact]
        public void PipelineAssertionShouldWorkCorrectlyWithDefaultQueryStringParameter()
        {
            MyPipeline
                .Configuration()
                .ShouldMap("api/versioning?api-version=2.0")
                .To<QueryVersioningController>(c => c.Index())
                .Which()
                .ShouldReturn()
                .Ok();
        }

        [Fact]
        public void PipelineAssertionShouldWorkCorrectlyWithExplicitlyMappedQueryVersion()
        {
            MyPipeline
                .Configuration()
                .ShouldMap("api/mapped-versioning?v=2.0")
                .To<MappedVersioningController>(c => c.SpecificVersion())
                .Which()
                .ShouldReturn()
                .Ok();
        }

        [Fact]
        public void PipelineAssertionShouldWorkCorrectlyWithExplicitlyMappedHeaderVersion()
        {
            MyPipeline
                .Configuration()
                .ShouldMap(request => request
                    .WithLocation("api/mapped-versioning")
                    .WithHeader("X-Custom-Version", "2.0"))
                .To<MappedVersioningController>(c => c.SpecificVersion())
                .Which()
                .ShouldReturn()
                .Ok();
        }

        [Fact]
        public void PipelineAssertionShouldWorkCorrectlyWithCustomRouteParameterName()
        {
            MyPipeline
                .Configuration()
                .ShouldMap("api/v1.0/custom-parameter")
                .To<CustomParameterVersioningController>(c => c.Index())
                .Which()
                .ShouldReturn()
                .Ok();
        }

        [Fact]
        public void PipelineAssertionShouldWorkCorrectlyWithDeprecatedVersion()
        {
            MyPipeline
                .Configuration()
                .ShouldMap("api/deprecated-versioning?v=1.0")
                .To<DeprecatedVersioningController>(c => c.Index())
                .Which()
                .ShouldReturn()
                .Ok();
        }

        [Fact]
        public void PipelineAssertionShouldReturnRequestedApiVersionFromDefaultQueryStringParameter()
        {
            MyPipeline
                .Configuration()
                .ShouldMap("api/requested-version?api-version=2.0")
                .To<RequestedVersionController>(c => c.Index())
                .Which()
                .ShouldReturn()
                .Ok(ok => ok
                    .Passing(result => Assert.Equal(new ApiVersion(2, 0), result.Value)));
        }

        [Fact]
        public void PipelineAssertionShouldReturnRequestedApiVersionFromMediaType()
        {
            MyPipeline
                .Configuration()
                .ShouldMap(request => request
                    .WithLocation("api/requested-version")
                    .WithHeader("Accept", "application/json;v=1.0"))
                .To<RequestedVersionController>(c => c.Index())
                .Which()
                .ShouldReturn()
                .Ok(ok => ok
                    .Passing(result => Assert.Equal(new ApiVersion(1, 0), result.Value)));
        }

        [Fact]
        public void PipelineAssertionShouldReturnAssumedDefaultApiVersion()
        {
            MyApplication.StartsFrom<AssumeDefaultVersionStartup>();

            MyPipeline
                .Configuration()
                .ShouldMap("api/requested-version")
                .To<RequestedVersionController>(c => c.Index())
                .Which()
                .ShouldReturn()
                .Ok(ok => ok
                    .Passing(result => Assert.Equal(new ApiVersion(2, 0), result.Value)));

            MyApplication.StartsFrom<TestStartup>();
        }

        [Fact]
        public void PipelineAssertionShouldThrowExceptionWithUnspecifiedVersion()
        {
            Test.AssertException<RouteAssertionException>(
                () =>
                {
                    MyPipeline
                        .Configuration()
                        .ShouldMap("api/requested-version")
                        .To<RequestedVersionController>(c => c.Index());
                },
                "Expected route '/api/requested-version' to match Index action in RequestedVersionController but action could not be matched.");
        }
    }
}
