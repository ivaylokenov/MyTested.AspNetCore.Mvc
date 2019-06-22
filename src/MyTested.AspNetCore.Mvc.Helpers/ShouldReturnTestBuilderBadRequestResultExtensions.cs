namespace MyTested.AspNetCore.Mvc
{
    using Builders.Contracts.Actions;
    using Builders.Contracts.And;
    using Microsoft.AspNetCore.Mvc.ModelBinding;

    /// <summary>
    /// Contains <see cref="Microsoft.AspNetCore.Mvc.BadRequestResult"/> and <see cref="Microsoft.AspNetCore.Mvc.BadRequestObjectResult"/>
    /// extension methods for <see cref="IShouldReturnTestBuilder{TActionResult}"/>.
    /// </summary>
    public static class ShouldReturnTestBuilderBadRequestResultExtensions
    {
        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.BadRequestObjectResult"/>
        /// with the same error object as the provided one.
        /// </summary>
        /// <typeparam name="TActionResult">Type of the action result.</typeparam>
        /// <typeparam name="TError">Expected error type.</typeparam>
        /// <param name="shouldReturnTestBuilder">Instance of <see cref="IShouldReturnTestBuilder{TActionResult}"/> type.</param>
        /// <param name="error">Expected error object.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        public static IAndTestBuilder BadRequest<TActionResult, TError>(
            this IShouldReturnTestBuilder<TActionResult> shouldReturnTestBuilder,
            TError error)
            => shouldReturnTestBuilder
                .BadRequest(result => result
                    .WithError(error));

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.BadRequestObjectResult"/>
        /// with the same model state error as the provided one.
        /// </summary>
        /// <typeparam name="TActionResult">Type of the action result.</typeparam>
        /// <param name="shouldReturnTestBuilder">Instance of <see cref="IShouldReturnTestBuilder{TActionResult}"/> type.</param>
        /// <param name="modelState">Expected model state dictionary.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        public static IAndTestBuilder BadRequest<TActionResult>(
            this IShouldReturnTestBuilder<TActionResult> shouldReturnTestBuilder,
            ModelStateDictionary modelState)
            => shouldReturnTestBuilder
                .BadRequest(result => result
                    .WithModelStateError(modelState));
    }
}
