namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Data
{
    /// <summary>
    /// Used for testing data provider entry.
    /// </summary>
    public interface IDataProviderEntryTestBuilder
    {
        /// <summary>
        /// Tests whether the value of the built data provider entry is deeply equal to the provided one.
        /// </summary>
        /// <param name="value">Value of the data provider entry.</param>
        /// <returns>The same <see cref="IAndDataProviderEntryTestBuilder"/>.</returns>
        IAndDataProviderEntryTestBuilder WithValue(object value);

        /// <summary>
        /// Tests whether the value of the built data provider entry is of the same type as the provided one.
        /// </summary>
        /// <typeparam name="TEntry">Type of the data provider entry.</typeparam>
        /// <returns>The same <see cref="IAndDataProviderEntryTestBuilder"/>.</returns>
        IAndDataProviderEntryDetailsTestBuilder<TEntry> WithValueOfType<TEntry>();
    }
}
