namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.ActionsTests.ShouldHaveTests
{
    using Exceptions;
    using Setups;
    using Setups.Controllers;
    using Xunit;

    public class ShouldHaveViewBagTests
    {
        [Fact]
        public void NoViewBagShouldNotThrowExceptionWithNoEntries()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.Ok())
                .ShouldHave()
                .NoViewBag()
                .AndAlso()
                .ShouldReturn()
                .Ok();
        }

        [Fact]
        public void NoViewBagShouldThrowExceptionWithEntries()
        {
            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyController<MvcController>
                       .Instance()
                       .Calling(c => c.AddViewBagAction())
                       .ShouldHave()
                       .NoViewBag()
                       .AndAlso()
                       .ShouldReturn()
                       .Ok();
                },
                "When calling AddViewBagAction action in MvcController expected to have view bag with no entries, but in fact it had some.");
        }

        [Fact]
        public void ViewBagWithNoNumberShouldNotThrowExceptionWithAnyEntries()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.AddViewBagAction())
                .ShouldHave()
                .ViewBag()
                .AndAlso()
                .ShouldReturn()
                .Ok();
        }

        [Fact]
        public void ViewBagWithNoNumberShouldThrowExceptionWithNoEntries()
        {
            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.Ok())
                        .ShouldHave()
                        .ViewBag()
                        .AndAlso()
                        .ShouldReturn()
                        .Ok();
                },
                "When calling Ok action in MvcController expected to have view bag entries, but none were found.");
        }

        [Fact]
        public void ViewBagWithNumberShouldNotThrowExceptionWithCorrectEntries()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.AddViewBagAction())
                .ShouldHave()
                .ViewBag(withNumberOfEntries: 2)
                .AndAlso()
                .ShouldReturn()
                .Ok();
        }

        [Fact]
        public void ViewBagWithNumberShouldThrowExceptionWithInvalidEntries()
        {
            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.Ok())
                        .ShouldHave()
                        .ViewBag(1)
                        .AndAlso()
                        .ShouldReturn()
                        .Ok();
                },
                "When calling Ok action in MvcController expected to have view bag with 1 entry, but in fact contained 0.");
        }

        [Fact]
        public void ViewBagWithNumberShouldThrowExceptionWithInvalidManyEntries()
        {
            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.AddViewBagAction())
                        .ShouldHave()
                        .ViewBag(3)
                        .AndAlso()
                        .ShouldReturn()
                        .Ok();
                },
                "When calling AddViewBagAction action in MvcController expected to have view bag with 3 entries, but in fact contained 2.");
        }

        [Fact]
        public void ViewBagWithBuilderShouldWorkCorrectly()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.AddViewBagAction())
                .ShouldHave()
                .ViewBag(viewBag => viewBag
                    .ContainingEntry("Test", "BagValue"))
                .AndAlso()
                .ShouldReturn()
                .Ok();
        }

        [Fact]
        public void ViewBagWithBuilderShouldContainMultipleEntries()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.AddViewBagAction())
                .ShouldHave()
                .ViewBag(viewBag => viewBag
                    .ContainingEntries(new { Test = "BagValue", Another = "AnotherValue" }))
                .AndAlso()
                .ShouldReturn()
                .Ok();
        }

        [Fact]
        public void ViewBagWithBuilderShouldContainEntryWithKey()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.AddViewBagAction())
                .ShouldHave()
                .ViewBag(viewBag => viewBag
                    .ContainingEntryWithKey("Another"))
                .AndAlso()
                .ShouldReturn()
                .Ok();
        }

        [Fact]
        public void ViewBagWithBuilderShouldContainEntryOfTypeString()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.AddViewBagAction())
                .ShouldHave()
                .ViewBag(viewBag => viewBag
                    .ContainingEntryOfType<string>())
                .AndAlso()
                .ShouldReturn()
                .Ok();
        }
    }
}
