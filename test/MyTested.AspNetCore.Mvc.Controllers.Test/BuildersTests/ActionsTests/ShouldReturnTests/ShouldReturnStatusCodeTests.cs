namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.ActionsTests.ShouldReturnTests
{
    using Exceptions;
    using Setups;
    using Setups.Controllers;
    using Xunit;

    public class ShouldReturnStatusCodeTests
    {
        [Fact]
        public void ShouldReturnHttpStatusCodeShouldNotThrowExceptionWhenActionReturnsHttpStatusCodeResult()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.StatusCodeAction())
                .ShouldReturn()
                .StatusCode();
        }

        [Fact]
        public void ShouldReturnHttpStatusCodeShouldThrowExceptionWhenActionDoesNotReturnHttpStatusCodeResult()
        {
            Test.AssertException<InvocationResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.BadRequestAction())
                        .ShouldReturn()
                        .StatusCode();
                },
                "When calling BadRequestAction action in MvcController expected result to be StatusCodeResult, but instead received BadRequestResult.");
        }
        
        [Fact]
        public void ShouldReturnHttpStatusCodeShouldNotThrowExceptionWithCorrectStatusCode()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.StatusCodeAction())
                .ShouldReturn()
                .StatusCode(500);
        }

        [Fact]
        public void ShouldReturnHttpStatusCodeShouldThrowExceptionWithIncorrectStatusCode()
        {
            Test.AssertException<StatusCodeResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.StatusCodeAction())
                        .ShouldReturn()
                        .StatusCode(200);
                },
                "When calling StatusCodeAction action in MvcController expected status code result to have 200 (OK) status code, but instead received 500 (InternalServerError).");
        }
    }
}
