namespace MyTested.Mvc.Builders.Contracts.Base
{
    using Models;

    /// <summary>
    /// Base interface for all test builders with response model.
    /// </summary>
    public interface IBaseTestBuilderWithResponseModel : IBaseTestBuilderWithInvokedAction
    {
        /// <summary>
        /// Tests whether response model is returned from the invoked action.
        /// </summary>
        /// <typeparam name="TResponseModel">Type of the response model.</typeparam>
        /// <returns>Builder for testing the response model errors.</returns>
        IModelDetailsTestBuilder<TResponseModel> WithResponseModelOfType<TResponseModel>();

        /// <summary>
        /// Tests whether a deeply equal object to the provided one is returned from the invoked action.
        /// </summary>
        /// <typeparam name="TResponseModel">Type of the response model.</typeparam>
        /// <param name="expectedModel">Expected model to be returned.</param>
        /// <returns>Builder for testing the response model errors.</returns>
        IModelDetailsTestBuilder<TResponseModel> WithResponseModel<TResponseModel>(TResponseModel expectedModel);
    }
}
