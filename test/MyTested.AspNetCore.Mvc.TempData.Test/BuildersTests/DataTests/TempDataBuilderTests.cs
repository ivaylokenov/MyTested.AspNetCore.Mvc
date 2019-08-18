namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.DataTests
{
    using System.Collections.Generic;
    using Setups.Controllers;
    using Xunit;

    public class TempDataBuilderTests
    {
        [Fact]
        public void WithEntryShouldSetCorrectValues()
        {
            MyController<MvcController>
                .Instance()
                .WithTempData(tempData => tempData
                    .WithEntry("Test", "Valid")
                    .AndAlso()
                    .WithEntry("Another", "AnotherValid"))
                .Calling(c => c.TempDataAction())
                .ShouldReturn()
                .Ok(ok => ok
                    .WithModel("Valid"));
        }
        
        [Fact]
        public void WithEntriesShouldSetCorrectValues()
        {
            MyController<MvcController>
                .Instance()
                .WithTempData(tempData => tempData
                    .WithEntries(new Dictionary<string, object>
                    {
                        ["Test"] = "Valid",
                        ["Second"] = "SecondValue",
                    }))
                .Calling(c => c.TempDataAction())
                .ShouldReturn()
                .Ok(ok => ok
                    .WithModel("Valid"));
        }
        
        [Fact]
        public void WithEntriesObjectShouldSetCorrectValues()
        {
            MyController<MvcController>
                .Instance()
                .WithTempData(tempData => tempData
                    .WithEntries(new { Test = "Valid", Second = "SecondValid" }))
                .Calling(c => c.TempDataAction())
                .ShouldReturn()
                .Ok(ok => ok
                    .WithModel("Valid"));
        }
    }
}
