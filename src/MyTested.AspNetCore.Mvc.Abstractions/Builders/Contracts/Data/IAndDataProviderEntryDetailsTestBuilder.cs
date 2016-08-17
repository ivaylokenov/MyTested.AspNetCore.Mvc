namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Data
{
    /// <summary>
    /// Used for adding AndAlso() method to the data provider entry details tests.
    /// </summary>
    /// <typeparam name="TValue">Type of data provider entry value.</typeparam>
    public interface IAndDataProviderEntryDetailsTestBuilder<TValue> : IDataProviderEntryDetailsTestBuilder<TValue>
    {
        /// <summary>
        /// AndAlso method for better readability when chaining data provider entry details tests.
        /// </summary>
        /// <returns>The same <see cref="IDataProviderEntryDetailsTestBuilder{TValue}"/>.</returns>
        IDataProviderEntryDetailsTestBuilder<TValue> AndAlso();
    }
}
