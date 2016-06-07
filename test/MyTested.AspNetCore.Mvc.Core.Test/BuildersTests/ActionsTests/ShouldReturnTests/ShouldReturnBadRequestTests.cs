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
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.BadRequestAction())
                .ShouldReturn()
                .BadRequest();
        }

        [Fact]
        public void ShouldReturnBadRequestShouldNotThrowExceptionWhenResultIsBadRequestErrorMessageResult()
        {
            MyMvc
                .Controller<MvcController>()
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
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.HttpNotFoundAction())
                        .ShouldReturn()
                        .BadRequest();
                },
                "When calling HttpNotFoundAction action in MvcController expected action result to be BadRequestResult, but instead received NotFoundResult.");
        }
    }
}
