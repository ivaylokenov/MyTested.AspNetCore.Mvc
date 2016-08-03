namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.ActionResultsTests.OkTests
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
    using Setups.Models;
    using Xunit;

    public class OkTestBuilderTests
    {
        [Fact]
        public void WithNoResponseModelShouldNotThrowExceptionIfNoResponseModelIsProvided()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.OkResultAction())
                .ShouldReturn()
                .Ok()
                .WithNoResponseModel();
        }

        [Fact]
        public void WithNoResponseModelShouldThrowExceptionIfResponseModelIsProvided()
        {
            Test.AssertException<ResponseModelAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.OkResultWithResponse())
                        .ShouldReturn()
                        .Ok()
                        .WithNoResponseModel();
                },
                "When calling OkResultWithResponse action in MvcController expected to not have response model but in fact response model was found.");
        }

        [Fact]
        public void WithResponseModelShouldWorkCorrectly()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.OkResultWithResponse())
                .ShouldReturn()
                .Ok()
                .WithModelOfType<List<ResponseModel>>();
        }
        
        [Fact]
        public void WithStatusCodeShouldNotThrowExceptionWithCorrectStatusCode()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.FullOkAction())
                .ShouldReturn()
                .Ok()
                .WithStatusCode(201);
        }

        [Fact]
        public void WithStatusCodeAsEnumShouldNotThrowExceptionWithCorrectStatusCode()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.FullOkAction())
                .ShouldReturn()
                .Ok()
                .WithStatusCode(HttpStatusCode.Created);
        }

        [Fact]
        public void WithStatusCodeAsEnumShouldThrowExceptionWithIncorrectStatusCode()
        {
            Test.AssertException<OkResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.FullOkAction())
                        .ShouldReturn()
                        .Ok()
                        .WithStatusCode(HttpStatusCode.OK);
                },
                "When calling FullOkAction action in MvcController expected OK result to have 200 (OK) status code, but instead received 201 (Created).");
        }

        [Fact]
        public void ContainingContentTypeShouldNotThrowExceptionWithCorrectValue()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.FullOkAction())
                .ShouldReturn()
                .Ok()
                .ContainingContentType(ContentType.ApplicationJson);
        }

        [Fact]
        public void ContainingContentTypeAsMediaTypeHeaderValueShouldNotThrowExceptionWithCorrectValue()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.FullOkAction())
                .ShouldReturn()
                .Ok()
                .ContainingContentType(new MediaTypeHeaderValue(ContentType.ApplicationJson));
        }
        
        [Fact]
        public void ContainingContentTypeAsMediaTypeHeaderValueShouldThrowExceptionWithIncorrectValue()
        {
            Test.AssertException<OkResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                       .Instance()
                       .Calling(c => c.FullOkAction())
                       .ShouldReturn()
                       .Ok()
                       .ContainingContentType(new MediaTypeHeaderValue(ContentType.ApplicationOctetStream));
                },
                "When calling FullOkAction action in MvcController expected OK result content types to contain application/octet-stream, but such was not found.");
        }

        [Fact]
        public void ContainingContentTypesAsStringValueShouldNotThrowExceptionWithCorrectValue()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.FullOkAction())
                .ShouldReturn()
                .Ok()
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
                .Calling(c => c.FullOkAction())
                .ShouldReturn()
                .Ok()
                .ContainingContentTypes(ContentType.ApplicationJson, ContentType.ApplicationXml);
        }

        [Fact]
        public void ContainingContentTypesStringValueShouldNotThrowExceptionWithIncorrectValue()
        {
            Test.AssertException<OkResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.FullOkAction())
                        .ShouldReturn()
                        .Ok()
                        .ContainingContentTypes(new List<string>
                        {
                            ContentType.ApplicationOctetStream,
                            ContentType.ApplicationXml
                        });
                },
                "When calling FullOkAction action in MvcController expected OK result content types to contain application/octet-stream, but none was found.");
        }

        [Fact]
        public void ContainingContentTypesAsStringValueShouldNotThrowExceptionWithIncorrectCount()
        {
            Test.AssertException<OkResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.FullOkAction())
                        .ShouldReturn()
                        .Ok()
                        .ContainingContentTypes(new List<string>
                        {
                            ContentType.ApplicationXml
                        });
                },
                "When calling FullOkAction action in MvcController expected OK result content types to have 1 item, but instead found 2.");
        }

        [Fact]
        public void ContainingContentTypesAsStringValueShouldNotThrowExceptionWithIncorrectCountWithMoreThanOneItem()
        {
            Test.AssertException<OkResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.FullOkAction())
                        .ShouldReturn()
                        .Ok()
                        .ContainingContentTypes(new List<string>
                        {
                            ContentType.ApplicationXml,
                            ContentType.ApplicationJson,
                            ContentType.ApplicationZip
                        });
                },
                "When calling FullOkAction action in MvcController expected OK result content types to have 3 items, but instead found 2.");
        }

        [Fact]
        public void ContainingContentTypesAsMediaTypeHeaderValueShouldNotThrowExceptionWithCorrectValue()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.FullOkAction())
                .ShouldReturn()
                .Ok()
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
                .Calling(c => c.FullOkAction())
                .ShouldReturn()
                .Ok()
                .ContainingContentTypes(new MediaTypeHeaderValue(ContentType.ApplicationJson), new MediaTypeHeaderValue(ContentType.ApplicationXml));
        }

        [Fact]
        public void ContainingContentTypesAsMediaTypeHeaderValueShouldNotThrowExceptionWithIncorrectValue()
        {
            Test.AssertException<OkResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.FullOkAction())
                        .ShouldReturn()
                        .Ok()
                        .ContainingContentTypes(new List<MediaTypeHeaderValue>
                        {
                            new MediaTypeHeaderValue(ContentType.ApplicationOctetStream),
                            new MediaTypeHeaderValue(ContentType.ApplicationXml)
                        });
                },
                "When calling FullOkAction action in MvcController expected OK result content types to contain application/octet-stream, but none was found.");
        }

        [Fact]
        public void ContainingContentTypesAsMediaTypeHeaderValueShouldNotThrowExceptionWithIncorrectCount()
        {
            Test.AssertException<OkResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.FullOkAction())
                        .ShouldReturn()
                        .Ok()
                        .ContainingContentTypes(new List<MediaTypeHeaderValue>
                        {
                            new MediaTypeHeaderValue(ContentType.ApplicationXml)
                        });
                },
                "When calling FullOkAction action in MvcController expected OK result content types to have 1 item, but instead found 2.");
        }

        [Fact]
        public void ContainingContentTypesAsMediaTypeHeaderValueValueShouldNotThrowExceptionWithIncorrectCountWithMoreThanOneItem()
        {
            Test.AssertException<OkResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.FullOkAction())
                        .ShouldReturn()
                        .Ok()
                        .ContainingContentTypes(new List<MediaTypeHeaderValue>
                        {
                            new MediaTypeHeaderValue(ContentType.ApplicationXml),
                            new MediaTypeHeaderValue(ContentType.ApplicationJson),
                            new MediaTypeHeaderValue(ContentType.ApplicationZip)
                        });
                },
                "When calling FullOkAction action in MvcController expected OK result content types to have 3 items, but instead found 2.");
        }

        [Fact]
        public void ContainingOutputFormatterShouldNotThrowExceptionWithCorrectValue()
        {
            var formatter = TestObjectFactory.GetOutputFormatter();

            MyController<MvcController>
                .Instance()
                .Calling(c => c.OkActionWithFormatter(formatter))
                .ShouldReturn()
                .Ok()
                .ContainingOutputFormatter(formatter);
        }

        [Fact]
        public void ContainingOutputFormatterShouldThrowExceptionWithIncorrectValue()
        {
            Test.AssertException<OkResultAssertionException>(
                () =>
                {
                    var formatter = TestObjectFactory.GetOutputFormatter();

                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.OkActionWithFormatter(formatter))
                        .ShouldReturn()
                        .Ok()
                        .ContainingOutputFormatter(TestObjectFactory.GetOutputFormatter());
                },
                "When calling OkActionWithFormatter action in MvcController expected OK result output formatters to contain the provided formatter, but such was not found.");
        }

        [Fact]
        public void ContainingOutputFormatterOfTypeShouldNotThrowExceptionWithCorrectValue()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.FullOkAction())
                .ShouldReturn()
                .Ok()
                .ContainingOutputFormatterOfType<JsonOutputFormatter>();
        }

        [Fact]
        public void ContainingOutputFormatterOfTypeShouldThrowExceptionWithIncorrectValue()
        {
            Test.AssertException<OkResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.FullOkAction())
                        .ShouldReturn()
                        .Ok()
                        .ContainingOutputFormatterOfType<IOutputFormatter>();
                },
                "When calling FullOkAction action in MvcController expected OK result output formatters to contain formatter of IOutputFormatter type, but such was not found.");
        }

        [Fact]
        public void ContainingOutputFormattersShouldNotThrowExceptionWithCorrectValue()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.FullOkAction())
                .ShouldReturn()
                .Ok()
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
                .Calling(c => c.FullOkAction())
                .ShouldReturn()
                .Ok()
                .ContainingOutputFormatters(TestObjectFactory.GetOutputFormatter(), new CustomOutputFormatter());
        }

        [Fact]
        public void ContainingOutputFormattersShouldThrowExceptionWithIncorrectValue()
        {
            Test.AssertException<OkResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.FullOkAction())
                        .ShouldReturn()
                        .Ok()
                        .ContainingOutputFormatters(new List<IOutputFormatter>
                        {
                            new StringOutputFormatter(),
                            new CustomOutputFormatter()
                        });
                },
                "When calling FullOkAction action in MvcController expected OK result output formatters to contain formatter of StringOutputFormatter type, but none was found.");
        }

        [Fact]
        public void ContainingOutputFormattersShouldThrowExceptionWithIncorrectCount()
        {
            Test.AssertException<OkResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.FullOkAction())
                        .ShouldReturn()
                        .Ok()
                        .ContainingOutputFormatters(new List<IOutputFormatter>
                        {
                            TestObjectFactory.GetOutputFormatter()
                        });
                },
                "When calling FullOkAction action in MvcController expected OK result output formatters to have 1 item, but instead found 2.");
        }

        [Fact]
        public void ContainingOutputFormattersShouldThrowExceptionWithIncorrectCountWithMoreThanOneItem()
        {
            Test.AssertException<OkResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.FullOkAction())
                        .ShouldReturn()
                        .Ok()
                        .ContainingOutputFormatters(new List<IOutputFormatter>
                        {
                            TestObjectFactory.GetOutputFormatter(),
                            new CustomOutputFormatter(),
                            TestObjectFactory.GetOutputFormatter()
                        });
                },
                "When calling FullOkAction action in MvcController expected OK result output formatters to have 3 items, but instead found 2.");
        }

        [Fact]
        public void AndAlsoShouldWorkCorrectly()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.FullOkAction())
                .ShouldReturn()
                .Ok()
                .WithStatusCode(201)
                .AndAlso()
                .ContainingOutputFormatters(TestObjectFactory.GetOutputFormatter(), new CustomOutputFormatter());
        }

        [Fact]
        public void AndProvideTheActionResultShouldWorkCorrectly()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.FullOkAction())
                .ShouldReturn()
                .Ok()
                .ShouldPassFor()
                .TheActionResult(actionResult =>
                {
                    Assert.NotNull(actionResult);
                    Assert.IsAssignableFrom<OkObjectResult>(actionResult);
                });
        }
    }
}
