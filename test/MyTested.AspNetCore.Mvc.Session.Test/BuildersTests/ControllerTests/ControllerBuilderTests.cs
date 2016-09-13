namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.ControllerTests
{
    using System;
    using Microsoft.Extensions.DependencyInjection;
    using Setups;
    using Setups.Controllers;
    using Xunit;

    public class ControllerBuilderTests
    {
        [Fact]
        public void WithSessionShouldPopulateSessionCorrectly()
        {
            MyApplication
                .StartsFrom<DefaultStartup>()
                .WithServices(services =>
                {
                    services.AddMemoryCache();
                    services.AddDistributedMemoryCache();
                    services.AddSession();
                });

            MyController<MvcController>
                .Instance()
                .WithSession(session =>
                {
                    session.WithEntry("test", "value");
                })
                .Calling(c => c.SessionAction())
                .ShouldReturn()
                .Ok();

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void WithSessionShouldThrowExceptionIfSessionIsNotSet()
        {
            Test.AssertException<InvalidOperationException>(
                () =>
                {
                    MyController<MvcController>
                       .Instance()
                       .WithSession(session =>
                       {
                           session.WithEntry("test", "value");
                       })
                       .Calling(c => c.SessionAction())
                       .ShouldReturn()
                       .Ok();
                },
                "Session has not been configured for this application or request.");
        }
        
        [Fact]
        public void WithSessionShouldPopulateSessionCorrectlyForPocoController()
        {
            MyApplication
                .StartsFrom<DefaultStartup>()
                .WithServices(services =>
                {
                    services.AddMemoryCache();
                    services.AddDistributedMemoryCache();
                    services.AddSession();
                });

            MyController<MvcController>
                .Instance()
                .WithSession(session =>
                {
                    session.WithEntry("test", "value");
                })
                .Calling(c => c.SessionAction())
                .ShouldReturn()
                .Ok();

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void WithSessionShouldThrowExceptionIfSessionIsNotSetForPocoController()
        {
            Test.AssertException<InvalidOperationException>(
                () =>
                {
                    MyController<MvcController>
                       .Instance()
                       .WithSession(session =>
                       {
                           session.WithEntry("test", "value");
                       })
                       .Calling(c => c.SessionAction())
                       .ShouldReturn()
                       .Ok();
                },
                "Session has not been configured for this application or request.");
        }
    }
}
