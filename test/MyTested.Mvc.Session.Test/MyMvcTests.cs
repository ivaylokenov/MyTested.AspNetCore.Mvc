namespace MyTested.Mvc.Test
{
    using Internal.Application;
    using Internal.Http;
    using Microsoft.AspNetCore.Session;
    using Microsoft.Extensions.DependencyInjection;
    using Mvc;
    using Setups.Common;
    using Setups.Startups;
    using Xunit;

    public class MyMvcTests
    {
        [Fact]
        public void DefaultConfigurationWithSessionShouldSetMockedSession()
        {
            MyMvc
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

            MyMvc.IsUsingDefaultConfiguration();
        }
        
        [Fact]
        public void ExplicitMockedSessionShouldOverrideIt()
        {
            MyMvc
                .StartsFrom<SessionDataStartup>()
                .WithServices(services =>
                {
                    services.ReplaceSession();
                });

            var session = TestServiceProvider.GetService<ISessionStore>();

            Assert.NotNull(session);
            Assert.IsAssignableFrom<MockedSessionStore>(session);

            MyMvc.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void DefaultConfigurationShouldNotSetMockedSession()
        {
            MyMvc.IsUsingDefaultConfiguration();

            var session = TestServiceProvider.GetService<ISessionStore>();

            Assert.Null(session);
        }

        [Fact]
        public void CustomSessionShouldOverrideTheMockedOne()
        {
            MyMvc.StartsFrom<SessionDataStartup>();

            var session = TestServiceProvider.GetService<ISessionStore>();

            Assert.NotNull(session);
            Assert.IsAssignableFrom<CustomSessionStore>(session);

            MyMvc.IsUsingDefaultConfiguration();
        }
    }
}
