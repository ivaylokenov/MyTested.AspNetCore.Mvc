namespace MyTested.Mvc.Builders.Contracts.ActionResults.Ok
{
    using Models;

    /// <summary>
    /// Used for testing ok result.
    /// </summary>
    public interface IOkTestBuilder : IBaseResponseModelTestBuilder
    {
        /// <summary>
        /// Tests whether no response model is returned from the invoked action.
        /// </summary>
        /// <returns>The same ok test builder.</returns>
        IAndOkTestBuilder WithNoResponseModel();
    }
}
