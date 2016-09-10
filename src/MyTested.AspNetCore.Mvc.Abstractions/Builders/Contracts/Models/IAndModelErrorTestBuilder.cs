namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Models
{
    /// <summary>
    /// Used for adding AndAlso() method to the <see cref="Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary"/> error tests.
    /// </summary>
    /// <typeparam name="TModel">Model from invoked method in ASP.NET Core MVC.</typeparam>
    public interface IAndModelErrorTestBuilder<TModel> : IModelErrorTestBuilder<TModel>
    {
        /// <summary>
        /// AndAlso method for better readability when chaining <see cref="Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary"/> error message tests.
        /// </summary>
        /// <returns>The same <see cref="IModelErrorTestBuilder{TModel}"/>.</returns>
        IModelErrorTestBuilder<TModel> AndAlso();
    }
}
