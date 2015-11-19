namespace MyTested.Mvc.Tests.BuildersTests.ActionsTests.ShouldReturn
{
    using Exceptions;
    using Setups.Controllers;
    
    public class ShouldReturnUnauthorizedTests
    {
        // TODO: add when unathorized is implemented
        //[Test]
        //public void ShouldReturnUnauthorizedShouldNotThrowExceptionWhenActionReturnsUnauthorizedResult()
        //{
        //    MyWebApi
        //        .Controller<WebApiController>()
        //        .Calling(c => c.UnauthorizedAction())
        //        .ShouldReturn()
        //        .Unauthorized();
        //}

        //[Test]
        //[ExpectedException(
        //    typeof(HttpActionResultAssertionException),
        //    ExpectedMessage = "When calling BadRequestAction action in WebApiController expected action result to be UnauthorizedResult, but instead received BadRequestResult.")]
        //public void ShouldReturnUnauthorizedShouldThrowExceptionWhenActionDoesNotReturnUnauthorizedResult()
        //{
        //    MyWebApi
        //        .Controller<WebApiController>()
        //        .Calling(c => c.BadRequestAction())
        //        .ShouldReturn()
        //        .Unauthorized();
        //}
    }
}
