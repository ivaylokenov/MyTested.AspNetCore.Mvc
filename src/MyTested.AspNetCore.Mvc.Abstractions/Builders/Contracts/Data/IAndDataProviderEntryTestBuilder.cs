namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Data
{
    /// <summary>
    /// Used for adding AndAlso() method to the data provider entry tests.
    /// </summary>
    public interface IAndDataProviderEntryTestBuilder : IDataProviderEntryTestBuilder
    {
        /// <summary>
        /// AndAlso method for better readability when chaining data provider entry tests.
        /// </summary>
        /// <returns>The same <see cref="IDataProviderEntryTestBuilder"/>.</returns>
        IDataProviderEntryTestBuilder AndAlso();
    }
}
