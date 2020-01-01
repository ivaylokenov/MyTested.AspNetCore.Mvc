namespace MyTested.AspNetCore.Mvc
{
    using System;
    using Builders.Contracts.And;
    using Builders.Contracts.Invocations;
    using Builders.Contracts.Models;
    using Builders.And;
    using Builders.Base;
    using Builders.Contracts.Results;
    using Builders.Models;
    using Builders.Results;
    using Exceptions;
    using Internal.TestContexts;
    using Microsoft.AspNetCore.Mvc;
    using Utilities.Validators;

    /// <summary>
    /// Contains model extension methods for <see cref="IBaseShouldReturnTestBuilder"/>.
    /// </summary>
    public static class BaseShouldReturnTestBuilderExtensions
    {
        /// <summary>
        /// Tests whether the result is of the provided type.
        /// </summary>
        /// <typeparam name="TResult">Expected result type.</typeparam>
        /// <param name="builder">Instance of <see cref="IBaseShouldReturnTestBuilder{TInvocationResult}"/> type.</param>
        /// <param name="resultType">Expected result type.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        public static IAndTestBuilder ResultOfType<TResult>(
            this IBaseShouldReturnTestBuilder<TResult> builder,
            Type resultType)
            => builder.ResultOfType(resultType, null);

        /// <summary>
        /// Tests whether the result is of the provided type.
        /// </summary>
        /// <typeparam name="TResult">Expected result type.</typeparam>
        /// <param name="builder">Instance of <see cref="IBaseShouldReturnTestBuilder{TInvocationResult}"/> type.</param>
        /// <param name="resultTestBuilder">Builder for testing the result.</param>
        /// <param name="resultType">Expected result type.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        public static IAndTestBuilder ResultOfType<TResult>(
            this IBaseShouldReturnTestBuilder<TResult> builder,
            Type resultType,
            Action<IResultDetailsTestBuilder<TResult>> resultTestBuilder)
        {
            var actualBuilder = (BaseTestBuilderWithActionContext)builder;

            InvocationResultValidator.ValidateInvocationResultType(
                actualBuilder.TestContext,
                resultType,
                canBeAssignable: true,
                allowDifferentGenericTypeDefinitions: true);

            resultTestBuilder?.Invoke(new ResultDetailsTestBuilder<TResult>(actualBuilder.TestContext));

            return new AndTestBuilder(actualBuilder.TestContext);
        }

        /// <summary>
        /// Tests whether the result is of the provided type.
        /// </summary>
        /// <param name="builder">Instance of <see cref="IBaseShouldReturnTestBuilder"/> type.</param>
        /// <typeparam name="TResult">Expected result type.</typeparam>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        public static IAndTestBuilder ResultOfType<TResult>(
            this IBaseShouldReturnTestBuilder builder)
            => builder.ResultOfType<TResult>(null);

        /// <summary>
        /// Tests whether the result is of the provided type.
        /// </summary>
        /// <param name="builder">Instance of <see cref="IBaseShouldReturnTestBuilder"/> type.</param>
        /// <typeparam name="TResult">Expected result type.</typeparam>
        /// <param name="resultTestBuilder">Builder for testing the result.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        public static IAndTestBuilder ResultOfType<TResult>(
            this IBaseShouldReturnTestBuilder builder,
            Action<IResultDetailsTestBuilder<TResult>> resultTestBuilder)
        {
            var actualBuilder = (BaseTestBuilderWithActionContext)builder;

            InvocationResultValidator.ValidateInvocationResultType<TResult>(
                actualBuilder.TestContext,
                canBeAssignable: true);

            resultTestBuilder?.Invoke(new ResultDetailsTestBuilder<TResult>(actualBuilder.TestContext));

            return new AndTestBuilder(actualBuilder.TestContext);
        }

        /// <summary>
        /// Tests the result by using a test builder.
        /// </summary>
        /// <param name="builder">Instance of <see cref="IBaseShouldReturnTestBuilder"/> type.</param>
        /// <param name="resultTestBuilder">Builder for testing the result.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        public static IAndTestBuilder Result(
            this IBaseShouldReturnTestBuilder builder,
            Action<IResultDetailsTestBuilder> resultTestBuilder)
        {
            var actualBuilder = (BaseTestBuilderWithActionContext)builder;

            resultTestBuilder?.Invoke(new ResultDetailsTestBuilder(actualBuilder.TestContext));

            return new AndTestBuilder(actualBuilder.TestContext);
        }

        /// <summary>
        /// Tests whether the result is deeply equal to the provided one.
        /// </summary>
        /// <typeparam name="TResult">Expected result type.</typeparam>
        /// <param name="builder">Instance of <see cref="IBaseShouldReturnTestBuilder"/> type.</param>
        /// <param name="result">Expected result object.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        public static IAndTestBuilder Result<TResult>(
            this IBaseShouldReturnTestBuilder builder,
            TResult result)
            => builder
                .Result(value => value
                    .EqualTo(result));

        private static ActionTestContext ConvertMethodResult(ActionTestContext testContext)
        {
            var methodResult = testContext.MethodResult;

            if (methodResult is IActionResult)
            {
                if (methodResult is ObjectResult objectResult)
                {
                    testContext.MethodResult = objectResult.Value;
                }
                else
                {
                    throw new InvocationResultAssertionException("Test");
                }

                return testContext;
            }

            return testContext;
        }
    }
}
