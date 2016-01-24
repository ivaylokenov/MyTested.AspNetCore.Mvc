namespace MyTested.Mvc.Builders.Contracts.ActionResults.HttpNotFound
{
    using Base;
    using Microsoft.AspNet.Mvc;

    /// <summary>
    /// Used for testing HTTP not found result.
    /// </summary>
    public interface IHttpNotFoundTestBuilder : IBaseTestBuilderWithResponseModel,
        IBaseTestBuilderWithActionResult<ActionResult>
    {
        /// <summary>
        /// Tests whether no response model is returned from the invoked action.
        /// </summary>
        /// <returns>Base test builder with action.</returns>
        IAndHttpNotFoundTestBuilder WithNoResponseModel();
    }
}
