namespace MyTested.AspNetCore.Mvc.Builders.Contracts.ShouldPassFor
{
    using System;

    /// <summary>
    /// Test builder allowing additional assertions on various components.
    /// </summary>
    /// <typeparam name="TModel">Model from invoked action in ASP.NET Core MVC controller.</typeparam>
    public interface IShouldPassForTestBuilderWithModel<TModel> : IShouldPassForTestBuilderWithInvokedAction
    {
        /// <summary>
        /// Tests whether the model passes the given assertions.
        /// </summary>
        /// <param name="assertions">Action containing assertions on the model.</param>
        /// <returns>The same <see cref="IShouldPassForTestBuilderWithModel{TModel}"/>.</returns>
        IShouldPassForTestBuilderWithModel<TModel> TheModel(Action<TModel> assertions);

        /// <summary>
        /// Tests whether the model passes the given predicate.
        /// </summary>
        /// <param name="predicate">Predicate testing the caught model.</param>
        /// <returns>The same <see cref="IShouldPassForTestBuilderWithModel{TModel}"/>.</returns>
        IShouldPassForTestBuilderWithModel<TModel> TheModel(Func<TModel, bool> predicate);
    }
}
