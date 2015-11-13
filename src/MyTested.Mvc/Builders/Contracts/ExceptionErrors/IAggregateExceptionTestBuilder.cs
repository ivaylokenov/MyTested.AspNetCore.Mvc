namespace MyTested.Mvc.Builders.Contracts.ExceptionErrors
{
    using System;

    /// <summary>
    /// Used for testing AggregateException.
    /// </summary>
    public interface IAggregateExceptionTestBuilder : IBaseExceptionTestBuilder
    {
        /// <summary>
        /// Tests whether AggregateException contains inner exception of the provided type.
        /// </summary>
        /// <typeparam name="TInnerException">Expected inner exception type.</typeparam>
        /// <returns>The same aggregate exception test builder.</returns>
        IAndAggregateExceptionTestBuilder ContainingInnerExceptionOfType<TInnerException>()
            where TInnerException : Exception;
    }
}
