namespace MyTested.Mvc.Test.BuildersTests.DataTests
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

    public class SessionBuilderTests
    {
        [Fact]
        public void WithIdShouldSetIdCorrectly()
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
                .WithSession(session => session
                    .WithId("TestId")
                    .AndAlso()
                    .WithEntry("HasId", "HasIdValue"))
                .Calling(c => c.FullSessionAction())
                .ShouldReturn()
                .Ok()
                .WithResponseModel("TestId");

            MyMvc.IsUsingDefaultConfiguration();
        }
        
        [Fact]
        public void WithoutIdShouldSetRandomId()
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
                .WithSession(session => session
                    .WithEntry("HasId", "HasIdValue"))
                .Calling(c => c.FullSessionAction())
                .ShouldReturn()
                .Ok()
                .ShouldPassFor()
                .TheActionResult(actionResult =>
                {
                    var okObjectResult = actionResult as OkObjectResult;

                    Assert.NotNull(okObjectResult);
                    Assert.IsAssignableFrom<string>(okObjectResult.Value);

                    var modelAsString = (string)okObjectResult.Value;

                    Assert.NotNull(modelAsString);
                    Assert.NotEmpty(modelAsString);

                    MyMvc.IsUsingDefaultConfiguration();
                });
        }

        [Fact]
        public void WithIdAndAnotherSessionShouldThrowException()
        {
            MyMvc
                .IsUsingDefaultConfiguration()
                .WithServices(services =>
                {
                    services.AddTransient<ISessionStore, CustomSessionStore>();
                });

            Test.AssertException<InvalidOperationException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .WithSession((Action<Builders.Contracts.Data.ISessionBuilder>)(session => session
                            .WithId("TestId")
                            .WithEntry("HasId", "HasIdValue")))
                        .Calling(c => c.FullSessionAction())
                        .ShouldReturn()
                        .Ok()
                        .WithResponseModel("TestId");
                },
                "Setting session Id requires the registered ISession service to implement IMockedSession.");

            MyMvc.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void WithEntryShouldSetCorrectEntry()
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
                .WithSession(session => session
                    .WithEntry("ByteEntry", new byte[] { 1, 2, 3 }))
                .Calling(c => c.FullSessionAction())
                .ShouldReturn()
                .Ok()
                .WithResponseModel(new byte[] { 1, 2, 3 });

            MyMvc.IsUsingDefaultConfiguration();
        }
        
        [Fact]
        public void WithIntegerEntryShouldSetCorrectEntry()
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
                .WithSession(session => session
                    .WithEntry("IntEntry", 1))
                .Calling(c => c.FullSessionAction())
                .ShouldReturn()
                .Ok()
                .WithResponseModel(1);

            MyMvc.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void WithEntriesAsObjectShouldWorkCorrectly()
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
                .WithResponseModel(new SessionResponseModel
                {
                    String = "test",
                    Integer = 1,
                    Byte = new byte[] { 1, 2, 3 }
                });

            MyMvc.IsUsingDefaultConfiguration();
        }
        
        [Fact]
        public void WithEntriesAsByteDictionaryShouldWorkCorrectly()
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
                .WithSession(session => session
                    .WithEntries(new Dictionary<string, byte[]> { ["ByteEntry"] = new byte[] { 1, 2, 3 }, ["Test"] = null }))
                .Calling(c => c.FullSessionAction())
                .ShouldReturn()
                .Ok()
                .WithResponseModel(new byte[] { 1, 2, 3 });

            MyMvc.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void WithEntriesAsStringDictionaryShouldWorkCorrectly()
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
                .WithSession(session => session
                    .WithEntries(new Dictionary<string, string> { ["StringEntry"] = "stringTest" }))
                .Calling(c => c.FullSessionAction())
                .ShouldReturn()
                .Ok()
                .WithResponseModel("stringTest");

            MyMvc.IsUsingDefaultConfiguration();
        }
        
        [Fact]
        public void WithEntriesAsIntDictionaryShouldWorkCorrectly()
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
                .WithSession(session => session
                    .WithEntries(new Dictionary<string, int> { ["IntEntry"] = 1 }))
                .Calling(c => c.FullSessionAction())
                .ShouldReturn()
                .Ok()
                .WithResponseModel(1);

            MyMvc.IsUsingDefaultConfiguration();
        }
    }
}
