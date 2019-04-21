namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.ActionResultsTests.FileTests
{
    using Microsoft.AspNetCore.Mvc;
    using Setups.Controllers;
    using Xunit;

    public class PhysicalFileTestBuilderTests
    {
        [Fact]
        public void AndProvideTheActionResultShouldWorkCorrectly()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.PhysicalFileResult())
                .ShouldReturn()
                .File()
                .ShouldPassForThe<IActionResult>(actionResult =>
                {
                    Assert.NotNull(actionResult);
                    Assert.IsAssignableFrom<PhysicalFileResult>(actionResult);
                });
        }
    }
}
