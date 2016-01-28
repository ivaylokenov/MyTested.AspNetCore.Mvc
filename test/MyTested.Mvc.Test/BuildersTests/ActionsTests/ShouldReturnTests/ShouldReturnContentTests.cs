namespace MyTested.Mvc.Tests.BuildersTests.ActionsTests.ShouldReturnTests
{
    using Exceptions;
    using Setups;
    using Setups.Controllers;
    using Xunit;
    
    public class ShouldReturnContentTests
    {
        [Fact]
        public void ShouldReturnContentShouldNotThrowExceptionWithNegotiatedContentResult()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.ContentAction())
                .ShouldReturn()
                .Content("content");
        }

        [Fact]
        public void ShouldReturnContentShouldNotThrowExceptionWithMediaTypeContentResult()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.ContentActionWithMediaType())
                .ShouldReturn()
                .Content("content");
        }

        [Fact]
        public void ShouldReturnContentShouldThrowExceptionWithIncorrectContent()
        {
            Test.AssertException<ContentResultAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.ContentAction())
                        .ShouldReturn()
                        .Content("incorrect");
                },
                "When calling ContentAction action in MvcController expected content result to contain 'incorrect', but instead received 'content'.");
        }

        [Fact]
        public void ShouldReturnContentShouldThrowExceptionWithBadRequestResult()
        {
            Test.AssertException<ActionResultAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.BadRequestAction())
                        .ShouldReturn()
                        .Content("content");
                }, 
                "When calling BadRequestAction action in MvcController expected action result to be ContentResult, but instead received BadRequestResult.");
        }
    }
}
