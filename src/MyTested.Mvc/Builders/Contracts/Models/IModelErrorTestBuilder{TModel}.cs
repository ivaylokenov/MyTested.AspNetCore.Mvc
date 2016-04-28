namespace MyTested.Mvc.Builders.Contracts.Models
{
    using System;
    using System.Linq.Expressions;
    using Base;

    /// <summary>
    /// Used for testing model errors.
    /// </summary>
    /// <typeparam name="TModel">Model from invoked action in ASP.NET Core MVC controller.</typeparam>
    public interface IModelErrorTestBuilder<TModel> : IModelErrorTestBuilder, IBaseTestBuilderWithModel<TModel>
    {
        /// <summary>
        /// Tests whether tested action's model state contains error by key.
        /// </summary>
        /// <param name="errorKey">Error key to search for.</param>
        /// <returns>Model error details test builder.</returns>
        IModelErrorDetailsTestBuilder<TModel> ContainingError(string errorKey);

        /// <summary>
        /// Tests whether tested action's model state contains error by member expression.
        /// </summary>
        /// <typeparam name="TMember">Type of the member which will be tested for errors.</typeparam>
        /// <param name="memberWithError">Member expression for the tested member.</param>
        /// <returns>Model error details test builder.</returns>
        IModelErrorDetailsTestBuilder<TModel> ContainingErrorFor<TMember>(Expression<Func<TModel, TMember>> memberWithError);

        /// <summary>
        /// Tests whether tested action's model state contains no error by member expression.
        /// </summary>
        /// <typeparam name="TMember">Type of the member which will be tested for no errors.</typeparam>
        /// <param name="memberWithNoError">Member expression for the tested member.</param>
        /// <returns>This instance in order to support method chaining.</returns>
        IAndModelErrorTestBuilder<TModel> ContainingNoErrorFor<TMember>(
            Expression<Func<TModel, TMember>> memberWithNoError);
    }
}
