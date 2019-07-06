namespace MyTested.AspNetCore.Mvc
{
    using Builders.Contracts.ActionResults.Base;
    using Builders.Contracts.Base;
    using Builders.Contracts.Models;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Contains extension methods for <see cref="IBaseTestBuilderWithErrorResult"/>.
    /// </summary>
    public static class BaseTestBuilderWithErrorResultExtensions
    {
        /// <summary>
        /// Tests whether the <see cref="ActionResult"/> contains
        /// deeply equal error value as the provided error object.
        /// </summary>
        /// <typeparam name="TError">Type of error object.</typeparam>
        /// <param name="baseTestBuilderWithErrorResult">
        /// Instance of <see cref="IBaseTestBuilderWithErrorResult"/> type.
        /// </param>
        /// <param name="error">Error object.</param>
        /// <returns>Test builder of type <see cref="IModelDetailsTestBuilder{TModel}"/>.</returns>
        public static IAndModelDetailsTestBuilder<TError> WithError<TError>(
            this IBaseTestBuilderWithErrorResult baseTestBuilderWithErrorResult,
            TError error)
        {
            var actualBuilder = (IBaseTestBuilderWithResponseModel)baseTestBuilderWithErrorResult;
            return actualBuilder.WithModel(error);
        }

        /// <summary>
        /// Tests whether the <see cref="ActionResult"/>
        /// contains error object of the provided type.
        /// </summary>
        /// <param name="baseTestBuilderWithErrorResult">
        /// Instance of <see cref="IBaseTestBuilderWithErrorResult"/> type.
        /// </param>
        /// <typeparam name="TError">Type of error object.</typeparam>
        /// <returns>Test builder of type <see cref="IModelDetailsTestBuilder{TError}"/>.</returns>
        public static IAndModelDetailsTestBuilder<TError> WithErrorOfType<TError>(
            this IBaseTestBuilderWithErrorResult baseTestBuilderWithErrorResult)
        {
            var actualBuilder = (IBaseTestBuilderWithResponseModel)baseTestBuilderWithErrorResult;
            return actualBuilder.WithModelOfType<TError>();
        }
    }
}
