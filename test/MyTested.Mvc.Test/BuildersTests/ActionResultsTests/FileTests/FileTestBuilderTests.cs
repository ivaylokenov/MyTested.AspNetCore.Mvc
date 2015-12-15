namespace MyTested.Mvc.Tests.BuildersTests.ActionResultsTests.FileTests
{
    using Exceptions;
    using Microsoft.Net.Http.Headers;
    using Setups;
    using Setups.Controllers;
    using Xunit;

    public class FileTestBuilderTests
    {
        [Fact]
        public void WithContentTypeAsStringShouldNotThrowExceptionWithValidContentType()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.FileWithVirtualPath())
                .ShouldReturn()
                .File()
                .WithContentType(ContentType.ApplicationJson);
        }

        [Fact]
        public void WithContentTypeAsMediaTypeHeaderValueShouldNotThrowExceptionWithValidContentType()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.FileWithVirtualPath())
                .ShouldReturn()
                .File()
                .WithContentType(new MediaTypeHeaderValue(ContentType.ApplicationJson));
        }
        
        [Fact]
        public void WithContentTypeAsMediaTypeHeaderValueShouldThrowExceptionWithInvalidContentType()
        {
            Test.AssertException<FileResultAssertionException>(() =>
            {
                MyMvc
                    .Controller<MvcController>()
                    .Calling(c => c.FileWithVirtualPath())
                    .ShouldReturn()
                    .File()
                    .WithContentType(new MediaTypeHeaderValue(ContentType.ApplicationXml));
            }, "When calling FileWithVirtualPath action in MvcController expected file result ContentType to be application/xml, but instead received application/json.");
        }

        [Fact]
        public void WithFileDownloadNameShouldNotThrowExceptionWithValidFileDownloadName()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.FileWithVirtualPath())
                .ShouldReturn()
                .File()
                .WithFileDownloadName("FileDownloadName");
        }

        [Fact]
        public void WithFileDownloadNameShouldThrowExceptionWithInvalidFileDownloadName()
        {
            Test.AssertException<FileResultAssertionException>(() =>
            {
                MyMvc
                    .Controller<MvcController>()
                    .Calling(c => c.FileWithVirtualPath())
                    .ShouldReturn()
                    .File()
                    .WithFileDownloadName("InvalidDownloadName");
            }, "When calling FileWithVirtualPath action in MvcController expected file result FileDownloadName to be 'InvalidDownloadName', but instead received 'FileDownloadName'.");
        }
    }
}
