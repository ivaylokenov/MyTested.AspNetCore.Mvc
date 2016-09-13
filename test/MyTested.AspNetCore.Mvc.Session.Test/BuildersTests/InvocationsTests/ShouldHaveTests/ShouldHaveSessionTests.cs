namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.InvocationsTests.ShouldHaveTests
{
    using Exceptions;
    using Microsoft.Extensions.DependencyInjection;
    using Setups;
    using Setups.ViewComponents;
    using Xunit;

    public class ShouldHaveSessionTests
    {
        [Fact]
        public void NoSessionShouldNotThrowExceptionWithNoEntries()
        {
            MyApplication
                .StartsFrom<DefaultStartup>()
                .WithServices(services =>
                {
                    services.AddMemoryCache();
                    services.AddDistributedMemoryCache();
                    services.AddSession();
                });

            MyViewComponent<NormalComponent>
                .Instance()
                .InvokedWith(c => c.Invoke())
                .ShouldHave()
                .NoSession()
                .AndAlso()
                .ShouldReturn()
                .Content();

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void NoSessionShouldThrowExceptionWithEntries()
        {
            MyApplication
                .StartsFrom<DefaultStartup>()
                .WithServices(services =>
                {
                    services.AddMemoryCache();
                    services.AddDistributedMemoryCache();
                    services.AddSession();
                });

            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyViewComponent<AddSessionComponent>
                       .Instance()
                       .InvokedWith(c => c.Invoke())
                       .ShouldHave()
                       .NoSession()
                       .AndAlso()
                       .ShouldReturn()
                       .View();
                },
                "When invoking AddSessionComponent expected to have session with no entries, but in fact it had some.");

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void SessionWithNoNumberShouldNotThrowExceptionWithAnyEntries()
        {
            MyApplication
                .StartsFrom<DefaultStartup>()
                .WithServices(services =>
                {
                    services.AddMemoryCache();
                    services.AddDistributedMemoryCache();
                    services.AddSession();
                });

            MyViewComponent<AddSessionComponent>
                .Instance()
                .InvokedWith(c => c.Invoke())
                .ShouldHave()
                .Session()
                .AndAlso()
                .ShouldReturn()
                .View();

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void SessionWithNoNumberShouldThrowExceptionWithNoEntries()
        {
            MyApplication
                .StartsFrom<DefaultStartup>()
                .WithServices(services =>
                {
                    services.AddMemoryCache();
                    services.AddDistributedMemoryCache();
                    services.AddSession();
                });

            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyViewComponent<NormalComponent>
                        .Instance()
                        .InvokedWith(c => c.Invoke())
                        .ShouldHave()
                        .Session()
                        .AndAlso()
                        .ShouldReturn()
                        .View();
                },
                "When invoking NormalComponent expected to have session entries, but none were found.");

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void SessionWithNumberShouldNotThrowExceptionWithCorrectEntries()
        {
            MyApplication
                .StartsFrom<DefaultStartup>()
                .WithServices(services =>
                {
                    services.AddMemoryCache();
                    services.AddDistributedMemoryCache();
                    services.AddSession();
                });

            MyViewComponent<AddSessionComponent>
                .Instance()
                .InvokedWith(c => c.Invoke())
                .ShouldHave()
                .Session(withNumberOfEntries: 3)
                .AndAlso()
                .ShouldReturn()
                .View();

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void SessionWithNumberShouldThrowExceptionWithInvalidEntries()
        {
            MyApplication
                .StartsFrom<DefaultStartup>()
                .WithServices(services =>
                {
                    services.AddMemoryCache();
                    services.AddDistributedMemoryCache();
                    services.AddSession();
                });

            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyViewComponent<NormalComponent>
                        .Instance()
                        .InvokedWith(c => c.Invoke())
                        .ShouldHave()
                        .Session(1)
                        .AndAlso()
                        .ShouldReturn()
                        .View();
                },
                "When invoking NormalComponent expected to have session with 1 entry, but in fact contained 0.");

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void SessionWithNumberShouldThrowExceptionWithInvalidManyEntries()
        {
            MyApplication
                .StartsFrom<DefaultStartup>()
                .WithServices(services =>
                {
                    services.AddMemoryCache();
                    services.AddDistributedMemoryCache();
                    services.AddSession();
                });

            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyViewComponent<AddSessionComponent>
                        .Instance()
                        .InvokedWith(c => c.Invoke())
                        .ShouldHave()
                        .Session(4)
                        .AndAlso()
                        .ShouldReturn()
                        .View();
                },
                "When invoking AddSessionComponent expected to have session with 4 entries, but in fact contained 3.");

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void SessionWithBuilderShouldWorkCorrectly()
        {
            MyApplication
                .StartsFrom<DefaultStartup>()
                .WithServices(services =>
                {
                    services.AddMemoryCache();
                    services.AddDistributedMemoryCache();
                    services.AddSession();
                });

            MyViewComponent<AddSessionComponent>
                .Instance()
                .InvokedWith(c => c.Invoke())
                .ShouldHave()
                .Session(session => session
                    .ContainingEntry("Integer", 1))
                .AndAlso()
                .ShouldReturn()
                .View();

            MyApplication.StartsFrom<DefaultStartup>();
        }
    }
}
