namespace MyTested.AspNetCore.Mvc.Builders.Data
{
    using System;
    using Contracts.Data;
    using Utilities.Validators;

    /// <summary>
    /// Used for testing <see cref="Microsoft.Extensions.Caching.Memory.IMemoryCache"/> entry details.
    /// </summary>
    /// <typeparam name="TValue">Type of <see cref="Microsoft.Extensions.Caching.Memory.IMemoryCache"/> entry value.</typeparam>
    public class MemoryCacheEntryDetailsTestBuilder<TValue> : MemoryCacheEntryTestBuilder, IMemoryCacheEntryDetailsTestBuilder<TValue>
    {
        private readonly MemoryCacheEntryTestBuilder memoryCacheEntryTestBuilder;

        /// <summary>
        /// Initializes a new instance of the <see cref="MemoryCacheEntryDetailsTestBuilder{TValue}"/> class.
        /// </summary>
        /// <param name="memoryCacheEntryTestBuilder">Test builder of <see cref="MemoryCacheEntryTestBuilder"/> type.</param>
        public MemoryCacheEntryDetailsTestBuilder(MemoryCacheEntryTestBuilder memoryCacheEntryTestBuilder)
            : base(memoryCacheEntryTestBuilder.TestContext)
        {
            CommonValidator.CheckForNullReference(memoryCacheEntryTestBuilder, nameof(memoryCacheEntryTestBuilder));
            this.memoryCacheEntryTestBuilder = memoryCacheEntryTestBuilder;
        }

        /// <inheritdoc />
        public IAndMemoryCacheEntryTestBuilder Passing(Action<TValue> assertions)
        {
            this.memoryCacheEntryTestBuilder
                .GetMemoryCacheEntryMockValidations()
                .Add((expected, actual) => assertions((TValue)actual.Value));

            return this;
        }

        /// <inheritdoc />
        public IAndMemoryCacheEntryTestBuilder Passing(Func<TValue, bool> predicate)
        {
            this.memoryCacheEntryTestBuilder
                .GetMemoryCacheEntryMockValidations()
                .Add((expected, actual) =>
                {
                    if (!predicate((TValue)actual.Value))
                    {
                        var memoryCacheEntry = this.memoryCacheEntryTestBuilder.GetMemoryCacheEntryMock();

                        this.memoryCacheEntryTestBuilder.ThrowNewDataProviderAssertionException(
                            $"to have entry with '{memoryCacheEntry.Key}' key and value passing the given predicate",
                            "it failed");
                    }
                });

            return this;
        }
    }
}
