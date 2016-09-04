namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Invocations
{
    using System;
    using Models;

    public interface IBaseShouldReturnTestBuilder<TInvocationResult, TBuilder>
    {
        /// <summary>
        /// Tests whether the result is the default value of the type.
        /// </summary>
        /// <returns>Base test builder.</returns>
        TBuilder DefaultValue();

        /// <summary>
        /// Tests whether the result is null.
        /// </summary>
        /// <returns>Base test builder.</returns>
        TBuilder Null();

        /// <summary>
        /// Tests whether the result is not null.
        /// </summary>
        /// <returns>Base test builder.</returns>
        TBuilder NotNull();

        /// <summary>
        /// Tests whether the result is of the provided type.
        /// </summary>
        /// <typeparam name="TResult">Expected response type.</typeparam>
        /// <returns>Test builder of <see cref="IModelDetailsTestBuilder{TResult}"/> type.</returns>
        IAndModelDetailsTestBuilder<TResult> ResultOfType<TResult>();

        /// <summary>
        /// Tests whether the result is of the provided type.
        /// </summary>
        /// <param name="returnType">Expected return type.</param>
        /// <returns>Test builder of <see cref="IModelDetailsTestBuilder{TInvocationResult}"/> type.</returns>
        IAndModelDetailsTestBuilder<TInvocationResult> ResultOfType(Type returnType);

        /// <summary>
        /// Tests whether the result is deeply equal to the provided one.
        /// </summary>
        /// <typeparam name="TResult">Expected response type.</typeparam>
        /// <param name="model">Expected return object.</param>
        /// <returns>Test builder of <see cref="IModelDetailsTestBuilder{TResult}"/> type.</returns>
        IAndModelDetailsTestBuilder<TResult> Result<TResult>(TResult model);
    }
}
