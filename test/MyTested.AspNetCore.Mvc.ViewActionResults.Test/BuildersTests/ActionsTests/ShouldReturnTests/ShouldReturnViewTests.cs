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
            Test.AssertException<ActionResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                       .Instance()
                       .Calling(c => c.BadRequestAction())
                       .ShouldReturn()
                       .View();
                },
                "When calling BadRequestAction action in MvcController expected action result to be ViewResult, but instead received BadRequestResult.");
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
        public void ShouldReturnViewWithNameShouldNotThrowExceptionWithCorrectModel()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.DefaultViewWithModel())
                .ShouldReturn()
                .View(TestObjectFactory.GetListOfResponseModels());
        }
        
        [Fact]
        public void ShouldReturnViewWithNameShouldNotThrowExceptionWithCorrectNameAndModel()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.IndexView())
                .ShouldReturn()
                .View("Index", TestObjectFactory.GetListOfResponseModels());
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
        public void ShouldReturnPartialViewWithNameShouldNotThrowExceptionWithCorrectModel()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.DefaultPartialViewWithModel())
                .ShouldReturn()
                .PartialView(TestObjectFactory.GetListOfResponseModels());
        }

        [Fact]
        public void ShouldReturnPartialViewWithNameShouldNotThrowExceptionWithCorrectNameAndModel()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.IndexPartialView())
                .ShouldReturn()
                .PartialView("_IndexPartial", TestObjectFactory.GetListOfResponseModels());
        }

        [Fact]
        public void ShouldReturnPartialViewShouldThrowExceptionIfActionResultIsNotPartialViewResult()
        {
            Test.AssertException<ActionResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                       .Instance()
                       .Calling(c => c.DefaultView())
                       .ShouldReturn()
                       .PartialView();
                },
                "When calling DefaultView action in MvcController expected action result to be PartialViewResult, but instead received ViewResult.");
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
