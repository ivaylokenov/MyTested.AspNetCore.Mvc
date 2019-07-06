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

            MyController<MvcController>
                .Instance()
                .Calling(c => c.ModelStateCheck(requestBodyWithErrors))
                .ShouldHave()
                .ModelState(modelState => modelState
                    .For<RequestModel>()
                    .ContainingErrorFor(r => r.RequiredString))
                .AndAlso()
                .ShouldReturn()
                .Ok(ok => ok
                    .WithModel(requestBodyWithErrors));
        }

        [Fact]
        public void AndModelStateErrorForShouldThrowExceptionWhenTheProvidedPropertyDoesNotHaveErrors()
        {
            var requestBody = TestObjectFactory.GetValidRequestModel();

            Test.AssertException<ModelErrorAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.ModelStateCheck(requestBody))
                        .ShouldHave()
                        .ModelState(modelState => modelState
                            .For<RequestModel>()
                            .ContainingErrorFor(r => r.RequiredString))
                        .AndAlso()
                        .ShouldReturn()
                        .Ok(ok => ok
                            .WithModel(requestBody));
                },
                "When calling ModelStateCheck action in MvcController expected to have a model error against key 'RequiredString', but in fact none was found.");
        }

        [Fact]
        public void AndNoModelStateErrorForShouldNotThrowExceptionWhenTheProvidedPropertyDoesNotHaveErrors()
        {
            var requestBody = TestObjectFactory.GetValidRequestModel();

            MyController<MvcController>
                .Instance()
                .Calling(c => c.ModelStateCheck(requestBody))
                .ShouldHave()
                .ModelState(modelState => modelState
                    .For<RequestModel>()
                    .ContainingNoErrorFor(r => r.RequiredString))
                .AndAlso()
                .ShouldReturn()
                .Ok(ok => ok
                    .WithModel(requestBody));
        }

        [Fact]
        public void AndNoModelStateErrorForShouldThrowExceptionWhenTheProvidedPropertyHasErrors()
        {
            var requestBodyWithErrors = TestObjectFactory.GetRequestModelWithErrors();

            Test.AssertException<ModelErrorAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.ModelStateCheck(requestBodyWithErrors))
                        .ShouldHave()
                        .ModelState(modelState => modelState
                            .For<RequestModel>()
                            .ContainingNoErrorFor(r => r.RequiredString))
                        .AndAlso()
                        .ShouldReturn()
                        .Ok(ok => ok
                            .WithModel(requestBodyWithErrors));
                },
                "When calling ModelStateCheck action in MvcController expected to have no model errors against key RequiredString, but found some.");
        }

        [Fact]
        public void AndNoModelStateErrorForShouldNotThrowExceptionWhenChainedWithValidModel()
        {
            var requestBody = TestObjectFactory.GetValidRequestModel();

            MyController<MvcController>
                .Instance()
                .Calling(c => c.ModelStateCheck(requestBody))
                .ShouldHave()
                .ModelState(modelState => modelState
                    .For<RequestModel>()
                    .ContainingNoErrorFor(r => r.Integer)
                    .ContainingNoErrorFor(r => r.RequiredString))
                .AndAlso()
                .ShouldReturn()
                .Ok(ok => ok
                    .WithModel(requestBody));
        }

        [Fact]
        public void AndNoModelStateErrorForShouldThrowExceptionWhenChainedWithInvalidModel()
        {
            var requestBodyWithErrors = TestObjectFactory.GetRequestModelWithErrors();

            Test.AssertException<ModelErrorAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.ModelStateCheck(requestBodyWithErrors))
                        .ShouldHave()
                        .ModelState(modelState => modelState
                            .For<RequestModel>()
                            .ContainingNoErrorFor(r => r.Integer)
                            .ContainingNoErrorFor(r => r.RequiredString))
                        .AndAlso()
                        .ShouldReturn()
                        .Ok(ok => ok
                            .WithModel(requestBodyWithErrors));
                },
                "When calling ModelStateCheck action in MvcController expected to have no model errors against key Integer, but found some.");
        }

        [Fact]
        public void AndProvideTheModelShouldReturnProperModelWhenThereIsResponseModelWithModelStateCheck()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.OkResultWithResponse())
                .ShouldHave()
                .ModelState(modelState => modelState
                    .For<List<ResponseModel>>()
                    .ContainingNoErrorFor(m => m.Count))
                .AndAlso()
                .ShouldReturn()
                .Ok(ok => ok
                    .WithModelOfType<List<ResponseModel>>()
                    .AndAlso()
                    .ShouldPassForThe<List<ResponseModel>>(responseModel =>
                    {
                        Assert.NotNull(responseModel);
                        Assert.IsAssignableFrom<List<ResponseModel>>(responseModel);
                        Assert.Equal(2, responseModel.Count);
                    }));
        }
    }
}
