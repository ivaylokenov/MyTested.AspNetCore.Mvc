namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.ActionResultsTests.RedirectTests
{
    using Microsoft.AspNetCore.Mvc;
    using Setups.Controllers;
    using Xunit;

    public class RedirectTestBuilderTests
    {
        [Fact]
        public void AndProvideTheActionResultShouldWorkCorrectlyRedirect()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.RedirectToActionResult())
                .ShouldReturn()
                .Redirect()
                .ShouldPassForThe<IActionResult>(actionResult =>
                {
                    Assert.NotNull(actionResult);
                    Assert.IsAssignableFrom<RedirectToActionResult>(actionResult);
                });
        }
    }
}
