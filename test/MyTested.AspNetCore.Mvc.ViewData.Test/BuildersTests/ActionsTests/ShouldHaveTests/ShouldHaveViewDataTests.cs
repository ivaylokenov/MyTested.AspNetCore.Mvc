namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.ActionsTests.ShouldHaveTests
{
    using Exceptions;
    using Setups;
    using Setups.Controllers;
    using Xunit;

    public class ShouldHaveViewDataTests
    {
        [Fact]
        public void NoViewDataShouldNotThrowExceptionWithNoEntries()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.Ok())
                .ShouldHave()
                .NoViewData()
                .AndAlso()
                .ShouldReturn()
                .Ok();
        }

        [Fact]
        public void NoViewDataShouldThrowExceptionWithEntries()
        {
            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyController<MvcController>
                       .Instance()
                       .Calling(c => c.AddViewDataAction())
                       .ShouldHave()
                       .NoViewData()
                       .AndAlso()
                       .ShouldReturn()
                       .Ok();
                },
                "When calling AddViewDataAction action in MvcController expected to have view data with no entries, but in fact it had some.");
        }

        [Fact]
        public void ViewDataWithNoNumberShouldNotThrowExceptionWithAnyEntries()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.AddViewDataAction())
                .ShouldHave()
                .ViewData()
                .AndAlso()
                .ShouldReturn()
                .Ok();
        }

        [Fact]
        public void ViewDataWithNoNumberShouldThrowExceptionWithNoEntries()
        {
            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.Ok())
                        .ShouldHave()
                        .ViewData()
                        .AndAlso()
                        .ShouldReturn()
                        .Ok();
                },
                "When calling Ok action in MvcController expected to have view data entries, but none were found.");
        }

        [Fact]
        public void ViewDataWithNumberShouldNotThrowExceptionWithCorrectEntries()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.AddViewDataAction())
                .ShouldHave()
                .ViewData(withNumberOfEntries: 2)
                .AndAlso()
                .ShouldReturn()
                .Ok();
        }

        [Fact]
        public void ViewDataWithNumberShouldThrowExceptionWithInvalidEntries()
        {
            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.Ok())
                        .ShouldHave()
                        .ViewData(1)
                        .AndAlso()
                        .ShouldReturn()
                        .Ok();
                },
                "When calling Ok action in MvcController expected to have view data with 1 entry, but in fact contained 0.");
        }

        [Fact]
        public void ViewDataWithNumberShouldThrowExceptionWithInvalidManyEntries()
        {
            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.AddViewDataAction())
                        .ShouldHave()
                        .ViewData(3)
                        .AndAlso()
                        .ShouldReturn()
                        .Ok();
                },
                "When calling AddViewDataAction action in MvcController expected to have view data with 3 entries, but in fact contained 2.");
        }

        [Fact]
        public void ViewDataWithBuilderShouldWorkCorrectly()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.AddViewDataAction())
                .ShouldHave()
                .ViewData(viewData => viewData
                    .ContainingEntry("Test", "DataValue"))
                .AndAlso()
                .ShouldReturn()
                .Ok();
        }

        [Fact]
        public void ViewDataWithBuilderShouldContainMultipleEntries()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.AddViewDataAction())
                .ShouldHave()
                .ViewData(viewData => viewData
                    .ContainingEntries(new { Test = "DataValue", Another = "AnotherValue" }))
                .AndAlso()
                .ShouldReturn()
                .Ok();
        }

        [Fact]
        public void ViewDataWithBuilderShouldContainEntryWithKey()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.AddViewDataAction())
                .ShouldHave()
                .ViewData(viewData => viewData
                    .ContainingEntryWithKey("Another"))
                .AndAlso()
                .ShouldReturn()
                .Ok();
        }

        [Fact]
        public void ViewDataWithBuilderShouldContainEntryOfTypeString()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.AddViewDataAction())
                .ShouldHave()
                .ViewData(viewData => viewData
                    .ContainingEntryOfType<string>())
                .AndAlso()
                .ShouldReturn()
                .Ok();
        }
    }
}
