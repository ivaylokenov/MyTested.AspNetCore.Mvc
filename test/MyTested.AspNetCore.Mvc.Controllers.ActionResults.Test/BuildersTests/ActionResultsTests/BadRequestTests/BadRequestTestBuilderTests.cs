namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.ActionResultsTests.BadRequestTests
{
    using System.Collections.Generic;
    using System.Net;
    using Exceptions;
    using Microsoft.AspNetCore.Mvc.Formatters;
    using Microsoft.Net.Http.Headers;
    using Setups;
    using Setups.Common;
    using Setups.Controllers;
    using Utilities;
    using Xunit;

    public class BadRequestTestBuilderTests
    {
        [Fact]
        public void WithNoErrorShouldNotThrowExceptionWithNoError()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.BadRequestAction())
                .ShouldReturn()
                .BadRequest(badRequest => badRequest
                    .WithNoError());
        }

        [Fact]
        public void WithNoErrorShouldThrowExceptionWithError()
        {
            Test.AssertException<BadRequestResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.BadRequestWithCustomError())
                        .ShouldReturn()
                        .BadRequest(badRequest => badRequest
                            .WithNoError());
                },
                "When calling BadRequestWithCustomError action in MvcController expected bad request result to not have error message, but in fact such was found.");
        }

        [Fact]
        public void WithErrorMessageShouldNotThrowExceptionWhenResultHasErrorMessage()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.BadRequestWithErrorAction())
                .ShouldReturn()
                .BadRequest(badRequest => badRequest
                    .WithErrorMessage());
        }

        [Fact]
        public void WithErrorMessageShouldThrowExceptionWhenResultDoesNotHaveErrorMessage()
        {
            Test.AssertException<InvocationResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.BadRequestAction())
                        .ShouldReturn()
                        .BadRequest(badRequest => badRequest
                            .WithErrorMessage());
                },
                "When calling BadRequestAction action in MvcController expected action result to inherit from ObjectResult and have response data, but it did not.");
        }

        [Fact]
        public void WithErrorMessageShouldNotThrowExceptionWhenResultHasCorrectErrorMessage()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.BadRequestWithErrorAction())
                .ShouldReturn()
                .BadRequest(badRequest => badRequest
                    .WithErrorMessage("Bad request"));
        }

        [Fact]
        public void WithErrorMessageShouldThrowExceptionWhenResultDoesNotHaveCorrectErrorMessage()
        {
            Test.AssertException<BadRequestResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.BadRequestWithErrorAction())
                        .ShouldReturn()
                        .BadRequest(badRequest => badRequest
                            .WithErrorMessage("Good request"));
                }, 
                "When calling BadRequestWithErrorAction action in MvcController expected bad request result with message 'Good request', but instead received 'Bad request'.");
        }

        [Fact]
        public void WithStatusCodeShouldNotThrowExceptionWithCorrectStatusCode()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.FullHttpBadRequestAction())
                .ShouldReturn()
                .BadRequest(badRequest => badRequest
                    .WithStatusCode(201));
        }

        [Fact]
        public void WithStatusCodeAsEnumShouldNotThrowExceptionWithCorrectStatusCode()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.FullHttpBadRequestAction())
                .ShouldReturn()
                .BadRequest(badRequest => badRequest
                    .WithStatusCode(HttpStatusCode.Created));
        }

        [Fact]
        public void WithStatusCodeAsEnumShouldThrowExceptionWithIncorrectStatusCode()
        {
            Test.AssertException<BadRequestResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.FullHttpBadRequestAction())
                        .ShouldReturn()
                        .BadRequest(badRequest => badRequest
                            .WithStatusCode(HttpStatusCode.OK));
                },
                "When calling FullHttpBadRequestAction action in MvcController expected bad request result to have 200 (OK) status code, but instead received 201 (Created).");
        }

        [Fact]
        public void ContainingContentTypeShouldNotThrowExceptionWithCorrectValue()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.FullHttpBadRequestAction())
                .ShouldReturn()
                .BadRequest(badRequest => badRequest
                    .ContainingContentType(ContentType.ApplicationJson));
        }

        [Fact]
        public void ContainingContentTypeAsMediaTypeHeaderValueShouldNotThrowExceptionWithCorrectValue()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.FullHttpBadRequestAction())
                .ShouldReturn()
                .BadRequest(badRequest => badRequest
                    .ContainingContentType(new MediaTypeHeaderValue(ContentType.ApplicationJson)));
        }
        
        [Fact]
        public void ContainingContentTypeAsMediaTypeHeaderValueShouldThrowExceptionWithIncorrectValue()
        {
            Test.AssertException<BadRequestResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.FullHttpBadRequestAction())
                        .ShouldReturn()
                        .BadRequest(badRequest => badRequest
                            .ContainingContentType(new MediaTypeHeaderValue(ContentType.ApplicationOctetStream)));
                },
                "When calling FullHttpBadRequestAction action in MvcController expected bad request result content types to contain application/octet-stream, but in fact such was not found.");
        }

        [Fact]
        public void ContainingContentTypesAsStringValueShouldNotThrowExceptionWithCorrectValue()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.FullHttpBadRequestAction())
                .ShouldReturn()
                .BadRequest(badRequest => badRequest
                    .ContainingContentTypes(new List<string>
                    {
                        ContentType.ApplicationJson,
                        ContentType.ApplicationXml
                    }));
        }

        [Fact]
        public void ContainingContentTypesAsStringShouldNotThrowExceptionWithCorrectParametersValue()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.FullHttpBadRequestAction())
                .ShouldReturn()
                .BadRequest(badRequest => badRequest
                    .ContainingContentTypes(
                        ContentType.ApplicationJson, 
                        ContentType.ApplicationXml));
        }

        [Fact]
        public void ContainingContentTypesStringValueShouldNotThrowExceptionWithIncorrectValue()
        {
            Test.AssertException<BadRequestResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.FullHttpBadRequestAction())
                        .ShouldReturn()
                        .BadRequest(badRequest => badRequest
                            .ContainingContentTypes(new List<string>
                            {
                                ContentType.ApplicationOctetStream,
                                ContentType.ApplicationXml
                            }));
                },
                "When calling FullHttpBadRequestAction action in MvcController expected bad request result content types to contain application/octet-stream, but in fact such was not found.");
        }

        [Fact]
        public void ContainingContentTypesAsStringValueShouldNotThrowExceptionWithIncorrectCount()
        {
            Test.AssertException<BadRequestResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.FullHttpBadRequestAction())
                        .ShouldReturn()
                        .BadRequest(badRequest => badRequest
                            .ContainingContentTypes(new List<string>
                            {
                                ContentType.ApplicationXml
                            }));
                },
                "When calling FullHttpBadRequestAction action in MvcController expected bad request result content types to have 1 item, but instead found 2.");
        }

        [Fact]
        public void ContainingContentTypesAsStringValueShouldNotThrowExceptionWithIncorrectCountWithMoreThanOneItem()
        {
            Test.AssertException<BadRequestResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.FullHttpBadRequestAction())
                        .ShouldReturn()
                        .BadRequest(badRequest => badRequest
                            .ContainingContentTypes(new List<string>
                            {
                                ContentType.ApplicationXml,
                                ContentType.ApplicationJson,
                                ContentType.ApplicationZip
                            }));
                },
                "When calling FullHttpBadRequestAction action in MvcController expected bad request result content types to have 3 items, but instead found 2.");
        }

        [Fact]
        public void ContainingContentTypesAsMediaTypeHeaderValueShouldNotThrowExceptionWithCorrectValue()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.FullHttpBadRequestAction())
                .ShouldReturn()
                .BadRequest(badRequest => badRequest
                    .ContainingContentTypes(new List<MediaTypeHeaderValue>
                    {
                        new MediaTypeHeaderValue(ContentType.ApplicationJson),
                        new MediaTypeHeaderValue(ContentType.ApplicationXml)
                    }));
        }

        [Fact]
        public void ContainingContentTypesAsMediaTypeHeaderValueShouldNotThrowExceptionWithCorrectParametersValue()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.FullHttpBadRequestAction())
                .ShouldReturn()
                .BadRequest(badRequest => badRequest
                    .ContainingContentTypes(
                        new MediaTypeHeaderValue(ContentType.ApplicationJson), 
                        new MediaTypeHeaderValue(ContentType.ApplicationXml)));
        }

        [Fact]
        public void ContainingContentTypesAsMediaTypeHeaderValueShouldNotThrowExceptionWithIncorrectValue()
        {
            Test.AssertException<BadRequestResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.FullHttpBadRequestAction())
                        .ShouldReturn()
                        .BadRequest(badRequest => badRequest
                            .ContainingContentTypes(new List<MediaTypeHeaderValue>
                            {
                                new MediaTypeHeaderValue(ContentType.ApplicationOctetStream),
                                new MediaTypeHeaderValue(ContentType.ApplicationXml)
                            }));
                },
                "When calling FullHttpBadRequestAction action in MvcController expected bad request result content types to contain application/octet-stream, but in fact such was not found.");
        }

        [Fact]
        public void ContainingContentTypesAsMediaTypeHeaderValueShouldNotThrowExceptionWithIncorrectCount()
        {
            Test.AssertException<BadRequestResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.FullHttpBadRequestAction())
                        .ShouldReturn()
                        .BadRequest(badRequest => badRequest
                            .ContainingContentTypes(new List<MediaTypeHeaderValue>
                            {
                                new MediaTypeHeaderValue(ContentType.ApplicationXml)
                            }));
                },
                "When calling FullHttpBadRequestAction action in MvcController expected bad request result content types to have 1 item, but instead found 2.");
        }

        [Fact]
        public void ContainingContentTypesAsMediaTypeHeaderValueValueShouldNotThrowExceptionWithIncorrectCountWithMoreThanOneItem()
        {
            Test.AssertException<BadRequestResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.FullHttpBadRequestAction())
                        .ShouldReturn()
                        .BadRequest(badRequest => badRequest
                            .ContainingContentTypes(new List<MediaTypeHeaderValue>
                            {
                                new MediaTypeHeaderValue(ContentType.ApplicationXml),
                                new MediaTypeHeaderValue(ContentType.ApplicationJson),
                                new MediaTypeHeaderValue(ContentType.ApplicationZip)
                            }));
                },
                "When calling FullHttpBadRequestAction action in MvcController expected bad request result content types to have 3 items, but instead found 2.");
        }

        [Fact]
        public void ContainingOutputFormatterShouldNotThrowExceptionWithCorrectValue()
        {
            var formatter = TestObjectFactory.GetOutputFormatter();

            MyController<MvcController>
                .Instance()
                .WithoutValidation()
                .Calling(c => c.HttpBadRequestActionWithFormatter(formatter))
                .ShouldReturn()
                .BadRequest(badRequest => badRequest
                    .ContainingOutputFormatter(formatter));
        }

        [Fact]
        public void ContainingOutputFormatterShouldThrowExceptionWithIncorrectValue()
        {
            Test.AssertException<BadRequestResultAssertionException>(
                () =>
                {
                    var formatter = TestObjectFactory.GetOutputFormatter();

                    MyController<MvcController>
                        .Instance()
                        .WithoutValidation()
                        .Calling(c => c.HttpBadRequestActionWithFormatter(formatter))
                        .ShouldReturn()
                        .BadRequest(badRequest => badRequest
                            .ContainingOutputFormatter(new CustomOutputFormatter()));
                },
                "When calling HttpBadRequestActionWithFormatter action in MvcController expected bad request result output formatters to contain the provided formatter, but such was not found.");
        }

        [Fact]
        public void ContainingOutputFormatterOfTypeShouldNotThrowExceptionWithCorrectValue()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.FullHttpBadRequestAction())
                .ShouldReturn()
                .BadRequest(badRequest => badRequest
                    .ContainingOutputFormatterOfType<NewtonsoftJsonOutputFormatter>());
        }

        [Fact]
        public void ContainingOutputFormatterOfTypeShouldThrowExceptionWithIncorrectValue()
        {
            Test.AssertException<BadRequestResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.FullHttpBadRequestAction())
                        .ShouldReturn()
                        .BadRequest(badRequest => badRequest
                            .ContainingOutputFormatterOfType<IOutputFormatter>());
                },
                "When calling FullHttpBadRequestAction action in MvcController expected bad request result output formatters to contain formatter of IOutputFormatter type, but such was not found.");
        }

        [Fact]
        public void ContainingOutputFormattersShouldNotThrowExceptionWithCorrectValue()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.FullHttpBadRequestAction())
                .ShouldReturn()
                .BadRequest(badRequest => badRequest
                    .ContainingOutputFormatters(new List<IOutputFormatter>
                    {
                        TestObjectFactory.GetOutputFormatter(),
                        new CustomOutputFormatter()
                    }));
        }

        [Fact]
        public void ContainingOutputFormattersShouldNotThrowExceptionWithCorrectParametersValue()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.FullHttpBadRequestAction())
                .ShouldReturn()
                .BadRequest(badRequest => badRequest
                    .ContainingOutputFormatters(
                        TestObjectFactory.GetOutputFormatter(), 
                        new CustomOutputFormatter()));
        }

        [Fact]
        public void ContainingOutputFormattersShouldThrowExceptionWithIncorrectValue()
        {
            Test.AssertException<BadRequestResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.FullHttpBadRequestAction())
                        .ShouldReturn()
                        .BadRequest(badRequest => badRequest
                            .ContainingOutputFormatters(new List<IOutputFormatter>
                            {
                                new CustomOutputFormatter(),
                                new StringOutputFormatter()
                            }));
                },
                "When calling FullHttpBadRequestAction action in MvcController expected bad request result output formatters to contain formatter of StringOutputFormatter type, but none was found.");
        }

        [Fact]
        public void ContainingOutputFormattersShouldThrowExceptionWithIncorrectCount()
        {
            Test.AssertException<BadRequestResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.FullHttpBadRequestAction())
                        .ShouldReturn()
                        .BadRequest(badRequest => badRequest
                            .ContainingOutputFormatters(new List<IOutputFormatter>
                            {
                                new CustomOutputFormatter()
                            }));
                },
                "When calling FullHttpBadRequestAction action in MvcController expected bad request result output formatters to have 1 item, but instead found 2.");
        }

        [Fact]
        public void ContainingOutputFormattersShouldThrowExceptionWithIncorrectCountWithMoreThanOneItem()
        {
            Test.AssertException<BadRequestResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.FullHttpBadRequestAction())
                        .ShouldReturn()
                        .BadRequest(badRequest => badRequest
                            .ContainingOutputFormatters(new List<IOutputFormatter>
                            {
                                TestObjectFactory.GetOutputFormatter(),
                                new CustomOutputFormatter(),
                                TestObjectFactory.GetOutputFormatter()
                            }));
                },
                "When calling FullHttpBadRequestAction action in MvcController expected bad request result output formatters to have 3 items, but instead found 2.");
        }

        [Fact]
        public void AndAlsoShouldWorkCorrectly()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.FullHttpBadRequestAction())
                .ShouldReturn()
                .BadRequest(badRequest => badRequest
                    .WithStatusCode(201)
                    .AndAlso()
                    .ContainingOutputFormatters(
                        TestObjectFactory.GetOutputFormatter(), 
                        new CustomOutputFormatter()));
        }

        [Fact]
        public void PassingShouldCorrectlyRunItsAssertionFunction()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.FullHttpBadRequestAction())
                .ShouldReturn()
                .BadRequest(badRequest => badRequest
                    .Passing(br => br.Formatters?.Count == 2));
        }

        [Fact]
        public void PassingShouldThrowAnExceptionOnAnIncorrectAssertion()
        {
            Test.AssertException<InvocationResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.FullHttpBadRequestAction())
                        .ShouldReturn()
                        .BadRequest(badRequest => badRequest
                            .Passing(br => br.Formatters?.Count == 0));
                },
                $"When calling FullHttpBadRequestAction action in MvcController expected the BadRequestObjectResult to pass the given predicate, but it failed.");
        }

        [Fact]
        public void PassingShouldCorrectlyRunItsAssertionAction()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.FullHttpBadRequestAction())
                .ShouldReturn()
                .BadRequest(badRequest => badRequest
                    .Passing(br =>
                    {
                        const int expectedFormattersCount = 2;
                        var actualFormattersCount = br.Formatters?.Count;
                        if (actualFormattersCount != expectedFormattersCount)
                        {
                            throw new InvalidAssertionException(
                                string.Format("Expected {0} to have {1} {2}, but it has {3}.",
                                    br.GetType().ToFriendlyTypeName(),
                                    expectedFormattersCount,
                                    nameof(br.Formatters),
                                    actualFormattersCount));
                        };
                    }));
        }
    }
}
