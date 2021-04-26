namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.ActionResultsTests.OkTests
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;
    using Setups.Controllers;
    using Setups.Models;
    using Xunit;

    public class OkTestBuilderTests
    {
        [Fact]
        public void ShouldPassForTheShouldWorkCorrectlyWithActionResult()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.FullOkAction())
                .ShouldReturn()
                .Ok()
                .AndAlso()
                .ShouldPassForThe<IActionResult>(actionResult =>
                {
                    Assert.NotNull(actionResult);
                    Assert.IsAssignableFrom<OkObjectResult>(actionResult);
                });
        }

        [Fact]
        public void ShouldPassForTheShouldWorkCorrectlyWithModel()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.FullOkAction())
                .ShouldReturn()
                .Ok()
                .AndAlso()
                .ShouldPassForThe<ICollection<ResponseModel>>(model => model.Count == 2);
        }
    }
}
