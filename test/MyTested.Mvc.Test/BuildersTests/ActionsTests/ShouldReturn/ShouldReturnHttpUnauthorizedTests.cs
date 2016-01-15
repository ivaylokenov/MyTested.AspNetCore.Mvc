namespace MyTested.Mvc.Tests.BuildersTests.ActionsTests.ShouldReturn
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
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.UnauthorizedAction())
                .ShouldReturn()
                .HttpUnauthorized();
        }

        [Fact]
        public void ShouldReturnUnauthorizedShouldThrowExceptionWhenActionDoesNotReturnUnauthorizedResult()
        {
            Test.AssertException<ActionResultAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.BadRequestAction())
                        .ShouldReturn()
                        .HttpUnauthorized();
                }, 
                "When calling BadRequestAction action in MvcController expected action result to be HttpUnauthorizedResult, but instead received BadRequestResult.");
        }
    }
}
