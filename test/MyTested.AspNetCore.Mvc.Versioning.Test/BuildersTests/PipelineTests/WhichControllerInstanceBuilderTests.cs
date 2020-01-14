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
    }
}
