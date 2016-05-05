namespace MyTested.Mvc.Builders.Contracts.ExceptionErrors
{
    /// <summary>
    /// Used for adding AndAlso() method to the <see cref="System.AggregateException"/> tests.
    /// </summary>
    public interface IAndAggregateExceptionTestBuilder : IAggregateExceptionTestBuilder
    {
        /// <summary>
        /// AndAlso method for better readability when chaining <see cref="System.AggregateException"/> tests.
        /// </summary>
        /// <returns>The same <see cref="IAggregateExceptionTestBuilder"/>.</returns>
        IAggregateExceptionTestBuilder AndAlso();
    }
}
