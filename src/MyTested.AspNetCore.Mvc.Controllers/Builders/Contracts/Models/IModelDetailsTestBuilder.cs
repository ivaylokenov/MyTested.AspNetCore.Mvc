namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Models
{
    using And;
    using System;

    /// <summary>
    /// Used for testing the model members.
    /// </summary>
    /// <typeparam name="TModel">Model from invoked action in ASP.NET Core MVC controller.</typeparam>
    public interface IModelDetailsTestBuilder<TModel> : IModelErrorTestBuilder<TModel>
    {
        /// <summary>
        /// Tests whether the returned model from the invoked action passes the given assertions.
        /// </summary>
        /// <param name="assertions">Action containing all assertions on the model.</param>
        /// <returns>Test builder of <see cref="IAndModelErrorTestBuilder{TResponseModel}"/>.</returns>
        IAndTestBuilderWithInvokedAction Passing(Action<TModel> assertions);

        /// <summary>
        /// Tests whether the returned model from the invoked action passes the given predicate.
        /// </summary>
        /// <param name="predicate">Predicate testing the model.</param>
        /// <returns>Test builder of <see cref="IAndModelErrorTestBuilder{TResponseModel}"/>.</returns>
        IAndTestBuilderWithInvokedAction Passing(Func<TModel, bool> predicate);
    }
}
