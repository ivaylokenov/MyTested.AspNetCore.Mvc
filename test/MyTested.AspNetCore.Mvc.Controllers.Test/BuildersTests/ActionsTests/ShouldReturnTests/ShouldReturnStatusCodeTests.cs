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
    }
}
