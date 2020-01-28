namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.ActionResultsTests.ObjectTests
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

    public class ObjectTestBuilderTests
    {
        [Fact]
        public void WithStatusCodeShouldNotThrowExceptionWithCorrectStatusCode()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.FullObjectResultAction())
                .ShouldReturn()
                .Object(result => result
                    .WithStatusCode(201));
        }
        
        [Fact]
        public void WithStatusCodeAsEnumShouldNotThrowExceptionWithCorrectStatusCode()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.FullObjectResultAction())
                .ShouldReturn()
                .Object(result => result
                    .WithStatusCode(HttpStatusCode.Created));
        }

        [Fact]
        public void WithStatusCodeAsEnumShouldThrowExceptionWithIncorrectStatusCode()
        {
            Test.AssertException<ObjectResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.FullObjectResultAction())
                        .ShouldReturn()
                        .Object(result => result
                            .WithStatusCode(HttpStatusCode.OK));
                },
                "When calling FullObjectResultAction action in MvcController expected object result to have 200 (OK) status code, but instead received 201 (Created).");
        }

        [Fact]
        public void ContainingContentTypeShouldNotThrowExceptionWithCorrectValue()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.FullObjectResultAction())
                .ShouldReturn()
                .Object(result => result
                    .ContainingContentType(ContentType.ApplicationJson));
        }

        [Fact]
        public void ContainingContentTypeAsMediaTypeHeaderValueShouldNotThrowExceptionWithCorrectValue()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.FullObjectResultAction())
                .ShouldReturn()
                .Object(result => result
                    .ContainingContentType(new MediaTypeHeaderValue(ContentType.ApplicationJson)));
        }

        [Fact]
        public void ContainingContentTypeAsMediaTypeHeaderValueShouldThrowExceptionWithIncorrectValue()
        {
            Test.AssertException<ObjectResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                       .Instance()
                       .Calling(c => c.FullObjectResultAction())
                       .ShouldReturn()
                       .Object(result => result
                        .ContainingContentType(new MediaTypeHeaderValue(ContentType.ApplicationOctetStream)));
                },
                "When calling FullObjectResultAction action in MvcController expected object result content types to contain application/octet-stream, but in fact such was not found.");
        }

        [Fact]
        public void ContainingContentTypesAsStringValueShouldNotThrowExceptionWithCorrectValue()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.FullObjectResultAction())
                .ShouldReturn()
                .Object(result => result
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
                .Calling(c => c.FullObjectResultAction())
                .ShouldReturn()
                .Object(result => result
                    .ContainingContentTypes(ContentType.ApplicationJson, ContentType.ApplicationXml));
        }

        [Fact]
        public void ContainingContentTypesStringValueShouldNotThrowExceptionWithIncorrectValue()
        {
            Test.AssertException<ObjectResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.FullObjectResultAction())
                        .ShouldReturn()
                        .Object(result => result
                            .ContainingContentTypes(new List<string>
                            {
                                ContentType.ApplicationOctetStream,
                                ContentType.ApplicationXml
                            }));
                },
                "When calling FullObjectResultAction action in MvcController expected object result content types to contain application/octet-stream, but in fact such was not found.");
        }

        [Fact]
        public void ContainingContentTypesAsStringValueShouldNotThrowExceptionWithIncorrectCount()
        {
            Test.AssertException<ObjectResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.FullObjectResultAction())
                        .ShouldReturn()
                        .Object(result => result
                            .ContainingContentTypes(new List<string>
                            {
                                ContentType.ApplicationXml
                            }));
                },
                "When calling FullObjectResultAction action in MvcController expected object result content types to have 1 item, but instead found 2.");
        }

        [Fact]
        public void ContainingContentTypesAsStringValueShouldNotThrowExceptionWithIncorrectCountWithMoreThanOneItem()
        {
            Test.AssertException<ObjectResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.FullObjectResultAction())
                        .ShouldReturn()
                        .Object(result => result
                            .ContainingContentTypes(new List<string>
                            {
                                ContentType.ApplicationXml,
                                ContentType.ApplicationJson,
                                ContentType.ApplicationZip
                            }));
                },
                "When calling FullObjectResultAction action in MvcController expected object result content types to have 3 items, but instead found 2.");
        }

        [Fact]
        public void ContainingContentTypesAsMediaTypeHeaderValueShouldNotThrowExceptionWithCorrectValue()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.FullObjectResultAction())
                .ShouldReturn()
                .Object(result => result
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
                .Calling(c => c.FullObjectResultAction())
                .ShouldReturn()
                .Object(result => result
                    .ContainingContentTypes(
                        new MediaTypeHeaderValue(ContentType.ApplicationJson), 
                        new MediaTypeHeaderValue(ContentType.ApplicationXml)));
        }

        [Fact]
        public void ContainingContentTypesAsMediaTypeHeaderValueShouldNotThrowExceptionWithIncorrectValue()
        {
            Test.AssertException<ObjectResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.FullObjectResultAction())
                        .ShouldReturn()
                        .Object(result => result
                            .ContainingContentTypes(new List<MediaTypeHeaderValue>
                            {
                                new MediaTypeHeaderValue(ContentType.ApplicationOctetStream),
                                new MediaTypeHeaderValue(ContentType.ApplicationXml)
                            }));
                },
                "When calling FullObjectResultAction action in MvcController expected object result content types to contain application/octet-stream, but in fact such was not found.");
        }

        [Fact]
        public void ContainingContentTypesAsMediaTypeHeaderValueShouldNotThrowExceptionWithIncorrectCount()
        {
            Test.AssertException<ObjectResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.FullObjectResultAction())
                        .ShouldReturn()
                        .Object(result => result
                            .ContainingContentTypes(new List<MediaTypeHeaderValue>
                            {
                                new MediaTypeHeaderValue(ContentType.ApplicationXml)
                            }));
                },
                "When calling FullObjectResultAction action in MvcController expected object result content types to have 1 item, but instead found 2.");
        }

        [Fact]
        public void ContainingContentTypesAsMediaTypeHeaderValueValueShouldNotThrowExceptionWithIncorrectCountWithMoreThanOneItem()
        {
            Test.AssertException<ObjectResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.FullObjectResultAction())
                        .ShouldReturn()
                        .Object(result => result
                            .ContainingContentTypes(new List<MediaTypeHeaderValue>
                            {
                                new MediaTypeHeaderValue(ContentType.ApplicationXml),
                                new MediaTypeHeaderValue(ContentType.ApplicationJson),
                                new MediaTypeHeaderValue(ContentType.ApplicationZip)
                            }));
                },
                "When calling FullObjectResultAction action in MvcController expected object result content types to have 3 items, but instead found 2.");
        }

        [Fact]
        public void ContainingOutputFormatterShouldNotThrowExceptionWithCorrectValue()
        {
            var formatter = TestObjectFactory.GetOutputFormatter();

            MyController<MvcController>
                .Instance()
                .WithoutValidation()
                .Calling(c => c.ObjectActionWithFormatter(formatter))
                .ShouldReturn()
                .Object(result => result
                    .ContainingOutputFormatter(formatter));
        }

        [Fact]
        public void ContainingOutputFormatterShouldThrowExceptionWithIncorrectValue()
        {
            Test.AssertException<ObjectResultAssertionException>(
                () =>
                {
                    var formatter = TestObjectFactory.GetOutputFormatter();

                    MyController<MvcController>
                        .Instance()
                        .WithoutValidation()
                        .Calling(c => c.ObjectActionWithFormatter(formatter))
                        .ShouldReturn()
                        .Object(result => result
                            .ContainingOutputFormatter(TestObjectFactory.GetOutputFormatter()));
                },
                "When calling ObjectActionWithFormatter action in MvcController expected object result output formatters to contain the provided formatter, but such was not found.");
        }

        [Fact]
        public void ContainingOutputFormatterOfTypeShouldNotThrowExceptionWithCorrectValue()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.FullObjectResultAction())
                .ShouldReturn()
                .Object(result => result
                    .ContainingOutputFormatterOfType<JsonOutputFormatter>());
        }

        [Fact]
        public void ContainingOutputFormatterOfTypeShouldThrowExceptionWithIncorrectValue()
        {
            Test.AssertException<ObjectResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.FullObjectResultAction())
                        .ShouldReturn()
                        .Object(result => result
                            .ContainingOutputFormatterOfType<IOutputFormatter>());
                },
                "When calling FullObjectResultAction action in MvcController expected object result output formatters to contain formatter of IOutputFormatter type, but such was not found.");
        }

        [Fact]
        public void ContainingOutputFormattersShouldNotThrowExceptionWithCorrectValue()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.FullObjectResultAction())
                .ShouldReturn()
                .Object(result => result
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
                .Calling(c => c.FullObjectResultAction())
                .ShouldReturn()
                .Object(result => result
                    .ContainingOutputFormatters(TestObjectFactory.GetOutputFormatter(), new CustomOutputFormatter()));
        }

        [Fact]
        public void ContainingOutputFormattersShouldThrowExceptionWithIncorrectValue()
        {
            Test.AssertException<ObjectResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.FullObjectResultAction())
                        .ShouldReturn()
                        .Object(result => result
                            .ContainingOutputFormatters(new List<IOutputFormatter>
                            {
                                new StringOutputFormatter(),
                                new CustomOutputFormatter()
                            }));
                },
                "When calling FullObjectResultAction action in MvcController expected object result output formatters to contain formatter of StringOutputFormatter type, but none was found.");
        }

        [Fact]
        public void ContainingOutputFormattersShouldThrowExceptionWithIncorrectCount()
        {
            Test.AssertException<ObjectResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.FullObjectResultAction())
                        .ShouldReturn()
                        .Object(result => result
                            .ContainingOutputFormatters(new List<IOutputFormatter>
                            {
                                TestObjectFactory.GetOutputFormatter()
                            }));
                },
                "When calling FullObjectResultAction action in MvcController expected object result output formatters to have 1 item, but instead found 2.");
        }

        [Fact]
        public void ContainingOutputFormattersShouldThrowExceptionWithIncorrectCountWithMoreThanOneItem()
        {
            Test.AssertException<ObjectResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.FullObjectResultAction())
                        .ShouldReturn()
                        .Object(result => result
                            .ContainingOutputFormatters(new List<IOutputFormatter>
                            {
                                TestObjectFactory.GetOutputFormatter(),
                                new CustomOutputFormatter(),
                                TestObjectFactory.GetOutputFormatter()
                            }));
                },
                "When calling FullObjectResultAction action in MvcController expected object result output formatters to have 3 items, but instead found 2.");
        }

        [Fact]
        public void AndAlsoShouldWorkCorrectly()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.FullObjectResultAction())
                .ShouldReturn()
                .Object(result => result
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
                .Calling(c => c.FullObjectResultAction())
                .ShouldReturn()
                .Object(obj => obj
                    .Passing(o => o.Formatters?.Count == 2));
        }

        [Fact]
        public void PassingShouldThrowAnExceptionOnAnIncorrectAssertion()
        {
            Test.AssertException<InvocationResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.FullObjectResultAction())
                        .ShouldReturn()
                        .Object(obj => obj
                            .Passing(o => o.Formatters?.Count == 0));
                },
                $"When calling FullObjectResultAction action in MvcController expected the ObjectResult to pass the given predicate, but it failed.");
        }

        [Fact]
        public void PassingShouldCorrectlyRunItsAssertionAction()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.FullObjectResultAction())
                .ShouldReturn()
                .Object(obj => obj
                    .Passing(o =>
                    {
                        const int expectedFormattersCount = 2;
                        var actualFormattersCount = o.Formatters?.Count;
                        if (actualFormattersCount != expectedFormattersCount)
                        {
                            throw new InvalidAssertionException(
                                string.Format("Expected {0} to have {1} {2}, but it has {3}.",
                                    o.GetType().ToFriendlyTypeName(),
                                    expectedFormattersCount,
                                    nameof(o.Formatters),
                                    actualFormattersCount));
                        };
                    }));
        }
    }
}
