namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.ActionResultsTests.ObjectTests
{
    using System.Collections.Generic;
    using Setups.Controllers;
    using Setups.Models;
    using Xunit;

    public class ObjectTestBuilderTests
    {
        [Fact]
        public void WithResponseModelShouldWorkCorrectly()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.ObjectResultWithResponse())
                .ShouldReturn()
                .Object()
                .WithModelOfType<List<ResponseModel>>();
        }
    }
}
