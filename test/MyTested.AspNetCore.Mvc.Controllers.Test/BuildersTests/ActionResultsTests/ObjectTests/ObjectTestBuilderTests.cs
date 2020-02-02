namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.ActionResultsTests.ObjectTests
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;
    using Setups.Controllers;
    using Setups.Models;
    using Xunit;

    public class ObjectTestBuilderTests
    {
        [Fact]
        public void ShouldPassForTheShouldWorkCorrectlyWithActionResult()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.FullObjectResultAction())
                .ShouldReturn()
                .Object()
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
                .Object()
                .AndAlso()
                .ShouldPassForThe<ICollection<ResponseModel>>(model => model.Count == 2);
        }
    }
}
