namespace MyTested.AspNetCore.Mvc
{
    using Builders.Actions.ShouldReturn;
    using Builders.Contracts.ActionResults.StatusCode;
    using Builders.Contracts.Actions;
    using Utilities.Validators;

    using SystemHttpStatusCode = System.Net.HttpStatusCode;

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
        /// <typeparam name="TActionResult">Type of action result type.</typeparam>
        /// <param name="builder">Instance of <see cref="IShouldReturnTestBuilder{TActionResult}"/> type.</param>
        /// <param name="statusCode">Status code as integer.</param>
        /// <returns>Test builder of <see cref="IAndStatusCodeTestBuilder"/> type.</returns>
        public static IAndStatusCodeTestBuilder StatusCode<TActionResult>(
            this IShouldReturnTestBuilder<TActionResult> builder,
            int statusCode)
            => builder
                .StatusCode((SystemHttpStatusCode)statusCode);

        /// <summary>
        /// Tests whether the action result is <see cref="Microsoft.AspNetCore.Mvc.StatusCodeResult"/>
        /// or <see cref="Microsoft.AspNetCore.Mvc.ObjectResult"/> and it
        /// has the same status code as the provided <see cref="SystemHttpStatusCode"/>.
        /// </summary>
        /// <typeparam name="TActionResult">Type of action result type.</typeparam>
        /// <param name="builder">Instance of <see cref="IShouldReturnTestBuilder{TActionResult}"/> type.</param>
        /// <param name="statusCode"><see cref="HttpStatusCode"/> enumeration.</param>
        /// <returns>Test builder of <see cref="IAndStatusCodeTestBuilder"/> type.</returns>
        public static IAndStatusCodeTestBuilder StatusCode<TActionResult>(
            this IShouldReturnTestBuilder<TActionResult> builder,
            SystemHttpStatusCode statusCode)
        {
            var actualBuilder = (ShouldReturnTestBuilder<TActionResult>)builder;

            HttpStatusCodeValidator.ValidateHttpStatusCode(
                actualBuilder.ActionResult,
                statusCode,
                actualBuilder.ThrowNewStatusCodeResultAssertionException);

            return actualBuilder.GetStatusCodeTestBuilder();
        }
    }
}
