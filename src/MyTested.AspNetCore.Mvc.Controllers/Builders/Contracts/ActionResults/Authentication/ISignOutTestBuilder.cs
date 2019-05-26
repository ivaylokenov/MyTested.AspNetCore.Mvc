namespace MyTested.AspNetCore.Mvc.Builders.Contracts.ActionResults.Authentication
{
    using Base;
    using Contracts.Base;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Used for testing <see cref="SignOutResult"/>.
    /// </summary>
    public interface ISignOutTestBuilder
        : IBaseTestBuilderWithAuthenticationSchemesResult<IAndSignOutTestBuilder>,
        IBaseTestBuilderWithActionResult<SignOutResult>
    {
    }
}
