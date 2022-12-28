namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.ControllersTests
{
    using Setups.Controllers;
    using Xunit;

    public class ControllerTestBuilderTests
    {
        [Fact]
        public void ControllerAssertionShouldWorkCorrectlyWithRazorRuntimeCompilation()
        {
            MyController<MvcController>
                .Calling(c => c.DefaultView())
                .ShouldReturn()
                .View();
        }
    }
}
