namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.ActionsTests.ShouldReturnTests
{
    using Exceptions;
    using Setups;
    using Setups.Controllers;
    using Xunit;

    public class ShouldReturnViewTests
    {
        [Fact]
        public void ShouldReturnViewWithNameShouldNotThrowExceptionWithCorrectName()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.IndexView())
                .ShouldReturn()
                .View(view => view
                    .WithName("Index"));
        }
        
        [Fact]
        public void ShouldReturnViewShouldThrowExceptionIfActionResultIsViewResultWithDifferentName()
        {
            Test.AssertException<ViewResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                       .Instance()
                       .Calling(c => c.IndexView())
                       .ShouldReturn()
                       .View(view => view
                           .WithName("Incorrect"));
                },
                "When calling IndexView action in MvcController expected view result to be 'Incorrect', but instead received 'Index'.");
        }
        
        [Fact]
        public void ShouldReturnPartialViewWithNameShouldNotThrowExceptionWithCorrectName()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.IndexPartialView())
                .ShouldReturn()
                .PartialView(view => view
                    .WithName("_IndexPartial"));
        }
        
        [Fact]
        public void ShouldReturnPartialViewShouldThrowExceptionIfActionResultIsViewResultWithDifferentName()
        {
            Test.AssertException<ViewResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                       .Instance()
                       .Calling(c => c.IndexPartialView())
                       .ShouldReturn()
                       .PartialView(view => view
                           .WithName("Incorrect"));
                },
                "When calling IndexPartialView action in MvcController expected partial view result to be 'Incorrect', but instead received '_IndexPartial'.");
        }
    }
}
