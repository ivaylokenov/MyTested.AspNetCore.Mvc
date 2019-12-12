namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.ActionResultsTests.FileTests
{
    using Exceptions;
    using Microsoft.Extensions.FileProviders;
    using Setups;
    using Setups.Common;
    using Setups.Controllers;
    using Xunit;

    public class FileTestBuilderTests
    {
        [Fact]
        public void WithFileProviderShouldNotThrowExceptionWithValidFileProvider()
        {
            var fileProvider = TestObjectFactory.GetFileProvider();

            MyController<MvcController>
                .Instance()
                .WithoutValidation()
                .Calling(c => c.FileWithFileProvider(fileProvider))
                .ShouldReturn()
                .File(file => file
                    .WithFileProvider(fileProvider));
        }

        [Fact]
        public void WithFileProviderShouldNotThrowExceptionWithNullFileProvider()
        {
            MyController<MvcController>
                .Instance()
                .WithoutValidation()
                .Calling(c => c.FileWithNullProvider())
                .ShouldReturn()
                .File(file => file
                    .WithFileProvider(null));
        }


        [Fact]
        public void WithFileProviderShouldThrowExceptionWithInvalidFileProvider()
        {
            Test.AssertException<FileResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .WithoutValidation()
                        .Calling(c => c.FileWithFileProvider(null))
                        .ShouldReturn()
                        .File(file => file
                            .WithFileProvider(new CustomFileProvider()));
                },
                "When calling FileWithFileProvider action in MvcController expected file result provider to be the same as the provided one, but instead received different result.");
        }

        [Fact]
        public void WithFileProviderOfTypeShouldNotThrowExceptionWithValidFileProviderForGeneric()
        {
            MyController<MvcController>
                .Instance()
                .WithoutValidation()
                .Calling(c => c.FileWithFileProvider(null))
                .ShouldReturn()
                .File(file => file
                    .WithFileProviderOfType<CustomFileProvider>());
        }

        [Fact]
        public void WithFileProviderOfTypeShouldNotThrowExceptionWithValidFileProvider()
        {
            MyController<MvcController>
                .Instance()
                .WithoutValidation()
                .Calling(c => c.FileWithFileProvider(null))
                .ShouldReturn()
                .File(file => file
                    .WithFileProviderOfType(typeof(CustomFileProvider)));
        }

        [Fact]
        public void WithFileProviderOfTypeShouldThrowExceptionWithInvalidFileProviderForGeneric()
        {
            Test.AssertException<FileResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .WithoutValidation()
                        .Calling(c => c.FileWithFileProvider(null))
                        .ShouldReturn()
                        .File(file => file
                            .WithFileProviderOfType<IFileProvider>());
                },
                "When calling FileWithFileProvider action in MvcController expected file result provider to be of IFileProvider type, but instead received CustomFileProvider.");
        }

        [Fact]
        public void WithFileProviderOfTypeShouldThrowExceptionWithInvalidFileProvider()
        {
            Test.AssertException<FileResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .WithoutValidation()
                        .Calling(c => c.FileWithFileProvider(null))
                        .ShouldReturn()
                        .File(file => file
                            .WithFileProviderOfType(typeof(IFileProvider)));
                },
                "When calling FileWithFileProvider action in MvcController expected file result provider to be of IFileProvider type, but instead received CustomFileProvider.");
        }

        [Fact]
        public void WithFileProviderOfTypeShouldNotThrowExceptionWithNullFileProviderForGeneric()
        {
            Test.AssertException<FileResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .WithoutValidation()
                        .Calling(c => c.FileWithNullProvider())
                        .ShouldReturn()
                        .File(file => file
                            .WithFileProviderOfType<CustomFileProvider>());
                },
                "When calling FileWithNullProvider action in MvcController expected file result provider to be of CustomFileProvider type, but instead received null.");
        }

        [Fact]
        public void WithFileProviderOfTypeShouldNotThrowExceptionWithNullFileProvider()
        {
            Test.AssertException<FileResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .WithoutValidation()
                        .Calling(c => c.FileWithNullProvider())
                        .ShouldReturn()
                        .File(file => file
                            .WithFileProviderOfType(typeof(CustomFileProvider)));
                },
                "When calling FileWithNullProvider action in MvcController expected file result provider to be of CustomFileProvider type, but instead received null.");
        }
    }
}
