namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.ViewComponentResultTests
{
    using Exceptions;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ViewComponents;
    using Setups;
    using Setups.Models;
    using Setups.ViewComponents;
    using System.Collections.Generic;
    using Xunit;
    using Xunit.Sdk;

    public class ViewTestBuilderTests
    {
        [Fact]
        public void WithModelShouldNotThrowExceptionWithCorrectModel()
        {
            MyViewComponent<ViewResultComponent>
                .Instance()
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
                        .Instance()
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
                        .Instance()
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
                        .Instance()
                        .InvokedWith(c => c.Invoke("All"))
                        .ShouldReturn()
                        .View("SomeView")
                        .WithModel(new ResponseModel { IntegerValue = 11 });
                },
                "When invoking ViewResultComponent expected response model ResponseModel to be the given model, but in fact it was a different one.");
        }

        [Fact]
        public void WithNoModelShouldNotThrowException()
        {
            MyViewComponent<ComponentWithCustomAttribute>
                .Instance()
                .InvokedWith(c => c.Invoke())
                .ShouldReturn()
                .View()
                .WithNoModel();
        }

        [Fact]
        public void WithNoModelShouldThrowExceptionWithModel()
        {
            Test.AssertException<ResponseModelAssertionException>(
                () =>
                {
                    MyViewComponent<ViewResultComponent>
                        .Instance()
                        .InvokedWith(c => c.Invoke("All"))
                        .ShouldReturn()
                        .View("SomeView")
                        .WithNoModel();
                },
                "When invoking ViewResultComponent expected to not have a view model but in fact such was found.");
        }

        [Fact]
        public void WithModelOfTypeShouldNotThrowExceptionWithCorrectType()
        {
            MyViewComponent<ViewResultComponent>
                .Instance()
                .InvokedWith(c => c.Invoke("All"))
                .ShouldReturn()
                .View("SomeView")
                .WithModelOfType<ResponseModel>();
        }

        [Fact]
        public void WithModelOfTypeShouldNotThrowExceptionWithCorrectTypeAndPassAssertions()
        {
            MyViewComponent<ViewResultComponent>
                .Instance()
                .InvokedWith(c => c.Invoke("All"))
                .ShouldReturn()
                .View("SomeView")
                .WithModelOfType<ResponseModel>()
                .AndAlso()
                .Passing(model =>
                {
                    Assert.IsAssignableFrom<IResponseModel>(model);
                    Assert.True(model.IntegerValue == 10);
                });
        }

        [Fact]
        public void WithModelOfTypeShouldNotThrowExceptionWithCorrectTypeAndIncorrectAssertions()
        {
            Assert.Throws<TrueException>(
            () =>
            {
                MyViewComponent<ViewResultComponent>
                    .Instance()
                    .InvokedWith(c => c.Invoke("All"))
                    .ShouldReturn()
                    .View("SomeView")
                    .WithModelOfType<ResponseModel>()
                    .AndAlso()
                    .Passing(model =>
                    {
                        Assert.IsAssignableFrom<IResponseModel>(model);
                        Assert.True(model.IntegerValue == 11);
                    });
            });
        }

        [Fact]
        public void WithModelOfTypeShouldThrowExceptionWithIncorrectType()
        {
            Test.AssertException<ResponseModelAssertionException>(
                () =>
                {
                    MyViewComponent<ViewResultComponent>
                        .Instance()
                        .InvokedWith(c => c.Invoke("All"))
                        .ShouldReturn()
                        .View("SomeView")
                        .WithModelOfType<ComparableModel>();
                },
                "When invoking ViewResultComponent expected response model to be of ComparableModel type, but instead received ResponseModel.");
        }

        [Fact]
        public void WithModelOfTypeShouldThrowExceptionWithoutModel()
        {
            Test.AssertException<ResponseModelAssertionException>(
                () =>
                {
                    MyViewComponent<ViewResultComponent>
                        .Instance()
                        .InvokedWith(c => c.Invoke(null))
                        .ShouldReturn()
                        .View()
                        .WithModelOfType<ResponseModel>();
                },
                "When invoking ViewResultComponent expected response model to be of ResponseModel type, but instead received null.");
        }


        [Fact]
        public void WithGenericModelShouldNotThrowException()
        {
            var model = TestObjectFactory.GetValidResponseModel();

            MyViewComponent<ViewResultComponent>
                .Instance()
                .InvokedWith(c => c.Invoke("All"))
                .ShouldReturn()
                .View("SomeView")
                .WithModel<ResponseModel>(model);
        }

        [Fact]
        public void WithGenericModelShouldThrowExceptionWithListOfModels()
        {
            Test.AssertException<ResponseModelAssertionException>(
                () =>
                {
                    MyViewComponent<ViewResultComponent>
                        .Instance()
                        .InvokedWith(c => c.Invoke("All"))
                        .ShouldReturn()
                        .View("SomeView")
                        .WithModel<List<ResponseModel>>(TestObjectFactory.GetListOfResponseModels());
                },
                "When invoking ViewResultComponent expected response model to be of List<ResponseModel> type, but instead received ResponseModel.");
        }

        [Fact]
        public void WithGenericModelShouldThrowExceptionWithExpectedNullModel()
        {
            Test.AssertException<ResponseModelAssertionException>(
                () =>
                {
                    MyViewComponent<ViewResultComponent>
                        .Instance()
                        .InvokedWith(c => c.Invoke("All"))
                        .ShouldReturn()
                        .View("SomeView")
                        .WithModel<ResponseModel>(null);
                },
                "When invoking ViewResultComponent expected response model ResponseModel to be the given model, but in fact it was a different one.");
        }

        [Fact]
        public void WithGenericModelShouldThrowExceptionWithIncorrectModel()
        {
            var model = TestObjectFactory.GetValidResponseModel();
            model.IntegerValue = 11;

            Test.AssertException<ResponseModelAssertionException>(
                () =>
                {
                    MyViewComponent<ViewResultComponent>
                        .Instance()
                        .InvokedWith(c => c.Invoke("All"))
                        .ShouldReturn()
                        .View("SomeView")
                        .WithModel<ResponseModel>(model);
                },
                "When invoking ViewResultComponent expected response model ResponseModel to be the given model, but in fact it was a different one.");
        }

        [Fact]
        public void AndProvideTheActionResultShouldWorkCorrectlyWithPartial()
        {
            MyViewComponent<ViewResultComponent>
                .Instance()
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
