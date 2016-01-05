namespace MyTested.Mvc.Builders.Actions.ShouldReturn
{
    using ActionResults.Challenge;
    using Contracts.ActionResults.Challenge;
    using Microsoft.AspNet.Mvc;

    /// <summary>
    /// Class containing methods for testing ChallengeResult.
    /// </summary>
    /// <typeparam name="TActionResult">Result from invoked action in ASP.NET MVC 6 controller.</typeparam>
    public partial class ShouldReturnTestBuilder<TActionResult>
    {
        /// <summary>
        /// Tests whether action result is ChallengeResult.
        /// </summary>
        /// <returns>Challenge test builder.</returns>
        public IChallengeTestBuilder Challenge()
        {
            this.ValidateActionReturnType(typeof(ChallengeResult));
            return new ChallengeTestBuilder(
                this.Controller,
                this.ActionName,
                this.CaughtException,
                this.ActionResult as ChallengeResult);
        }
    }
}
