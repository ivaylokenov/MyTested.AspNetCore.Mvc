namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.ModelsTests
{
    using Setups;
    using Setups.Controllers;
    using Xunit;
    using System.Collections.Generic;

    public class ModelStateBuilderTests
    {
        [Fact]
        public void WithModelStateWithErrorShouldWorkCorrectly()
        {
            var requestBody = TestObjectFactory.GetValidRequestModel();

            MyController<MvcController>
                .Instance()
                .WithModelState(modelState => modelState
                    .WithError("TestError", "Invalid value"))
                .Calling(c => c.BadRequestWithModelState(requestBody))
                .ShouldReturn()
                .BadRequest();
        }

        [Fact]
        public void WithModelStateWithErrorsDictionaryShouldWorkCorrectly()
        {
            var requestBody = TestObjectFactory.GetValidRequestModel();
            var errorsDictionary = new Dictionary<string, string>()
            {
                ["First"] = "SomeError",
                ["Second"] = "AnotherError",
            };

            MyController<MvcController>
                .Instance()
                .WithModelState(modelState => modelState
                    .WithErrors(errorsDictionary))
                .Calling(c => c.BadRequestWithModelState(requestBody))
                .ShouldReturn()
                .BadRequest();
        }

        [Fact]
        public void WithModelStateWithErrorsObjectShouldWorkCorrectly()
        {
            var requestBody = TestObjectFactory.GetValidRequestModel();
            var errorsObjcet = new
            {
                First = "SomeError",
                Second = "AnotherError",
            };

            MyController<MvcController>
                .Instance()
                .WithModelState(modelState => modelState
                    .WithErrors(errorsObjcet))
                .Calling(c => c.BadRequestWithModelState(requestBody))
                .ShouldReturn()
                .BadRequest();
        }
    }
}
