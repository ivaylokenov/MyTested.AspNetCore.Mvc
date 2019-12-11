namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.ModelsTests
{
    using System.Collections.Generic;
    using System.Linq;
    using Exceptions;
    using Setups;
    using Setups.ActionResults;
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
                "When calling OkResultAction action in MvcController expected response model to be ResponseModel, but instead received null.");
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
                "When calling OkResultWithInterfaceResponse action in MvcController expected response model to be ResponseModel, but instead received List<ResponseModel>.");
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
                "When calling OkResultWithInterfaceResponse action in MvcController expected response model to be ICollection<Int32>, but instead received List<ResponseModel>.");
        }

        [Fact]
        public void WithResponseModelShouldThrowCorrectExceptionWithTypesWithEqualShortNames()
        {
            Test.AssertException<ResponseModelAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.OkResultWithRepeatedName())
                        .ShouldReturn()
                        .Ok(ok => ok
                            .WithModelOfType<CustomActionResult>());
                },
                "When calling OkResultWithRepeatedName action in MvcController expected response model to be MyTested.AspNetCore.Mvc.Test.Setups.ActionResults.CustomActionResult, but instead received MyTested.AspNetCore.Mvc.Test.Setups.Common.CustomActionResult.");
        }

        [Fact]
        public void WithResponseModelShouldThrowCorrectExceptionWithTypesWithEqualShortNamesInCollection()
        {
            Test.AssertException<ResponseModelAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.OkResultWithRepeatedCollectionName())
                        .ShouldReturn()
                        .Ok(ok => ok
                            .WithModelOfType<List<CustomActionResult>>());
                },
                "When calling OkResultWithRepeatedCollectionName action in MvcController expected response model to be System.Collections.Generic.List<MyTested.AspNetCore.Mvc.Test.Setups.ActionResults.CustomActionResult>, but instead received System.Collections.Generic.List<MyTested.AspNetCore.Mvc.Test.Setups.Common.CustomActionResult>.");
        }

        [Fact]
        public void WithResponseModelOfTypeShouldThrowCorrectExceptionWithTypesWithEqualShortNames()
        {
            Test.AssertException<InvocationResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.OkResultWithRepeatedName())
                        .ShouldReturn()
                        .Ok(ok => ok
                            .WithModelOfType(typeof(CustomActionResult)));
                },
                "When calling OkResultWithRepeatedName action in MvcController expected result to be MyTested.AspNetCore.Mvc.Test.Setups.ActionResults.CustomActionResult, but instead received MyTested.AspNetCore.Mvc.Test.Setups.Common.CustomActionResult.");
        }

        [Fact]
        public void WithResponseModelOfTypeShouldThrowCorrectExceptionWithTypesWithEqualShortNamesInCollection()
        {
            Test.AssertException<InvocationResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.OkResultWithRepeatedCollectionName())
                        .ShouldReturn()
                        .Ok(ok => ok
                            .WithModelOfType(typeof(List<CustomActionResult>)));
                },
                "When calling OkResultWithRepeatedCollectionName action in MvcController expected result to be System.Collections.Generic.List<MyTested.AspNetCore.Mvc.Test.Setups.ActionResults.CustomActionResult>, but instead received System.Collections.Generic.List<MyTested.AspNetCore.Mvc.Test.Setups.Common.CustomActionResult>.");
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
                "When calling OkResultWithResponse action in MvcController expected response model List<ResponseModel> to be the given model, but in fact it was a different one. Difference occurs at 'List<ResponseModel>.Count'. Expected a value of '3', but in fact it was '2'.");
        }

        [Fact]
        public void WithAnonymousModelShouldNotThrowExceptionWithDeeplyEqualModels()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.AnonymousOkResult())
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
                        .Calling(c => c.AnonymousOkResult())
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
               "When calling AnonymousOkResult action in MvcController expected response model AnonymousType<Int32, String, AnonymousType<Boolean>> to be the given model, but in fact it was a different one. Difference occurs at 'AnonymousType<Int32, String, AnonymousType<Boolean>>.Nested.IsTrue'. Expected a value of 'False', but in fact it was 'True'.");
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
