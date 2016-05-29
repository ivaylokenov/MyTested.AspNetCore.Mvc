namespace MyTested.Mvc.Builders.Contracts.Models
{
    using System;

    /// <summary>
    /// Used for testing the model members.
    /// </summary>
    /// <typeparam name="TResponseModel">Model from invoked action in ASP.NET Core MVC controller.</typeparam>
    public interface IModelDetailsTestBuilder<TResponseModel> : IModelErrorTestBuilder<TResponseModel>
    {
        /// <summary>
        /// Tests whether the returned model from the invoked action passes given assertions.
        /// </summary>
        /// <param name="assertions">Action containing all assertions on the model.</param>
        /// <returns>Test builder of <see cref="IModelErrorTestBuilder{TResponseModel}"/>.</returns>
        IModelErrorTestBuilder<TResponseModel> Passing(Action<TResponseModel> assertions);

        /// <summary>
        /// Tests whether the returned model from the invoked action passes given predicate.
        /// </summary>
        /// <param name="predicate">Predicate testing the model.</param>
        /// <returns>Test builder of <see cref="IModelErrorTestBuilder{TResponseModel}"/>.</returns>
        IModelErrorTestBuilder<TResponseModel> Passing(Func<TResponseModel, bool> predicate);
    }
}
