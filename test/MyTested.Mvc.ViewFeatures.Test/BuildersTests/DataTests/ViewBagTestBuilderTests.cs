namespace MyTested.Mvc.Test.BuildersTests.DataTests
{
    using System.Collections.Generic;
    using Exceptions;
    using Setups;
    using Setups.Controllers;
    using Xunit;

    public class ViewBagTestBuilderTests
    {
        [Fact]
        public void ContainingEntryWithKeyShouldNotThrowExceptionWithCorrectEntry()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.AddViewBagAction())
                .ShouldHave()
                .ViewBag(viewBag => viewBag
                    .ContainingEntryWithKey("Test")
                    .AndAlso()
                    .ContainingEntryWithKey("Another"))
                .AndAlso()
                .ShouldReturn()
                .Ok();
        }

        [Fact]
        public void ContainingEntryWithKeyShouldThrowExceptionWithIncorrectEntry()
        {
            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyMvc
                       .Controller<MvcController>()
                       .Calling(c => c.AddViewBagAction())
                       .ShouldHave()
                       .ViewBag(viewBag => viewBag.ContainingEntryWithKey("Invalid"))
                       .AndAlso()
                       .ShouldReturn()
                       .Ok();
                },
                "When calling AddViewBagAction action in MvcController expected view bag to have entry with 'Invalid' key, but such was not found.");
        }

        [Fact]
        public void ContainingEntryWithValueShouldNotThrowExceptionWithCorrectEntry()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.AddViewBagAction())
                .ShouldHave()
                .ViewBag(viewBag => viewBag.ContainingEntryWithValue("BagValue"))
                .AndAlso()
                .ShouldReturn()
                .Ok();
        }

        [Fact]
        public void ContainingEntryWithValueShouldThrowExceptionWithIncorrectEntry()
        {
            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyMvc
                       .Controller<MvcController>()
                       .Calling(c => c.AddViewBagAction())
                       .ShouldHave()
                       .ViewBag(viewBag => viewBag.ContainingEntryWithValue("Invalid"))
                       .AndAlso()
                       .ShouldReturn()
                       .Ok();
                },
                "When calling AddViewBagAction action in MvcController expected view bag to have entry with the provided value, but none was found.");
        }

        [Fact]
        public void ContainingEntryOfTypeShouldNotThrowExceptionWithCorrectEntry()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.AddViewBagAction())
                .ShouldHave()
                .ViewBag(viewBag => viewBag.ContainingEntryOfType<string>())
                .AndAlso()
                .ShouldReturn()
                .Ok();
        }

        [Fact]
        public void ContainingEntryOfTypeShouldThrowExceptionWithIncorrectEntry()
        {
            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyMvc
                       .Controller<MvcController>()
                       .Calling(c => c.AddViewBagAction())
                       .ShouldHave()
                       .ViewBag(viewBag => viewBag.ContainingEntryOfType<int>())
                       .AndAlso()
                       .ShouldReturn()
                       .Ok();
                },
                "When calling AddViewBagAction action in MvcController expected view bag to have at least one entry of Int32 type, but none was found.");
        }

        [Fact]
        public void ContainingEntryOfTypeAndKeyShouldNotThrowExceptionWithCorrectEntry()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.AddViewBagAction())
                .ShouldHave()
                .ViewBag(viewBag => viewBag.ContainingEntryOfType<string>("Test"))
                .AndAlso()
                .ShouldReturn()
                .Ok();
        }

        [Fact]
        public void ContainingEntryOfTypeAndKeyShouldThrowExceptionWithIncorrectEntryKey()
        {
            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyMvc
                       .Controller<MvcController>()
                       .Calling(c => c.AddViewBagAction())
                       .ShouldHave()
                       .ViewBag(viewBag => viewBag.ContainingEntryOfType<string>("Invalid"))
                       .AndAlso()
                       .ShouldReturn()
                       .Ok();
                },
                "When calling AddViewBagAction action in MvcController expected view bag to have entry with 'Invalid' key and value of String type, but such was not found.");
        }

        [Fact]
        public void ContainingEntryOfTypeAndKeyShouldThrowExceptionWithIncorrectEntry()
        {
            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyMvc
                       .Controller<MvcController>()
                       .Calling(c => c.AddViewBagAction())
                       .ShouldHave()
                       .ViewBag(viewBag => viewBag.ContainingEntryOfType<int>("Test"))
                       .AndAlso()
                       .ShouldReturn()
                       .Ok();
                },
                "When calling AddViewBagAction action in MvcController expected view bag to have entry with 'Test' key and value of Int32 type, but in fact found String.");
        }

        [Fact]
        public void ContainingEntryShouldNotThrowExceptionWithCorrectEntry()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.AddViewBagAction())
                .ShouldHave()
                .ViewBag(viewBag => viewBag.ContainingEntry("Test", "BagValue"))
                .AndAlso()
                .ShouldReturn()
                .Ok();
        }

        [Fact]
        public void ContainingEntryShouldThrowExceptionWithIncorrectEntryKey()
        {
            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyMvc
                       .Controller<MvcController>()
                       .Calling(c => c.AddViewBagAction())
                       .ShouldHave()
                       .ViewBag(viewBag => viewBag.ContainingEntry("Invalid", "BagValue"))
                       .AndAlso()
                       .ShouldReturn()
                       .Ok();
                },
                "When calling AddViewBagAction action in MvcController expected view bag to have entry with 'Invalid' key and the provided value, but such was not found.");
        }

        [Fact]
        public void ContainingEntryShouldThrowExceptionWithIncorrectEntry()
        {
            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyMvc
                       .Controller<MvcController>()
                       .Calling(c => c.AddViewBagAction())
                       .ShouldHave()
                       .ViewBag(viewBag => viewBag.ContainingEntry("Test", "Invalid"))
                       .AndAlso()
                       .ShouldReturn()
                       .Ok();
                },
                "When calling AddViewBagAction action in MvcController expected view bag to have entry with 'Test' key and the provided value, but the value was different.");
        }

        [Fact]
        public void ContainingEntriesShouldNotThrowExceptionWithCorrectValues()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.AddViewBagAction())
                .ShouldHave()
                .ViewBag(viewBag => viewBag.ContainingEntries(new Dictionary<string, object>
                {
                    ["Test"] = "BagValue",
                    ["Another"] = "AnotherValue"
                }))
                .AndAlso()
                .ShouldReturn()
                .Ok();
        }

        [Fact]
        public void ContainingEntriesWithObjectShouldNotThrowExceptionWithCorrectValues()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.AddViewBagAction())
                .ShouldHave()
                .ViewBag(viewBag => viewBag.ContainingEntries(new
                {
                    Test = "BagValue",
                    Another = "AnotherValue"
                }))
                .AndAlso()
                .ShouldReturn()
                .Ok();
        }

        [Fact]
        public void ContainingEntriesShouldThrowExceptionWithIncorrectCount()
        {
            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.AddViewBagAction())
                        .ShouldHave()
                        .ViewBag(viewBag => viewBag.ContainingEntries(new Dictionary<string, object>
                        {
                            ["Test"] = "BagValue",
                        }))
                        .AndAlso()
                        .ShouldReturn()
                        .Ok();
                },
                "When calling AddViewBagAction action in MvcController expected view bag to have 1 entry, but in fact found 2.");
        }

        [Fact]
        public void ContainingEntriesShouldThrowExceptionWithIncorrectManyCount()
        {
            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.AddViewBagAction())
                        .ShouldHave()
                        .ViewBag(viewBag => viewBag.ContainingEntries(new Dictionary<string, object>
                        {
                            ["Test"] = "BagValue",
                            ["Another"] = "AnotherValue",
                            ["Third"] = "AnotherThirdValue"
                        }))
                        .AndAlso()
                        .ShouldReturn()
                        .Ok();
                },
                "When calling AddViewBagAction action in MvcController expected view bag to have 3 entries, but in fact found 2.");
        }

        [Fact]
        public void ContainingEntriesShouldThrowExceptionWithInvalidKey()
        {
            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.AddViewBagAction())
                        .ShouldHave()
                        .ViewBag(viewBag => viewBag.ContainingEntries(new Dictionary<string, object>
                        {
                            ["Test"] = "BagValue",
                            ["Invalid"] = "AnotherValue"
                        }))
                        .AndAlso()
                        .ShouldReturn()
                        .Ok();
                },
                "When calling AddViewBagAction action in MvcController expected view bag to have entry with 'Invalid' key and the provided value, but such was not found.");
        }

        [Fact]
        public void ContainingEntriesShouldThrowExceptionWithInvalidValue()
        {
            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.AddViewBagAction())
                        .ShouldHave()
                        .ViewBag(viewBag => viewBag.ContainingEntries(new Dictionary<string, object>
                        {
                            ["Test"] = "Value",
                            ["Another"] = "Invalid"
                        }))
                        .AndAlso()
                        .ShouldReturn()
                        .Ok();
                },
                "When calling AddViewBagAction action in MvcController expected view bag to have entry with 'Test' key and the provided value, but the value was different.");
        }
    }
}
