namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.ActionsTests.ShouldReturnTests
{
    using Exceptions;
    using Setups;
    using Setups.Controllers;
    using Xunit;

    public class ShouldReturnViewTests
    {
        [Fact]
        public void ShouldReturnViewShouldNotThrowExceptionWithDefaultView()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.DefaultView())
                .ShouldReturn()
                .View();
        }
        
        [Fact]
        public void ShouldReturnViewShouldThrowExceptionIfActionResultIsNotViewResult()
        {
            Test.AssertException<InvocationResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                       .Instance()
                       .Calling(c => c.BadRequestAction())
                       .ShouldReturn()
                       .View();
                },
                "When calling BadRequestAction action in MvcController expected result to be ViewResult, but instead received BadRequestResult.");
        }
        
        [Fact]
        public void ShouldReturnViewShouldThrowExceptionIfActionResultIsViewResultWithoutDefaultName()
        {
            Test.AssertException<ViewResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                       .Instance()
                       .Calling(c => c.IndexView())
                       .ShouldReturn()
                       .View();
                },
                "When calling IndexView action in MvcController expected view result name to be the default one, but instead received 'Index'.");
        }

        [Fact]
        public void ShouldReturnPartialViewShouldNotThrowExceptionWithDefaultPartialView()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.DefaultPartialView())
                .ShouldReturn()
                .PartialView();
        }
        
        [Fact]
        public void ShouldReturnPartialViewShouldThrowExceptionIfActionResultIsNotPartialViewResult()
        {
            Test.AssertException<InvocationResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                       .Instance()
                       .Calling(c => c.DefaultView())
                       .ShouldReturn()
                       .PartialView();
                },
                "When calling DefaultView action in MvcController expected result to be PartialViewResult, but instead received ViewResult.");
        }

        [Fact]
        public void ShouldReturnPartialViewShouldThrowExceptionIfActionResultIsPartialViewResultWithoutDefaultName()
        {
            Test.AssertException<ViewResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                       .Instance()
                       .Calling(c => c.IndexPartialView())
                       .ShouldReturn()
                       .PartialView();
                },
                "When calling IndexPartialView action in MvcController expected partial view result name to be the default one, but instead received '_IndexPartial'.");
        }
    }
}
