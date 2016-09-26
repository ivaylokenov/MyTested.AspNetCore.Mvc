namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Models
{
    /// <summary>
    /// Used for adding AndAlso() method to the model details tests.
    /// </summary>
    /// <typeparam name="TModel">Model from invoked method in ASP.NET Core MVC.</typeparam>
    public interface IAndModelDetailsTestBuilder<TModel> : IModelDetailsTestBuilder<TModel>
    {
        /// <summary>
        /// AndAlso method for better readability when chaining model details tests.
        /// </summary>
        /// <returns>The same <see cref="IModelDetailsTestBuilder{TModel}"/>.</returns>
        IModelDetailsTestBuilder<TModel> AndAlso();
    }
}
