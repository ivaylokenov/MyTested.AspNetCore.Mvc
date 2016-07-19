namespace MyTested.AspNetCore.Mvc.Builders.Data
{
    using System;
    using System.Collections.Generic;
    using Base;
    using Contracts.Data;
    using Exceptions;
    using Internal.Contracts;
    using Internal.TestContexts;
    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.Extensions.DependencyInjection;
    using Utilities;
    using Utilities.Extensions;
    using Utilities.Validators;

    /// <summary>
    /// Used for testing <see cref="IMemoryCache"/>.
    /// </summary>
    public class MemoryCacheTestBuilder : BaseTestBuilderWithInvokedAction, IAndMemoryCacheTestBuilder
    {
        internal const string MemoryCacheName = "memory cache";

        private readonly IMemoryCache memoryCache;

        private IMockedMemoryCache mockedMemoryCache;
        private IDictionary<object, object> memoryCacheAsDictionary;

        /// <summary>
        /// Initializes a new instance of the <see cref="MemoryCacheTestBuilder"/> class.
        /// </summary>
        /// <param name="testContext"><see cref="ControllerTestContext"/> containing data about the currently executed assertion chain.</param>
        public MemoryCacheTestBuilder(ControllerTestContext testContext)
            : base(testContext)
        {
            this.memoryCache = this.GetMemoryCache();
        }

        /// <inheritdoc />
        public IAndMemoryCacheTestBuilder ContainingEntryWithKey(object key)
        {
            this.GetValue(key);
            return this;
        }

        /// <inheritdoc />
        public IAndMemoryCacheTestBuilder ContainingEntryWithValue<TEntry>(TEntry value)
        {
            DictionaryValidator.ValidateValue(
                MemoryCacheName,
                this.GetMemoryCacheAsDictionary(),
                value,
                this.ThrowNewDataProviderAssertionException);

            return this;
        }

        /// <inheritdoc />
        public IAndMemoryCacheTestBuilder ContainingEntryOfType<TEntry>()
        {
            DictionaryValidator.ValidateValueOfType<TEntry>(
                MemoryCacheName,
                this.GetMemoryCacheAsDictionary(),
                this.ThrowNewDataProviderAssertionException);

            return this;
        }

        /// <inheritdoc />
        public IAndMemoryCacheTestBuilder ContainingEntryOfType<TEntry>(object key)
        {
            var value = this.GetValue(key);
            var expectedType = typeof(TEntry);
            var actualType = value.GetType();

            if (Reflection.AreDifferentTypes(expectedType, actualType))
            {
                this.ThrowNewDataProviderAssertionException(
                    MemoryCacheName,
                    $"to have entry with the given key and value of {expectedType.ToFriendlyTypeName()} type",
                    $"in fact found {actualType.ToFriendlyTypeName()}");
            }

            return this;
        }

        /// <inheritdoc />
        public IAndMemoryCacheTestBuilder ContainingEntry(object key, object value)
        {
            var actualValue = this.GetValue(key);
            if (Reflection.AreNotDeeplyEqual(value, actualValue))
            {
                this.ThrowNewDataProviderAssertionException(
                    MemoryCacheName,
                    "to have entry with the given value",
                    "in fact it was different");
            }

            return this;
        }

        /// <inheritdoc />
        public IAndMemoryCacheTestBuilder ContainingEntry(object key, object value, MemoryCacheEntryOptions options)
        {
            var mockedMemoryCache = this.GetMockedMemoryCache();

            this.ContainingEntry(key, value);

            ICacheEntry cacheEntry;
            mockedMemoryCache.TryGetCacheEntry(key, out cacheEntry);

            var actualOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = cacheEntry.AbsoluteExpiration,
                AbsoluteExpirationRelativeToNow = cacheEntry.AbsoluteExpirationRelativeToNow,
                Priority = cacheEntry.Priority,
                SlidingExpiration = cacheEntry.SlidingExpiration
            };

            if (Reflection.AreNotDeeplyEqual(options, actualOptions))
            {
                this.ThrowNewDataProviderAssertionException(
                    MemoryCacheName,
                    $"to have entry with the given options",
                    "in fact they were different");
            }

            return this;
        }

        /// <inheritdoc />
        public IAndMemoryCacheTestBuilder ContainingEntry(Action<IMemoryCacheEntryTestBuilder> memoryCacheEntryTestBuilder)
        {
            var mockedMemoryCache = this.GetMockedMemoryCache();

            var newMemoryCacheEntryBuilder = new MemoryCacheEntryTestBuilder(this.TestContext);
            memoryCacheEntryTestBuilder(newMemoryCacheEntryBuilder);
            var expectedMemoryCacheEntry = newMemoryCacheEntryBuilder.GetMockedMemoryCacheEntry();

            var key = expectedMemoryCacheEntry.Key;
            this.ContainingEntryWithKey(key);

            ICacheEntry actualMemoryCacheEntry;
            mockedMemoryCache.TryGetCacheEntry(key, out actualMemoryCacheEntry);

            var validations = newMemoryCacheEntryBuilder.GetMockedMemoryCacheEntryValidations();
            validations.ForEach(v => v(expectedMemoryCacheEntry, actualMemoryCacheEntry));

            return this;
        }

        /// <inheritdoc />
        public IAndMemoryCacheTestBuilder ContainingEntries(IDictionary<object, object> entries)
        {
            var expectedItems = entries.Count;
            var actualItems = this.GetMemoryCacheAsDictionary().Count;

            if (expectedItems != actualItems)
            {
                this.ThrowNewDataProviderAssertionException(
                    MemoryCacheName,
                    $"to have {expectedItems} {(expectedItems != 1 ? "entries" : "entry")}",
                    $"in fact found {actualItems}");
            }

            entries.ForEach(e => this.ContainingEntry(e.Key, e.Value));
            return this;
        }

        /// <inheritdoc />
        public IMemoryCacheTestBuilder AndAlso() => this;

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
                this.mockedMemoryCache = this.memoryCache.AsMockedMemoryCache();
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
                this.Component.GetName(),
                propertyName,
                expectedValue,
                actualValue));
        }
    }
}
