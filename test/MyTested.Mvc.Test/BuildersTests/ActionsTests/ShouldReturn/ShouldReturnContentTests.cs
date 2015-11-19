namespace MyTested.Mvc.Test.BuildersTests.ActionsTests.ShouldReturn
{
    using Exceptions;
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
                .Content();
        }

        [Fact]
        public void ShouldReturnContentShouldNotThrowExceptionWithMediaTypeContentResult()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.ContentActionWithMediaType())
                .ShouldReturn()
                .Content();
        }

        [Fact]
        public void ShouldReturnContentShouldThrowExceptionWithBadRequestResult()
        {
            var exception = Assert.Throws<ActionResultAssertionException>(() =>
            {
                MyMvc
                    .Controller<MvcController>()
                    .Calling(c => c.BadRequestAction())
                    .ShouldReturn()
                    .Content();
            });

            Assert.Equal("When calling BadRequestAction action in MvcController expected action result to be ContentResult, but instead received BadRequestResult.", exception.Message);
        }
    }
}
