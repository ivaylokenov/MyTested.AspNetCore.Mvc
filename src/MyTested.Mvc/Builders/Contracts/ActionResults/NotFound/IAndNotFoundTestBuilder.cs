namespace MyTested.Mvc.Builders.Contracts.ActionResults.NotFound
{
    /// <summary>
    /// Used for adding AndAlso() method to the HTTP not found response tests.
    /// </summary>
    public interface IAndNotFoundTestBuilder : INotFoundTestBuilder
    {
        /// <summary>
        /// AndAlso method for better readability when chaining HTTP not found result tests.
        /// </summary>
        /// <returns>HTTP not found result test builder.</returns>
        IAndNotFoundTestBuilder AndAlso();
    }
}
