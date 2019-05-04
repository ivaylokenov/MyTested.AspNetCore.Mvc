namespace MyTested.AspNetCore.Mvc.Builders.Contracts.ActionResults.Redirect
{
    using System;
    using And;
    using Base;
    using Contracts.Base;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Used for testing <see cref="RedirectResult"/>, <see cref="RedirectToActionResult"/>
    /// or <see cref="RedirectToRouteResult"/>.
    /// </summary>
    public interface IRedirectTestBuilder 
        : IBaseTestBuilderWithRedirectResult<IAndRedirectTestBuilder>, 
        IBaseTestBuilderWithRouteValuesResult<IAndRedirectTestBuilder>,
        IBaseTestBuilderWithActionResult<RedirectResult>,
        IBaseTestBuilderWithActionResult<RedirectToRouteResult>
    {
        /// <summary>
        /// Tests whether the <see cref="RedirectToActionResult"/> passes the given assertions.
        /// </summary>
        /// <param name="assertions">Action containing all assertions
        /// for the <see cref="RedirectToActionResult"/>.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        IAndTestBuilder Passing(Action<RedirectToActionResult> assertions);

        /// <summary>
        /// Tests whether the <see cref="RedirectToActionResult"/> passes the given predicate.
        /// </summary>
        /// <param name="predicate">Predicate testing the <see cref="RedirectToActionResult"/>.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder"/> type.</returns>
        IAndTestBuilder Passing(Func<RedirectToActionResult, bool> predicate);
    }
}
