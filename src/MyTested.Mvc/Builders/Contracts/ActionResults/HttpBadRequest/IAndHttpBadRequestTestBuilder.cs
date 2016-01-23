namespace MyTested.Mvc.Builders.Contracts.ActionResults.HttpBadRequest
{
    /// <summary>
    /// Used for adding AndAlso() method to the HTTP bad request response tests.
    /// </summary>
    public interface IAndHttpBadRequestTestBuilder : IHttpBadRequestTestBuilder
    {
        /// <summary>
        /// AndAlso method for better readability when chaining HTTP bad request result tests.
        /// </summary>
        /// <returns>HTTP bad request result test builder.</returns>
        IHttpBadRequestTestBuilder AndAlso();
    }
}
