namespace MyTested.AspNetCore.Mvc
{
    using Builders.Contracts.ActionResults.BadRequest;
    using Builders.Contracts.Base;
    using Builders.Contracts.Models;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Contains extension methods for <see cref="IBadRequestTestBuilder"/>.
    /// </summary>
    public static class BadRequestTestBuilderExtensions
    {
        /// <summary>
        /// Tests whether <see cref="BadRequestObjectResult"/> contains deeply equal error value as the provided error object.
        /// </summary>
        /// <typeparam name="TError">Type of error object.</typeparam>
        /// <param name="builder">Instance of <see cref="IBadRequestTestBuilder"/> type.</param>
        /// <param name="error">Error object.</param>
        /// <returns>Test builder of type <see cref="IModelDetailsTestBuilder{TError}"/>.</returns>
        public static IAndModelDetailsTestBuilder<TError> WithError<TError>(
            this IBadRequestTestBuilder builder,
            TError error)
        {
            var actualBuilder = (IBaseTestBuilderWithResponseModel)builder;
            return actualBuilder.WithModel(error);
        }

        /// <summary>
        /// Tests whether <see cref="BadRequestObjectResult"/> contains error object of the provided type.
        /// </summary>
        /// <param name="builder">Instance of <see cref="IBadRequestTestBuilder"/> type.</param>
        /// <typeparam name="TError">Type of error object.</typeparam>
        /// <returns>Test builder of type <see cref="IModelDetailsTestBuilder{TError}"/>.</returns>
        public static IAndModelDetailsTestBuilder<TError> WithErrorOfType<TError>(this IBadRequestTestBuilder builder)
        {
            var actualBuilder = (IBaseTestBuilderWithResponseModel)builder;
            return actualBuilder.WithModelOfType<TError>();
        }
    }
}
