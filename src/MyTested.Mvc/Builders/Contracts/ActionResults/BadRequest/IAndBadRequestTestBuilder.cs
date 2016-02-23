namespace MyTested.Mvc.Builders.Contracts.ActionResults.BadRequest
{
    /// <summary>
    /// Used for adding AndAlso() method to the HTTP bad request response tests.
    /// </summary>
    public interface IAndBadRequestTestBuilder : IBadRequestTestBuilder
    {
        /// <summary>
        /// AndAlso method for better readability when chaining HTTP bad request result tests.
        /// </summary>
        /// <returns>HTTP bad request result test builder.</returns>
        IBadRequestTestBuilder AndAlso();
    }
}
