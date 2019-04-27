namespace MyTested.AspNetCore.Mvc
{
    using System;
    using Builders.ActionResults.Json;
    using Builders.Actions.ShouldReturn;
    using Builders.Contracts.ActionResults.Json;
    using Builders.Contracts.Actions;
    using Builders.Contracts.And;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Contains <see cref="JsonResult"/> extension methods for <see cref="IShouldReturnTestBuilder{TActionResult}"/>.
    /// </summary>
    public static class ShouldReturnTestBuilderJsonResultExtensions
    {
        /// <summary>
        /// Tests whether the action result is <see cref="JsonResult"/>.
        /// </summary>
        /// <typeparam name="TActionResult">Type of action result type.</typeparam>
        /// <param name="builder">Instance of <see cref="IShouldReturnTestBuilder{TActionResult}"/> type.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        public static IAndTestBuilder Json<TActionResult>(
            this IShouldReturnTestBuilder<TActionResult> builder)
            => builder.Json(null);

        /// <summary>
        /// Tests whether the action result is <see cref="JsonResult"/>.
        /// </summary>
        /// <typeparam name="TActionResult">Type of action result type.</typeparam>
        /// <param name="builder">Instance of <see cref="IShouldReturnTestBuilder{TActionResult}"/> type.</param>
        /// <param name="jsonTestBuilder">Builder for testing the JSON result.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        public static IAndTestBuilder Json<TActionResult>(
            this IShouldReturnTestBuilder<TActionResult> builder,
            Action<IJsonTestBuilder> jsonTestBuilder)
        {
            var actualBuilder = (ShouldReturnTestBuilder<TActionResult>)builder;
            
            return actualBuilder.ValidateActionResult<JsonResult, IJsonTestBuilder>(
                jsonTestBuilder,
                new JsonTestBuilder(actualBuilder.TestContext));
        }
    }
}
