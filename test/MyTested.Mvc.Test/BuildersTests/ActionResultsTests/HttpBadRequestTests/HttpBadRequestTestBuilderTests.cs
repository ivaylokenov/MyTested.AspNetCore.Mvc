namespace MyTested.Mvc.Tests.BuildersTests.ActionResultsTests.HttpBadRequestTests
{
    using Setups.Controllers;
    using Setups;
    using Setups.Models;
    using Xunit;

    public class HttpBadRequestTestBuilderTests
    {
        // TODO: api changed
        //[Fact]
        //public void WithErrorMessageShouldNotThrowExceptionWhenResultHasErrorMessage()
        //{
        //    MyMvc
        //        .Controller<MvcController>()
        //        .Calling(c => c.BadRequestWithErrorAction())
        //        .ShouldReturn()
        //        .HttpBadRequest()
        //        .WithErrorMessage();
        //}

        //[Fact]
        //[ExpectedException(
        //    typeof(BadRequestResultAssertionException),
        //    ExpectedMessage = "When calling BadRequestAction action in MvcController expected bad request result to contain error message, but it could not be found.")]
        //public void WithErrorMessageShouldThrowExceptionWhenResultDoesNotHaveErrorMessage()
        //{
        //    MyMvc
        //        .Controller<MvcController>()
        //        .Calling(c => c.BadRequestAction())
        //        .ShouldReturn()
        //        .HttpBadRequest()
        //        .WithErrorMessage();
        //}

        //[Fact]
        //public void WithErrorMessageShouldNotThrowExceptionWhenResultHasCorrentErrorMessage()
        //{
        //    MyMvc
        //        .Controller<MvcController>()
        //        .Calling(c => c.BadRequestWithErrorAction())
        //        .ShouldReturn()
        //        .HttpBadRequest()
        //        .WithErrorMessage("Bad request");
        //}

        //[Fact]
        //[ExpectedException(
        //    typeof(BadRequestResultAssertionException),
        //    ExpectedMessage = "When calling BadRequestWithErrorAction action in MvcController expected bad request with message 'Good request', but instead received 'Bad request'.")]
        //public void WithErrorMessageShouldThrowExceptionWhenResultDoesNotHaveCorrentErrorMessage()
        //{
        //    MyMvc
        //        .Controller<MvcController>()
        //        .Calling(c => c.BadRequestWithErrorAction())
        //        .ShouldReturn()
        //        .HttpBadRequest()
        //        .WithErrorMessage("Good request");
        //}

        //[Fact]
        //public void WithModelStateShouldNotThrowExceptionWhenModelStateHasSameErrors()
        //{
        //    var requestModelWithErrors = TestObjectFactory.GetRequestModelWithErrors();
        //    var modelState = new ModelStateDictionary();
        //    modelState.AddModelError("Integer", "The field Integer must be between 1 and 2147483647.");
        //    modelState.AddModelError("RequiredString", "The RequiredString field is required.");

        //    MyMvc
        //        .Controller<MvcController>()
        //        .Calling(c => c.BadRequestWithModelState(requestModelWithErrors))
        //        .ShouldReturn()
        //        .HttpBadRequest()
        //        .WithModelState(modelState);
        //}

        //[Fact]
        //[ExpectedException(
        //    typeof(BadRequestResultAssertionException),
        //    ExpectedMessage = "When calling BadRequestWithModelState action in MvcController expected bad request model state dictionary to contain String key, but none found.")]
        //public void WithModelStateShouldThrowExceptionWhenModelStateHasDifferentKeys()
        //{
        //    var requestModelWithErrors = TestObjectFactory.GetRequestModelWithErrors();
        //    var modelState = new ModelStateDictionary();
        //    modelState.AddModelError("Integer", "The field Integer must be between 1 and 2147483647.");
        //    modelState.AddModelError("String", "The RequiredString field is required.");

        //    MyMvc
        //        .Controller<MvcController>()
        //        .Calling(c => c.BadRequestWithModelState(requestModelWithErrors))
        //        .ShouldReturn()
        //        .HttpBadRequest()
        //        .WithModelState(modelState);
        //}

        //[Fact]
        //[ExpectedException(
        //    typeof(BadRequestResultAssertionException),
        //    ExpectedMessage = "When calling BadRequestWithModelState action in MvcController expected bad request model state dictionary to contain 1 keys, but found 2.")]
        //public void WithModelStateShouldThrowExceptionWhenModelStateHasLessNumberOfKeys()
        //{
        //    var requestModelWithErrors = TestObjectFactory.GetRequestModelWithErrors();
        //    var modelState = new ModelStateDictionary();
        //    modelState.AddModelError("Integer", "The field Integer must be between 1 and 2147483647.");

        //    MyMvc
        //        .Controller<MvcController>()
        //        .Calling(c => c.BadRequestWithModelState(requestModelWithErrors))
        //        .ShouldReturn()
        //        .HttpBadRequest()
        //        .WithModelState(modelState);
        //}

        //[Fact]
        //[ExpectedException(
        //    typeof(BadRequestResultAssertionException),
        //    ExpectedMessage = "When calling BadRequestWithModelState action in MvcController expected bad request model state dictionary to contain 3 keys, but found 2.")]
        //public void WithModelStateShouldThrowExceptionWhenModelStateHasMoreNumberOfKeys()
        //{
        //    var requestModelWithErrors = TestObjectFactory.GetRequestModelWithErrors();
        //    var modelState = new ModelStateDictionary();
        //    modelState.AddModelError("Integer", "The field Integer must be between 1 and 2147483647.");
        //    modelState.AddModelError("RequiredString", "The RequiredString field is required.");
        //    modelState.AddModelError("NonRequiredString", "The NonRequiredString field is required.");

        //    MyMvc
        //        .Controller<MvcController>()
        //        .Calling(c => c.BadRequestWithModelState(requestModelWithErrors))
        //        .ShouldReturn()
        //        .HttpBadRequest()
        //        .WithModelState(modelState);
        //}

        //[Fact]
        //[ExpectedException(
        //    typeof(BadRequestResultAssertionException),
        //    ExpectedMessage = "When calling BadRequestWithModelState action in MvcController expected bad request with message 'The RequiredString field is not required.', but instead received 'The RequiredString field is required.'.")]
        //public void WithModelStateShouldThrowExceptionWhenModelStateHasWrongError()
        //{
        //    var requestModelWithErrors = TestObjectFactory.GetRequestModelWithErrors();
        //    var modelState = new ModelStateDictionary();
        //    modelState.AddModelError("Integer", "The field Integer must be between 1 and 2147483647.");
        //    modelState.AddModelError("RequiredString", "The RequiredString field is not required.");

        //    MyMvc
        //        .Controller<MvcController>()
        //        .Calling(c => c.BadRequestWithModelState(requestModelWithErrors))
        //        .ShouldReturn()
        //        .HttpBadRequest()
        //        .WithModelState(modelState);
        //}

        //[Fact]
        //[ExpectedException(
        //    typeof(BadRequestResultAssertionException),
        //    ExpectedMessage = "When calling BadRequestWithModelState action in MvcController expected bad request model state dictionary to contain 2 errors for RequiredString key, but found 1.")]
        //public void WithModelStateShouldThrowExceptionWhenModelStateHasMoreErrors()
        //{
        //    var requestModelWithErrors = TestObjectFactory.GetRequestModelWithErrors();
        //    var modelState = new ModelStateDictionary();
        //    modelState.AddModelError("Integer", "The field Integer must be between 1 and 2147483647.");
        //    modelState.AddModelError("RequiredString", "The RequiredString field is not required.");
        //    modelState.AddModelError("RequiredString", "The RequiredString field is required.");

        //    MyMvc
        //        .Controller<MvcController>()
        //        .Calling(c => c.BadRequestWithModelState(requestModelWithErrors))
        //        .ShouldReturn()
        //        .HttpBadRequest()
        //        .WithModelState(modelState);
        //}

        //[Fact]
        //public void WithModelStateForShouldNotThrowExceptionWhenModelStateHasSameErrors()
        //{
        //    var requestModelWithErrors = TestObjectFactory.GetRequestModelWithErrors();
        //    var modelState = new ModelStateDictionary();
        //    modelState.AddModelError("Integer", "The field Integer must be between 1 and 2147483647.");
        //    modelState.AddModelError("RequiredString", "The RequiredString field is required.");

        //    MyMvc
        //        .Controller<MvcController>()
        //        .Calling(c => c.BadRequestWithModelState(requestModelWithErrors))
        //        .ShouldReturn()
        //        .HttpBadRequest()
        //        .WithModelStateFor<RequestModel>()
        //        .ContainingModelStateErrorFor(m => m.Integer).ThatEquals("The field Integer must be between 1 and 2147483647.")
        //        .AndAlso()
        //        .ContainingModelStateErrorFor(m => m.RequiredString).BeginningWith("The RequiredString")
        //        .AndAlso()
        //        .ContainingModelStateErrorFor(m => m.RequiredString).EndingWith("required.")
        //        .AndAlso()
        //        .ContainingModelStateErrorFor(m => m.RequiredString).Containing("field")
        //        .AndAlso()
        //        .ContainingNoModelStateErrorFor(m => m.NonRequiredString);
        //}
    }
}
