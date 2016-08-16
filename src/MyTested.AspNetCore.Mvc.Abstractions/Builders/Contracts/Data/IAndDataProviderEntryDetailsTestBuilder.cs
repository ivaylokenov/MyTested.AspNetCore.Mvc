namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Data
{
    /// <summary>
    /// Used for adding AndAlso() method to the data provider entry details tests.
    /// </summary>
    public interface IAndDataProviderEntryDetailsTestBuilder<TEntry> : IDataProviderEntryDetailsTestBuilder<TEntry>
    {
        /// <summary>
        /// AndAlso method for better readability when chaining data provider entry details tests.
        /// </summary>
        /// <returns>The same <see cref="IDataProviderEntryDetailsTestBuilder{TEntry}"/>.</returns>
        IDataProviderEntryDetailsTestBuilder<TEntry> AndAlso();
    }
}
