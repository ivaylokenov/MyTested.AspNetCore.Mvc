namespace MyTested.AspNetCore.Mvc.Builders.Contracts.ActionResults.Forbid
{
    using Base;

    /// <summary>
    /// Used for testing <see cref="Microsoft.AspNetCore.Mvc.ForbidResult"/>.
    /// </summary>
    public interface IForbidTestBuilder : IBaseTestBuilderWithAuthenticationResult<IAndForbidTestBuilder>
    {
    }
}
