namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.ActionsTests.ShouldReturnTests
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
            MyController<MvcController>
                .Instance()
                .Calling(c => c.OkResultAction())
                .ShouldReturn()
                .Ok();
        }

        [Fact]
        public void ShouldReturnOkWithAsyncShouldThrowExceptionIfActionThrowsExceptionWithDefaultReturnValue()
        {
            Test.AssertException<InvocationAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.ActionWithExceptionAsync())
                        .ShouldReturn()
                        .Ok();
                },
                "When calling ActionWithExceptionAsync action in MvcController expected no exception but AggregateException (containing NullReferenceException with 'Test exception message' message) was thrown without being caught.");
        }

        [Fact]
        public void ShouldReturnOkResultShouldThrowExceptionWithOtherThanOkResult()
        {
            Test.AssertException<ActionResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.BadRequestAction())
                        .ShouldReturn()
                        .Ok();
                }, 
                "When calling BadRequestAction action in MvcController expected action result to be OkResult, but instead received BadRequestResult.");
        }

        [Fact]
        public void ShouldReturnOkResultShouldThrowExceptionWithInheritedOkResult()
        {
            Test.AssertException<ActionResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.CustomActionResult())
                        .ShouldReturn()
                        .Ok();
                },
                "When calling CustomActionResult action in MvcController expected action result to be OkResult, but instead received CustomActionResult.");
        }
    }
}
