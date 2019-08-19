namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.InvocationsTests.ShouldHaveTests
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using Exceptions;
    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.Extensions.DependencyInjection;
    using Setups;
    using Setups.ViewComponents;
    using Xunit;

    public class ShouldHaveMemoryCacheTests : IDisposable
    {
        public ShouldHaveMemoryCacheTests()
        {
            MyApplication
                .StartsFrom<DefaultStartup>()
                .WithServices(services => services.AddMemoryCache());
        }

        [Fact]
        public void NoMemoryCacheShouldNotThrowExceptionWithViewComponentNoCacheEntries()
        {
            MyViewComponent<NormalComponent>
                .Instance()
                .InvokedWith(c => c.Invoke())
                .ShouldHave()
                .NoMemoryCache()
                .AndAlso()
                .ShouldReturn()
                .Content();
        }

        [Fact]
        public void NoMemoryCacheShouldThrowExceptionWithViewComponentCacheEntries()
        {
            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyViewComponent<MemoryCacheValuesComponent>
                        .Instance()
                        .InvokedWith(c => c.Invoke())
                        .ShouldHave()
                        .NoMemoryCache()
                        .AndAlso()
                        .ShouldReturn()
                        .View();
                },
                "When invoking MemoryCacheValuesComponent expected to have memory cache with no entries, but in fact it had some.");
        }

        [Fact]
        public void MemoryCacheWithNoNumberShouldNotThrowExceptionWithViewComponentAnyCacheEntries()
        {
            MyViewComponent<MemoryCacheValuesComponent>
                .Instance()
                .InvokedWith(c => c.Invoke())
                .ShouldHave()
                .MemoryCache()
                .AndAlso()
                .ShouldReturn()
                .View();
        }

        [Fact]
        public void MemoryCacheWithNoNumberShouldThrowExceptionWithNoCacheEntries()
        {
            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyViewComponent<NormalComponent>
                        .Instance()
                        .InvokedWith(c => c.Invoke())
                        .ShouldHave()
                        .MemoryCache()
                        .AndAlso()
                        .ShouldReturn()
                        .View();
                },
                "When invoking NormalComponent expected to have memory cache entries, but none were found.");
        }

        [Fact]
        public void MemoryCacheWithNumberShouldNotThrowExceptionWithCorrectCacheEntries()
        {
            MyViewComponent<MemoryCacheValuesComponent>
                .Instance()
                .InvokedWith(c => c.Invoke())
                .ShouldHave()
                .MemoryCache(withNumberOfEntries: 2)
                .AndAlso()
                .ShouldReturn()
                .View();
        }

        [Fact]
        public void MemoryCacheWithNumberShouldThrowExceptionWithInvalidCacheEntries()
        {
            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyViewComponent<MemoryCacheValuesComponent>
                        .Instance()
                        .InvokedWith(c => c.Invoke())
                        .ShouldHave()
                        .MemoryCache(1)
                        .AndAlso()
                        .ShouldReturn()
                        .View();
                },
                "When invoking MemoryCacheValuesComponent expected to have memory cache with 1 entry, but in fact contained 2.");
        }

        [Fact]
        public void MemoryCacheWithNumberShouldThrowExceptionWithInvalidManyCacheEntries()
        {
            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyViewComponent<MemoryCacheValuesComponent>
                        .Instance()
                        .InvokedWith(c => c.Invoke())
                        .ShouldHave()
                        .MemoryCache(3)
                        .AndAlso()
                        .ShouldReturn()
                        .View();
                },
                "When invoking MemoryCacheValuesComponent expected to have memory cache with 3 entries, but in fact contained 2.");
        }

        [Fact]
        public void MemoryCacheWithBuilderShouldNotThrowWithCorrectEntry()
        {
            MyViewComponent<MemoryCacheValuesComponent>
                .Instance()
                .InvokedWith(c => c.Invoke())
                .ShouldHave()
                .MemoryCache(cache => cache
                    .ContainingEntry("test", "value"))
                .AndAlso()
                .ShouldReturn()
                .View();
        }

        [Fact]
        public void MemoryCacheWithBuilderShouldThrowWithIncorrectEntryKey()
        {
            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyViewComponent<MemoryCacheValuesComponent>
                        .Instance()
                        .InvokedWith(c => c.Invoke())
                        .ShouldHave()
                        .MemoryCache(cache => cache
                            .ContainingEntry("Invalid", "value"))
                        .AndAlso()
                        .ShouldReturn()
                        .View();
                },
                "When invoking MemoryCacheValuesComponent expected memory cache to have entry with the given key, but such was not found.");
        }

        [Fact]
        public void MemoryCacheWithBuilderShouldThrowWithIncorrectEntryValue()
        {
            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyViewComponent<MemoryCacheValuesComponent>
                        .Instance()
                        .InvokedWith(c => c.Invoke())
                        .ShouldHave()
                        .MemoryCache(cache => cache
                            .ContainingEntry("test", "invalid"))
                        .AndAlso()
                        .ShouldReturn()
                        .View();
                },
                "When invoking MemoryCacheValuesComponent expected memory cache to have entry with the given value, but in fact it was different.");
        }

        [Fact]
        public void MemoryCacheWithBuilderShouldWorkCorrectlyWithMemoryCacheEntryOptions()
        {
            MyViewComponent<MemoryCacheValuesComponent>
                .Instance()
                .InvokedWith(c => c.Invoke())
                .ShouldHave()
                .MemoryCache(cache => cache
                    .ContainingEntry("test", "value", new MemoryCacheEntryOptions
                    {
                        AbsoluteExpiration = new DateTimeOffset(new DateTime(2016, 1, 1, 1, 1, 1, DateTimeKind.Utc)),
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1),
                        Priority = CacheItemPriority.High,
                        SlidingExpiration = TimeSpan.FromMinutes(5)
                    }))
                .AndAlso()
                .ShouldReturn()
                .View();
        }

        [Fact]
        public void MemoryCacheWithBuilderShouldThrowWithMemoryCacheEntryOptionsAndIncorrectEntryKey()
        {
            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyViewComponent<MemoryCacheValuesComponent>
                        .Instance()
                        .InvokedWith(c => c.Invoke())
                        .ShouldHave()
                        .MemoryCache(cache => cache
                            .ContainingEntry("invalid", "value", new MemoryCacheEntryOptions
                            {
                                AbsoluteExpiration = new DateTimeOffset(new DateTime(2016, 1, 1, 1, 1, 1, DateTimeKind.Utc)),
                                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1),
                                Priority = CacheItemPriority.High,
                                SlidingExpiration = TimeSpan.FromMinutes(5)
                            }))
                        .AndAlso()
                        .ShouldReturn()
                        .View();
                },
                "When invoking MemoryCacheValuesComponent expected memory cache to have entry with the given key, but such was not found.");
        }

        [Fact]
        public void MemoryCacheWithBuilderShouldThrowWithMemoryCacheEntryOptionsAndIncorrectEntryValue()
        {
            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyViewComponent<MemoryCacheValuesComponent>
                        .Instance()
                        .InvokedWith(c => c.Invoke())
                        .ShouldHave()
                        .MemoryCache(cache => cache
                            .ContainingEntry("test", "invalid", new MemoryCacheEntryOptions
                            {
                                AbsoluteExpiration = new DateTimeOffset(new DateTime(2016, 1, 1, 1, 1, 1, DateTimeKind.Utc)),
                                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1),
                                Priority = CacheItemPriority.High,
                                SlidingExpiration = TimeSpan.FromMinutes(5)
                            }))
                        .AndAlso()
                        .ShouldReturn()
                        .View();
                },
                "When invoking MemoryCacheValuesComponent expected memory cache to have entry with the given value, but in fact it was different.");
        }

        [Fact]
        public void MemoryCacheWithBuilderShouldThrowWithMemoryCacheEntryOptionsWithInvalidAbsoluteExpiration()
        {
            var invalidExpirationDate = new DateTime(2017, 1, 1, 1, 1, 1, DateTimeKind.Utc);

            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyViewComponent<MemoryCacheValuesComponent>
                        .Instance()
                        .InvokedWith(c => c.Invoke())
                        .ShouldHave()
                        .MemoryCache(cache => cache
                            .ContainingEntry("test", "value", new MemoryCacheEntryOptions
                            {
                                AbsoluteExpiration = new DateTimeOffset(invalidExpirationDate),
                                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1),
                                Priority = CacheItemPriority.High,
                                SlidingExpiration = TimeSpan.FromMinutes(5)
                            }))
                        .AndAlso()
                        .ShouldReturn()
                        .View();
                },
                "When invoking MemoryCacheValuesComponent expected memory cache to have entry with the given options, but in fact they were different.");
        }

        [Fact]
        public void MemoryCacheWithBuilderShouldThrowWithMemoryCacheEntryOptionsWithInvalidAbsoluteExpirationRelativeToNow()
        {
            var invalidAbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(2);

            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyViewComponent<MemoryCacheValuesComponent>
                        .Instance()
                        .InvokedWith(c => c.Invoke())
                        .ShouldHave()
                        .MemoryCache(cache => cache
                            .ContainingEntry("test", "value", new MemoryCacheEntryOptions
                            {
                                AbsoluteExpiration = new DateTimeOffset(new DateTime(2016, 1, 1, 1, 1, 1, DateTimeKind.Utc)),
                                AbsoluteExpirationRelativeToNow = invalidAbsoluteExpirationRelativeToNow,
                                Priority = CacheItemPriority.High,
                                SlidingExpiration = TimeSpan.FromMinutes(5)
                            }))
                        .AndAlso()
                        .ShouldReturn()
                        .View();
                },
                "When invoking MemoryCacheValuesComponent expected memory cache to have entry with the given options, but in fact they were different.");
        }

        [Fact]
        public void MemoryCacheWithBuilderShouldThrowWithMemoryCacheEntryOptionsWithInvalidPriority()
        {
            var invalidPriority = CacheItemPriority.Low;

            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyViewComponent<MemoryCacheValuesComponent>
                        .Instance()
                        .InvokedWith(c => c.Invoke())
                        .ShouldHave()
                        .MemoryCache(cache => cache
                            .ContainingEntry("test", "value", new MemoryCacheEntryOptions
                            {
                                AbsoluteExpiration = new DateTimeOffset(new DateTime(2016, 1, 1, 1, 1, 1, DateTimeKind.Utc)),
                                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1),
                                Priority = invalidPriority,
                                SlidingExpiration = TimeSpan.FromMinutes(5)
                            }))
                        .AndAlso()
                        .ShouldReturn()
                        .View();
                },
                "When invoking MemoryCacheValuesComponent expected memory cache to have entry with the given options, but in fact they were different.");
        }

        [Fact]
        public void MemoryCacheWithBuilderShouldThrowWithMemoryCacheEntryOptionsWithInvalidSlidingExpiration()
        {
            int invalidSlidingExpirationValue = 1;

            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyViewComponent<MemoryCacheValuesComponent>
                        .Instance()
                        .InvokedWith(c => c.Invoke())
                        .ShouldHave()
                        .MemoryCache(cache => cache
                            .ContainingEntry("test", "value", new MemoryCacheEntryOptions
                            {
                                AbsoluteExpiration = new DateTimeOffset(new DateTime(2016, 1, 1, 1, 1, 1, DateTimeKind.Utc)),
                                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1),
                                Priority = CacheItemPriority.High,
                                SlidingExpiration = TimeSpan.FromMinutes(invalidSlidingExpirationValue)
                            }))
                        .AndAlso()
                        .ShouldReturn()
                        .View();
                },
                "When invoking MemoryCacheValuesComponent expected memory cache to have entry with the given options, but in fact they were different.");
        }

        [Fact]
        public void MemoryCacheWithBuilderShouldWorkCorrectlyWithDictionaryOfCacheEntries()
        {
            MyViewComponent<MemoryCacheValuesComponent>
                .Instance()
                .InvokedWith(c => c.Invoke())
                .ShouldHave()
                .MemoryCache(cache => cache
                    .ContainingEntries(new Dictionary<object, object>
                    {
                        ["test"] = "value",
                        ["another"] = "anotherValue"
                    }))
                .AndAlso()
                .ShouldReturn()
                .View();
        }

        [Fact]
        public void MemoryCacheWithBuilderShouldThrowWithDictionaryOfCacheEntriesWithIncorrectCount()
        {
            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyViewComponent<MemoryCacheValuesComponent>
                        .Instance()
                        .InvokedWith(c => c.Invoke())
                        .ShouldHave()
                        .MemoryCache(cache => cache
                            .ContainingEntries(new Dictionary<object, object>
                            {
                                ["test"] = "value"
                            }))
                        .AndAlso()
                        .ShouldReturn()
                        .View();
                },
                "When invoking MemoryCacheValuesComponent expected memory cache to have 1 entry, but in fact found 2.");
        }

        [Fact]
        public void MemoryCacheWithBuilderShouldThrowWithDictionaryOfCacheEntriesWithInvalidValue()
        {
            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyViewComponent<MemoryCacheValuesComponent>
                        .Instance()
                        .InvokedWith(c => c.Invoke())
                        .ShouldHave()
                        .MemoryCache(cache => cache
                            .ContainingEntries(new Dictionary<object, object>
                            {
                                ["test"] = "invalid",
                                ["another"] = "anotherValue"
                            }))
                        .AndAlso()
                        .ShouldReturn()
                        .View();
                },
                "When invoking MemoryCacheValuesComponent expected memory cache to have entry with the given value, but in fact it was different.");
        }

        [Fact]
        public void MemoryCacheWithBuilderShouldThrowWithDictionaryOfCacheEntriesWithInvalidKey()
        {
            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyViewComponent<MemoryCacheValuesComponent>
                        .Instance()
                        .InvokedWith(c => c.Invoke())
                        .ShouldHave()
                        .MemoryCache(cache => cache
                            .ContainingEntries(new Dictionary<object, object>
                            {
                                ["test"] = "value",
                                ["invalid"] = "anotherValue"
                            }))
                        .AndAlso()
                        .ShouldReturn()
                        .View();
                },
                "When invoking MemoryCacheValuesComponent expected memory cache to have entry with the given key, but such was not found.");
        }

        [Fact]
        public void MemoryCacheWithBuilderWithPredicateShouldWorkWithCorrectPassingAssertions()
        {
            MyViewComponent<MemoryCacheValuesComponent>
                .Instance()
                .InvokedWith(c => c.Invoke())
                .ShouldHave()
                .MemoryCache(cache => cache
                    .ContainingEntry(entry => entry
                        .WithKey("test")
                        .WithValueOfType<string>()
                        .Passing(v => Assert.StartsWith("val", v))))
                .AndAlso()
                .ShouldReturn()
                .View();
        }

        [Fact]
        public void MemoryCacheWithBuilderWithPredicateShouldWorkWithCorrectPassingPredicate()
        {
            MyViewComponent<MemoryCacheValuesComponent>
                .Instance()
                .InvokedWith(c => c.Invoke())
                .ShouldHave()
                .MemoryCache(cache => cache
                    .ContainingEntry(entry => entry
                        .WithKey("test")
                        .WithValueOfType<string>()
                        .Passing(v => v.StartsWith("val"))))
                .AndAlso()
                .ShouldReturn()
                .View();
        }

        [Fact]
        public void MemoryCacheWithBuilderWithPredicateShouldThrowWithIncorrectPassingPredicate()
        {
            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyViewComponent<MemoryCacheValuesComponent>
                        .Instance()
                        .InvokedWith(c => c.Invoke())
                        .ShouldHave()
                        .MemoryCache(cache => cache
                            .ContainingEntry(entry => entry
                                .WithKey("test")
                                .WithValueOfType<string>()
                                .Passing(v => v.StartsWith("Inv"))))
                        .AndAlso()
                        .ShouldReturn()
                        .View();
                },
                "When invoking MemoryCacheValuesComponent expected memory cache to have entry with 'test' key and value passing the given predicate, but it failed.");
        }

        [Fact]
        public void MemoryCacheWithBuilderWithPredicateShouldThrowWithIncorrectKey()
        {
            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyViewComponent<MemoryCacheValuesComponent>
                        .Instance()
                        .InvokedWith(c => c.Invoke())
                        .ShouldHave()
                        .MemoryCache(cache => cache
                            .ContainingEntry(entry => entry
                                .WithKey("Invalid")))
                        .AndAlso()
                        .ShouldReturn()
                        .View();
                },
                "When invoking MemoryCacheValuesComponent expected memory cache to have entry with the given key, but such was not found.");
        }

        [Fact]
        public void MemoryCacheWithBuilderWithPredicateShouldThrowWithIncorrectValue()
        {
            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyViewComponent<MemoryCacheValuesComponent>
                        .Instance()
                        .InvokedWith(c => c.Invoke())
                        .ShouldHave()
                        .MemoryCache(cache => cache
                            .ContainingEntry(entry => entry
                                .WithKey("test")
                                .WithValue("invalid")))
                        .AndAlso()
                        .ShouldReturn()
                        .View();
                },
                "When invoking MemoryCacheValuesComponent expected memory cache to have entry with 'test' key and the given value, but in fact it was different.");
        }

        [Fact]
        public void MemoryCacheWithBuilderWithPredicateShouldWorkWithCorrectAbsoluteExpiration()
        {
            MyViewComponent<MemoryCacheValuesComponent>
                .Instance()
                .InvokedWith(c => c.Invoke())
                .ShouldHave()
                .MemoryCache(cache => cache
                    .ContainingEntry(entry => entry
                        .WithKey("test")
                        .WithAbsoluteExpiration(new DateTimeOffset(new DateTime(2016, 1, 1, 1, 1, 1, DateTimeKind.Utc)))))
                .AndAlso()
                .ShouldReturn()
                .View();
        }

        [Fact]
        public void MemoryCacheWithBuilderWithPredicateShouldThrowWithIncorrectAbsoluteExpiration()
        {
            var invalidExpirationDate = new DateTime(2017, 1, 1, 1, 1, 1, DateTimeKind.Utc);
            var actualExpirationDate = new DateTime(2016, 1, 1, 1, 1, 1, DateTimeKind.Utc);

            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyViewComponent<MemoryCacheValuesComponent>
                        .Instance()
                        .InvokedWith(c => c.Invoke())
                        .ShouldHave()
                        .MemoryCache(cache => cache
                            .ContainingEntry(entry => entry
                                .WithKey("test")
                                .WithAbsoluteExpiration(new DateTimeOffset(invalidExpirationDate))))
                        .AndAlso()
                        .ShouldReturn()
                        .View();
                },
                "When invoking MemoryCacheValuesComponent expected memory cache to have entry with 'test' key and '" + invalidExpirationDate.ToString("r", CultureInfo.InvariantCulture) + "' absolute expiration, but in fact found '" + actualExpirationDate.ToString("r", CultureInfo.InvariantCulture) + "'.");
        }

        [Fact]
        public void MemoryCacheWithBuilderWithPredicateShouldWorkWithCorrectAbsoluteExpirationRelativeToNow()
        {
            MyViewComponent<MemoryCacheValuesComponent>
                .Instance()
                .InvokedWith(c => c.Invoke())
                .ShouldHave()
                .MemoryCache(cache => cache
                    .ContainingEntry(entry => entry
                        .WithKey("test")
                        .WithAbsoluteExpirationRelativeToNow(TimeSpan.FromMinutes(1))))
                .AndAlso()
                .ShouldReturn()
                .View();
        }

        [Fact]
        public void MemoryCacheWithBuilderWithPredicateShouldThrowWithIncorrectAbsoluteExpirationRelativeToNow()
        {
            var invalidAbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(2);
            var actualAbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1);

            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyViewComponent<MemoryCacheValuesComponent>
                        .Instance()
                        .InvokedWith(c => c.Invoke())
                        .ShouldHave()
                        .MemoryCache(cache => cache
                            .ContainingEntry(entry => entry
                                .WithKey("test")
                                .WithAbsoluteExpirationRelativeToNow(invalidAbsoluteExpirationRelativeToNow)))
                        .AndAlso()
                        .ShouldReturn()
                        .View();
                },
                "When invoking MemoryCacheValuesComponent expected memory cache to have entry with 'test' key and '" + invalidAbsoluteExpirationRelativeToNow.ToString() + "' absolute expiration relative to now, but in fact found '" + actualAbsoluteExpirationRelativeToNow.ToString() + "'.");
        }

        [Fact]
        public void MemoryCacheWithBuilderWithPredicateShouldWorkWithCorrectSlidingExpiration()
        {
            MyViewComponent<MemoryCacheValuesComponent>
                .Instance()
                .InvokedWith(c => c.Invoke())
                .ShouldHave()
                .MemoryCache(cache => cache
                    .ContainingEntry(entry => entry
                        .WithKey("test")
                        .WithSlidingExpiration(TimeSpan.FromMinutes(5))))
                .AndAlso()
                .ShouldReturn()
                .View();
        }

        [Fact]
        public void MemoryCacheWithBuilderWithPredicateShouldThrowWithIncorrectSlidingExpiration()
        {
            var invalidSlidingExpiration = TimeSpan.FromMinutes(1);
            var actualSlidingExpiration = TimeSpan.FromMinutes(5);

            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyViewComponent<MemoryCacheValuesComponent>
                        .Instance()
                        .InvokedWith(c => c.Invoke())
                        .ShouldHave()
                        .MemoryCache(cache => cache
                            .ContainingEntry(entry => entry
                                .WithKey("test")
                                .WithSlidingExpiration(invalidSlidingExpiration)))
                        .AndAlso()
                        .ShouldReturn()
                        .View();
                },
                "When invoking MemoryCacheValuesComponent expected memory cache to have entry with 'test' key and '" + invalidSlidingExpiration.ToString() + "' sliding expiration, but in fact found '" + actualSlidingExpiration.ToString() + "'.");
        }

        [Fact]
        public void MemoryCacheWithBuilderWithPredicateShouldWorkWithCorrectPriority()
        {
            MyViewComponent<MemoryCacheValuesComponent>
                .Instance()
                .InvokedWith(c => c.Invoke())
                .ShouldHave()
                .MemoryCache(cache => cache
                    .ContainingEntry(entry => entry
                        .WithKey("test")
                        .WithPriority(CacheItemPriority.High)))
                .AndAlso()
                .ShouldReturn()
                .View();
        }

        [Fact]
        public void MemoryCacheWithBuilderWithPredicateShouldThrowWithIncorrectPriority()
        {
            var invalidPriority = CacheItemPriority.Low;
            var actualPriority = CacheItemPriority.High;

            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyViewComponent<MemoryCacheValuesComponent>
                        .Instance()
                        .InvokedWith(c => c.Invoke())
                        .ShouldHave()
                        .MemoryCache(cache => cache
                            .ContainingEntry(entry => entry
                                .WithKey("test")
                                .WithPriority(invalidPriority)))
                        .AndAlso()
                        .ShouldReturn()
                        .View();
                },
                "When invoking MemoryCacheValuesComponent expected memory cache to have entry with 'test' key and " + invalidPriority.ToString() + " priority, but in fact found " + actualPriority.ToString() + ".");
        }

        [Fact]
        public void ContainingEntryOfTypeShouldNotThrowWithCorrectEntry()
        {
            MyViewComponent<MemoryCacheValuesComponent>
                .Instance()
                .InvokedWith(c => c.Invoke())
                .ShouldHave()
                .MemoryCache(cache => cache.ContainingEntryOfType<string>())
                .AndAlso()
                .ShouldReturn()
                .View();
        }

        [Fact]
        public void ContainingEntryOfTypeShouldThrowWithIncorrectEntry()
        {
            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyViewComponent<MemoryCacheValuesComponent>
                        .Instance()
                        .InvokedWith(c => c.Invoke())
                        .ShouldHave()
                        .MemoryCache(cache => cache.ContainingEntryOfType<int>())
                        .AndAlso()
                        .ShouldReturn()
                        .View();
                },
                "When invoking MemoryCacheValuesComponent expected memory cache to have at least one entry of Int32 type, but none was found.");
        }

        [Fact]
        public void ContainingEntryOfTypeAndKeyShouldNotThrowWithCorrectEntry()
        {
            MyViewComponent<MemoryCacheValuesComponent>
                .Instance()
                .InvokedWith(c => c.Invoke())
                .ShouldHave()
                .MemoryCache(cache => cache.ContainingEntryOfType<string>("test"))
                .AndAlso()
                .ShouldReturn()
                .View();
        }

        [Fact]
        public void ContainingEntryOfTypeAndKeyShouldThrowWithIncorrectEntryKey()
        {
            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyViewComponent<MemoryCacheValuesComponent>
                        .Instance()
                        .InvokedWith(c => c.Invoke())
                        .ShouldHave()
                        .MemoryCache(cache => cache.ContainingEntryOfType<string>("Invalid"))
                        .AndAlso()
                        .ShouldReturn()
                        .View();
                },
                "When invoking MemoryCacheValuesComponent expected memory cache to have entry with the given key, but such was not found.");
        }

        [Fact]
        public void ContainingEntryOfTypeAndKeyShouldThrowWithIncorrectEntry()
        {
            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyViewComponent<MemoryCacheValuesComponent>
                        .Instance()
                        .InvokedWith(c => c.Invoke())
                        .ShouldHave()
                        .MemoryCache(cache => cache.ContainingEntryOfType<int>("test"))
                        .AndAlso()
                        .ShouldReturn()
                        .View();
                },
                "When invoking MemoryCacheValuesComponent expected memory cache to have entry with the given key and value of Int32 type, but in fact found String.");
        }

        [Fact]
        public void ContainingEntryWithValueShouldNotThrowWithCorrectEntry()
        {
            MyViewComponent<MemoryCacheValuesComponent>
                .Instance()
                .InvokedWith(c => c.Invoke())
                .ShouldHave()
                .MemoryCache(cache => cache.ContainingEntryWithValue("value"))
                .AndAlso()
                .ShouldReturn()
                .View();
        }

        [Fact]
        public void ContainingEntryWithValueShouldThrowWithIncorrectEntry()
        {
            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyViewComponent<MemoryCacheValuesComponent>
                        .Instance()
                        .InvokedWith(c => c.Invoke())
                        .ShouldHave()
                        .MemoryCache(cache => cache.ContainingEntryWithValue("invalid"))
                        .AndAlso()
                        .ShouldReturn()
                        .View();
                },
                "When invoking MemoryCacheValuesComponent expected memory cache to have entry with the provided value, but none was found.");
        }

        [Fact]
        public void ContainingEntryWithKeyShouldNotThrowWithCorrectEntry()
        {
            MyViewComponent<MemoryCacheValuesComponent>
                .Instance()
                .InvokedWith(c => c.Invoke())
                .ShouldHave()
                .MemoryCache(cache => cache
                    .ContainingEntryWithKey("test")
                    .AndAlso()
                    .ContainingEntryWithKey("another"))
                .AndAlso()
                .ShouldReturn()
                .View();
        }

        [Fact]
        public void ContainingEntryWithKeyShouldThrowWithIncorrectEntry()
        {
            Test.AssertException<DataProviderAssertionException>(
                () =>
                {
                    MyViewComponent<MemoryCacheValuesComponent>
                        .Instance()
                        .InvokedWith(c => c.Invoke())
                        .ShouldHave()
                        .MemoryCache(cache => cache.ContainingEntryWithKey("Invalid"))
                        .AndAlso()
                        .ShouldReturn()
                        .View();
                },
                "When invoking MemoryCacheValuesComponent expected memory cache to have entry with the given key, but such was not found.");
        }

        public void Dispose()
        {
            MyApplication.StartsFrom<DefaultStartup>();
        }
    }
}
