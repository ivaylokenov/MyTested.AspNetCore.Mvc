namespace MyTested.AspNetCore.Mvc.Builders.Contracts.ActionResults.Challenge
{
    using Base;
    using Contracts.Base;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Used for testing <see cref="ChallengeResult"/>.
    /// </summary>
    public interface IChallengeTestBuilder 
        : IBaseTestBuilderWithAuthenticationResult<IAndChallengeTestBuilder>,
        IBaseTestBuilderWithActionResult<ChallengeResult>
    {
    }
}
