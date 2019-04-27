namespace MyTested.AspNetCore.Mvc.Builders.Contracts.ActionResults.Forbid
{
    using Base;
    using Contracts.Base;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Used for testing <see cref="ForbidResult"/>.
    /// </summary>
    public interface IForbidTestBuilder 
        : IBaseTestBuilderWithAuthenticationResult<IAndForbidTestBuilder>,
        IBaseTestBuilderWithActionResult<ForbidResult>
    {
    }
}
