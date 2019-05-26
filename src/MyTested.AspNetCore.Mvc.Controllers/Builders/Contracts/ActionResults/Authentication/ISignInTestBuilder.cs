namespace MyTested.AspNetCore.Mvc.Builders.Contracts.ActionResults.Authentication
{
    using Base;
    using Contracts.Base;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Used for testing <see cref="SignInResult"/>.
    /// </summary>
    public interface ISignInTestBuilder
        : IBaseTestBuilderWithAuthenticationResult<IAndSignInTestBuilder>,
        IBaseTestBuilderWithActionResult<SignInResult>
    {
    }
}
