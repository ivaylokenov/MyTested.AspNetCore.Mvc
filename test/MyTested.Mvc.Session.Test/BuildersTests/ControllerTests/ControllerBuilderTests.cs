namespace MyTested.Mvc.Test.BuildersTests.ControllerTests
{
    using System;
    using Microsoft.Extensions.DependencyInjection;
    using Mvc;
    using Setups;
    using Setups.Controllers;
    using Xunit;

    public class ControllerBuilderTests
    {

        [Fact]
        public void WithSessionShouldPopulateSessionCorrectly()
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
                .WithSession(session =>
                {
                    session.WithEntry("test", "value");
                })
                .Calling(c => c.SessionAction())
                .ShouldReturn()
                .Ok();

            MyMvc.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void WithSessionShouldThrowExceptionIfSessionIsNotSet()
        {
            Test.AssertException<InvalidOperationException>(
                () =>
                {
                    MyMvc
                       .Controller<MvcController>()
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
            MyMvc
                .IsUsingDefaultConfiguration()
                .WithServices(services =>
                {
                    services.AddMemoryCache();
                    services.AddDistributedMemoryCache();
                    services.AddSession();
                    services.AddHttpContextAccessor();
                });

            MyMvc
                .Controller<FullPocoController>()
                .WithSession(session =>
                {
                    session.WithEntry("test", "value");
                })
                .Calling(c => c.SessionAction())
                .ShouldReturn()
                .Ok();

            MyMvc.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void WithSessionShouldThrowExceptionIfSessionIsNotSetForPocoController()
        {
            Test.AssertException<InvalidOperationException>(
                () =>
                {
                    MyMvc
                       .Controller<FullPocoController>()
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
