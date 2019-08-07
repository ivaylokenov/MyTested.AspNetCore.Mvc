namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.ActionsTests.ShouldHaveTests
{
    using Exceptions;
    using Setups;
    using Setups.Controllers;
    using System.Collections.Generic;
    using Xunit;

    public class ShouldHaveTempDataTests
    {
        [Fact]
        public void NoTempDataShouldNotThrowExceptionWithNoEntries()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.Ok())
                .ShouldHave()
                .NoTempData()
                .AndAlso()
                .ShouldReturn()
                .Ok();
        }

        [Fact]
        public void NoTempDataShouldThrowExceptionWithEntries()
        {
            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyController<MvcController>
                       .Instance()
                       .Calling(c => c.AddTempDataAction())
                       .ShouldHave()
                       .NoTempData()
                       .AndAlso()
                       .ShouldReturn()
                       .Ok();
                },
                "When calling AddTempDataAction action in MvcController expected to have temp data with no entries, but in fact it had some.");
        }

        [Fact]
        public void TempDataWithNoNumberShouldNotThrowExceptionWithAnyEntries()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.AddTempDataAction())
                .ShouldHave()
                .TempData()
                .AndAlso()
                .ShouldReturn()
                .Ok();
        }

        [Fact]
        public void TempDataWithNoNumberShouldThrowExceptionWithNoEntries()
        {
            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.Ok())
                        .ShouldHave()
                        .TempData()
                        .AndAlso()
                        .ShouldReturn()
                        .Ok();
                },
                "When calling Ok action in MvcController expected to have temp data entries, but none were found.");
        }

        [Fact]
        public void TempDataWithNumberShouldNotThrowExceptionWithCorrectEntries()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.AddTempDataAction())
                .ShouldHave()
                .TempData(withNumberOfEntries: 2)
                .AndAlso()
                .ShouldReturn()
                .Ok();
        }

        [Fact]
        public void TempDataWithNumberShouldThrowExceptionWithInvalidEntries()
        {
            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.Ok())
                        .ShouldHave()
                        .TempData(1)
                        .AndAlso()
                        .ShouldReturn()
                        .Ok();
                },
                "When calling Ok action in MvcController expected to have temp data with 1 entry, but in fact contained 0.");
        }

        [Fact]
        public void TempDataWithNumberShouldThrowExceptionWithInvalidManyEntries()
        {
            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.AddTempDataAction())
                        .ShouldHave()
                        .TempData(3)
                        .AndAlso()
                        .ShouldReturn()
                        .Ok();
                },
                "When calling AddTempDataAction action in MvcController expected to have temp data with 3 entries, but in fact contained 2.");
        }

        [Fact]
        public void TempDataWithBuilderShouldNotThrowWithCorrectEntry()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.AddTempDataAction())
                .ShouldHave()
                .TempData(tempData => tempData
                    .ContainingEntry("Test", "TempValue"))
                .AndAlso()
                .ShouldReturn()
                .Ok();
        }

        [Fact]
        public void TempDataWithBuilderShouldThrowWithIncorrectEntryKey()
        {
            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.AddTempDataAction())
                        .ShouldHave()
                        .TempData(tempData => tempData
                            .ContainingEntry("Invalid", "TempValue"))
                        .AndAlso()
                        .ShouldReturn()
                        .Ok();
                },
                "When calling AddTempDataAction action in MvcController expected temp data to have entry with 'Invalid' key and the provided value, but such was not found.");
        }

        [Fact]
        public void TempDataWithBuilderShouldThrowWithIncorrectEntryValue()
        {
            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.AddTempDataAction())
                        .ShouldHave()
                        .TempData(tempData => tempData
                            .ContainingEntry("Test", "Invalid"))
                        .AndAlso()
                        .ShouldReturn()
                        .Ok();
                },
                "When calling AddTempDataAction action in MvcController expected temp data to have entry with 'Test' key and the provided value, but the value was different.");
        }

        [Fact]
        public void TempDataWithBuilderShouldWorkCorrectlyWithAnonymousObjectOfTempDataEntries()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.AddTempDataAction())
                .ShouldHave()
                .TempData(tempData => tempData
                    .ContainingEntries(new
                    {
                        Test = "TempValue",
                        Another = "AnotherValue"
                    }))
                .AndAlso()
                .ShouldReturn()
                .Ok();
        }

        [Fact]
        public void TempDataWithBuilderShouldThrowWithAnonymousObjectOfTempDataEntriesWithIncorrectCount()
        {
            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.AddTempDataAction())
                        .ShouldHave()
                        .TempData(tempData => tempData
                            .ContainingEntries(new
                            {
                                Test = "TempValue"
                            }))
                        .AndAlso()
                        .ShouldReturn()
                        .Ok();
                },
                "When calling AddTempDataAction action in MvcController expected temp data to have 1 entry, but in fact found 2.");
        }

        [Fact]
        public void TempDataWithBuilderShouldThrowWithAnonymousObjectOfTempDataEntriesWithInvalidValue()
        {
            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c =>c.AddTempDataAction())
                        .ShouldHave()
                        .TempData(tempData => tempData
                            .ContainingEntries(new Dictionary<string, object>
                            {
                                ["Test"] = "TempValue",
                                ["Another"] = "Invalid"
                            }))
                        .AndAlso()
                        .ShouldReturn()
                        .Ok();
                },
                "When calling AddTempDataAction action in MvcController expected temp data to have entry with 'Another' key and the provided value, but the value was different.");
        }

        [Fact]
        public void TempDataWithBuilderShouldThrowWithWithAnonymousObjectOfTempDataEntriesWithInvalidKey()
        {
            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.AddTempDataAction())
                        .ShouldHave()
                        .TempData(tempData => tempData
                            .ContainingEntries(new Dictionary<string, object>
                            {
                                ["Test"] = "TempValue",
                                ["Invalid"] = "AnotherValue"
                            }))
                        .AndAlso()
                        .ShouldReturn()
                        .Ok();
                },
                "When calling AddTempDataAction action in MvcController expected temp data to have entry with 'Invalid' key and the provided value, but such was not found.");
        }

        [Fact]
        public void TempDataWithBuilderShouldWorkCorrectlyWithDictionaryOfTempDataEntries()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.AddTempDataAction())
                .ShouldHave()
                .TempData(tempData => tempData
                    .ContainingEntries(new Dictionary<string, object>
                    {
                        ["Test"] = "TempValue",
                        ["Another"] = "AnotherValue"
                    }))
                .AndAlso()
                .ShouldReturn()
                .Ok();
        }

        [Fact]
        public void TempDataWithBuilderShouldThrowWithDictionaryOfTempDataEntriesWithIncorrectCount()
        {
            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.AddTempDataAction())
                        .ShouldHave()
                        .TempData(tempData => tempData
                            .ContainingEntries(new Dictionary<string, object>
                            {
                                ["Test"] = "TempValue"
                            }))
                        .AndAlso()
                        .ShouldReturn()
                        .Ok();
                },
                "When calling AddTempDataAction action in MvcController expected temp data to have 1 entry, but in fact found 2.");
        }

        [Fact]
        public void TempDataWithBuilderShouldThrowWithDictionaryOfTempDataEntriesWithInvalidValue()
        {
            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.AddTempDataAction())
                        .ShouldHave()
                        .TempData(tempData => tempData
                            .ContainingEntries(new Dictionary<string, object>
                            {
                                ["Test"] = "TempValue",
                                ["Another"] = "Invalid"
                            }))
                        .AndAlso()
                        .ShouldReturn()
                        .Ok();
                },
                "When calling AddTempDataAction action in MvcController expected temp data to have entry with 'Another' key and the provided value, but the value was different.");
        }

        [Fact]
        public void TempDataWithBuilderShouldThrowWithDictionaryOfTempDataEntriesWithInvalidKey()
        {
            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.AddTempDataAction())
                        .ShouldHave()
                        .TempData(tempData => tempData
                            .ContainingEntries(new Dictionary<string, object>
                            {
                                ["Test"] = "TempValue",
                                ["Invalid"] = "AnotherValue"
                            }))
                        .AndAlso()
                        .ShouldReturn()
                        .Ok();
                },
                "When calling AddTempDataAction action in MvcController expected temp data to have entry with 'Invalid' key and the provided value, but such was not found.");
        }

        [Fact]
        public void TempDataWithBuilderWithPredicateShouldWorkWithCorrectPassingAssertions()
        {
            MyController<MvcController>
                        .Instance()
                        .Calling(c => c.AddTempDataAction())
                        .ShouldHave()
                        .TempData(tempData => tempData
                            .ContainingEntry(entry => entry
                                .WithKey("Test")
                                .WithValueOfType<string>()
                                .Passing(v => Assert.StartsWith("Temp", v))))
                        .AndAlso()
                        .ShouldReturn()
                        .Ok();
        }

        [Fact]
        public void TempDataWithBuilderWithPredicateShouldWorkWithCorrectPassingPredicate()
        {
            MyController<MvcController>
                        .Instance()
                        .Calling(c => c.AddTempDataAction())
                        .ShouldHave()
                        .TempData(tempData => tempData
                            .ContainingEntry(entry => entry
                                .WithKey("Test")
                                .WithValueOfType<string>()
                                .Passing(v => v.StartsWith("Temp"))))
                        .AndAlso()
                        .ShouldReturn()
                        .Ok();
        }

        [Fact]
        public void TempDataWithBuilderWithPredicateShouldThrowWithIncorrectPassingPredicate()
        {
            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.AddTempDataAction())
                        .ShouldHave()
                        .TempData(tempData => tempData
                            .ContainingEntry(entry => entry
                                .WithKey("Test")
                                .WithValueOfType<string>()
                                .Passing(v => v.StartsWith("Inv"))))
                        .AndAlso()
                        .ShouldReturn()
                        .Ok();
                },
                "When calling AddTempDataAction action in MvcController expected temp data to have entry with 'Test' key and value passing the given predicate, but it failed.");
        }

        [Fact]
        public void ContainingEntryOfTypeShouldNotThrowWithCorrectEntry()
        {
            MyController<MvcController>
                        .Instance()
                        .Calling(c => c.AddTempDataAction())
                        .ShouldHave()
                        .TempData(tempData => tempData.ContainingEntryOfType<string>())
                        .AndAlso()
                        .ShouldReturn()
                        .Ok();
        }

        [Fact]
        public void ContainingEntryOfTypeShouldThrowWithIncorrectEntry()
        {
            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.AddTempDataAction())
                        .ShouldHave()
                        .TempData(tempData => tempData.ContainingEntryOfType<int>())
                        .AndAlso()
                        .ShouldReturn()
                        .Ok();
                },
                "When calling AddTempDataAction action in MvcController expected temp data to have at least one entry of Int32 type, but none was found.");
        }

        [Fact]
        public void ContainingEntryOfTypeAndKeyShouldNotThrowWithCorrectEntry()
        {
            MyController<MvcController>
                        .Instance()
                        .Calling(c => c.AddTempDataAction())
                        .ShouldHave()
                        .TempData(tempData => tempData.ContainingEntryOfType<string>("Test"))
                        .AndAlso()
                        .ShouldReturn()
                        .Ok();
        }

        [Fact]
        public void ContainingEntryOfTypeAndKeyShouldThrowWithIncorrectEntryKey()
        {
            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.AddTempDataAction())
                        .ShouldHave()
                        .TempData(tempData => tempData.ContainingEntryOfType<string>("Invalid"))
                        .AndAlso()
                        .ShouldReturn()
                        .Ok();
                },
                "When calling AddTempDataAction action in MvcController expected temp data to have entry with 'Invalid' key and value of String type, but such was not found.");
        }

        [Fact]
        public void ContainingEntryOfTypeAndKeyShouldThrowWithIncorrectEntry()
        {
            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.AddTempDataAction())
                        .ShouldHave()
                        .TempData(tempData => tempData.ContainingEntryOfType<int>("Test"))
                        .AndAlso()
                        .ShouldReturn()
                        .Ok();
                },
                "When calling AddTempDataAction action in MvcController expected temp data to have entry with 'Test' key and value of Int32 type, but in fact found String.");
        }

        [Fact]
        public void ContainingEntryWithValueShouldNotThrowWithCorrectEntry()
        {
            MyController<MvcController>
                        .Instance()
                        .Calling(c => c.AddTempDataAction())
                        .ShouldHave()
                        .TempData(tempData => tempData.ContainingEntryWithValue("TempValue"))
                        .AndAlso()
                        .ShouldReturn()
                        .Ok();
        }

        [Fact]
        public void ContainingEntryWithValueShouldThrowWithIncorrectEntry()
        {
            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.AddTempDataAction())
                        .ShouldHave()
                        .TempData(tempData => tempData.ContainingEntryWithValue("Invalid"))
                        .AndAlso()
                        .ShouldReturn()
                        .Ok();
                },
                "When calling AddTempDataAction action in MvcController expected temp data to have entry with the provided value, but none was found.");
        }

        [Fact]
        public void ContainingEntryWithKeyShouldNotThrowWithCorrectEntry()
        {
            MyController<MvcController>
                        .Instance()
                        .Calling(c => c.AddTempDataAction())
                        .ShouldHave()
                        .TempData(tempData => tempData
                            .ContainingEntryWithKey("Test")
                            .AndAlso()
                            .ContainingEntryWithKey("Another"))
                        .AndAlso()
                        .ShouldReturn()
                        .Ok();
        }

        [Fact]
        public void ContainingEntryWithKeyShouldThrowWithIncorrectEntry()
        {
            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.AddTempDataAction())
                        .ShouldHave()
                        .TempData(tempData => tempData.ContainingEntryWithKey("Invalid"))
                        .AndAlso()
                        .ShouldReturn()
                        .Ok();
                },
                "When calling AddTempDataAction action in MvcController expected temp data to have entry with 'Invalid' key, but such was not found.");
        }
    }
}
