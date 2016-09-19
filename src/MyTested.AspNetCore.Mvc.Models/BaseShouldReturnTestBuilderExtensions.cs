namespace MyTested.AspNetCore.Mvc
{
    using System;
    using Builders.Contracts.Invocations;
    using Builders.Contracts.Models;
    using Builders.Base;
    using Utilities.Validators;
    using Builders.Models;

    /// <summary>
    /// Contains model extension methods for <see cref="IBaseShouldReturnTestBuilder"/>.
    /// </summary>
    public static class BaseShouldReturnTestBuilderExtensions
    {
        /// <summary>
        /// Tests whether the result is of the provided type.
        /// </summary>
        /// <param name="builder">Instance of <see cref="IBaseShouldReturnTestBuilder{TInvocationResult}"/> type.</param>
        /// <param name="returnType">Expected return type.</param>
        /// <returns>Test builder of <see cref="IModelDetailsTestBuilder{TInvocationResult}"/> type.</returns>
        public static IAndModelDetailsTestBuilder<TInvocationResult> ResultOfType<TInvocationResult>(
            this IBaseShouldReturnTestBuilder<TInvocationResult> builder,
            Type returnType)
        {
            var actualBuilder = (BaseTestBuilderWithActionContext)builder;

            InvocationResultValidator.ValidateInvocationResultType(
                actualBuilder.TestContext,
                returnType,
                canBeAssignable: true,
                allowDifferentGenericTypeDefinitions: true);

            return new ModelDetailsTestBuilder<TInvocationResult>(actualBuilder.TestContext);
        }

        /// <summary>
        /// Tests whether the result is of the provided type.
        /// </summary>
        /// <param name="builder">Instance of <see cref="IBaseShouldReturnTestBuilder"/> type.</param>
        /// <typeparam name="TResult">Expected response type.</typeparam>
        /// <returns>Test builder of <see cref="IModelDetailsTestBuilder{TResult}"/> type.</returns>
        public static IAndModelDetailsTestBuilder<TResult> ResultOfType<TResult>(
            this IBaseShouldReturnTestBuilder builder)
        {
            var actualBuilder = (BaseTestBuilderWithActionContext)builder;

            InvocationResultValidator.ValidateInvocationResultType<TResult>(
                actualBuilder.TestContext,
                canBeAssignable: true);

            return new ModelDetailsTestBuilder<TResult>(actualBuilder.TestContext);
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
