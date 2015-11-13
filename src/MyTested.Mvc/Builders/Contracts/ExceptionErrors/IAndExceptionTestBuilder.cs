namespace MyTested.Mvc.Builders.Contracts.ExceptionErrors
{
    /// <summary>
    /// Used for adding AndAlso() method to the expected exception tests.
    /// </summary>
    public interface IAndExceptionTestBuilder : IExceptionTestBuilder
    {
        /// <summary>
        /// AndAlso method for better readability when chaining expected exception tests.
        /// </summary>
        /// <returns>The same exception test builder.</returns>
        IExceptionTestBuilder AndAlso();
    }
}
