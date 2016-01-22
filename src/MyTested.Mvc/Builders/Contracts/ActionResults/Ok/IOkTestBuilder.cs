namespace MyTested.Mvc.Builders.Contracts.ActionResults.Ok
{
    using Base;

    /// <summary>
    /// Used for testing ok result.
    /// </summary>
    public interface IOkTestBuilder : IBaseTestBuilderWithResponseModel
    {
        /// <summary>
        /// Tests whether no response model is returned from the invoked action.
        /// </summary>
        /// <returns>The same ok test builder.</returns>
        IAndOkTestBuilder WithNoResponseModel();
    }
}
