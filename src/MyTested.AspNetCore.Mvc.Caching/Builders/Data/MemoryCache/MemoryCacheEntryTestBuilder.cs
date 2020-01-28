namespace MyTested.AspNetCore.Mvc.Builders.Data.MemoryCache
{
    using System;
    using System.Collections.Generic;
    using Contracts.Data.MemoryCache;
    using Exceptions;
    using Internal.TestContexts;
    using Microsoft.Extensions.Caching.Memory;
    using Utilities;
    using Utilities.Extensions;
    using Utilities.Validators;

    /// <summary>
    /// Used for building mocked <see cref="IMemoryCache"/> entry.
    /// </summary>
    public class MemoryCacheEntryTestBuilder : MemoryCacheEntryBuilder, IMemoryCacheEntryKeyTestBuilder, IAndMemoryCacheEntryTestBuilder
    {
        private readonly ICollection<Action<ICacheEntry, ICacheEntry>> validations;

        /// <summary>
        /// Initializes a new instance of the <see cref="MemoryCacheEntryTestBuilder"/> class.
        /// </summary>
        /// <param name="testContext"><see cref="ComponentTestContext"/> containing data about the currently executed assertion chain.</param>
        public MemoryCacheEntryTestBuilder(ComponentTestContext testContext)
        {
            CommonValidator.CheckForNullReference(testContext, nameof(ComponentTestContext));

            this.validations = new List<Action<ICacheEntry, ICacheEntry>>();
            this.TestContext = testContext;
        }

        internal ComponentTestContext TestContext { get; private set; }

        public new IAndMemoryCacheEntryTestBuilder WithKey(object key)
        {
            base.WithKey(key);
            return this;
        }

        /// <inheritdoc />
        public new IAndMemoryCacheEntryTestBuilder WithValue(object value)
        {
            this.validations.Add((expected, actual) =>
            {
                if (Reflection.AreNotDeeplyEqual(expected.Value, actual.Value))
                {
                    this.ThrowNewDataProviderAssertionException(
                        $"to have entry with '{this.MemoryCacheEntry.Key}' key and the given value",
                        "in fact it was different");
                }
            });

            base.WithValue(value);
            return this;
        }
        
        /// <inheritdoc />
        public IMemoryCacheEntryDetailsTestBuilder<TValue> WithValueOfType<TValue>()
        {
            this.validations.Add((expected, actual) =>
            {
                var expectedType = typeof(TValue);
                var actualType = actual.Value.GetType();

                if (Reflection.AreDifferentTypes(expectedType, actualType))
                {
                    this.ThrowNewDataProviderAssertionException(
                        $"to have entry with '{this.MemoryCacheEntry.Key}' key and value of {expectedType.ToFriendlyTypeName()} type",
                        $"in fact found {actualType.ToFriendlyTypeName()}");
                }
            });

            return new MemoryCacheEntryDetailsTestBuilder<TValue>(this);
        }

        /// <inheritdoc />
        public new IAndMemoryCacheEntryTestBuilder WithAbsoluteExpiration(DateTimeOffset? absoluteExpiration)
        {
            this.validations.Add((expected, actual) =>
            {
                var expectedExpiration = expected.AbsoluteExpiration;
                var actualExpiration = actual.AbsoluteExpiration;

                if (expectedExpiration != actualExpiration)
                {
                    this.ThrowNewDataProviderAssertionException(
                        $"to have entry with '{this.MemoryCacheEntry.Key}' key and {expectedExpiration.ToFormattedString().GetErrorMessageName()} absolute expiration",
                        $"in fact found {actualExpiration.ToFormattedString().GetErrorMessageName()}");
                }
            });

            base.WithAbsoluteExpiration(absoluteExpiration);
            return this;
        }

        /// <inheritdoc />
        public new IAndMemoryCacheEntryTestBuilder WithAbsoluteExpirationRelativeToNow(TimeSpan? absoluteExpirationRelativeToNow)
        {
            this.validations.Add((expected, actual) =>
            {
                var expectedRelativeExpiration = expected.AbsoluteExpirationRelativeToNow;
                var actualRelativeExpiration = actual.AbsoluteExpirationRelativeToNow;

                if (expectedRelativeExpiration != actualRelativeExpiration)
                {
                    this.ThrowNewDataProviderAssertionException(
                        $"to have entry with '{this.MemoryCacheEntry.Key}' key and {expectedRelativeExpiration.GetErrorMessageName()} absolute expiration relative to now",
                        $"in fact found {actualRelativeExpiration.GetErrorMessageName()}");
                }
            });

            base.WithAbsoluteExpirationRelativeToNow(absoluteExpirationRelativeToNow);
            return this;
        }

        /// <inheritdoc />
        public new IAndMemoryCacheEntryTestBuilder WithPriority(CacheItemPriority priority)
        {
            this.validations.Add((expected, actual) =>
            {
                var expectedPriority = expected.Priority;
                var actualPriority = actual.Priority;

                if (expected.Priority != actual.Priority)
                {
                    this.ThrowNewDataProviderAssertionException(
                        $"to have entry with '{this.MemoryCacheEntry.Key}' key and {expectedPriority.GetErrorMessageName(includeQuotes: false)} priority",
                        $"in fact found {actualPriority.GetErrorMessageName(includeQuotes: false)}");
                }
            });

            base.WithPriority(priority);
            return this;
        }

        /// <inheritdoc />
        public new IAndMemoryCacheEntryTestBuilder WithSlidingExpiration(TimeSpan? slidingExpiration)
        {
            this.validations.Add((expected, actual) =>
            {
                var expectedSlidingExpiration = expected.SlidingExpiration;
                var actualSlidingExpiration = actual.SlidingExpiration;

                if (expectedSlidingExpiration != actualSlidingExpiration)
                {
                    this.ThrowNewDataProviderAssertionException(
                        $"to have entry with '{this.MemoryCacheEntry.Key}' key and {expectedSlidingExpiration.GetErrorMessageName()} sliding expiration",
                        $"in fact found {actualSlidingExpiration.GetErrorMessageName()}");
                }
            });

            base.WithSlidingExpiration(slidingExpiration);
            return this;
        }

        /// <inheritdoc />
        public new IMemoryCacheEntryTestBuilder AndAlso() => this;

        internal ICollection<Action<ICacheEntry, ICacheEntry>> GetMemoryCacheEntryMockValidations()
            => this.validations;

        internal void ThrowNewDataProviderAssertionException(string expectedValue, string actualValue)
        {
            throw new DataProviderAssertionException(string.Format(
                "{0} memory cache {1}, but {2}.",
                this.TestContext.ExceptionMessagePrefix,
                expectedValue,
                actualValue));
        }
    }
}
