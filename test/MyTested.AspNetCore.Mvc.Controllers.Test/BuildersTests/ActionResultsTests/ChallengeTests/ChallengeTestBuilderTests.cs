namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.ActionResultsTests.ChallengeTests
{
    using Microsoft.AspNetCore.Mvc;
    using Setups.Controllers;
    using Xunit;

    public class ChallengeTestBuilderTests
    {
        [Fact]
        public void AndProvideTheActionResultShouldWorkCorrectly()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.ChallengeWithAuthenticationProperties())
                .ShouldReturn()
                .Challenge()
                .AndAlso()
                .ShouldPassForThe<ActionResult>(actionResult =>
                {
                    Assert.NotNull(actionResult);
                    Assert.IsAssignableFrom<ChallengeResult>(actionResult);
                });
        }
    }
}
