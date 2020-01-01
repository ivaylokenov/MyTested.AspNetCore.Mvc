namespace MyTested.AspNetCore.Mvc
{
    using System;
    using Builders.Contracts.And;
    using Builders.Contracts.Invocations;
    using Builders.And;
    using Builders.Base;
    using Builders.Contracts.Results;
    using Builders.Results;
    using Internal.TestContexts;
    using Microsoft.AspNetCore.Mvc;
    using Utilities;
    using Utilities.Validators;

    /// <summary>
    /// Contains model extension methods for <see cref="IBaseShouldReturnTestBuilder"/>.
    /// </summary>
    public static class BaseShouldReturnTestBuilderExtensions
    {
        private static readonly Type ActionResultGenericType = typeof(ActionResult<>);
        private static readonly Type ObjectResultType = typeof(ObjectResult);

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
                ConvertMethodResult(actualBuilder.TestContext),
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
                ConvertMethodResult(actualBuilder.TestContext),
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

            var convertedTestContext = ConvertMethodResult(actualBuilder.TestContext);

            resultTestBuilder?.Invoke(new ResultDetailsTestBuilder(convertedTestContext));

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
            var methodReturnType = testContext.Method.ReturnType;

            if (Reflection.AreAssignableByGeneric(ActionResultGenericType, methodReturnType))
            {
                var methodResultType = testContext.MethodResult.GetType();

                if (Reflection.AreSameTypes(ObjectResultType, methodResultType))
                {
                    var objectResult = testContext.MethodResult as ObjectResult;

                    testContext.MethodResult = objectResult?.Value;
                }
            }

            return testContext;
        }
    }
}
