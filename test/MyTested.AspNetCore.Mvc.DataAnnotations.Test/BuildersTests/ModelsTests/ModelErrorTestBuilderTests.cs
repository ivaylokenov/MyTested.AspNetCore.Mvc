namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.ModelsTests
{
    using System.Collections.Generic;
    using Exceptions;
    using Setups;
    using Setups.Controllers;
    using Setups.Models;
    using Xunit;

    public class ModelErrorTestBuilderTests
    {
        [Fact]
        public void ContainingNoErrorsShouldNotThrowExceptionWhenThereAreNoModelStateErrors()
        {
            var requestBody = TestObjectFactory.GetValidRequestModel();

            MyController<MvcController>
                .Instance()
                .Calling(c => c.OkResultActionWithRequestBody(1, requestBody))
                .ShouldReturn()
                .Ok()
                .WithModelOfType<ICollection<ResponseModel>>()
                .ContainingNoErrors();
        }

        [Fact]
        public void ContainingNoErrorsShouldThrowExceptionWhenThereAreModelStateErrors()
        {
            var requestBodyWithErrors = TestObjectFactory.GetRequestModelWithErrors();

            Test.AssertException<ModelErrorAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.OkResultActionWithRequestBody(1, requestBodyWithErrors))
                        .ShouldReturn()
                        .Ok()
                        .WithModelOfType<ICollection<ResponseModel>>()
                        .ContainingNoErrors();
                }, 
                "When calling OkResultActionWithRequestBody action in MvcController expected to have valid model state with no errors, but it had some.");
        }

        [Fact]
        public void AndModelStateErrorShouldNotThrowExceptionWhenTheProvidedModelStateErrorExists()
        {
            var requestBodyWithErrors = TestObjectFactory.GetRequestModelWithErrors();

            MyController<MvcController>
                .Instance()
                .Calling(c => c.ModelStateCheck(requestBodyWithErrors))
                .ShouldReturn()
                .Ok()
                .WithModel(requestBodyWithErrors)
                .ContainingError("RequiredString");
        }

        [Fact]
        public void AndModelStateErrorShouldThrowExceptionWhenTheProvidedModelStateErrorDoesNotExist()
        {
            var requestBody = TestObjectFactory.GetValidRequestModel();

            Test.AssertException<ModelErrorAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.ModelStateCheck(requestBody))
                        .ShouldReturn()
                        .Ok()
                        .WithModel(requestBody)
                        .ContainingError("Name");
                }, 
                "When calling ModelStateCheck action in MvcController expected to have a model error against key Name, but none found.");
        }

        [Fact]
        public void AndProvideTheModelShouldReturnProperModelWhenThereIsResponseModel()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.OkResultWithResponse())
                .ShouldReturn()
                .Ok()
                .WithModelOfType<List<ResponseModel>>()
                .ShouldPassFor()
                .TheModel(responseModel =>
                {
                    Assert.NotNull(responseModel);
                    Assert.IsAssignableFrom<List<ResponseModel>>(responseModel);
                    Assert.Equal(2, responseModel.Count);
                });
        }

        [Fact]
        public void AndProvideTheModelShouldReturnProperModelWhenThereIsResponseModelWithPassing()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.OkResultWithResponse())
                .ShouldReturn()
                .Ok()
                .WithModelOfType<List<ResponseModel>>()
                .Passing(m => m.Count == 2)
                .ShouldPassFor()
                .TheModel(responseModel =>
                {
                    Assert.NotNull(responseModel);
                    Assert.IsAssignableFrom<List<ResponseModel>>(responseModel);
                    Assert.Equal(2, responseModel.Count);
                });
        }

        [Fact]
        public void AndProvideTheModelShouldReturnProperModelWhenThereIsResponseModelWithModelStateError()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.CustomModelStateError())
                .ShouldReturn()
                .Ok()
                .WithModelOfType<ICollection<ResponseModel>>()
                .ContainingError("Test")
                .ShouldPassFor()
                .TheModel(responseModel =>
                {
                    Assert.NotNull(responseModel);
                    Assert.IsAssignableFrom<List<ResponseModel>>(responseModel);
                    Assert.Equal(2, responseModel.Count);
                });
        }

        [Fact]
        public void AndProvideTheModelShouldReturnProperModelWhenThereIsResponseModelWithModelStateErrorAndErrorCheck()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.CustomModelStateError())
                .ShouldReturn()
                .Ok()
                .WithModelOfType<ICollection<ResponseModel>>()
                .ContainingError("Test").ThatEquals("Test error")
                .ShouldPassFor()
                .TheModel(responseModel =>
                {
                    Assert.NotNull(responseModel);
                    Assert.IsAssignableFrom<List<ResponseModel>>(responseModel);
                    Assert.Equal(2, responseModel.Count);
                });
        }
    }
}
