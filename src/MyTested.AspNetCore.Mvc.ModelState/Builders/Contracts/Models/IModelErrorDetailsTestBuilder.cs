namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Models
{
    using Base;

    /// <summary>
    /// Used for testing specific <see cref="Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary"/> errors.
    /// </summary>
    public interface IModelErrorDetailsTestBuilder : IBaseTestBuilderWithComponent
    {
        /// <summary>
        /// Tests whether the error message is equal to given message.
        /// </summary>
        /// <param name="errorMessage">Expected error message.</param>
        /// <returns>Test builder of <see cref="IAndModelErrorTestBuilder"/>.</returns>
        IAndModelErrorTestBuilder ThatEquals(string errorMessage);

        /// <summary>
        /// Tests whether the error message begins with given message.
        /// </summary>
        /// <param name="beginMessage">Expected beginning for the error message.</param>
        /// <returns>Test builder of <see cref="IAndModelErrorTestBuilder"/>.</returns>
        IAndModelErrorTestBuilder BeginningWith(string beginMessage);

        /// <summary>
        /// Tests whether the error message ends with given message.
        /// </summary>
        /// <param name="endMessage">Expected ending for the error message.</param>
        /// <returns>Test builder of <see cref="IAndModelErrorTestBuilder"/>.</returns>
        IAndModelErrorTestBuilder EndingWith(string endMessage);

        /// <summary>
        /// Tests whether the error message contains given message.
        /// </summary>
        /// <param name="containsMessage">Expected containing string for the error message.</param>
        /// <returns>Test builder of <see cref="IAndModelErrorTestBuilder"/>.</returns>
        IAndModelErrorTestBuilder Containing(string containsMessage);

        /// <summary>
        /// Tests whether the tested <see cref="Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary"/> contains error by key.
        /// </summary>
        /// <param name="errorKey">Error key to search for.</param>
        /// <returns>The same <see cref="IModelErrorDetailsTestBuilder"/>.</returns>
        IModelErrorDetailsTestBuilder ContainingError(string errorKey);

        /// <summary>
        /// AndAlso method for better readability when chaining error message tests.
        /// </summary>
        /// <returns>Test builder of <see cref="IModelErrorTestBuilder"/> type.</returns>
        IModelErrorTestBuilder AndAlso();
    }
}
