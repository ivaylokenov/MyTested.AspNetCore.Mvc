namespace MyTested.AspNetCore.Mvc.Builders.Data.DistributedCache
{
    using System;
    using System.Collections.Generic;
    using Contracts.Data.DistributedCache;
    using Exceptions;
    using Internal.Caching;
    using Internal.TestContexts;
    using Utilities;
    using Utilities.Extensions;
    using Utilities.Validators;

    public class DistributedCacheEntryTestBuilder : DistributedCacheEntryBuilder, IDistributedCacheEntryKeyTestBuilder, IAndDistributedCacheEntryTestBuilder
    {
        private readonly ICollection<Action<DistributedCacheEntryMock, DistributedCacheEntryMock>> validations;

        public DistributedCacheEntryTestBuilder(ComponentTestContext testContext)
        {
            CommonValidator.CheckForNullReference(testContext, nameof(ComponentTestContext));

            this.validations = new List<Action<DistributedCacheEntryMock, DistributedCacheEntryMock>>();
            this.TestContext = testContext;
        }

        internal ComponentTestContext TestContext { get; private set; }

        public new IAndDistributedCacheEntryTestBuilder WithKey(string key)
        {
            base.WithKey(key);
            return this;
        }

        public new IAndDistributedCacheEntryTestBuilder WithValue(byte[] value)
        {
            this.validations.Add((expected, actual) =>
            {
                if (Reflection.AreNotDeeplyEqual(expected.Value, actual.Value))
                {
                    this.ThrowNewDataProviderAssertionException(
                        $"to have an entry with '{this.EntryKey}' key and the given value",
                        "in fact it was different");
                }
            });

            base.WithValue(value);
            return this;
        }

        public new IAndDistributedCacheEntryTestBuilder WithValue(string value)
        {
            this.WithValue(BytesHelper.GetBytes(value));
            return this;
        }

        public new IAndDistributedCacheEntryTestBuilder WithAbsoluteExpiration(DateTimeOffset? absoluteExpiration)
        {
            this.validations.Add((expected, actual) =>
            {
                var expectedExpiration = expected.Options.AbsoluteExpiration;
                var actualExpiration = actual.Options.AbsoluteExpiration;

                if (expectedExpiration != actualExpiration)
                {
                    this.ThrowNewDataProviderAssertionException(
                        $"to have an entry with '{this.EntryKey}' key and {expectedExpiration.ToFormattedString().GetErrorMessageName()} absolute expiration",
                        $"in fact found {actualExpiration.ToFormattedString().GetErrorMessageName()}");
                }
            });

            base.WithAbsoluteExpiration(absoluteExpiration);
            return this;
        }

        public new IAndDistributedCacheEntryTestBuilder WithAbsoluteExpirationRelativeToNow(TimeSpan? absoluteExpirationRelativeToNow)
        {
            this.validations.Add((expected, actual) =>
            {
                var expectedRelativeExpiration = expected.Options.AbsoluteExpirationRelativeToNow;
                var actualRelativeExpiration = actual.Options.AbsoluteExpirationRelativeToNow;

                if (expectedRelativeExpiration != actualRelativeExpiration)
                {
                    this.ThrowNewDataProviderAssertionException(
                        $"to have an entry with '{this.EntryKey}' key and {expectedRelativeExpiration.GetErrorMessageName()} absolute expiration relative to now",
                        $"in fact found {actualRelativeExpiration.GetErrorMessageName()}");
                }
            });

            base.WithAbsoluteExpirationRelativeToNow(absoluteExpirationRelativeToNow);
            return this;
        }

        public new IAndDistributedCacheEntryTestBuilder WithSlidingExpiration(TimeSpan? slidingExpiration)
        {
            this.validations.Add((expected, actual) =>
            {
                var expectedSlidingExpiration = expected.Options.SlidingExpiration;
                var actualSlidingExpiration = actual.Options.SlidingExpiration;

                if (expectedSlidingExpiration != actualSlidingExpiration)
                {
                    this.ThrowNewDataProviderAssertionException(
                        $"to have an entry with '{this.EntryKey}' key and {expectedSlidingExpiration.GetErrorMessageName()} sliding expiration",
                        $"in fact found {actualSlidingExpiration.GetErrorMessageName()}");
                }
            });

            base.WithSlidingExpiration(slidingExpiration);
            return this;
        }

        public new IDistributedCacheEntryTestBuilder AndAlso() => this;

        internal ICollection<Action<DistributedCacheEntryMock, DistributedCacheEntryMock>> GetDistributedCacheEntryValidations()
            => this.validations;

        internal void ThrowNewDataProviderAssertionException(string expectedValue, string actualValue)
            => throw new DataProviderAssertionException(string.Format(
                "{0} distributed cache {1}, but {2}.",
                this.TestContext.ExceptionMessagePrefix,
                expectedValue,
                actualValue));
    }
}
