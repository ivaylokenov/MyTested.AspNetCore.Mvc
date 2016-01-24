namespace MyTested.Mvc.Tests.BuildersTests.ActionsTests.ShouldReturn
{
    using Exceptions;
    using Setups;
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
            Test.AssertException<ActionCallAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .CallingAsync(c => c.ActionWithExceptionAsync())
                        .ShouldReturn()
                        .Ok();
                }, 
                "AggregateException (containing NullReferenceException with 'Test exception message' message) was thrown but was not caught or expected.");
        }

        [Fact]
        public void ShouldReturnOkResultShouldThrowExceptionWithOtherThanOkResult()
        {
            Test.AssertException<ActionResultAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.BadRequestAction())
                        .ShouldReturn()
                        .Ok();
                }, 
                "When calling BadRequestAction action in MvcController expected action result to be HttpOkResult, but instead received BadRequestResult.");
        }

        [Fact]
        public void ShouldReturnOkResultShouldThrowExceptionWithInheritedOkResult()
        {
            Test.AssertException<ActionResultAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.CustomActionResult())
                        .ShouldReturn()
                        .Ok();
                },
                "When calling CustomActionResult action in MvcController expected action result to be HttpOkResult, but instead received CustomActionResult.");
        }
    }
}
