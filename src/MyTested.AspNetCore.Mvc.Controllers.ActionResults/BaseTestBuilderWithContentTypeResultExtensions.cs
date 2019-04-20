namespace MyTested.AspNetCore.Mvc
{
    using Builders.Contracts.ActionResults.Base;
    using Builders.Contracts.Base;
    using Internal.Contracts.ActionResults;
    using Microsoft.Net.Http.Headers;
    using Utilities.Validators;

    /// <summary>
    /// Contains extension methods for <see cref="IBaseTestBuilderWithContentTypeResult{TContentTypeResultTestBuilder}"/>.
    /// </summary>
    public static class BaseTestBuilderWithContentTypeResultExtensions
    {
        /// <summary>
        /// Tests whether the <see cref="Microsoft.AspNetCore.Mvc.ActionResult"/>
        /// has the same content type as the provided string.
        /// </summary>
        /// <param name="baseTestBuilderWithContentTypeResult">
        /// Instance of <see cref="IBaseTestBuilderWithContentTypeResult{TContentTypeResultTestBuilder}"/> type.
        /// </param>
        /// <param name="contentType">Content type as string.</param>
        /// <returns>The same content type <see cref="Microsoft.AspNetCore.Mvc.ActionResult"/> test builder.</returns>
        public static TContentTypeResultTestBuilder WithContentType<TContentTypeResultTestBuilder>(
            this IBaseTestBuilderWithContentTypeResult<TContentTypeResultTestBuilder> baseTestBuilderWithContentTypeResult,
            string contentType)
            where TContentTypeResultTestBuilder : IBaseTestBuilderWithActionResult
        {
            var actualBuilder = GetActualBuilder(baseTestBuilderWithContentTypeResult);

            ContentTypeValidator.ValidateContentType(
                actualBuilder.TestContext.MethodResult,
                contentType,
                actualBuilder.ThrowNewFailedValidationException);

            return actualBuilder.ResultTestBuilder;
        }

        /// <summary>
        /// Tests whether the <see cref="Microsoft.AspNetCore.Mvc.ActionResult"/> has the
        /// same content type as the provided <see cref="MediaTypeHeaderValue"/>.
        /// </summary>
        /// <param name="baseTestBuilderWithContentTypeResult">
        /// Instance of <see cref="IBaseTestBuilderWithContentTypeResult{TContentTypeResultTestBuilder}"/> type.
        /// </param>
        /// <param name="contentType">Content type as <see cref="MediaTypeHeaderValue"/>.</param>
        /// <returns>The same <see cref="Microsoft.AspNetCore.Mvc.ActionResult"/> test builder.</returns>
        public static TContentTypeResultTestBuilder WithContentType<TContentTypeResultTestBuilder>(
            this IBaseTestBuilderWithContentTypeResult<TContentTypeResultTestBuilder> baseTestBuilderWithContentTypeResult,
            MediaTypeHeaderValue contentType)
            where TContentTypeResultTestBuilder : IBaseTestBuilderWithActionResult
            => baseTestBuilderWithContentTypeResult
                .WithContentType(contentType?.MediaType.Value);

        private static IBaseTestBuilderWithContentTypeResultInternal<TContentTypeResultTestBuilder>
            GetActualBuilder<TContentTypeResultTestBuilder>(
                IBaseTestBuilderWithContentTypeResult<TContentTypeResultTestBuilder> baseTestBuilderWithContentTypeResult)
            where TContentTypeResultTestBuilder : IBaseTestBuilderWithActionResult
            => (IBaseTestBuilderWithContentTypeResultInternal<TContentTypeResultTestBuilder>)baseTestBuilderWithContentTypeResult;
    }
}
