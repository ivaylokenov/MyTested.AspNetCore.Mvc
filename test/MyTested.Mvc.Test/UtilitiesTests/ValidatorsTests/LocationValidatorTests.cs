namespace MyTested.Mvc.Test.UtilitiesTests.ValidatorsTests
{
    using System;
    using Microsoft.AspNetCore.Mvc;
    using Setups;
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

            Test.AssertException<NullReferenceException>(
                () =>
                {
                    LocationValidator.ValidateAndGetWellFormedUriString(
                        uriAsString,
                        TestObjectFactory.GetFailingValidationAction());
                }, 
                "location to be URI valid instead received 'http://somehost!@#?Query==true'");
        }
        
        [Fact]
        public void ValidateUrhouldNotThrowExceptionWithProperUriWithCorrectString()
        {
            var actionResultWithLocation = new CreatedResult(TestObjectFactory.GetUri(), "Test");
            
            LocationValidator.ValidateUri(
                actionResultWithLocation,
                TestObjectFactory.GetUri().OriginalString,
                TestObjectFactory.GetFailingValidationAction());
        }

        [Fact]
        public void ValidateUrhouldThrowExceptionWithIncorrectString()
        {
            Test.AssertException<NullReferenceException>(
                () =>
                {
                    var actionResultWithLocation = new CreatedResult(TestObjectFactory.GetUri(), "Test");

                    LocationValidator.ValidateUri(
                        actionResultWithLocation,
                        "http://somehost.com/",
                        TestObjectFactory.GetFailingValidationAction());
                }, 
                "location to be 'http://somehost.com/' instead received 'http://somehost.com/someuri/1?query=Test'");
        }

        [Fact]
        public void ValidateLocationShouldNotThrowExceptionWithCorrectLocationBuilder()
        {
            var actionResultWithLocation = new CreatedResult(TestObjectFactory.GetUri(), "Test");

            LocationValidator.ValidateLocation(
                actionResultWithLocation,
                location =>
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
                        .WithQuery("?query=Test"),
                TestObjectFactory.GetFailingValidationAction());
        }

        [Fact]
        public void ValidateLocationShouldThrowExceptionWithIncorrectLocationBuilder()
        {
            Test.AssertException<NullReferenceException>(
                () =>
                {
                    var actionResultWithLocation = new CreatedResult(TestObjectFactory.GetUri(), "Test");

                    LocationValidator.ValidateLocation(
                        actionResultWithLocation,
                        location =>
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
                                .WithQuery("?query=Test"),
                        TestObjectFactory.GetFailingValidationAction());
                },
                "URI to be 'http://somehost12.com/someuri/1?query=Test' was in fact 'http://somehost.com/someuri/1?query=Test'");
        }
    }
}
