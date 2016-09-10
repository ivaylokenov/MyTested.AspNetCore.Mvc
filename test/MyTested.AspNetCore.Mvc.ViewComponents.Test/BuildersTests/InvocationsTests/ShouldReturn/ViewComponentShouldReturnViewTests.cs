namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.InvocationsTests.ShouldReturn
{
    using Exceptions;
    using Setups;
    using Setups.Models;
    using Setups.ViewComponents;
    using Xunit;

    public class ViewComponentShouldReturnViewTests
    {
        [Fact]
        public void ShouldReturnViewShouldNotThrowExceptionWithDefaultView()
        {
            MyViewComponent<ViewResultComponent>
                .Instance()
                .InvokedWith(c => c.Invoke(null))
                .ShouldReturn()
                .View();
        }

        [Fact]
        public void ShouldReturnViewWithNameShouldNotThrowExceptionWithCorrectName()
        {
            MyViewComponent<ViewResultComponent>
                .Instance()
                .InvokedWith(c => c.Invoke("custom"))
                .ShouldReturn()
                .View("Custom");
        }

        [Fact]
        public void ShouldReturnViewShouldThrowExceptionIfActionResultIsNotViewResult()
        {
            Test.AssertException<InvocationResultAssertionException>(
                () =>
                {
                    MyViewComponent<NormalComponent>
                       .Instance()
                       .InvokedWith(c => c.Invoke())
                       .ShouldReturn()
                       .View();
                },
                "When invoking NormalComponent expected result to be ViewViewComponentResult, but instead received ContentViewComponentResult.");
        }

        [Fact]
        public void ShouldReturnViewShouldThrowExceptionIfActionResultIsViewResultWithDifferentName()
        {
            Test.AssertException<ViewViewComponentResultAssertionException>(
                () =>
                {
                    MyViewComponent<ViewResultComponent>
                        .Instance()
                        .InvokedWith(c => c.Invoke("custom"))
                        .ShouldReturn()
                        .View("Incorrect");
                },
                "When invoking ViewResultComponent expected view result to be 'Incorrect', but instead received 'Custom'.");
        }

        [Fact]
        public void ShouldReturnViewWithNameShouldNotThrowExceptionWithCorrectModel()
        {
            MyViewComponent<ViewResultComponent>
                .Instance()
                .InvokedWith(c => c.Invoke("model"))
                .ShouldReturn()
                .View(new ResponseModel { StringValue = "TestValue" });
        }

        [Fact]
        public void ShouldReturnViewWithNameShouldNotThrowExceptionWithCorrectNameAndModel()
        {
            MyViewComponent<ViewResultComponent>
                .Instance()
                .InvokedWith(c => c.Invoke("All"))
                .ShouldReturn()
                .View("SomeView", new ResponseModel { IntegerValue = 10 });
        }
    }
}
