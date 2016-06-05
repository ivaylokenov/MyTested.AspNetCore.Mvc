namespace MyTested.Mvc.Test.BuildersTests.ModelsTests
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

            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.OkResultActionWithRequestBody(1, requestBody))
                .ShouldReturn()
                .Ok()
                .WithResponseModelOfType<ICollection<ResponseModel>>()
                .ContainingNoErrors();
        }

        [Fact]
        public void ContainingNoErrorsShouldThrowExceptionWhenThereAreModelStateErrors()
        {
            var requestBodyWithErrors = TestObjectFactory.GetRequestModelWithErrors();

            Test.AssertException<ModelErrorAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.OkResultActionWithRequestBody(1, requestBodyWithErrors))
                        .ShouldReturn()
                        .Ok()
                        .WithResponseModelOfType<ICollection<ResponseModel>>()
                        .ContainingNoErrors();
                }, 
                "When calling OkResultActionWithRequestBody action in MvcController expected to have valid model state with no errors, but it had some.");
        }

        [Fact]
        public void AndModelStateErrorShouldNotThrowExceptionWhenTheProvidedModelStateErrorExists()
        {
            var requestBodyWithErrors = TestObjectFactory.GetRequestModelWithErrors();

            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.ModelStateCheck(requestBodyWithErrors))
                .ShouldReturn()
                .Ok()
                .WithResponseModel(requestBodyWithErrors)
                .ContainingError("RequiredString");
        }

        [Fact]
        public void AndModelStateErrorShouldThrowExceptionWhenTheProvidedModelStateErrorDoesNotExist()
        {
            var requestBody = TestObjectFactory.GetValidRequestModel();

            Test.AssertException<ModelErrorAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.ModelStateCheck(requestBody))
                        .ShouldReturn()
                        .Ok()
                        .WithResponseModel(requestBody)
                        .ContainingError("Name");
                }, 
                "When calling ModelStateCheck action in MvcController expected to have a model error against key Name, but none found.");
        }

        [Fact]
        public void AndProvideTheModelShouldReturnProperModelWhenThereIsResponseModel()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.OkResultWithResponse())
                .ShouldReturn()
                .Ok()
                .WithResponseModelOfType<List<ResponseModel>>()
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
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.OkResultWithResponse())
                .ShouldReturn()
                .Ok()
                .WithResponseModelOfType<List<ResponseModel>>()
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
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.CustomModelStateError())
                .ShouldReturn()
                .Ok()
                .WithResponseModelOfType<ICollection<ResponseModel>>()
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
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.CustomModelStateError())
                .ShouldReturn()
                .Ok()
                .WithResponseModelOfType<ICollection<ResponseModel>>()
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
