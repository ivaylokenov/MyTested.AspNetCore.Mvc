namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.ActionsTests.ShouldHaveTests
{
    using Exceptions;
    using Setups;
    using Setups.Controllers;
    using Xunit;

    public class ShouldHaveTempDataTests
    {
        [Fact]
        public void NoTempDataShouldNotThrowExceptionWithNoEntries()
        {
            MyMvc
                .Controller<MvcController>()
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
                    MyMvc
                       .Controller<MvcController>()
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
            MyMvc
                .Controller<MvcController>()
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
                    MyMvc
                        .Controller<MvcController>()
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
            MyMvc
                .Controller<MvcController>()
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
                    MyMvc
                        .Controller<MvcController>()
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
                    MyMvc
                        .Controller<MvcController>()
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
        public void TempDataWithBuilderShouldWorkCorrectly()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.AddTempDataAction())
                .ShouldHave()
                .TempData(tempData => tempData
                    .ContainingEntry("Test", "TempValue"))
                .AndAlso()
                .ShouldReturn()
                .Ok();
        }
    }
}
