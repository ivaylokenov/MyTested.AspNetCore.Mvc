namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.ActionResultsTests.FileTests
{
    using System.IO;
    using Exceptions;
    using Microsoft.Net.Http.Headers;
    using Setups;
    using Setups.Controllers;
    using Utilities;
    using Xunit;

    public class FileTestBuilderTests
    {
        [Fact]
        public void WithContentTypeAsStringShouldNotThrowExceptionWithValidContentType()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.FileWithVirtualPath())
                .ShouldReturn()
                .File(file => file
                    .WithContentType(ContentType.ApplicationJson));
        }

        [Fact]
        public void WithContentTypeAsMediaTypeHeaderValueShouldNotThrowExceptionWithValidContentType()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.FileWithVirtualPath())
                .ShouldReturn()
                .File(file => file
                    .WithContentType(new MediaTypeHeaderValue(ContentType.ApplicationJson)));
        }
        
        [Fact]
        public void WithContentTypeAsMediaTypeHeaderValueShouldThrowExceptionWithInvalidContentType()
        {
            Test.AssertException<FileResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.FileWithVirtualPath())
                        .ShouldReturn()
                        .File(file => file
                            .WithContentType(new MediaTypeHeaderValue(ContentType.ApplicationXml)));
                }, 
                "When calling FileWithVirtualPath action in MvcController expected file result ContentType to be 'application/xml', but instead received 'application/json'.");
        }

        [Fact]
        public void WithFileDownloadNameShouldNotThrowExceptionWithValidFileDownloadName()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.FileWithVirtualPath())
                .ShouldReturn()
                .File(file => file
                    .WithDownloadName("FileDownloadName"));
        }

        [Fact]
        public void WithFileDownloadNameShouldThrowExceptionWithInvalidFileDownloadName()
        {
            Test.AssertException<FileResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.FileWithVirtualPath())
                        .ShouldReturn()
                        .File(file => file
                            .WithDownloadName("InvalidDownloadName"));
                }, 
                "When calling FileWithVirtualPath action in MvcController expected file result download name to be 'InvalidDownloadName', but instead received 'FileDownloadName'.");
        }

        [Fact]
        public void WithFileDownloadNameShouldThrowExceptionWithEmptyFileDownloadName()
        {
            Test.AssertException<FileResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.FileWithStream())
                        .ShouldReturn()
                        .File(file => file
                            .WithDownloadName(null));
                },
                "When calling FileWithStream action in MvcController expected file result download name to be null, but instead received empty string.");
        }

        [Fact]
        public void WithStreamShouldNotThrowExceptionWithValidStreamContents()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.FileWithStream())
                .ShouldReturn()
                .File(file => file
                    .WithStream(new MemoryStream(new byte[] { 1, 2, 3 })));
        }

        [Fact]
        public void WithStreamShouldThrowExceptionWithInvalidStream()
        {
            Test.AssertException<FileResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.FileWithStream())
                        .ShouldReturn()
                        .File(file => file
                            .WithStream(new MemoryStream(new byte[] { 1, 2 })));
                },
                "When calling FileWithStream action in MvcController expected file result stream to have value as the provided one, but instead received different result.");
        }

        [Fact]
        public void WithFileNameShouldNotThrowExceptionWithValidFileName()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.FileWithVirtualPath())
                .ShouldReturn()
                .File(file => file
                    .WithName("/Test"));
        }

        [Fact]
        public void WithFileNameShouldThrowExceptionWithInvalidFileName()
        {
            Test.AssertException<FileResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.FileWithVirtualPath())
                        .ShouldReturn()
                        .File(file => file
                            .WithName("Invalid"));
                }, 
                "When calling FileWithVirtualPath action in MvcController expected file result name to be 'Invalid', but instead received '/Test'.");
        }

        [Fact]
        public void FileShouldThrowWhenPropertyDoesNotExist()
        {
            Test.AssertException<FileResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.FileWithVirtualPath())
                        .ShouldReturn()
                        .File(file => file
                            .WithContents(new byte[0]));
                },
                "When calling FileWithVirtualPath action in MvcController expected file result to contain contents, but such could not be found.");
        }

        [Fact]
        public void WithFileContentsShouldNotThrowExceptionWithValidFileContents()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.FileWithContents())
                .ShouldReturn()
                .File(file => file
                    .WithContents(new byte[] { 1, 2, 3 }));
        }

        [Fact]
        public void WithFileContentsOfTypeShouldThrowExceptionWithInvalidFileContents()
        {
            Test.AssertException<FileResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.FileWithContents())
                        .ShouldReturn()
                        .File(file => file
                            .WithContents(new byte[] { 1, 2, 3, 4 }));
                }, 
                "When calling FileWithContents action in MvcController expected file result contents to have values as the provided ones, but instead received different result.");
        }

        [Fact]
        public void AndAlsoShouldWorkCorrectly()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.FileWithContents())
                .ShouldReturn()
                .File(file => file
                    .WithContents(new byte[] { 1, 2, 3 })
                    .AndAlso()
                    .WithContentType(ContentType.ApplicationJson));
        }


        [Fact]
        public void PassingShouldCorrectlyRunItsAssertionFunction()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.FileWithVirtualPath())
                .ShouldReturn()
                .File(file => file
                    .Passing(f => f.FileName == "/Test"));
        }

        [Fact]
        public void PassingShouldThrowAnExceptionOnAnIncorrectAssertion()
        {
            Test.AssertException<InvocationResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.FileWithVirtualPath())
                        .ShouldReturn()
                        .File(file => file
                            .Passing(f => f.FileName == string.Empty));
                },
                $"When calling {nameof(MvcController.FileWithVirtualPath)} " +
                $"action in {nameof(MvcController)} expected the VirtualFileResult to pass the given predicate, but it failed.");
        }

        [Fact]
        public void PassingShouldCorrectlyRunItsAssertionAction()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.FileWithVirtualPath())
                .ShouldReturn()
                .File(file => file
                    .Passing(f =>
                    {
                        const string expectedFileName = "/Test";
                        var actualFileName = f.FileName;
                        if (actualFileName != expectedFileName)
                        {
                            throw new InvalidAssertionException(
                                string.Format("Expected {0} to have {1} equal to {2}, but it was {3}.",
                                    f.GetType().ToFriendlyTypeName(),
                                    nameof(f.FileName),
                                    expectedFileName,
                                    actualFileName));
                        };
                    }));
        }
    }
}
