namespace MyTested.AspNetCore.Mvc.Test
{
    using Internal.Application;
    using Internal.Http;
    using Microsoft.AspNetCore.Session;
    using Microsoft.Extensions.DependencyInjection;
    using Setups.Common;
    using Setups.Startups;
    using Xunit;

    public class ServicesTests
    {
        [Fact]
        public void DefaultConfigurationWithSessionShouldSetMockedSession()
        {
            MyApplication
                .IsUsingDefaultConfiguration()
                .WithServices(services =>
                {
                    services.AddMemoryCache();
                    services.AddDistributedMemoryCache();
                    services.AddSession();
                });

            var session = TestServiceProvider.GetService<ISessionStore>();

            Assert.NotNull(session);
            Assert.IsAssignableFrom<MockedSessionStore>(session);

            MyApplication.IsUsingDefaultConfiguration();
        }
        
        [Fact]
        public void ExplicitMockedSessionShouldOverrideIt()
        {
            MyApplication
                .StartsFrom<SessionDataStartup>()
                .WithServices(services =>
                {
                    services.ReplaceSession();
                });

            var session = TestServiceProvider.GetService<ISessionStore>();

            Assert.NotNull(session);
            Assert.IsAssignableFrom<MockedSessionStore>(session);

            MyApplication.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void DefaultConfigurationShouldNotSetMockedSession()
        {
            MyApplication.IsUsingDefaultConfiguration();

            var session = TestServiceProvider.GetService<ISessionStore>();

            Assert.Null(session);
        }

        [Fact]
        public void CustomSessionShouldOverrideTheMockedOne()
        {
            MyApplication.StartsFrom<SessionDataStartup>();

            var session = TestServiceProvider.GetService<ISessionStore>();

            Assert.NotNull(session);
            Assert.IsAssignableFrom<CustomSessionStore>(session);

            MyApplication.IsUsingDefaultConfiguration();
        }
    }
}
