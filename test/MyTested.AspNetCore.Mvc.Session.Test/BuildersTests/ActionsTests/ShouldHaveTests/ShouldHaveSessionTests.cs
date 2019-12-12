namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.ActionsTests.ShouldHaveTests
{
    using Exceptions;
    using Microsoft.Extensions.DependencyInjection;
    using Setups;
    using Setups.Controllers;
    using Xunit;

    public class ShouldHaveSessionTests
    {
        [Fact]
        public void NoSessionShouldNotThrowExceptionWithNoEntries()
        {
            this.SetDefaultSession();

            MyController<MvcController>
                .Instance()
                .Calling(c => c.Ok())
                .ShouldHave()
                .NoSession()
                .AndAlso()
                .ShouldReturn()
                .Ok();

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void NoSessionShouldThrowExceptionWithEntries()
        {
            this.SetDefaultSession();

            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyController<MvcController>
                       .Instance()
                       .Calling(c => c.AddSessionAction())
                       .ShouldHave()
                       .NoSession()
                       .AndAlso()
                       .ShouldReturn()
                       .Ok();
                },
                "When calling AddSessionAction action in MvcController expected to have session with no entries, but in fact it had some.");

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void SessionWithNoNumberShouldNotThrowExceptionWithAnyEntries()
        {
            this.SetDefaultSession();

            MyController<MvcController>
                .Instance()
                .Calling(c => c.AddSessionAction())
                .ShouldHave()
                .Session()
                .AndAlso()
                .ShouldReturn()
                .Ok();

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void SessionWithNoNumberShouldThrowExceptionWithNoEntries()
        {
            this.SetDefaultSession();

            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.Ok())
                        .ShouldHave()
                        .Session()
                        .AndAlso()
                        .ShouldReturn()
                        .Ok();
                },
                "When calling Ok action in MvcController expected to have session entries, but none were found.");

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void SessionWithNumberShouldNotThrowExceptionWithCorrectEntries()
        {
            this.SetDefaultSession();

            MyController<MvcController>
                .Instance()
                .Calling(c => c.AddSessionAction())
                .ShouldHave()
                .Session(withNumberOfEntries: 3)
                .AndAlso()
                .ShouldReturn()
                .Ok();

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void SessionWithNumberShouldThrowExceptionWithInvalidEntries()
        {
            this.SetDefaultSession();

            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.Ok())
                        .ShouldHave()
                        .Session(1)
                        .AndAlso()
                        .ShouldReturn()
                        .Ok();
                },
                "When calling Ok action in MvcController expected to have session with 1 entry, but in fact contained 0.");

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void SessionWithNumberShouldThrowExceptionWithInvalidManyEntries()
        {
            this.SetDefaultSession();

            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.AddSessionAction())
                        .ShouldHave()
                        .Session(4)
                        .AndAlso()
                        .ShouldReturn()
                        .Ok();
                },
                "When calling AddSessionAction action in MvcController expected to have session with 4 entries, but in fact contained 3.");

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void SessionWithBuilderShouldWorkCorrectly()
        {
            this.SetDefaultSession();

            MyController<MvcController>
                .Instance()
                .Calling(c => c.AddSessionAction())
                .ShouldHave()
                .Session(session => session
                    .ContainingEntry("Integer", 1))
                .AndAlso()
                .ShouldReturn()
                .Ok();

            MyApplication.StartsFrom<DefaultStartup>();
        }

        private void SetDefaultSession()
        {
            MyApplication
                .StartsFrom<DefaultStartup>()
                .WithServices(services =>
                {
                    services.AddMemoryCache();
                    services.AddDistributedMemoryCache();
                    services.AddSession();
                });
        }
    }
}
