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
                .Calling(c => c.AddSessionAction())
                .ShouldHave()
                .Session(session => session
                    .ContainingEntryWithKey("Integer")
                    .AndAlso()
                    .ContainingEntryWithKey("String"))
                .AndAlso()
                .ShouldReturn()
                .Ok();

            MyMvc.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void ContainingEntryWithKeyShouldThrowExceptionWithIncorrectEntry()
        {
            MyMvc
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
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.AddSessionAction())
                        .ShouldHave()
                        .Session(session => session.ContainingEntryWithKey("Invalid"))
                        .AndAlso()
                        .ShouldReturn()
                        .Ok();
                },
                "When calling AddSessionAction action in MvcController expected session to have entry with 'Invalid' key, but such was not found.");

            MyMvc.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void ContainingEntryWithByteValueShouldNotThrowExceptionWithCorrectEntry()
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
                .Calling(c => c.AddSessionAction())
                .ShouldHave()
                .Session(session => session.ContainingEntryWithValue(new byte[] { 1, 2, 3 }))
                .AndAlso()
                .ShouldReturn()
                .Ok();

            MyMvc.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void ContainingEntryWithByteValueShouldThrowExceptionWithIncorrectEntry()
        {
            MyMvc
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
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.AddSessionAction())
                        .ShouldHave()
                        .Session(session => session.ContainingEntryWithValue(new byte[] { 1, 2, 4 }))
                        .AndAlso()
                        .ShouldReturn()
                        .Ok();
                },
                "When calling AddSessionAction action in MvcController expected session to have entry with the provided value, but none was found.");

            MyMvc.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void ContainingEntryWithStringValueShouldNotThrowExceptionWithCorrectEntry()
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
                .Calling(c => c.AddSessionAction())
                .ShouldHave()
                .Session(session => session.ContainingEntryWithValue("Text"))
                .AndAlso()
                .ShouldReturn()
                .Ok();

            MyMvc.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void ContainingEntryWithStringValueShouldThrowExceptionWithIncorrectEntry()
        {
            MyMvc
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
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.AddSessionAction())
                        .ShouldHave()
                        .Session(session => session.ContainingEntryWithValue("Invalid"))
                        .AndAlso()
                        .ShouldReturn()
                        .Ok();
                },
                "When calling AddSessionAction action in MvcController expected session to have entry with the provided value, but none was found.");

            MyMvc.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void ContainingEntryWithIntegerValueShouldNotThrowExceptionWithCorrectEntry()
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
                .Calling(c => c.AddSessionAction())
                .ShouldHave()
                .Session(session => session.ContainingEntryWithValue(1))
                .AndAlso()
                .ShouldReturn()
                .Ok();

            MyMvc.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void ContainingEntryWithIntegerValueShouldThrowExceptionWithIncorrectEntry()
        {
            MyMvc
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
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.AddSessionAction())
                        .ShouldHave()
                        .Session(session => session.ContainingEntryWithValue(2))
                        .AndAlso()
                        .ShouldReturn()
                        .Ok();
                },
                "When calling AddSessionAction action in MvcController expected session to have entry with the provided value, but none was found.");

            MyMvc.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void ContainingEntryShouldNotThrowExceptionWithCorrectEntry()
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
                .Calling(c => c.AddSessionAction())
                .ShouldHave()
                .Session(session => session.ContainingEntry("Bytes", new byte[] { 1, 2, 3 }))
                .AndAlso()
                .ShouldReturn()
                .Ok();

            MyMvc.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void ContainingEntryShouldThrowExceptionWithIncorrectEntry()
        {
            MyMvc
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
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.AddSessionAction())
                        .ShouldHave()
                        .Session(session => session.ContainingEntry("Bytes", new byte[] { 1, 2, 4 }))
                        .AndAlso()
                        .ShouldReturn()
                        .Ok();
                },
                "When calling AddSessionAction action in MvcController expected session to have entry with 'Bytes' key and the provided value, but the value was different.");

            MyMvc.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void ContainingStringEntryShouldNotThrowExceptionWithCorrectEntry()
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
                .Calling(c => c.AddSessionAction())
                .ShouldHave()
                .Session(session => session.ContainingEntry("String", "Text"))
                .AndAlso()
                .ShouldReturn()
                .Ok();

            MyMvc.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void ContainingStringEntryShouldThrowExceptionWithIncorrectEntry()
        {
            MyMvc
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
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.AddSessionAction())
                        .ShouldHave()
                        .Session(session => session.ContainingEntry("String", "Invalid"))
                        .AndAlso()
                        .ShouldReturn()
                        .Ok();
                },
                "When calling AddSessionAction action in MvcController expected session to have entry with 'String' key and the provided value, but the value was different.");

            MyMvc.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void ContainingIntegerEntryShouldNotThrowExceptionWithCorrectEntry()
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
                .Calling(c => c.AddSessionAction())
                .ShouldHave()
                .Session(session => session.ContainingEntry("Integer", 1))
                .AndAlso()
                .ShouldReturn()
                .Ok();

            MyMvc.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void ContainingIntegerEntryShouldThrowExceptionWithIncorrectEntry()
        {
            MyMvc
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
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.AddSessionAction())
                        .ShouldHave()
                        .Session(session => session.ContainingEntry("Integer", 2))
                        .AndAlso()
                        .ShouldReturn()
                        .Ok();
                },
                "When calling AddSessionAction action in MvcController expected session to have entry with 'Integer' key and the provided value, but the value was different.");

            MyMvc.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void ContainingEntriesWithObjectShouldNotThrowExceptionWithCorrectEntry()
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

            MyMvc.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void ContainingEntriesWithObjectShouldThrowExceptionWithIncorrectEntryKey()
        {
            MyMvc
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
                    MyMvc
                        .Controller<MvcController>()
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

            MyMvc.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void ContainingEntriesWithObjectShouldThrowExceptionWithIncorrectEntry()
        {
            MyMvc
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
                    MyMvc
                        .Controller<MvcController>()
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

            MyMvc.IsUsingDefaultConfiguration();
        }
        
        [Fact]
        public void ContainingEntriesWithObjectShouldThrowExceptionWithDifferentCount()
        {
            MyMvc
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
                    MyMvc
                        .Controller<MvcController>()
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

            MyMvc.IsUsingDefaultConfiguration();
        }
        
        [Fact]
        public void ContainingEntriesWithObjectDictionaryShouldNotThrowExceptionWithCorrectEntry()
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

            MyMvc.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void ContainingEntriesWithObjectDictionaryShouldThrowExceptionWithIncorrectEntryKey()
        {
            MyMvc
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
                    MyMvc
                        .Controller<MvcController>()
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

            MyMvc.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void ContainingEntriesWithObjectDictionaryShouldThrowExceptionWithIncorrectEntry()
        {
            MyMvc
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
                    MyMvc
                        .Controller<MvcController>()
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

            MyMvc.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void ContainingEntriesWithObjectDictionaryShouldThrowExceptionWithDifferentCount()
        {
            MyMvc
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
                    MyMvc
                        .Controller<MvcController>()
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

            MyMvc.IsUsingDefaultConfiguration();
        }
        
        [Fact]
        public void ContainingByteEntriesWithObjectDictionaryShouldNotThrowExceptionWithCorrectEntry()
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
                .Calling(c => c.AddSessionAction())
                .ShouldHave()
                .Session(session => session.ContainingEntries(new Dictionary<string, byte[]>
                {
                    ["Bytes"] = new byte[] { 1, 2, 3 }
                }))
                .AndAlso()
                .ShouldReturn()
                .Ok();

            MyMvc.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void ContainingByteEntriesWithObjectDictionaryShouldThrowExceptionWithIncorrectEntryKey()
        {
            MyMvc
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
                    MyMvc
                        .Controller<MvcController>()
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

            MyMvc.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void ContainingByteEntriesWithObjectDictionaryShouldThrowExceptionWithIncorrectEntry()
        {
            MyMvc
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
                    MyMvc
                        .Controller<MvcController>()
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

            MyMvc.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void ContainingByteEntriesWithObjectDictionaryShouldThrowExceptionWithDifferentCount()
        {
            MyMvc
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
                    MyMvc
                        .Controller<MvcController>()
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

            MyMvc.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void ContainingStringEntriesWithObjectDictionaryShouldNotThrowExceptionWithCorrectEntry()
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
                .Calling(c => c.AddSessionAction())
                .ShouldHave()
                .Session(session => session.ContainingEntries(new Dictionary<string, string>
                {
                    ["String"] = "Text"
                }))
                .AndAlso()
                .ShouldReturn()
                .Ok();

            MyMvc.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void ContainingStringEntriesWithObjectDictionaryShouldThrowExceptionWithIncorrectEntryKey()
        {
            MyMvc
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
                    MyMvc
                        .Controller<MvcController>()
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

            MyMvc.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void ContainingStringEntriesWithObjectDictionaryShouldThrowExceptionWithIncorrectEntry()
        {
            MyMvc
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
                    MyMvc
                        .Controller<MvcController>()
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

            MyMvc.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void ContainingStringEntriesWithObjectDictionaryShouldThrowExceptionWithDifferentCount()
        {
            MyMvc
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
                    MyMvc
                        .Controller<MvcController>()
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

            MyMvc.IsUsingDefaultConfiguration();
        }
        
        [Fact]
        public void ContainingIntegerEntriesWithObjectDictionaryShouldNotThrowExceptionWithCorrectEntry()
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
                .Calling(c => c.AddSessionAction())
                .ShouldHave()
                .Session(session => session.ContainingEntries(new Dictionary<string, int>
                {
                    ["Integer"] = 1
                }))
                .AndAlso()
                .ShouldReturn()
                .Ok();

            MyMvc.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void ContainingIntegerEntriesWithObjectDictionaryShouldThrowExceptionWithIncorrectEntryKey()
        {
            MyMvc
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
                    MyMvc
                        .Controller<MvcController>()
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

            MyMvc.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void ContainingIntegerEntriesWithObjectDictionaryShouldThrowExceptionWithIncorrectEntry()
        {
            MyMvc
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
                    MyMvc
                        .Controller<MvcController>()
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

            MyMvc.IsUsingDefaultConfiguration();
        }

        [Fact]
        public void ContainingIntegerEntriesWithObjectDictionaryShouldThrowExceptionWithDifferentCount()
        {
            MyMvc
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
                    MyMvc
                        .Controller<MvcController>()
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

            MyMvc.IsUsingDefaultConfiguration();
        }
    }
}
