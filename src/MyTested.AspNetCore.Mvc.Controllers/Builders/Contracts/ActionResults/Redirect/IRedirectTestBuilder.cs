namespace MyTested.AspNetCore.Mvc.Builders.Contracts.ActionResults.Redirect
{
    using Base;

    /// <summary>
    /// Used for testing <see cref="Microsoft.AspNetCore.Mvc.RedirectResult"/>,
    /// <see cref="Microsoft.AspNetCore.Mvc.RedirectToActionResult"/>
    /// or <see cref="Microsoft.AspNetCore.Mvc.RedirectToRouteResult"/>.
    /// </summary>
    public interface IRedirectTestBuilder : IBaseTestBuilderWithRedirectResult<IAndRedirectTestBuilder>, 
        IBaseTestBuilderWithRouteValuesResult<IAndRedirectTestBuilder>
    {
        /// <summary>
        /// Tests whether the <see cref="Microsoft.AspNetCore.Mvc.RedirectToActionResult"/>
        /// has specific action name.
        /// </summary>
        /// <param name="actionName">Expected action name.</param>
        /// <returns>The same <see cref="IAndRedirectTestBuilder"/>.</returns>
        IAndRedirectTestBuilder ToAction(string actionName);

        /// <summary>
        /// Tests whether the <see cref="Microsoft.AspNetCore.Mvc.RedirectToActionResult"/>
        /// result has specific controller name.
        /// </summary>
        /// <param name="controllerName">Expected controller name.</param>
        /// <returns>The same <see cref="IAndRedirectTestBuilder"/>.</returns>
        IAndRedirectTestBuilder ToController(string controllerName);
    }
}
