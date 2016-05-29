namespace MyTested.Mvc.Test.BuildersTests.ActionsTests.ShouldHaveTests
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
            MyMvc
                .IsUsingDefaultConfiguration()
                .WithServices(services =>
                {
                    services.AddMemoryCache();
                    services.AddDistributedMemoryCache();
                    services.AddSession();
                });

            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.Ok())
                .ShouldHave()
                .NoSession()
                .AndAlso()
                .ShouldReturn()
                .Ok();

            MyMvc.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void NoSessionShouldThrowExceptionWithEntries()
        {
            MyMvc
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
                    MyMvc
                       .Controller<MvcController>()
                       .Calling(c => c.AddSessionAction())
                       .ShouldHave()
                       .NoSession()
                       .AndAlso()
                       .ShouldReturn()
                       .Ok();
                },
                "When calling AddSessionAction action in MvcController expected to have session with no entries, but in fact it had some.");

            MyMvc.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void SessionWithNoNumberShouldNotThrowExceptionWithAnyEntries()
        {
            MyMvc
                .IsUsingDefaultConfiguration()
                .WithServices(services =>
                {
                    services.AddMemoryCache();
                    services.AddDistributedMemoryCache();
                    services.AddSession();
                });

            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.AddSessionAction())
                .ShouldHave()
                .Session()
                .AndAlso()
                .ShouldReturn()
                .Ok();

            MyMvc.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void SessionWithNoNumberShouldThrowExceptionWithNoEntries()
        {
            MyMvc
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
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.Ok())
                        .ShouldHave()
                        .Session()
                        .AndAlso()
                        .ShouldReturn()
                        .Ok();
                },
                "When calling Ok action in MvcController expected to have session entries, but none were found.");

            MyMvc.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void SessionWithNumberShouldNotThrowExceptionWithCorrectEntries()
        {
            MyMvc
                .IsUsingDefaultConfiguration()
                .WithServices(services =>
                {
                    services.AddMemoryCache();
                    services.AddDistributedMemoryCache();
                    services.AddSession();
                });

            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.AddSessionAction())
                .ShouldHave()
                .Session(withNumberOfEntries: 3)
                .AndAlso()
                .ShouldReturn()
                .Ok();

            MyMvc.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void SessionWithNumberShouldThrowExceptionWithInvalidEntries()
        {
            MyMvc
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
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.Ok())
                        .ShouldHave()
                        .Session(1)
                        .AndAlso()
                        .ShouldReturn()
                        .Ok();
                },
                "When calling Ok action in MvcController expected to have session with 1 entry, but in fact contained 0.");

            MyMvc.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void SessionWithNumberShouldThrowExceptionWithInvalidManyEntries()
        {
            MyMvc
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
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.AddSessionAction())
                        .ShouldHave()
                        .Session(4)
                        .AndAlso()
                        .ShouldReturn()
                        .Ok();
                },
                "When calling AddSessionAction action in MvcController expected to have session with 4 entries, but in fact contained 3.");

            MyMvc.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void SessionWithBuilderShouldWorkCorrectly()
        {
            MyMvc
                .IsUsingDefaultConfiguration()
                .WithServices(services =>
                {
                    services.AddMemoryCache();
                    services.AddDistributedMemoryCache();
                    services.AddSession();
                });

            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.AddSessionAction())
                .ShouldHave()
                .Session(session => session
                    .ContainingEntry("Integer", 1))
                .AndAlso()
                .ShouldReturn()
                .Ok();

            MyMvc.IsUsingDefaultConfiguration();
        }
    }
}
