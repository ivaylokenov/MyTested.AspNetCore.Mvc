namespace MyTested.Mvc.Tests.BuildersTests.ActionsTests.ShouldReturnTests
{
    using Exceptions;
    using Setups;
    using Setups.Controllers;
    using Xunit;

    public class ShouldReturnNoContentTests
    {
        [Fact]
        public void ShouldReturnNoContentShouldNotThrowExceptionWhenActionReturnsNoContentResult()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.NoContentResultAction())
                .ShouldReturn()
                .NoContent();
        }

        [Fact]
        public void ShouldReturnNoContentShouldThrowExceptionWhenActionDoesNotReturnNoContentResult()
        {
            Test.AssertException<ActionResultAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.BadRequestAction())
                        .ShouldReturn()
                        .NoContent();
                },
                "When calling BadRequestAction action in MvcController expected action result to be NoContentResult, but instead received BadRequestResult.");
        }
    }
}
