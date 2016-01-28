namespace MyTested.Mvc.Tests.BuildersTests.ActionsTests.ShouldReturnTests
{
    using Exceptions;
    using Setups;
    using Setups.Controllers;
    using Xunit;

    public class ShouldReturnHttpStatusCodeTests
    {
        [Fact]
        public void ShouldReturnHttpStatusCodeShouldNotThrowExceptionWhenActionReturnsHttpStatusCodeResult()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.StatusCodeAction())
                .ShouldReturn()
                .HttpStatusCode();
        }

        [Fact]
        public void ShouldReturnHttpStatusCodeShouldThrowExceptionWhenActionDoesNotReturnHttpStatusCodeResult()
        {
            Test.AssertException<ActionResultAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.BadRequestAction())
                        .ShouldReturn()
                        .HttpStatusCode();
                },
                "When calling BadRequestAction action in MvcController expected action result to be HttpStatusCodeResult, but instead received BadRequestResult.");
        }
        
        [Fact]
        public void ShouldReturnHttpStatusCodeShouldNotThrowExceptionWithCorrectStatusCode()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.StatusCodeAction())
                .ShouldReturn()
                .HttpStatusCode(500);
        }

        [Fact]
        public void ShouldReturnHttpStatusCodeShouldThrowExceptionWithIncorrectStatusCode()
        {
            Test.AssertException<HttpStatusCodeResultAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.StatusCodeAction())
                        .ShouldReturn()
                        .HttpStatusCode(200);
                },
                "When calling StatusCodeAction action in MvcController expected HTTP status code result to have 200 (OK) status code, but instead received 500 (InternalServerError).");
        }
    }
}
