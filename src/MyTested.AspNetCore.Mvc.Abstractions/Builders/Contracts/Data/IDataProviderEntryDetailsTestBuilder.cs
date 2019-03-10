namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Data
{
    using System;

    /// <summary>
    /// Used for testing data provider entry details.
    /// </summary>
    /// <typeparam name="TValue">Type of data provider entry value.</typeparam>
    public interface IDataProviderEntryDetailsTestBuilder<TValue>
    {
        /// <summary>
        /// Tests whether the data provider entry passes the given assertions.
        /// </summary>
        /// <param name="assertions">Action containing all assertions for the data provider entry.</param>
        /// <returns>The same <see cref="IAndDataProviderEntryDetailsTestBuilder{TValue}"/>.</returns>
        IAndDataProviderEntryDetailsTestBuilder<TValue> Passing(Action<TValue> assertions);

        /// <summary>
        /// Tests whether the data provider entry passes the given predicate.
        /// </summary>
        /// <param name="predicate">Predicate testing the data provider entry.</param>
        /// <returns>The same <see cref="IAndDataProviderEntryDetailsTestBuilder{TValue}"/>.</returns>
        IAndDataProviderEntryDetailsTestBuilder<TValue> Passing(Func<TValue, bool> predicate);
    }
}
