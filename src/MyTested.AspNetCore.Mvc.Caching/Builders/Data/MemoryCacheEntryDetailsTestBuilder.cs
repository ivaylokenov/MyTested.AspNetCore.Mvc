namespace MyTested.AspNetCore.Mvc.Builders.Data
{
    using System;
    using Base;
    using Contracts.Data;
    using Utilities.Validators;

    /// <summary>
    /// Used for testing <see cref="Microsoft.Extensions.Caching.Memory.IMemoryCache"/> entry details.
    /// </summary>
    /// <typeparam name="TEntry">Type of <see cref="Microsoft.Extensions.Caching.Memory.IMemoryCache"/> entry value.</typeparam>
    public class MemoryCacheEntryDetailsTestBuilder<TEntry> : BaseTestBuilderWithComponent, IAndMemoryCacheEntryDetailsTestBuilder<TEntry>
    {
        private readonly MemoryCacheEntryTestBuilder memoryCacheEntryTestBuilder;

        /// <summary>
        /// Initializes a new instance of the <see cref="MemoryCacheEntryDetailsTestBuilder{TEntry}"/> class.
        /// </summary>
        /// <param name="memoryCacheEntryTestBuilder">Test builder of <see cref="MemoryCacheEntryTestBuilder"/> type.</param>
        public MemoryCacheEntryDetailsTestBuilder(MemoryCacheEntryTestBuilder memoryCacheEntryTestBuilder)
            : base(memoryCacheEntryTestBuilder.TestContext)
        {
            CommonValidator.CheckForNullReference(memoryCacheEntryTestBuilder, nameof(memoryCacheEntryTestBuilder));
            this.memoryCacheEntryTestBuilder = memoryCacheEntryTestBuilder;
        }

        /// <inheritdoc />
        public IAndMemoryCacheEntryDetailsTestBuilder<TEntry> Passing(Action<TEntry> assertions)
        {
            this.memoryCacheEntryTestBuilder
                .GetMockedMemoryCacheEntryValidations()
                .Add((expected, actual) => assertions((TEntry)actual));

            return this;
        }

        /// <inheritdoc />
        public IAndMemoryCacheEntryDetailsTestBuilder<TEntry> Passing(Func<TEntry, bool> predicate)
        {
            this.memoryCacheEntryTestBuilder
                .GetMockedMemoryCacheEntryValidations()
                .Add((expected, actual) =>
                {
                    if (!predicate((TEntry)actual))
                    {
                        var memoryCacheEntry = this.memoryCacheEntryTestBuilder.GetMockedMemoryCacheEntry();

                        this.memoryCacheEntryTestBuilder.ThrowNewDataProviderAssertionException(
                            $"to have entry with '{memoryCacheEntry.Key}' key and value passing the given predicate",
                            "it failed");
                    }
                });

            return this;
        }

        /// <inheritdoc />
        public IMemoryCacheEntryDetailsTestBuilder<TEntry> AndAlso() => this;
    }
}
