namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.ControllerTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
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
            MyApplication.StartsFrom<DefaultStartup>();

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

        [Fact]
        public void RemovingSessionEntryByKeyShouldReturnCorrectSession()
        {
            this.SetDefaultSession();

            IDictionary<string, string> entries = new Dictionary<string, string>
            {
                { "testKey1", "testValue1" },
                { "testKey2", "testValue2" }
            };

            var keyToRemove = "testKey1";

            var keys = entries.Keys.ToList();
            keys.Remove(keyToRemove);

            MyController<MvcController>
                .Instance()
                .WithSession(session =>
                {
                    session.WithEntries(entries);
                })
                .WithoutSession(session => session.WithoutEntry(keyToRemove))
                .Calling(c => c.GetSessionKeys())
                .ShouldReturn()
                .Ok(ok => ok
                    .WithModel(keys));

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void ClearingSessionShouldReturnCorrectEmptyCollectionOfKeys()
        {
            this.SetDefaultSession();

            IDictionary<string, string> entries = new Dictionary<string, string>
            {
                { "testKey1", "testValue1" },
                { "testKey2", "testValue2" }
            };

            MyController<MvcController>
                .Instance()
                .WithSession(session =>
                {
                    session.WithEntries(entries);
                })
                .WithoutSession()
                .Calling(c => c.GetSessionKeysCount())
                .ShouldReturn()
                .Ok(ok => ok
                    .WithModel(0));

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void ClearingEmptySessionShouldReturnCorrectEmptyCollectionOfKeys()
        {
            this.SetDefaultSession();

            MyController<MvcController>
                .Instance()
                .WithoutSession()
                .Calling(c => c.GetSessionKeysCount())
                .ShouldReturn()
                .Ok(ok => ok
                    .WithModel(0));

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void RemovingSessionEntryByKeyAndAlsoShouldReturnCorrectSession()
        {
            this.SetDefaultSession();

            IDictionary<string, string> entries = new Dictionary<string, string>
            {
                { "testKey1", "testValue1" },
                { "testKey2", "testValue2" },
                { "testKey3", "testValue3" },
                { "testKey4", "testValue4" }
            };

            var keysToRemove = new List<string> { "testKey1", "testKey3" };
            var keys = entries.Keys.ToList();
            keysToRemove.ForEach(key => keys.Remove(key));

            MyController<MvcController>
                .Instance()
                .WithSession(session =>
                {
                    session.WithEntries(entries);
                })
                .WithoutSession(session => session.WithoutEntry(keysToRemove.First()))
                .AndAlso()
                .WithoutSession(session => session.WithoutEntry(keysToRemove.Last()))
                .Calling(c => c.GetSessionKeys())
                .ShouldReturn()
                .Ok(ok => ok
                    .WithModel(keys));

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
