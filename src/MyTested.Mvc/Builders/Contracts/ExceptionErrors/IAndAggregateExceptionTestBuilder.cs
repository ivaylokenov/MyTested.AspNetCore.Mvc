namespace MyTested.Mvc.Builders.Contracts.ExceptionErrors
{
    /// <summary>
    /// Used for adding AndAlso() method to the aggregate exception tests.
    /// </summary>
    public interface IAndAggregateExceptionTestBuilder : IAggregateExceptionTestBuilder
    {
        /// <summary>
        /// AndAlso method for better readability when chaining aggregate exception tests.
        /// </summary>
        /// <returns>The same aggregate exception test builder.</returns>
        IAggregateExceptionTestBuilder AndAlso();
    }
}
