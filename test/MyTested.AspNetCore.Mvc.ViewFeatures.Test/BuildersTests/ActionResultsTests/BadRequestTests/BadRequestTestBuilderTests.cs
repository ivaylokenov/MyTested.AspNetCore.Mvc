namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.ActionResultsTests.BadRequestTests
{
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
                .BadRequest()
                .WithModelStateErrorFor<RequestModel>()
                .ContainingErrorFor(m => m.Integer).ThatEquals("The field Integer must be between 1 and 2147483647.")
                .AndAlso()
                .ContainingErrorFor(m => m.RequiredString).BeginningWith("The RequiredString")
                .AndAlso()
                .ContainingErrorFor(m => m.RequiredString).EndingWith("required.")
                .AndAlso()
                .ContainingErrorFor(m => m.RequiredString).Containing("field")
                .AndAlso()
                .ContainingNoErrorFor(m => m.NonRequiredString);
        }
    }
}
