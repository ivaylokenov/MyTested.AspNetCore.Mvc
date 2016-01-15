namespace MyTested.Mvc.Tests.BuildersTests.ActionsTests.ShouldReturn
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
            MyMvc
                .Controller<MvcController>()
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
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.BadRequestAction())
                        .ShouldReturn()
                        .Json();
                }, 
                "When calling BadRequestAction action in MvcController expected action result to be JsonResult, but instead received BadRequestResult.");
        }
    }
}
