namespace MyTested.Mvc.Builders.Contracts.ActionResults.HttpNotFound
{
    using Base;

    /// <summary>
    /// Used for testing HTTP not found result.
    /// </summary>
    public interface IHttpNotFoundTestBuilder : IBaseTestBuilderWithResponseModel
    {
        /// <summary>
        /// Tests whether no response model is returned from the invoked action.
        /// </summary>
        /// <returns>Base test builder with action.</returns>
        IBaseTestBuilderWithCaughtException WithNoResponseModel();
    }
}
