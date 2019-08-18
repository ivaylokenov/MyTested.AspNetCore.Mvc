namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.ActionsTests.ShouldReturnTests
{
    using Exceptions;
    using Setups;
    using Setups.Controllers;
    using Xunit;

    public class ShouldReturnViewComponentTests
    {
        [Fact]
        public void ShouldReturnViewComponentShouldNotThrowExceptionWithViewComponent()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.ViewComponentResultByName())
                .ShouldReturn()
                .ViewComponent();
        }

        [Fact]
        public void ShouldReturnViewComponentShouldThrowExceptionWithIncorrectViewComponent()
        {
            Test.AssertException<InvocationResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                       .Instance()
                       .Calling(c => c.BadRequestAction())
                       .ShouldReturn()
                       .ViewComponent();
                },
                "When calling BadRequestAction action in MvcController expected result to be ViewComponentResult, but instead received BadRequestResult.");
        }
    }
}
