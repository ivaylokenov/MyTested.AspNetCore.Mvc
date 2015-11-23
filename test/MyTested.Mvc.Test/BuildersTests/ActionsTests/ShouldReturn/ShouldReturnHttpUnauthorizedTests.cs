namespace MyTested.Mvc.Tests.BuildersTests.ActionsTests.ShouldReturn
{
    using Exceptions;
    using Setups.Controllers;
    
    public class ShouldReturnUnauthorizedTests
    {
        // TODO: add when unathorized is implemented
        //[Fact]
        //public void ShouldReturnUnauthorizedShouldNotThrowExceptionWhenActionReturnsUnauthorizedResult()
        //{
        //    MyMvc
        //        .Controller<MvcController>()
        //        .Calling(c => c.UnauthorizedAction())
        //        .ShouldReturn()
        //        .Unauthorized();
        //}

        //[Fact]
        //[ExpectedException(
        //    typeof(HttpActionResultAssertionException),
        //    ExpectedMessage = "When calling BadRequestAction action in MvcController expected action result to be UnauthorizedResult, but instead received BadRequestResult.")]
        //public void ShouldReturnUnauthorizedShouldThrowExceptionWhenActionDoesNotReturnUnauthorizedResult()
        //{
        //    MyMvc
        //        .Controller<MvcController>()
        //        .Calling(c => c.BadRequestAction())
        //        .ShouldReturn()
        //        .Unauthorized();
        //}
    }
}
