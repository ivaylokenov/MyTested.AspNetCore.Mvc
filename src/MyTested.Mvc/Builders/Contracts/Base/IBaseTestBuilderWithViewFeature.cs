namespace MyTested.Mvc.Builders.Contracts.Base
{
    using Microsoft.AspNetCore.Mvc;
    using Models;

    /// <summary>
    /// Base interface for all test builder with view features.
    /// </summary>
    public interface IBaseTestBuilderWithViewFeature : IBaseTestBuilderWithActionResult<ActionResult>
    {
        /// <summary>
        /// Tests whether view result contains deeply equal model object as the provided one.
        /// </summary>
        /// <typeparam name="TModel">Type of model object.</typeparam>
        /// <param name="model">Model object.</param>
        /// <returns>Model details test builder.</returns>
        IModelDetailsTestBuilder<TModel> WithModel<TModel>(TModel model);

        /// <summary>
        /// Tests whether view result contains model object of the provided type.
        /// </summary>
        /// <typeparam name="TModel">Type of model object.</typeparam>
        /// <returns>Model details test builder.</returns>
        IModelDetailsTestBuilder<TModel> WithModelOfType<TModel>();
    }
}
