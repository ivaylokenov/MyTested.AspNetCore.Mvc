namespace MyTested.AspNetCore.Mvc.Builders.Actions.ShouldReturn
{
    using ActionResults.Challenge;
    using Contracts.ActionResults.Challenge;
    using Microsoft.AspNetCore.Mvc;

    /// <content>
    /// Class containing methods for testing <see cref="ChallengeResult"/>.
    /// </content>
    public partial class ShouldReturnTestBuilder<TActionResult>
    {
        /// <inheritdoc />
        public IChallengeTestBuilder Challenge()
        {
            this.TestContext.MethodResult = this.GetReturnObject<ChallengeResult>();
            return new ChallengeTestBuilder(this.TestContext);
        }
    }
}
