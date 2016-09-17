namespace MyTested.AspNetCore.Mvc.Builders.Contracts.CaughtExceptions
{
    using Base;

    /// <summary>
    /// Used for testing expected <see cref="System.Exception"/> messages.
    /// </summary>
    public interface IBaseExceptionTestBuilder : IBaseTestBuilderWithComponent
    {
        /// <summary>
        /// Tests <see cref="System.Exception.Message"/> using test builder.
        /// </summary>
        /// <returns>Test builder of <see cref="IExceptionMessageTestBuilder"/> type.</returns>
        IExceptionMessageTestBuilder WithMessage();

        /// <summary>
        /// Tests <see cref="System.Exception.Message"/> whether it is equal to the provided message as string.
        /// </summary>
        /// <param name="message">Expected <see cref="System.Exception.Message"/> as string.</param>
        /// <returns>The same <see cref="IAndExceptionTestBuilder"/>.</returns>
        IAndExceptionTestBuilder WithMessage(string message);
    }
}
