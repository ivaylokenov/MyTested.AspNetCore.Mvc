namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.ActionResultsTests.FileTests
{
    using Exceptions;
    using Microsoft.Net.Http.Headers;
    using Setups;
    using Setups.Controllers;
    using Xunit;

    public class PhysicalFileTestBuilderTests
    {
        [Fact]
        public void WithContentTypeAsStringShouldNotThrowExceptionWithValidContentType()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.PhysicalFileResult())
                .ShouldReturn()
                .PhysicalFile()
                .WithContentType(ContentType.ApplicationJson);
        }

        [Fact]
        public void WithContentTypeAsMediaTypeHeaderValueShouldNotThrowExceptionWithValidContentType()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.PhysicalFileResult())
                .ShouldReturn()
                .PhysicalFile()
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
                        .Calling(c => c.PhysicalFileResult())
                        .ShouldReturn()
                        .PhysicalFile()
                        .WithContentType(new MediaTypeHeaderValue(ContentType.ApplicationXml));
                },
                "When calling PhysicalFileResult action in MvcController expected file result ContentType to be 'application/xml', but instead received 'application/json'.");
        }

        [Fact]
        public void WithFileDownloadNameShouldNotThrowExceptionWithValidFileDownloadName()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.PhysicalFileResult())
                .ShouldReturn()
                .PhysicalFile()
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
                        .Calling(c => c.PhysicalFileResult())
                        .ShouldReturn()
                        .PhysicalFile()
                        .WithFileDownloadName("InvalidDownloadName");
                },
                "When calling PhysicalFileResult action in MvcController expected file result FileDownloadName to be 'InvalidDownloadName', but instead received 'FileDownloadName'.");
        }

        [Fact]
        public void WithFileDownloadNameShouldThrowExceptionWithEmptyFileDownloadName()
        {
            Test.AssertException<FileResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.PhysicalFileResult())
                        .ShouldReturn()
                        .PhysicalFile()
                        .WithFileDownloadName(null);
                },
                "When calling PhysicalFileResult action in MvcController expected file result FileDownloadName to be null, but instead received 'FileDownloadName'.");
        }
        
        [Fact]
        public void WithPhysicalPathShouldNotThrowExceptionWithValidPath()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.PhysicalFileResult())
                .ShouldReturn()
                .PhysicalFile()
                .WithPhysicalPath("/test/file");
        }

        [Fact]
        public void WithPhysicalPathShouldThrowExceptionWithInvalidPath()
        {
            Test.AssertException<FileResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                    .Instance()
                    .Calling(c => c.PhysicalFileResult())
                    .ShouldReturn()
                    .PhysicalFile()
                    .WithPhysicalPath("/another");
                },
                "When calling PhysicalFileResult action in MvcController expected file result FileName to be '/another', but instead received '/test/file'.");
        }

        [Fact]
        public void WithPhysicalPathShouldThrowExceptionWithNullPath()
        {
            Test.AssertException<FileResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                    .Instance()
                    .Calling(c => c.PhysicalFileResult())
                    .ShouldReturn()
                    .PhysicalFile()
                    .WithPhysicalPath(null);
                },
                "When calling PhysicalFileResult action in MvcController expected file result FileName to be null, but instead received '/test/file'.");
        }

        [Fact]
        public void AndAlsoShouldWorkCorrectly()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.PhysicalFileResult())
                .ShouldReturn()
                .PhysicalFile()
                .WithFileDownloadName("FileDownloadName")
                .AndAlso()
                .WithContentType(ContentType.ApplicationJson);
        }
    }
}
