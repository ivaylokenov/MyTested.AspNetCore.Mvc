namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.ViewComponentResultTests
{
    using Exceptions;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ViewComponents;
    using Setups;
    using Setups.Models;
    using Setups.ViewComponents;
    using Xunit;
    using Xunit.Sdk;

    public class ViewTestBuilderTests
    {
        [Fact]
        public void WithModelShouldNotThrowExceptionWithCorrectModel()
        {
            MyViewComponent<ViewResultComponent>
                .InvokedWith(c => c.Invoke("All"))
                .ShouldReturn()
                .View(view => view
                    .WithModel(new ResponseModel { IntegerValue = 10 }));
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
                        .View(view => view
                            .WithModel(TestObjectFactory.GetListOfResponseModels()));
                },
                "When invoking ViewResultComponent expected response model to be List<ResponseModel>, but instead received ResponseModel.");
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
                        .View(view => view
                            .WithModel((string)null));
                },
                "When invoking ViewResultComponent expected response model to be String, but instead received ResponseModel.");
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
                        .View(view => view
                            .WithModel(new ResponseModel { IntegerValue = 11 }));
                },
                "When invoking ViewResultComponent expected response model ResponseModel to be the given model, but in fact it was a different one. Difference occurs at 'ResponseModel.IntegerValue'. Expected a value of '11', but in fact it was '10'.");
        }

        [Fact]
        public void WithNoModelShouldNotThrowException()
        {
            MyViewComponent<ComponentWithCustomAttribute>
                .Instance()
                .InvokedWith(c => c.Invoke())
                .ShouldReturn()
                .View(view => view
                    .WithNoModel());
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
                        .View(view => view
                            .WithNoModel());
                },
                "When invoking ViewResultComponent expected to not have a view model but in fact such was found.");
        }

        [Fact]
        public void WithModelOfTypeShouldNotThrowExceptionWithCorrectType()
        {
            MyViewComponent<ViewResultComponent>
                .InvokedWith(c => c.Invoke("All"))
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<ResponseModel>());
        }
       
        [Fact]
        public void WithModelOfTypeShouldNotThrowExceptionWithCorrectTypeAndPassAssertions()
        {
            MyViewComponent<ViewResultComponent>
                .Instance()
                .InvokedWith(c => c.Invoke("All"))
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<ResponseModel>()
                    .AndAlso()
                    .Passing(model =>
                    {
                        Assert.IsAssignableFrom<IResponseModel>(model);
                        Assert.True(model.IntegerValue == 10);
                    }));
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
                    .View(view => view
                        .WithModelOfType<ResponseModel>()
                        .AndAlso()
                        .Passing(model =>
                        {
                            Assert.IsAssignableFrom<IResponseModel>(model);
                            Assert.True(model.IntegerValue == 11);
                        }));
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
                        .View(view => view
                            .WithModelOfType<ComparableModel>());
                },
                "When invoking ViewResultComponent expected response model to be ComparableModel, but instead received ResponseModel.");
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
                        .View(view => view
                            .WithModelOfType<ResponseModel>());
                },
                "When invoking ViewResultComponent expected response model to be ResponseModel, but instead received null.");
        }

        [Fact]
        public void WithGenericModelShouldNotThrowException()
        {
            var model = TestObjectFactory.GetValidResponseModel();

            MyViewComponent<ViewResultComponent>
                .Instance()
                .InvokedWith(c => c.Invoke("All"))
                .ShouldReturn()
                .View(view => view
                    .WithModel(model));
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
                        .View(view => view
                            .WithModel(TestObjectFactory.GetListOfResponseModels()));
                },
                "When invoking ViewResultComponent expected response model to be List<ResponseModel>, but instead received ResponseModel.");
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
                        .View(view => view
                            .WithModel<ResponseModel>(null));
                },
                "When invoking ViewResultComponent expected response model ResponseModel to be the given model, but in fact it was a different one. Expected a value of null, but in fact it was 'MyTested.AspNetCore.Mvc.Test.Setups.Models.ResponseModel'.");
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
                        .View(view => view
                            .WithModel(model));
                },
                "When invoking ViewResultComponent expected response model ResponseModel to be the given model, but in fact it was a different one. Difference occurs at 'ResponseModel.IntegerValue'. Expected a value of '11', but in fact it was '10'.");
        }

        [Fact]
        public void PassingWithActionShouldWorkCorrectly()
        {
            MyViewComponent<ViewResultComponent>
                .InvokedWith(c => c.Invoke("custom"))
                .ShouldReturn()
                .View(result => result
                    .Passing(view =>
                    {
                        Assert.Equal("Custom", view.ViewName);
                    }));
        }

        [Fact]
        public void PassingWithPredicateShouldWorkCorrectly()
        {
            MyViewComponent<ViewResultComponent>
                .InvokedWith(c => c.Invoke("custom"))
                .ShouldReturn()
                .View(result => result
                    .Passing(view => view.ViewName == "Custom"));
        }

        [Fact]
        public void PassingWithPredicateShouldThrowExceptionWithInvalidAssertions()
        {
            Test.AssertException<InvocationResultAssertionException>(
                () =>
                {
                    MyViewComponent<ViewResultComponent>
                        .InvokedWith(c => c.Invoke("custom"))
                        .ShouldReturn()
                        .View(result => result
                            .Passing(view => view
                                .ViewName == "Invalid"));
                },
                "When invoking ViewResultComponent expected the ViewViewComponentResult to pass the given predicate, but it failed.");
        }

        [Fact]
        public void AndAlsoShouldWorkCorrectlyWithViewComponentResult()
        {
            MyViewComponent<ViewResultComponent>
                .InvokedWith(c => c.Invoke("All"))
                .ShouldReturn()
                .View()
                .AndAlso()
                .ShouldPassForThe<IViewComponentResult>(actionResult =>
                {
                    Assert.NotNull(actionResult);
                    Assert.IsAssignableFrom<ViewViewComponentResult>(actionResult);
                });
        }
    }
}
