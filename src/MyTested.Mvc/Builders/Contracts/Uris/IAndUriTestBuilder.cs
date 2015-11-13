namespace MyTested.Mvc.Builders.Contracts.Uris
{
    /// <summary>
    /// Used for adding AndAlso() method to the the URI tests.
    /// </summary>
    public interface IAndUriTestBuilder : IUriTestBuilder
    {
        /// <summary>
        /// AndAlso method for better readability when chaining URI tests.
        /// </summary>
        /// <returns>The same URI test builder.</returns>
        IUriTestBuilder AndAlso();
    }
}
