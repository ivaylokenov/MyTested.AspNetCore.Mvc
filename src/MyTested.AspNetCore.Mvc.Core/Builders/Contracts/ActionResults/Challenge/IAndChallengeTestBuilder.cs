namespace MyTested.AspNetCore.Mvc.Builders.Contracts.ActionResults.Challenge
{
    /// <summary>
    /// Used for adding AndAlso() method to the <see cref="Microsoft.AspNetCore.Mvc.ChallengeResult"/> tests.
    /// </summary>
    public interface IAndChallengeTestBuilder : IChallengeTestBuilder
    {
        /// <summary>
        /// AndAlso method for better readability when chaining <see cref="Microsoft.AspNetCore.Mvc.ChallengeResult"/> tests.
        /// </summary>
        /// <returns>The same <see cref="IChallengeTestBuilder"/>.</returns>
        IChallengeTestBuilder AndAlso();
    }
}
