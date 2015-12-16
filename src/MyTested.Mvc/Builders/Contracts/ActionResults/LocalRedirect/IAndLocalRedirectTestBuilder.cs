namespace MyTested.Mvc.Builders.Contracts.ActionResults.LocalRedirect
{
    /// <summary>
    /// Used for adding AndAlso() method to the the local redirect response tests.
    /// </summary>
    public interface IAndLocalRedirectTestBuilder : ILocalRedirectTestBuilder
    {
        /// <summary>
        /// AndAlso method for better readability when chaining local redirect result tests.
        /// </summary>
        /// <returns>Local redirect result test builder.</returns>
        ILocalRedirectTestBuilder AndAlso();
    }
}
