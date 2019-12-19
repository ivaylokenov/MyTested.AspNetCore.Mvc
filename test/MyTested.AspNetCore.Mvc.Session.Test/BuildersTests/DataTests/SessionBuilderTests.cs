﻿namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.DataTests
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
                .StartsFrom<DefaultStartup>()
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
                .Ok(ok => ok
                    .WithModel("TestId"));

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void WithoutIdShouldSetRandomId()
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
                .WithSession(session => session
                    .WithEntry("HasId", "HasIdValue"))
                .Calling(c => c.FullSessionAction())
                .ShouldReturn()
                .Ok(ok => ok
                    .ShouldPassForThe<OkObjectResult>(actionResult =>
                    {
                        var okObjectResult = actionResult as OkObjectResult;

                        Assert.NotNull(okObjectResult);
                        Assert.IsAssignableFrom<string>(okObjectResult.Value);

                        var modelAsString = (string)okObjectResult.Value;

                        Assert.NotNull(modelAsString);
                        Assert.NotEmpty(modelAsString);

                        MyApplication.StartsFrom<DefaultStartup>();
                    }));
        }

        [Fact]
        public void WithIdAndAnotherSessionShouldThrowException()
        {
            MyApplication
                .StartsFrom<DefaultStartup>()
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
                        .Ok(ok => ok
                            .WithModel("TestId"));
                },
                "Setting session Id requires the registered ISession service to implement ISessionMock.");

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void WithEntryShouldSetCorrectEntry()
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
                .WithSession(session => session
                    .WithEntry("ByteEntry", new byte[] { 1, 2, 3 }))
                .Calling(c => c.FullSessionAction())
                .ShouldReturn()
                .Ok(ok => ok
                    .WithModel(new byte[] { 1, 2, 3 }));

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void WithIntegerEntryShouldSetCorrectEntry()
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
                .WithSession(session => session
                    .WithEntry("IntEntry", 1))
                .Calling(c => c.FullSessionAction())
                .ShouldReturn()
                .Ok(ok => ok
                    .WithModel(1));

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void WithWrongEntryKeyAndStringValueShouldReturnBadRequest()
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
                .WithSession(session => session
                    .WithEntry("Invalid", "value"))
                .Calling(c => c.FullSessionAction())
                .ShouldReturn()
                .BadRequest();

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void WithWrongEntryKeyAndIntegerValueShouldReturnBadRequest()
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
                .WithSession(session => session
                    .WithEntry("Invalid", 1))
                .Calling(c => c.FullSessionAction())
                .ShouldReturn()
                .BadRequest();

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void WithWrongEntryKeyAndByteArrayValueShouldReturnBadRequest()
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
                .WithSession(session => session
                    .WithEntry("Invalid", new byte[] { 1, 2, 3 }))
                .Calling(c => c.FullSessionAction())
                .ShouldReturn()
                .BadRequest();

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void WithEntriesAsObjectShouldWorkCorrectly()
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
                .WithSession(session => session
                    .WithEntries(new
                    {
                        StringKey = "test",
                        IntKey = 1,
                        ByteKey = new byte[] { 1, 2, 3 }
                    }))
                .Calling(c => c.MultipleSessionValuesAction())
                .ShouldReturn()
                .Ok(ok => ok
                    .WithModel(new SessionResponseModel
                    {
                        String = "test",
                        Integer = 1,
                        Byte = new byte[] { 1, 2, 3 }
                    }));

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void WithEntriesAsObjectShouldReturnBadRequestWithWrongKeys()
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
                .WithSession(session => session
                .WithEntries(new
                {
                    InvalidStringKey = "value",
                    InvalidIntKey = 1,
                    InvalidByteKey = new byte[] { 1, 2, 3 }
                }))
                .Calling(c => c.FullSessionAction())
                .ShouldReturn()
                .BadRequest();

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void WithEntriesAsByteDictionaryShouldWorkCorrectly()
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
                .WithSession(session => session
                    .WithEntries(new Dictionary<string, byte[]>
                    {
                        ["ByteEntry"] = new byte[] { 1, 2, 3 },
                        ["Test"] = null
                    }))
                .Calling(c => c.FullSessionAction())
                .ShouldReturn()
                .Ok(ok => ok
                    .WithModel(new byte[] { 1, 2, 3 }));

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void WithEntriesAsByteDictionaryAndWrongKeyShouldReturnBadRequest()
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
                .WithSession(session => session
                    .WithEntries(new Dictionary<string, byte[]>
                    {
                        ["InvalidEntry"] = new byte[] { 1, 2, 3 },
                        ["Test"] = null
                    }))
                .Calling(c => c.FullSessionAction())
                .ShouldReturn()
                .BadRequest();

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void WithEntriesAsStringDictionaryShouldWorkCorrectly()
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
                .WithSession(session => session
                    .WithEntries(new Dictionary<string, string>
                    {
                        ["StringEntry"] = "stringTest"
                    }))
                .Calling(c => c.FullSessionAction())
                .ShouldReturn()
                .Ok(ok => ok
                    .WithModel("stringTest"));

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void WithEntriesAsStringDictionaryAndWrongKeyShouldReturnBadRequest()
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
                .WithSession(session => session
                    .WithEntries(new Dictionary<string, string>
                    {
                        ["InvalidEntry"] = "stringTest"
                    }))
                .Calling(c => c.FullSessionAction())
                .ShouldReturn()
                .BadRequest();

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void WithEntriesAsIntDictionaryShouldWorkCorrectly()
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
                .WithSession(session => session
                    .WithEntries(new Dictionary<string, int>
                    {
                        ["IntEntry"] = 1
                    }))
                .Calling(c => c.FullSessionAction())
                .ShouldReturn()
                .Ok(ok => ok
                    .WithModel(1));

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void WithEntriesAsIntDictionaryAndWrongKeyShouldReturnBadRequest()
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
                .WithSession(session => session
                    .WithEntries(new Dictionary<string, int>
                    {
                        ["InvalidEntry"] = 1
                    }))
                .Calling(c => c.FullSessionAction())
                .ShouldReturn()
                .BadRequest();

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void WithIdShouldSetIdCorrectlyInViewComponent()
        {
            MyApplication
                .StartsFrom<DefaultStartup>()
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

            MyApplication.StartsFrom<DefaultStartup>();
        }
        
        [Fact]
        public void WithByteArrayEntryShouldSetCorrectEntryInViewComponent()
        {
            MyApplication
                .StartsFrom<DefaultStartup>()
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
                .View(view => view
                    .WithModel(new byte[] { 1, 2, 3 }));

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void WithIntEntryShouldSetCorrectEntryInViewComponent()
        {
            MyApplication
                .StartsFrom<DefaultStartup>()
                .WithServices(services =>
                {
                    services.AddMemoryCache();
                    services.AddDistributedMemoryCache();
                    services.AddSession();
                });

            MyViewComponent<FullSessionComponent>
                .Instance()
                .WithSession(session => session
                    .WithEntry("IntEntry", 1))
                .InvokedWith(c => c.Invoke())
                .ShouldReturn()
                .View(view => view
                    .WithModel(1));

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void WithWrongEntryKeyAndStringValueShouldNotSetEntryInViewComponent()
        {
            MyApplication
                .StartsFrom<DefaultStartup>()
                .WithServices(services =>
                {
                    services.AddMemoryCache();
                    services.AddDistributedMemoryCache();
                    services.AddSession();
                });

            MyViewComponent<FullSessionComponent>
                .Instance()
                .WithSession(session => session
                    .WithEntry("InvalidEntry", "value"))
                .InvokedWith(c => c.Invoke())
                .ShouldReturn()
                .Content("Invalid");

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void WithWrongEntryKeyAndIntValueShouldNotSetEntryInViewComponent()
        {
            MyApplication
                .StartsFrom<DefaultStartup>()
                .WithServices(services =>
                {
                    services.AddMemoryCache();
                    services.AddDistributedMemoryCache();
                    services.AddSession();
                });

            MyViewComponent<FullSessionComponent>
                .Instance()
                .WithSession(session => session
                    .WithEntry("InvalidEntry", 1))
                .InvokedWith(c => c.Invoke())
                .ShouldReturn()
                .Content("Invalid");

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void WithWrongEntryKeyAndByteArrayValueShouldNotSetEntryInViewComponent()
        {
            MyApplication
                .StartsFrom<DefaultStartup>()
                .WithServices(services =>
                {
                    services.AddMemoryCache();
                    services.AddDistributedMemoryCache();
                    services.AddSession();
                });

            MyViewComponent<FullSessionComponent>
                .Instance()
                .WithSession(session => session
                    .WithEntry("InvalidEntry", new byte[] { 1, 2, 3 }))
                .InvokedWith(c => c.Invoke())
                .ShouldReturn()
                .Content("Invalid");

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void WithEntriesAsObjectShouldSetCorrectEntryInViewComponent()
        {
            MyApplication
                .StartsFrom<DefaultStartup>()
                .WithServices(services =>
                {
                    services.AddMemoryCache();
                    services.AddDistributedMemoryCache();
                    services.AddSession();
                });

            MyViewComponent<FullSessionComponent>
                .Instance()
                .WithSession(session => session
                    .WithEntries(new
                    {
                        StringEntry = "test",
                        IntEntry = 1,
                        ByteEntry = new byte[] { 1, 2, 3 }
                    }))
                .InvokedWith(c => c.Invoke())
                .ShouldReturn()
                .View(view => view
                    .WithModel(new byte[] { 1, 2, 3 }));

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void WithWrongEntryKeyAndEntriesAsObjectShouldNotSetEntryInViewComponent()
        {
            MyApplication
                .StartsFrom<DefaultStartup>()
                .WithServices(services =>
                {
                    services.AddMemoryCache();
                    services.AddDistributedMemoryCache();
                    services.AddSession();
                });

            MyViewComponent<FullSessionComponent>
                .Instance()
                .WithSession(session => session
                .WithEntries(new
                {
                    InvalidStringKey = "value",
                    InvalidIntKey = 1,
                    InvalidByteKey = new byte[] { 1, 2, 3 }
                }))
                .InvokedWith(c => c.Invoke())
                .ShouldReturn()
                .Content("Invalid");

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void WithEntriesAsByteDictionaryShouldSetCorrectEntryInViewComponent()
        {
            MyApplication
                .StartsFrom<DefaultStartup>()
                .WithServices(services =>
                {
                    services.AddMemoryCache();
                    services.AddDistributedMemoryCache();
                    services.AddSession();
                });

            MyViewComponent<FullSessionComponent>
                .Instance()
                .WithSession(session => session
                    .WithEntries(new Dictionary<string, byte[]>
                    {
                        ["ByteEntry"] = new byte[] { 1, 2, 3 },
                        ["Test"] = null
                    }))
                .InvokedWith(c => c.Invoke())
                .ShouldReturn()
                .View(view => view
                    .WithModel(new byte[] { 1, 2, 3 }));

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void WithEntriesAsByteDictionaryAndWrongKeyShouldNotSetCorrectEntryInViewComponent()
        {
            MyApplication
                .StartsFrom<DefaultStartup>()
                .WithServices(services =>
                {
                    services.AddMemoryCache();
                    services.AddDistributedMemoryCache();
                    services.AddSession();
                });

            MyViewComponent<FullSessionComponent>
                .Instance()
                .WithSession(session => session
                    .WithEntries(new Dictionary<string, byte[]>
                    {
                        ["InvalidEntry"] = new byte[] { 1, 2, 3 },
                        ["Test"] = null
                    }))
                .InvokedWith(c => c.Invoke())
                .ShouldReturn()
                .Content("Invalid");

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void WithEntriesAsStringDictionaryShouldSetCorrectEntryInViewComponent()
        {
            MyApplication
                .StartsFrom<DefaultStartup>()
                .WithServices(services =>
                {
                    services.AddMemoryCache();
                    services.AddDistributedMemoryCache();
                    services.AddSession();
                });

            MyViewComponent<FullSessionComponent>
                .Instance()
                .WithSession(session => session
                    .WithEntries(new Dictionary<string, string>
                    {
                        ["StringEntry"] = "stringTest"
                    }))
                .InvokedWith(c => c.Invoke())
                .ShouldReturn()
                .View(view => view
                    .WithModel("stringTest"));

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void WithEntriesAsStringDictionaryAndWrongKeyShouldNotSetCorrectEntryInViewComponent()
        {
            MyApplication
                .StartsFrom<DefaultStartup>()
                .WithServices(services =>
                {
                    services.AddMemoryCache();
                    services.AddDistributedMemoryCache();
                    services.AddSession();
                });

            MyViewComponent<FullSessionComponent>
                .Instance()
                .WithSession(session => session
                    .WithEntries(new Dictionary<string, string>
                    {
                        ["InvalidEntry"] = "stringTest"
                    }))
                .InvokedWith(c => c.Invoke())
                .ShouldReturn()
                .Content("Invalid");

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void WithEntriesAsIntDictionaryShouldSetCorrectEntryInViewComponent()
        {
            MyApplication
                .StartsFrom<DefaultStartup>()
                .WithServices(services =>
                {
                    services.AddMemoryCache();
                    services.AddDistributedMemoryCache();
                    services.AddSession();
                });

            MyViewComponent<FullSessionComponent>
                .Instance()
                .WithSession(session => session
                    .WithEntries(new Dictionary<string, int>
                    {
                        ["IntEntry"] = 1
                    }))
                .InvokedWith(c => c.Invoke())
                .ShouldReturn()
                .View(view => view
                    .WithModel(1));

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void WithEntriesAsIntDictionaryAndWrongKeyShouldNotSetCorrectEntryInViewComponent()
        {
            MyApplication
                .StartsFrom<DefaultStartup>()
                .WithServices(services =>
                {
                    services.AddMemoryCache();
                    services.AddDistributedMemoryCache();
                    services.AddSession();
                });

            MyViewComponent<FullSessionComponent>
                .Instance()
                .WithSession(session => session
                    .WithEntries(new Dictionary<string, int>
                    {
                        ["InvalidEntry"] = 1
                    }))
                .InvokedWith(c => c.Invoke())
                .ShouldReturn()
                .Content("Invalid");

            MyApplication.StartsFrom<DefaultStartup>();
        }
    }
}
