namespace MyTested.Mvc.Tests.BuildersTests.ActionResultsTests.JsonTests
{
    using System.Collections.Generic;
    using Exceptions;
    using Newtonsoft.Json;
    using Setups;
    using Setups.Controllers;
    using Setups.Models;
    using Xunit;

    [Collection(MyMvcTests.ServiceBasedTests)]
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
            MyMvc.IsUsingDefaultServices();
            
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.JsonAction())
                .ShouldReturn()
                .Json()
                .WithDefaulJsonSerializerSettings();

            MyMvc.IsNotUsingServices();
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
        public void WithJsonSerializerSettingsShouldThrowExceptionWithDifferentJsonSettings()
        {
            var jsonSerializerSettings = TestObjectFactory.GetJsonSerializerSettings();
            jsonSerializerSettings.DateParseHandling = DateParseHandling.DateTime;

            Test.AssertException<JsonResultAssertionException>(() =>
            {
                MyMvc
                    .Controller<MvcController>()
                    .Calling(c => c.JsonWithSettingsAction())
                    .ShouldReturn()
                    .Json()
                    .WithJsonSerializerSettings(jsonSerializerSettings);
            }, "When calling JsonWithSettingsAction action in MvcController expected JSON result serializer settings to have DateTime date parse handling, but in fact found DateTimeOffset.");
        }
    }
}
