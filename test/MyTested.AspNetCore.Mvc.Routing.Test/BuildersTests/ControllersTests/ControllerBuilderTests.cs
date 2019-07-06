namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.ControllersTests
{
    using Setups.Controllers;
    using Xunit;

    public class ControllerBuilderTests
    {
        [Fact]
        public void UsingUrlHelperInsideControllerShouldWorkCorrectly()
        {
            MyController<MvcController>
                .Instance()
                .WithRouteData()
                .Calling(c => c.UrlAction())
                .ShouldReturn()
                .Ok(ok => ok
                    .WithModel("/api/test"));
        }
    }
}
