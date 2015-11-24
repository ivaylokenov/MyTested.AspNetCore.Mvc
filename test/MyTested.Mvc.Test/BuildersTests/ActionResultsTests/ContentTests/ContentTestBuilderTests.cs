namespace MyTested.Mvc.Tests.BuildersTests.ActionResultsTests.ContentTests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using Exceptions;
    using Setups;
    using Setups.Controllers;
    using Setups.Models;
    using Xunit;
    
    public class ContentTestBuilderTests
    {
        [Fact]
        public void WithStatusCodeShouldNotThrowExceptionWhenActionReturnsCorrectStatusCode()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.ContentAction())
                .ShouldReturn()
                .Content()
                .WithStatusCode(HttpStatusCode.OK);
        }

        [Fact]
        public void WithStatusCodeShouldThrowExceptionWhenActionReturnsWrongStatusCode()
        {
            Test.AssertException<ContentResultAssertionException>(() =>
            {
                MyMvc
                    .Controller<MvcController>()
                    .Calling(c => c.ContentAction())
                    .ShouldReturn()
                    .Content()
                    .WithStatusCode(HttpStatusCode.NotFound);
            }, "When calling ContentAction action in MvcController expected to have 404 (NotFound) status code, but received 200 (OK).");
        }

        [Fact]
        public void WithMediaTypeShouldNotThrowExceptionWithString()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.ContentActionWithMediaType())
                .ShouldReturn()
                .Content()
                .WithMediaType(TestObjectFactory.MediaType);
        }

        // TODO: media type and response model
        //[Fact]
        //public void WithMediaTypeShouldNotThrowExceptionWithMediaTypeHeaderValue()
        //{
        //    MyMvc
        //        .Controller<MvcController>()
        //        .Calling(c => c.ContentActionWithMediaType())
        //        .ShouldReturn()
        //        .Content()
        //        .WithMediaType(new MediaTypeHeaderValue(TestObjectFactory.MediaType));
        //}

        //[Fact]
        //public void WithMediaTypeShouldNotThrowExceptionWithMediaTypeHeaderValueConstant()
        //{
        //    MyMvc
        //        .Controller<MvcController>()
        //        .Calling(c => c.ContentActionWithMediaType())
        //        .ShouldReturn()
        //        .Content()
        //        .WithMediaType(MediaType.ApplicationJson);
        //}

        //[Fact]
        //[ExpectedException(
        //    typeof(ContentResultAssertionException),
        //    ExpectedMessage = "When calling ContentActionWithMediaType action in MvcController expected content result MediaType to be text/plain, but instead received application/json.")]
        //public void WithMediaTypeShouldThrowExceptionWithMediaTypeHeaderValue()
        //{
        //    MyMvc
        //        .Controller<MvcController>()
        //        .Calling(c => c.ContentActionWithMediaType())
        //        .ShouldReturn()
        //        .Content()
        //        .WithMediaType(new MediaTypeHeaderValue("text/plain"));
        //}

        //[Fact]
        //[ExpectedException(
        //    typeof(ContentResultAssertionException),
        //    ExpectedMessage = "When calling ContentActionWithMediaType action in MvcController expected content result MediaType to be null, but instead received application/json.")]
        //public void WithMediaTypeShouldThrowExceptionWithNullMediaTypeHeaderValue()
        //{
        //    MyMvc
        //        .Controller<MvcController>()
        //        .Calling(c => c.ContentActionWithMediaType())
        //        .ShouldReturn()
        //        .Content()
        //        .WithMediaType((MediaTypeHeaderValue)null);
        //}

        //[Fact]
        //public void WithMediaTypeShouldThrowExceptionWithNullMediaTypeHeaderValueAndNullActual()
        //{
        //    MyMvc
        //        .Controller<MvcController>()
        //        .Calling(c => c.ContentActionWithNullMediaType())
        //        .ShouldReturn()
        //        .Content()
        //        .WithMediaType((MediaTypeHeaderValue)null);
        //}

        //[Fact]
        //[ExpectedException(
        //    typeof(ContentResultAssertionException),
        //    ExpectedMessage = "When calling ContentActionWithNullMediaType action in MvcController expected content result MediaType to be application/json, but instead received null.")]
        //public void WithMediaTypeShouldThrowExceptionWithMediaTypeHeaderValueAndNullActual()
        //{
        //    MyMvc
        //        .Controller<MvcController>()
        //        .Calling(c => c.ContentActionWithNullMediaType())
        //        .ShouldReturn()
        //        .Content()
        //        .WithMediaType(new MediaTypeHeaderValue(TestObjectFactory.MediaType));
        //}

        //[Fact]
        //public void WithDefaultContentNegotiatorShouldNotThrowExceptionWhenActionReturnsDefaultContentNegotiator()
        //{
        //    MyMvc
        //        .Controller<MvcController>()
        //        .Calling(c => c.ContentAction())
        //        .ShouldReturn()
        //        .Content()
        //        .WithDefaultContentNegotiator();
        //}

        //[Fact]
        //[ExpectedException(
        //    typeof(ContentResultAssertionException),
        //    ExpectedMessage = "When calling ContentActionWithCustomFormatters action in MvcController expected content result IContentNegotiator to be DefaultContentNegotiator, but instead received CustomContentNegotiator.")]
        //public void WithDefaultContentNegotiatorShouldThrowExceptionWhenActionReturnsNotDefaultContentNegotiator()
        //{
        //    MyMvc
        //        .Controller<MvcController>()
        //        .Calling(c => c.ContentActionWithCustomFormatters())
        //        .ShouldReturn()
        //        .Content()
        //        .WithDefaultContentNegotiator();
        //}

        //[Fact]
        //public void WithContentNegotiatorShouldNotThrowExceptionWhenActionReturnsCorrectContentNegotiator()
        //{
        //    MyMvc
        //        .Controller<MvcController>()
        //        .Calling(c => c.ContentActionWithCustomFormatters())
        //        .ShouldReturn()
        //        .Content()
        //        .WithContentNegotiator(new CustomContentNegotiator());
        //}

        //[Fact]
        //public void WithContentNegotiatorOfTypeShouldNotThrowExceptionWhenActionReturnsCorrectContentNegotiator()
        //{
        //    MyMvc
        //        .Controller<MvcController>()
        //        .Calling(c => c.ContentActionWithCustomFormatters())
        //        .ShouldReturn()
        //        .Content()
        //        .WithContentNegotiatorOfType<CustomContentNegotiator>();
        //}

        //[Fact]
        //public void ContainingMediaTypeFormatterShouldNotThrowExceptionWhenActionResultHasTheProvidedMediaTypeFormatter()
        //{
        //    MyMvc
        //        .Controller<MvcController>()
        //        .Calling(c => c.ContentActionWithMediaType())
        //        .ShouldReturn()
        //        .Content()
        //        .ContainingMediaTypeFormatter(TestObjectFactory.GetCustomMediaTypeFormatter());
        //}

        //[Fact]
        //public void ContainingMediaTypeOfTypeFormatterShouldNotThrowExceptionWhenActionResultHasTheProvidedMediaTypeFormatter()
        //{
        //    MyMvc
        //        .Controller<MvcController>()
        //        .Calling(c => c.ContentActionWithCustomFormatters())
        //        .ShouldReturn()
        //        .Content()
        //        .ContainingMediaTypeFormatterOfType<JsonMediaTypeFormatter>();
        //}

        //[Fact]
        //public void ContainingDefaultFormattersShouldNotThrowExceptionWhenActionResultHasDefaultMediaTypeFormatters()
        //{
        //    MyMvc
        //        .Controller<MvcController>()
        //        .Calling(c => c.ContentAction())
        //        .ShouldReturn()
        //        .Content()
        //        .ContainingDefaultFormatters();
        //}

        //[Fact]
        //[ExpectedException(typeof(
        //    ContentResultAssertionException),
        //    ExpectedMessage = "When calling ContentActionWithMediaType action in MvcController expected content result Formatters to be 4, but instead found 1.")]
        //public void ContainingDefaultFormattersShouldThrowExceptionWhenActionResultHasNotDefaultMediaTypeFormatters()
        //{
        //    MyMvc
        //        .Controller<MvcController>()
        //        .Calling(c => c.ContentActionWithMediaType())
        //        .ShouldReturn()
        //        .Content()
        //        .ContainingDefaultFormatters();
        //}

        //[Fact]
        //public void ContainingFormattersShouldNotThrowExceptionWhenActionResultHasCorrectMediaTypeFormatters()
        //{
        //    MyMvc
        //        .Controller<MvcController>()
        //        .Calling(c => c.ContentActionWithCustomFormatters())
        //        .ShouldReturn()
        //        .Content()
        //        .ContainingMediaTypeFormatters(TestObjectFactory.GetFormatters().Reverse());
        //}

        //[Fact]
        //public void ContainingFormattersWithBuilderShouldNotThrowExceptionWhenActionResultHasCorrectMediaTypeFormatters()
        //{
        //    MyMvc
        //        .Controller<MvcController>()
        //        .Calling(c => c.ContentAction())
        //        .ShouldReturn()
        //        .Content()
        //        .ContainingMediaTypeFormatters(
        //            formatters => formatters
        //                .ContainingMediaTypeFormatter(new JsonMediaTypeFormatter())
        //                .AndAlso()
        //                .ContainingMediaTypeFormatterOfType<FormUrlEncodedMediaTypeFormatter>());
        //}

        //[Fact]
        //public void ContainingFormattersShouldNotThrowExceptionWhenActionResultHasCorrectMediaTypeFormattersAsParams()
        //{
        //    var mediaTypeFormatters = TestObjectFactory.GetFormatters().ToList();

        //    MyMvc
        //        .Controller<MvcController>()
        //        .Calling(c => c.ContentActionWithCustomFormatters())
        //        .ShouldReturn()
        //        .Content()
        //        .ContainingMediaTypeFormatters(
        //            mediaTypeFormatters[0],
        //            mediaTypeFormatters[1],
        //            mediaTypeFormatters[2],
        //            mediaTypeFormatters[3],
        //            mediaTypeFormatters[4]);
        //}

        [Fact]
        public void AndAlsoShouldWorkCorrectly()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.ContentAction())
                .ShouldReturn()
                .Content()
                .WithStatusCode(HttpStatusCode.OK)
                .AndAlso()
                .WithResponseModelOfType<ICollection<ResponseModel>>();
        }

        [Fact]
        public void WithResponseModelOfTypeShouldWorkCorrectly()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.ContentAction())
                .ShouldReturn()
                .Content()
                .WithResponseModelOfType<ICollection<ResponseModel>>();
        }
    }
}
