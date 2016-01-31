namespace MyTested.Mvc.Tests.BuildersTests.ActionResultsTests.JsonTests
{
    using System.Collections.Generic;
    using Exceptions;
    using Newtonsoft.Json;
    using Setups;
    using Setups.Controllers;
    using Setups.Models;
    using Xunit;

    public class JsonTestBuilderTests
    {
        [Fact]
        public void WithResponseModelOfTypeShouldWorkCorrectlyWithJson()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.JsonAction())
                .ShouldReturn()
                .Json()
                .WithResponseModelOfType<ICollection<ResponseModel>>();
        }
        
        [Fact]
        public void WithDefaultJsonSettingsShouldNotThrowExeptionWithDefaultJsonSettings()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.JsonAction())
                .ShouldReturn()
                .Json()
                .WithDefaulJsonSerializerSettings();
        }

        [Fact]
        public void WithJsonSerializerSettingsShouldNotThrowExceptionWithSameJsonSettings()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.JsonWithSettingsAction())
                .ShouldReturn()
                .Json()
                .WithJsonSerializerSettings(TestObjectFactory.GetJsonSerializerSettings());
        }

        [Fact]
        public void WithHttpStatusCodeShouldNotThrowExceptionWithCorrectStatusCode()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.JsonWithStatusCodeAction())
                .ShouldReturn()
                .Json()
                .WithStatusCode(200);
        }

        [Fact]
        public void WithHttpStatusCodeShouldThrowExceptionWithIncorrectStatusCode()
        {
            Test.AssertException<JsonResultAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.JsonWithStatusCodeAction())
                        .ShouldReturn()
                        .Json()
                        .WithStatusCode(500);
                },
                "When calling JsonWithStatusCodeAction action in MvcController expected JSON result to have 500 (InternalServerError) status code, but instead received 200 (OK).");
        }
        
        [Fact]
        public void WithContentTypeShouldNotThrowExceptionWithCorrectContentType()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.JsonWithStatusCodeAction())
                .ShouldReturn()
                .Json()
                .WithContentType(ContentType.ApplicationXml);
        }

        [Fact]
        public void WithContentTypeShouldThrowExceptionWithIncorrectContentType()
        {
            Test.AssertException<JsonResultAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.JsonWithStatusCodeAction())
                        .ShouldReturn()
                        .Json()
                        .WithContentType(ContentType.ApplicationJson);
                },
                "When calling JsonWithStatusCodeAction action in MvcController expected JSON result ContentType to be 'application/json', but instead received 'application/xml'.");
        }

        [Fact]
        public void WithJsonSerializerSettingsShouldThrowExceptionWithDifferentJsonSettings()
        {
            var jsonSerializerSettings = TestObjectFactory.GetJsonSerializerSettings();
            jsonSerializerSettings.DateParseHandling = DateParseHandling.DateTime;

            Test.AssertException<JsonResultAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.JsonWithSettingsAction())
                        .ShouldReturn()
                        .Json()
                        .WithJsonSerializerSettings(jsonSerializerSettings);
                }, 
                "When calling JsonWithSettingsAction action in MvcController expected JSON result serializer settings to have DateTime date parse handling, but in fact found DateTimeOffset.");
        }
        
        [Fact]
        public void AndAlsoShouldWorkCorrectly()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.JsonWithStatusCodeAction())
                .ShouldReturn()
                .Json()
                .WithStatusCode(200)
                .AndAlso()
                .WithContentType(ContentType.ApplicationXml);
        }
    }
}
