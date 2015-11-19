namespace MyTested.Mvc.Test.BuildersTests.ActionsTests.ShouldReturn
{
    using Exceptions;
    using Setups.Controllers;
    using Xunit;
    
    public class ShouldReturnOkResultTests
    {
        [Fact]
        public void ShouldReturnOkResultShouldNotThrowExceptionWithOkResult()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.OkResultAction())
                .ShouldReturn()
                .Ok();
        }

        [Fact]
        public void ShouldReturnOkWithAsyncShouldThrowExceptionIfActionThrowsExceptionWithDefaultReturnValue()
        {
            var exception = Assert.Throws<ActionCallAssertionException>(() =>
            {
                MyMvc
                    .Controller<MvcController>()
                    .CallingAsync(c => c.ActionWithExceptionAsync())
                    .ShouldReturn()
                    .Ok();
            });

            Assert.Equal("AggregateException (containing NullReferenceException with 'Test exception message' message) was thrown but was not caught or expected.", exception.Message);
        }

        [Fact]
        public void ShouldReturnOkResultShouldThrowExceptionWithOtherThanOkResult()
        {
            var exception = Assert.Throws<ActionResultAssertionException>(() =>
            {
                MyMvc
                    .Controller<MvcController>()
                    .Calling(c => c.BadRequestAction())
                    .ShouldReturn()
                    .Ok();
            });

            Assert.Equal("When calling BadRequestAction action in MvcController expected action result to be HttpOkResult or HttpOkObjectResult, but instead received BadRequestResult.", exception.Message);
        }
    }
}
