namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.AndTests
{
    using System;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using Setups.Controllers;
    using Xunit;

    public class AndProvideTestBuilderTests
    {
        [Fact]
        public void AndProvideShouldReturnProperController()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.BadRequestWithErrorAction())
                .ShouldReturn()
                .BadRequest()
                .AndAlso()
                .ShouldPassForThe<MvcController>(controller =>
                {
                    Assert.NotNull(controller);
                    Assert.IsAssignableFrom<MvcController>(controller);
                });
        }
        
        [Fact]
        public void AndProvideTheControllerAttributesShouldReturnProperAttributes()
        {
            MyController<MvcController>
                .ShouldHave()
                .Attributes()
                .ShouldPassForThe<ControllerAttributes>(attributes =>
                {
                    Assert.Equal(4, attributes.Count());
                });
        }
        
        [Fact]
        public void AndProvideShouldReturnProperActionAttributes()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.VariousAttributesAction())
                .ShouldHave()
                .ActionAttributes()
                .AndAlso()
                .ShouldPassForThe<ActionAttributes>(attributes =>
                {
                    Assert.Equal(9, attributes.Count());
                });
        }

        [Fact]
        public void AndProvideShouldReturnProperActionResult()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.LocalRedirect("URL"))
                .ShouldReturn()
                .LocalRedirect()
                .ShouldPassForThe<IActionResult>(actionResult =>
                {
                    Assert.NotNull(actionResult);
                    Assert.IsAssignableFrom<LocalRedirectResult>(actionResult);
                });
        }

        [Fact]
        public void AndProvideShouldReturnProperCaughtException()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.ActionWithException())
                .ShouldThrow()
                .Exception()
                .ShouldPassForThe<Exception>(caughtException =>
                {
                    Assert.NotNull(caughtException);
                    Assert.IsAssignableFrom<NullReferenceException>(caughtException);
                });            
        }
    }
}
