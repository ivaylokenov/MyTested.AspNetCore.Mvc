namespace MyTested.AspNetCore.Mvc.Builders.Contracts.ActionResults.LocalRedirect
{
    using Base;
    using Contracts.Base;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Used for testing <see cref="LocalRedirectResult"/>.
    /// </summary>
    public interface ILocalRedirectTestBuilder 
        : IBaseTestBuilderWithRedirectResult<IAndLocalRedirectTestBuilder>,
        IBaseTestBuilderWithActionResult<LocalRedirectResult>
    {
    }
}
