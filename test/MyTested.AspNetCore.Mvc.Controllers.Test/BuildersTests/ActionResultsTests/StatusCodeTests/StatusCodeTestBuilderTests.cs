namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.ActionResultsTests.StatusCodeTests
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;
    using Setups.Controllers;
    using Setups.Models;
    using Xunit;

    public class StatusCodeTestBuilderTests
    {
        [Fact]
        public void ShouldPassForTheShouldWorkCorrectlyWithActionResult()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.FullObjectResultAction())
                .ShouldReturn()
                .StatusCode()
                .AndAlso()
                .ShouldPassForThe<IActionResult>(actionResult =>
                {
                    Assert.NotNull(actionResult);
                    Assert.IsAssignableFrom<ObjectResult>(actionResult);
                });
        }

        [Fact]
        public void ShouldPassForTheShouldWorkCorrectlyWithModel()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.FullObjectResultAction())
                .ShouldReturn()
                .StatusCode()
                .AndAlso()
                .ShouldPassForThe<ICollection<ResponseModel>>(model => model.Count == 2);
        }
    }
}
