namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.ActionsTests.ShouldReturnTests
{
    using Exceptions;
    using Setups;
    using Setups.Controllers;
    using Xunit;

    public class ShouldReturnUnauthorizedTests
    {
        [Fact]
        public void ShouldReturnUnauthorizedShouldNotThrowExceptionWhenActionReturnsUnauthorizedResult()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.UnauthorizedAction())
                .ShouldReturn()
                .Unauthorized();
        }

        [Fact]
        public void ShouldReturnUnauthorizedShouldThrowExceptionWhenActionDoesNotReturnUnauthorizedResult()
        {
            Test.AssertException<InvocationResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.BadRequestAction())
                        .ShouldReturn()
                        .Unauthorized();
                }, 
                "When calling BadRequestAction action in MvcController expected result to be UnauthorizedResult, but instead received BadRequestResult.");
        }
    }
}
