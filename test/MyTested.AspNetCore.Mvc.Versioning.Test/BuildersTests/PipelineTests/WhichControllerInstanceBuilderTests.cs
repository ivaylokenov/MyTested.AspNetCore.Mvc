namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.PipelineTests
{
    using Setups.Controllers;
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
                .ShouldMap("api/versioning?api-version=2.0")
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
                    .WithHeader("x-api-version", "1.0"))
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
                .ShouldMap("api/neutral-query?api-version=99.0")
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
                    .WithHeader("x-api-version", "99.0"))
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
                .ShouldMap("api/query-competing?api-version=1.0")
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
                .ShouldMap("api/query-competing?api-version=2.0")
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
                    .WithHeader("x-api-version", "1.0"))
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
                    .WithHeader("x-api-version", "2.0"))
                .To<HeaderCompetingV2Controller>(c => c.Index())
                .Which()
                .ShouldReturn()
                .Ok();
        }
    }
}
