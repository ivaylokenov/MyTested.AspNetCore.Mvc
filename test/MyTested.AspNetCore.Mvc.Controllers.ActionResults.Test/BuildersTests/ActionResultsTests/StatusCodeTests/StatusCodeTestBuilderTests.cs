namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.ActionResultsTests.StatusCodeTests
{
    using System.Collections.Generic;
    using Exceptions;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Formatters;
    using Microsoft.Net.Http.Headers;
    using Setups;
    using Setups.Common;
    using Setups.Controllers;
    using Utilities;
    using Xunit;

    public class StatusCodeTestBuilderTests
    {
        [Fact]
        public void ContainingContentTypeShouldNotThrowExceptionWithCorrectValue()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.FullObjectResultAction())
                .ShouldReturn()
                .StatusCode(result => result
                    .ContainingContentType(ContentType.ApplicationJson));
        }

        [Fact]
        public void ContainingContentTypeAsMediaTypeHeaderValueShouldNotThrowExceptionWithCorrectValue()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.FullObjectResultAction())
                .ShouldReturn()
                .StatusCode(result => result
                    .ContainingContentType(new MediaTypeHeaderValue(ContentType.ApplicationJson)));
        }

        [Fact]
        public void ContainingContentTypeAsMediaTypeHeaderValueShouldThrowExceptionWithIncorrectValue()
        {
            Test.AssertException<StatusCodeResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                       .Instance()
                       .Calling(c => c.FullObjectResultAction())
                       .ShouldReturn()
                       .StatusCode(result => result
                        .ContainingContentType(new MediaTypeHeaderValue(ContentType.ApplicationOctetStream)));
                },
                "When calling FullObjectResultAction action in MvcController expected status code result content types to contain application/octet-stream, but in fact such was not found.");
        }

        [Fact]
        public void ContainingContentTypesAsStringValueShouldNotThrowExceptionWithCorrectValue()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.FullObjectResultAction())
                .ShouldReturn()
                .StatusCode(result => result
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
                .StatusCode(result => result
                    .ContainingContentTypes(
                        ContentType.ApplicationJson, 
                        ContentType.ApplicationXml));
        }

        [Fact]
        public void ContainingContentTypesStringValueShouldNotThrowExceptionWithIncorrectValue()
        {
            Test.AssertException<StatusCodeResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.FullObjectResultAction())
                        .ShouldReturn()
                        .StatusCode(result => result
                            .ContainingContentTypes(new List<string>
                            {
                                ContentType.ApplicationOctetStream,
                                ContentType.ApplicationXml
                            }));
                },
                "When calling FullObjectResultAction action in MvcController expected status code result content types to contain application/octet-stream, but in fact such was not found.");
        }

        [Fact]
        public void ContainingContentTypesAsStringValueShouldNotThrowExceptionWithIncorrectCount()
        {
            Test.AssertException<StatusCodeResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.FullObjectResultAction())
                        .ShouldReturn()
                        .StatusCode(result => result
                            .ContainingContentTypes(new List<string>
                            {
                                ContentType.ApplicationXml
                            }));
                },
                "When calling FullObjectResultAction action in MvcController expected status code result content types to have 1 item, but instead found 2.");
        }

        [Fact]
        public void ContainingContentTypesAsStringValueShouldNotThrowExceptionWithIncorrectCountWithMoreThanOneItem()
        {
            Test.AssertException<StatusCodeResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.FullObjectResultAction())
                        .ShouldReturn()
                        .StatusCode(result => result
                            .ContainingContentTypes(new List<string>
                            {
                                ContentType.ApplicationXml,
                                ContentType.ApplicationJson,
                                ContentType.ApplicationZip
                            }));
                },
                "When calling FullObjectResultAction action in MvcController expected status code result content types to have 3 items, but instead found 2.");
        }

        [Fact]
        public void ContainingContentTypesAsMediaTypeHeaderValueShouldNotThrowExceptionWithCorrectValue()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.FullObjectResultAction())
                .ShouldReturn()
                .StatusCode(result => result
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
                .StatusCode(result => result
                    .ContainingContentTypes(
                        new MediaTypeHeaderValue(ContentType.ApplicationJson), 
                        new MediaTypeHeaderValue(ContentType.ApplicationXml)));
        }

        [Fact]
        public void ContainingContentTypesAsMediaTypeHeaderValueShouldNotThrowExceptionWithIncorrectValue()
        {
            Test.AssertException<StatusCodeResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.FullObjectResultAction())
                        .ShouldReturn()
                        .StatusCode(result => result
                            .ContainingContentTypes(new List<MediaTypeHeaderValue>
                            {
                                new MediaTypeHeaderValue(ContentType.ApplicationOctetStream),
                                new MediaTypeHeaderValue(ContentType.ApplicationXml)
                            }));
                },
                "When calling FullObjectResultAction action in MvcController expected status code result content types to contain application/octet-stream, but in fact such was not found.");
        }

        [Fact]
        public void ContainingContentTypesAsMediaTypeHeaderValueShouldNotThrowExceptionWithIncorrectCount()
        {
            Test.AssertException<StatusCodeResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.FullObjectResultAction())
                        .ShouldReturn()
                        .StatusCode(result => result
                            .ContainingContentTypes(new List<MediaTypeHeaderValue>
                            {
                                new MediaTypeHeaderValue(ContentType.ApplicationXml)
                            }));
                },
                "When calling FullObjectResultAction action in MvcController expected status code result content types to have 1 item, but instead found 2.");
        }

        [Fact]
        public void ContainingContentTypesAsMediaTypeHeaderValueValueShouldNotThrowExceptionWithIncorrectCountWithMoreThanOneItem()
        {
            Test.AssertException<StatusCodeResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.FullObjectResultAction())
                        .ShouldReturn()
                        .StatusCode(result => result
                            .ContainingContentTypes(new List<MediaTypeHeaderValue>
                            {
                                new MediaTypeHeaderValue(ContentType.ApplicationXml),
                                new MediaTypeHeaderValue(ContentType.ApplicationJson),
                                new MediaTypeHeaderValue(ContentType.ApplicationZip)
                            }));
                },
                "When calling FullObjectResultAction action in MvcController expected status code result content types to have 3 items, but instead found 2.");
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
                .StatusCode(result => result
                    .ContainingOutputFormatter(formatter));
        }

        [Fact]
        public void ContainingOutputFormatterShouldThrowExceptionWithIncorrectValue()
        {
            Test.AssertException<StatusCodeResultAssertionException>(
                () =>
                {
                    var formatter = TestObjectFactory.GetOutputFormatter();

                    MyController<MvcController>
                        .Instance()
                        .WithoutValidation()
                        .Calling(c => c.ObjectActionWithFormatter(formatter))
                        .ShouldReturn()
                        .StatusCode(result => result
                            .ContainingOutputFormatter(TestObjectFactory.GetOutputFormatter()));
                },
                "When calling ObjectActionWithFormatter action in MvcController expected status code result output formatters to contain the provided formatter, but such was not found.");
        }

        [Fact]
        public void ContainingOutputFormatterOfTypeShouldNotThrowExceptionWithCorrectValue()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.FullObjectResultAction())
                .ShouldReturn()
                .StatusCode(result => result
                    .ContainingOutputFormatterOfType<JsonOutputFormatter>());
        }

        [Fact]
        public void ContainingOutputFormatterOfTypeShouldThrowExceptionWithIncorrectValue()
        {
            Test.AssertException<StatusCodeResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.FullObjectResultAction())
                        .ShouldReturn()
                        .StatusCode(result => result
                            .ContainingOutputFormatterOfType<IOutputFormatter>());
                },
                "When calling FullObjectResultAction action in MvcController expected status code result output formatters to contain formatter of IOutputFormatter type, but such was not found.");
        }

        [Fact]
        public void ContainingOutputFormattersShouldNotThrowExceptionWithCorrectValue()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.FullObjectResultAction())
                .ShouldReturn()
                .StatusCode(result => result
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
                .StatusCode(result => result
                    .ContainingOutputFormatters(
                        TestObjectFactory.GetOutputFormatter(), 
                        new CustomOutputFormatter()));
        }

        [Fact]
        public void ContainingOutputFormattersShouldThrowExceptionWithIncorrectValue()
        {
            Test.AssertException<StatusCodeResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.FullObjectResultAction())
                        .ShouldReturn()
                        .StatusCode(result => result
                            .ContainingOutputFormatters(new List<IOutputFormatter>
                            {
                                new StringOutputFormatter(),
                                new CustomOutputFormatter()
                            }));
                },
                "When calling FullObjectResultAction action in MvcController expected status code result output formatters to contain formatter of StringOutputFormatter type, but none was found.");
        }

        [Fact]
        public void ContainingOutputFormattersShouldThrowExceptionWithIncorrectCount()
        {
            Test.AssertException<StatusCodeResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.FullObjectResultAction())
                        .ShouldReturn()
                        .StatusCode(result => result
                            .ContainingOutputFormatters(new List<IOutputFormatter>
                            {
                                TestObjectFactory.GetOutputFormatter()
                            }));
                },
                "When calling FullObjectResultAction action in MvcController expected status code result output formatters to have 1 item, but instead found 2.");
        }

        [Fact]
        public void ContainingOutputFormattersShouldThrowExceptionWithIncorrectCountWithMoreThanOneItem()
        {
            Test.AssertException<StatusCodeResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.FullObjectResultAction())
                        .ShouldReturn()
                        .StatusCode(result => result
                            .ContainingOutputFormatters(new List<IOutputFormatter>
                            {
                                TestObjectFactory.GetOutputFormatter(),
                                new CustomOutputFormatter(),
                                TestObjectFactory.GetOutputFormatter()
                            }));
                },
                "When calling FullObjectResultAction action in MvcController expected status code result output formatters to have 3 items, but instead found 2.");
        }
        
        [Fact]
        public void CallingObjectResultApiWithStatusCodeShouldThrowException()
        {
            Test.AssertException<StatusCodeResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.StatusCodeAction())
                        .ShouldReturn()
                        .StatusCode(result => result
                            .ContainingOutputFormatters(new List<IOutputFormatter>
                            {
                                TestObjectFactory.GetOutputFormatter(),
                                new CustomOutputFormatter(),
                                TestObjectFactory.GetOutputFormatter()
                            }));
                },
                "When calling StatusCodeAction action in MvcController expected status code result to inherit ObjectResult, but in fact it did not.");
        }

        [Fact]
        public void AndProvideTheActionResultShouldWorkCorrectly()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.FullObjectResultAction())
                .ShouldReturn()
                .StatusCode(result => result
                    .ShouldPassForThe<IActionResult>(actionResult =>
                    {
                        Assert.NotNull(actionResult);
                        Assert.IsAssignableFrom<ObjectResult>(actionResult);
                    }));
        }


        [Fact]
        public void PassingShouldCorrectlyRunItsAssertionFunction()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.FullObjectResultAction())
                .ShouldReturn()
                .StatusCode(result => result
                    .Passing(r => r.Formatters?.Count == 2));
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
                        .StatusCode(result => result
                            .Passing(r => r.Formatters?.Count == 0));
                },
                $"When calling {nameof(MvcController.FullObjectResultAction)} " +
                $"action in {nameof(MvcController)} expected the ObjectResult to pass the given predicate, but it failed.");
        }

        [Fact]
        public void PassingShouldCorrectlyRunItsAssertionAction()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.FullObjectResultAction())
                .ShouldReturn()
                .StatusCode(result => result
                    .Passing(r =>
                    {
                        const int expectedFormattersCount = 2;
                        var actualFormattersCount = r.Formatters?.Count;
                        if (actualFormattersCount != expectedFormattersCount)
                        {
                            throw new InvalidAssertionException(
                                string.Format("Expected {0} to have {1} {2}, but it has {3}.",
                                    r.GetType().ToFriendlyTypeName(),
                                    expectedFormattersCount,
                                    nameof(r.Formatters),
                                    actualFormattersCount));
                        };
                    }));
        }
    }
}
