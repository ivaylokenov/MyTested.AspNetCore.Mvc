namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.ActionsTests.ShouldReturnTests
{
    using Exceptions;
    using Setups;
    using Setups.Controllers;
    using Xunit;

    public class ShouldReturnEmptyTests
    {
        [Fact]
        public void ShouldReturnEmptyShouldNotThrowExceptionWhenActionReturnsEmptyResult()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.EmptyResultAction())
                .ShouldReturn()
                .Empty();
        }

        [Fact]
        public void ShouldReturnEmptyShouldThrowExceptionWhenActionDoesNotReturnEmptyResult()
        {
            Test.AssertException<InvocationResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.BadRequestAction())
                        .ShouldReturn()
                        .Empty();
                },
                "When calling BadRequestAction action in MvcController expected result to be EmptyResult, but instead received BadRequestResult.");
        }
    }
}
