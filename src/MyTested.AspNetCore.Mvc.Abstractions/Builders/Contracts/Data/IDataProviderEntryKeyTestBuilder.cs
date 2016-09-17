namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Data
{
    /// <summary>
    /// Used for setting data provider entry key.
    /// </summary>
    public interface IDataProviderEntryKeyTestBuilder
    {
        /// <summary>
        /// Sets the key of the built data provider entry.
        /// </summary>
        /// <param name="key">Entry key to set.</param>
        /// <returns>The same <see cref="IAndDataProviderEntryTestBuilder"/>.</returns>
        IAndDataProviderEntryTestBuilder WithKey(string key);
    }
}
