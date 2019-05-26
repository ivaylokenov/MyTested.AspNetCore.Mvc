namespace MyTested.AspNetCore.Mvc
{
    using System;
    using Builders.Contracts.ActionResults.Base;
    using Builders.Contracts.Base;
    using Builders.Contracts.ErrorMessages;
    using Builders.ErrorMessages;
    using Internal.Contracts.ActionResults;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Contains extension methods for <see cref="IBaseTestBuilderWithErrorResult{TErrorResultTestBuilder}"/>.
    /// </summary>
    public static class BaseTestBuilderWithErrorResultExtensions
    {
        /// <summary>
        /// Tests whether no specific error is returned
        /// from the error <see cref="ActionResult"/>.
        /// </summary>
        /// <param name="baseTestBuilderWithErrorResult">
        /// Instance of <see cref="IBaseTestBuilderWithErrorResult{TErrorResultTestBuilder}"/> type.
        /// </param>
        /// <returns>The same error <see cref="ActionResult"/> test builder.</returns>
        public static TErrorResultTestBuilder WithNoError<TErrorResultTestBuilder>(
            this IBaseTestBuilderWithErrorResult<TErrorResultTestBuilder> baseTestBuilderWithErrorResult)
            where TErrorResultTestBuilder : IBaseTestBuilderWithActionResult
        {
            var actualBuilder = GetActualBuilder(baseTestBuilderWithErrorResult);

            var actualResult = actualBuilder.TestContext.MethodResult as ObjectResult;

            if (actualResult == null)
            {
                actualBuilder.ThrowNewFailedValidationException(
                    "to not have",
                    "error message",
                    "in fact such was found");
            }

            return actualBuilder.ResultTestBuilder;
        }

        /// <summary>
        /// Tests whether the <see cref="ActionResult"/> has
        /// specific text error message using test builder.
        /// </summary>
        /// <param name="baseTestBuilderWithErrorResult">
        /// Instance of <see cref="IBaseTestBuilderWithErrorResult{TErrorResultTestBuilder}"/> type.
        /// </param>
        /// <returns><see cref="IErrorMessageTestBuilder{TTestBuilder}"/> testing the error message details.</returns>
        public static IErrorMessageTestBuilder<TErrorResultTestBuilder> WithErrorMessage<TErrorResultTestBuilder>(
            this IBaseTestBuilderWithErrorResult<TErrorResultTestBuilder> baseTestBuilderWithErrorResult)
            where TErrorResultTestBuilder : IBaseTestBuilderWithActionResult
        {
            var actualBuilder = GetActualBuilder(baseTestBuilderWithErrorResult);

            var actualErrorMessage = actualBuilder.GetErrorMessage();

            return new ErrorMessageTestBuilder<TErrorResultTestBuilder>(
                actualBuilder.TestContext,
                actualErrorMessage,
                actualBuilder.ResultTestBuilder);
        }

        /// <summary>
        /// Tests whether the <see cref="ActionResult"/> has
        /// specific text error message provided as string.
        /// </summary>
        /// <param name="baseTestBuilderWithErrorResult">
        /// Instance of <see cref="IBaseTestBuilderWithErrorResult{TErrorResultTestBuilder}"/> type.
        /// </param>
        /// <param name="error">Expected error message.</param>
        /// <returns>The same error <see cref="ActionResult"/> test builder.</returns>
        public static TErrorResultTestBuilder WithErrorMessage<TErrorResultTestBuilder>(
            this IBaseTestBuilderWithErrorResult<TErrorResultTestBuilder> baseTestBuilderWithErrorResult,
            string error)
            where TErrorResultTestBuilder : IBaseTestBuilderWithActionResult
        {
            var actualBuilder = GetActualBuilder(baseTestBuilderWithErrorResult);

            var actualErrorMessage = actualBuilder.GetErrorMessage();
            actualBuilder.ValidateErrorMessage(error, actualErrorMessage);

            return actualBuilder.ResultTestBuilder;
        }

        /// <summary>
        /// Tests whether the <see cref="ActionResult"/> has
        /// error message passing the given assertions.
        /// </summary>
        /// <param name="baseTestBuilderWithErrorResult">
        /// Instance of <see cref="IBaseTestBuilderWithErrorResult{TErrorResultTestBuilder}"/> type.
        /// </param>
        /// <param name="assertions">
        /// Action containing all assertions for the error message.
        /// </param>
        /// <returns>The same error <see cref="ActionResult"/> test builder.</returns>
        public static TErrorResultTestBuilder WithErrorMessage<TErrorResultTestBuilder>(
            this IBaseTestBuilderWithErrorResult<TErrorResultTestBuilder> baseTestBuilderWithErrorResult,
            Action<string> assertions)
            where TErrorResultTestBuilder : IBaseTestBuilderWithActionResult
        {
            var actualBuilder = GetActualBuilder(baseTestBuilderWithErrorResult);

            var actualErrorMessage = actualBuilder.GetErrorMessage();
            assertions(actualErrorMessage);

            return actualBuilder.ResultTestBuilder;
        }

        /// <summary>
        /// Tests whether the <see cref="ActionResult"/> has
        /// error message passing the given predicate.
        /// </summary>
        /// <param name="baseTestBuilderWithErrorResult">
        /// Instance of <see cref="IBaseTestBuilderWithErrorResult{TErrorResultTestBuilder}"/> type.
        /// </param>
        /// <param name="predicate">Predicate testing the error message.</param>
        /// <returns>The same error <see cref="ActionResult"/> test builder.</returns>
        public static TErrorResultTestBuilder WithErrorMessage<TErrorResultTestBuilder>(
            this IBaseTestBuilderWithErrorResult<TErrorResultTestBuilder> baseTestBuilderWithErrorResult,
            Func<string, bool> predicate)
            where TErrorResultTestBuilder : IBaseTestBuilderWithActionResult
        {
            var actualBuilder = GetActualBuilder(baseTestBuilderWithErrorResult);

            var actualErrorMessage = actualBuilder.GetErrorMessage();
            if (!predicate(actualErrorMessage))
            {
                actualBuilder.ThrowNewFailedValidationException(
                    $"error message ('{actualErrorMessage}')",
                    "to pass the given predicate",
                    "it failed");
            }

            return actualBuilder.ResultTestBuilder;
        }

        private static IBaseTestBuilderWithErrorResultInternal<TErrorResultTestBuilder>
            GetActualBuilder<TErrorResultTestBuilder>(
                IBaseTestBuilderWithErrorResult<TErrorResultTestBuilder> baseTestBuilderWithErrorResult)
            where TErrorResultTestBuilder : IBaseTestBuilderWithActionResult
            => (IBaseTestBuilderWithErrorResultInternal<TErrorResultTestBuilder>)baseTestBuilderWithErrorResult;
    }
}
