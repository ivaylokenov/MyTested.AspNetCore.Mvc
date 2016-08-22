namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.ShouldPassForTests
{
    using Exceptions;
    using Microsoft.AspNetCore.Mvc;
    using Setups;
    using Setups.Controllers;
    using Xunit;

    public class ShouldPassForTestBuilderWithActionResultTests
    {
        [Fact]
        public void ActionResultAssertionsShouldWorkCorrectly()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.FullOkAction())
                .ShouldReturn()
                .Ok()
                .ShouldPassForThe<OkObjectResult>(actionResult =>
                {
                    Assert.NotNull(actionResult);
                });
        }

        [Fact]
        public void ActionResultPredicateShouldWorkCorrectly()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.FullOkAction())
                .ShouldReturn()
                .Ok()
                .ShouldPassForThe<OkObjectResult>(actionResult => actionResult != null);
        }

        [Fact]
        public void ActionResultPredicateShouldThrowExceptionWithInvalidTest()
        {
            Test.AssertException<InvalidAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.FullOkAction())
                        .ShouldReturn()
                        .Ok()
                        .ShouldPassForThe<OkObjectResult>(actionResult => actionResult == null);
                },
                "Expected OkObjectResult to pass the given predicate but it failed.");
        }
    }
}
