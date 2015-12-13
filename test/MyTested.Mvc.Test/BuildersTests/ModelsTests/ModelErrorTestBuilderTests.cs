namespace MyTested.Mvc.Tests.BuildersTests.ModelsTests
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
                .ContainingNoModelStateErrors();
        }

        [Fact]
        public void ContainingNoErrorsShouldThrowExceptionWhenThereAreModelStateErrors()
        {
            var requestBodyWithErrors = TestObjectFactory.GetRequestModelWithErrors();

            Test.AssertException<ModelErrorAssertionException>(() =>
            {
                MyMvc
                    .Controller<MvcController>()
                    .Calling(c => c.OkResultActionWithRequestBody(1, requestBodyWithErrors))
                    .ShouldReturn()
                    .Ok()
                    .WithResponseModelOfType<ICollection<ResponseModel>>()
                    .ContainingNoModelStateErrors();
            }, "When calling OkResultActionWithRequestBody action in MvcController expected to have valid model state with no errors, but it had some.");
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
                .ContainingModelStateError("RequiredString");
        }

        [Fact]
        public void AndModelStateErrorShouldThrowExceptionWhenTheProvidedModelStateErrorDoesNotExist()
        {
            var requestBody = TestObjectFactory.GetValidRequestModel();

            Test.AssertException<ModelErrorAssertionException>(() =>
            {
                MyMvc
                    .Controller<MvcController>()
                    .Calling(c => c.ModelStateCheck(requestBody))
                    .ShouldReturn()
                    .Ok()
                    .WithResponseModel(requestBody)
                    .ContainingModelStateError("Name");
            }, "When calling ModelStateCheck action in MvcController expected to have a model error against key Name, but none found.");
        }

        [Fact]
        public void AndModelStateErrorForShouldNotThrowExceptionWhenTheProvidedPropertyHasErrors()
        {
            var requestBodyWithErrors = TestObjectFactory.GetRequestModelWithErrors();

            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.ModelStateCheck(requestBodyWithErrors))
                .ShouldReturn()
                .Ok()
                .WithResponseModel(requestBodyWithErrors)
                .ContainingModelStateErrorFor(r => r.RequiredString);
        }

        [Fact]
        public void AndModelStateErrorForShouldThrowExceptionWhenTheProvidedPropertyDoesNotHaveErrors()
        {
            var requestBody = TestObjectFactory.GetValidRequestModel();

            Test.AssertException<ModelErrorAssertionException>(() =>
            {
                MyMvc
                    .Controller<MvcController>()
                    .Calling(c => c.ModelStateCheck(requestBody))
                    .ShouldReturn()
                    .Ok()
                    .WithResponseModel(requestBody)
                    .ContainingModelStateErrorFor(r => r.RequiredString);
            }, "When calling ModelStateCheck action in MvcController expected to have a model error against key RequiredString, but none found.");
        }

        [Fact]
        public void AndNoModelStateErrorForShouldNotThrowExceptionWhenTheProvidedPropertyDoesNotHaveErrors()
        {
            var requestBody = TestObjectFactory.GetValidRequestModel();

            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.ModelStateCheck(requestBody))
                .ShouldReturn()
                .Ok()
                .WithResponseModel(requestBody)
                .ContainingNoModelStateErrorFor(r => r.RequiredString);
        }

        [Fact]
        public void AndNoModelStateErrorForShouldThrowExceptionWhenTheProvidedPropertyHasErrors()
        {
            var requestBodyWithErrors = TestObjectFactory.GetRequestModelWithErrors();

            Test.AssertException<ModelErrorAssertionException>(() =>
            {
                MyMvc
                    .Controller<MvcController>()
                    .Calling(c => c.ModelStateCheck(requestBodyWithErrors))
                    .ShouldReturn()
                    .Ok()
                    .WithResponseModel(requestBodyWithErrors)
                    .ContainingNoModelStateErrorFor(r => r.RequiredString);
            }, "When calling ModelStateCheck action in MvcController expected to have no model errors against key RequiredString, but found some.");
        }

        [Fact]
        public void AndNoModelStateErrorForShouldNotThrowExceptionWhenChainedWithValidModel()
        {
            var requestBody = TestObjectFactory.GetValidRequestModel();

            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.ModelStateCheck(requestBody))
                .ShouldReturn()
                .Ok()
                .WithResponseModel(requestBody)
                .ContainingNoModelStateErrorFor(r => r.Integer)
                .ContainingNoModelStateErrorFor(r => r.RequiredString);
        }

        [Fact]
        public void AndNoModelStateErrorForShouldThrowExceptionWhenChainedWithInvalidModel()
        {
            var requestBodyWithErrors = TestObjectFactory.GetRequestModelWithErrors();

            Test.AssertException<ModelErrorAssertionException>(() =>
            {
                MyMvc
                    .Controller<MvcController>()
                    .Calling(c => c.ModelStateCheck(requestBodyWithErrors))
                    .ShouldReturn()
                    .Ok()
                    .WithResponseModel(requestBodyWithErrors)
                    .ContainingNoModelStateErrorFor(r => r.Integer)
                    .ContainingNoModelStateErrorFor(r => r.RequiredString);
            }, "When calling ModelStateCheck action in MvcController expected to have no model errors against key Integer, but found some.");
        }

        [Fact]
        public void AndProvideTheModelShouldReturnProperModelWhenThereIsResponseModel()
        {
            var responseModel = MyMvc
                .Controller<MvcController>()
                .Calling(c => c.OkResultWithResponse())
                .ShouldReturn()
                .Ok()
                .WithResponseModelOfType<List<ResponseModel>>()
                .AndProvideTheModel();

            Assert.NotNull(responseModel);
            Assert.IsAssignableFrom<List<ResponseModel>>(responseModel);
            Assert.Equal(2, responseModel.Count);
        }

        [Fact]
        public void AndProvideTheModelShouldReturnProperModelWhenThereIsResponseModelWithPassing()
        {
            var responseModel = MyMvc
                .Controller<MvcController>()
                .Calling(c => c.OkResultWithResponse())
                .ShouldReturn()
                .Ok()
                .WithResponseModelOfType<List<ResponseModel>>()
                .Passing(m => m.Count == 2)
                .AndProvideTheModel();

            Assert.NotNull(responseModel);
            Assert.IsAssignableFrom<List<ResponseModel>>(responseModel);
            Assert.Equal(2, responseModel.Count);
        }

        [Fact]
        public void AndProvideTheModelShouldReturnProperModelWhenThereIsResponseModelWithModelStateCheck()
        {
            var responseModel = MyMvc
                .Controller<MvcController>()
                .Calling(c => c.OkResultWithResponse())
                .ShouldReturn()
                .Ok()
                .WithResponseModelOfType<List<ResponseModel>>()
                .ContainingNoModelStateErrorFor(m => m.Count)
                .AndProvideTheModel();

            Assert.NotNull(responseModel);
            Assert.IsAssignableFrom<List<ResponseModel>>(responseModel);
            Assert.Equal(2, responseModel.Count);
        }

        [Fact]
        public void AndProvideTheModelShouldReturnProperModelWhenThereIsResponseModelWithModelStateError()
        {
            var responseModel = MyMvc
                .Controller<MvcController>()
                .Calling(c => c.CustomModelStateError())
                .ShouldReturn()
                .Ok()
                .WithResponseModelOfType<ICollection<ResponseModel>>()
                .ContainingModelStateError("Test")
                .AndProvideTheModel();

            Assert.NotNull(responseModel);
            Assert.IsAssignableFrom<List<ResponseModel>>(responseModel);
            Assert.Equal(2, responseModel.Count);
        }

        [Fact]
        public void AndProvideTheModelShouldReturnProperModelWhenThereIsResponseModelWithModelStateErrorAndErrorCheck()
        {
            var responseModel = MyMvc
                .Controller<MvcController>()
                .Calling(c => c.CustomModelStateError())
                .ShouldReturn()
                .Ok()
                .WithResponseModelOfType<ICollection<ResponseModel>>()
                .ContainingModelStateError("Test").ThatEquals("Test error")
                .AndProvideTheModel();

            Assert.NotNull(responseModel);
            Assert.IsAssignableFrom<List<ResponseModel>>(responseModel);
            Assert.Equal(2, responseModel.Count);
        }
    }
}
