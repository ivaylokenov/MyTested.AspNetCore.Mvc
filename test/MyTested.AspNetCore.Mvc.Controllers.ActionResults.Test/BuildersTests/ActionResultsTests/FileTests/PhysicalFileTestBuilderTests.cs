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
                .PhysicalFile(physicalFile => physicalFile
                    .WithContentType(ContentType.ApplicationJson));
        }

        [Fact]
        public void WithContentTypeAsMediaTypeHeaderValueShouldNotThrowExceptionWithValidContentType()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.PhysicalFileResult())
                .ShouldReturn()
                .PhysicalFile(physicalFile => physicalFile
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
                        .Calling(c => c.PhysicalFileResult())
                        .ShouldReturn()
                        .PhysicalFile(physicalFile => physicalFile
                            .WithContentType(new MediaTypeHeaderValue(ContentType.ApplicationXml)));
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
                .PhysicalFile(physicalFile => physicalFile
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
                        .Calling(c => c.PhysicalFileResult())
                        .ShouldReturn()
                        .PhysicalFile(physicalFile => physicalFile
                            .WithDownloadName("InvalidDownloadName"));
                },
                "When calling PhysicalFileResult action in MvcController expected file result download name to be 'InvalidDownloadName', but instead received 'FileDownloadName'.");
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
                        .PhysicalFile(physicalFile => physicalFile
                            .WithDownloadName(null));
                },
                "When calling PhysicalFileResult action in MvcController expected file result download name to be null, but instead received 'FileDownloadName'.");
        }
        
        [Fact]
        public void WithPhysicalPathShouldNotThrowExceptionWithValidPath()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.PhysicalFileResult())
                .ShouldReturn()
                .PhysicalFile(physicalFile => physicalFile
                    .WithPath("/test/file"));
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
                        .PhysicalFile(physicalFile => physicalFile
                            .WithPath("/another"));
                },
                "When calling PhysicalFileResult action in MvcController expected file result path to be '/another', but instead received '/test/file'.");
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
                        .PhysicalFile(physicalFile => physicalFile
                            .WithPath(null));
                },
                "When calling PhysicalFileResult action in MvcController expected file result path to be null, but instead received '/test/file'.");
        }

        [Fact]
        public void AndAlsoShouldWorkCorrectlyPhysicalFile()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.PhysicalFileResult())
                .ShouldReturn()
                .PhysicalFile(physicalFile => physicalFile
                    .WithDownloadName("FileDownloadName")
                    .AndAlso()
                    .WithContentType(ContentType.ApplicationJson));
        }
    }
}
