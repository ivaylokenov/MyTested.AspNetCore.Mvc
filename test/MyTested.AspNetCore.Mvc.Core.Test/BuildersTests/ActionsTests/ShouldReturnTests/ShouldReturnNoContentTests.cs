namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.ActionsTests.ShouldReturnTests
{
    using Exceptions;
    using Setups;
    using Setups.Controllers;
    using Xunit;

    public class ShouldReturnNoContentTests
    {
        [Fact]
        public void ShouldReturnNoContentShouldNotThrowExceptionWhenActionReturnsNoContentResult()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.NoContentResultAction())
                .ShouldReturn()
                .NoContent();
        }

        [Fact]
        public void ShouldReturnNoContentShouldThrowExceptionWhenActionDoesNotReturnNoContentResult()
        {
            Test.AssertException<ActionResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.BadRequestAction())
                        .ShouldReturn()
                        .NoContent();
                },
                "When calling BadRequestAction action in MvcController expected action result to be NoContentResult, but instead received BadRequestResult.");
        }
    }
}
