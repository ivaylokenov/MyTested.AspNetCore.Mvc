namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.ActionsTests.ShouldReturnTests
{
    using Exceptions;
    using Setups;
    using Setups.Controllers;
    using Xunit;

    public class ShouldReturnUnsupportedMediaTypeResult
    {
        [Fact]
        public void ShouldReturnUnsupportedMediaTypeShouldNotThrowExceptionWhenActionReturnsUnsupportedMediaTypeResult()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.UnsupportedMediaTypeResultAction())
                .ShouldReturn()
                .UnsupportedMediaType();
        }

        [Fact]
        public void ShouldReturnUnsupportedMediaTypeShouldThrowExceptionWhenActionDoesNotReturnUnsupportedMediaTypeResult()
        {
            Test.AssertException<ActionResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.BadRequestAction())
                        .ShouldReturn()
                        .UnsupportedMediaType();
                },
                "When calling BadRequestAction action in MvcController expected action result to be UnsupportedMediaTypeResult, but instead received BadRequestResult.");
        }
    }
}
