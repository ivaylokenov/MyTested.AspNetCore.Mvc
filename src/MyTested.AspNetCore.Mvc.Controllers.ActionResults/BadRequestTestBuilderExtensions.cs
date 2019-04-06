namespace MyTested.AspNetCore.Mvc
{
    using System;
    using Builders.ActionResults.BadRequest;
    using Builders.Base;
    using Builders.Contracts.ActionResults.BadRequest;
    using Exceptions;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Contains extension methods for <see cref="IBadRequestTestBuilder"/>.
    /// </summary>
    public static class BadRequestTestBuilderExtensions
    {
        /// <summary>
        /// Tests whether no specific error is returned
        /// from the <see cref="BadRequestObjectResult"/>.
        /// </summary>
        /// <param name="badRequestTestBuilder">
        /// Instance of <see cref="IBadRequestTestBuilder"/> type.
        /// </param>
        /// <returns>The same <see cref="IAndBadRequestTestBuilder"/>.</returns>
        public static IAndBadRequestTestBuilder WithNoError(this IBadRequestTestBuilder badRequestTestBuilder)
        {
            var actualBuilder = (BaseTestBuilderWithComponent)badRequestTestBuilder;
            
            var actualResult = actualBuilder.TestContext.MethodResult as BadRequestResult;
            if (actualResult == null)
            {
                throw new ResponseModelAssertionException(string.Format(
                    "{0} bad request result to not have error message, but in fact such was found.",
                    actualBuilder.TestContext.ExceptionMessagePrefix));
            }

            return (IAndBadRequestTestBuilder)badRequestTestBuilder;
        }

        /// <summary>
        /// Tests <see cref="BadRequestObjectResult"/> with
        /// specific text error message using test builder.
        /// </summary>
        /// <param name="badRequestTestBuilder">
        /// Instance of <see cref="IBadRequestTestBuilder"/> type.
        /// </param>
        /// <returns><see cref="IBadRequestErrorMessageTestBuilder"/>.</returns>
        public static IBadRequestErrorMessageTestBuilder WithErrorMessage(this IBadRequestTestBuilder badRequestTestBuilder)
        {
            var actualBuilder = GetBadRequestTestBuilder(badRequestTestBuilder);

            var actualErrorMessage = actualBuilder.GetBadRequestErrorMessage();
            return new BadRequestErrorMessageTestBuilder(
                actualBuilder.TestContext,
                actualErrorMessage,
                actualBuilder);
        }

        /// <summary>
        /// Tests <see cref="BadRequestObjectResult"/> with
        /// specific text error message provided as string.
        /// </summary>
        /// <param name="badRequestTestBuilder">
        /// Instance of <see cref="IBadRequestTestBuilder"/> type.
        /// </param>
        /// <param name="error">Expected error message.</param>
        /// <returns>The same <see cref="IAndBadRequestTestBuilder"/>.</returns>
        public static IAndBadRequestTestBuilder WithErrorMessage(
            this IBadRequestTestBuilder badRequestTestBuilder,
            string error)
        {
            var actualBuilder = GetBadRequestTestBuilder(badRequestTestBuilder);

            var actualErrorMessage = actualBuilder.GetBadRequestErrorMessage();
            actualBuilder.ValidateErrorMessage(error, actualErrorMessage);

            return actualBuilder;
        }

        /// <summary>
        /// Tests whether the <see cref="BadRequestObjectResult"/> error message passes the given assertions.
        /// </summary>
        /// <param name="badRequestTestBuilder">
        /// Instance of <see cref="IBadRequestTestBuilder"/> type.
        /// </param>
        /// <param name="assertions">Action containing all assertions
        /// for the <see cref="BadRequestObjectResult"/> error message.</param>
        /// <returns>The same <see cref="IAndBadRequestTestBuilder"/>.</returns>
        public static IAndBadRequestTestBuilder WithErrorMessage(
            this IBadRequestTestBuilder badRequestTestBuilder,
            Action<string> assertions)
        {
            var actualBuilder = GetBadRequestTestBuilder(badRequestTestBuilder);

            var actualErrorMessage = actualBuilder.GetBadRequestErrorMessage();
            assertions(actualErrorMessage);

            return actualBuilder;
        }

        /// <summary>
        /// Tests whether the <see cref="BadRequestObjectResult"/> error message passes the given predicate.
        /// </summary>
        /// <param name="badRequestTestBuilder">
        /// Instance of <see cref="IBadRequestTestBuilder"/> type.
        /// </param>
        /// <param name="predicate">Predicate testing the <see cref="BadRequestObjectResult"/> error message.</param>
        /// <returns>The same <see cref="IAndBadRequestTestBuilder"/>.</returns>
        public static IAndBadRequestTestBuilder WithErrorMessage(
            this IBadRequestTestBuilder badRequestTestBuilder,
            Func<string, bool> predicate)
        {
            var actualBuilder = GetBadRequestTestBuilder(badRequestTestBuilder);

            var actualErrorMessage = actualBuilder.GetBadRequestErrorMessage();
            if (!predicate(actualErrorMessage))
            {
                throw new BadRequestResultAssertionException(string.Format(
                    "{0} bad request error message ('{1}') to pass the given predicate, but it failed.",
                    actualBuilder.TestContext.ExceptionMessagePrefix,
                    actualErrorMessage));
            }

            return actualBuilder;
        }

        public static BadRequestTestBuilder<BadRequestObjectResult> GetBadRequestTestBuilder(IBadRequestTestBuilder badRequestTestBuilder)
        {
            var actualBadRequestTestBuilder = badRequestTestBuilder as BadRequestTestBuilder<BadRequestObjectResult>;

            if (actualBadRequestTestBuilder == null)
            {
                var badRequestTestBuilderBase = (BaseTestBuilderWithComponent)badRequestTestBuilder;

                throw new BadRequestResultAssertionException(string.Format(
                    "{0} bad request result to contain error object, but it could not be found.",
                    badRequestTestBuilderBase.TestContext.ExceptionMessagePrefix));
            }

            return actualBadRequestTestBuilder;
        }
    }
}
