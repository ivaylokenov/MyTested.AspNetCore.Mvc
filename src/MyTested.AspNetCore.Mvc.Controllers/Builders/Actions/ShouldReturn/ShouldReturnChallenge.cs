namespace MyTested.AspNetCore.Mvc.Builders.Actions.ShouldReturn
{
    using System;
    using ActionResults.Challenge;
    using Contracts.ActionResults.Challenge;
    using Contracts.And;
    using Microsoft.AspNetCore.Mvc;

    /// <content>
    /// Class containing methods for testing <see cref="ChallengeResult"/>.
    /// </content>
    public partial class ShouldReturnTestBuilder<TActionResult>
    {
        /// <inheritdoc />
        public IAndTestBuilder Challenge() => this.Challenge(null);

        /// <inheritdoc />
        public IAndTestBuilder Challenge(Action<IChallengeTestBuilder> challengeTestBuilder)
            => this.ValidateActionResult<ChallengeResult, IChallengeTestBuilder>(
                challengeTestBuilder,
                new ChallengeTestBuilder(this.TestContext));
    }
}
