namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.ActionResultsTests.NotFoundTests
{
    using Microsoft.AspNetCore.Mvc;
    using Setups.Controllers;
    using Xunit;

    public class NotFoundTestBuilderTests
    {
        [Fact]
        public void AndProvideTheActionResultShouldWorkCorrectlyNotFound()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.FullHttpNotFoundAction())
                .ShouldReturn()
                .NotFound()
                .ShouldPassForThe<IActionResult>(actionResult =>
                {
                    Assert.NotNull(actionResult);
                    Assert.IsAssignableFrom<NotFoundObjectResult>(actionResult);
                });
        }
    }
}
