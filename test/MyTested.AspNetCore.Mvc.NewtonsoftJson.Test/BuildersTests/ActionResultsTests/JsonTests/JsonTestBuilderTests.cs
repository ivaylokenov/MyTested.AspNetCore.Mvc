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
        public void WithDefaultJsonSettingsShouldNotThrowExceptionAndPassAssertions()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.JsonAction())
                .ShouldReturn()
                .Json(result => result
                    .WithDefaultJsonSerializerSettings()
                    .AndAlso()
                    .Passing(json => 
                    {
                        Assert.Null(json.SerializerSettings);
                    }));
        }

        [Fact]
        public void WithDefaultJsonSettingsShouldWorkCorrectlyWithNull()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.JsonWithSpecificSettingsAction(null))
                .ShouldReturn()
                .Json(json => json
                    .WithDefaultJsonSerializerSettings());
        }

        [Fact]
        public void WithDefaultJsonSettingsShouldThrowExceptionWithBuildInSerializationSettings()
        {
            var jsonSettings = new JsonSerializerSettings();

            Test.AssertException<JsonResultAssertionException>(() =>
            {
                MyController<MvcController>
                    .Instance()
                    .Calling(c => c.JsonWithSpecificSettingsAction(jsonSettings))
                    .ShouldReturn()
                    .Json(json => json
                        .WithDefaultJsonSerializerSettings());
            },
            "When calling JsonWithSpecificSettingsAction action in MvcController expected JSON result serializer settings to have DefaultContractResolver, but in fact found null.");
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
        public void WithJsonSerializerSettingsShouldNotThrowExceptionWithValidSettings()
        {
            var jsonSettings = TestObjectFactory.GetJsonSerializerSettings();

            MyController<MvcController>
                .Instance()
                .Calling(c => c.JsonWithSpecificSettingsAction(jsonSettings))
                .ShouldReturn()
                .Json(json => json
                    .WithJsonSerializerSettings(jsonSettings));
        }

        [Fact]
        public void WithJsonSerializerSettingsShouldNotThrowExceptionWithBuiltInSettings()
        {
            var jsonSettings = new JsonSerializerSettings();

            MyController<MvcController>
                .Instance()
                .Calling(c => c.JsonWithSpecificSettingsAction(jsonSettings))
                .ShouldReturn()
                .Json(json => json
                    .WithJsonSerializerSettings(jsonSettings));
        }

        [Fact]
        public void WithJsonSerializerSettingsShouldNotThrowExceptionWithActionSettings()
        {
            var jsonSettings = new JsonSerializerSettings
            {
                CheckAdditionalContent = true,
                NullValueHandling = NullValueHandling.Ignore
            };

            MyController<MvcController>
                .Instance()
                .Calling(c => c.JsonWithSpecificSettingsAction(jsonSettings))
                .ShouldReturn()
                .Json(json => json
                    .WithJsonSerializerSettings(settings => 
                    {
                        settings.WithAdditionalContentChecking(true);
                        settings.WithNullValueHandling(NullValueHandling.Ignore);
                    }));
        }

        [Fact]
        public void WithJsonSerializerSettingsShouldThrowExceptionWithActionSettings()
        {
            var jsonSettings = new JsonSerializerSettings
            {
                CheckAdditionalContent = true,
                NullValueHandling = NullValueHandling.Ignore
            };

            Test.AssertException<JsonResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.JsonWithSpecificSettingsAction(jsonSettings))
                        .ShouldReturn()
                        .Json(json => json
                            .WithJsonSerializerSettings(settings =>
                            {
                                settings.WithAdditionalContentChecking(false);
                                settings.WithNullValueHandling(NullValueHandling.Ignore);
                            }));
                },
                "When calling JsonWithSpecificSettingsAction action in MvcController expected JSON result serializer settings to have disabled checking for additional content, but in fact it was enabled.");
        }

        [Fact]
        public void WithJsonSerializerSettingsShouldThrowWithNull()
        {
            var jsonSettings = TestObjectFactory.GetJsonSerializerSettings();

            Assert.Throws<JsonResultAssertionException>(() =>
            {
                MyController<MvcController>
                    .Instance()
                    .Calling(c => c.JsonWithSpecificSettingsAction(null))
                    .ShouldReturn()
                    .Json(json => json
                        .WithJsonSerializerSettings(jsonSettings));
            });
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
        public void AndAlsoShouldWorkCorrectly()
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
