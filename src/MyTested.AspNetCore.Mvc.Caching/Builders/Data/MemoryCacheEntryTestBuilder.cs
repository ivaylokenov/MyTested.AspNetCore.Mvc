namespace MyTested.AspNetCore.Mvc.Builders.Data
{
    using System;
    using System.Collections.Generic;
    using Contracts.Data;
    using Exceptions;
    using Internal.TestContexts;
    using Microsoft.Extensions.Caching.Memory;
    using Utilities;
    using Utilities.Extensions;
    using Utilities.Validators;

    /// <summary>
    /// Used for building mocked <see cref="IMemoryCache"/> entry.
    /// </summary>
    public class MemoryCacheEntryTestBuilder : MemoryCacheEntryBuilder, IAndMemoryCacheEntryTestBuilder
    {
        private readonly ComponentTestContext testContext;
        private readonly ICollection<Action<ICacheEntry, ICacheEntry>> validations;

        /// <summary>
        /// Initializes a new instance of the <see cref="MemoryCacheEntryTestBuilder"/> class.
        /// </summary>
        /// <param name="testContext"><see cref="ComponentTestContext"/> containing data about the currently executed assertion chain.</param>
        public MemoryCacheEntryTestBuilder(ComponentTestContext testContext)
        {
            CommonValidator.CheckForNullReference(testContext, nameof(testContext));

            this.testContext = testContext;
            this.validations = new List<Action<ICacheEntry, ICacheEntry>>();
        }

        /// <inheritdoc />
        public override IAndMemoryCacheEntryTestBuilder WithValue(object value)
        {
            this.validations.Add((expected, actual) =>
            {
                if (Reflection.AreNotDeeplyEqual(expected.Value, actual.Value))
                {
                    this.ThrowNewDataProviderAssertionException(
                        "to have entry with the given value",
                        "in fact it was different");
                }
            });

            return base.WithValue(value);
        }

        /// <inheritdoc />
        public override IAndMemoryCacheEntryTestBuilder WithAbsoluteExpiration(DateTimeOffset? absoluteExpiration)
        {
            this.validations.Add((expected, actual) =>
            {
                var expectedExpiration = expected.AbsoluteExpiration;
                var actualExpiration = actual.AbsoluteExpiration;

                if (expectedExpiration != actualExpiration)
                {
                    this.ThrowNewDataProviderAssertionException(
                        $"to have entry with {expectedExpiration.GetErrorMessageName()} absolute expiration",
                        $"in fact found {actualExpiration.GetErrorMessageName()}");
                }
            });

            return base.WithAbsoluteExpiration(absoluteExpiration);
        }

        /// <inheritdoc />
        public override IAndMemoryCacheEntryTestBuilder WithAbsoluteExpirationRelativeToNow(TimeSpan? absoluteExpirationRelativeToNow)
        {
            this.validations.Add((expected, actual) =>
            {
                var expectedRelativeExpiration = expected.AbsoluteExpirationRelativeToNow;
                var actualRelativeExpiration = actual.AbsoluteExpirationRelativeToNow;

                if (expectedRelativeExpiration != actualRelativeExpiration)
                {
                    this.ThrowNewDataProviderAssertionException(
                        $"to have entry with {expectedRelativeExpiration.GetErrorMessageName()} absolute expiration relative to now",
                        $"in fact found {actualRelativeExpiration.GetErrorMessageName()}");
                }
            });

            return base.WithAbsoluteExpirationRelativeToNow(absoluteExpirationRelativeToNow);
        }

        /// <inheritdoc />
        public override IAndMemoryCacheEntryTestBuilder WithPriority(CacheItemPriority priority)
        {
            this.validations.Add((expected, actual) =>
            {
                var expectedPriority = expected.Priority;
                var actualPriority = actual.Priority;

                if (expected.Priority != actual.Priority)
                {
                    this.ThrowNewDataProviderAssertionException(
                        $"to have entry with {expectedPriority.GetErrorMessageName(includeQuotes: false)} priority",
                        $"in fact found {actualPriority.GetErrorMessageName(includeQuotes: false)}");
                }
            });

            return base.WithPriority(priority);
        }

        /// <inheritdoc />
        public override IAndMemoryCacheEntryTestBuilder WithSlidingExpiration(TimeSpan? slidingExpiration)
        {
            this.validations.Add((expected, actual) =>
            {
                var expectedSlidingExpiration = expected.SlidingExpiration;
                var actualSlidingExpiration = actual.SlidingExpiration;

                if (expectedSlidingExpiration != actualSlidingExpiration)
                {
                    this.ThrowNewDataProviderAssertionException(
                        $"to have entry with {expectedSlidingExpiration.GetErrorMessageName()} sliding expiration",
                        $"in fact found {actualSlidingExpiration.GetErrorMessageName()}");
                }
            });

            return base.WithSlidingExpiration(slidingExpiration);
        }

        internal ICollection<Action<ICacheEntry, ICacheEntry>> GetMockedMemoryCacheEntryValidations()
            => this.validations;

        private void ThrowNewDataProviderAssertionException(string expectedValue, string actualValue)
        {
            throw new DataProviderAssertionException(string.Format(
                "When calling {0} action in {1} expected memory cache {2}, but {3}.",
                this.testContext.MethodName,
                this.testContext.Component.GetName(),
                expectedValue,
                actualValue));
        }
    }
}
