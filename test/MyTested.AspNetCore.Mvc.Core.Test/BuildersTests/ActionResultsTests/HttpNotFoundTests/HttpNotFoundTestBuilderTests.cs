namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.ActionResultsTests.HttpNotFoundTests
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

    public class HttpNotFoundTestBuilderTests
    {
        [Fact]
        public void WithNoResponseModelShouldNotThrowExceptionWithNoResponseModel()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.HttpNotFoundAction())
                .ShouldReturn()
                .NotFound()
                .WithNoResponseModel();
        }

        [Fact]
        public void WithNoResponseModelShouldThrowExceptionWithAnyResponseModel()
        {
            Test.AssertException<ResponseModelAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.HttpNotFoundWithObjectAction())
                        .ShouldReturn()
                        .NotFound()
                        .WithNoResponseModel();
                },
                "When calling HttpNotFoundWithObjectAction action in MvcController expected to not have response model but in fact response model was found.");
        }
        
        [Fact]
        public void WithStatusCodeShouldNotThrowExceptionWithCorrectStatusCode()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.FullHttpNotFoundAction())
                .ShouldReturn()
                .NotFound()
                .WithStatusCode(201);
        }

        [Fact]
        public void WithStatusCodeAsEnumShouldNotThrowExceptionWithCorrectStatusCode()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.FullHttpNotFoundAction())
                .ShouldReturn()
                .NotFound()
                .WithStatusCode(HttpStatusCode.Created);
        }

        [Fact]
        public void WithStatusCodeAsEnumShouldThrowExceptionWithIncorrectStatusCode()
        {
            Test.AssertException<NotFoundResultAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.FullHttpNotFoundAction())
                        .ShouldReturn()
                        .NotFound()
                        .WithStatusCode(HttpStatusCode.OK);
                },
                "When calling FullHttpNotFoundAction action in MvcController expected HTTP not found result to have 200 (OK) status code, but instead received 201 (Created).");
        }

        [Fact]
        public void ContainingContentTypeShouldNotThrowExceptionWithCorrectValue()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.FullHttpNotFoundAction())
                .ShouldReturn()
                .NotFound()
                .ContainingContentType(ContentType.ApplicationJson);
        }

        [Fact]
        public void ContainingContentTypeAsMediaTypeHeaderValueShouldNotThrowExceptionWithCorrectValue()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.FullHttpNotFoundAction())
                .ShouldReturn()
                .NotFound()
                .ContainingContentType(new MediaTypeHeaderValue(ContentType.ApplicationJson));
        }
        
        [Fact]
        public void ContainingContentTypeAsMediaTypeHeaderValueShouldThrowExceptionWithIncorrectValue()
        {
            Test.AssertException<NotFoundResultAssertionException>(
                () =>
                {
                    MyMvc
                       .Controller<MvcController>()
                       .Calling(c => c.FullHttpNotFoundAction())
                       .ShouldReturn()
                       .NotFound()
                       .ContainingContentType(new MediaTypeHeaderValue(ContentType.ApplicationOctetStream));
                },
                "When calling FullHttpNotFoundAction action in MvcController expected HTTP not found result content types to contain application/octet-stream, but such was not found.");
        }

        [Fact]
        public void ContainingContentTypesAsStringValueShouldNotThrowExceptionWithCorrectValue()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.FullHttpNotFoundAction())
                .ShouldReturn()
                .NotFound()
                .ContainingContentTypes(new List<string>
                {
                    ContentType.ApplicationJson,
                    ContentType.ApplicationXml
                });
        }

        [Fact]
        public void ContainingContentTypesAsStringShouldNotThrowExceptionWithCorrectParametersValue()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.FullHttpNotFoundAction())
                .ShouldReturn()
                .NotFound()
                .ContainingContentTypes(ContentType.ApplicationJson, ContentType.ApplicationXml);
        }

        [Fact]
        public void ContainingContentTypesStringValueShouldNotThrowExceptionWithIncorrectValue()
        {
            Test.AssertException<NotFoundResultAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.FullHttpNotFoundAction())
                        .ShouldReturn()
                        .NotFound()
                        .ContainingContentTypes(new List<string>
                        {
                            ContentType.ApplicationOctetStream,
                            ContentType.ApplicationXml
                        });
                },
                "When calling FullHttpNotFoundAction action in MvcController expected HTTP not found result content types to contain application/octet-stream, but none was found.");
        }

        [Fact]
        public void ContainingContentTypesAsStringValueShouldNotThrowExceptionWithIncorrectCount()
        {
            Test.AssertException<NotFoundResultAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.FullHttpNotFoundAction())
                        .ShouldReturn()
                        .NotFound()
                        .ContainingContentTypes(new List<string>
                        {
                            ContentType.ApplicationXml
                        });
                },
                "When calling FullHttpNotFoundAction action in MvcController expected HTTP not found result content types to have 1 item, but instead found 2.");
        }

        [Fact]
        public void ContainingContentTypesAsStringValueShouldNotThrowExceptionWithIncorrectCountWithMoreThanOneItem()
        {
            Test.AssertException<NotFoundResultAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.FullHttpNotFoundAction())
                        .ShouldReturn()
                        .NotFound()
                        .ContainingContentTypes(new List<string>
                        {
                            ContentType.ApplicationXml,
                            ContentType.ApplicationJson,
                            ContentType.ApplicationZip
                        });
                },
                "When calling FullHttpNotFoundAction action in MvcController expected HTTP not found result content types to have 3 items, but instead found 2.");
        }

        [Fact]
        public void ContainingContentTypesAsMediaTypeHeaderValueShouldNotThrowExceptionWithCorrectValue()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.FullHttpNotFoundAction())
                .ShouldReturn()
                .NotFound()
                .ContainingContentTypes(new List<MediaTypeHeaderValue>
                {
                    new MediaTypeHeaderValue(ContentType.ApplicationJson),
                    new MediaTypeHeaderValue(ContentType.ApplicationXml)
                });
        }

        [Fact]
        public void ContainingContentTypesAsMediaTypeHeaderValueShouldNotThrowExceptionWithCorrectParametersValue()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.FullHttpNotFoundAction())
                .ShouldReturn()
                .NotFound()
                .ContainingContentTypes(new MediaTypeHeaderValue(ContentType.ApplicationJson), new MediaTypeHeaderValue(ContentType.ApplicationXml));
        }

        [Fact]
        public void ContainingContentTypesAsMediaTypeHeaderValueShouldNotThrowExceptionWithIncorrectValue()
        {
            Test.AssertException<NotFoundResultAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.FullHttpNotFoundAction())
                        .ShouldReturn()
                        .NotFound()
                        .ContainingContentTypes(new List<MediaTypeHeaderValue>
                        {
                            new MediaTypeHeaderValue(ContentType.ApplicationOctetStream),
                            new MediaTypeHeaderValue(ContentType.ApplicationXml)
                        });
                },
                "When calling FullHttpNotFoundAction action in MvcController expected HTTP not found result content types to contain application/octet-stream, but none was found.");
        }

        [Fact]
        public void ContainingContentTypesAsMediaTypeHeaderValueShouldNotThrowExceptionWithIncorrectCount()
        {
            Test.AssertException<NotFoundResultAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.FullHttpNotFoundAction())
                        .ShouldReturn()
                        .NotFound()
                        .ContainingContentTypes(new List<MediaTypeHeaderValue>
                        {
                            new MediaTypeHeaderValue(ContentType.ApplicationXml)
                        });
                },
                "When calling FullHttpNotFoundAction action in MvcController expected HTTP not found result content types to have 1 item, but instead found 2.");
        }

        [Fact]
        public void ContainingContentTypesAsMediaTypeHeaderValueValueShouldNotThrowExceptionWithIncorrectCountWithMoreThanOneItem()
        {
            Test.AssertException<NotFoundResultAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.FullHttpNotFoundAction())
                        .ShouldReturn()
                        .NotFound()
                        .ContainingContentTypes(new List<MediaTypeHeaderValue>
                        {
                            new MediaTypeHeaderValue(ContentType.ApplicationXml),
                            new MediaTypeHeaderValue(ContentType.ApplicationJson),
                            new MediaTypeHeaderValue(ContentType.ApplicationZip)
                        });
                },
                "When calling FullHttpNotFoundAction action in MvcController expected HTTP not found result content types to have 3 items, but instead found 2.");
        }

        [Fact]
        public void ContainingOutputFormatterShouldNotThrowExceptionWithCorrectValue()
        {
            var formatter = TestObjectFactory.GetOutputFormatter();

            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.HttpNotFoundActionWithFormatter(formatter))
                .ShouldReturn()
                .NotFound()
                .ContainingOutputFormatter(formatter);
        }

        [Fact]
        public void ContainingOutputFormatterShouldThrowExceptionWithIncorrectValue()
        {
            Test.AssertException<NotFoundResultAssertionException>(
                () =>
                {
                    var formatter = TestObjectFactory.GetOutputFormatter();

                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.HttpNotFoundActionWithFormatter(formatter))
                        .ShouldReturn()
                        .NotFound()
                        .ContainingOutputFormatter(TestObjectFactory.GetOutputFormatter());
                },
                "When calling HttpNotFoundActionWithFormatter action in MvcController expected HTTP not found result output formatters to contain the provided formatter, but such was not found.");
        }

        [Fact]
        public void ContainingOutputFormatterOfTypeShouldNotThrowExceptionWithCorrectValue()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.FullHttpNotFoundAction())
                .ShouldReturn()
                .NotFound()
                .ContainingOutputFormatterOfType<JsonOutputFormatter>();
        }

        [Fact]
        public void ContainingOutputFormatterOfTypeShouldThrowExceptionWithIncorrectValue()
        {
            Test.AssertException<NotFoundResultAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.FullHttpNotFoundAction())
                        .ShouldReturn()
                        .NotFound()
                        .ContainingOutputFormatterOfType<IOutputFormatter>();
                },
                "When calling FullHttpNotFoundAction action in MvcController expected HTTP not found result output formatters to contain formatter of IOutputFormatter type, but such was not found.");
        }

        [Fact]
        public void ContainingOutputFormattersShouldNotThrowExceptionWithCorrectValue()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.FullHttpNotFoundAction())
                .ShouldReturn()
                .NotFound()
                .ContainingOutputFormatters(new List<IOutputFormatter>
                {
                    TestObjectFactory.GetOutputFormatter(),
                    new CustomOutputFormatter()
                });
        }

        [Fact]
        public void ContainingOutputFormattersShouldNotThrowExceptionWithCorrectParametersValue()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.FullHttpNotFoundAction())
                .ShouldReturn()
                .NotFound()
                .ContainingOutputFormatters(TestObjectFactory.GetOutputFormatter(), new CustomOutputFormatter());
        }

        [Fact]
        public void ContainingOutputFormattersShouldThrowExceptionWithIncorrectValue()
        {
            Test.AssertException<NotFoundResultAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.FullHttpNotFoundAction())
                        .ShouldReturn()
                        .NotFound()
                        .ContainingOutputFormatters(new List<IOutputFormatter>
                        {
                            TestObjectFactory.GetOutputFormatter(),
                            new CustomOutputFormatter()
                        });
                },
                "When calling FullHttpNotFoundAction action in MvcController expected HTTP not found result output formatters to contain formatter of HttpNotAcceptableOutputFormatter type, but none was found.");
        }

        [Fact]
        public void ContainingOutputFormattersShouldThrowExceptionWithIncorrectCount()
        {
            Test.AssertException<NotFoundResultAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.FullHttpNotFoundAction())
                        .ShouldReturn()
                        .NotFound()
                        .ContainingOutputFormatters(new List<IOutputFormatter>
                        {
                            TestObjectFactory.GetOutputFormatter()
                        });
                },
                "When calling FullHttpNotFoundAction action in MvcController expected HTTP not found result output formatters to have 1 item, but instead found 2.");
        }

        [Fact]
        public void ContainingOutputFormattersShouldThrowExceptionWithIncorrectCountWithMoreThanOneItem()
        {
            Test.AssertException<NotFoundResultAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.FullHttpNotFoundAction())
                        .ShouldReturn()
                        .NotFound()
                        .ContainingOutputFormatters(new List<IOutputFormatter>
                        {
                            TestObjectFactory.GetOutputFormatter(),
                            new CustomOutputFormatter(),
                            TestObjectFactory.GetOutputFormatter()
                        });
                },
                "When calling FullHttpNotFoundAction action in MvcController expected HTTP not found result output formatters to have 3 items, but instead found 2.");
        }

        [Fact]
        public void AndAlsoShouldWorkCorrectly()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.FullHttpNotFoundAction())
                .ShouldReturn()
                .NotFound()
                .WithStatusCode(201)
                .AndAlso()
                .ContainingOutputFormatters(TestObjectFactory.GetOutputFormatter(), new CustomOutputFormatter());
        }

        [Fact]
        public void AndProvideTheActionResultShouldWorkCorrectly()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.FullHttpNotFoundAction())
                .ShouldReturn()
                .NotFound()
                .ShouldPassFor()
                .TheActionResult(actionResult =>
                {
                    Assert.NotNull(actionResult);
                    Assert.IsAssignableFrom<NotFoundObjectResult>(actionResult);
                });
        }
    }
}
