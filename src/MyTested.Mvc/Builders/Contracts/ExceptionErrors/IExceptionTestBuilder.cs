namespace MyTested.Mvc.Builders.Contracts.ExceptionErrors
{
    /// <summary>
    /// Used for testing expected exceptions.
    /// </summary>
    public interface IExceptionTestBuilder : IBaseExceptionTestBuilder
    {
        /// <summary>
        /// Tests whether certain type of exception is returned from the invoked action.
        /// </summary>
        /// <typeparam name="TException">Type of the expected exception.</typeparam>
        /// <returns>The same exception test builder.</returns>
        IAndExceptionTestBuilder OfType<TException>();
    }
}
