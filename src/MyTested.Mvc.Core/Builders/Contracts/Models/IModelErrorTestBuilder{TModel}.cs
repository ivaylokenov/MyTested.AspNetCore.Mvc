namespace MyTested.Mvc.Builders.Contracts.Models
{
    using Base;

    /// <summary>
    /// Used for testing <see cref="Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary"/> errors.
    /// </summary>
    /// <typeparam name="TModel">Model from invoked action in ASP.NET Core MVC controller.</typeparam>
    public interface IModelErrorTestBuilder<TModel> : IModelErrorTestBuilder, IBaseTestBuilderWithModel<TModel>
    {
    }
}
