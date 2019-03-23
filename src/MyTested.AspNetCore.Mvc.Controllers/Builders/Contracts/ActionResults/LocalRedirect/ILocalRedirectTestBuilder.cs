namespace MyTested.AspNetCore.Mvc.Builders.Contracts.ActionResults.LocalRedirect
{
    using Base;

    /// <summary>
    /// Used for testing <see cref="Microsoft.AspNetCore.Mvc.LocalRedirectResult"/>.
    /// </summary>
    public interface ILocalRedirectTestBuilder : IBaseTestBuilderWithRedirectResult<IAndLocalRedirectTestBuilder>
    {
    }
}
