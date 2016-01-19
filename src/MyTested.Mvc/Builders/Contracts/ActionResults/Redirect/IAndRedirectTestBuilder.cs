namespace MyTested.Mvc.Builders.Contracts.ActionResults.Redirect
{
    /// <summary>
    /// Used for adding AndAlso() method to the the redirect response tests.
    /// </summary>
    public interface IAndRedirectTestBuilder : IRedirectTestBuilder
    {
        /// <summary>
        /// AndAlso method for better readability when chaining redirect result tests.
        /// </summary>
        /// <returns>Redirect result test builder.</returns>
        IRedirectTestBuilder AndAlso();
    }
}
