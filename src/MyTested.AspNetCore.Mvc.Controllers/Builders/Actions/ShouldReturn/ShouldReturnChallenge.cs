namespace MyTested.AspNetCore.Mvc.Builders.Actions.ShouldReturn
{
    using ActionResults.Challenge;
    using Contracts.ActionResults.Challenge;
    using Microsoft.AspNetCore.Mvc;
    using Utilities.Validators;

    /// <content>
    /// Class containing methods for testing <see cref="ChallengeResult"/>.
    /// </content>
    public partial class ShouldReturnTestBuilder<TActionResult>
    {
        /// <inheritdoc />
        public IAndChallengeTestBuilder Challenge()
        {
            InvocationResultValidator.ValidateInvocationResultType<ChallengeResult>(this.TestContext);
            return new ChallengeTestBuilder(this.TestContext);
        }
    }
}
