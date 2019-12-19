namespace MyTested.AspNetCore.Mvc
{
    using System;
    using Builders.Contracts.And;
    using Builders.Contracts.Invocations;
    using Builders.Contracts.Models;
    using Builders.And;
    using Builders.Base;
    using Builders.Models;
    using Utilities.Validators;

    /// <summary>
    /// Contains model extension methods for <see cref="IBaseShouldReturnTestBuilder"/>.
    /// </summary>
    public static class BaseShouldReturnTestBuilderExtensions
    {
        /// <summary>
        /// Tests whether the result is of the provided type.
        /// </summary>
        /// <param name="builder">Instance of <see cref="IBaseShouldReturnTestBuilder{TInvocationResult}"/> type.</param>
        /// <param name="resultType">Expected return type.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        public static IAndTestBuilder ResultOfType<TInvocationResult>(
            this IBaseShouldReturnTestBuilder<TInvocationResult> builder,
            Type resultType)
            => builder.ResultOfType(resultType, null);

        /// <summary>
        /// Tests whether the result is of the provided type.
        /// </summary>
        /// <param name="builder">Instance of <see cref="IBaseShouldReturnTestBuilder{TInvocationResult}"/> type.</param>
        /// <param name="resultTestBuilder">Builder for testing the result.</param>
        /// <param name="resultType">Expected return type.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        public static IAndTestBuilder ResultOfType<TInvocationResult>(
            this IBaseShouldReturnTestBuilder<TInvocationResult> builder,
            Type resultType,
            Action<IModelDetailsTestBuilder<TInvocationResult>> resultTestBuilder)
        {
            var actualBuilder = (BaseTestBuilderWithActionContext)builder;

            InvocationResultValidator.ValidateInvocationResultType(
                actualBuilder.TestContext,
                resultType,
                canBeAssignable: true,
                allowDifferentGenericTypeDefinitions: true);

            resultTestBuilder?.Invoke(new ModelDetailsTestBuilder<TInvocationResult>(actualBuilder.TestContext));

            return new AndTestBuilder(actualBuilder.TestContext);
        }

        /// <summary>
        /// Tests whether the result is of the provided type.
        /// </summary>
        /// <param name="builder">Instance of <see cref="IBaseShouldReturnTestBuilder"/> type.</param>
        /// <typeparam name="TResult">Expected response type.</typeparam>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        public static IAndTestBuilder ResultOfType<TResult>(
            this IBaseShouldReturnTestBuilder builder)
            => builder.ResultOfType<TResult>(null);

        /// <summary>
        /// Tests whether the result is of the provided type.
        /// </summary>
        /// <param name="builder">Instance of <see cref="IBaseShouldReturnTestBuilder"/> type.</param>
        /// <typeparam name="TResult">Expected response type.</typeparam>
        /// <param name="resultTestBuilder">Builder for testing the result.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        public static IAndTestBuilder ResultOfType<TResult>(
            this IBaseShouldReturnTestBuilder builder,
            Action<IModelDetailsTestBuilder<TResult>> resultTestBuilder)
        {
            var actualBuilder = (BaseTestBuilderWithActionContext)builder;

            InvocationResultValidator.ValidateInvocationResultType<TResult>(
                actualBuilder.TestContext,
                canBeAssignable: true);

            resultTestBuilder?.Invoke(new ModelDetailsTestBuilder<TResult>(actualBuilder.TestContext));

            return new AndTestBuilder(actualBuilder.TestContext);
        }

        /// <summary>
        /// Tests whether the result is deeply equal to the provided one.
        /// </summary>
        /// <typeparam name="TResult">Expected response type.</typeparam>
        /// <param name="builder">Instance of <see cref="IBaseShouldReturnTestBuilder"/> type.</param>
        /// <param name="model">Expected return object.</param>
        /// <returns>Test builder of <see cref="IModelDetailsTestBuilder{TResult}"/> type.</returns>
        public static IAndModelDetailsTestBuilder<TResult> Result<TResult>(
            this IBaseShouldReturnTestBuilder builder,
            TResult model)
        {
            var actualBuilder = (BaseTestBuilderWithActionContext)builder;

            InvocationResultValidator.ValidateInvocationResult(actualBuilder.TestContext, model);

            return new ModelDetailsTestBuilder<TResult>(actualBuilder.TestContext);
        }
    }
}
