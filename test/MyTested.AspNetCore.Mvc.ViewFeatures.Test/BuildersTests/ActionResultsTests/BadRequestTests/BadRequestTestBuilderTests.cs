namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.ActionResultsTests.BadRequestTests
{
    using Exceptions;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Setups;
    using Setups.Controllers;
    using Setups.Models;
    using Xunit;

    public class BadRequestTestBuilderTests
    {
        [Fact]
        public void WithModelStateForShouldNotThrowExceptionWhenModelStateHasSameErrors()
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
                    .WithModelStateError(modelStateError => modelStateError
                        .For<RequestModel>()
                        .ContainingErrorFor(m => m.Integer).ThatEquals("The field Integer must be between 1 and 2147483647.")
                        .AndAlso()
                        .ContainingErrorFor(m => m.RequiredString).BeginningWith("The RequiredString")
                        .AndAlso()
                        .ContainingErrorFor(m => m.RequiredString).EndingWith("required.")
                        .AndAlso()
                        .ContainingErrorFor(m => m.RequiredString).Containing("field")
                        .AndAlso()
                        .ContainingNoErrorFor(m => m.NonRequiredString)));
        }
        
        [Fact]
        public void WithModelStateForShouldThrowExceptionWhenModelStateHasSameErrors()
        {
            Test.AssertException<ModelErrorAssertionException>(
                () =>
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
                            .WithModelStateError(modelStateError => modelStateError
                                .For<RequestModel>()
                                .ContainingErrorFor(m => m.Integer).ThatEquals("The field Integer 1 must be between 1 and 2147483647.")
                                .AndAlso()
                                .ContainingErrorFor(m => m.RequiredString).BeginningWith("The RequiredString")
                                .AndAlso()
                                .ContainingErrorFor(m => m.RequiredString).EndingWith("required.")
                                .AndAlso()
                                .ContainingErrorFor(m => m.RequiredString).Containing("field")
                                .AndAlso()
                                .ContainingNoErrorFor(m => m.NonRequiredString)));
                },
                "When calling BadRequestWithModelState action in MvcController expected error message for key Integer to be 'The field Integer 1 must be between 1 and 2147483647.', but instead found 'The field Integer must be between 1 and 2147483647.'.");
        }
    }
}
