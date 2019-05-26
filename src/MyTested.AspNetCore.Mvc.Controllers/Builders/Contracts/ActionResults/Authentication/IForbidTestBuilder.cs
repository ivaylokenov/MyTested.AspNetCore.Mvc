namespace MyTested.AspNetCore.Mvc.Builders.Contracts.ActionResults.Authentication
{
    using Base;
    using Contracts.Base;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Used for testing <see cref="ForbidResult"/>.
    /// </summary>
    public interface IForbidTestBuilder 
        : IBaseTestBuilderWithAuthenticationSchemesResult<IAndForbidTestBuilder>,
        IBaseTestBuilderWithActionResult<ForbidResult>
    {
    }
}
