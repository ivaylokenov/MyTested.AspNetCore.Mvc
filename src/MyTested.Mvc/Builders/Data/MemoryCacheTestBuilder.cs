namespace MyTested.Mvc.Builders.Data
{
    using Base;
    using Contracts.Data;
    using Exceptions;
    using Internal.Contracts;
    using Internal.TestContexts;
    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Collections.Generic;
    using Utilities;
    using Utilities.Extensions;
    using Utilities.Validators;

    public class MemoryCacheTestBuilder : BaseTestBuilderWithInvokedAction, IAndMemoryCacheTestBuilder
    {
        private const string MemoryCacheName = "memory cache";

        private readonly IMemoryCache memoryCache;

        private IMockedMemoryCache mockedMemoryCache;
        private IDictionary<object, object> memoryCacheAsDictionary;

        public MemoryCacheTestBuilder(ControllerTestContext testContext)
            : base(testContext)
        {
            this.memoryCache = this.GetMemoryCache();
        }

        public IAndMemoryCacheTestBuilder ContainingEntryWithKey(object key)
        {
            this.GetValue(key);
            return this;
        }

        public IAndMemoryCacheTestBuilder ContainingEntryWithValue<TEntry>(TEntry value)
        {
            DictionaryValidator.ValidateValue(
                MemoryCacheName,
                this.GetMemoryCacheAsDictionary(),
                value,
                this.ThrowNewDataProviderAssertionException);

            return this;
        }

        public IAndMemoryCacheTestBuilder ContainingEntryOfType<TEntry>()
        {
            DictionaryValidator.ValidateValueOfType<TEntry>(
                MemoryCacheName,
                this.GetMemoryCacheAsDictionary(),
                this.ThrowNewDataProviderAssertionException);

            return this;
        }

        public IAndMemoryCacheTestBuilder ContainingEntryOfType<TEntry>(object key)
        {
            var value = this.GetValue(key);
            var expectedType = typeof(TEntry);
            var actualType = value.GetType();

            if (Reflection.AreDifferentTypes(expectedType, actualType))
            {
                this.ThrowNewDataProviderAssertionException(
                    MemoryCacheName,
                    $"to have entry with the given key and of {expectedType.ToFriendlyTypeName()} type",
                    $"in fact found {actualType.ToFriendlyTypeName()}");
            }

            return this;
        }

        public IAndMemoryCacheTestBuilder ContainingEntry(object key, object value)
        {
            var actualValue = this.GetValue(key);
            if (Reflection.AreNotDeeplyEqual(value, actualValue))
            {
                this.ThrowNewDataProviderAssertionException(
                    MemoryCacheName,
                    $"to have entry with the given value",
                    "in fact it was different");
            }

            return this;
        }

        public IAndMemoryCacheTestBuilder ContainingEntry(object key, object value, MemoryCacheEntryOptions options)
        {
            this.ContainingEntry(key, value);

            IMockedCacheEntry cacheEntry;
            this.mockedMemoryCache.TryGetCacheEntry(key, out cacheEntry);
            var actualOptions = cacheEntry.Options;

            if (Reflection.AreNotDeeplyEqual(options, actualOptions))
            {
                this.ThrowNewDataProviderAssertionException(
                    MemoryCacheName,
                    $"to have entry with the given options",
                    "in fact they were different");
            }

            return this;
        }

        public IAndMemoryCacheTestBuilder ContainingEntries(IDictionary<object, object> entries)
        {
            entries.ForEach(e => this.ContainingEntry(e.Key, e.Value));
            return this;
        }

        public IMemoryCacheTestBuilder AndAlso()
        {
            return this;
        }

        private IMemoryCache GetMemoryCache()
        {
            return this.TestContext
                .HttpContext
                .RequestServices
                .GetRequiredService<IMemoryCache>();
        }

        private IMockedMemoryCache GetMockedMemoryCache()
        {
            if (this.mockedMemoryCache == null)
            {
                this.mockedMemoryCache = this.memoryCache as IMockedMemoryCache;
                if (this.mockedMemoryCache == null)
                {
                    throw new InvalidOperationException("This test requires the registered IMemoryCache service to implement IMockedMemoryCache.");
                }
            }

            return this.mockedMemoryCache;
        }

        private IDictionary<object, object> GetMemoryCacheAsDictionary()
        {
            if (this.memoryCacheAsDictionary == null)
            {
                this.memoryCacheAsDictionary = this
                    .GetMockedMemoryCache()
                    .GetCacheAsDictionary();
            }

            return this.memoryCacheAsDictionary;
        }

        private object GetValue(object key)
        {
            object value;
            if (!this.memoryCache.TryGetValue(key, out value))
            {
                this.ThrowNewDataProviderAssertionException(
                    MemoryCacheName,
                    $"to have entry with the given key",
                    "such was not found");
            }

            return value;
        }

        private void ThrowNewDataProviderAssertionException(string propertyName, string expectedValue, string actualValue)
        {
            throw new DataProviderAssertionException(string.Format(
                    "When calling {0} action in {1} expected {2} {3}, but {4}.",
                    this.ActionName,
                    this.Controller.GetName(),
                    propertyName,
                    expectedValue,
                    actualValue));
        }
    }
}
