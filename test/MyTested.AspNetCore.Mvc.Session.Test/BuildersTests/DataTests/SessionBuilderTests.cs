namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.DataTests
{
    using System;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Session;
    using Microsoft.Extensions.DependencyInjection;
    using Setups;
    using Setups.Common;
    using Setups.Controllers;
    using Setups.Models;
    using Xunit;
    using Setups.ViewComponents;

    public class SessionBuilderTests
    {
        [Fact]
        public void WithIdShouldSetIdCorrectly()
        {
            MyApplication
                .IsUsingDefaultConfiguration()
                .WithServices(services =>
                {
                    services.AddMemoryCache();
                    services.AddDistributedMemoryCache();
                    services.AddSession();
                });

            MyController<MvcController>
                .Instance()
                .WithSession(session => session
                    .WithId("TestId")
                    .AndAlso()
                    .WithEntry("HasId", "HasIdValue"))
                .Calling(c => c.FullSessionAction())
                .ShouldReturn()
                .Ok()
                .WithModel("TestId");

            MyApplication.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void WithoutIdShouldSetRandomId()
        {
            MyApplication
                .IsUsingDefaultConfiguration()
                .WithServices(services =>
                {
                    services.AddMemoryCache();
                    services.AddDistributedMemoryCache();
                    services.AddSession();
                });

            MyController<MvcController>
                .Instance()
                .WithSession(session => session
                    .WithEntry("HasId", "HasIdValue"))
                .Calling(c => c.FullSessionAction())
                .ShouldReturn()
                .Ok()
                .ShouldPassForThe<OkObjectResult>(actionResult =>
                {
                    var okObjectResult = actionResult as OkObjectResult;

                    Assert.NotNull(okObjectResult);
                    Assert.IsAssignableFrom<string>(okObjectResult.Value);

                    var modelAsString = (string)okObjectResult.Value;

                    Assert.NotNull(modelAsString);
                    Assert.NotEmpty(modelAsString);

                    MyApplication.IsUsingDefaultConfiguration();
                });
        }

        [Fact]
        public void WithIdAndAnotherSessionShouldThrowException()
        {
            MyApplication
                .IsUsingDefaultConfiguration()
                .WithServices(services =>
                {
                    services.AddTransient<ISessionStore, CustomSessionStore>();
                });

            Test.AssertException<InvalidOperationException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .WithSession(session => session
                            .WithId("TestId")
                            .WithEntry("HasId", "HasIdValue"))
                        .Calling(c => c.FullSessionAction())
                        .ShouldReturn()
                        .Ok()
                        .WithModel("TestId");
                },
                "Setting session Id requires the registered ISession service to implement ISessionMock.");

            MyApplication.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void WithEntryShouldSetCorrectEntry()
        {
            MyApplication
                .IsUsingDefaultConfiguration()
                .WithServices(services =>
                {
                    services.AddMemoryCache();
                    services.AddDistributedMemoryCache();
                    services.AddSession();
                });

            MyController<MvcController>
                .Instance()
                .WithSession(session => session
                    .WithEntry("ByteEntry", new byte[] { 1, 2, 3 }))
                .Calling(c => c.FullSessionAction())
                .ShouldReturn()
                .Ok()
                .WithModel(new byte[] { 1, 2, 3 });

            MyApplication.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void WithIntegerEntryShouldSetCorrectEntry()
        {
            MyApplication
                .IsUsingDefaultConfiguration()
                .WithServices(services =>
                {
                    services.AddMemoryCache();
                    services.AddDistributedMemoryCache();
                    services.AddSession();
                });

            MyController<MvcController>
                .Instance()
                .WithSession(session => session
                    .WithEntry("IntEntry", 1))
                .Calling(c => c.FullSessionAction())
                .ShouldReturn()
                .Ok()
                .WithModel(1);

            MyApplication.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void WithEntriesAsObjectShouldWorkCorrectly()
        {
            MyApplication
                .IsUsingDefaultConfiguration()
                .WithServices(services =>
                {
                    services.AddMemoryCache();
                    services.AddDistributedMemoryCache();
                    services.AddSession();
                });

            MyController<MvcController>
                .Instance()
                .WithSession(session => session
                    .WithEntries(new
                    {
                        StringKey = "test",
                        IntKey = 1,
                        ByteKey = new byte[] { 1, 2, 3 }
                    }))
                .Calling(c => c.MultipleSessionValuesAction())
                .ShouldReturn()
                .Ok()
                .WithModel(new SessionResponseModel
                {
                    String = "test",
                    Integer = 1,
                    Byte = new byte[] { 1, 2, 3 }
                });

            MyApplication.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void WithEntriesAsByteDictionaryShouldWorkCorrectly()
        {
            MyApplication
                .IsUsingDefaultConfiguration()
                .WithServices(services =>
                {
                    services.AddMemoryCache();
                    services.AddDistributedMemoryCache();
                    services.AddSession();
                });

            MyController<MvcController>
                .Instance()
                .WithSession(session => session
                    .WithEntries(new Dictionary<string, byte[]> { ["ByteEntry"] = new byte[] { 1, 2, 3 }, ["Test"] = null }))
                .Calling(c => c.FullSessionAction())
                .ShouldReturn()
                .Ok()
                .WithModel(new byte[] { 1, 2, 3 });

            MyApplication.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void WithEntriesAsStringDictionaryShouldWorkCorrectly()
        {
            MyApplication
                .IsUsingDefaultConfiguration()
                .WithServices(services =>
                {
                    services.AddMemoryCache();
                    services.AddDistributedMemoryCache();
                    services.AddSession();
                });

            MyController<MvcController>
                .Instance()
                .WithSession(session => session
                    .WithEntries(new Dictionary<string, string> { ["StringEntry"] = "stringTest" }))
                .Calling(c => c.FullSessionAction())
                .ShouldReturn()
                .Ok()
                .WithModel("stringTest");

            MyApplication.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void WithEntriesAsIntDictionaryShouldWorkCorrectly()
        {
            MyApplication
                .IsUsingDefaultConfiguration()
                .WithServices(services =>
                {
                    services.AddMemoryCache();
                    services.AddDistributedMemoryCache();
                    services.AddSession();
                });

            MyController<MvcController>
                .Instance()
                .WithSession(session => session
                    .WithEntries(new Dictionary<string, int> { ["IntEntry"] = 1 }))
                .Calling(c => c.FullSessionAction())
                .ShouldReturn()
                .Ok()
                .WithModel(1);

            MyApplication.IsUsingDefaultConfiguration();
        }
        
        [Fact]
        public void WithIdShouldSetIdCorrectlyInViewComponent()
        {
            MyApplication
                .IsUsingDefaultConfiguration()
                .WithServices(services =>
                {
                    services.AddMemoryCache();
                    services.AddDistributedMemoryCache();
                    services.AddSession();
                });

            MyViewComponent<FullSessionComponent>
                .Instance()
                .WithSession(session => session
                    .WithId("TestId")
                    .AndAlso()
                    .WithEntry("HasId", "HasIdValue"))
                .InvokedWith(c => c.Invoke())
                .ShouldReturn()
                .Content("TestId");

            MyApplication.IsUsingDefaultConfiguration();
        }
        
        [Fact]
        public void WithEntryShouldSetCorrectEntryInViewComponent()
        {
            MyApplication
                .IsUsingDefaultConfiguration()
                .WithServices(services =>
                {
                    services.AddMemoryCache();
                    services.AddDistributedMemoryCache();
                    services.AddSession();
                });

            MyViewComponent<FullSessionComponent>
                .Instance()
                .WithSession(session => session
                    .WithEntry("ByteEntry", new byte[] { 1, 2, 3 }))
                .InvokedWith(c => c.Invoke())
                .ShouldReturn()
                .View()
                .WithModel(new byte[] { 1, 2, 3 });

            MyApplication.IsUsingDefaultConfiguration();
        }
    }
}
