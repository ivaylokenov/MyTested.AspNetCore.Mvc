namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.ActionResultsTests.JsonTests
{
    using Exceptions;
    using Newtonsoft.Json;
    using Setups;
    using Setups.Controllers;
    using Xunit;

    public class JsonTestBuilderTests
    {
        [Fact]
        public void WithDefaultJsonSettingsShouldNotThrowExceptionWithDefaultJsonSettings()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.JsonAction())
                .ShouldReturn()
                .Json(json => json
                    .WithDefaultJsonSerializerSettings());
        }

        [Fact]
        public void WithJsonSerializerSettingsShouldNotThrowExceptionWithSameJsonSettings()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.JsonWithSettingsAction())
                .ShouldReturn()
                .Json(json => json
                    .WithJsonSerializerSettings(TestObjectFactory.GetJsonSerializerSettings()));
        }
        
        [Fact]
        public void WithJsonSerializerSettingsShouldThrowExceptionWithDifferentJsonSettings()
        {
            var jsonSerializerSettings = TestObjectFactory.GetJsonSerializerSettings();
            jsonSerializerSettings.DateParseHandling = DateParseHandling.DateTime;

            Test.AssertException<JsonResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.JsonWithSettingsAction())
                        .ShouldReturn()
                        .Json(json => json
                            .WithJsonSerializerSettings(jsonSerializerSettings));
                }, 
                "When calling JsonWithSettingsAction action in MvcController expected JSON result serializer settings to have DateTime date parse handling, but in fact found DateTimeOffset.");
        }
        
        [Fact]
        public void AndAlsoShouldWorkCorrectlyJsonActionResults()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.JsonWithStatusCodeAction())
                .ShouldReturn()
                .Json(json => json
                    .WithStatusCode(200)
                    .AndAlso()
                    .WithContentType(ContentType.ApplicationXml));
        }
    }
}
