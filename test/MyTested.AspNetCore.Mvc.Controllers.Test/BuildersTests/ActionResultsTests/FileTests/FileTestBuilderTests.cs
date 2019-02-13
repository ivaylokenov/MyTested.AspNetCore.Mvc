namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.ActionResultsTests.FileTests
{
    using System.IO;
    using Exceptions;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Net.Http.Headers;
    using Setups;
    using Setups.Controllers;
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
                .File()
                .WithContentType(ContentType.ApplicationJson);
        }

        [Fact]
        public void WithContentTypeAsMediaTypeHeaderValueShouldNotThrowExceptionWithValidContentType()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.FileWithVirtualPath())
                .ShouldReturn()
                .File()
                .WithContentType(new MediaTypeHeaderValue(ContentType.ApplicationJson));
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
                        .File()
                        .WithContentType(new MediaTypeHeaderValue(ContentType.ApplicationXml));
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
                .File()
                .WithFileDownloadName("FileDownloadName");
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
                        .File()
                        .WithFileDownloadName("InvalidDownloadName");
                }, 
                "When calling FileWithVirtualPath action in MvcController expected file result FileDownloadName to be 'InvalidDownloadName', but instead received 'FileDownloadName'.");
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
                        .File()
                        .WithFileDownloadName(null);
                },
                "When calling FileWithStream action in MvcController expected file result FileDownloadName to be null, but instead received empty string.");
        }

        [Fact]
        public void WithStreamShouldNotThrowExceptionWithValidStreamContents()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.FileWithStream())
                .ShouldReturn()
                .File()
                .WithStream(new MemoryStream(new byte[] { 1, 2, 3 }));
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
                        .File()
                        .WithStream(new MemoryStream(new byte[] { 1, 2 }));
                },
                "When calling FileWithStream action in MvcController expected file result FileStream to have contents as the provided one, but instead received different result.");
        }

        [Fact]
        public void WithFileNameShouldNotThrowExceptionWithValidFileName()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.FileWithVirtualPath())
                .ShouldReturn()
                .File()
                .WithFileName("/Test");
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
                        .File()
                        .WithFileName("Invalid");
                }, 
                "When calling FileWithVirtualPath action in MvcController expected file result FileName to be 'Invalid', but instead received '/Test'.");
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
                           .File()
                           .WithContents(new byte[0]);
                   },
                   "When calling FileWithVirtualPath action in MvcController expected file result to contain file contents, but it could not be found.");
        }

        [Fact]
        public void WithFileContentsShouldNotThrowExceptionWithValidFileContents()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.FileWithContents())
                .ShouldReturn()
                .File()
                .WithContents(new byte[] { 1, 2, 3 });
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
                        .File()
                        .WithContents(new byte[] { 1, 2, 3, 4 });
                }, 
                "When calling FileWithContents action in MvcController expected file result FileContents to have contents as the provided ones, but instead received different result.");
        }

        [Fact]
        public void AndAlsoShouldWorkCorrectly()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.FileWithContents())
                .ShouldReturn()
                .File()
                .WithContents(new byte[] { 1, 2, 3 })
                .AndAlso()
                .WithContentType(ContentType.ApplicationJson);
        }

        [Fact]
        public void AndProvideTheActionResultShouldWorkCorrectly()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.FileWithContents())
                .ShouldReturn()
                .File()
                .ShouldPassForThe<IActionResult>(actionResult =>
                {
                    Assert.NotNull(actionResult);
                    Assert.IsAssignableFrom<FileResult>(actionResult);
                });
        }
    }
}
