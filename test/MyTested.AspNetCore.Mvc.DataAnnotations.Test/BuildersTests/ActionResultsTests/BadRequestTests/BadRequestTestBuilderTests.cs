namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.ActionResultsTests.BadRequestTests
{
    using Exceptions;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Setups;
    using Setups.Controllers;
    using Xunit;

    public class BadRequestTestBuilderTests
    {
        [Fact]
        public void WithErrorMessageShouldThrowExceptionWhenResultIsNotString()
        {
            Test.AssertException<BadRequestResultAssertionException>(
                () =>
                {
                    var requestModelWithErrors = TestObjectFactory.GetRequestModelWithErrors();

                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.BadRequestWithModelState(requestModelWithErrors))
                        .ShouldReturn()
                        .BadRequest(badRequest => badRequest
                            .WithErrorMessage("Good request"));
                },
                "When calling BadRequestWithModelState action in MvcController expected bad request result with error message, but instead received non-string value.");
        }
        
        [Fact]
        public void WithModelStateShouldNotThrowExceptionWhenModelStateHasDefaultErrors()
        {
            var requestModelWithErrors = TestObjectFactory.GetRequestModelWithErrors();

            MyController<MvcController>
                .Instance()
                .Calling(c => c.BadRequestWithModelState(requestModelWithErrors))
                .ShouldReturn()
                .BadRequest(badRequest => badRequest
                    .WithModelStateError());
        }

        [Fact]
        public void WithModelStateShouldNotThrowExceptionWhenModelStateHasSameErrors()
        {
            var requestModelWithErrors = TestObjectFactory.GetRequestModelWithErrors();
            var modelState = new ModelStateDictionary();
            modelState.AddModelError("Integer", "The field Integer must be between 1 and 2147483647.");
            modelState.AddModelError("RequiredString", "The RequiredString field is required.");

            MyController<MvcController>
                .Instance()
                .Calling(c => c.BadRequestWithModelState(requestModelWithErrors))
                .ShouldReturn()
                .BadRequest(badRequest => badRequest
                    .WithModelStateError(modelState));
        }

        [Fact]
        public void WithModelStateShouldThrowExceptionWhenModelStateHasDifferentKeys()
        {
            Test.AssertException<BadRequestResultAssertionException>(
                () =>
                {
                    var requestModelWithErrors = TestObjectFactory.GetRequestModelWithErrors();
                    var modelState = new ModelStateDictionary();
                    modelState.AddModelError("Integer", "The field Integer must be between 1 and 2147483647.");
                    modelState.AddModelError("String", "The RequiredString field is required.");

                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.BadRequestWithModelState(requestModelWithErrors))
                        .ShouldReturn()
                        .BadRequest(badRequest => badRequest
                            .WithModelStateError(modelState));
                },
                "When calling BadRequestWithModelState action in MvcController expected bad request result model state dictionary to contain 'String' key, but it was not found.");
        }

        [Fact]
        public void WithModelStateShouldThrowExceptionWhenErrorIsNotModelState()
        {
            Test.AssertException<BadRequestResultAssertionException>(
                () =>
                {
                    var modelState = new ModelStateDictionary();

                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.BadRequestWithErrorAction())
                        .ShouldReturn()
                        .BadRequest(badRequest => badRequest
                            .WithModelStateError(modelState));
                },
                "When calling BadRequestWithErrorAction action in MvcController expected bad request result with model state dictionary as error, but instead received other type of error.");
        }

        [Fact]
        public void WithModelStateShouldThrowExceptionWhenModelStateHasLessNumberOfKeys()
        {
            Test.AssertException<BadRequestResultAssertionException>(
                () =>
                {
                    var requestModelWithErrors = TestObjectFactory.GetRequestModelWithErrors();
                    var modelState = new ModelStateDictionary();
                    modelState.AddModelError("Integer", "The field Integer must be between 1 and 2147483647.");

                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.BadRequestWithModelState(requestModelWithErrors))
                        .ShouldReturn()
                        .BadRequest(badRequest => badRequest
                            .WithModelStateError(modelState));
                },
                "When calling BadRequestWithModelState action in MvcController expected bad request result model state dictionary to contain 1 keys, but instead found 2.");
        }

        [Fact]
        public void WithModelStateShouldThrowExceptionWhenModelStateHasMoreNumberOfKeys()
        {
            Test.AssertException<BadRequestResultAssertionException>(
                () =>
                {
                    var requestModelWithErrors = TestObjectFactory.GetRequestModelWithErrors();
                    var modelState = new ModelStateDictionary();
                    modelState.AddModelError("Integer", "The field Integer must be between 1 and 2147483647.");
                    modelState.AddModelError("RequiredString", "The RequiredString field is required.");
                    modelState.AddModelError("NonRequiredString", "The NonRequiredString field is required.");

                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.BadRequestWithModelState(requestModelWithErrors))
                        .ShouldReturn()
                        .BadRequest(badRequest => badRequest
                            .WithModelStateError(modelState));
                },
                "When calling BadRequestWithModelState action in MvcController expected bad request result model state dictionary to contain 3 keys, but instead found 2.");
        }

        [Fact]
        public void WithModelStateShouldThrowExceptionWhenModelStateHasWrongError()
        {
            Test.AssertException<BadRequestResultAssertionException>(
                () =>
                {
                    var requestModelWithErrors = TestObjectFactory.GetRequestModelWithErrors();
                    var modelState = new ModelStateDictionary();
                    modelState.AddModelError("Integer", "The field Integer must be between 1 and 2147483647.");
                    modelState.AddModelError("RequiredString", "The RequiredString field is not required.");

                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.BadRequestWithModelState(requestModelWithErrors))
                        .ShouldReturn()
                        .BadRequest(badRequest => badRequest
                            .WithModelStateError(modelState));
                },
                "When calling BadRequestWithModelState action in MvcController expected bad request result with message 'The RequiredString field is not required.', but instead received 'The RequiredString field is required.'.");
        }

        [Fact]
        public void WithModelStateShouldThrowExceptionWhenModelStateHasMoreErrors()
        {
            Test.AssertException<BadRequestResultAssertionException>(
                () =>
                {
                    var requestModelWithErrors = TestObjectFactory.GetRequestModelWithErrors();
                    var modelState = new ModelStateDictionary();
                    modelState.AddModelError("Integer", "The field Integer must be between 1 and 2147483647.");
                    modelState.AddModelError("RequiredString", "The RequiredString field is not required.");
                    modelState.AddModelError("RequiredString", "The RequiredString field is required.");

                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.BadRequestWithModelState(requestModelWithErrors))
                        .ShouldReturn()
                        .BadRequest(badRequest => badRequest
                            .WithModelStateError(modelState));
                },
                "When calling BadRequestWithModelState action in MvcController expected bad request result model state dictionary to contain 2 errors for the 'RequiredString' key, but instead found 1.");
        }
    }
}
