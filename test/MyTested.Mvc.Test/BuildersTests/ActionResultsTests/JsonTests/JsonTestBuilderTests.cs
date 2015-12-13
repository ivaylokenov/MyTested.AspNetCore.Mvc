namespace MyTested.Mvc.Tests.BuildersTests.ActionResultsTests.JsonTests
{
    using System.Collections.Generic;
    using System.Text;
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
        public void WithDefaultEncodingShouldNotThrowExceptionWhenUsingDefaultEncoding()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.JsonAction())
                .ShouldReturn()
                .Json()
                .WithDefaultEncoding();
        }

        // TODO: no encoding?
        //[Fact]
        //[ExpectedException(
        //    typeof(JsonResultAssertionException),
        //    ExpectedMessage = "When calling JsonWithEncodingAction action in MvcController expected JSON result encoding to be UTF8Encoding, but instead received ASCIIEncoding.")]
        //public void WithDefaultEncodingShouldThrowExceptionWhenNotUsingDefaultEncoding()
        //{
        //    MyMvc
        //        .Controller<MvcController>()
        //        .Calling(c => c.JsonWithEncodingAction())
        //        .ShouldReturn()
        //        .Json()
        //        .WithDefaultEncoding();
        //}

        //[Fact]
        //public void WithEncodingShouldNotThrowExceptionWhenUsingDefaultEncoding()
        //{
        //    MyMvc
        //        .Controller<MvcController>()
        //        .Calling(c => c.JsonWithEncodingAction())
        //        .ShouldReturn()
        //        .Json()
        //        .WithEncoding(Encoding.ASCII);
        //}

        //[Fact]
        //[ExpectedException(
        //    typeof(JsonResultAssertionException),
        //    ExpectedMessage = "When calling JsonWithEncodingAction action in MvcController expected JSON result encoding to be UTF8Encoding, but instead received ASCIIEncoding.")]
        //public void WithEncodingShouldThrowExceptionWhenNotUsingDefaultEncoding()
        //{
        //    MyMvc
        //        .Controller<MvcController>()
        //        .Calling(c => c.JsonWithEncodingAction())
        //        .ShouldReturn()
        //        .Json()
        //        .WithEncoding(Encoding.UTF8);
        //}

        [Fact]
        public void WithDefaultJsonSettingsShouldNotThrowExeptionWithDefaultJsonSettings()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.JsonAction())
                .ShouldReturn()
                .Json()
                .WithDefaultEncoding()
                .AndAlso()
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
