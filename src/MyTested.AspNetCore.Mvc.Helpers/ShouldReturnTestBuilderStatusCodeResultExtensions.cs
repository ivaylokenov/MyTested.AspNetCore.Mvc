namespace MyTested.AspNetCore.Mvc
{
    using Builders.Contracts.Actions;
    using Builders.Contracts.And;
    
    /// <summary>
    /// Contains <see cref="Microsoft.AspNetCore.Mvc.StatusCodeResult"/>
    /// extension methods for <see cref="IShouldReturnTestBuilder{TActionResult}"/>.
    /// </summary>
    public static class ShouldReturnTestBuilderStatusCodeResultExtensions
    {
        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.StatusCodeResult"/>
        /// or <see cref="Microsoft.AspNetCore.Mvc.ObjectResult"/> and it has the same status code as the provided one.
        /// </summary>
        /// <typeparam name="TActionResult">Type of the action result.</typeparam>
        /// <param name="shouldReturnTestBuilder">Instance of <see cref="IShouldReturnTestBuilder{TActionResult}"/> type.</param>
        /// <param name="statusCode">Status code as integer.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        public static IAndTestBuilder StatusCode<TActionResult>(
            this IShouldReturnTestBuilder<TActionResult> shouldReturnTestBuilder,
            int statusCode)
            => shouldReturnTestBuilder
                .StatusCode(result => result
                    .WithStatusCode(statusCode));

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.StatusCodeResult"/>
        /// or <see cref="Microsoft.AspNetCore.Mvc.ObjectResult"/> with the same status code
        /// and deeply equal model as the provided ones.
        /// </summary>
        /// <typeparam name="TActionResult">Type of the action result.</typeparam>
        /// <typeparam name="TModel">Expected model type.</typeparam>
        /// <param name="shouldReturnTestBuilder">Instance of <see cref="IShouldReturnTestBuilder{TActionResult}"/> type.</param>
        /// <param name="statusCode">Status code as integer.</param>
        /// <param name="model">Expected deeply equal model object.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        public static IAndTestBuilder StatusCode<TActionResult, TModel>(
            this IShouldReturnTestBuilder<TActionResult> shouldReturnTestBuilder,
            int statusCode,
            TModel model)
            => shouldReturnTestBuilder
                .StatusCode(result => result
                    .WithStatusCode(statusCode)
                    .WithModel(model));
    }
}
