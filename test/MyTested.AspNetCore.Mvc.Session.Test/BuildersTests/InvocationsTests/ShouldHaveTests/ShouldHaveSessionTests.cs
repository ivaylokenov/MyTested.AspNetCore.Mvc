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
                .IsUsingDefaultConfiguration()
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

            MyApplication.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void NoSessionShouldThrowExceptionWithEntries()
        {
            MyApplication
                .IsUsingDefaultConfiguration()
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

            MyApplication.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void SessionWithNoNumberShouldNotThrowExceptionWithAnyEntries()
        {
            MyApplication
                .IsUsingDefaultConfiguration()
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

            MyApplication.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void SessionWithNoNumberShouldThrowExceptionWithNoEntries()
        {
            MyApplication
                .IsUsingDefaultConfiguration()
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

            MyApplication.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void SessionWithNumberShouldNotThrowExceptionWithCorrectEntries()
        {
            MyApplication
                .IsUsingDefaultConfiguration()
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

            MyApplication.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void SessionWithNumberShouldThrowExceptionWithInvalidEntries()
        {
            MyApplication
                .IsUsingDefaultConfiguration()
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

            MyApplication.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void SessionWithNumberShouldThrowExceptionWithInvalidManyEntries()
        {
            MyApplication
                .IsUsingDefaultConfiguration()
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

            MyApplication.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void SessionWithBuilderShouldWorkCorrectly()
        {
            MyApplication
                .IsUsingDefaultConfiguration()
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

            MyApplication.IsUsingDefaultConfiguration();
        }
    }
}
