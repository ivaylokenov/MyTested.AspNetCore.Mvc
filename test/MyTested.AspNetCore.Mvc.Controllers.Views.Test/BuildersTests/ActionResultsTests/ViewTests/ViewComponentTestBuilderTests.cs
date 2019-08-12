namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.ActionResultsTests.ViewTests
{
    using System.Net;
    using Exceptions;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Net.Http.Headers;
    using Setups;
    using Setups.Controllers;
    using Xunit;

    public class ViewComponentTestBuilderTests
    {
        [Fact]
        public void WithStatusCodeAsIntShouldNotThrowExceptionWhenActionReturnsCorrectStatusCode()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.CustomViewComponentResult())
                .ShouldReturn()
                .ViewComponent(viewComponent => viewComponent
                    .WithStatusCode(500));
        }

        [Fact]
        public void WithStatusCodeShouldNotThrowExceptionWhenActionReturnsCorrectStatusCode()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.CustomViewComponentResult())
                .ShouldReturn()
                .ViewComponent(viewComponent => viewComponent
                    .WithStatusCode(HttpStatusCode.InternalServerError));
        }

        [Fact]
        public void WithStatusCodeShouldThrowExceptionWhenActionReturnsWrongStatusCode()
        {
            Test.AssertException<ViewResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.CustomViewComponentResult())
                        .ShouldReturn()
                        .ViewComponent(viewComponent => viewComponent
                            .WithStatusCode(HttpStatusCode.NotFound));
                },
                "When calling CustomViewComponentResult action in MvcController expected view component result to have 404 (NotFound) status code, but instead received 500 (InternalServerError).");
        }

        [Fact]
        public void WithMediaTypeShouldNotThrowExceptionWithString()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.CustomViewComponentResult())
                .ShouldReturn()
                .ViewComponent(viewComponent => viewComponent
                    .WithContentType(ContentType.ApplicationXml));
        }

        [Fact]
        public void WithMediaTypeShouldNotThrowExceptionWithMediaTypeHeaderValue()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.CustomViewComponentResult())
                .ShouldReturn()
                .ViewComponent(viewComponent => viewComponent
                    .WithContentType(new MediaTypeHeaderValue(ContentType.ApplicationXml)));
        }

        [Fact]
        public void WithMediaTypeShouldNotThrowExceptionWithMediaTypeHeaderValueConstant()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.CustomViewComponentResult())
                .ShouldReturn()
                .ViewComponent(viewComponent => viewComponent
                    .WithContentType(ContentType.ApplicationXml));
        }

        [Fact]
        public void WithMediaTypeShouldThrowExceptionWithMediaTypeHeaderValue()
        {
            Test.AssertException<ViewResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.CustomViewComponentResult())
                        .ShouldReturn()
                        .ViewComponent(viewComponent => viewComponent
                            .WithContentType(new MediaTypeHeaderValue(ContentType.ApplicationJson)));
                },
                "When calling CustomViewComponentResult action in MvcController expected view component result ContentType to be 'application/json', but instead received 'application/xml'.");
        }

        [Fact]
        public void WithMediaTypeShouldThrowExceptionWithNullMediaTypeHeaderValue()
        {
            Test.AssertException<ViewResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.CustomViewComponentResult())
                        .ShouldReturn()
                        .ViewComponent(viewComponent => viewComponent
                            .WithContentType((MediaTypeHeaderValue)null));
                },
                "When calling CustomViewComponentResult action in MvcController expected view component result ContentType to be null, but instead received 'application/xml'.");
        }

        [Fact]
        public void WithMediaTypeShouldThrowExceptionWithNullMediaTypeHeaderValueAndNullActual()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.ViewComponentResultByName())
                .ShouldReturn()
                .ViewComponent(viewComponent => viewComponent
                    .WithContentType((MediaTypeHeaderValue)null));
        }

        [Fact]
        public void WithMediaTypeShouldThrowExceptionWithMediaTypeHeaderValueAndNullActual()
        {
            Test.AssertException<ViewResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.ViewComponentResultByName())
                        .ShouldReturn()
                        .ViewComponent(viewComponent => viewComponent
                            .WithContentType(new MediaTypeHeaderValue(TestObjectFactory.MediaType)));
                },
                "When calling ViewComponentResultByName action in MvcController expected view component result ContentType to be 'application/json', but instead received null.");
        }

        [Fact]
        public void AndAlsoShouldWorkCorrectly()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.CustomViewComponentResult())
                .ShouldReturn()
                .ViewComponent(viewComponent => viewComponent
                    .WithContentType(ContentType.ApplicationXml)
                    .AndAlso()
                    .WithStatusCode(500));
        }

        [Fact]
        public void AndProvideTheActionResultShouldWorkCorrectly()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.ViewComponentResultByName())
                .ShouldReturn()
                .ViewComponent()
                .AndAlso()
                .ShouldPassForThe<IActionResult>(actionResult =>
                {
                    Assert.NotNull(actionResult);
                    Assert.IsAssignableFrom<ViewComponentResult>(actionResult);
                });
        }

        [Fact]
        public void WithNoModelShouldNotThrowExceptionWhenNoModelIsReturned()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.ViewComponentResultByType())
                .ShouldReturn()
                .ViewComponent(viewComponent =>
                    viewComponent.WithNoModel()); 
            // ViewComponent is not settable, not sure if this is a valid case.
        }
    }
}
