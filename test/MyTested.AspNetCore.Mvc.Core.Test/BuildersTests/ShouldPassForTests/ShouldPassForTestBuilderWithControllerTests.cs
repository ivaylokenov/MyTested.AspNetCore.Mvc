namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.ShouldPassForTests
{
    using System.Linq;
    using Exceptions;
    using Setups;
    using Setups.Controllers;
    using Xunit;

    public class ShouldPassForTestBuilderWithControllerTests
    {
        [Fact]
        public void ControllerAssertionsShouldWorkCorrectly()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.FullOkAction())
                .ShouldReturn()
                .Ok()
                .ShouldPassFor()
                .TheController(controller =>
                {
                    Assert.NotNull(controller);
                });
        }

        [Fact]
        public void ControllerPredicateShouldWorkCorrectly()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.FullOkAction())
                .ShouldPassFor()
                .TheController(controller => controller != null);
        }

        [Fact]
        public void ControllerPredicateShouldThrowExceptionWithInvalidTest()
        {
            Test.AssertException<InvalidAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.FullOkAction())
                        .ShouldPassFor()
                        .TheController(controller => controller == null);
                },
                "Expected the MvcController to pass the given predicate but it failed.");
        }

        [Fact]
        public void ControllerAttributesAssertionsShouldWorkCorrectly()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.FullOkAction())
                .ShouldReturn()
                .Ok()
                .ShouldPassFor()
                .TheControllerAttributes(attributes =>
                {
                    Assert.Equal(2, attributes.Count());
                });
        }

        [Fact]
        public void ControllerAttributesPredicateShouldWorkCorrectly()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.FullOkAction())
                .ShouldPassFor()
                .TheControllerAttributes(attributes => attributes.Count() == 2);
        }

        [Fact]
        public void ControllerAttributesPredicateShouldThrowExceptionWithInvalidTest()
        {
            Test.AssertException<InvalidAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.FullOkAction())
                        .ShouldPassFor()
                        .TheControllerAttributes(attributes => attributes.Count() == 3);
                },
                "Expected the controller attributes to pass the given predicate but it failed.");
        }
    }
}
