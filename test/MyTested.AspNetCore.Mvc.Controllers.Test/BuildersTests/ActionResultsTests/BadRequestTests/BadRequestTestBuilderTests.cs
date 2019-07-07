namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.ActionResultsTests.BadRequestTests
{
    using Microsoft.AspNetCore.Mvc;
    using Setups.Controllers;
    using Xunit;

    public class BadRequestTestBuilderTests
    {
        [Fact]
        public void AndProvideTheActionResultShouldWorkCorrectlyBadRequest()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.FullHttpBadRequestAction())
                .ShouldReturn()
                .BadRequest()
                .AndAlso()
                .ShouldPassForThe<IActionResult>(actionResult =>
                {
                    Assert.NotNull(actionResult);
                    Assert.IsAssignableFrom<BadRequestObjectResult>(actionResult);
                });
        }
    }
}
