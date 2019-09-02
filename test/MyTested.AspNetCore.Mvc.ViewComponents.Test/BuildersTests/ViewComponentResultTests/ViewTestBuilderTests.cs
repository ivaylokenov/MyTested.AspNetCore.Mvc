namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.ViewComponentResultTests
{
    using Exceptions;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ViewComponents;
    using Setups;
    using Setups.Models;
    using Setups.ViewComponents;
    using Xunit;

    public class ViewTestBuilderTests
    {
        [Fact]
        public void WithModelShouldNotThrowExceptionWithCorrectModel()
        {
            MyViewComponent<ViewResultComponent>
                .InvokedWith(c => c.Invoke("All"))
                .ShouldReturn()
                .View("SomeView")
                .WithModel(new ResponseModel { IntegerValue = 10 });
        }

        [Fact]
        public void WithModelShouldThrowExceptionWithNullModel()
        {
            Test.AssertException<ResponseModelAssertionException>(
                () =>
                {
                    MyViewComponent<ViewResultComponent>
                        .InvokedWith(c => c.Invoke("All"))
                        .ShouldReturn()
                        .View("SomeView")
                        .WithModel(TestObjectFactory.GetListOfResponseModels());
                },
                "When invoking ViewResultComponent expected response model to be of List<ResponseModel> type, but instead received ResponseModel.");
        }

        [Fact]
        public void WithModelShouldThrowExceptionWithExpectedNullModel()
        {
            Test.AssertException<ResponseModelAssertionException>(
                () =>
                {
                    MyViewComponent<ViewResultComponent>
                        .InvokedWith(c => c.Invoke("All"))
                        .ShouldReturn()
                        .View("SomeView")
                        .WithModel((string)null);
                },
                "When invoking ViewResultComponent expected response model to be of String type, but instead received ResponseModel.");
        }

        [Fact]
        public void WithModelShouldThrowExceptionWithIncorrectModel()
        {
            Test.AssertException<ResponseModelAssertionException>(
                () =>
                {
                    MyViewComponent<ViewResultComponent>
                        .InvokedWith(c => c.Invoke("All"))
                        .ShouldReturn()
                        .View("SomeView")
                        .WithModel(new ResponseModel { IntegerValue = 11 });
                },
                "When invoking ViewResultComponent expected response model ResponseModel to be the given model, but in fact it was a different one.");
        }

        [Fact]
        public void WithModelOfTypeShouldNotThrowExceptionWithCorrectType()
        {
            MyViewComponent<ViewResultComponent>
                .InvokedWith(c => c.Invoke("All"))
                .ShouldReturn()
                .View("SomeView")
                .WithModelOfType<ResponseModel>();
        }
        
        [Fact]
        public void AndProvideTheActionResultShouldWorkCorrectlyWithPartial()
        {
            MyViewComponent<ViewResultComponent>
                .InvokedWith(c => c.Invoke("All"))
                .ShouldReturn()
                .View("SomeView")
                .AndAlso()
                .ShouldPassForThe<IViewComponentResult>(actionResult =>
                {
                    Assert.NotNull(actionResult);
                    Assert.IsAssignableFrom<ViewViewComponentResult>(actionResult);
                });
        }
    }
}
