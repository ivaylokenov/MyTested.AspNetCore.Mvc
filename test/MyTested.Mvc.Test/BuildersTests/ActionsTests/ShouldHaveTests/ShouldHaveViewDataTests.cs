namespace MyTested.Mvc.Test.BuildersTests.ActionsTests.ShouldHaveTests
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
            MyMvc
                .Controller<MvcController>()
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
                    MyMvc
                       .Controller<MvcController>()
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
            MyMvc
                .Controller<MvcController>()
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
                    MyMvc
                        .Controller<MvcController>()
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
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.AddViewDataAction())
                .ShouldHave()
                .ViewData(withNumberOfEntries: 1)
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
                    MyMvc
                        .Controller<MvcController>()
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
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.AddViewDataAction())
                        .ShouldHave()
                        .ViewData(3)
                        .AndAlso()
                        .ShouldReturn()
                        .Ok();
                },
                "When calling AddViewDataAction action in MvcController expected to have view data with 3 entries, but in fact contained 1.");
        }

        [Fact]
        public void ViewDataWithBuilderShouldWorkCorrectly()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.AddViewDataAction())
                .ShouldHave()
                .ViewData(viewData => viewData
                    .ContainingEntry("Test", "datavalue"))
                .AndAlso()
                .ShouldReturn()
                .Ok();
        }
    }
}
