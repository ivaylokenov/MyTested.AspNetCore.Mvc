namespace MyTested.Mvc.Test.BuildersTests.ShouldPassForTests
{
    using Exceptions;
    using Setups;
    using Setups.Controllers;
    using Xunit;

    public class ShouldPassForTestBuilderWithActionResultTests
    {
        [Fact]
        public void ActionResultAssertionsShouldWorkCorrectly()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.FullOkAction())
                .ShouldReturn()
                .Ok()
                .ShouldPassFor()
                .TheActionResult(actionResult =>
                {
                    Assert.NotNull(actionResult);
                });
        }

        [Fact]
        public void ActionResultPredicateShouldWorkCorrectly()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.FullOkAction())
                .ShouldPassFor()
                .TheActionResult(actionResult => actionResult != null);
        }

        [Fact]
        public void ActionResultPredicateShouldThrowExceptionWithInvalidTest()
        {
            Test.AssertException<InvalidAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.FullOkAction())
                        .ShouldPassFor()
                        .TheActionResult(actionResult => actionResult == null);
                },
                "Expected the OkObjectResult to pass the given predicate but it failed.");
        }
    }
}
