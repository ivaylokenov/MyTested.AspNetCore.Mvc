namespace MyTested.Mvc.Builders.Contracts.ExceptionErrors
{
    /// <summary>
    /// Used for testing expected <see cref="System.Exception"/>.
    /// </summary>
    public interface IExceptionTestBuilder : IBaseExceptionTestBuilder
    {
        /// <summary>
        /// Tests whether certain type of <see cref="System.Exception"/> is thrown from the invoked action.
        /// </summary>
        /// <typeparam name="TException">Type of the expected <see cref="System.Exception"/>.</typeparam>
        /// <returns>The same <see cref="IAndExceptionTestBuilder"/>.</returns>
        IAndExceptionTestBuilder OfType<TException>();
    }
}
