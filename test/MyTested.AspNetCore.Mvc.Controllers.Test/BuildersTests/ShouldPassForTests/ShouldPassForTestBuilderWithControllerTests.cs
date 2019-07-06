namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.ShouldPassForTests
{
    using System.Linq;
    using Exceptions;
    using Setups;
    using Setups.Controllers;
    using Xunit;
    using System;

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
                .ShouldPassForThe<MvcController>(controller =>
                {
                    Assert.NotNull(controller);
                });
        }

        [Fact]
        public void ControllerAssertionsShouldWorkCorrectlyAndThrowExceptionIfIncorrect()
        {
            Assert.ThrowsAny<Exception>(() =>
            {
                MyController<MvcController>
                    .Instance()
                    .Calling(c => c.FullOkAction())
                    .ShouldReturn()
                    .Ok()
                    .ShouldPassForThe<MvcController>(controller =>
                    {
                        Assert.Null(controller);
                    });
            });
        }

        [Fact]
        public void ControllerPredicateShouldWorkCorrectly()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.FullOkAction())
                .ShouldPassForThe<MvcController>(controller => controller != null);
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
                        .ShouldPassForThe<MvcController>(controller => controller == null);
                },
                "Expected MvcController to pass the given predicate but it failed.");
        }

        [Fact]
        public void ControllerAttributesAssertionsShouldWorkCorrectly()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.FullOkAction())
                .ShouldReturn()
                .Ok()
                .ShouldPassForThe<ControllerAttributes>(attributes =>
                {
                    Assert.Equal(4, attributes.Count());
                });
        }

        [Fact]
        public void ControllerAttributesPredicateShouldWorkCorrectly()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.FullOkAction())
                .ShouldPassForThe<ControllerAttributes>(attributes => attributes.Count() == 4);
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
                        .ShouldPassForThe<ControllerAttributes>(attributes => attributes.Count() == 3);
                },
                "Expected ControllerAttributes to pass the given predicate but it failed.");
        }
    }
}
