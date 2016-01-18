namespace MyTested.Mvc.Builders.Contracts.Models
{
    /// <summary>
    /// Used for adding AndAlso() method to the the model error tests.
    /// </summary>
    /// <typeparam name="TModel">Model from invoked action in ASP.NET MVC controller.</typeparam>
    public interface IAndModelErrorTestBuilder<TModel> : IModelErrorTestBuilder<TModel>
    {
        /// <summary>
        /// AndAlso method for better readability when chaining error message tests.
        /// </summary>
        /// <returns>Model error details test builder.</returns>
        IModelErrorTestBuilder<TModel> AndAlso();
    }
}
