namespace MyTested.Mvc.Builders.Contracts.ExceptionErrors
{
    using Base;

    /// <summary>
    /// Used for testing specific <see cref="System.Exception"/> messages.
    /// </summary>
    public interface IExceptionMessageTestBuilder : IBaseTestBuilderWithInvokedAction
    {
        /// <summary>
        /// Tests whether the <see cref="System.Exception.Message"/> is equal to given message.
        /// </summary>
        /// <param name="errorMessage">Expected error message for the <see cref="System.Exception"/>.</param>
        /// <returns>Test builder of <see cref="IAndExceptionTestBuilder"/> type.</returns>
        IAndExceptionTestBuilder ThatEquals(string errorMessage);

        /// <summary>
        /// Tests whether the <see cref="System.Exception.Message"/> begins with given message.
        /// </summary>
        /// <param name="beginMessage">Expected beginning for the <see cref="System.Exception.Message"/>.</param>
        /// <returns>Test builder of <see cref="IAndExceptionTestBuilder"/> type.</returns>
        IAndExceptionTestBuilder BeginningWith(string beginMessage);

        /// <summary>
        /// Tests whether the <see cref="System.Exception.Message"/> ends with given message.
        /// </summary>
        /// <param name="endMessage">Expected ending for the <see cref="System.Exception.Message"/>.</param>
        /// <returns>Test builder of <see cref="IAndExceptionTestBuilder"/> type.</returns>
        IAndExceptionTestBuilder EndingWith(string endMessage);

        /// <summary>
        /// Tests whether the <see cref="System.Exception.Message"/> contains given message.
        /// </summary>
        /// <param name="containsMessage">Expected containing string for the <see cref="System.Exception.Message"/>.</param>
        /// <returns>Test builder of <see cref="IAndExceptionTestBuilder"/> type.</returns>
        IAndExceptionTestBuilder Containing(string containsMessage);
    }
}
