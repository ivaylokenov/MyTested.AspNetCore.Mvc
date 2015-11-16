namespace MyTested.Mvc.Test.UtilitiesTests.ValidatorsTests
{
    using System;
    using Setups;
    using Setups.Controllers;
    using Utilities.Validators;
    using Xunit;
    
    public class LocationValidatorTests
    {
        [Fact]
        public void ValidateAndGetWellFormedUrtringShouldReturnProperUriWithCorrectString()
        {
            string uriAsString = "http://somehost.com/someuri/1?query=Test";

            var uri = LocationValidator.ValidateAndGetWellFormedUriString(
                uriAsString,
                TestObjectFactory.GetFailingValidationAction());
            
            Assert.NotNull(uri);
            Assert.Equal(uriAsString, uri.OriginalString);
        }

        [Fact]
        public void ValidateAndGetWellFormedUrtringShouldThrowExceptionWithIncorrectString()
        {
            string uriAsString = "http://somehost!@#?Query==true";

            var exception = Assert.Throws<NullReferenceException>(() =>
                LocationValidator.ValidateAndGetWellFormedUriString(
                    uriAsString,
                    TestObjectFactory.GetFailingValidationAction()));

            Assert.Equal("location to be URI valid instead received http://somehost!@#?Query==true", exception.Message);
        }

        // TODO: ?
        //[Fact]
        //public void ValidateUrhouldNotThrowExceptionWithProperUriWithCorrectString()
        //{
        //    var actionResultWithLocation = new CreatedNegotiatedContentResult<int>(
        //        TestObjectFactory.GetUri(), 5, MyWebApi.Controller<WebApiController>().AndProvideTheController());

        //    LocationValidator.ValidateUri(
        //        actionResultWithLocation,
        //        TestObjectFactory.GetUri(),
        //        TestObjectFactory.GetFailingValidationAction());
        //}

        //[Fact]
        //[ExpectedException(
        //   typeof(NullReferenceException),
        //   ExpectedMessage = "location to be http://somehost.com/ instead received http://somehost.com/someuri/1?query=Test")]
        //public void ValidateUrhouldThrowExceptionWithIncorrectString()
        //{
        //    var actionResultWithLocation = new CreatedNegotiatedContentResult<int>(
        //        TestObjectFactory.GetUri(), 5, MyWebApi.Controller<WebApiController>().AndProvideTheController());

        //    LocationValidator.ValidateUri(
        //        actionResultWithLocation,
        //        new Uri("http://somehost.com/"),
        //        TestObjectFactory.GetFailingValidationAction());
        //}

        //[Fact]
        //public void ValidateLocationShouldNotThrowExceptionWithCorrectLocationBuilder()
        //{
        //    var actionResultWithLocation = new CreatedNegotiatedContentResult<int>(
        //        TestObjectFactory.GetUri(), 5, MyWebApi.Controller<WebApiController>().AndProvideTheController());

        //    LocationValidator.ValidateLocation(
        //        actionResultWithLocation,
        //        location =>
        //            location
        //                .WithHost("somehost.com")
        //                .AndAlso()
        //                .WithAbsolutePath("/someuri/1")
        //                .AndAlso()
        //                .WithPort(80)
        //                .AndAlso()
        //                .WithScheme("http")
        //                .AndAlso()
        //                .WithFragment(string.Empty)
        //                .AndAlso()
        //                .WithQuery("?query=Test"),
        //        TestObjectFactory.GetFailingValidationAction());
        //}

        //[Fact]
        //[ExpectedException(
        //   typeof(NullReferenceException),
        //   ExpectedMessage = "URI to equal the provided one was in fact different")]
        //public void ValidateLocationShouldThrowExceptionWithIncorrectLocationBuilder()
        //{
        //    var actionResultWithLocation = new CreatedNegotiatedContentResult<int>(
        //        TestObjectFactory.GetUri(), 5, MyWebApi.Controller<WebApiController>().AndProvideTheController());

        //    LocationValidator.ValidateLocation(
        //        actionResultWithLocation,
        //        location =>
        //            location
        //                .WithHost("somehost12.com")
        //                .AndAlso()
        //                .WithAbsolutePath("/someuri/1")
        //                .AndAlso()
        //                .WithPort(80)
        //                .AndAlso()
        //                .WithScheme("http")
        //                .AndAlso()
        //                .WithFragment(string.Empty)
        //                .AndAlso()
        //                .WithQuery("?query=Test"),
        //        TestObjectFactory.GetFailingValidationAction());
        //}
    }
}
