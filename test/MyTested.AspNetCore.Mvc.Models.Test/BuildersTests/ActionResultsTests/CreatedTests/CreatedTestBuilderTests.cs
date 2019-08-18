namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.ActionResultsTests.CreatedTests
{
    using System.Collections.Generic;
    using Setups.Controllers;
    using Setups.Models;
    using Xunit;

    public class CreatedTestBuilderTests
    {
        [Fact]
        public void WithResponseModelOfTypeShouldWorkCorrectly()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.CreatedAction())
                .ShouldReturn()
                .Created(created => created
                    .WithModelOfType<ICollection<ResponseModel>>());
        }
    }
}
