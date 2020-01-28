namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.ControllersTests
{
    using Setups.Controllers;
    using Xunit;

    public class ControllerTestBuilderTests
    {
        [Fact]
        public void ControllerAssertionShouldWorkCorrectlyWithVersioning()
        {
            MyController<VersioningController>
                .Calling(c => c.Index())
                .ShouldReturn()
                .Ok();
        }
    }
}
