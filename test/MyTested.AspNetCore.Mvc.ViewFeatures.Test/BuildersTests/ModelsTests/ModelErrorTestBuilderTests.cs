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
        public void AndModelStateErrorForShouldNotThrowExceptionWhenTheProvidedPropertyHasErrors()
        {
            var requestBodyWithErrors = TestObjectFactory.GetRequestModelWithErrors();

            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.ModelStateCheck(requestBodyWithErrors))
                .ShouldReturn()
                .Ok()
                .WithModel(requestBodyWithErrors)
                .ContainingErrorFor(r => r.RequiredString);
        }

        [Fact]
        public void AndModelStateErrorForShouldThrowExceptionWhenTheProvidedPropertyDoesNotHaveErrors()
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
                        .WithModel(requestBody)
                        .ContainingErrorFor(r => r.RequiredString);
                },
                "When calling ModelStateCheck action in MvcController expected to have a model error against key RequiredString, but none found.");
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
                .WithModel(requestBody)
                .ContainingNoErrorFor(r => r.RequiredString);
        }

        [Fact]
        public void AndNoModelStateErrorForShouldThrowExceptionWhenTheProvidedPropertyHasErrors()
        {
            var requestBodyWithErrors = TestObjectFactory.GetRequestModelWithErrors();

            Test.AssertException<ModelErrorAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.ModelStateCheck(requestBodyWithErrors))
                        .ShouldReturn()
                        .Ok()
                        .WithModel(requestBodyWithErrors)
                        .ContainingNoErrorFor(r => r.RequiredString);
                },
                "When calling ModelStateCheck action in MvcController expected to have no model errors against key RequiredString, but found some.");
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
                .WithModel(requestBody)
                .ContainingNoErrorFor(r => r.Integer)
                .ContainingNoErrorFor(r => r.RequiredString);
        }

        [Fact]
        public void AndNoModelStateErrorForShouldThrowExceptionWhenChainedWithInvalidModel()
        {
            var requestBodyWithErrors = TestObjectFactory.GetRequestModelWithErrors();

            Test.AssertException<ModelErrorAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.ModelStateCheck(requestBodyWithErrors))
                        .ShouldReturn()
                        .Ok()
                        .WithModel(requestBodyWithErrors)
                        .ContainingNoErrorFor(r => r.Integer)
                        .ContainingNoErrorFor(r => r.RequiredString);
                },
                "When calling ModelStateCheck action in MvcController expected to have no model errors against key Integer, but found some.");
        }

        [Fact]
        public void AndProvideTheModelShouldReturnProperModelWhenThereIsResponseModelWithModelStateCheck()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.OkResultWithResponse())
                .ShouldReturn()
                .Ok()
                .WithModelOfType<List<ResponseModel>>()
                .ContainingNoErrorFor(m => m.Count)
                .AndAlso()
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
