namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.ActionsTests.ShouldReturnTests
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

        [Fact]
        public void ShouldReturnContentWithPredicateShouldNotThrowExceptionWithValidPredicate()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.ContentAction())
                .ShouldReturn()
                .Content(content => content.StartsWith("con"));
        }
        
        [Fact]
        public void ShouldReturnContentWithPredicateShouldThrowExceptionWithInvalidPredicate()
        {
            Test.AssertException<ContentResultAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.ContentAction())
                        .ShouldReturn()
                        .Content(content => content.StartsWith("invalid"));
                },
                "When calling ContentAction action in MvcController expected content result ('content') to pass the given predicate, but it failed.");
        }

        [Fact]
        public void ShouldReturnContentWithAssertionsShouldNotThrowExceptionWithValidPredicate()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.ContentAction())
                .ShouldReturn()
                .Content(content =>
                {
                    Assert.True(content.StartsWith("con"));
                });
        }
    }
}
