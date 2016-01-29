namespace MyTested.Mvc.Builders.Contracts.ExceptionErrors
{
    using Base;

    /// <summary>
    /// Used for testing expected exception messages.
    /// </summary>
    public interface IBaseExceptionTestBuilder : IBaseTestBuilderWithInvokedAction
    {
        /// <summary>
        /// Tests exception message using test builder.
        /// </summary>
        /// <returns>Exception message test builder.</returns>
        IExceptionMessageTestBuilder WithMessage();

        /// <summary>
        /// Tests exception message whether it is equal to the provided message as string.
        /// </summary>
        /// <param name="message">Expected exception message as string.</param>
        /// <returns>The same exception test builder.</returns>
        IAndExceptionTestBuilder WithMessage(string message);
    }
}
