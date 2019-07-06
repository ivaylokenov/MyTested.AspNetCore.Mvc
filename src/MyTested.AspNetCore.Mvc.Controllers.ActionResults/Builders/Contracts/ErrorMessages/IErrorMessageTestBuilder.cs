namespace MyTested.AspNetCore.Mvc.Builders.Contracts.ErrorMessages
{
    using Base;

    /// <summary>
    /// Used for testing specific text error messages.
    /// </summary>
    public interface IErrorMessageTestBuilder<TTestBuilder> : IBaseTestBuilderWithInvokedAction
        where TTestBuilder : IBaseTestBuilderWithComponent
    {
        /// <summary>
        /// Tests whether particular error message is equal to the given message.
        /// </summary>
        /// <param name="errorMessage">Expected error message.</param>
        /// <returns>The original test builder.</returns>
        TTestBuilder ThatEquals(string errorMessage);

        /// <summary>
        /// Tests whether the particular error message begins with given message.
        /// </summary>
        /// <param name="beginMessage">Expected beginning of the particular error message.</param>
        /// <returns>The original test builder.</returns>
        TTestBuilder BeginningWith(string beginMessage);

        /// <summary>
        /// Tests whether the particular error message ends with given message.
        /// </summary>
        /// <param name="endMessage">Expected ending of the particular error message.</param>
        /// <returns>The original test builder.</returns>
        TTestBuilder EndingWith(string endMessage);

        /// <summary>
        /// Tests whether the particular error message contains given message.
        /// </summary>
        /// <param name="containsMessage">Expected containing string of the particular error message.</param>
        /// <returns>The original test builder.</returns>
        TTestBuilder Containing(string containsMessage);
    }
}
