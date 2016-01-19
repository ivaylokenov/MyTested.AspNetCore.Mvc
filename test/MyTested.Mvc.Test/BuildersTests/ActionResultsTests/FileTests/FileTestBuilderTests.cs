namespace MyTested.Mvc.Tests.BuildersTests.ActionResultsTests.FileTests
{
    using System.IO;
    using Exceptions;
    using Microsoft.AspNet.FileProviders;
    using Microsoft.Net.Http.Headers;
    using Setups;
    using Setups.Common;
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
            Test.AssertException<FileResultAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.FileWithVirtualPath())
                        .ShouldReturn()
                        .File()
                        .WithContentType(new MediaTypeHeaderValue(ContentType.ApplicationXml));
                }, 
                "When calling FileWithVirtualPath action in MvcController expected file result ContentType to be application/xml, but instead received application/json.");
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
            Test.AssertException<FileResultAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
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
                    MyMvc
                        .Controller<MvcController>()
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
            MyMvc
                .Controller<MvcController>()
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
                    MyMvc
                        .Controller<MvcController>()
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
            MyMvc
                .Controller<MvcController>()
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
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.FileWithVirtualPath())
                        .ShouldReturn()
                        .File()
                        .WithFileName("Invalid");
                }, 
                "When calling FileWithVirtualPath action in MvcController expected file result FileName to be 'Invalid', but instead received '/Test'.");
        }

        [Fact]
        public void WithFileProviderShouldNotThrowExceptionWithValidFileProvider()
        {
            var fileProvider = TestObjectFactory.GetFileProvider();

            MyMvc
                .Controller<MvcController>()
                .WithoutValidation()
                .Calling(c => c.FileWithFileProvider(fileProvider))
                .ShouldReturn()
                .File()
                .WithFileProvider(fileProvider);
        }

        [Fact]
        public void WithFileProviderShouldNotThrowExceptionWithNullFileProvider()
        {
            MyMvc
                .Controller<MvcController>()
                .WithoutValidation()
                .Calling(c => c.FileWithNullProvider())
                .ShouldReturn()
                .File()
                .WithFileProvider(null);
        }

        [Fact]
        public void FileShouldThrowWhenPropertyDoesNotExist()
        {
            Test.AssertException<FileResultAssertionException>(
                   () =>
                   {
                       MyMvc
                           .Controller<MvcController>()
                           .Calling(c => c.FileWithVirtualPath())
                           .ShouldReturn()
                           .File()
                           .WithContents(new byte[0]);
                   },
                   "When calling FileWithVirtualPath action in MvcController expected file result to contain file contents, but it could not be found.");
        }

        [Fact]
        public void WithFileProviderShouldThrowExceptionWithInvalidFileProvider()
        {
            Test.AssertException<FileResultAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .WithoutValidation()
                        .Calling(c => c.FileWithFileProvider(null))
                        .ShouldReturn()
                        .File()
                        .WithFileProvider(new CustomFileProvider());
                }, 
                "When calling FileWithFileProvider action in MvcController expected file result FileProvider to be the same as the provided one, but instead received different result.");
        }

        [Fact]
        public void WithFileProviderOfTypeShouldNotThrowExceptionWithValidFileProvider()
        {
            MyMvc
                .Controller<MvcController>()
                .WithoutValidation()
                .Calling(c => c.FileWithFileProvider(null))
                .ShouldReturn()
                .File()
                .WithFileProviderOfType<CustomFileProvider>();
        }

        [Fact]
        public void WithFileProviderOfTypeShouldThrowExceptionWithInvalidFileProvider()
        {
            Test.AssertException<FileResultAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .WithoutValidation()
                        .Calling(c => c.FileWithFileProvider(null))
                        .ShouldReturn()
                        .File()
                        .WithFileProviderOfType<IFileProvider>();
                }, 
                "When calling FileWithFileProvider action in MvcController expected file result FileProvider to be of IFileProvider type, but instead received CustomFileProvider.");
        }

        [Fact]
        public void WithFileProviderOfTypeShouldNotThrowExceptionWithNullFileProvider()
        {
            Test.AssertException<FileResultAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .WithoutValidation()
                        .Calling(c => c.FileWithNullProvider())
                        .ShouldReturn()
                        .File()
                        .WithFileProviderOfType<CustomFileProvider>();
                },
                "When calling FileWithNullProvider action in MvcController expected file result FileProvider to be of CustomFileProvider type, but instead received null.");
        }

        [Fact]
        public void WithFileContentsShouldNotThrowExceptionWithValidFileContents()
        {
            MyMvc
                .Controller<MvcController>()
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
                    MyMvc
                        .Controller<MvcController>()
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
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.FileWithContents())
                .ShouldReturn()
                .File()
                .WithContents(new byte[] { 1, 2, 3 })
                .AndAlso()
                .WithContentType(ContentType.ApplicationJson);
        }
    }
}
