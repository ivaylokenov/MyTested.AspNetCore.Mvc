namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.ActionResultsTests.ContentTests
{
    using System.Net;
    using Exceptions;
    using Microsoft.Net.Http.Headers;
    using Setups;
    using Setups.Controllers;
    using Utilities;
    using Xunit;

    public class ContentTestBuilderTests
    {
        [Fact]
        public void WithStatusCodeAsIntShouldNotThrowExceptionWhenActionReturnsCorrectStatusCode()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.ContentAction())
                .ShouldReturn()
                .Content(result => result
                    .WithContent("content")
                    .WithStatusCode(200));
        }

        [Fact]
        public void WithStatusCodeShouldNotThrowExceptionWhenActionReturnsCorrectStatusCode()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.ContentAction())
                .ShouldReturn()
                .Content(result => result
                    .WithContent("content")
                    .WithStatusCode(HttpStatusCode.OK));
        }

        [Fact]
        public void WithStatusCodeShouldThrowExceptionWhenActionReturnsWrongStatusCode()
        {
            Test.AssertException<ContentResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.ContentAction())
                        .ShouldReturn()
                        .Content(result => result
                            .WithContent("content")
                            .WithStatusCode(HttpStatusCode.NotFound));
                },
                "When calling ContentAction action in MvcController expected content result to have 404 (NotFound) status code, but instead received 200 (OK).");
        }

        [Fact]
        public void WithMediaTypeShouldNotThrowExceptionWithString()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.ContentActionWithMediaType())
                .ShouldReturn()
                .Content(result => result
                    .WithContent("content")
                    .WithContentType(ContentType.TextPlain));
        }

        [Fact]
        public void WithMediaTypeShouldNotThrowExceptionWithMediaTypeHeaderValue()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.ContentActionWithMediaType())
                .ShouldReturn()
                .Content(content => content
                    .WithContentType(new MediaTypeHeaderValue(ContentType.TextPlain)));
        }

        [Fact]
        public void WithMediaTypeShouldNotThrowExceptionWithMediaTypeHeaderValueConstant()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.ContentActionWithMediaType())
                .ShouldReturn()
                .Content(content => content
                    .WithContentType(ContentType.TextPlain));
        }

        [Fact]
        public void WithMediaTypeShouldThrowExceptionWithMediaTypeHeaderValue()
        {
            Test.AssertException<ContentResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.ContentActionWithMediaType())
                        .ShouldReturn()
                        .Content(content => content
                            .WithContentType(new MediaTypeHeaderValue(ContentType.ApplicationJson)));
                },
                "When calling ContentActionWithMediaType action in MvcController expected content result ContentType to be 'application/json', but instead received 'text/plain'.");
        }

        [Fact]
        public void WithMediaTypeShouldThrowExceptionWithNullMediaTypeHeaderValue()
        {
            Test.AssertException<ContentResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.ContentActionWithMediaType())
                        .ShouldReturn()
                        .Content(content => content
                            .WithContentType((MediaTypeHeaderValue)null));
                },
                "When calling ContentActionWithMediaType action in MvcController expected content result ContentType to be null, but instead received 'text/plain'.");
        }

        [Fact]
        public void WithMediaTypeShouldThrowExceptionWithNullMediaTypeHeaderValueAndNullActual()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.ContentActionWithNullMediaType())
                .ShouldReturn()
                .Content(content => content
                    .WithContentType((MediaTypeHeaderValue)null));
        }

        [Fact]
        public void WithMediaTypeShouldThrowExceptionWithMediaTypeHeaderValueAndNullActual()
        {
            Test.AssertException<ContentResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.ContentActionWithNullMediaType())
                        .ShouldReturn()
                        .Content(content => content
                            .WithContentType(new MediaTypeHeaderValue(TestObjectFactory.MediaType)));
                },
                "When calling ContentActionWithNullMediaType action in MvcController expected content result ContentType to be 'application/json', but instead received null.");
        }

        [Fact]
        public void AndAlsoShouldWorkCorrectly()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.ContentAction())
                .ShouldReturn()
                .Content(result => result
                    .WithContent("content")
                    .WithStatusCode(HttpStatusCode.OK)
                    .AndAlso()
                    .WithContentType(ContentType.ApplicationJson));
        }


        [Fact]
        public void PassingShouldCorrectlyRunItsAssertionFunction()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.ContentAction())
                .ShouldReturn()
                .Content(content => content
                    .Passing(c => c.Content == "content"));
        }

        [Fact]
        public void PassingShouldThrowAnExceptionOnAnIncorrectAssertion()
        {
            Test.AssertException<InvocationResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.ContentAction())
                        .ShouldReturn()
                        .Content(content => content
                            .Passing(c => c.Content == string.Empty));
                },
                $"When calling ContentAction action in MvcController expected the ContentResult to pass the given predicate, but it failed.");
        }

        [Fact]
        public void PassingShouldCorrectlyRunItsAssertionAction()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.ContentAction())
                .ShouldReturn()
                .Content(content => content
                    .Passing(c =>
                    {
                        const string expectedContentBody = "content";
                        var actualContentBody = c.Content;
                        if (actualContentBody != expectedContentBody)
                        {
                            throw new InvalidAssertionException(
                                string.Format("Expected {0} to have {1} equal to content, but it was {2}.",
                                    c.GetType().ToFriendlyTypeName(),
                                    nameof(c.Content),
                                    actualContentBody));
                        };
                    }));
        }


    }
}
