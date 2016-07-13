namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.ActionsTests.ShouldReturnTests
{
    using Exceptions;
    using Setups;
    using Setups.Controllers;
    using Xunit;
    
    public class ShouldReturnJsonTests
    {
        [Fact]
        public void ShouldReturnJsonShouldNotThrowExceptionIfResultIsJson()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.JsonAction())
                .ShouldReturn()
                .Json();
        }

        [Fact]
        public void ShouldReturnJsonShouldThrowExceptionIfResultIsNotJson()
        {
            Test.AssertException<ActionResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.BadRequestAction())
                        .ShouldReturn()
                        .Json();
                }, 
                "When calling BadRequestAction action in MvcController expected action result to be JsonResult, but instead received BadRequestResult.");
        }
    }
}
