namespace MyTested.Mvc.Test.BuildersTests.ShouldPassForTests
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
            MyMvc
                .Controller<MvcController>()
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
            MyMvc
                .Controller<MvcController>()
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
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.FullOkAction())
                        .ShouldPassFor()
                        .TheController(controller => controller == null);
                },
                "Expected the MvcController to pass the given predicate but it failed.");
        }

        [Fact]
        public void ControllerAttributesAssertionsShouldWorkCorrectly()
        {
            MyMvc
                .Controller<MvcController>()
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
            MyMvc
                .Controller<MvcController>()
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
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.FullOkAction())
                        .ShouldPassFor()
                        .TheControllerAttributes(attributes => attributes.Count() == 3);
                },
                "Expected the controller attributes to pass the given predicate but it failed.");
        }
    }
}
