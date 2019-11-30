namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.ModelsTests
{
    using System.Collections.Generic;
    using System.Linq;
    using Exceptions;
    using Setups;
    using Setups.Controllers;
    using Setups.Models;
    using Xunit;

    public class ResponseModelTestBuilderTests
    {
        [Fact]
        public void WithResponseModelShouldNotThrowExceptionWithCorrectResponseModel()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.OkResultWithResponse())
                .ShouldReturn()
                .Ok(ок => ок
                    .WithModelOfType<ICollection<ResponseModel>>());
        }

        [Fact]
        public void WithResponseModelShouldNotThrowExceptionWithIncorrectInheritedTypeArgument()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.OkResultWithResponse())
                .ShouldReturn()
                .Ok(ок => ок
                    .WithModelOfType<IList<ResponseModel>>());
        }

        [Fact]
        public void WithResponseModelShouldNotThrowExceptionWithCorrectImplementatorTypeArgument()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.OkResultWithResponse())
                .ShouldReturn()
                .Ok(ок => ок
                    .WithModelOfType<List<ResponseModel>>());
        }

        [Fact]
        public void WithResponseModelShouldThrowExceptionWithNoResponseModel()
        {
            Test.AssertException<ResponseModelAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.OkResultAction())
                        .ShouldReturn()
                        .Ok(ok => ok
                            .WithModelOfType<ResponseModel>());
                }, 
                "When calling OkResultAction action in MvcController expected response model to be of ResponseModel type, but instead received null.");
        }

        [Fact]
        public void WithResponseModelShouldThrowExceptionWithIncorrectResponseModel()
        {
            Test.AssertException<ResponseModelAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.OkResultWithInterfaceResponse())
                        .ShouldReturn()
                        .Ok(ok => ok
                            .WithModelOfType<ResponseModel>());
                }, 
                "When calling OkResultWithInterfaceResponse action in MvcController expected response model to be of ResponseModel type, but instead received List<ResponseModel>.");
        }

        [Fact]
        public void WithResponseModelShouldThrowExceptionWithIncorrectGenericTypeArgument()
        {
            Test.AssertException<ResponseModelAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.OkResultWithInterfaceResponse())
                        .ShouldReturn()
                        .Ok(ok => ok
                            .WithModelOfType<ICollection<int>>());
                }, 
                "When calling OkResultWithInterfaceResponse action in MvcController expected response model to be of ICollection<Int32> type, but instead received List<ResponseModel>.");
        }

        [Fact]
        public void WithResponseModelShouldNotThrowExceptionWithCorrectPassedExpectedObject()
        {
            var controller = new MvcController();

            MyController<MvcController>
                .Instance(() => controller)
                .Calling(c => c.OkResultWithInterfaceResponse())
                .ShouldReturn()
                .Ok(ok => ok
                    .WithModel(controller.ResponseModel));
        }

        [Fact]
        public void WithResponseModelShouldNotThrowExceptionWithDeeplyEqualPassedExpectedObject()
        {
            var controller = new MvcController();

            MyController<MvcController>
                .Instance()
                .Calling(c => c.OkResultWithResponse())
                .ShouldReturn()
                .Ok(ok => ok
                    .WithModel(controller.ResponseModel));
        }

        [Fact]
        public void WithResponseModelShouldThrowExceptionWithDifferentPassedExpectedObject()
        {
            var controller = new MvcController();

            var another = controller.ResponseModel.ToList();
            another.Add(new ResponseModel());

            Test.AssertException<ResponseModelAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.OkResultWithResponse())
                        .ShouldReturn()
                        .Ok(ok => ok
                            .WithModel(another));
                },
                "When calling OkResultWithResponse action in MvcController expected response model List<ResponseModel> to be the given model, but in fact it was a different one.");
        }

        [Fact]
        public void WithAnonymousModelShouldNotThrowExceptionWithDeeplyEqualModels()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.AnonymousResult())
                .ShouldReturn()
                .Ok(ok => ok
                    .WithModel(new
                    {
                        Id = 1,
                        Text = "test",
                        Nested = new
                        {
                            IsTrue = true
                        }
                    }));
        }

        [Fact]
        public void WithAnonymousModelShouldThrowExceptionWithDeeplyDifferentModels()
        {
            Test.AssertException<ResponseModelAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.AnonymousResult())
                        .ShouldReturn()
                        .Ok(ok => ok
                            .WithModel(new
                            {
                                Id = 1,
                                Text = "test",
                                Nested = new
                                {
                                    IsTrue = false
                                }
                            }));
                },
               "When calling AnonymousResult action in MvcController expected response model AnonymousType0<Int32, String, AnonymousType1<Boolean>> to be the given model, but in fact it was a different one.");
        }

        [Fact]
        public void WithNoResponseModelShouldNotThrowExceptionWhenNoResponseModel()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.OkResultAction())
                .ShouldReturn()
                .Ok(ok => ok
                    .WithNoModel());
        }

        [Fact]
        public void WithNoResponseModelShouldThrowExceptionWhenResponseModelExists()
        {
            Test.AssertException<ResponseModelAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.OkResultWithResponse())
                        .ShouldReturn()
                        .Ok(ok => ok
                            .WithNoModel());
                }, 
                "When calling OkResultWithResponse action in MvcController expected to not have a response model but in fact such was found.");
        }
    }
}
