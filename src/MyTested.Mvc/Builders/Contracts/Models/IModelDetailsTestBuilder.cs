namespace MyTested.Mvc.Builders.Contracts.Models
{
    using System;

    /// <summary>
    /// Used for testing the response model members.
    /// </summary>
    /// <typeparam name="TResponseModel">Response model from invoked action in ASP.NET Core MVC controller.</typeparam>
    public interface IModelDetailsTestBuilder<TResponseModel> : IModelErrorTestBuilder<TResponseModel>
    {
        /// <summary>
        /// Tests whether the returned response model from the invoked action passes given assertions.
        /// </summary>
        /// <param name="assertions">Action containing all assertions on the response model.</param>
        /// <returns>Builder for testing the response model errors.</returns>
        IModelErrorTestBuilder<TResponseModel> Passing(Action<TResponseModel> assertions);

        /// <summary>
        /// Tests whether the returned response model from the invoked action passes given predicate.
        /// </summary>
        /// <param name="predicate">Predicate testing the response model.</param>
        /// <returns>Builder for testing the response model errors.</returns>
        IModelErrorTestBuilder<TResponseModel> Passing(Func<TResponseModel, bool> predicate);
    }
}
