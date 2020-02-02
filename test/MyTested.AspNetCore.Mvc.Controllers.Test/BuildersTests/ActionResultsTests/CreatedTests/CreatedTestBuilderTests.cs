namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.ActionResultsTests.CreatedTests
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;
    using Setups.Controllers;
    using Setups.Models;
    using Xunit;

    public class CreatedTestBuilderTests
    {
        [Fact]
        public void ShouldPassForTheShouldWorkCorrectlyWithActionResult()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.CreatedAction())
                .ShouldReturn()
                .Created()
                .ShouldPassForThe<ActionResult>(actionResult =>
                {
                    Assert.NotNull(actionResult);
                    Assert.IsAssignableFrom<CreatedResult>(actionResult);
                });
        }

        [Fact]
        public void ShouldPassForTheShouldWorkCorrectlyWithModel()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.CreatedAction())
                .ShouldReturn()
                .Created()
                .ShouldPassForThe<ICollection<ResponseModel>>(model => model.Count == 2);
        }
    }
}
