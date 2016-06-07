namespace MyTested.Mvc.Test.BuildersTests.DataTests
{
    using System;
    using System.Collections.Generic;
    using Exceptions;
    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.Extensions.DependencyInjection;
    using Setups;
    using Setups.Controllers;
    using Xunit;

    public class MemoryCacheTestBuilderTests : IDisposable
    {
        public MemoryCacheTestBuilderTests()
        {
            MyMvc
                .IsUsingDefaultConfiguration()
                .WithServices(services => services.AddMemoryCache());
        }

        [Fact]
        public void ContainingEntryWithKeyShouldNotThrowExceptionWithCorrectEntry()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.AddMemoryCacheAction())
                .ShouldHave()
                .MemoryCache(cache => cache
                    .ContainingEntryWithKey("test")
                    .AndAlso()
                    .ContainingEntryWithValue("value"))
                .AndAlso()
                .ShouldReturn()
                .Ok();
        }
        
        [Fact]
        public void ContainingEntryWithKeyShouldThrowExceptionWithIncorrectEntry()
        {
            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyMvc
                       .Controller<MvcController>()
                       .Calling(c => c.AddMemoryCacheAction())
                       .ShouldHave()
                       .MemoryCache(cache => cache.ContainingEntryWithKey("invalid"))
                       .AndAlso()
                       .ShouldReturn()
                       .Ok();
                },
                "When calling AddMemoryCacheAction action in MvcController expected memory cache to have entry with the given key, but such was not found.");
        }
        
        [Fact]
        public void ContainingEntryWithValueShouldNotThrowExceptionWithCorrectEntry()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.AddMemoryCacheAction())
                .ShouldHave()
                .MemoryCache(cache => cache.ContainingEntryWithValue("value"))
                .AndAlso()
                .ShouldReturn()
                .Ok();
        }

        [Fact]
        public void ContainingEntryWithValueShouldThrowExceptionWithIncorrectEntry()
        {
            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyMvc
                       .Controller<MvcController>()
                       .Calling(c => c.AddMemoryCacheAction())
                       .ShouldHave()
                       .MemoryCache(cache => cache.ContainingEntryWithValue("invalid"))
                       .AndAlso()
                       .ShouldReturn()
                       .Ok();
                },
                "When calling AddMemoryCacheAction action in MvcController expected memory cache to have entry with the provided value, but none was found.");
        }
        
        [Fact]
        public void ContainingEntryOfTypeShouldNotThrowExceptionWithCorrectEntry()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.AddMemoryCacheAction())
                .ShouldHave()
                .MemoryCache(cache => cache.ContainingEntryOfType<string>())
                .AndAlso()
                .ShouldReturn()
                .Ok();
        }

        [Fact]
        public void ContainingEntryOfTypeShouldThrowExceptionWithIncorrectEntry()
        {
            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyMvc
                       .Controller<MvcController>()
                       .Calling(c => c.AddMemoryCacheAction())
                       .ShouldHave()
                       .MemoryCache(cache => cache.ContainingEntryOfType<int>())
                       .AndAlso()
                       .ShouldReturn()
                       .Ok();
                },
                "When calling AddMemoryCacheAction action in MvcController expected memory cache to have at least one entry of Int32 type, but none was found.");
        }
        
        [Fact]
        public void ContainingEntryOfTypeAndKeyShouldNotThrowExceptionWithCorrectEntry()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.AddMemoryCacheAction())
                .ShouldHave()
                .MemoryCache(cache => cache.ContainingEntryOfType<string>("test"))
                .AndAlso()
                .ShouldReturn()
                .Ok();
        }

        [Fact]
        public void ContainingEntryOfTypeAndKeyShouldThrowExceptionWithIncorrectEntryKey()
        {
            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyMvc
                       .Controller<MvcController>()
                       .Calling(c => c.AddMemoryCacheAction())
                       .ShouldHave()
                       .MemoryCache(cache => cache.ContainingEntryOfType<string>("invalid"))
                       .AndAlso()
                       .ShouldReturn()
                       .Ok();
                },
                "When calling AddMemoryCacheAction action in MvcController expected memory cache to have entry with the given key, but such was not found.");
        }

        [Fact]
        public void ContainingEntryOfTypeAndKeyShouldThrowExceptionWithIncorrectEntry()
        {
            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyMvc
                       .Controller<MvcController>()
                       .Calling(c => c.AddMemoryCacheAction())
                       .ShouldHave()
                       .MemoryCache(cache => cache.ContainingEntryOfType<int>("test"))
                       .AndAlso()
                       .ShouldReturn()
                       .Ok();
                },
                "When calling AddMemoryCacheAction action in MvcController expected memory cache to have entry with the given key and value of Int32 type, but in fact found String.");
        }
        
        [Fact]
        public void ContainingEntryShouldNotThrowExceptionWithCorrectEntry()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.AddMemoryCacheAction())
                .ShouldHave()
                .MemoryCache(cache => cache.ContainingEntry("test", "value"))
                .AndAlso()
                .ShouldReturn()
                .Ok();
        }

        [Fact]
        public void ContainingEntryShouldThrowExceptionWithIncorrectEntryKey()
        {
            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyMvc
                       .Controller<MvcController>()
                       .Calling(c => c.AddMemoryCacheAction())
                       .ShouldHave()
                       .MemoryCache(cache => cache.ContainingEntry("invalid", "value"))
                       .AndAlso()
                       .ShouldReturn()
                       .Ok();
                },
                "When calling AddMemoryCacheAction action in MvcController expected memory cache to have entry with the given key, but such was not found.");
        }

        [Fact]
        public void ContainingEntryShouldThrowExceptionWithIncorrectEntry()
        {
            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyMvc
                       .Controller<MvcController>()
                       .Calling(c => c.AddMemoryCacheAction())
                       .ShouldHave()
                       .MemoryCache(cache => cache.ContainingEntry("test", "invalid"))
                       .AndAlso()
                       .ShouldReturn()
                       .Ok();
                },
                "When calling AddMemoryCacheAction action in MvcController expected memory cache to have entry with the given value, but in fact it was different.");
        }

        [Fact]
        public void ContainingEntryWithOptionsShouldNotThrowExceptionWithCorrectEntry()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.AddMemoryCacheAction())
                .ShouldHave()
                .MemoryCache(cache => cache.ContainingEntry("test", "value", new MemoryCacheEntryOptions
                {
                    AbsoluteExpiration = new DateTimeOffset(new DateTime(2016, 1, 1, 1, 1, 1, DateTimeKind.Utc)),
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1),
                    Priority = CacheItemPriority.High,
                    SlidingExpiration = TimeSpan.FromMinutes(5)
                }))
                .AndAlso()
                .ShouldReturn()
                .Ok();
        }

        [Fact]
        public void ContainingEntryWithOptionsShouldThrowExceptionWithIncorrectEntryKey()
        {
            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyMvc
                       .Controller<MvcController>()
                       .Calling(c => c.AddMemoryCacheAction())
                       .ShouldHave()
                       .MemoryCache(cache => cache.ContainingEntry("test", "value", new MemoryCacheEntryOptions
                       {
                           AbsoluteExpiration = new DateTimeOffset(new DateTime(2016, 1, 1, 1, 1, 1, DateTimeKind.Utc)),
                           AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(2),
                           Priority = CacheItemPriority.High,
                           SlidingExpiration = TimeSpan.FromMinutes(5)
                       }))
                       .AndAlso()
                       .ShouldReturn()
                       .Ok();
                },
                "When calling AddMemoryCacheAction action in MvcController expected memory cache to have entry with the given options, but in fact they were different.");
        }

        [Fact]
        public void ContainingEntriesShouldNotThrowExceptionWithCorrectValues()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.AddMemoryCacheAction())
                .ShouldHave()
                .MemoryCache(cache => cache.ContainingEntries(new Dictionary<object, object>
                {
                    ["test"] = "value",
                    ["another"] = "anotherValue"
                }))
                .AndAlso()
                .ShouldReturn()
                .Ok();
        }
        
        [Fact]
        public void ContainingEntriesShouldThrowExceptionWithIncorrectCount()
        {
            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.AddMemoryCacheAction())
                        .ShouldHave()
                        .MemoryCache(cache => cache.ContainingEntries(new Dictionary<object, object>
                        {
                            ["test"] = "value",
                        }))
                        .AndAlso()
                        .ShouldReturn()
                        .Ok();
                },
                "When calling AddMemoryCacheAction action in MvcController expected memory cache to have 1 entry, but in fact found 2.");
        }
        
        [Fact]
        public void ContainingEntriesShouldThrowExceptionWithIncorrectManyCount()
        {
            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.AddMemoryCacheAction())
                        .ShouldHave()
                        .MemoryCache(cache => cache.ContainingEntries(new Dictionary<object, object>
                        {
                            ["test"] = "value",
                            ["another"] = "anotherValue",
                            ["third"] = "anotherThirdValue"
                        }))
                        .AndAlso()
                        .ShouldReturn()
                        .Ok();
                },
                "When calling AddMemoryCacheAction action in MvcController expected memory cache to have 3 entries, but in fact found 2.");
        }
        
        [Fact]
        public void ContainingEntriesShouldThrowExceptionWithInvalidKey()
        {
            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.AddMemoryCacheAction())
                        .ShouldHave()
                        .MemoryCache(cache => cache.ContainingEntries(new Dictionary<object, object>
                        {
                            ["test"] = "value",
                            ["invalid"] = "anotherValue"
                        }))
                        .AndAlso()
                        .ShouldReturn()
                        .Ok();
                },
                "When calling AddMemoryCacheAction action in MvcController expected memory cache to have entry with the given key, but such was not found.");
        }
        
        [Fact]
        public void ContainingEntriesShouldThrowExceptionWithInvalidValue()
        {
            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.AddMemoryCacheAction())
                        .ShouldHave()
                        .MemoryCache(cache => cache.ContainingEntries(new Dictionary<object, object>
                        {
                            ["test"] = "value",
                            ["another"] = "invalid"
                        }))
                        .AndAlso()
                        .ShouldReturn()
                        .Ok();
                },
                "When calling AddMemoryCacheAction action in MvcController expected memory cache to have entry with the given value, but in fact it was different.");
        }

        public void Dispose()
        {
            MyMvc.IsUsingDefaultConfiguration();
        }
    }
}
