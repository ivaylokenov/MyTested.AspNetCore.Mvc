namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Base
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
        /// <returns>Test builder of <see cref="IModelDetailsTestBuilder{TModel}"/>.</returns>
        IAndModelDetailsTestBuilder<TModel> WithModel<TModel>(TModel model);

        /// <summary>
        /// Tests whether view result has model object of the provided type.
        /// </summary>
        /// <typeparam name="TModel">Type of model object.</typeparam>
        /// <returns>Test builder of <see cref="IModelDetailsTestBuilder{TModel}"/>.</returns>
        IAndModelDetailsTestBuilder<TModel> WithModelOfType<TModel>();
    }
}
