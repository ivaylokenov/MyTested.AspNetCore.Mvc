namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.DataTests
{
    using System.Collections.Generic;
    using Exceptions;
    using Microsoft.Extensions.DependencyInjection;
    using Mvc;
    using Setups;
    using Setups.Controllers;
    using Xunit;

    public class SessionTestBuilderTests
    {
        [Fact]
        public void ContainingEntryWithKeyShouldNotThrowExceptionWithCorrectEntry()
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
                .Calling(c => c.AddSessionAction())
                .ShouldHave()
                .Session(session => session
                    .ContainingEntryWithKey("Integer")
                    .AndAlso()
                    .ContainingEntryWithKey("String"))
                .AndAlso()
                .ShouldReturn()
                .Ok();

            MyApplication.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void ContainingEntryWithKeyShouldThrowExceptionWithIncorrectEntry()
        {
            MyApplication
                .IsUsingDefaultConfiguration()
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

            MyApplication.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void ContainingEntryWithByteValueShouldNotThrowExceptionWithCorrectEntry()
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
                .Calling(c => c.AddSessionAction())
                .ShouldHave()
                .Session(session => session.ContainingEntryWithValue(new byte[] { 1, 2, 3 }))
                .AndAlso()
                .ShouldReturn()
                .Ok();

            MyApplication.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void ContainingEntryWithByteValueShouldThrowExceptionWithIncorrectEntry()
        {
            MyApplication
                .IsUsingDefaultConfiguration()
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

            MyApplication.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void ContainingEntryWithStringValueShouldNotThrowExceptionWithCorrectEntry()
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
                .Calling(c => c.AddSessionAction())
                .ShouldHave()
                .Session(session => session.ContainingEntryWithValue("Text"))
                .AndAlso()
                .ShouldReturn()
                .Ok();

            MyApplication.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void ContainingEntryWithStringValueShouldThrowExceptionWithIncorrectEntry()
        {
            MyApplication
                .IsUsingDefaultConfiguration()
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

            MyApplication.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void ContainingEntryWithIntegerValueShouldNotThrowExceptionWithCorrectEntry()
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
                .Calling(c => c.AddSessionAction())
                .ShouldHave()
                .Session(session => session.ContainingEntryWithValue(1))
                .AndAlso()
                .ShouldReturn()
                .Ok();

            MyApplication.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void ContainingEntryWithIntegerValueShouldThrowExceptionWithIncorrectEntry()
        {
            MyApplication
                .IsUsingDefaultConfiguration()
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

            MyApplication.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void ContainingEntryShouldNotThrowExceptionWithCorrectEntry()
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
                .Calling(c => c.AddSessionAction())
                .ShouldHave()
                .Session(session => session.ContainingEntry("Bytes", new byte[] { 1, 2, 3 }))
                .AndAlso()
                .ShouldReturn()
                .Ok();

            MyApplication.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void ContainingEntryShouldThrowExceptionWithIncorrectEntry()
        {
            MyApplication
                .IsUsingDefaultConfiguration()
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

            MyApplication.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void ContainingStringEntryShouldNotThrowExceptionWithCorrectEntry()
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
                .Calling(c => c.AddSessionAction())
                .ShouldHave()
                .Session(session => session.ContainingEntry("String", "Text"))
                .AndAlso()
                .ShouldReturn()
                .Ok();

            MyApplication.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void ContainingStringEntryShouldThrowExceptionWithIncorrectEntry()
        {
            MyApplication
                .IsUsingDefaultConfiguration()
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

            MyApplication.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void ContainingIntegerEntryShouldNotThrowExceptionWithCorrectEntry()
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
                .Calling(c => c.AddSessionAction())
                .ShouldHave()
                .Session(session => session.ContainingEntry("Integer", 1))
                .AndAlso()
                .ShouldReturn()
                .Ok();

            MyApplication.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void ContainingIntegerEntryShouldThrowExceptionWithIncorrectEntry()
        {
            MyApplication
                .IsUsingDefaultConfiguration()
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

            MyApplication.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void ContainingEntriesWithObjectShouldNotThrowExceptionWithCorrectEntry()
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

            MyApplication.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void ContainingEntriesWithObjectShouldThrowExceptionWithIncorrectEntryKey()
        {
            MyApplication
                .IsUsingDefaultConfiguration()
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

            MyApplication.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void ContainingEntriesWithObjectShouldThrowExceptionWithIncorrectEntry()
        {
            MyApplication
                .IsUsingDefaultConfiguration()
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

            MyApplication.IsUsingDefaultConfiguration();
        }
        
        [Fact]
        public void ContainingEntriesWithObjectShouldThrowExceptionWithDifferentCount()
        {
            MyApplication
                .IsUsingDefaultConfiguration()
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

            MyApplication.IsUsingDefaultConfiguration();
        }
        
        [Fact]
        public void ContainingEntriesWithObjectDictionaryShouldNotThrowExceptionWithCorrectEntry()
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

            MyApplication.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void ContainingEntriesWithObjectDictionaryShouldThrowExceptionWithIncorrectEntryKey()
        {
            MyApplication
                .IsUsingDefaultConfiguration()
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

            MyApplication.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void ContainingEntriesWithObjectDictionaryShouldThrowExceptionWithIncorrectEntry()
        {
            MyApplication
                .IsUsingDefaultConfiguration()
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

            MyApplication.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void ContainingEntriesWithObjectDictionaryShouldThrowExceptionWithDifferentCount()
        {
            MyApplication
                .IsUsingDefaultConfiguration()
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

            MyApplication.IsUsingDefaultConfiguration();
        }
        
        [Fact]
        public void ContainingByteEntriesWithObjectDictionaryShouldNotThrowExceptionWithCorrectEntry()
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
                .Calling(c => c.AddSessionAction())
                .ShouldHave()
                .Session(session => session.ContainingEntries(new Dictionary<string, byte[]>
                {
                    ["Bytes"] = new byte[] { 1, 2, 3 }
                }))
                .AndAlso()
                .ShouldReturn()
                .Ok();

            MyApplication.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void ContainingByteEntriesWithObjectDictionaryShouldThrowExceptionWithIncorrectEntryKey()
        {
            MyApplication
                .IsUsingDefaultConfiguration()
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

            MyApplication.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void ContainingByteEntriesWithObjectDictionaryShouldThrowExceptionWithIncorrectEntry()
        {
            MyApplication
                .IsUsingDefaultConfiguration()
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

            MyApplication.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void ContainingByteEntriesWithObjectDictionaryShouldThrowExceptionWithDifferentCount()
        {
            MyApplication
                .IsUsingDefaultConfiguration()
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

            MyApplication.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void ContainingStringEntriesWithObjectDictionaryShouldNotThrowExceptionWithCorrectEntry()
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
                .Calling(c => c.AddSessionAction())
                .ShouldHave()
                .Session(session => session.ContainingEntries(new Dictionary<string, string>
                {
                    ["String"] = "Text"
                }))
                .AndAlso()
                .ShouldReturn()
                .Ok();

            MyApplication.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void ContainingStringEntriesWithObjectDictionaryShouldThrowExceptionWithIncorrectEntryKey()
        {
            MyApplication
                .IsUsingDefaultConfiguration()
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

            MyApplication.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void ContainingStringEntriesWithObjectDictionaryShouldThrowExceptionWithIncorrectEntry()
        {
            MyApplication
                .IsUsingDefaultConfiguration()
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

            MyApplication.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void ContainingStringEntriesWithObjectDictionaryShouldThrowExceptionWithDifferentCount()
        {
            MyApplication
                .IsUsingDefaultConfiguration()
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

            MyApplication.IsUsingDefaultConfiguration();
        }
        
        [Fact]
        public void ContainingIntegerEntriesWithObjectDictionaryShouldNotThrowExceptionWithCorrectEntry()
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
                .Calling(c => c.AddSessionAction())
                .ShouldHave()
                .Session(session => session.ContainingEntries(new Dictionary<string, int>
                {
                    ["Integer"] = 1
                }))
                .AndAlso()
                .ShouldReturn()
                .Ok();

            MyApplication.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void ContainingIntegerEntriesWithObjectDictionaryShouldThrowExceptionWithIncorrectEntryKey()
        {
            MyApplication
                .IsUsingDefaultConfiguration()
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

            MyApplication.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void ContainingIntegerEntriesWithObjectDictionaryShouldThrowExceptionWithIncorrectEntry()
        {
            MyApplication
                .IsUsingDefaultConfiguration()
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

            MyApplication.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void ContainingIntegerEntriesWithObjectDictionaryShouldThrowExceptionWithDifferentCount()
        {
            MyApplication
                .IsUsingDefaultConfiguration()
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

            MyApplication.IsUsingDefaultConfiguration();
        }
    }
}
