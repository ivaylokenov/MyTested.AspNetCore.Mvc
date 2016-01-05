namespace MyTested.Mvc.Builders.Contracts.ActionResults.Challenge
{
    /// <summary>
    /// Used for adding AndAlso() method to the the challenge response tests.
    /// </summary>
    public interface IAndChallengeTestBuilder : IChallengeTestBuilder
    {
        /// <summary>
        /// AndAlso method for better readability when chaining challenge tests.
        /// </summary>
        /// <returns>The same challenge test builder.</returns>
        IChallengeTestBuilder AndAlso();
    }
}
