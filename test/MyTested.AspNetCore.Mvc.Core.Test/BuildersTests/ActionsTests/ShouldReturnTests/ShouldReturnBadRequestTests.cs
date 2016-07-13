namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.ActionsTests.ShouldReturnTests
{
    using Exceptions;
    using Setups;
    using Setups.Controllers;
    using Xunit;
    
    public class ShouldReturnBadRequestTests
    {
        [Fact]
        public void ShouldReturnBadRequestShouldNotThrowExceptionWhenResultIsHttpBadRequest()
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
        public void ShouldReturnNotFoundShouldThrowExceptionWhenActionDoesNotReturnNotFound()
        {
            Test.AssertException<ActionResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.HttpNotFoundAction())
                        .ShouldReturn()
                        .BadRequest();
                },
                "When calling HttpNotFoundAction action in MvcController expected action result to be BadRequestResult, but instead received NotFoundResult.");
        }
    }
}
