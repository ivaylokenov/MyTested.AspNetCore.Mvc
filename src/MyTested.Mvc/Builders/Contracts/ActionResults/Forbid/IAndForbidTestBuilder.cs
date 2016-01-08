namespace MyTested.Mvc.Builders.Contracts.ActionResults.Forbid
{
    /// <summary>
    /// Used for adding AndAlso() method to the the forbid response tests.
    /// </summary>
    public interface IAndForbidTestBuilder : IForbidTestBuilder
    {
        /// <summary>
        /// AndAlso method for better readability when chaining forbid tests.
        /// </summary>
        /// <returns>The same forbid test builder.</returns>
        IForbidTestBuilder AndAlso();
    }
}
