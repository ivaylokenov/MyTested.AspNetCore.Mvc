namespace MyTested.AspNetCore.Mvc.Builders.Data.MemoryCache
{
    using System;
    using System.Collections.Generic;
    using Base;
    using Contracts.Data.MemoryCache;
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
    public class MemoryCacheTestBuilder : BaseTestBuilderWithComponent, IAndMemoryCacheTestBuilder
    {
        internal const string MemoryCacheName = "memory cache";

        private readonly IMemoryCache memoryCache;

        private IMemoryCacheMock memoryCacheMock;
        private IDictionary<object, object> memoryCacheAsDictionary;

        /// <summary>
        /// Initializes a new instance of the <see cref="MemoryCacheTestBuilder"/> class.
        /// </summary>
        /// <param name="testContext"><see cref="ComponentTestContext"/> containing data about the currently executed assertion chain.</param>
        public MemoryCacheTestBuilder(ComponentTestContext testContext)
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
        public IAndMemoryCacheTestBuilder ContainingEntryWithValue<TValue>(TValue value)
        {
            DictionaryValidator.ValidateValue(
                MemoryCacheName,
                this.GetMemoryCacheAsDictionary(),
                value,
                this.ThrowNewDataProviderAssertionException);

            return this;
        }

        /// <inheritdoc />
        public IAndMemoryCacheTestBuilder ContainingEntryOfType<TValue>()
        {
            DictionaryValidator.ValidateValueOfType<TValue>(
                MemoryCacheName,
                this.GetMemoryCacheAsDictionary(),
                this.ThrowNewDataProviderAssertionException);

            return this;
        }

        /// <inheritdoc />
        public IAndMemoryCacheTestBuilder ContainingEntryOfType<TValue>(object key)
        {
            var value = this.GetValue(key);
            var expectedType = typeof(TValue);
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
            var mockedMemoryCache = this.GetMemoryCacheMock();

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
        public IAndMemoryCacheTestBuilder ContainingEntry(Action<IMemoryCacheEntryKeyTestBuilder> memoryCacheEntryTestBuilder)
        {
            var mockedMemoryCache = this.GetMemoryCacheMock();

            var newMemoryCacheEntryBuilder = new MemoryCacheEntryTestBuilder(this.TestContext);
            memoryCacheEntryTestBuilder(newMemoryCacheEntryBuilder);
            var expectedMemoryCacheEntry = newMemoryCacheEntryBuilder.GetMemoryCacheEntryMock();

            var key = expectedMemoryCacheEntry.Key;
            this.ContainingEntryWithKey(key);

            mockedMemoryCache.TryGetCacheEntry(key, out var actualMemoryCacheEntry);

            var validations = newMemoryCacheEntryBuilder.GetMemoryCacheEntryMockValidations();
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

        private IMemoryCacheMock GetMemoryCacheMock()
        {
            if (this.memoryCacheMock == null)
            {
                this.memoryCacheMock = this.memoryCache.AsMemoryCacheMock();
            }

            return this.memoryCacheMock;
        }

        private IDictionary<object, object> GetMemoryCacheAsDictionary()
        {
            if (this.memoryCacheAsDictionary == null)
            {
                this.memoryCacheAsDictionary = this
                    .GetMemoryCacheMock()
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
                "{0} {1} {2}, but {3}.",
                this.TestContext.ExceptionMessagePrefix,
                propertyName,
                expectedValue,
                actualValue));
        }
    }
}
