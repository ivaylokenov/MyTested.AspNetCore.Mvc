namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.ShouldPassForTests
{
    using System;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ViewComponents;
    using Setups;
    using Setups.ViewComponents;
    using Xunit;
    using Exceptions;

    public class ShouldPassForTests
    {
        [Fact]
        public void AndProvideShouldReturnProperController()
        {
            MyViewComponent<NormalComponent>
                .Instance()
                .InvokedWith(c => c.Invoke())
                .ShouldReturn()
                .Content("Test")
                .ShouldPassForThe<NormalComponent>(viewComponent =>
                {
                    Assert.NotNull(viewComponent);
                    Assert.IsAssignableFrom<NormalComponent>(viewComponent);
                });
        }

        [Fact]
        public void AndProvideTheControllerAttributesShouldReturnProperAttributes()
        {
            MyViewComponent<AttributesComponent>
                .ShouldHave()
                .Attributes()
                .ShouldPassForThe<ViewComponentAttributes>(attributes =>
                {
                    Assert.Equal(2, attributes.Count());
                });
        }
        
        [Fact]
        public void AndProvideShouldReturnProperActionResult()
        {
            MyViewComponent<NormalComponent>
                .Instance()
                .InvokedWith(c => c.Invoke())
                .ShouldReturn()
                .Content("Test")
                .ShouldPassForThe<IViewComponentResult>(viewComponentResult =>
                {
                    Assert.NotNull(viewComponentResult);
                    Assert.IsAssignableFrom<ContentViewComponentResult>(viewComponentResult);
                });
        }

        [Fact]
        public void AndProvideShouldReturnProperCaughtException()
        {
            MyViewComponent<ExceptionComponent>
                .Instance()
                .InvokedWith(c => c.Invoke())
                .ShouldThrow()
                .Exception()
                .ShouldPassForThe<Exception>(caughtException =>
                {
                    Assert.NotNull(caughtException);
                    Assert.IsAssignableFrom<IndexOutOfRangeException>(caughtException);
                });
        }

        [Fact]
        public void ShouldPassForShouldThrowExceptionWithInvalidTest()
        {
            Test.AssertException<InvalidAssertionException>(
                () =>
                {
                    MyViewComponent<NormalComponent>
                        .Instance()
                        .InvokedWith(c => c.Invoke())
                        .ShouldPassForThe<ViewComponentAttributes>(attributes => attributes.Count() == 4);
                },
                "Expected ViewComponentAttributes to pass the given predicate but it failed.");
        }
    }
}
