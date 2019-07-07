namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.ActionResultsTests.ObjectTests
{
    using Microsoft.AspNetCore.Mvc;
    using Setups.Controllers;
    using Xunit;

    public class ObjectTestBuilderTests
    {
        [Fact]
        public void AndProvideTheActionResultShouldWorkCorrectlyObject()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.FullObjectResultAction())
                .ShouldReturn()
                .Object()
                .ShouldPassForThe<IActionResult>(actionResult =>
                {
                    Assert.NotNull(actionResult);
                    Assert.IsAssignableFrom<ObjectResult>(actionResult);
                });
        }
    }
}
