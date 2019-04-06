namespace MyTested.AspNetCore.Mvc
{
    using System.Collections.Generic;
    using System.Linq;
    using Builders.Contracts.ActionResults.Base;
    using Builders.Contracts.Base;
    using Internal.Contracts.ActionResults;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Formatters;
    using Microsoft.Net.Http.Headers;
    using Utilities.Validators;

    /// <summary>
    /// Contains extension methods for <see cref="IBaseTestBuilderWithOutputResult{TOutputResultTestBuilder}"/>.
    /// </summary>
    public static class BaseTestBuilderWithOutputResultExtensions
    {
        /// <summary>
        /// Tests whether the <see cref="ActionResult"/>
        /// contains the content type provided as string.
        /// </summary>
        /// <param name="baseTestBuilderWithOutputResult">
        /// Instance of <see cref="IBaseTestBuilderWithOutputResult{TOutputResultTestBuilder}"/> type.
        /// </param>
        /// <param name="contentType">Content type as string.</param>
        /// <returns>The same output <see cref="ActionResult"/> test builder.</returns>
        public static TOutputResultTestBuilder ContainingContentType<TOutputResultTestBuilder>(
            this IBaseTestBuilderWithOutputResult<TOutputResultTestBuilder> baseTestBuilderWithOutputResult,
            string contentType)
            where TOutputResultTestBuilder : IBaseTestBuilderWithActionResult
            => baseTestBuilderWithOutputResult.ContainingContentType(new MediaTypeHeaderValue(contentType));

        /// <summary>
        /// Tests whether the <see cref="ActionResult"/>
        /// contains the content type provided as <see cref="MediaTypeHeaderValue"/>.
        /// </summary>
        /// <param name="baseTestBuilderWithOutputResult">
        /// Instance of <see cref="IBaseTestBuilderWithOutputResult{TOutputResultTestBuilder}"/> type.
        /// </param>
        /// <param name="contentType">Content type as <see cref="MediaTypeHeaderValue"/>.</param>
        /// <returns>The same output <see cref="ActionResult"/> test builder.</returns>
        public static TOutputResultTestBuilder ContainingContentType<TOutputResultTestBuilder>(
            this IBaseTestBuilderWithOutputResult<TOutputResultTestBuilder> baseTestBuilderWithOutputResult,
            MediaTypeHeaderValue contentType)
            where TOutputResultTestBuilder : IBaseTestBuilderWithActionResult
        {
            var actualBuilder = (IBaseTestBuilderWithOutputResultInternal<TOutputResultTestBuilder>)baseTestBuilderWithOutputResult;

            ValidateObjectResult(actualBuilder);

            ContentTypeValidator.ValidateContainingOfContentType(
                actualBuilder.TestContext.MethodResult,
                contentType,
                actualBuilder.ThrowNewFailedValidationException);

            return actualBuilder.ResultTestBuilder;
        }

        /// <summary>
        /// Tests whether the <see cref="ActionResult"/>
        /// contains the same content types provided as collection of strings.
        /// </summary>
        /// <param name="baseTestBuilderWithOutputResult">
        /// Instance of <see cref="IBaseTestBuilderWithOutputResult{TOutputResultTestBuilder}"/> type.
        /// </param>
        /// <param name="contentTypes">Content types as collection of strings.</param>
        /// <returns>The same output <see cref="ActionResult"/> test builder.</returns>
        public static TOutputResultTestBuilder ContainingContentTypes<TOutputResultTestBuilder>(
            this IBaseTestBuilderWithOutputResult<TOutputResultTestBuilder> baseTestBuilderWithOutputResult,
            IEnumerable<string> contentTypes)
            where TOutputResultTestBuilder : IBaseTestBuilderWithActionResult
            => baseTestBuilderWithOutputResult
                .ContainingContentTypes(contentTypes.Select(ct => new MediaTypeHeaderValue(ct)));

        /// <summary>
        /// Tests whether the <see cref="ActionResult"/>
        /// contains the same content types provided as string parameters.
        /// </summary>
        /// <param name="baseTestBuilderWithOutputResult">
        /// Instance of <see cref="IBaseTestBuilderWithOutputResult{TOutputResultTestBuilder}"/> type.
        /// </param>
        /// <param name="contentTypes">Content types as string parameters.</param>
        /// <returns>The same output <see cref="ActionResult"/> test builder.</returns>
        public static TOutputResultTestBuilder ContainingContentTypes<TOutputResultTestBuilder>(
            this IBaseTestBuilderWithOutputResult<TOutputResultTestBuilder> baseTestBuilderWithOutputResult,
            params string[] contentTypes)
            where TOutputResultTestBuilder : IBaseTestBuilderWithActionResult
            => baseTestBuilderWithOutputResult
                .ContainingContentTypes(contentTypes.AsEnumerable());

        /// <summary>
        /// Tests whether the <see cref="ActionResult"/>
        /// contains the same content types provided as collection of <see cref="MediaTypeHeaderValue"/>.
        /// </summary>
        /// <param name="baseTestBuilderWithOutputResult">
        /// Instance of <see cref="IBaseTestBuilderWithOutputResult{TOutputResultTestBuilder}"/> type.
        /// </param>
        /// <param name="contentTypes">Content types as collection of <see cref="MediaTypeHeaderValue"/>.</param>
        /// <returns>The same output <see cref="ActionResult"/> test builder.</returns>
        public static TOutputResultTestBuilder ContainingContentTypes<TOutputResultTestBuilder>(
            this IBaseTestBuilderWithOutputResult<TOutputResultTestBuilder> baseTestBuilderWithOutputResult,
            IEnumerable<MediaTypeHeaderValue> contentTypes)
            where TOutputResultTestBuilder : IBaseTestBuilderWithActionResult
        {
            var actualBuilder = (IBaseTestBuilderWithOutputResultInternal<TOutputResultTestBuilder>)baseTestBuilderWithOutputResult;

            ValidateObjectResult(actualBuilder);

            ContentTypeValidator.ValidateContentTypes(
                actualBuilder.TestContext.MethodResult,
                contentTypes,
                actualBuilder.ThrowNewFailedValidationException);

            return actualBuilder.ResultTestBuilder;
        }

        /// <summary>
        /// Tests whether the <see cref="ActionResult"/>
        /// contains the same content types provided as <see cref="MediaTypeHeaderValue"/> parameters.
        /// </summary>
        /// <param name="baseTestBuilderWithOutputResult">
        /// Instance of <see cref="IBaseTestBuilderWithOutputResult{TOutputResultTestBuilder}"/> type.
        /// </param>
        /// <param name="contentTypes">Content types as <see cref="MediaTypeHeaderValue"/> parameters.</param>
        /// <returns>The same output <see cref="ActionResult"/> test builder.</returns>
        public static TOutputResultTestBuilder ContainingContentTypes<TOutputResultTestBuilder>(
            this IBaseTestBuilderWithOutputResult<TOutputResultTestBuilder> baseTestBuilderWithOutputResult,
            params MediaTypeHeaderValue[] contentTypes)
            where TOutputResultTestBuilder : IBaseTestBuilderWithActionResult
            => baseTestBuilderWithOutputResult
                .ContainingContentTypes(contentTypes.AsEnumerable());

        /// <summary>
        /// Tests whether the <see cref="ActionResult"/>
        /// contains the provided <see cref="IOutputFormatter"/>.
        /// </summary>
        /// <param name="baseTestBuilderWithOutputResult">
        /// Instance of <see cref="IBaseTestBuilderWithOutputResult{TOutputResultTestBuilder}"/> type.
        /// </param>
        /// <param name="outputFormatter">Instance of <see cref="IOutputFormatter"/>.</param>
        /// <returns>The same output <see cref="ActionResult"/> test builder.</returns>
        public static TOutputResultTestBuilder ContainingOutputFormatter<TOutputResultTestBuilder>(
            this IBaseTestBuilderWithOutputResult<TOutputResultTestBuilder> baseTestBuilderWithOutputResult,
            IOutputFormatter outputFormatter)
            where TOutputResultTestBuilder : IBaseTestBuilderWithActionResult
        {
            var actualBuilder = (IBaseTestBuilderWithOutputResultInternal<TOutputResultTestBuilder>)baseTestBuilderWithOutputResult;

            ValidateObjectResult(actualBuilder);

            OutputFormatterValidator.ValidateContainingOfOutputFormatter(
                actualBuilder.GetObjectResult(),
                outputFormatter,
                actualBuilder.ThrowNewFailedValidationException);

            return actualBuilder.ResultTestBuilder;
        }

        /// <summary>
        /// Tests whether the <see cref="ActionResult"/>
        /// contains <see cref="IOutputFormatter"/> of the provided type.
        /// </summary>
        /// <param name="baseTestBuilderWithOutputResult">
        /// Instance of <see cref="IBaseTestBuilderWithOutputResult{TOutputResultTestBuilder}"/> type.
        /// </param>
        /// <returns>The same output <see cref="ActionResult"/> test builder.</returns>
        public static TOutputResultTestBuilder ContainingOutputFormatterOfType<
            TOutputResultTestBuilder, TOutputFormatter>(
            this IBaseTestBuilderWithOutputResult<TOutputResultTestBuilder> baseTestBuilderWithOutputResult)
            where TOutputResultTestBuilder : IBaseTestBuilderWithActionResult
            where TOutputFormatter : IOutputFormatter
        {
            var actualBuilder = (IBaseTestBuilderWithOutputResultInternal<TOutputResultTestBuilder>)baseTestBuilderWithOutputResult;

            ValidateObjectResult(actualBuilder);

            OutputFormatterValidator.ValidateContainingOutputFormatterOfType<TOutputFormatter>(
                actualBuilder.GetObjectResult(),
                actualBuilder.ThrowNewFailedValidationException);

            return actualBuilder.ResultTestBuilder;
        }

        /// <summary>
        /// Tests whether the <see cref="ActionResult"/>
        /// contains the provided output formatters.
        /// </summary>
        /// <param name="baseTestBuilderWithOutputResult">
        /// Instance of <see cref="IBaseTestBuilderWithOutputResult{TOutputResultTestBuilder}"/> type.
        /// </param>
        /// <param name="outputFormatters">Collection of <see cref="IOutputFormatter"/>.</param>
        /// <returns>The same output <see cref="ActionResult"/> test builder.</returns>
        public static TOutputResultTestBuilder ContainingOutputFormatters<TOutputResultTestBuilder>(
            this IBaseTestBuilderWithOutputResult<TOutputResultTestBuilder> baseTestBuilderWithOutputResult,
            IEnumerable<IOutputFormatter> outputFormatters)
            where TOutputResultTestBuilder : IBaseTestBuilderWithActionResult
        {
            var actualBuilder = (IBaseTestBuilderWithOutputResultInternal<TOutputResultTestBuilder>)baseTestBuilderWithOutputResult;

            ValidateObjectResult(actualBuilder);

            OutputFormatterValidator.ValidateOutputFormatters(
                actualBuilder.GetObjectResult(),
                outputFormatters,
                actualBuilder.ThrowNewFailedValidationException);

            return actualBuilder.ResultTestBuilder;
        }

        /// <summary>
        /// Tests whether the <see cref="ActionResult"/>
        /// contains the provided output formatters.
        /// </summary>
        /// <param name="baseTestBuilderWithOutputResult">
        /// Instance of <see cref="IBaseTestBuilderWithOutputResult{TOutputResultTestBuilder}"/> type.
        /// </param>
        /// <param name="outputFormatters"><see cref="IOutputFormatter"/> parameters.</param>
        /// <returns>The same output <see cref="ActionResult"/> test builder.</returns>
        public static TOutputResultTestBuilder ContainingOutputFormatters<TOutputResultTestBuilder>(
            this IBaseTestBuilderWithOutputResult<TOutputResultTestBuilder> baseTestBuilderWithOutputResult,
            params IOutputFormatter[] outputFormatters)
            where TOutputResultTestBuilder : IBaseTestBuilderWithActionResult
            => baseTestBuilderWithOutputResult
                .ContainingOutputFormatters(outputFormatters.AsEnumerable());

        private static void ValidateObjectResult<TOutputResultTestBuilder>(
            IBaseTestBuilderWithOutputResultInternal<TOutputResultTestBuilder> baseTestBuilderWithComponent)
            where TOutputResultTestBuilder : IBaseTestBuilderWithActionResult
        {
            if (!(baseTestBuilderWithComponent.TestContext.MethodResult is ObjectResult))
            {
                baseTestBuilderWithComponent.ThrowNewFailedValidationException("to inherit", nameof(ObjectResult), "in fact it did not");
            }
        }
    }
}
