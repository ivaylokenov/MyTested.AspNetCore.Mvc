namespace MyTested.AspNetCore.Mvc.Builders.Contracts.ActionResults.Challenge
{
    using Base;

    /// <summary>
    /// Used for testing <see cref="Microsoft.AspNetCore.Mvc.ChallengeResult"/>.
    /// </summary>
    public interface IChallengeTestBuilder : IBaseTestBuilderWithAuthenticationResult<IAndChallengeTestBuilder>
    {
    }
}
