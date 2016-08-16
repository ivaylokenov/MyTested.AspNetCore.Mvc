namespace MyTested.AspNetCore.Mvc.Builders.Data
{
    using System;
    using Base;
    using Contracts.Data;
    using Utilities.Validators;

    /// <summary>
    /// Used for testing data provider entry details.
    /// </summary>
    /// <typeparam name="TEntry">Type of data provider entry value.</typeparam>
    public class DataProviderEntryDetailsTestBuilder<TEntry> : BaseTestBuilderWithComponent, IAndDataProviderEntryDetailsTestBuilder<TEntry>
    {
        private readonly DataProviderEntryTestBuilder dataProviderEntryTestBuilder;

        /// <summary>
        /// Initializes a new instance of the <see cref="DataProviderEntryDetailsTestBuilder{TEntry}"/> class.
        /// </summary>
        /// <param name="dataProviderEntryTestBuilder">Test builder of <see cref="DataProviderEntryTestBuilder"/> type.</param>
        public DataProviderEntryDetailsTestBuilder(DataProviderEntryTestBuilder dataProviderEntryTestBuilder)
            : base(dataProviderEntryTestBuilder.TestContext)
        {
            CommonValidator.CheckForNullReference(dataProviderEntryTestBuilder, nameof(dataProviderEntryTestBuilder));
            this.dataProviderEntryTestBuilder = dataProviderEntryTestBuilder;
        }

        /// <inheritdoc />
        public IAndDataProviderEntryDetailsTestBuilder<TEntry> Passing(Action<TEntry> assertions)
        {
            this.dataProviderEntryTestBuilder
                .GetDataProviderEntryValidations()
                .Add(actual => assertions((TEntry)actual));

            return this;
        }

        /// <inheritdoc />
        public IAndDataProviderEntryDetailsTestBuilder<TEntry> Passing(Func<TEntry, bool> predicate)
        {
            this.dataProviderEntryTestBuilder
                .GetDataProviderEntryValidations()
                .Add(actual =>
                {
                    if (!predicate((TEntry)actual))
                    {
                        var entryKey = this.dataProviderEntryTestBuilder.GetDataProviderEntryKey();

                        this.dataProviderEntryTestBuilder.ThrowNewDataProviderAssertionException(
                            $"to have entry with '{entryKey}' key and value passing the given predicate",
                            "it failed");
                    }
                });

            return this;
        }

        /// <inheritdoc />
        public IDataProviderEntryDetailsTestBuilder<TEntry> AndAlso() => this;
    }
}
