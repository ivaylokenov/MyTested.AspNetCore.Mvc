namespace MyTested.AspNetCore.Mvc.Builders.Contracts.ExceptionErrors
{
    using System;

    /// <summary>
    /// Used for testing <see cref="AggregateException"/>.
    /// </summary>
    public interface IAggregateExceptionTestBuilder : IBaseExceptionTestBuilder
    {
        /// <summary>
        /// Tests whether <see cref="AggregateException"/> contains inner exception of the provided type.
        /// </summary>
        /// <typeparam name="TInnerException">Expected inner exception type.</typeparam>
        /// <returns>The same <see cref="IAndAggregateExceptionTestBuilder"/>.</returns>
        IAndAggregateExceptionTestBuilder ContainingInnerExceptionOfType<TInnerException>()
            where TInnerException : Exception;
    }
}
