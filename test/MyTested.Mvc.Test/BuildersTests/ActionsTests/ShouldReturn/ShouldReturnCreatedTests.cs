namespace MyTested.Mvc.Tests.BuildersTests.ActionsTests.ShouldReturn
{
    using Exceptions;
    using Setups.Controllers;
    using Xunit;
    
    public class ShouldReturnCreatedTests
    {
        [Fact]
        public void ShouldReturnCreatedShouldNotThrowExceptionWithCreatedResult()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.CreatedAction())
                .ShouldReturn()
                .Created();
        }

        [Fact]
        public void ShouldReturnCreatedShouldNotThrowExceptionWithCreatedAtRouteResult()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.CreatedAtRouteAction())
                .ShouldReturn()
                .Created();
        }

        [Fact]
        public void ShouldReturnCreatedShouldThrowExceptionWithBadRequestResult()
        {
            var exception = Assert.Throws<ActionResultAssertionException>(() =>
            {
                MyMvc
                    .Controller<MvcController>()
                    .Calling(c => c.BadRequestAction())
                    .ShouldReturn()
                    .Created();
            });

            Assert.Equal("When calling BadRequestAction action in MvcController expected action result to be CreatedResult or CreatedAtActionResult or CreatedAtRouteResult, but instead received BadRequestResult.", exception.Message);
        }
    }
}
