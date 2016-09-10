namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.InvocationsTests.ShouldHaveTests
{
    using Exceptions;
    using Setups;
    using Setups.ViewComponents;
    using Xunit;

    public class ShouldHaveViewDataTests
    {
        [Fact]
        public void NoViewDataShouldNotThrowExceptionWithNoEntries()
        {
            MyViewComponent<NormalComponent>
                .Instance()
                .InvokedWith(c => c.Invoke())
                .ShouldHave()
                .NoViewData()
                .AndAlso()
                .ShouldReturn()
                .Content();
        }

        [Fact]
        public void NoViewDataShouldThrowExceptionWithEntries()
        {
            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyViewComponent<AddViewDataComponent>
                       .Instance()
                       .InvokedWith(c => c.Invoke())
                       .ShouldHave()
                       .NoViewData()
                       .AndAlso()
                       .ShouldReturn()
                       .View();
                },
                "When invoking AddViewDataComponent expected to have view data with no entries, but in fact it had some.");
        }

        [Fact]
        public void ViewDataWithNoNumberShouldNotThrowExceptionWithAnyEntries()
        {
            MyViewComponent<AddViewDataComponent>
                .Instance()
                .InvokedWith(c => c.Invoke())
                .ShouldHave()
                .ViewData()
                .AndAlso()
                .ShouldReturn()
                .View();
        }

        [Fact]
        public void ViewDataWithNoNumberShouldThrowExceptionWithNoEntries()
        {
            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyViewComponent<NormalComponent>
                        .Instance()
                        .InvokedWith(c => c.Invoke())
                        .ShouldHave()
                        .ViewData()
                        .AndAlso()
                        .ShouldReturn()
                        .View();
                },
                "When invoking NormalComponent expected to have view data entries, but none were found.");
        }

        [Fact]
        public void ViewDataWithNumberShouldNotThrowExceptionWithCorrectEntries()
        {
            MyViewComponent<AddViewDataComponent>
                .Instance()
                .InvokedWith(c => c.Invoke())
                .ShouldHave()
                .ViewData(withNumberOfEntries: 2)
                .AndAlso()
                .ShouldReturn()
                .View();
        }

        [Fact]
        public void ViewDataWithNumberShouldThrowExceptionWithInvalidEntries()
        {
            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyViewComponent<NormalComponent>
                        .Instance()
                        .InvokedWith(c => c.Invoke())
                        .ShouldHave()
                        .ViewData(1)
                        .AndAlso()
                        .ShouldReturn()
                        .View();
                },
                "When invoking NormalComponent expected to have view data with 1 entry, but in fact contained 0.");
        }

        [Fact]
        public void ViewDataWithNumberShouldThrowExceptionWithInvalidManyEntries()
        {
            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyViewComponent<AddViewDataComponent>
                        .Instance()
                        .InvokedWith(c => c.Invoke())
                        .ShouldHave()
                        .ViewData(3)
                        .AndAlso()
                        .ShouldReturn()
                        .View();
                },
                "When invoking AddViewDataComponent expected to have view data with 3 entries, but in fact contained 2.");
        }

        [Fact]
        public void ViewDataWithBuilderShouldWorkCorrectly()
        {
            MyViewComponent<AddViewDataComponent>
                .Instance()
                .InvokedWith(c => c.Invoke())
                .ShouldHave()
                .ViewData(viewData => viewData
                    .ContainingEntry("Test", "DataValue"))
                .AndAlso()
                .ShouldReturn()
                .View();
        }
    }
}
