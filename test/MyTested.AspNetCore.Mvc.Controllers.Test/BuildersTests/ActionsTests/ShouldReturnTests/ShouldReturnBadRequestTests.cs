namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.ActionsTests.ShouldReturnTests
{
    using Exceptions;
    using Setups;
    using Setups.Controllers;
    using Xunit;
    
    public class ShouldReturnBadRequestTests
    {
        [Fact]
        public void ShouldReturnBadRequestShouldNotThrowExceptionWhenResultIsBadRequest()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.BadRequestAction())
                .ShouldReturn()
                .BadRequest();
        }

        [Fact]
        public void ShouldReturnBadRequestShouldNotThrowExceptionWhenResultIsBadRequestErrorMessageResult()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.BadRequestWithErrorAction())
                .ShouldReturn()
                .BadRequest();
        }

        [Fact]
        public void ShouldReturnBadRequestShouldThrowExceptionWhenActionDoesNotReturnBadRequest()
        {
            Test.AssertException<InvocationResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.NotFoundAction())
                        .ShouldReturn()
                        .BadRequest();
                },
                "When calling NotFoundAction action in MvcController expected result to be BadRequestResult, but instead received NotFoundResult.");
        }
    }
}
