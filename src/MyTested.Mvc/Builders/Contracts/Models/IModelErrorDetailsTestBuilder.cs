namespace MyTested.Mvc.Builders.Contracts.Models
{
    using Base;
    using System;
    using System.Linq.Expressions;

    /// <summary>
    /// Used for testing specific model errors.
    /// </summary>
    /// <typeparam name="TModel">Model from invoked action in ASP.NET MVC 6 controller.</typeparam>
    public interface IModelErrorDetailsTestBuilder<TModel> : IBaseTestBuilderWithModel<TModel>
    {
        /// <summary>
        /// Tests whether particular error message is equal to given message.
        /// </summary>
        /// <param name="errorMessage">Expected error message for particular key.</param>
        /// <returns>The original model error test builder.</returns>
        IAndModelErrorTestBuilder<TModel> ThatEquals(string errorMessage);

        /// <summary>
        /// Tests whether particular error message begins with given message.
        /// </summary>
        /// <param name="beginMessage">Expected beginning for particular error message.</param>
        /// <returns>The original model error test builder.</returns>
        IAndModelErrorTestBuilder<TModel> BeginningWith(string beginMessage);

        /// <summary>
        /// Tests whether particular error message ends with given message.
        /// </summary>
        /// <param name="endMessage">Expected ending for particular error message.</param>
        /// <returns>The original model error test builder.</returns>
        IAndModelErrorTestBuilder<TModel> EndingWith(string endMessage);

        /// <summary>
        /// Tests whether particular error message contains given message.
        /// </summary>
        /// <param name="containsMessage">Expected containing string for particular error message.</param>
        /// <returns>The original model error test builder.</returns>
        IAndModelErrorTestBuilder<TModel> Containing(string containsMessage);

        /// <summary>
        /// Tests whether tested action's model state contains error by key.
        /// </summary>
        /// <param name="errorKey">Error key to search for.</param>
        /// <returns>Model error details test builder.</returns>
        IModelErrorDetailsTestBuilder<TModel> ContainingModelStateError(string errorKey);

        /// <summary>
        /// Tests whether tested action's model state contains error by member expression.
        /// </summary>
        /// <typeparam name="TMember">Type of the member which will be tested for errors.</typeparam>
        /// <param name="memberWithError">Member expression for the tested member.</param>
        /// <returns>Model error details test builder.</returns>
        IModelErrorDetailsTestBuilder<TModel> ContainingModelStateErrorFor<TMember>(
            Expression<Func<TModel, TMember>> memberWithError);

        /// <summary>
        /// Tests whether tested action's model state contains no error by member expression.
        /// </summary>
        /// <typeparam name="TMember">Type of the member which will be tested for no errors.</typeparam>
        /// <param name="memberWithNoError">Member expression for the tested member.</param>
        /// <returns>Model error details test builder.</returns>
        IModelErrorTestBuilder<TModel> ContainingNoModelStateErrorFor<TMember>(
            Expression<Func<TModel, TMember>> memberWithNoError);

        /// <summary>
        /// AndAlso method for better readability when chaining error message tests.
        /// </summary>
        /// <returns>Model error details test builder.</returns>
        IModelErrorTestBuilder<TModel> AndAlso();
    }
}
