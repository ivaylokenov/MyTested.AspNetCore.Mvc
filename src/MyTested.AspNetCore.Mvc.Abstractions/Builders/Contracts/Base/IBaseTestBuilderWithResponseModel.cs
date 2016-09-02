namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Base
{
    using Models;

    /// <summary>
    /// Base interface for all test builders with response model.
    /// </summary>
    public interface IBaseTestBuilderWithResponseModel : IBaseTestBuilderWithComponent
    {
        /// <summary>
        /// Tests whether response model of the given type is returned from the invoked method.
        /// </summary>
        /// <typeparam name="TModel">Type of the response model.</typeparam>
        /// <returns>Test builder of <see cref="IModelDetailsTestBuilder{TResponseModel}"/>.</returns>
        IAndModelDetailsTestBuilder<TModel> WithModelOfType<TModel>();

        /// <summary>
        /// Tests whether a deeply equal object to the provided one is returned from the invoked method.
        /// </summary>
        /// <typeparam name="TModel">Type of the response model.</typeparam>
        /// <param name="expectedModel">Expected model to be returned.</param>
        /// <returns>Test builder of <see cref="IModelDetailsTestBuilder{TResponseModel}"/>.</returns>
        IAndModelDetailsTestBuilder<TModel> WithModel<TModel>(TModel expectedModel);
    }
}
