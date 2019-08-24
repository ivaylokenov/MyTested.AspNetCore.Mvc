namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.InvocationsTests.ShouldHaveTests
{
    using System.Collections.Generic;
    using Exceptions;
    using Setups;
    using Setups.ViewComponents;
    using Xunit;

    public class ShouldHaveTempDataTests
    {
        [Fact]
        public void NoTempDataShouldNotThrowExceptionWithNoEntries()
        {
            MyViewComponent<NormalComponent>
                .InvokedWith(c => c.Invoke())
                .ShouldHave()
                .NoTempData()
                .AndAlso()
                .ShouldReturn()
                .Content();
        }

        [Fact]
        public void NoTempDataShouldThrowExceptionWithEntries()
        {
            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyViewComponent<AddTempDataComponent>
                       .InvokedWith(c => c.Invoke())
                       .ShouldHave()
                       .NoTempData()
                       .AndAlso()
                       .ShouldReturn()
                       .View();
                },
                "When invoking AddTempDataComponent expected to have temp data with no entries, but in fact it had some.");
        }

        [Fact]
        public void TempDataWithNoNumberShouldNotThrowExceptionWithAnyEntries()
        {
            MyViewComponent<AddTempDataComponent>
                .InvokedWith(c => c.Invoke())
                .ShouldHave()
                .TempData()
                .AndAlso()
                .ShouldReturn()
                .View();
        }

        [Fact]
        public void TempDataWithNoNumberShouldThrowExceptionWithNoEntries()
        {
            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyViewComponent<NormalComponent>
                        .InvokedWith(c => c.Invoke())
                        .ShouldHave()
                        .TempData()
                        .AndAlso()
                        .ShouldReturn()
                        .View();
                },
                "When invoking NormalComponent expected to have temp data entries, but none were found.");
        }

        [Fact]
        public void TempDataWithNumberShouldNotThrowExceptionWithCorrectEntries()
        {
            MyViewComponent<AddTempDataComponent>
                .InvokedWith(c => c.Invoke())
                .ShouldHave()
                .TempData(withNumberOfEntries: 2)
                .AndAlso()
                .ShouldReturn()
                .View();
        }

        [Fact]
        public void TempDataWithNumberShouldThrowExceptionWithInvalidEntries()
        {
            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyViewComponent<NormalComponent>
                        .InvokedWith(c => c.Invoke())
                        .ShouldHave()
                        .TempData(1)
                        .AndAlso()
                        .ShouldReturn()
                        .View();
                },
                "When invoking NormalComponent expected to have temp data with 1 entry, but in fact contained 0.");
        }

        [Fact]
        public void TempDataWithNumberShouldThrowExceptionWithInvalidManyEntries()
        {
            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyViewComponent<AddTempDataComponent>
                        .InvokedWith(c => c.Invoke())
                        .ShouldHave()
                        .TempData(3)
                        .AndAlso()
                        .ShouldReturn()
                        .View();
                },
                "When invoking AddTempDataComponent expected to have temp data with 3 entries, but in fact contained 2.");
        }

        [Fact]
        public void TempDataWithBuilderShouldNotThrowWithCorrectEntry()
        {
            MyViewComponent<AddTempDataComponent>
                .InvokedWith(c => c.Invoke())
                .ShouldHave()
                .TempData(tempData => tempData
                    .ContainingEntry("Test", "TempValue"))
                .AndAlso()
                .ShouldReturn()
                .View();
        }

        [Fact]
        public void TempDataWithBuilderShouldThrowWithIncorrectEntryKey()
        {
            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyViewComponent<AddTempDataComponent>
                        .InvokedWith(c => c.Invoke())
                        .ShouldHave()
                        .TempData(tempData => tempData
                            .ContainingEntry("Invalid", "TempValue"))
                        .AndAlso()
                        .ShouldReturn()
                        .View();
                },
                "When invoking AddTempDataComponent expected temp data to have entry with 'Invalid' key and the provided value, but such was not found.");
        }

        [Fact]
        public void TempDataWithBuilderShouldThrowWithIncorrectEntryValue()
        {
            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyViewComponent<AddTempDataComponent>
                        .InvokedWith(c => c.Invoke())
                        .ShouldHave()
                        .TempData(tempData => tempData
                            .ContainingEntry("Test", "Invalid"))
                        .AndAlso()
                        .ShouldReturn()
                        .View();
                },
                "When invoking AddTempDataComponent expected temp data to have entry with 'Test' key and the provided value, but the value was different.");
        }

        [Fact]
        public void TempDataWithBuilderShouldWorkCorrectlyWithAnonymousObjectOfTempDataEntries()
        {
            MyViewComponent<AddTempDataComponent>
                .InvokedWith(c => c.Invoke())
                .ShouldHave()
                .TempData(tempData => tempData
                    .ContainingEntries(new
                    {
                        Test = "TempValue",
                        Another = "AnotherValue"
                    }))
                .AndAlso()
                .ShouldReturn()
                .View();
        }

        [Fact]
        public void TempDataWithBuilderShouldThrowWithAnonymousObjectOfTempDataEntriesWithIncorrectCount()
        {
            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyViewComponent<AddTempDataComponent>
                        .InvokedWith(c => c.Invoke())
                        .ShouldHave()
                        .TempData(tempData => tempData
                            .ContainingEntries(new
                            {
                                Test = "TempValue"
                            }))
                        .AndAlso()
                        .ShouldReturn()
                        .View();
                },
                "When invoking AddTempDataComponent expected temp data to have 1 entry, but in fact found 2.");
        }

        [Fact]
        public void TempDataWithBuilderShouldThrowWithAnonymousObjectOfTempDataEntriesWithInvalidValue()
        {
            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyViewComponent<AddTempDataComponent>
                        .InvokedWith(c => c.Invoke())
                        .ShouldHave()
                        .TempData(tempData => tempData
                            .ContainingEntries(new Dictionary<string, object>
                            {
                                ["Test"] = "TempValue",
                                ["Another"] = "Invalid"
                            }))
                        .AndAlso()
                        .ShouldReturn()
                        .View();
                },
                "When invoking AddTempDataComponent expected temp data to have entry with 'Another' key and the provided value, but the value was different.");
        }

        [Fact]
        public void TempDataWithBuilderShouldThrowWithWithAnonymousObjectOfTempDataEntriesWithInvalidKey()
        {
            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyViewComponent<AddTempDataComponent>
                        .InvokedWith(c => c.Invoke())
                        .ShouldHave()
                        .TempData(tempData => tempData
                            .ContainingEntries(new Dictionary<string, object>
                            {
                                ["Test"] = "TempValue",
                                ["Invalid"] = "AnotherValue"
                            }))
                        .AndAlso()
                        .ShouldReturn()
                        .View();
                },
                "When invoking AddTempDataComponent expected temp data to have entry with 'Invalid' key and the provided value, but such was not found.");
        }

        [Fact]
        public void TempDataWithBuilderShouldWorkCorrectlyWithDictionaryOfTempDataEntries()
        {
            MyViewComponent<AddTempDataComponent>
                .InvokedWith(c => c.Invoke())
                .ShouldHave()
                .TempData(tempData => tempData
                    .ContainingEntries(new Dictionary<string, object>
                    {
                        ["Test"] = "TempValue",
                        ["Another"] = "AnotherValue"
                    }))
                .AndAlso()
                .ShouldReturn()
                .View();
        }

        [Fact]
        public void TempDataWithBuilderShouldThrowWithDictionaryOfTempDataEntriesWithIncorrectCount()
        {
            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyViewComponent<AddTempDataComponent>
                        .InvokedWith(c => c.Invoke())
                        .ShouldHave()
                        .TempData(tempData => tempData
                            .ContainingEntries(new Dictionary<string, object>
                            {
                                ["Test"] = "TempValue"
                            }))
                        .AndAlso()
                        .ShouldReturn()
                        .View();
                },
                "When invoking AddTempDataComponent expected temp data to have 1 entry, but in fact found 2.");
        }

        [Fact]
        public void TempDataWithBuilderShouldThrowWithDictionaryOfTempDataEntriesWithInvalidValue()
        {
            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyViewComponent<AddTempDataComponent>
                        .InvokedWith(c => c.Invoke())
                        .ShouldHave()
                        .TempData(tempData => tempData
                            .ContainingEntries(new Dictionary<string, object>
                            {
                                ["Test"] = "TempValue",
                                ["Another"] = "Invalid"
                            }))
                        .AndAlso()
                        .ShouldReturn()
                        .View();
                },
                "When invoking AddTempDataComponent expected temp data to have entry with 'Another' key and the provided value, but the value was different.");
        }

        [Fact]
        public void TempDataWithBuilderShouldThrowWithDictionaryOfTempDataEntriesWithInvalidKey()
        {
            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyViewComponent<AddTempDataComponent>
                        .InvokedWith(c => c.Invoke())
                        .ShouldHave()
                        .TempData(tempData => tempData
                            .ContainingEntries(new Dictionary<string, object>
                            {
                                ["Test"] = "TempValue",
                                ["Invalid"] = "AnotherValue"
                            }))
                        .AndAlso()
                        .ShouldReturn()
                        .View();
                },
                "When invoking AddTempDataComponent expected temp data to have entry with 'Invalid' key and the provided value, but such was not found.");
        }

        [Fact]
        public void TempDataWithBuilderWithPredicateShouldWorkWithCorrectPassingAssertions()
        {
            MyViewComponent<AddTempDataComponent>
                .InvokedWith(c => c.Invoke())
                .ShouldHave()
                .TempData(tempData => tempData
                    .ContainingEntry(entry => entry
                        .WithKey("Test")
                        .WithValueOfType<string>()
                        .Passing(v => Assert.StartsWith("Temp", v))))
                .AndAlso()
                .ShouldReturn()
                .View();
        }

        [Fact]
        public void TempDataWithBuilderWithPredicateShouldWorkWithCorrectPassingPredicate()
        {
            MyViewComponent<AddTempDataComponent>
                .InvokedWith(c => c.Invoke())
                .ShouldHave()
                .TempData(tempData => tempData
                    .ContainingEntry(entry => entry
                    .WithKey("Test")
                    .WithValueOfType<string>()
                    .Passing(v => v.StartsWith("Temp"))))
                .AndAlso()
                .ShouldReturn()
                .View();
        }

        [Fact]
        public void TempDataWithBuilderWithPredicateShouldThrowWithIncorrectPassingPredicate()
        {
            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyViewComponent<AddTempDataComponent>
                        .InvokedWith(c => c.Invoke())
                        .ShouldHave()
                        .TempData(tempData => tempData
                            .ContainingEntry(entry => entry
                                .WithKey("Test")
                                .WithValueOfType<string>()
                                .Passing(v => v.StartsWith("Inv"))))
                        .AndAlso()
                        .ShouldReturn()
                        .View();
                },
                "When invoking AddTempDataComponent expected temp data to have entry with 'Test' key and value passing the given predicate, but it failed.");
        }

        [Fact]
        public void ContainingEntryOfTypeShouldNotThrowWithCorrectEntry()
        {
            MyViewComponent<AddTempDataComponent>
                .InvokedWith(c => c.Invoke())
                .ShouldHave()
                .TempData(tempData => tempData.ContainingEntryOfType<string>())
                .AndAlso()
                .ShouldReturn()
                .View();
        }

        [Fact]
        public void ContainingEntryOfTypeShouldThrowWithIncorrectEntry()
        {
            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyViewComponent<AddTempDataComponent>
                        .InvokedWith(c => c.Invoke())
                        .ShouldHave()
                        .TempData(tempData => tempData.ContainingEntryOfType<int>())
                        .AndAlso()
                        .ShouldReturn()
                        .View();
                },
                "When invoking AddTempDataComponent expected temp data to have at least one entry of Int32 type, but none was found.");
        }

        [Fact]
        public void ContainingEntryOfTypeAndKeyShouldNotThrowWithCorrectEntry()
        {
            MyViewComponent<AddTempDataComponent>
                .InvokedWith(c => c.Invoke())
                .ShouldHave()
                .TempData(tempData => tempData.ContainingEntryOfType<string>("Test"))
                .AndAlso()
                .ShouldReturn()
                .View();
        }

        [Fact]
        public void ContainingEntryOfTypeAndKeyShouldThrowWithIncorrectEntryKey()
        {
            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyViewComponent<AddTempDataComponent>
                        .InvokedWith(c => c.Invoke())
                        .ShouldHave()
                        .TempData(tempData => tempData.ContainingEntryOfType<string>("Invalid"))
                        .AndAlso()
                        .ShouldReturn()
                        .View();
                },
                "When invoking AddTempDataComponent expected temp data to have entry with 'Invalid' key and value of String type, but such was not found.");
        }

        [Fact]
        public void ContainingEntryOfTypeAndKeyShouldThrowWithIncorrectEntry()
        {
            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyViewComponent<AddTempDataComponent>
                        .InvokedWith(c => c.Invoke())
                        .ShouldHave()
                        .TempData(tempData => tempData.ContainingEntryOfType<int>("Test"))
                        .AndAlso()
                        .ShouldReturn()
                        .View();
                },
                "When invoking AddTempDataComponent expected temp data to have entry with 'Test' key and value of Int32 type, but in fact found String.");
        }

        [Fact]
        public void ContainingEntryWithValueShouldNotThrowWithCorrectEntry()
        {
            MyViewComponent<AddTempDataComponent>
                .InvokedWith(c => c.Invoke())
                .ShouldHave()
                .TempData(tempData => tempData.ContainingEntryWithValue("TempValue"))
                .AndAlso()
                .ShouldReturn()
                .View();
        }

        [Fact]
        public void ContainingEntryWithValueShouldThrowWithIncorrectEntry()
        {
            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyViewComponent<AddTempDataComponent>
                        .InvokedWith(c => c.Invoke())
                        .ShouldHave()
                        .TempData(tempData => tempData.ContainingEntryWithValue("Invalid"))
                        .AndAlso()
                        .ShouldReturn()
                        .View();
                },
                "When invoking AddTempDataComponent expected temp data to have entry with the provided value, but none was found.");
        }

        [Fact]
        public void ContainingEntryWithKeyShouldNotThrowWithCorrectEntry()
        {
            MyViewComponent<AddTempDataComponent>
                .InvokedWith(c => c.Invoke())
                .ShouldHave()
                .TempData(tempData => tempData
                    .ContainingEntryWithKey("Test")
                    .AndAlso()
                    .ContainingEntryWithKey("Another"))
                .AndAlso()
                .ShouldReturn()
                .View();
        }

        [Fact]
        public void ContainingEntryWithKeyShouldThrowWithIncorrectEntry()
        {
            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyViewComponent<AddTempDataComponent>
                        .InvokedWith(c => c.Invoke())
                        .ShouldHave()
                        .TempData(tempData => tempData.ContainingEntryWithKey("Invalid"))
                        .AndAlso()
                        .ShouldReturn()
                        .View();
                },
                "When invoking AddTempDataComponent expected temp data to have entry with 'Invalid' key, but such was not found.");
        }
    }
}
