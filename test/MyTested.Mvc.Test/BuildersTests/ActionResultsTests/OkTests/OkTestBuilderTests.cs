namespace MyTested.Mvc.Tests.BuildersTests.ActionResultsTests.OkTests
{
    using System.Linq;
    using Exceptions;
    using Setups;
    using Setups.Controllers;
    using Xunit;
    
    public class OkTestBuilderTests
    {
        // TODO: 
        //[Fact]
        //public void WithDefaultContentNegotiatorShouldNotThrowExceptionWhenActionReturnsDefaultContentNegotiator()
        //{
        //    MyMvc
        //        .Controller<MvcController>()
        //        .Calling(c => c.OkResultWithResponse())
        //        .ShouldReturn()
        //        .Ok()
        //        .WithDefaultContentNegotiator();
        //}

        //[Fact]
        //[ExpectedException(
        //    typeof(OkResultAssertionException),
        //    ExpectedMessage = "When calling OkResultWithContentNegotiatorAction action in MvcController expected ok result IContentNegotiator to be DefaultContentNegotiator, but instead received CustomContentNegotiator.")]
        //public void WithDefaultContentNegotiatorShouldThrowExceptionWhenActionReturnsNotDefaultContentNegotiator()
        //{
        //    MyMvc
        //        .Controller<MvcController>()
        //        .Calling(c => c.OkResultWithContentNegotiatorAction())
        //        .ShouldReturn()
        //        .Ok()
        //        .WithDefaultContentNegotiator();
        //}

        //[Fact]
        //public void WithContentNegotiatorShouldNotThrowExceptionWhenActionReturnsCorrectContentNegotiator()
        //{
        //    MyMvc
        //        .Controller<MvcController>()
        //        .Calling(c => c.OkResultWithContentNegotiatorAction())
        //        .ShouldReturn()
        //        .Ok()
        //        .WithContentNegotiator(new CustomContentNegotiator());
        //}

        //[Fact]
        //public void WithContentNegotiatorOfTypeShouldNotThrowExceptionWhenActionReturnsCorrectContentNegotiator()
        //{
        //    MyMvc
        //        .Controller<MvcController>()
        //        .Calling(c => c.OkResultWithContentNegotiatorAction())
        //        .ShouldReturn()
        //        .Ok()
        //        .WithContentNegotiatorOfType<CustomContentNegotiator>();
        //}

        //[Fact]
        //public void ContainingMediaTypeFormatterShouldNotThrowExceptionWhenActionResultHasTheProvidedMediaTypeFormatter()
        //{
        //    MyMvc
        //        .Controller<MvcController>()
        //        .Calling(c => c.OkResultWithContentNegotiatorAction())
        //        .ShouldReturn()
        //        .Ok()
        //        .ContainingMediaTypeFormatter(new JsonMediaTypeFormatter());
        //}

        //[Fact]
        //public void ContainingMediaTypeOfTypeFormatterShouldNotThrowExceptionWhenActionResultHasTheProvidedMediaTypeFormatter()
        //{
        //    MyMvc
        //        .Controller<MvcController>()
        //        .Calling(c => c.OkResultWithContentNegotiatorAction())
        //        .ShouldReturn()
        //        .Ok()
        //        .ContainingMediaTypeFormatterOfType<JsonMediaTypeFormatter>();
        //}

        //[Fact]
        //public void ContainingDefaultFormattersShouldNotThrowExceptionWhenActionResultHasDefaultMediaTypeFormatters()
        //{
        //    MyMvc
        //        .Controller<MvcController>()
        //        .Calling(c => c.OkResultWithResponse())
        //        .ShouldReturn()
        //        .Ok()
        //        .ContainingDefaultFormatters();
        //}

        //[Fact]
        //[ExpectedException(typeof(
        //    OkResultAssertionException),
        //    ExpectedMessage = "When calling OkResultWithContentNegotiatorAction action in MvcController expected ok result Formatters to be 4, but instead found 5.")]
        //public void ContainingDefaultFormattersShouldThrowExceptionWhenActionResultHasNotDefaultMediaTypeFormatters()
        //{
        //    MyMvc
        //        .Controller<MvcController>()
        //        .Calling(c => c.OkResultWithContentNegotiatorAction())
        //        .ShouldReturn()
        //        .Ok()
        //        .ContainingDefaultFormatters();
        //}

        //[Fact]
        //public void ContainingFormattersShouldNotThrowExceptionWhenActionResultHasCorrectMediaTypeFormatters()
        //{
        //    MyMvc
        //        .Controller<MvcController>()
        //        .Calling(c => c.OkResultWithContentNegotiatorAction())
        //        .ShouldReturn()
        //        .Ok()
        //        .ContainingMediaTypeFormatters(TestObjectFactory.GetFormatters().Reverse());
        //}

        //[Fact]
        //public void ContainingFormattersWithBuilderShouldNotThrowExceptionWhenActionResultHasCorrectMediaTypeFormatters()
        //{
        //    MyMvc
        //        .Controller<MvcController>()
        //        .Calling(c => c.OkResultWithContentNegotiatorAction())
        //        .ShouldReturn()
        //        .Ok()
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
        //        .Calling(c => c.OkResultWithContentNegotiatorAction())
        //        .ShouldReturn()
        //        .Ok()
        //        .ContainingMediaTypeFormatters(
        //            mediaTypeFormatters[0],
        //            mediaTypeFormatters[1],
        //            mediaTypeFormatters[2],
        //            mediaTypeFormatters[3],
        //            mediaTypeFormatters[4]);
        //}

        //[Fact]
        //public void AndAlsoShouldWorkCorrectly()
        //{
        //    MyMvc
        //        .Controller<MvcController>()
        //        .Calling(c => c.OkResultWithContentNegotiatorAction())
        //        .ShouldReturn()
        //        .Ok()
        //        .ContainingMediaTypeFormatterOfType<JsonMediaTypeFormatter>()
        //        .AndAlso()
        //        .WithResponseModelOfType<int>();
        //}
    }
}
