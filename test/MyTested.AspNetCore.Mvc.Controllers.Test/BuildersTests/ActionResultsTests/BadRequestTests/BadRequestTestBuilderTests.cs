namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.ActionResultsTests.BadRequestTests
{
    using System.Collections.Generic;
    using System.Net;
    using Exceptions;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Formatters;
    using Microsoft.Net.Http.Headers;
    using Setups;
    using Setups.Common;
    using Setups.Controllers;
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
                .BadRequest()
                .WithNoError();
        }

        [Fact]
        public void WithNoErrorShouldThrowExceptionWithError()
        {
            Test.AssertException<ResponseModelAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.BadRequestWithCustomError())
                        .ShouldReturn()
                        .BadRequest()
                        .WithNoError();
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
                .BadRequest()
                .WithErrorMessage();
        }

        [Fact]
        public void WithErrorMessageShouldThrowExceptionWhenResultDoesNotHaveErrorMessage()
        {
            Test.AssertException<BadRequestResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.BadRequestAction())
                        .ShouldReturn()
                        .BadRequest()
                        .WithErrorMessage();
                }, 
                "When calling BadRequestAction action in MvcController expected bad request result to contain error object, but it could not be found.");
        }

        [Fact]
        public void WithErrorMessageShouldNotThrowExceptionWhenResultHasCorrentErrorMessage()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.BadRequestWithErrorAction())
                .ShouldReturn()
                .BadRequest()
                .WithErrorMessage("Bad request");
        }

        [Fact]
        public void WithErrorMessageShouldThrowExceptionWhenResultDoesNotHaveCorrentErrorMessage()
        {
            Test.AssertException<BadRequestResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.BadRequestWithErrorAction())
                        .ShouldReturn()
                        .BadRequest()
                        .WithErrorMessage("Good request");
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
                .BadRequest()
                .WithStatusCode(201);
        }

        [Fact]
        public void WithStatusCodeAsEnumShouldNotThrowExceptionWithCorrectStatusCode()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.FullHttpBadRequestAction())
                .ShouldReturn()
                .BadRequest()
                .WithStatusCode(HttpStatusCode.Created);
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
                        .BadRequest()
                        .WithStatusCode(HttpStatusCode.OK);
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
                .BadRequest()
                .ContainingContentType(ContentType.ApplicationJson);
        }

        [Fact]
        public void ContainingContentTypeAsMediaTypeHeaderValueShouldNotThrowExceptionWithCorrectValue()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.FullHttpBadRequestAction())
                .ShouldReturn()
                .BadRequest()
                .ContainingContentType(new MediaTypeHeaderValue(ContentType.ApplicationJson));
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
                        .BadRequest()
                        .ContainingContentType(new MediaTypeHeaderValue(ContentType.ApplicationOctetStream));
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
                .BadRequest()
                .ContainingContentTypes(new List<string>
                {
                    ContentType.ApplicationJson,
                    ContentType.ApplicationXml
                });
        }

        [Fact]
        public void ContainingContentTypesAsStringShouldNotThrowExceptionWithCorrectParametersValue()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.FullHttpBadRequestAction())
                .ShouldReturn()
                .BadRequest()
                .ContainingContentTypes(ContentType.ApplicationJson, ContentType.ApplicationXml);
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
                        .BadRequest()
                        .ContainingContentTypes(new List<string>
                        {
                            ContentType.ApplicationOctetStream,
                            ContentType.ApplicationXml
                        });
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
                        .BadRequest()
                        .ContainingContentTypes(new List<string>
                        {
                            ContentType.ApplicationXml
                        });
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
                        .BadRequest()
                        .ContainingContentTypes(new List<string>
                        {
                            ContentType.ApplicationXml,
                            ContentType.ApplicationJson,
                            ContentType.ApplicationZip
                        });
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
                .BadRequest()
                .ContainingContentTypes(new List<MediaTypeHeaderValue>
                {
                    new MediaTypeHeaderValue(ContentType.ApplicationJson),
                    new MediaTypeHeaderValue(ContentType.ApplicationXml)
                });
        }

        [Fact]
        public void ContainingContentTypesAsMediaTypeHeaderValueShouldNotThrowExceptionWithCorrectParametersValue()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.FullHttpBadRequestAction())
                .ShouldReturn()
                .BadRequest()
                .ContainingContentTypes(new MediaTypeHeaderValue(ContentType.ApplicationJson), new MediaTypeHeaderValue(ContentType.ApplicationXml));
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
                        .BadRequest()
                        .ContainingContentTypes(new List<MediaTypeHeaderValue>
                        {
                            new MediaTypeHeaderValue(ContentType.ApplicationOctetStream),
                            new MediaTypeHeaderValue(ContentType.ApplicationXml)
                        });
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
                        .BadRequest()
                        .ContainingContentTypes(new List<MediaTypeHeaderValue>
                        {
                            new MediaTypeHeaderValue(ContentType.ApplicationXml)
                        });
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
                        .BadRequest()
                        .ContainingContentTypes(new List<MediaTypeHeaderValue>
                        {
                            new MediaTypeHeaderValue(ContentType.ApplicationXml),
                            new MediaTypeHeaderValue(ContentType.ApplicationJson),
                            new MediaTypeHeaderValue(ContentType.ApplicationZip)
                        });
                },
                "When calling FullHttpBadRequestAction action in MvcController expected bad request result content types to have 3 items, but instead found 2.");
        }

        [Fact]
        public void ContainingOutputFormatterShouldNotThrowExceptionWithCorrectValue()
        {
            var formatter = TestObjectFactory.GetOutputFormatter();

            MyController<MvcController>
                .Instance()
                .Calling(c => c.HttpBadRequestActionWithFormatter(formatter))
                .ShouldReturn()
                .BadRequest()
                .ContainingOutputFormatter(formatter);
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
                        .Calling(c => c.HttpBadRequestActionWithFormatter(formatter))
                        .ShouldReturn()
                        .BadRequest()
                        .ContainingOutputFormatter(new CustomOutputFormatter());
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
                .BadRequest()
                .ContainingOutputFormatterOfType<JsonOutputFormatter>();
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
                        .BadRequest()
                        .ContainingOutputFormatterOfType<IOutputFormatter>();
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
                .BadRequest()
                .ContainingOutputFormatters(new List<IOutputFormatter>
                {
                    TestObjectFactory.GetOutputFormatter(),
                    new CustomOutputFormatter()
                });
        }

        [Fact]
        public void ContainingOutputFormattersShouldNotThrowExceptionWithCorrectParametersValue()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.FullHttpBadRequestAction())
                .ShouldReturn()
                .BadRequest()
                .ContainingOutputFormatters(TestObjectFactory.GetOutputFormatter(), new CustomOutputFormatter());
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
                        .BadRequest()
                        .ContainingOutputFormatters(new List<IOutputFormatter>
                        {
                            new CustomOutputFormatter(),
                            new StringOutputFormatter()
                        });
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
                        .BadRequest()
                        .ContainingOutputFormatters(new List<IOutputFormatter>
                        {
                            new CustomOutputFormatter()
                        });
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
                        .BadRequest()
                        .ContainingOutputFormatters(new List<IOutputFormatter>
                        {
                            TestObjectFactory.GetOutputFormatter(),
                            new CustomOutputFormatter(),
                            TestObjectFactory.GetOutputFormatter()
                        });
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
                .BadRequest()
                .WithStatusCode(201)
                .AndAlso()
                .ContainingOutputFormatters(TestObjectFactory.GetOutputFormatter(), new CustomOutputFormatter());
        }

        [Fact]
        public void AndProvideTheActionResultShouldWorkCorrectly()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.FullHttpBadRequestAction())
                .ShouldReturn()
                .BadRequest()
                .ShouldPassForThe<IActionResult>(actionResult =>
                {
                    Assert.NotNull(actionResult);
                    Assert.IsAssignableFrom<BadRequestObjectResult>(actionResult);
                });
        }
    }
}
