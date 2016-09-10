namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.InvocationsTests.ShouldHaveTests
{
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
                .Instance()
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
                       .Instance()
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
                .Instance()
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
                        .Instance()
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
                .Instance()
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
                        .Instance()
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
                        .Instance()
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
        public void TempDataWithBuilderShouldWorkCorrectly()
        {
            MyViewComponent<AddTempDataComponent>
                .Instance()
                .InvokedWith(c => c.Invoke())
                .ShouldHave()
                .TempData(tempData => tempData
                    .ContainingEntry("Test", "TempValue"))
                .AndAlso()
                .ShouldReturn()
                .View();
        }
    }
}
