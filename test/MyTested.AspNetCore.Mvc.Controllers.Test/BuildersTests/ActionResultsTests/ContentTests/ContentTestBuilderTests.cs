namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.ActionResultsTests.ContentTests
{
    using Microsoft.AspNetCore.Mvc;
    using Setups.Controllers;
    using Xunit;

    public class ContentTestBuilderTests
    {
        [Fact]
        public void AndProvideTheActionResultShouldWorkCorrectly()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.ContentAction())
                .ShouldReturn()
                .Content()
                .AndAlso()
                .ShouldPassForThe<IActionResult>(actionResult =>
                {
                    Assert.NotNull(actionResult);
                    Assert.IsAssignableFrom<ContentResult>(actionResult);
                });
        }
    }
}
