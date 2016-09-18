namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Models
{
    using And;
    using Base;
    using System;

    /// <summary>
    /// Used for testing the model members.
    /// </summary>
    /// <typeparam name="TModel">Model from invoked method in ASP.NET Core MVC.</typeparam>
    public interface IModelDetailsTestBuilder<TModel> : IBaseTestBuilderWithComponent
    {
        /// <summary>
        /// Tests whether the returned model from the invoked method passes the given assertions.
        /// </summary>
        /// <param name="assertions">Method containing all assertions on the model.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/>.</returns>
        IAndTestBuilder Passing(Action<TModel> assertions);

        /// <summary>
        /// Tests whether the returned model from the invoked method passes the given predicate.
        /// </summary>
        /// <param name="predicate">Predicate testing the model.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/>.</returns>
        IAndTestBuilder Passing(Func<TModel, bool> predicate);
    }
}
