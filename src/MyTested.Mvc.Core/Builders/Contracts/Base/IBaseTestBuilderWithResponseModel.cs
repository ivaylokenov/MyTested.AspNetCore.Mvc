namespace MyTested.Mvc.Builders.Contracts.Base
{
    using Models;

    /// <summary>
    /// Base interface for all test builders with response model.
    /// </summary>
    public interface IBaseTestBuilderWithResponseModel : IBaseTestBuilderWithInvokedAction
    {
        /// <summary>
        /// Tests whether response model of the given type is returned from the invoked action.
        /// </summary>
        /// <typeparam name="TResponseModel">Type of the response model.</typeparam>
        /// <returns>Test builder of <see cref="IModelDetailsTestBuilder{TResponseModel}"/>.</returns>
        IModelDetailsTestBuilder<TResponseModel> WithResponseModelOfType<TResponseModel>();

        /// <summary>
        /// Tests whether a deeply equal object to the provided one is returned from the invoked action.
        /// </summary>
        /// <typeparam name="TResponseModel">Type of the response model.</typeparam>
        /// <param name="expectedModel">Expected model to be returned.</param>
        /// <returns>Test builder of <see cref="IModelDetailsTestBuilder{TResponseModel}"/>.</returns>
        IModelDetailsTestBuilder<TResponseModel> WithResponseModel<TResponseModel>(TResponseModel expectedModel);
    }
}
