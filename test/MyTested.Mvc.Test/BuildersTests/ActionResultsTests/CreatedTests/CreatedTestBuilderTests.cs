namespace MyTested.Mvc.Tests.BuildersTests.ActionResultsTests.CreatedTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Exceptions;
    using Setups;
    using Setups.Controllers;
    using Setups.Models;
    using Xunit;
    
    public class CreatedTestBuilderTests
    {
        // TODO: content negotiator?
        //[Fact]
        //public void WithDefaultContentNegotiatorShouldNotThrowExceptionWhenActionReturnsDefaultContentNegotiator()
        //{
        //    MyMvc
        //        .Controller<MvcController>()
        //        .Calling(c => c.CreatedAction())
        //        .ShouldReturn()
        //        .Created()
        //        .WithDefaultContentNegotiator();
        //}

        //[Fact]
        //[ExpectedException(
        //    typeof(CreatedResultAssertionException),
        //    ExpectedMessage = "When calling CreatedActionWithCustomContentNegotiator action in MvcController expected created result IContentNegotiator to be DefaultContentNegotiator, but instead received CustomContentNegotiator.")]
        //public void WithDefaultContentNegotiatorShouldThrowExceptionWhenActionReturnsNotDefaultContentNegotiator()
        //{
        //    MyMvc
        //        .Controller<MvcController>()
        //        .Calling(c => c.CreatedActionWithCustomContentNegotiator())
        //        .ShouldReturn()
        //        .Created()
        //        .WithDefaultContentNegotiator();
        //}

        //[Fact]
        //public void WithContentNegotiatorShouldNotThrowExceptionWhenActionReturnsCorrectContentNegotiator()
        //{
        //    MyMvc
        //        .Controller<MvcController>()
        //        .Calling(c => c.CreatedActionWithCustomContentNegotiator())
        //        .ShouldReturn()
        //        .Created()
        //        .WithContentNegotiator(new CustomContentNegotiator());
        //}

        //[Fact]
        //[ExpectedException(
        //    typeof(CreatedResultAssertionException),
        //    ExpectedMessage = "When calling CreatedAction action in MvcController expected created result IContentNegotiator to be CustomContentNegotiator, but instead received DefaultContentNegotiator.")]
        //public void WithContentNegotiatorShouldThrowExceptionWhenActionReturnsIncorrectContentNegotiator()
        //{
        //    MyMvc
        //        .Controller<MvcController>()
        //        .Calling(c => c.CreatedAction())
        //        .ShouldReturn()
        //        .Created()
        //        .WithContentNegotiator(new CustomContentNegotiator());
        //}

        //[Fact]
        //public void WithContentNegotiatorOfTypeShouldNotThrowExceptionWhenActionReturnsCorrectContentNegotiator()
        //{
        //    MyMvc
        //        .Controller<MvcController>()
        //        .Calling(c => c.CreatedActionWithCustomContentNegotiator())
        //        .ShouldReturn()
        //        .Created()
        //        .WithContentNegotiatorOfType<CustomContentNegotiator>();
        //}

        //[Fact]
        //[ExpectedException(
        //    typeof(CreatedResultAssertionException),
        //    ExpectedMessage = "When calling CreatedAction action in MvcController expected created result IContentNegotiator to be CustomContentNegotiator, but instead received DefaultContentNegotiator.")]
        //public void WithContentNegotiatorOfTypeShouldThrowExceptionWhenActionReturnsIncorrectContentNegotiator()
        //{
        //    MyMvc
        //        .Controller<MvcController>()
        //        .Calling(c => c.CreatedAction())
        //        .ShouldReturn()
        //        .Created()
        //        .WithContentNegotiatorOfType<CustomContentNegotiator>();
        //}

        [Fact]
        public void AtLocationWithStringShouldNotThrowExceptionIfTheLocationIsCorrect()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.CreatedAction())
                .ShouldReturn()
                .Created()
                .AtLocation("http://somehost.com/someuri/1?query=Test");
        }

        [Fact]
        public void AtLocationWithStringShouldThrowExceptionIfTheLocationIsIncorrect()
        {
            Test.AssertException<CreatedResultAssertionException>(() =>
            {
                MyMvc
                    .Controller<MvcController>()
                    .Calling(c => c.CreatedAction())
                    .ShouldReturn()
                    .Created()
                    .AtLocation("http://somehost.com/");
            }, "When calling CreatedAction action in MvcController expected created result location to be http://somehost.com/, but instead received http://somehost.com/someuri/1?query=Test.");
        }

        [Fact]
        public void AtLocationWithStringShouldThrowExceptionIfTheLocationIsNotValid()
        {
            Test.AssertException<CreatedResultAssertionException>(() =>
            {
                MyMvc
                    .Controller<MvcController>()
                    .Calling(c => c.CreatedAction())
                    .ShouldReturn()
                    .Created()
                    .AtLocation("http://somehost!@#?Query==true");
            }, "When calling CreatedAction action in MvcController expected created result location to be URI valid, but instead received http://somehost!@#?Query==true.");
        }

        [Fact]
        public void AtLocationWithUriShouldNotThrowExceptionIfTheLocationIsCorrect()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.CreatedAction())
                .ShouldReturn()
                .Created()
                .AtLocation(new Uri("http://somehost.com/someuri/1?query=Test"));
        }

        [Fact]
        public void AtLocationWithUriShouldThrowExceptionIfTheLocationIsIncorrect()
        {
            Test.AssertException<CreatedResultAssertionException>(() =>
            {
                MyMvc
                    .Controller<MvcController>()
                    .Calling(c => c.CreatedAction())
                    .ShouldReturn()
                    .Created()
                    .AtLocation(new Uri("http://somehost.com/"));
            }, "When calling CreatedAction action in MvcController expected created result location to be http://somehost.com/, but instead received http://somehost.com/someuri/1?query=Test.");
        }

        [Fact]
        public void AtLocationWithBuilderShouldNotThrowExceptionIfTheLocationIsCorrect()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.CreatedAction())
                .ShouldReturn()
                .Created()
                .AtLocation(location =>
                    location
                        .WithHost("somehost.com")
                        .AndAlso()
                        .WithAbsolutePath("/someuri/1")
                        .AndAlso()
                        .WithPort(80)
                        .AndAlso()
                        .WithScheme("http")
                        .AndAlso()
                        .WithFragment(string.Empty)
                        .AndAlso()
                        .WithQuery("?query=Test"));
        }

        [Fact]
        public void AtLocationWithBuilderShouldThrowExceptionIfTheLocationIsIncorrect()
        {
            Test.AssertException<CreatedResultAssertionException>(() =>
            {
                MyMvc
                    .Controller<MvcController>()
                    .Calling(c => c.CreatedAction())
                    .ShouldReturn()
                    .Created()
                    .AtLocation(location =>
                        location
                            .WithHost("somehost12.com")
                            .AndAlso()
                            .WithAbsolutePath("/someuri/1")
                            .AndAlso()
                            .WithPort(80)
                            .AndAlso()
                            .WithScheme("http")
                            .AndAlso()
                            .WithFragment(string.Empty)
                            .AndAlso()
                            .WithQuery("?query=Test"));
            }, "When calling CreatedAction action in MvcController expected created result URI to equal the provided one, but was in fact different.");
        }

        // TODO: formatters
        //[Fact]
        //public void ContainingMediaTypeFormatterShouldNotThrowExceptionWhenActionResultHasTheProvidedMediaTypeFormatter()
        //{
        //    MyMvc
        //        .Controller<MvcController>()
        //        .Calling(c => c.CreatedActionWithCustomContentNegotiator())
        //        .ShouldReturn()
        //        .Created()
        //        .ContainingMediaTypeFormatter(new JsonMediaTypeFormatter());
        //}

        //[Fact]
        //[ExpectedException(
        //    typeof(CreatedResultAssertionException),
        //    ExpectedMessage = "When calling CreatedAction action in MvcController expected created result Formatters to contain CustomMediaTypeFormatter, but none was found.")]
        //public void ContainingMediaTypeFormatterShouldThrowExceptionWhenActionResultDoesNotHaveTheProvidedMediaTypeFormatter()
        //{
        //    MyMvc
        //        .Controller<MvcController>()
        //        .Calling(c => c.CreatedAction())
        //        .ShouldReturn()
        //        .Created()
        //        .ContainingMediaTypeFormatter(TestObjectFactory.GetCustomMediaTypeFormatter());
        //}

        //[Fact]
        //public void ContainingMediaTypeOfTypeFormatterShouldNotThrowExceptionWhenActionResultHasTheProvidedMediaTypeFormatter()
        //{
        //    MyMvc
        //        .Controller<MvcController>()
        //        .Calling(c => c.CreatedActionWithCustomContentNegotiator())
        //        .ShouldReturn()
        //        .Created()
        //        .ContainingMediaTypeFormatterOfType<JsonMediaTypeFormatter>();
        //}

        //[Fact]
        //[ExpectedException(
        //    typeof(CreatedResultAssertionException),
        //    ExpectedMessage = "When calling CreatedAction action in MvcController expected created result Formatters to contain CustomMediaTypeFormatter, but none was found.")]
        //public void ContainingMediaTypeFormatterOfTypeShouldThrowExceptionWhenActionResultDoesNotHaveTheProvidedMediaTypeFormatter()
        //{
        //    MyMvc
        //        .Controller<MvcController>()
        //        .Calling(c => c.CreatedAction())
        //        .ShouldReturn()
        //        .Created()
        //        .ContainingMediaTypeFormatterOfType<CustomMediaTypeFormatter>();
        //}

        //[Fact]
        //public void ContainingDefaultFormattersShouldNotThrowExceptionWhenActionResultHasDefaultMediaTypeFormatters()
        //{
        //    MyMvc
        //        .Controller<MvcController>()
        //        .Calling(c => c.CreatedAction())
        //        .ShouldReturn()
        //        .Created()
        //        .ContainingDefaultFormatters();
        //}

        //[Fact]
        //[ExpectedException(
        //    typeof(CreatedResultAssertionException),
        //    ExpectedMessage = "When calling CreatedActionWithCustomContentNegotiator action in MvcController expected created result Formatters to be 4, but instead found 5.")]
        //public void ContainingDefaultFormattersShouldThrowExceptionWhenActionResultDoesNotHaveDefaultMediaTypeFormatter()
        //{
        //    MyMvc
        //        .Controller<MvcController>()
        //        .Calling(c => c.CreatedActionWithCustomContentNegotiator())
        //        .ShouldReturn()
        //        .Created()
        //        .ContainingDefaultFormatters();
        //}

        //[Fact]
        //public void ContainingFormattersShouldNotThrowExceptionWhenActionResultHasCorrectMediaTypeFormatters()
        //{
        //    MyMvc
        //        .Controller<MvcController>()
        //        .Calling(c => c.CreatedActionWithCustomContentNegotiator())
        //        .ShouldReturn()
        //        .Created()
        //        .ContainingMediaTypeFormatters(TestObjectFactory.GetFormatters().Reverse());
        //}

        //[Fact]
        //[ExpectedException(
        //    typeof(CreatedResultAssertionException),
        //    ExpectedMessage = "When calling CreatedActionWithCustomContentNegotiator action in MvcController expected created result Formatters to be 4, but instead found 5.")]
        //public void ContainingFormattersShouldThrowExceptionWhenActionResultHasDifferentCountOfMediaTypeFormatters()
        //{
        //    var mediaTypeFormatters = TestObjectFactory.GetFormatters().ToList();
        //    mediaTypeFormatters.Remove(mediaTypeFormatters.Last());

        //    MyMvc
        //        .Controller<MvcController>()
        //        .Calling(c => c.CreatedActionWithCustomContentNegotiator())
        //        .ShouldReturn()
        //        .Created()
        //        .ContainingMediaTypeFormatters(mediaTypeFormatters);
        //}

        //[Fact]
        //[ExpectedException(
        //    typeof(CreatedResultAssertionException),
        //    ExpectedMessage = "When calling CreatedActionWithCustomContentNegotiator action in MvcController expected created result Formatters to have CustomMediaTypeFormatter, but none was found.")]
        //public void ContainingFormattersShouldThrowExceptionWhenActionResultHasDifferentTypeOfMediaTypeFormatters()
        //{
        //    var mediaTypeFormatters = TestObjectFactory.GetFormatters().ToList();
        //    mediaTypeFormatters.Remove(mediaTypeFormatters.Last());
        //    mediaTypeFormatters.Add(new CustomMediaTypeFormatter());

        //    MyMvc
        //        .Controller<MvcController>()
        //        .Calling(c => c.CreatedActionWithCustomContentNegotiator())
        //        .ShouldReturn()
        //        .Created()
        //        .ContainingMediaTypeFormatters(mediaTypeFormatters);
        //}

        //[Fact]
        //public void ContainingFormattersShouldNotThrowExceptionWhenActionResultHasCorrectMediaTypeFormattersAsParams()
        //{
        //    var mediaTypeFormatters = TestObjectFactory.GetFormatters().ToList();

        //    MyMvc
        //        .Controller<MvcController>()
        //        .Calling(c => c.CreatedActionWithCustomContentNegotiator())
        //        .ShouldReturn()
        //        .Created()
        //        .ContainingMediaTypeFormatters(
        //            mediaTypeFormatters[0],
        //            mediaTypeFormatters[1],
        //            mediaTypeFormatters[2],
        //            mediaTypeFormatters[3],
        //            mediaTypeFormatters[4]);
        //}

        //[Fact]
        //public void ContainingFormattersWithBuilderShouldNotThrowExceptionWhenActionResultHasCorrectMediaTypeFormatters()
        //{
        //    MyMvc
        //        .Controller<MvcController>()
        //        .Calling(c => c.CreatedActionWithCustomContentNegotiator())
        //        .ShouldReturn()
        //        .Created()
        //        .ContainingMediaTypeFormatters(
        //            formatters => formatters
        //                .ContainingMediaTypeFormatter(new JsonMediaTypeFormatter())
        //                .AndAlso()
        //                .ContainingMediaTypeFormatterOfType<FormUrlEncodedMediaTypeFormatter>());
        //}

        //[Fact]
        //[ExpectedException(
        //    typeof(CreatedResultAssertionException),
        //    ExpectedMessage = "When calling CreatedActionWithCustomContentNegotiator action in MvcController expected created result Formatters to contain CustomMediaTypeFormatter, but none was found.")]
        //public void ContainingFormattersWithBuilderShouldThrowExceptionWhenActionResultHasIncorrectMediaTypeFormatters()
        //{
        //    MyMvc
        //        .Controller<MvcController>()
        //        .Calling(c => c.CreatedActionWithCustomContentNegotiator())
        //        .ShouldReturn()
        //        .Created()
        //        .ContainingMediaTypeFormatters(
        //            formatters => formatters
        //                .ContainingMediaTypeFormatterOfType<CustomMediaTypeFormatter>());
        //}

        //[Fact]
        //public void AndAlsoShouldWorkCorrectly()
        //{
        //    MyMvc
        //        .Controller<MvcController>()
        //        .Calling(c => c.CreatedActionWithCustomContentNegotiator())
        //        .ShouldReturn()
        //        .Created()
        //        .AtLocation(TestObjectFactory.GetUri())
        //        .AndAlso()
        //        .ContainingMediaTypeFormatterOfType<JsonMediaTypeFormatter>();
        //}

        [Fact]
        public void WithResponseModelOfTypeShouldWorkCorrectly()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.CreatedAction())
                .ShouldReturn()
                .Created()
                .WithResponseModelOfType<ICollection<ResponseModel>>();
        }

        [Fact]
        public void AtShouldWorkCorrectlyWithCorrectActionCall()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.CreatedAtRouteAction())
                .ShouldReturn()
                .Created()
                .At<NoAttributesController>(c => c.WithParameter(1));
        }

        [Fact]
        public void AtShouldWorkCorrectlyWithCorrectVoidActionCall()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.CreatedAtRouteVoidAction())
                .ShouldReturn()
                .Created()
                .At<NoAttributesController>(c => c.VoidAction());
        }

        // TODO: routing is not implemented
        //[Fact]
        //[ExpectedException(
        //    typeof(CreatedResultAssertionException),
        //    ExpectedMessage = "When calling CreatedAtRouteAction action in MvcController expected created result to redirect to '/api/Redirect/WithParameter?id=2', but in fact redirected to '/api/Redirect/WithParameter?id=1'.")]
        //public void AtShouldThrowExceptionWithIncorrectActionParameter()
        //{
        //    MyMvc
        //        .Controller<MvcController>()
        //        .Calling(c => c.CreatedAtRouteAction())
        //        .ShouldReturn()
        //        .Created()
        //        .At<NoAttributesController>(c => c.WithParameter(2));
        //}

        //[Fact]
        //[ExpectedException(
        //    typeof(CreatedResultAssertionException),
        //    ExpectedMessage = "When calling CreatedAtRouteAction action in MvcController expected created result to redirect to a specific URI, but such URI could not be resolved from the 'Redirect' route template.")]
        //public void AtShouldThrowExceptionWithIncorrectActionCall()
        //{
        //    MyMvc
        //        .Controller<MvcController>()
        //        .Calling(c => c.CreatedAtRouteAction())
        //        .ShouldReturn()
        //        .Created()
        //        .At<RouteController>(c => c.VoidAction());
        //}

        //[Fact]
        //[ExpectedException(
        //    typeof(InvalidCallAssertionException),
        //    ExpectedMessage = "Expected action result to contain a 'RouteName' property to test, but in fact such property was not found.")]
        //public void AtShouldThrowExceptionWithIncorrectActionResult()
        //{
        //    MyMvc
        //        .Controller<MvcController>()
        //        .Calling(c => c.CreatedAction())
        //        .ShouldReturn()
        //        .Created()
        //        .At<RouteController>(c => c.VoidAction());
        //}
    }
}
