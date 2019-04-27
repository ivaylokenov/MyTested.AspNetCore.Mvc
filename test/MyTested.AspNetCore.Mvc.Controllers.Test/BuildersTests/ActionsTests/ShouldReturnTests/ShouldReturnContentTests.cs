namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.ActionsTests.ShouldReturnTests
{
    using Setups.Controllers;
    using Xunit;
    
    public class ShouldReturnContentTests
    {
        [Fact]
        public void ShouldReturnContentShouldNotThrowExceptionWithNegotiatedContentResult()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.ContentAction())
                .ShouldReturn()
                .Content();
        }
    }
}
