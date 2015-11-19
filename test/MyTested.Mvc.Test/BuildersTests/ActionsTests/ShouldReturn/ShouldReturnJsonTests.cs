namespace MyTested.Mvc.Test.BuildersTests.ActionsTests.ShouldReturn
{
    using Exceptions;
    using Setups.Controllers;
    using Xunit;
    
    public class ShouldReturnJsonTests
    {
        [Fact]
        public void ShouldReturnJsonShouldNotThrowExceptionIfResultIsJson()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.JsonAction())
                .ShouldReturn()
                .Json();
        }

        [Fact]
        public void ShouldReturnJsonShouldThrowExceptionIfResultIsNotJson()
        {
            var exception = Assert.Throws<ActionResultAssertionException>(() =>
            {
                MyMvc
                    .Controller<MvcController>()
                    .Calling(c => c.BadRequestAction())
                    .ShouldReturn()
                    .Json();
            });

            Assert.Equal("When calling BadRequestAction action in MvcController expected action result to be JsonResult, but instead received BadRequestResult.", exception.Message);
        }
    }
}
