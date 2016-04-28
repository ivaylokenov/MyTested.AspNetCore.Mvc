namespace MyTested.Mvc.Builders.Actions.ShouldReturn
{
    using ActionResults.Challenge;
    using Contracts.ActionResults.Challenge;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Class containing methods for testing <see cref="ChallengeResult"/>.
    /// </summary>
    /// <typeparam name="TActionResult">Result from invoked action in ASP.NET Core MVC controller.</typeparam>
    public partial class ShouldReturnTestBuilder<TActionResult>
    {
        /// <inheritdoc />
        public IChallengeTestBuilder Challenge()
        {
            this.TestContext.ActionResult = this.GetReturnObject<ChallengeResult>();
            return new ChallengeTestBuilder(this.TestContext);
        }
    }
}
