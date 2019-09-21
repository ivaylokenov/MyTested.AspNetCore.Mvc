using MyTested.AspNetCore.Mvc.Internal.Caching;

namespace MyTested.AspNetCore.Mvc.Builders.Data.DistributedCache
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.Extensions.Caching.Distributed;
    using Microsoft.Extensions.DependencyInjection;
    using Base;
    using Contracts.Data.DistributedCache;
    using Exceptions;
    using Internal.TestContexts;
    using Internal.Contracts;
    using Utilities;
    using Utilities.Extensions;

    /// <summary>
    /// Used for testing <see cref="IDistributedCache"/>.
    /// </summary>
    public class DistributedCacheTestBuilder : BaseTestBuilderWithComponent, IAndDistributedCacheTestBuilder
    {
        internal const string DistributedCacheName = "distributed cache";

        private readonly IDistributedCache distributedCache;

        private IDistributedCacheMock distributedCacheMock;
        private Dictionary<string, byte[]> distributedCacheDictionary;

        /// <summary>
        /// Initializes a new instance of the <see cref="DistributedCacheTestBuilder"/> class.
        /// </summary>
        /// <param name="testContext"><see cref="ComponentTestContext"/> containing data about the currently executed assertion chain.</param>
        public DistributedCacheTestBuilder(ComponentTestContext testContext)
            : base(testContext)
        {
            this.distributedCache = this.GetDistributedCache();
            this.distributedCacheMock = this.GetDistributedCacheMock();
            this.distributedCacheDictionary = this.GetDistributedCacheDictionary();
        }

        /// <inheritdoc />
        public IAndDistributedCacheTestBuilder ContainingEntries(IDictionary<string, byte[]> entries)
        {
            var expectedItems = entries.Count;
            var actualItems = ((IDistributedCacheMock)distributedCache).Count; //TODO: check where count is needed and either copy mem cache implementation or provide way to get count.

            if (expectedItems != actualItems)
            {
                this.ThrowNewDataProviderAssertionException(
                    DistributedCacheName,
                    $"to have {expectedItems} {(expectedItems != 1 ? "entries" : "entry")}",
                    $"in fact found {actualItems}");
            }

            entries.ForEach(e => this.ContainingEntry(e.Key, e.Value));
            return this;
        }

        /// <inheritdoc />
        public IAndDistributedCacheTestBuilder ContainingEntry(string key, byte[] value)
        {
            var actualValue = this.GetValue(key);
            if (Reflection.AreNotDeeplyEqual(value, actualValue))
            {
                this.ThrowNewDataProviderAssertionException(
                    DistributedCacheName,
                    "to have entry with the given value",
                    "in fact it was different");
            }

            return this;
        }

        /// <inheritdoc />
        public IAndDistributedCacheTestBuilder ContainingEntry(string key, byte[] value, DistributedCacheEntryOptions options)
        {
            this.ContainingEntry(key, value);

            this.distributedCacheMock.TryGetCacheEntryOptions(key, out var actualOptions);

            if (Reflection.AreNotDeeplyEqual(options, actualOptions))
            {
                this.ThrowNewDataProviderAssertionException(
                    DistributedCacheName,
                    "to have entry with the given options",
                    "in fact they were different");
            }

            return this;
        }

        public IAndDistributedCacheTestBuilder ContainingEntry(Action<IDistributedCacheEntryKeyTestBuilder> distributedCacheEntryTestBuilder)
        {
            var mockedDistributedCache = this.GetDistributedCacheMock();

            var newDistributedCacheEntryBuilder = new DistributedCacheEntryTestBuilder(this.TestContext);
            distributedCacheEntryTestBuilder(newDistributedCacheEntryBuilder);

            var expectedDistributedCacheEntry = newDistributedCacheEntryBuilder.DistributedCacheEntry;
            var entryKey = newDistributedCacheEntryBuilder.EntryKey;
            
            this.ContainingEntryWithKey(entryKey);

            mockedDistributedCache.TryGetCacheEntryOptions(entryKey, out var actualDistributedCacheEntryOptions);
            var actualDistributedCacheEntryValue
                = mockedDistributedCache.Get(entryKey);

            var actualDistributedCacheEntry = new DistributedCacheEntry(actualDistributedCacheEntryValue, actualDistributedCacheEntryOptions);

            var validations = newDistributedCacheEntryBuilder.GetDistributedCacheEntryValidations();
            validations.ForEach(v => v(expectedDistributedCacheEntry, actualDistributedCacheEntry));

            return this;
        }

        /// <inheritdoc />
        public IAndDistributedCacheTestBuilder ContainingEntryWithKey(string key)
        {
            this.GetValue(key);

            return this;
        }

        /// <inheritdoc />
        public IAndDistributedCacheTestBuilder ContainingEntryWithValue(byte[] value)
        {
            var hasValue = this.distributedCacheDictionary
                .Values
                .Any(v => Reflection.AreDeeplyEqual(v, value));

            if (!hasValue)
            {
                this.ThrowNewDataProviderAssertionException(
                    DistributedCacheName,
                    "to have an entry with the given value",
                    "such was not found");
            }

            return this;
        }

        /// <inheritdoc />
        public IDistributedCacheTestBuilder AndAlso() => this;

        private IDistributedCache GetDistributedCache()
        {
            return this.TestContext
                .HttpContext
                .RequestServices
                .GetRequiredService<IDistributedCache>();
        }

        private IDistributedCacheMock GetDistributedCacheMock()
        {
            if (this.distributedCacheMock == null)
            {
                this.distributedCacheMock = this.distributedCache.AsDistributedCacheMock();
            }

            return this.distributedCacheMock;
        }

        private Dictionary<string, byte[]> GetDistributedCacheDictionary()
            => this.distributedCacheMock.GetCacheAsDictionary();

        private byte[] GetValue(string key)
        {
            var value = this.distributedCache.Get(key);

            if (value == null)
            {
                this.ThrowNewDataProviderAssertionException(
                    DistributedCacheName,
                    "to have an entry with the given key",
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