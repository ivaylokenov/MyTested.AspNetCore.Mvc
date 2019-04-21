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
        public void ShouldReturnViewWithNameShouldNotThrowExceptionWithCorrectName()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.IndexView())
                .ShouldReturn()
                .View("Index");
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
        public void ShouldReturnViewShouldThrowExceptionIfActionResultIsViewResultWithDifferentName()
        {
            Test.AssertException<ViewResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                       .Instance()
                       .Calling(c => c.IndexView())
                       .ShouldReturn()
                       .View("Incorrect");
                },
                "When calling IndexView action in MvcController expected view result to be 'Incorrect', but instead received 'Index'.");
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
        public void ShouldReturnPartialViewWithNameShouldNotThrowExceptionWithCorrectName()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.IndexPartialView())
                .ShouldReturn()
                .PartialView("_IndexPartial");
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
        public void ShouldReturnPartialViewShouldThrowExceptionIfActionResultIsViewResultWithDifferentName()
        {
            Test.AssertException<ViewResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                       .Instance()
                       .Calling(c => c.IndexPartialView())
                       .ShouldReturn()
                       .PartialView("Incorrect");
                },
                "When calling IndexPartialView action in MvcController expected partial view result to be 'Incorrect', but instead received '_IndexPartial'.");
        }
    }
}
