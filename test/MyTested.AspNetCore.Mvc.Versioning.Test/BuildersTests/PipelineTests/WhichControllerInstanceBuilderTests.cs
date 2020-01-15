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
        public void RouteAssertionShouldWorkCorrectlyWithActionVersioningWhichDoesNotExist()
        {
            MyPipeline
                .Configuration()
                .ShouldMap("api/v3.0/versioning")
                .To<VersioningController>(c => c.SpecificVersion())
                .Which()
                .ShouldReturn()
                .Ok();
        }
    }
}
