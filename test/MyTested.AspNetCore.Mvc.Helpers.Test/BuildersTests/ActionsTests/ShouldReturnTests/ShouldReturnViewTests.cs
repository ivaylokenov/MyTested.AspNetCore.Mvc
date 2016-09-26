namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.ActionsTests.ShouldReturnTests
{
    using Setups;
    using Setups.Controllers;
    using Xunit;

    public class ShouldReturnViewTests
    {
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
    }
}
