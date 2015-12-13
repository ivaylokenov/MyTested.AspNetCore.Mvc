namespace MyTested.Mvc.Tests.BuildersTests.AndTests
{
    using System;
    using System.Linq;
    using Setups;
    using Setups.Controllers;
    using Xunit;
    
    public class AndProvideTestBuilderTests
    {
        [Fact]
        public void AndProvideShouldReturnProperController()
        {
            // TODO: HttpBadRequest builder?
            //var controller = MyMvc
            //    .Controller<MvcController>()
            //    .Calling(c => c.BadRequestWithErrorAction())
            //    .ShouldReturn()
            //    .HttpBadRequest()
            //    .WithErrorMessage()
            //    .AndProvideTheController();

            //Assert.NotNull(controller);
            //Assert.IsAssignableFrom<MvcController>(controller);
        }

        [Fact]
        public void AndProvideShouldReturnProperHttpRequestMessage()
        {
            // TODO: HttpRequest builder
            //var httpRequestMessage = MyMvc
            //    .Controller<MvcController>()
            //    .WithHttpRequestMessage(request
            //        => request
            //            .WithMethod(HttpMethod.Get)
            //            .WithHeader("TestHeader", "TestHeaderValue"))
            //    .Calling(c => c.HttpResponseMessageAction())
            //    .ShouldReturn()
            //    .HttpResponseMessage()
            //    .AndProvideTheHttpRequestMessage();

            //Assert.NotNull(httpRequestMessage);
            //Assert.Equal(HttpMethod.Get, httpRequestMessage.Method);
            //Assert.True(httpRequestMessage.Headers.Contains("TestHeader"));
        }

        [Fact]
        public void AndProvideShouldReturnProperHttpConfiguration()
        {
            // TODO: http config/context
            //var config = TestObjectFactory.GetHttpConfigurationWithRoutes();

            //var actualConfig = MyMvc
            //    .Controller<MvcController>()
            //    .WithHttpConfiguration(config)
            //    .Calling(c => c.OkResultAction())
            //    .ShouldReturn()
            //    .Ok()
            //    .AndProvideTheHttpConfiguration();

            //Assert.AreSame(config, actualConfig);
        }

        [Fact]
        public void AndProvideTheControllerAttributesShouldReturnProperAttributes()
        {
            var attributes = MyMvc
                .Controller<MvcController>()
                .ShouldHave()
                .Attributes()
                .AndProvideTheControllerAttributes();

            Assert.Equal(2, attributes.Count());
        }

        [Fact]
        public void AndProvideShouldReturnProperActionName()
        {
            // TODO: badrequest builder?
            //var actionName = MyMvc
            //    .Controller<MvcController>()
            //    .Calling(c => c.BadRequestWithErrorAction())
            //    .ShouldReturn()
            //    .HttpBadRequest()
            //    .WithErrorMessage()
            //    .AndProvideTheActionName();

            //Assert.Equal("BadRequestWithErrorAction", actionName);
        }

        [Fact]
        public void AndProvideShouldReturnProperActionAttributes()
        {
            var attributes = MyMvc
                .Controller<MvcController>()
                .Calling(c => c.VariousAttributesAction())
                .ShouldHave()
                .ActionAttributes()
                .AndProvideTheActionAttributes();

            Assert.Equal(6, attributes.Count());
        }

        [Fact]
        public void AndProvideShouldReturnProperActionResult()
        {
            // TODO: examine?
            //var actionResult = MyMvc
            //    .Controller<MvcController>()
            //    .Calling(c => c.OkResultAction())
            //    .ShouldReturn()
            //    .Ok()
            //    .AndProvideTheActionResult();

            //Assert.NotNull(actionResult);
            //Assert.IsAssignableFrom<StatusCodeResult>(actionResult);
        }

        [Fact]
        public void AndProvideShouldReturnProperCaughtException()
        {
            var caughtException = MyMvc
                .Controller<MvcController>()
                .Calling(c => c.ActionWithException())
                .ShouldThrow()
                .Exception()
                .AndProvideTheCaughtException();

            Assert.NotNull(caughtException);
            Assert.IsAssignableFrom<NullReferenceException>(caughtException);
        }

        [Fact]
        public void AndProvideShouldThrowExceptionIfActionIsVoid()
        {
            Test.AssertException<InvalidOperationException>(() =>
            {
                MyMvc
                    .Controller<MvcController>()
                    .Calling(c => c.EmptyActionWithException())
                    .ShouldHave()
                    .ValidModelState()
                    .AndProvideTheActionResult();
            }, "Void methods cannot provide action result because they do not have return value.");
        }
    }
}
