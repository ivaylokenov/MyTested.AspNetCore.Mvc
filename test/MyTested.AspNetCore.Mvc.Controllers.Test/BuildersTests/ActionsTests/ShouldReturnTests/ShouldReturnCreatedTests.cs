namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.ActionsTests.ShouldReturnTests
{
    using Exceptions;
    using Setups;
    using Setups.Controllers;
    using Xunit;

    public class ShouldReturnCreatedTests
    {
        [Fact]
        public void ShouldReturnCreatedShouldNotThrowExceptionWithCreatedResult()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.CreatedAction())
                .ShouldReturn()
                .Created();
        }

        [Fact]
        public void ShouldReturnCreatedShouldNotThrowExceptionWithCreatedAtActionResult()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.CreatedAtActionResult())
                .ShouldReturn()
                .Created();
        }

        [Fact]
        public void ShouldReturnCreatedShouldNotThrowExceptionWithCreatedAtRouteResult()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.CreatedAtRouteAction())
                .ShouldReturn()
                .Created();
        }

        [Fact]
        public void ShouldReturnCreatedShouldThrowExceptionWithBadRequestResult()
        {
            Test.AssertException<ActionResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.BadRequestAction())
                        .ShouldReturn()
                        .Created();
                },
                "When calling BadRequestAction action in MvcController expected action result to be CreatedResult, but instead received BadRequestResult.");
        }
    }
}
