namespace MyTested.Mvc.Test.BuildersTests.ActionResultsTests.ContentTests
{
    using System.Net;
    using Exceptions;
    using Microsoft.Net.Http.Headers;
    using Setups;
    using Setups.Controllers;
    using Xunit;

    public class ContentTestBuilderTests
    {
        [Fact]
        public void WithStatusCodeAsIntShouldNotThrowExceptionWhenActionReturnsCorrectStatusCode()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.ContentAction())
                .ShouldReturn()
                .Content("content")
                .WithStatusCode(200);
        }

        [Fact]
        public void WithStatusCodeShouldNotThrowExceptionWhenActionReturnsCorrectStatusCode()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.ContentAction())
                .ShouldReturn()
                .Content("content")
                .WithStatusCode(HttpStatusCode.OK);
        }

        [Fact]
        public void WithStatusCodeShouldThrowExceptionWhenActionReturnsWrongStatusCode()
        {
            Test.AssertException<ContentResultAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.ContentAction())
                        .ShouldReturn()
                        .Content("content")
                        .WithStatusCode(HttpStatusCode.NotFound);
                },
                "When calling ContentAction action in MvcController expected content result to have 404 (NotFound) status code, but instead received 200 (OK).");
        }

        [Fact]
        public void WithMediaTypeShouldNotThrowExceptionWithString()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.ContentActionWithMediaType())
                .ShouldReturn()
                .Content("content")
                .WithContentType(ContentType.TextPlain);
        }

        [Fact]
        public void WithMediaTypeShouldNotThrowExceptionWithMediaTypeHeaderValue()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.ContentActionWithMediaType())
                .ShouldReturn()
                .Content()
                .WithContentType(new MediaTypeHeaderValue(ContentType.TextPlain));
        }

        [Fact]
        public void WithMediaTypeShouldNotThrowExceptionWithMediaTypeHeaderValueConstant()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.ContentActionWithMediaType())
                .ShouldReturn()
                .Content()
                .WithContentType(ContentType.TextPlain);
        }

        [Fact]
        public void WithMediaTypeShouldThrowExceptionWithMediaTypeHeaderValue()
        {
            Test.AssertException<ContentResultAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.ContentActionWithMediaType())
                        .ShouldReturn()
                        .Content()
                        .WithContentType(new MediaTypeHeaderValue(ContentType.ApplicationJson));
                },
                "When calling ContentActionWithMediaType action in MvcController expected content result ContentType to be 'application/json', but instead received 'text/plain'.");
        }

        [Fact]
        public void WithMediaTypeShouldThrowExceptionWithNullMediaTypeHeaderValue()
        {
            Test.AssertException<ContentResultAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.ContentActionWithMediaType())
                        .ShouldReturn()
                        .Content()
                        .WithContentType((MediaTypeHeaderValue)null);
                }, 
                "When calling ContentActionWithMediaType action in MvcController expected content result ContentType to be null, but instead received 'text/plain'.");
        }

        [Fact]
        public void WithMediaTypeShouldThrowExceptionWithNullMediaTypeHeaderValueAndNullActual()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.ContentActionWithNullMediaType())
                .ShouldReturn()
                .Content()
                .WithContentType((MediaTypeHeaderValue)null);
        }

        [Fact]
        public void WithMediaTypeShouldThrowExceptionWithMediaTypeHeaderValueAndNullActual()
        {
            Test.AssertException<ContentResultAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.ContentActionWithNullMediaType())
                        .ShouldReturn()
                        .Content()
                        .WithContentType(new MediaTypeHeaderValue(TestObjectFactory.MediaType));
                }, 
                "When calling ContentActionWithNullMediaType action in MvcController expected content result ContentType to be 'application/json', but instead received null.");
        }
        
        [Fact]
        public void AndAlsoShouldWorkCorrectly()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.ContentAction())
                .ShouldReturn()
                .Content("content")
                .WithStatusCode(HttpStatusCode.OK)
                .AndAlso()
                .WithContentType(ContentType.ApplicationJson);
        }
    }
}
