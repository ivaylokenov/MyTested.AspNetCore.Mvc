namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.ActionResultsTests.LocalRedirectTests
{
    using Microsoft.AspNetCore.Mvc;
    using Setups.Controllers;
    using Xunit;

    public class LocalRedirectTestBuilderTests
    {
        [Fact]
        public void AndProvideTheActionResultShouldWorkCorrectlyLocalRedirect()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.LocalRedirectAction())
                .ShouldReturn()
                .LocalRedirect()
                .AndAlso()
                .ShouldPassForThe<ActionResult>(actionResult =>
                {
                    Assert.NotNull(actionResult);
                    Assert.IsAssignableFrom<LocalRedirectResult>(actionResult);
                });
        }
    }
}
