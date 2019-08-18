namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.ActionResultsTests.CreatedTests
{
    using Microsoft.AspNetCore.Mvc;
    using Setups.Controllers;
    using Xunit;

    public class CreatedTestBuilderTests
    {
        [Fact]
        public void AndProvideTheActionResultShouldWorkCorrectly()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.CreatedAction())
                .ShouldReturn()
                .Created()
                .ShouldPassForThe<ActionResult>(actionResult =>
                {
                    Assert.NotNull(actionResult);
                    Assert.IsAssignableFrom<CreatedResult>(actionResult);
                });
        }
    }
}
