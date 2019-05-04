namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.ActionResultsTests.NotFoundTests
{
    using System.Collections.Generic;
    using System.Net;
    using Exceptions;
    using Microsoft.AspNetCore.Mvc.Formatters;
    using Microsoft.Net.Http.Headers;
    using Setups;
    using Setups.Common;
    using Setups.Controllers;
    using Xunit;

    public class NotFoundTestBuilderTests
    {
        [Fact]
        public void WithStatusCodeShouldNotThrowExceptionWithCorrectStatusCode()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.FullHttpNotFoundAction())
                .ShouldReturn()
                .NotFound(notFound => notFound
                    .WithStatusCode(201));
        }

        [Fact]
        public void WithStatusCodeAsEnumShouldNotThrowExceptionWithCorrectStatusCode()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.FullHttpNotFoundAction())
                .ShouldReturn()
                .NotFound(notFound => notFound
                    .WithStatusCode(HttpStatusCode.Created));
        }

        [Fact]
        public void WithStatusCodeAsEnumShouldThrowExceptionWithIncorrectStatusCode()
        {
            Test.AssertException<NotFoundResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.FullHttpNotFoundAction())
                        .ShouldReturn()
                        .NotFound(notFound => notFound
                            .WithStatusCode(HttpStatusCode.OK));
                },
                "When calling FullHttpNotFoundAction action in MvcController expected not found result to have 200 (OK) status code, but instead received 201 (Created).");
        }

        [Fact]
        public void ContainingContentTypeShouldNotThrowExceptionWithCorrectValue()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.FullHttpNotFoundAction())
                .ShouldReturn()
                .NotFound(notFound => notFound
                    .ContainingContentType(ContentType.ApplicationJson));
        }

        [Fact]
        public void ContainingContentTypeAsMediaTypeHeaderValueShouldNotThrowExceptionWithCorrectValue()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.FullHttpNotFoundAction())
                .ShouldReturn()
                .NotFound(notFound => notFound
                    .ContainingContentType(new MediaTypeHeaderValue(ContentType.ApplicationJson)));
        }
        
        [Fact]
        public void ContainingContentTypeAsMediaTypeHeaderValueShouldThrowExceptionWithIncorrectValue()
        {
            Test.AssertException<NotFoundResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.FullHttpNotFoundAction())
                        .ShouldReturn()
                        .NotFound(notFound => notFound
                            .ContainingContentType(new MediaTypeHeaderValue(ContentType.ApplicationOctetStream)));
                },
                "When calling FullHttpNotFoundAction action in MvcController expected not found result content types to contain application/octet-stream, but in fact such was not found.");
        }

        [Fact]
        public void ContainingContentTypesAsStringValueShouldNotThrowExceptionWithCorrectValue()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.FullHttpNotFoundAction())
                .ShouldReturn()
                .NotFound(notFound => notFound
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
                .Calling(c => c.FullHttpNotFoundAction())
                .ShouldReturn()
                .NotFound(notFound => notFound
                    .ContainingContentTypes(
                        ContentType.ApplicationJson, 
                        ContentType.ApplicationXml));
        }

        [Fact]
        public void ContainingContentTypesStringValueShouldNotThrowExceptionWithIncorrectValue()
        {
            Test.AssertException<NotFoundResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.FullHttpNotFoundAction())
                        .ShouldReturn()
                        .NotFound(notFound => notFound
                            .ContainingContentTypes(new List<string>
                            {
                                ContentType.ApplicationOctetStream,
                                ContentType.ApplicationXml
                            }));
                },
                "When calling FullHttpNotFoundAction action in MvcController expected not found result content types to contain application/octet-stream, but in fact such was not found.");
        }

        [Fact]
        public void ContainingContentTypesAsStringValueShouldNotThrowExceptionWithIncorrectCount()
        {
            Test.AssertException<NotFoundResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.FullHttpNotFoundAction())
                        .ShouldReturn()
                        .NotFound(notFound => notFound
                            .ContainingContentTypes(new List<string>
                            {
                                ContentType.ApplicationXml
                            }));
                },
                "When calling FullHttpNotFoundAction action in MvcController expected not found result content types to have 1 item, but instead found 2.");
        }

        [Fact]
        public void ContainingContentTypesAsStringValueShouldNotThrowExceptionWithIncorrectCountWithMoreThanOneItem()
        {
            Test.AssertException<NotFoundResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.FullHttpNotFoundAction())
                        .ShouldReturn()
                        .NotFound(notFound => notFound
                            .ContainingContentTypes(new List<string>
                            {
                                ContentType.ApplicationXml,
                                ContentType.ApplicationJson,
                                ContentType.ApplicationZip
                            }));
                },
                "When calling FullHttpNotFoundAction action in MvcController expected not found result content types to have 3 items, but instead found 2.");
        }

        [Fact]
        public void ContainingContentTypesAsMediaTypeHeaderValueShouldNotThrowExceptionWithCorrectValue()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.FullHttpNotFoundAction())
                .ShouldReturn()
                .NotFound(notFound => notFound
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
                .Calling(c => c.FullHttpNotFoundAction())
                .ShouldReturn()
                .NotFound(notFound => notFound
                    .ContainingContentTypes(
                        new MediaTypeHeaderValue(ContentType.ApplicationJson), 
                        new MediaTypeHeaderValue(ContentType.ApplicationXml)));
        }

        [Fact]
        public void ContainingContentTypesAsMediaTypeHeaderValueShouldNotThrowExceptionWithIncorrectValue()
        {
            Test.AssertException<NotFoundResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.FullHttpNotFoundAction())
                        .ShouldReturn()
                        .NotFound(notFound => notFound
                            .ContainingContentTypes(new List<MediaTypeHeaderValue>
                            {
                                new MediaTypeHeaderValue(ContentType.ApplicationOctetStream),
                                new MediaTypeHeaderValue(ContentType.ApplicationXml)
                            }));
                },
                "When calling FullHttpNotFoundAction action in MvcController expected not found result content types to contain application/octet-stream, but in fact such was not found.");
        }

        [Fact]
        public void ContainingContentTypesAsMediaTypeHeaderValueShouldNotThrowExceptionWithIncorrectCount()
        {
            Test.AssertException<NotFoundResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.FullHttpNotFoundAction())
                        .ShouldReturn()
                        .NotFound(notFound => notFound
                            .ContainingContentTypes(new List<MediaTypeHeaderValue>
                            {
                                new MediaTypeHeaderValue(ContentType.ApplicationXml)
                            }));
                },
                "When calling FullHttpNotFoundAction action in MvcController expected not found result content types to have 1 item, but instead found 2.");
        }

        [Fact]
        public void ContainingContentTypesAsMediaTypeHeaderValueValueShouldNotThrowExceptionWithIncorrectCountWithMoreThanOneItem()
        {
            Test.AssertException<NotFoundResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.FullHttpNotFoundAction())
                        .ShouldReturn()
                        .NotFound(notFound => notFound
                            .ContainingContentTypes(new List<MediaTypeHeaderValue>
                            {
                                new MediaTypeHeaderValue(ContentType.ApplicationXml),
                                new MediaTypeHeaderValue(ContentType.ApplicationJson),
                                new MediaTypeHeaderValue(ContentType.ApplicationZip)
                            }));
                },
                "When calling FullHttpNotFoundAction action in MvcController expected not found result content types to have 3 items, but instead found 2.");
        }

        [Fact]
        public void ContainingOutputFormatterShouldNotThrowExceptionWithCorrectValue()
        {
            var formatter = TestObjectFactory.GetOutputFormatter();

            MyController<MvcController>
                .Instance()
                .Calling(c => c.HttpNotFoundActionWithFormatter(formatter))
                .ShouldReturn()
                .NotFound(notFound => notFound
                    .ContainingOutputFormatter(formatter));
        }

        [Fact]
        public void ContainingOutputFormatterShouldThrowExceptionWithIncorrectValue()
        {
            Test.AssertException<NotFoundResultAssertionException>(
                () =>
                {
                    var formatter = TestObjectFactory.GetOutputFormatter();

                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.HttpNotFoundActionWithFormatter(formatter))
                        .ShouldReturn()
                        .NotFound(notFound => notFound
                            .ContainingOutputFormatter(TestObjectFactory.GetOutputFormatter()));
                },
                "When calling HttpNotFoundActionWithFormatter action in MvcController expected not found result output formatters to contain the provided formatter, but such was not found.");
        }

        [Fact]
        public void ContainingOutputFormatterOfTypeShouldNotThrowExceptionWithCorrectValue()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.FullHttpNotFoundAction())
                .ShouldReturn()
                .NotFound(notFound => notFound
                    .ContainingOutputFormatterOfType<JsonOutputFormatter>());
        }

        [Fact]
        public void ContainingOutputFormatterOfTypeShouldThrowExceptionWithIncorrectValue()
        {
            Test.AssertException<NotFoundResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.FullHttpNotFoundAction())
                        .ShouldReturn()
                        .NotFound(notFound => notFound
                            .ContainingOutputFormatterOfType<IOutputFormatter>());
                },
                "When calling FullHttpNotFoundAction action in MvcController expected not found result output formatters to contain formatter of IOutputFormatter type, but such was not found.");
        }

        [Fact]
        public void ContainingOutputFormattersShouldNotThrowExceptionWithCorrectValue()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.FullHttpNotFoundAction())
                .ShouldReturn()
                .NotFound(notFound => notFound
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
                .Calling(c => c.FullHttpNotFoundAction())
                .ShouldReturn()
                .NotFound(notFound => notFound
                    .ContainingOutputFormatters(
                        TestObjectFactory.GetOutputFormatter(), 
                        new CustomOutputFormatter()));
        }

        [Fact]
        public void ContainingOutputFormattersShouldThrowExceptionWithIncorrectValue()
        {
            Test.AssertException<NotFoundResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.FullHttpNotFoundAction())
                        .ShouldReturn()
                        .NotFound(notFound => notFound
                            .ContainingOutputFormatters(new List<IOutputFormatter>
                            {
                                new StringOutputFormatter(),
                                new CustomOutputFormatter()
                            }));
                },
                "When calling FullHttpNotFoundAction action in MvcController expected not found result output formatters to contain formatter of StringOutputFormatter type, but none was found.");
        }

        [Fact]
        public void ContainingOutputFormattersShouldThrowExceptionWithIncorrectCount()
        {
            Test.AssertException<NotFoundResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.FullHttpNotFoundAction())
                        .ShouldReturn()
                        .NotFound(notFound => notFound
                            .ContainingOutputFormatters(new List<IOutputFormatter>
                            {
                                TestObjectFactory.GetOutputFormatter()
                            }));
                },
                "When calling FullHttpNotFoundAction action in MvcController expected not found result output formatters to have 1 item, but instead found 2.");
        }

        [Fact]
        public void ContainingOutputFormattersShouldThrowExceptionWithIncorrectCountWithMoreThanOneItem()
        {
            Test.AssertException<NotFoundResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.FullHttpNotFoundAction())
                        .ShouldReturn()
                        .NotFound(notFound => notFound
                            .ContainingOutputFormatters(new List<IOutputFormatter>
                            {
                                TestObjectFactory.GetOutputFormatter(),
                                new CustomOutputFormatter(),
                                TestObjectFactory.GetOutputFormatter()
                            }));
                },
                "When calling FullHttpNotFoundAction action in MvcController expected not found result output formatters to have 3 items, but instead found 2.");
        }

        [Fact]
        public void AndAlsoShouldWorkCorrectly()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.FullHttpNotFoundAction())
                .ShouldReturn()
                .NotFound(notFound => notFound
                    .WithStatusCode(201)
                    .AndAlso()
                    .ContainingOutputFormatters(
                        TestObjectFactory.GetOutputFormatter(), 
                        new CustomOutputFormatter()));
        }
    }
}
