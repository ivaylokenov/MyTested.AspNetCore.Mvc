namespace MyTested.Mvc.Tests.BuildersTests.ActionsTests.ShouldReturn
{
    using Exceptions;
    using Setups;
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
        public void ShouldReturnCreatedShouldNotThrowExceptionWithCreatedAtActionResult()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.CreatedAtActionResult())
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
            Test.AssertException<ActionResultAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.BadRequestAction())
                        .ShouldReturn()
                        .Created();
                }, 
                "When calling BadRequestAction action in MvcController expected action result to be CreatedResult, but instead received BadRequestResult.");
        }
    }
}
