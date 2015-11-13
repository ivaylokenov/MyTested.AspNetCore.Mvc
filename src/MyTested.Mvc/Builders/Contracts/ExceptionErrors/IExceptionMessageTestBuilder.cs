namespace MyTested.Mvc.Builders.Contracts.ExceptionErrors
{
    using Base;

    /// <summary>
    /// Used for testing specific exception messages.
    /// </summary>
    public interface IExceptionMessageTestBuilder : IBaseTestBuilderWithCaughtException
    {
        /// <summary>
        /// Tests whether particular exception message is equal to given message.
        /// </summary>
        /// <param name="errorMessage">Expected error message for particular exception.</param>
        /// <returns>Exception test builder.</returns>
        IAndExceptionTestBuilder ThatEquals(string errorMessage);

        /// <summary>
        /// Tests whether particular exception message begins with given message.
        /// </summary>
        /// <param name="beginMessage">Expected beginning for particular exception message.</param>
        /// <returns>Exception test builder.</returns>
        IAndExceptionTestBuilder BeginningWith(string beginMessage);

        /// <summary>
        /// Tests whether particular exception message ends with given message.
        /// </summary>
        /// <param name="endMessage">Expected ending for particular exception message.</param>
        /// <returns>Exception test builder.</returns>
        IAndExceptionTestBuilder EndingWith(string endMessage);

        /// <summary>
        /// Tests whether particular exception message contains given message.
        /// </summary>
        /// <param name="containsMessage">Expected containing string for particular exception message.</param>
        /// <returns>Exception test builder.</returns>
        IAndExceptionTestBuilder Containing(string containsMessage);
    }
}
