namespace MyTested.Mvc.Builders.Contracts.ActionResults.LocalRedirect
{
    /// <summary>
    /// Used for adding AndAlso() method to the <see cref="Microsoft.AspNetCore.Mvc.LocalRedirectResult"/> tests.
    /// </summary>
    public interface IAndLocalRedirectTestBuilder : ILocalRedirectTestBuilder
    {
        /// <summary>
        /// AndAlso method for better readability when chaining <see cref="Microsoft.AspNetCore.Mvc.LocalRedirectResult"/> tests.
        /// </summary>
        /// <returns>The same <see cref="ILocalRedirectTestBuilder"/>.</returns>
        ILocalRedirectTestBuilder AndAlso();
    }
}
