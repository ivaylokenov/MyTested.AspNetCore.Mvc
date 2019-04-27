namespace MyTested.AspNetCore.Mvc.Builders.Contracts.ActionResults.Redirect
{
    using Base;

    /// <summary>
    /// Used for testing <see cref="Microsoft.AspNetCore.Mvc.RedirectResult"/>,
    /// <see cref="Microsoft.AspNetCore.Mvc.RedirectToActionResult"/>
    /// or <see cref="Microsoft.AspNetCore.Mvc.RedirectToRouteResult"/>.
    /// </summary>
    public interface IRedirectTestBuilder 
        : IBaseTestBuilderWithRedirectResult<IAndRedirectTestBuilder>, 
        IBaseTestBuilderWithRouteValuesResult<IAndRedirectTestBuilder>
    {
    }
}
