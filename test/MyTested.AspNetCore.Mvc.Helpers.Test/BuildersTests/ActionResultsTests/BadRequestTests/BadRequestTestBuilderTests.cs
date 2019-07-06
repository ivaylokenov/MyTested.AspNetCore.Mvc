namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.ActionResultsTests.BadRequestTests
{
    using System.Collections.Generic;
    using Setups;
    using Setups.Controllers;
    using Setups.Models;
    using Xunit;

    public class BadRequestTestBuilderTests
    {
        [Fact]
        public void WithErrorShouldNotThrowExceptionWithCorrectErrorObject()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.BadRequestWithCustomError())
                .ShouldReturn()
                .BadRequest(badRequest => badRequest
                    .WithError(TestObjectFactory.GetListOfResponseModels()));
        }

        [Fact]
        public void WithErrorOfTypeShouldNotThrowExceptionWithCorrectErrorObject()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.BadRequestWithCustomError())
                .ShouldReturn()
                .BadRequest(badRequest => badRequest
                    .WithErrorOfType<List<ResponseModel>>());
        }
    }
}
