namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.ActionResultsTests.BadRequestTests
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;
    using Setups.Controllers;
    using Setups.Models;
    using Xunit;

    public class BadRequestTestBuilderTests
    {
        [Fact]
        public void ShouldPassForTheShouldWorkCorrectlyWithActionResult()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.FullBadRequestAction())
                .ShouldReturn()
                .BadRequest()
                .AndAlso()
                .ShouldPassForThe<IActionResult>(actionResult =>
                {
                    Assert.NotNull(actionResult);
                    Assert.IsAssignableFrom<BadRequestObjectResult>(actionResult);
                });
        }

        [Fact]
        public void ShouldPassForTheShouldWorkCorrectlyWithModel()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.FullBadRequestAction())
                .ShouldReturn()
                .BadRequest()
                .AndAlso()
                .ShouldPassForThe<ICollection<ResponseModel>>(model => model.Count == 2);
        }
    }
}
