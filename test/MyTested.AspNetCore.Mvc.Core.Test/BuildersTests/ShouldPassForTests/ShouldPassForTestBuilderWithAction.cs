namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.ShouldPassForTests
{
    using System.Linq;
    using Exceptions;
    using Setups;
    using Setups.Controllers;
    using Xunit;

    public class ShouldPassForTestBuilderWithAction
    {
        [Fact]
        public void ActionAssertionsShouldWorkCorrectly()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.FullOkAction())
                .ShouldPassFor()
                .TheAction(action =>
                {
                    Assert.Equal("FullOkAction", action);
                });
        }

        [Fact]
        public void ActionPredicateShouldWorkCorrectly()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.FullOkAction())
                .ShouldPassFor()
                .TheAction(action => action == "FullOkAction");
        }

        [Fact]
        public void ActionPredicateShouldThrowExceptionWithInvalidTest()
        {
            Test.AssertException<InvalidAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.FullOkAction())
                        .ShouldPassFor()
                        .TheAction(action => action == "Invalid");
                },
                "Expected the action name to pass the given predicate but it failed.");
        }

        [Fact]
        public void ActionAttributesAssertionsShouldWorkCorrectly()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.NormalActionWithAttributes())
                .ShouldPassFor()
                .TheActionAttributes(attributes =>
                {
                    Assert.Equal(3, attributes.Count());
                });
        }

        [Fact]
        public void ActionAttributesPredicateShouldWorkCorrectly()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.NormalActionWithAttributes())
                .ShouldPassFor()
                .TheActionAttributes(attributes => attributes.Count() == 3);
        }

        [Fact]
        public void ActionAttributesPredicateShouldThrowExceptionWithInvalidTest()
        {
            Test.AssertException<InvalidAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.NormalActionWithAttributes())
                        .ShouldPassFor()
                        .TheActionAttributes(attributes => attributes.Count() == 4);
                },
                "Expected the action attributes to pass the given predicate but it failed.");
        }
    }
}
