namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.DataTests
{
    using System.Collections.Generic;
    using Exceptions;
    using Microsoft.Extensions.DependencyInjection;
    using Mvc;
    using Setups;
    using Setups.Controllers;
    using Xunit;
    using Setups.ViewComponents;

    public class SessionTestBuilderTests
    {
        [Fact]
        public void ContainingEntryWithKeyShouldNotThrowExceptionWithCorrectEntry()
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
                .Calling(c => c.AddSessionAction())
                .ShouldHave()
                .Session(session => session
                    .ContainingEntryWithKey("Integer")
                    .AndAlso()
                    .ContainingEntryWithKey("String"))
                .AndAlso()
                .ShouldReturn()
                .Ok();

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void ContainingEntryWithKeyShouldThrowExceptionWithIncorrectEntry()
        {
            MyApplication
                .StartsFrom<DefaultStartup>()
                .WithServices(services =>
                {
                    services.AddMemoryCache();
                    services.AddDistributedMemoryCache();
                    services.AddSession();
                });

            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.AddSessionAction())
                        .ShouldHave()
                        .Session(session => session.ContainingEntryWithKey("Invalid"))
                        .AndAlso()
                        .ShouldReturn()
                        .Ok();
                },
                "When calling AddSessionAction action in MvcController expected session to have entry with 'Invalid' key, but such was not found.");

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void ContainingEntryWithByteValueShouldNotThrowExceptionWithCorrectEntry()
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
                .Calling(c => c.AddSessionAction())
                .ShouldHave()
                .Session(session => session.ContainingEntryWithValue(new byte[] { 1, 2, 3 }))
                .AndAlso()
                .ShouldReturn()
                .Ok();

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void ContainingEntryWithByteValueShouldThrowExceptionWithIncorrectEntry()
        {
            MyApplication
                .StartsFrom<DefaultStartup>()
                .WithServices(services =>
                {
                    services.AddMemoryCache();
                    services.AddDistributedMemoryCache();
                    services.AddSession();
                });

            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.AddSessionAction())
                        .ShouldHave()
                        .Session(session => session.ContainingEntryWithValue(new byte[] { 1, 2, 4 }))
                        .AndAlso()
                        .ShouldReturn()
                        .Ok();
                },
                "When calling AddSessionAction action in MvcController expected session to have entry with the provided value, but none was found.");

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void ContainingEntryWithStringValueShouldNotThrowExceptionWithCorrectEntry()
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
                .Calling(c => c.AddSessionAction())
                .ShouldHave()
                .Session(session => session.ContainingEntryWithValue("Text"))
                .AndAlso()
                .ShouldReturn()
                .Ok();

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void ContainingEntryWithStringValueShouldThrowExceptionWithIncorrectEntry()
        {
            MyApplication
                .StartsFrom<DefaultStartup>()
                .WithServices(services =>
                {
                    services.AddMemoryCache();
                    services.AddDistributedMemoryCache();
                    services.AddSession();
                });

            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.AddSessionAction())
                        .ShouldHave()
                        .Session(session => session.ContainingEntryWithValue("Invalid"))
                        .AndAlso()
                        .ShouldReturn()
                        .Ok();
                },
                "When calling AddSessionAction action in MvcController expected session to have entry with the provided value, but none was found.");

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void ContainingEntryWithIntegerValueShouldNotThrowExceptionWithCorrectEntry()
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
                .Calling(c => c.AddSessionAction())
                .ShouldHave()
                .Session(session => session.ContainingEntryWithValue(1))
                .AndAlso()
                .ShouldReturn()
                .Ok();

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void ContainingEntryWithIntegerValueShouldThrowExceptionWithIncorrectEntry()
        {
            MyApplication
                .StartsFrom<DefaultStartup>()
                .WithServices(services =>
                {
                    services.AddMemoryCache();
                    services.AddDistributedMemoryCache();
                    services.AddSession();
                });

            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.AddSessionAction())
                        .ShouldHave()
                        .Session(session => session.ContainingEntryWithValue(2))
                        .AndAlso()
                        .ShouldReturn()
                        .Ok();
                },
                "When calling AddSessionAction action in MvcController expected session to have entry with the provided value, but none was found.");

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void ContainingEntryShouldNotThrowExceptionWithCorrectEntry()
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
                .Calling(c => c.AddSessionAction())
                .ShouldHave()
                .Session(session => session.ContainingEntry("Bytes", new byte[] { 1, 2, 3 }))
                .AndAlso()
                .ShouldReturn()
                .Ok();

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void ContainingEntryShouldThrowExceptionWithIncorrectEntryValue()
        {
            MyApplication
                .StartsFrom<DefaultStartup>()
                .WithServices(services =>
                {
                    services.AddMemoryCache();
                    services.AddDistributedMemoryCache();
                    services.AddSession();
                });

            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.AddSessionAction())
                        .ShouldHave()
                        .Session(session => session.ContainingEntry("Bytes", new byte[] { 1, 2, 4 }))
                        .AndAlso()
                        .ShouldReturn()
                        .Ok();
                },
                "When calling AddSessionAction action in MvcController expected session to have entry with 'Bytes' key and the provided value, but the value was different.");

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void ContainingEntryShouldThrowExceptionWithIncorrectEntryKey()
        {
            MyApplication
                .StartsFrom<DefaultStartup>()
                .WithServices(services =>
                {
                    services.AddMemoryCache();
                    services.AddDistributedMemoryCache();
                    services.AddSession();
                });

            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.AddSessionAction())
                        .ShouldHave()
                        .Session(session => session.ContainingEntry("Invalid", new byte[] { 1, 2, 3 }))
                        .AndAlso()
                        .ShouldReturn()
                        .Ok();
                },
                "When calling AddSessionAction action in MvcController expected session to have entry with 'Invalid' key and the provided value, but such was not found.");

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void ContainingStringEntryShouldNotThrowExceptionWithCorrectEntry()
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
                .Calling(c => c.AddSessionAction())
                .ShouldHave()
                .Session(session => session.ContainingEntry("String", "Text"))
                .AndAlso()
                .ShouldReturn()
                .Ok();

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void ContainingStringEntryShouldThrowExceptionWithIncorrectEntryValue()
        {
            MyApplication
                .StartsFrom<DefaultStartup>()
                .WithServices(services =>
                {
                    services.AddMemoryCache();
                    services.AddDistributedMemoryCache();
                    services.AddSession();
                });

            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.AddSessionAction())
                        .ShouldHave()
                        .Session(session => session.ContainingEntry("String", "Invalid"))
                        .AndAlso()
                        .ShouldReturn()
                        .Ok();
                },
                "When calling AddSessionAction action in MvcController expected session to have entry with 'String' key and the provided value, but the value was different.");

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void ContainingStringEntryShouldThrowExceptionWithIncorrectEntryKey()
        {
            MyApplication
                .StartsFrom<DefaultStartup>()
                .WithServices(services =>
                {
                    services.AddMemoryCache();
                    services.AddDistributedMemoryCache();
                    services.AddSession();
                });

            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.AddSessionAction())
                        .ShouldHave()
                        .Session(session => session.ContainingEntry("Invalid", "Text"))
                        .AndAlso()
                        .ShouldReturn()
                        .Ok();
                },
                "When calling AddSessionAction action in MvcController expected session to have entry with 'Invalid' key and the provided value, but such was not found.");

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void ContainingIntegerEntryShouldNotThrowExceptionWithCorrectEntry()
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
                .Calling(c => c.AddSessionAction())
                .ShouldHave()
                .Session(session => session.ContainingEntry("Integer", 1))
                .AndAlso()
                .ShouldReturn()
                .Ok();

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void ContainingIntegerEntryShouldThrowExceptionWithIncorrectEntryValue()
        {
            MyApplication
                .StartsFrom<DefaultStartup>()
                .WithServices(services =>
                {
                    services.AddMemoryCache();
                    services.AddDistributedMemoryCache();
                    services.AddSession();
                });

            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.AddSessionAction())
                        .ShouldHave()
                        .Session(session => session.ContainingEntry("Integer", 2))
                        .AndAlso()
                        .ShouldReturn()
                        .Ok();
                },
                "When calling AddSessionAction action in MvcController expected session to have entry with 'Integer' key and the provided value, but the value was different.");

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void ContainingIntegerEntryShouldThrowExceptionWithIncorrectEntryKey()
        {
            MyApplication
                .StartsFrom<DefaultStartup>()
                .WithServices(services =>
                {
                    services.AddMemoryCache();
                    services.AddDistributedMemoryCache();
                    services.AddSession();
                });

            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.AddSessionAction())
                        .ShouldHave()
                        .Session(session => session.ContainingEntry("Invalid", 1))
                        .AndAlso()
                        .ShouldReturn()
                        .Ok();
                },
                "When calling AddSessionAction action in MvcController expected session to have entry with 'Invalid' key and the provided value, but such was not found.");

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void ContainingEntriesWithObjectShouldNotThrowExceptionWithCorrectEntry()
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
                .Calling(c => c.AddSessionAction())
                .ShouldHave()
                .Session(session => session.ContainingEntries(new
                {
                    Integer = 1,
                    String = "Text",
                    Bytes = new byte[] { 1, 2, 3 }
                }))
                .AndAlso()
                .ShouldReturn()
                .Ok();

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void ContainingEntriesWithObjectShouldThrowExceptionWithIncorrectEntryKey()
        {
            MyApplication
                .StartsFrom<DefaultStartup>()
                .WithServices(services =>
                {
                    services.AddMemoryCache();
                    services.AddDistributedMemoryCache();
                    services.AddSession();
                });

            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.AddSessionAction())
                        .ShouldHave()
                        .Session(session => session.ContainingEntries(new
                        {
                            Integer = 1,
                            Strin = "Text",
                            Bytes = new byte[] { 1, 2, 3 }
                        }))
                        .AndAlso()
                        .ShouldReturn()
                        .Ok();
                },
                "When calling AddSessionAction action in MvcController expected session to have entry with 'Strin' key and the provided value, but such was not found.");

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void ContainingEntriesWithObjectShouldThrowExceptionWithIncorrectEntry()
        {
            MyApplication
                .StartsFrom<DefaultStartup>()
                .WithServices(services =>
                {
                    services.AddMemoryCache();
                    services.AddDistributedMemoryCache();
                    services.AddSession();
                });

            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.AddSessionAction())
                        .ShouldHave()
                        .Session(session => session.ContainingEntries(new
                        {
                            Integer = 1,
                            String = "Invalid",
                            Bytes = new byte[] { 1, 2, 3 }
                        }))
                        .AndAlso()
                        .ShouldReturn()
                        .Ok();
                },
                "When calling AddSessionAction action in MvcController expected session to have entry with 'String' key and the provided value, but the value was different.");

            MyApplication.StartsFrom<DefaultStartup>();
        }
        
        [Fact]
        public void ContainingEntriesWithObjectShouldThrowExceptionWithDifferentCount()
        {
            MyApplication
                .StartsFrom<DefaultStartup>()
                .WithServices(services =>
                {
                    services.AddMemoryCache();
                    services.AddDistributedMemoryCache();
                    services.AddSession();
                });

            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.AddSessionAction())
                        .ShouldHave()
                        .Session(session => session.ContainingEntries(new
                        {
                            Integer = 1,
                            Bytes = new byte[] { 1, 2, 3 }
                        }))
                        .AndAlso()
                        .ShouldReturn()
                        .Ok();
                },
                "When calling AddSessionAction action in MvcController expected session to have 2 entries, but in fact found 3.");

            MyApplication.StartsFrom<DefaultStartup>();
        }
        
        [Fact]
        public void ContainingEntriesWithObjectDictionaryShouldNotThrowExceptionWithCorrectEntry()
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
                .Calling(c => c.AddSessionAction())
                .ShouldHave()
                .Session(session => session.ContainingEntries(new Dictionary<string, object>
                {
                    ["Integer"] = 1,
                    ["String"] = "Text",
                    ["Bytes"] = new byte[] { 1, 2, 3 }
                }))
                .AndAlso()
                .ShouldReturn()
                .Ok();

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void ContainingEntriesWithObjectDictionaryShouldThrowExceptionWithIncorrectEntryKey()
        {
            MyApplication
                .StartsFrom<DefaultStartup>()
                .WithServices(services =>
                {
                    services.AddMemoryCache();
                    services.AddDistributedMemoryCache();
                    services.AddSession();
                });

            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.AddSessionAction())
                        .ShouldHave()
                        .Session(session => session.ContainingEntries(new Dictionary<string, object>
                        {
                            ["Integer"] = 1,
                            ["Strin"] = "Text",
                            ["Bytes"] = new byte[] { 1, 2, 3 }
                        }))
                        .AndAlso()
                        .ShouldReturn()
                        .Ok();
                },
                "When calling AddSessionAction action in MvcController expected session to have entry with 'Strin' key and the provided value, but such was not found.");

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void ContainingEntriesWithObjectDictionaryShouldThrowExceptionWithIncorrectEntry()
        {
            MyApplication
                .StartsFrom<DefaultStartup>()
                .WithServices(services =>
                {
                    services.AddMemoryCache();
                    services.AddDistributedMemoryCache();
                    services.AddSession();
                });

            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.AddSessionAction())
                        .ShouldHave()
                        .Session(session => session.ContainingEntries(new Dictionary<string, object>
                        {
                            ["Integer"] = 1,
                            ["String"] = "Invalid",
                            ["Bytes"] = new byte[] { 1, 2, 3 }
                        }))
                        .AndAlso()
                        .ShouldReturn()
                        .Ok();
                },
                "When calling AddSessionAction action in MvcController expected session to have entry with 'String' key and the provided value, but the value was different.");

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void ContainingEntriesWithObjectDictionaryShouldThrowExceptionWithDifferentCount()
        {
            MyApplication
                .StartsFrom<DefaultStartup>()
                .WithServices(services =>
                {
                    services.AddMemoryCache();
                    services.AddDistributedMemoryCache();
                    services.AddSession();
                });

            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.AddSessionAction())
                        .ShouldHave()
                        .Session(session => session.ContainingEntries(new Dictionary<string, object>
                        {
                            ["Integer"] = 1,
                            ["Bytes"] = new byte[] { 1, 2, 3 }
                        }))
                        .AndAlso()
                        .ShouldReturn()
                        .Ok();
                },
                "When calling AddSessionAction action in MvcController expected session to have 2 entries, but in fact found 3.");

            MyApplication.StartsFrom<DefaultStartup>();
        }
        
        [Fact]
        public void ContainingByteEntriesWithObjectDictionaryShouldNotThrowExceptionWithCorrectEntry()
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
                .Calling(c => c.AddSessionAction())
                .ShouldHave()
                .Session(session => session.ContainingEntries(new Dictionary<string, byte[]>
                {
                    ["Bytes"] = new byte[] { 1, 2, 3 }
                }))
                .AndAlso()
                .ShouldReturn()
                .Ok();

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void ContainingByteEntriesWithObjectDictionaryShouldThrowExceptionWithIncorrectEntryKey()
        {
            MyApplication
                .StartsFrom<DefaultStartup>()
                .WithServices(services =>
                {
                    services.AddMemoryCache();
                    services.AddDistributedMemoryCache();
                    services.AddSession();
                });

            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.AddSessionAction())
                        .ShouldHave()
                        .Session(session => session.ContainingEntries(new Dictionary<string, byte[]>
                        {
                            ["Byte"] = new byte[] { 1, 2, 3 }
                        }))
                        .AndAlso()
                        .ShouldReturn()
                        .Ok();
                },
                "When calling AddSessionAction action in MvcController expected session to have entry with 'Byte' key and the provided value, but such was not found.");

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void ContainingByteEntriesWithObjectDictionaryShouldThrowExceptionWithIncorrectEntry()
        {
            MyApplication
                .StartsFrom<DefaultStartup>()
                .WithServices(services =>
                {
                    services.AddMemoryCache();
                    services.AddDistributedMemoryCache();
                    services.AddSession();
                });

            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.AddSessionAction())
                        .ShouldHave()
                        .Session(session => session.ContainingEntries(new Dictionary<string, byte[]>
                        {
                            ["Bytes"] = new byte[] { 1, 2, 3, 4 }
                        }))
                        .AndAlso()
                        .ShouldReturn()
                        .Ok();
                },
                "When calling AddSessionAction action in MvcController expected session to have entry with 'Bytes' key and the provided value, but the value was different.");

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void ContainingByteEntriesWithObjectDictionaryShouldThrowExceptionWithDifferentCount()
        {
            MyApplication
                .StartsFrom<DefaultStartup>()
                .WithServices(services =>
                {
                    services.AddMemoryCache();
                    services.AddDistributedMemoryCache();
                    services.AddSession();
                });

            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.AddSessionAction())
                        .ShouldHave()
                        .Session(session => session.ContainingEntries(new Dictionary<string, byte[]>
                        {
                            ["Integer"] = new byte[] { 4, 5 },
                            ["Bytes"] = new byte[] { 1, 2, 3 }
                        }))
                        .AndAlso()
                        .ShouldReturn()
                        .Ok();
                },
                "When calling AddSessionAction action in MvcController expected session to have entry with 'Integer' key and the provided value, but the value was different.");

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void ContainingStringEntriesWithObjectDictionaryShouldNotThrowExceptionWithCorrectEntry()
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
                .Calling(c => c.AddSessionAction())
                .ShouldHave()
                .Session(session => session.ContainingEntries(new Dictionary<string, string>
                {
                    ["String"] = "Text"
                }))
                .AndAlso()
                .ShouldReturn()
                .Ok();

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void ContainingStringEntriesWithObjectDictionaryShouldThrowExceptionWithIncorrectEntryKey()
        {
            MyApplication
                .StartsFrom<DefaultStartup>()
                .WithServices(services =>
                {
                    services.AddMemoryCache();
                    services.AddDistributedMemoryCache();
                    services.AddSession();
                });

            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.AddSessionAction())
                        .ShouldHave()
                        .Session(session => session.ContainingEntries(new Dictionary<string, string>
                        {
                            ["Strin"] = "Text"
                        }))
                        .AndAlso()
                        .ShouldReturn()
                        .Ok();
                },
                "When calling AddSessionAction action in MvcController expected session to have entry with 'Strin' key and the provided value, but such was not found.");

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void ContainingStringEntriesWithObjectDictionaryShouldThrowExceptionWithIncorrectEntry()
        {
            MyApplication
                .StartsFrom<DefaultStartup>()
                .WithServices(services =>
                {
                    services.AddMemoryCache();
                    services.AddDistributedMemoryCache();
                    services.AddSession();
                });

            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.AddSessionAction())
                        .ShouldHave()
                        .Session(session => session.ContainingEntries(new Dictionary<string, string>
                        {
                            ["String"] = "Invalid"
                        }))
                        .AndAlso()
                        .ShouldReturn()
                        .Ok();
                },
                "When calling AddSessionAction action in MvcController expected session to have entry with 'String' key and the provided value, but the value was different.");

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void ContainingStringEntriesWithObjectDictionaryShouldThrowExceptionWithDifferentCount()
        {
            MyApplication
                .StartsFrom<DefaultStartup>()
                .WithServices(services =>
                {
                    services.AddMemoryCache();
                    services.AddDistributedMemoryCache();
                    services.AddSession();
                });

            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.AddSessionAction())
                        .ShouldHave()
                        .Session(session => session.ContainingEntries(new Dictionary<string, string>
                        {
                            ["Integer"] = "Invalid",
                            ["String"] = "Text"
                        }))
                        .AndAlso()
                        .ShouldReturn()
                        .Ok();
                },
                "When calling AddSessionAction action in MvcController expected session to have entry with 'Integer' key and the provided value, but the value was different.");

            MyApplication.StartsFrom<DefaultStartup>();
        }
        
        [Fact]
        public void ContainingIntegerEntriesWithObjectDictionaryShouldNotThrowExceptionWithCorrectEntry()
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
                .Calling(c => c.AddSessionAction())
                .ShouldHave()
                .Session(session => session.ContainingEntries(new Dictionary<string, int>
                {
                    ["Integer"] = 1
                }))
                .AndAlso()
                .ShouldReturn()
                .Ok();

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void ContainingIntegerEntriesWithObjectDictionaryShouldThrowExceptionWithIncorrectEntryKey()
        {
            MyApplication
                .StartsFrom<DefaultStartup>()
                .WithServices(services =>
                {
                    services.AddMemoryCache();
                    services.AddDistributedMemoryCache();
                    services.AddSession();
                });

            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.AddSessionAction())
                        .ShouldHave()
                        .Session(session => session.ContainingEntries(new Dictionary<string, int>
                        {
                            ["Intege"] = 1
                        }))
                        .AndAlso()
                        .ShouldReturn()
                        .Ok();
                },
                "When calling AddSessionAction action in MvcController expected session to have entry with 'Intege' key and the provided value, but such was not found.");

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void ContainingIntegerEntriesWithObjectDictionaryShouldThrowExceptionWithIncorrectEntry()
        {
            MyApplication
                .StartsFrom<DefaultStartup>()
                .WithServices(services =>
                {
                    services.AddMemoryCache();
                    services.AddDistributedMemoryCache();
                    services.AddSession();
                });

            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.AddSessionAction())
                        .ShouldHave()
                        .Session(session => session.ContainingEntries(new Dictionary<string, int>
                        {
                            ["Integer"] = 2
                        }))
                        .AndAlso()
                        .ShouldReturn()
                        .Ok();
                },
                "When calling AddSessionAction action in MvcController expected session to have entry with 'Integer' key and the provided value, but the value was different.");

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void ContainingIntegerEntriesWithObjectDictionaryShouldThrowExceptionWithDifferentCount()
        {
            MyApplication
                .StartsFrom<DefaultStartup>()
                .WithServices(services =>
                {
                    services.AddMemoryCache();
                    services.AddDistributedMemoryCache();
                    services.AddSession();
                });

            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.AddSessionAction())
                        .ShouldHave()
                        .Session(session => session.ContainingEntries(new Dictionary<string, int>
                        {
                            ["Integer"] = 1,
                            ["String"] = 2
                        }))
                        .AndAlso()
                        .ShouldReturn()
                        .Ok();
                },
                "When calling AddSessionAction action in MvcController expected session to have entry with 'String' key and the provided value, but the value was different.");

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void ContainingEntryWithKeyShouldNotThrowExceptionWithCorrectViewComponentEntry()
        {
            MyApplication
                .StartsFrom<DefaultStartup>()
                .WithServices(services =>
                {
                    services.AddMemoryCache();
                    services.AddDistributedMemoryCache();
                    services.AddSession();
                });

            MyViewComponent<AddSessionComponent>
                .Instance()
                .InvokedWith(c => c.Invoke())
                .ShouldHave()
                .Session(session => session
                    .ContainingEntryWithKey("Integer")
                    .AndAlso()
                    .ContainingEntryWithKey("String"))
                .AndAlso()
                .ShouldReturn()
                .View();

            MyApplication.StartsFrom<DefaultStartup>();
        }

        [Fact]
        public void ContainingEntryWithKeyShouldThrowExceptionWithIncorrectViewComponentEntry()
        {
            MyApplication
                .StartsFrom<DefaultStartup>()
                .WithServices(services =>
                {
                    services.AddMemoryCache();
                    services.AddDistributedMemoryCache();
                    services.AddSession();
                });

            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyViewComponent<AddSessionComponent>
                        .Instance()
                        .InvokedWith(c => c.Invoke())
                        .ShouldHave()
                        .Session(session => session.ContainingEntryWithKey("Invalid"))
                        .AndAlso()
                        .ShouldReturn()
                        .View();
                },
                "When invoking AddSessionComponent expected session to have entry with 'Invalid' key, but such was not found.");

            MyApplication.StartsFrom<DefaultStartup>();
        }
    }
}
