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

        [Fact]
        public void WithoutEntryShouldReturnCorrectValues()
        {
            MyController<MvcController>
                .Instance()
                .WithTempData(tempData => tempData
                    .WithEntries(new Dictionary<string, object>
                    {
                        ["Test"] = "Valid",
                        ["Second"] = "SecondValue",
                    }))
                .WithoutTempData("Test")
                .Calling(c => c.GetTempDataKeys())
                .ShouldReturn()
                .Ok(ok => ok
                   .WithModel(new List<string>
                    {
                        "Second"
                    }));
        }

        [Fact]
        public void WithoutEntriesShouldReturnCorrectValues()
        {
            MyController<MvcController>
                .Instance()
                .WithTempData(tempData => tempData
                    .WithEntries(new Dictionary<string, object>
                    {
                        ["Test"] = "Valid",
                        ["Second"] = "SecondValue",
                    }))
                .WithoutTempData(new List<string>() { "Second" })
                .Calling(c => c.GetTempDataKeys())
                .ShouldReturn()
                .Ok(ok => ok
                   .WithModel(new List<string>
                    {
                        "Test"
                    }));
        }

        [Fact]
        public void WithoutEntriesByProvidingParamsShouldReturnCorrectValues()
        {
            MyController<MvcController>
                .Instance()
                .WithTempData(tempData => tempData
                    .WithEntries(new Dictionary<string, object>
                    {
                        ["Test"] = "Valid",
                        ["Second"] = "SecondValue",
                    }))
                .WithoutTempData("Second", "Test")
                .Calling(c => c.GetTempDataKeys())
                .ShouldReturn()
                .Ok(ok => ok
                   .WithModel(new List<string>()));
        }

        [Fact]
        public void WithoutEntriesShouldRemoveAllEntities()
        {
            MyController<MvcController>
                .Instance()
                .WithTempData(tempData => tempData
                    .WithEntries(new Dictionary<string, object>
                    {
                        ["Test"] = "Valid",
                        ["Second"] = "SecondValue",
                    }))
                .WithoutTempData()
                .Calling(c => c.GetTempDataKeys())
                .ShouldReturn()
                .Ok(ok => ok
                   .WithModel(new List<string>()));
        }

        [Fact]
        public void WithoutEntryNonExistingItemShouldReturnCorrectValues()
        {
            MyController<MvcController>
                .Instance()
                .WithTempData(tempData => tempData
                    .WithEntries(new Dictionary<string, object>
                    {
                        ["Test"] = "Valid",
                        ["Second"] = "SecondValue",
                    }))
                .WithoutTempData("NonExisting")
                .Calling(c => c.GetTempDataKeys())
                .ShouldReturn()
                .Ok(ok => ok
                   .WithModel(new List<string>() { "Test" , "Second"}));
        }
    }
}
