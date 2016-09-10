namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.InvocationsTests.ShouldHaveTests
{
    using Exceptions;
    using Setups;
    using Setups.ViewComponents;
    using Xunit;

    public class ShouldHaveViewBagTests
    {
        [Fact]
        public void NoViewBagShouldNotThrowExceptionWithNoEntries()
        {
            MyViewComponent<NormalComponent>
                .Instance()
                .InvokedWith(c => c.Invoke())
                .ShouldHave()
                .NoViewBag()
                .AndAlso()
                .ShouldReturn()
                .Content();
        }

        [Fact]
        public void NoViewBagShouldThrowExceptionWithEntries()
        {
            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyViewComponent<AddViewBagComponent>
                       .Instance()
                       .InvokedWith(c => c.Invoke())
                       .ShouldHave()
                       .NoViewBag()
                       .AndAlso()
                       .ShouldReturn()
                       .View();
                },
                "When invoking AddViewBagComponent expected to have view bag with no entries, but in fact it had some.");
        }

        [Fact]
        public void ViewBagWithNoNumberShouldNotThrowExceptionWithAnyEntries()
        {
            MyViewComponent<AddViewBagComponent>
                .Instance()
                .InvokedWith(c => c.Invoke())
                .ShouldHave()
                .ViewBag()
                .AndAlso()
                .ShouldReturn()
                .View();
        }

        [Fact]
        public void ViewBagWithNoNumberShouldThrowExceptionWithNoEntries()
        {
            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyViewComponent<NormalComponent>
                        .Instance()
                        .InvokedWith(c => c.Invoke())
                        .ShouldHave()
                        .ViewBag()
                        .AndAlso()
                        .ShouldReturn()
                        .View();
                },
                "When invoking NormalComponent expected to have view bag entries, but none were found.");
        }

        [Fact]
        public void ViewBagWithNumberShouldNotThrowExceptionWithCorrectEntries()
        {
            MyViewComponent<AddViewBagComponent>
                .Instance()
                .InvokedWith(c => c.Invoke())
                .ShouldHave()
                .ViewBag(withNumberOfEntries: 2)
                .AndAlso()
                .ShouldReturn()
                .View();
        }

        [Fact]
        public void ViewBagWithNumberShouldThrowExceptionWithInvalidEntries()
        {
            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyViewComponent<NormalComponent>
                        .Instance()
                        .InvokedWith(c => c.Invoke())
                        .ShouldHave()
                        .ViewBag(1)
                        .AndAlso()
                        .ShouldReturn()
                        .View();
                },
                "When invoking NormalComponent expected to have view bag with 1 entry, but in fact contained 0.");
        }

        [Fact]
        public void ViewBagWithNumberShouldThrowExceptionWithInvalidManyEntries()
        {
            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyViewComponent<AddViewBagComponent>
                        .Instance()
                        .InvokedWith(c => c.Invoke())
                        .ShouldHave()
                        .ViewBag(3)
                        .AndAlso()
                        .ShouldReturn()
                        .View();
                },
                "When invoking AddViewBagComponent expected to have view bag with 3 entries, but in fact contained 2.");
        }

        [Fact]
        public void ViewBagWithBuilderShouldWorkCorrectly()
        {
            MyViewComponent<AddViewBagComponent>
                .Instance()
                .InvokedWith(c => c.Invoke())
                .ShouldHave()
                .ViewBag(viewBag => viewBag
                    .ContainingEntry("Test", "BagValue"))
                .AndAlso()
                .ShouldReturn()
                .View();
        }
    }
}
