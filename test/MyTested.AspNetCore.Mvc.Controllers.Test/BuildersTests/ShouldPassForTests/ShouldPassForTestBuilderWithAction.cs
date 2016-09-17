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
        public void ActionAttributesAssertionsShouldWorkCorrectly()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.NormalActionWithAttributes())
                .ShouldPassForThe<ActionAttributes>(attributes =>
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
                .ShouldPassForThe<ActionAttributes>(attributes => attributes.Count() == 3);
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
                        .ShouldPassForThe<ActionAttributes>(attributes => attributes.Count() == 4);
                },
                "Expected ActionAttributes to pass the given predicate but it failed.");
        }
    }
}
