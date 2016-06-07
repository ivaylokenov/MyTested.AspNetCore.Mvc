namespace MyTested.Mvc.Builders.Contracts.Models
{
    /// <summary>
    /// Used for testing <see cref="Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary"/> errors.
    /// </summary>
    public interface IModelStateTestBuilder : IModelErrorTestBuilder
    {
        /// <summary>
        /// Specifies the model which will be tested for <see cref="Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary"/> errors.
        /// </summary>
        /// <typeparam name="TModel">Model from invoked action in ASP.NET Core MVC controller.</typeparam>
        /// <returns>Test builder of <see cref="IModelErrorTestBuilder{TModel}"/>.</returns>
        IModelErrorTestBuilder<TModel> For<TModel>();
    }
}
