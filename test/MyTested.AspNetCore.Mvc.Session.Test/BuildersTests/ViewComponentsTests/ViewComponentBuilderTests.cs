namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.ViewComponentsTests
{
    using Microsoft.Extensions.DependencyInjection;
    using Setups;
    using Setups.ViewComponents;
    using System;
    using Xunit;

    public class ViewComponentBuilderTests
    {
        [Fact]
        public void WithSessionShouldPopulateSessionCorrectly()
        {
            MyApplication
                .IsUsingDefaultConfiguration()
                .WithServices(services =>
                {
                    services.AddMemoryCache();
                    services.AddDistributedMemoryCache();
                    services.AddSession();
                });

            MyViewComponent<SessionComponent>
                .Instance()
                .WithSession(session =>
                {
                    session.WithEntry("test", "value");
                })
                .InvokedWith(c => c.Invoke())
                .ShouldReturn()
                .View();

            MyApplication.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void WithSessionShouldThrowExceptionIfSessionIsNotSet()
        {
            Test.AssertException<InvalidOperationException>(
                () =>
                {
                    MyViewComponent<SessionComponent>
                       .Instance()
                       .WithSession(session =>
                       {
                           session.WithEntry("test", "value");
                       })
                       .InvokedWith(c => c.Invoke())
                       .ShouldReturn()
                       .View();
                },
                "Session has not been configured for this application or request.");
        }
    }
}
