namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.ModelsTests
{
    using System.Collections.Generic;
    using Exceptions;
    using Setups;
    using Setups.Controllers;
    using Setups.Models;
    using Xunit;

    public class ModelErrorTestBuilderTModelTest
    {
        [Fact]
        public void ContainingNoErrorsShouldNotThrowExceptionWhenThereAreNoModelStateErrors()
        {
            var requestBody = TestObjectFactory.GetValidRequestModel();

            MyController<MvcController>
                .Instance()
                .Calling(c => c.OkResultActionWithRequestBody(1, requestBody))
                .ShouldHave()
                .ModelState(modelState => modelState
                    .For<RequestModel>()
                    .ContainingNoErrors())
                .AndAlso()
                .ShouldReturn()
                .Ok(ok => ok
                    .WithModelOfType<ICollection<ResponseModel>>());
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
                        .ShouldHave()
                        .ModelState(modelState => modelState
                            .For<RequestModel>()
                            .ContainingNoErrors())
                        .AndAlso()
                        .ShouldReturn()
                        .Ok(ok => ok
                            .WithModelOfType<ICollection<ResponseModel>>());
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
                .ShouldHave()
                .ModelState(modelState => modelState
                    .For<RequestModel>()
                    .ContainingError("RequiredString")
                    .AndAlso()
                    .ContainingNoError("MissingError"))
                .AndAlso()
                .ShouldReturn()
                .Ok(ok => ok
                    .WithModel(requestBodyWithErrors));
        }

        [Fact]
        public void AndModelStateErrorShouldThrowExceptionWhenTheProvidedModelStateErrorDoesNotExists()
        {
            Test.AssertException<ModelErrorAssertionException>(
                () =>
                {
                    var requestBodyWithErrors = TestObjectFactory.GetRequestModelWithErrors();

                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.ModelStateCheck(requestBodyWithErrors))
                        .ShouldHave()
                        .ModelState(modelState => modelState
                            .For<RequestModel>()
                            .ContainingNoError("RequiredString"))
                        .AndAlso()
                        .ShouldReturn()
                        .Ok(ok => ok
                            .WithModel(requestBodyWithErrors));
                },
                "When calling ModelStateCheck action in MvcController expected to not have a model error against key 'RequiredString', but in fact such was found.");
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
                        .ShouldHave()
                        .ModelState(modelState => modelState
                            .For<RequestModel>()
                            .ContainingError("Name"))
                        .AndAlso()
                        .ShouldReturn()
                        .Ok(ok => ok
                            .WithModel(requestBody));
                },
                "When calling ModelStateCheck action in MvcController expected to have a model error against key 'Name', but in fact none was found.");
        }

        [Fact]
        public void AndProvideTheModelShouldReturnProperModelWhenThereIsResponseModelWithModelStateError()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.CustomModelStateError())
                .ShouldHave()
                .ModelState(modelState => modelState
                    .For<RequestModel>()
                    .ContainingError("Test"))
                .AndAlso()
                .ShouldReturn()
                .Ok(ok => ok
                    .WithModelOfType<ICollection<ResponseModel>>()
                    .ShouldPassForThe<ICollection<ResponseModel>>(responseModel =>
                    {
                        Assert.NotNull(responseModel);
                        Assert.IsAssignableFrom<List<ResponseModel>>(responseModel);
                        Assert.Equal(2, responseModel.Count);
                    }));
        }

        [Fact]
        public void AndProvideTheModelShouldReturnProperModelWhenThereIsResponseModelWithModelStateErrorAndErrorCheck()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.CustomModelStateError())
                .ShouldHave()
                .ModelState(modelState => modelState
                    .For<RequestModel>()
                    .ContainingError("Test")
                    .ThatEquals("Test error"))
                .AndAlso()
                .ShouldReturn()
                .Ok(ok => ok
                    .WithModelOfType<ICollection<ResponseModel>>()
                    .ShouldPassForThe<ICollection<ResponseModel>>(responseModel =>
                    {
                        Assert.NotNull(responseModel);
                        Assert.IsAssignableFrom<List<ResponseModel>>(responseModel);
                        Assert.Equal(2, responseModel.Count);
                    }));
        }

        [Fact]
        public void AndProvideTheModelShouldReturnProperModelWhenThereIsResponseModelWithModelStateErrorAndBeginningWith()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.CustomModelStateError())
                .ShouldHave()
                .ModelState(modelState => modelState
                    .For<RequestModel>()
                    .ContainingError("Test")
                    .BeginningWith("Test"))
                .AndAlso()
                .ShouldReturn()
                .Ok(ok => ok
                    .WithModelOfType<ICollection<ResponseModel>>()
                    .ShouldPassForThe<ICollection<ResponseModel>>(responseModel =>
                    {
                        Assert.NotNull(responseModel);
                        Assert.IsAssignableFrom<List<ResponseModel>>(responseModel);
                        Assert.Equal(2, responseModel.Count);
                    }));
        }

        [Fact]
        public void AndProvideTheModelShouldReturnProperModelWhenThereIsResponseModelWithModelStateErrorAndEndingWith()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.CustomModelStateError())
                .ShouldHave()
                .ModelState(modelState => modelState
                    .For<RequestModel>()
                    .ContainingError("Test")
                    .EndingWith("error"))
                .AndAlso()
                .ShouldReturn()
                .Ok(ok => ok
                    .WithModelOfType<ICollection<ResponseModel>>()
                    .ShouldPassForThe<ICollection<ResponseModel>>(responseModel =>
                    {
                        Assert.NotNull(responseModel);
                        Assert.IsAssignableFrom<List<ResponseModel>>(responseModel);
                        Assert.Equal(2, responseModel.Count);
                    }));
        }

        [Fact]
        public void AndProvideTheModelShouldReturnProperModelWhenThereIsResponseModelWithModelStateErrorAndContaining()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.CustomModelStateError())
                .ShouldHave()
                .ModelState(modelState => modelState
                    .For<RequestModel>()
                    .ContainingError("Test")
                    .Containing("st err"))
                .AndAlso()
                .ShouldReturn()
                .Ok(ok => ok
                    .WithModelOfType<ICollection<ResponseModel>>()
                    .ShouldPassForThe<ICollection<ResponseModel>>(responseModel =>
                    {
                        Assert.NotNull(responseModel);
                        Assert.IsAssignableFrom<List<ResponseModel>>(responseModel);
                        Assert.Equal(2, responseModel.Count);
                    }));
        }

        [Fact]
        public void AndProvideTheModelShouldReturnProperModelWhenThereIsResponseModelWithModelStateErrorAndContainingError()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.CustomModelStateError())
                .ShouldHave()
                .ModelState(modelState => modelState
                    .For<RequestModel>()
                    .ContainingError("Test")
                    .ContainingError("Test"))
                .AndAlso()
                .ShouldReturn()
                .Ok(ok => ok
                    .WithModelOfType<ICollection<ResponseModel>>()
                    .ShouldPassForThe<ICollection<ResponseModel>>(responseModel =>
                    {
                        Assert.NotNull(responseModel);
                        Assert.IsAssignableFrom<List<ResponseModel>>(responseModel);
                        Assert.Equal(2, responseModel.Count);
                    }));
        }
    }
}

