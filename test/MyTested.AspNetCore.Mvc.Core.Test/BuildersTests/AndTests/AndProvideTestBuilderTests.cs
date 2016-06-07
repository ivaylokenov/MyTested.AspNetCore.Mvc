namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.AndTests
{
    using System;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using Setups;
    using Setups.Controllers;
    using Xunit;

    public class AndProvideTestBuilderTests
    {
        [Fact]
        public void AndProvideShouldReturnProperController()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.BadRequestWithErrorAction())
                .ShouldReturn()
                .BadRequest()
                .WithErrorMessage()
                .ShouldPassFor()
                .TheController(controller =>
                {
                    Assert.NotNull(controller);
                    Assert.IsAssignableFrom<MvcController>(controller);
                });
        }
        
        [Fact]
        public void AndProvideTheControllerAttributesShouldReturnProperAttributes()
        {
            MyMvc
                .Controller<MvcController>()
                .ShouldHave()
                .Attributes()
                .ShouldPassFor()
                .TheControllerAttributes(attributes =>
                {
                    Assert.Equal(2, attributes.Count());
                });
        }

        [Fact]
        public void AndProvideShouldReturnProperActionName()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.BadRequestWithErrorAction())
                .ShouldReturn()
                .BadRequest()
                .WithErrorMessage()
                .ShouldPassFor()
                .TheAction(actionName =>
                {
                    Assert.Equal("BadRequestWithErrorAction", actionName);
                });
        }

        [Fact]
        public void AndProvideShouldReturnProperActionAttributes()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.VariousAttributesAction())
                .ShouldHave()
                .ActionAttributes()
                .ShouldPassFor()
                .TheActionAttributes(attributes =>
                {
                    Assert.Equal(6, attributes.Count());
                });
        }

        [Fact]
        public void AndProvideShouldReturnProperActionResult()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.LocalRedirect("URL"))
                .ShouldReturn()
                .LocalRedirect()
                .ShouldPassFor()
                .TheActionResult(actionResult =>
                {
                    Assert.NotNull(actionResult);
                    Assert.IsAssignableFrom<LocalRedirectResult>(actionResult);
                });
        }

        [Fact]
        public void AndProvideShouldReturnProperCaughtException()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.ActionWithException())
                .ShouldThrow()
                .Exception()
                .ShouldPassFor()
                .TheCaughtException(caughtException =>
                {
                    Assert.NotNull(caughtException);
                    Assert.IsAssignableFrom<NullReferenceException>(caughtException);
                });            
        }
    }
}
