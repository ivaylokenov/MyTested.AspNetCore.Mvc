namespace MyTested.AspNetCore.Mvc.Builders.Contracts.ActionResults.Redirect
{
    /// <summary>
    /// Used for adding AndAlso() method to the <see cref="Microsoft.AspNetCore.Mvc.RedirectResult"/>, <see cref="Microsoft.AspNetCore.Mvc.RedirectToActionResult"/> or <see cref="Microsoft.AspNetCore.Mvc.RedirectToRouteResult"/> tests.
    /// </summary>
    public interface IAndRedirectTestBuilder : IRedirectTestBuilder
    {
        /// <summary>
        /// AndAlso method for better readability when chaining <see cref="Microsoft.AspNetCore.Mvc.RedirectResult"/>, <see cref="Microsoft.AspNetCore.Mvc.RedirectToActionResult"/> or <see cref="Microsoft.AspNetCore.Mvc.RedirectToRouteResult"/> tests.
        /// </summary>
        /// <returns>The same <see cref="IRedirectTestBuilder"/>.</returns>
        IRedirectTestBuilder AndAlso();
    }
}
