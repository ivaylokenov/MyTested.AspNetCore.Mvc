namespace MyTested.Mvc.Test.BuildersTests.ActionsTests.ShouldReturnTests
{
    using Exceptions;
    using Setups;
    using Setups.Controllers;
    using Xunit;

    public class ShouldReturnChallengeTests
    {
        [Fact]
        public void ShouldReturnChallengeShouldNotThrowExceptionIfResultIsChallenge()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.ChallengeResultAction())
                .ShouldReturn()
                .Challenge();
        }

        [Fact]
        public void ShouldReturnChallengeShouldThrowExceptionIfResultIsNotChallenge()
        {
            Test.AssertException<ActionResultAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.BadRequestAction())
                        .ShouldReturn()
                        .Challenge();
                },
                "When calling BadRequestAction action in MvcController expected action result to be ChallengeResult, but instead received BadRequestResult.");
        }
    }
}
