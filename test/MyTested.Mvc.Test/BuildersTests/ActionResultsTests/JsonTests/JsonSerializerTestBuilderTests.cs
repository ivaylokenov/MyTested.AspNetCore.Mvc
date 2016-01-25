namespace MyTested.Mvc.Tests.BuildersTests.ActionResultsTests.JsonTests
{
    using System.Globalization;
    using System.Runtime.Serialization.Formatters;
    using Exceptions;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;
    using Setups;
    using Setups.Controllers;
    using Xunit;
    
    public class JsonSerializerSettingsTestBuilderTests
    {
        [Fact]
        public void WithCultureShouldNotThrowExceptionWithCorrectValue()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.JsonWithSettingsAction())
                .ShouldReturn()
                .Json()
                .WithJsonSerializerSettings(s =>
                    s.WithCulture(CultureInfo.InvariantCulture));
        }

        [Fact]
        public void WithCultureShouldThrowExceptionWithIncorrectValue()
        {
            Test.AssertException<JsonResultAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.JsonWithSettingsAction())
                        .ShouldReturn()
                        .Json()
                        .WithJsonSerializerSettings(s =>
                            s.WithCulture(CultureInfo.CurrentCulture));
                }, 
                "When calling JsonWithSettingsAction action in MvcController expected JSON result serializer settings to have 'English (United States)' culture, but in fact found 'Invariant Language (Invariant Country)'.");
        }
        
        [Fact]
        public void WithCultureShouldValidateOnlyTheProperty()
        {
            var jsonSerializerSettings = TestObjectFactory.GetJsonSerializerSettings();
            jsonSerializerSettings.Culture = CultureInfo.InvariantCulture;
            jsonSerializerSettings.ConstructorHandling = ConstructorHandling.Default;

            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.JsonWithSpecificSettingsAction(jsonSerializerSettings))
                .ShouldReturn()
                .Json()
                .WithJsonSerializerSettings(s =>
                    s.WithCulture(CultureInfo.InvariantCulture));
        }

        [Fact]
        public void WithContractResolverOfTypeShouldNotThrowExceptionWithCorrectValue()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.JsonWithSettingsAction())
                .ShouldReturn()
                .Json()
                .WithJsonSerializerSettings(s =>
                    s.WithContractResolverOfType<CamelCasePropertyNamesContractResolver>());
        }

        [Fact]
        public void WithContractResolverOfTypeShouldThrowExceptionWithIncorrectValue()
        {
            Test.AssertException<JsonResultAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.JsonWithSettingsAction())
                        .ShouldReturn()
                        .Json()
                        .WithJsonSerializerSettings(s =>
                            s.WithContractResolverOfType<DefaultContractResolver>());
                }, 
                "When calling JsonWithSettingsAction action in MvcController expected JSON result serializer settings to have DefaultContractResolver, but in fact found CamelCasePropertyNamesContractResolver.");
        }

        [Fact]
        public void WithContractResolverOfTypeShouldValidateTheProperty()
        {
            var jsonSerializerSettings = TestObjectFactory.GetJsonSerializerSettings();
            jsonSerializerSettings.MaxDepth = int.MaxValue;
            jsonSerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.JsonWithSpecificSettingsAction(jsonSerializerSettings))
                .ShouldReturn()
                .Json()
                .WithJsonSerializerSettings(s =>
                    s.WithContractResolverOfType<CamelCasePropertyNamesContractResolver>());
        }

        [Fact]
        public void WithContractResolverShouldNotThrowExceptionWithCorrectValue()
        {
            var jsonSettings = TestObjectFactory.GetJsonSerializerSettings();

            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.JsonWithSpecificSettingsAction(jsonSettings))
                .ShouldReturn()
                .Json()
                .WithJsonSerializerSettings(s =>
                    s.WithContractResolver(jsonSettings.ContractResolver));
        }

        [Fact]
        public void WithContractResolverShouldThrowExceptionWithIncorrectValue()
        {
            Test.AssertException<JsonResultAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.JsonWithSettingsAction())
                        .ShouldReturn()
                        .Json()
                        .WithJsonSerializerSettings(s =>
                            s.WithContractResolver(new DefaultContractResolver()));
                }, 
                "When calling JsonWithSettingsAction action in MvcController expected JSON result serializer settings to have the same contract resolver as the provided one, but in fact it was different.");
        }

        [Fact]
        public void WithContractResolverShouldValidateOnlyTheProperty()
        {
            var jsonSerializerSettings = TestObjectFactory.GetJsonSerializerSettings();
            var contractResolver = new CamelCasePropertyNamesContractResolver();
            jsonSerializerSettings.MaxDepth = int.MaxValue;
            jsonSerializerSettings.ContractResolver = contractResolver;

            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.JsonWithSpecificSettingsAction(jsonSerializerSettings))
                .ShouldReturn()
                .Json()
                .WithJsonSerializerSettings(s =>
                    s.WithContractResolver(contractResolver));
        }

        [Fact]
        public void WithConstructorHandlingShouldNotThrowExceptionWithCorrectValue()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.JsonWithSettingsAction())
                .ShouldReturn()
                .Json()
                .WithJsonSerializerSettings(s =>
                    s.WithConstructorHandling(ConstructorHandling.AllowNonPublicDefaultConstructor));
        }

        [Fact]
        public void WithConstructorHandlingShouldThrowExceptionWithIncorrectValue()
        {
            Test.AssertException<JsonResultAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.JsonWithSettingsAction())
                        .ShouldReturn()
                        .Json()
                        .WithJsonSerializerSettings(s =>
                            s.WithConstructorHandling(ConstructorHandling.Default));
                }, 
                "When calling JsonWithSettingsAction action in MvcController expected JSON result serializer settings to have Default constructor handling, but in fact found AllowNonPublicDefaultConstructor.");
        }

        [Fact]
        public void WithConstructorHandlingShouldValidateOnlyTheProperty()
        {
            var jsonSerializerSettings = TestObjectFactory.GetJsonSerializerSettings();
            jsonSerializerSettings.MaxDepth = int.MaxValue;
            jsonSerializerSettings.ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor;

            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.JsonWithSpecificSettingsAction(jsonSerializerSettings))
                .ShouldReturn()
                .Json()
                .WithJsonSerializerSettings(s =>
                    s.WithConstructorHandling(ConstructorHandling.AllowNonPublicDefaultConstructor));
        }

        [Fact]
        public void WithDateFormatHandlingShouldNotThrowExceptionWithCorrectValue()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.JsonWithSettingsAction())
                .ShouldReturn()
                .Json()
                .WithJsonSerializerSettings(s =>
                    s.WithDateFormatHandling(DateFormatHandling.MicrosoftDateFormat));
        }

        [Fact]
        public void WithDateFormatHandlingShouldThrowExceptionWithIncorrectValue()
        {
            Test.AssertException<JsonResultAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.JsonWithSettingsAction())
                        .ShouldReturn()
                        .Json()
                        .WithJsonSerializerSettings(s =>
                            s.WithDateFormatHandling(DateFormatHandling.IsoDateFormat));
                }, 
                "When calling JsonWithSettingsAction action in MvcController expected JSON result serializer settings to have IsoDateFormat date format handling, but in fact found MicrosoftDateFormat.");
        }

        [Fact]
        public void WithDateFormatHandlingShouldValidateOnlyTheProperty()
        {
            var jsonSerializerSettings = TestObjectFactory.GetJsonSerializerSettings();
            jsonSerializerSettings.MaxDepth = int.MaxValue;
            jsonSerializerSettings.DateFormatHandling = DateFormatHandling.MicrosoftDateFormat;

            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.JsonWithSpecificSettingsAction(jsonSerializerSettings))
                .ShouldReturn()
                .Json()
                .WithJsonSerializerSettings(s =>
                    s.WithDateFormatHandling(DateFormatHandling.MicrosoftDateFormat));
        }

        [Fact]
        public void WithDateParseHandlingShouldNotThrowExceptionWithCorrectValue()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.JsonWithSettingsAction())
                .ShouldReturn()
                .Json()
                .WithJsonSerializerSettings(s =>
                    s.WithDateParseHandling(DateParseHandling.DateTimeOffset));
        }

        [Fact]
        public void WithDateParseHandlingShouldThrowExceptionWithIncorrectValue()
        {
            Test.AssertException<JsonResultAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.JsonWithSettingsAction())
                        .ShouldReturn()
                        .Json()
                        .WithJsonSerializerSettings(s =>
                            s.WithDateParseHandling(DateParseHandling.DateTime));
                }, 
                "When calling JsonWithSettingsAction action in MvcController expected JSON result serializer settings to have DateTime date parse handling, but in fact found DateTimeOffset.");
        }

        [Fact]
        public void WithDateParseHandlingShouldValidateOnlyTheProperty()
        {
            var jsonSerializerSettings = TestObjectFactory.GetJsonSerializerSettings();
            jsonSerializerSettings.MaxDepth = int.MaxValue;
            jsonSerializerSettings.DateParseHandling = DateParseHandling.DateTimeOffset;

            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.JsonWithSpecificSettingsAction(jsonSerializerSettings))
                .ShouldReturn()
                .Json()
                .WithJsonSerializerSettings(s =>
                    s.WithDateParseHandling(DateParseHandling.DateTimeOffset));
        }

        [Fact]
        public void WithDateTimeZoneHandlingShouldNotThrowExceptionWithCorrectValue()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.JsonWithSettingsAction())
                .ShouldReturn()
                .Json()
                .WithJsonSerializerSettings(s =>
                    s.WithDateTimeZoneHandling(DateTimeZoneHandling.Utc));
        }

        [Fact]
        public void WithDateTimeZoneHandlingShouldThrowExceptionWithIncorrectValue()
        {
            Test.AssertException<JsonResultAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.JsonWithSettingsAction())
                        .ShouldReturn()
                        .Json()
                        .WithJsonSerializerSettings(s =>
                            s.WithDateTimeZoneHandling(DateTimeZoneHandling.Local));
                }, 
                "When calling JsonWithSettingsAction action in MvcController expected JSON result serializer settings to have Local date time zone handling, but in fact found Utc.");
        }

        [Fact]
        public void WithDateTimeZoneHandlingShouldValidateOnlyTheProperty()
        {
            var jsonSerializerSettings = TestObjectFactory.GetJsonSerializerSettings();
            jsonSerializerSettings.MaxDepth = int.MaxValue;
            jsonSerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Local;

            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.JsonWithSpecificSettingsAction(jsonSerializerSettings))
                .ShouldReturn()
                .Json()
                .WithJsonSerializerSettings(s =>
                    s.WithDateTimeZoneHandling(DateTimeZoneHandling.Local));
        }

        [Fact]
        public void WithDefaultValueHandlingShouldNotThrowExceptionWithCorrectValue()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.JsonWithSettingsAction())
                .ShouldReturn()
                .Json()
                .WithJsonSerializerSettings(s =>
                    s.WithDefaultValueHandling(DefaultValueHandling.Ignore));
        }

        [Fact]
        public void WithDefaultValueHandlingShouldThrowExceptionWithIncorrectValue()
        {
            Test.AssertException<JsonResultAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.JsonWithSettingsAction())
                        .ShouldReturn()
                        .Json()
                        .WithJsonSerializerSettings(s =>
                            s.WithDefaultValueHandling(DefaultValueHandling.Include));
                }, 
                "When calling JsonWithSettingsAction action in MvcController expected JSON result serializer settings to have Include default value handling, but in fact found Ignore.");
        }

        [Fact]
        public void WithDefaultValueHandlingShouldValidateOnlyTheProperty()
        {
            var jsonSerializerSettings = TestObjectFactory.GetJsonSerializerSettings();
            jsonSerializerSettings.MaxDepth = int.MaxValue;
            jsonSerializerSettings.DefaultValueHandling = DefaultValueHandling.Ignore;

            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.JsonWithSpecificSettingsAction(jsonSerializerSettings))
                .ShouldReturn()
                .Json()
                .WithJsonSerializerSettings(s =>
                    s.WithDefaultValueHandling(DefaultValueHandling.Ignore));
        }

        [Fact]
        public void WithFormattingShouldNotThrowExceptionWithCorrectValue()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.JsonWithSettingsAction())
                .ShouldReturn()
                .Json()
                .WithJsonSerializerSettings(s =>
                    s.WithFormatting(Formatting.Indented));
        }

        [Fact]
        public void WithFormattingShouldThrowExceptionWithIncorrectValue()
        {
            Test.AssertException<JsonResultAssertionException>(
                () =>
                {
                    MyMvc
                       .Controller<MvcController>()
                       .Calling(c => c.JsonWithSettingsAction())
                       .ShouldReturn()
                       .Json()
                       .WithJsonSerializerSettings(s =>
                           s.WithFormatting(Formatting.None));
                }, 
                "When calling JsonWithSettingsAction action in MvcController expected JSON result serializer settings to have None formatting, but in fact found Indented.");
        }

        [Fact]
        public void WithFormattingShouldValidateOnlyTheProperty()
        {
            var jsonSerializerSettings = TestObjectFactory.GetJsonSerializerSettings();
            jsonSerializerSettings.MaxDepth = int.MaxValue;
            jsonSerializerSettings.Formatting = Formatting.Indented;

            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.JsonWithSpecificSettingsAction(jsonSerializerSettings))
                .ShouldReturn()
                .Json()
                .WithJsonSerializerSettings(s =>
                    s.WithFormatting(Formatting.Indented));
        }

        [Fact]
        public void WithMaxDepthShouldNotThrowExceptionWithCorrectValue()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.JsonWithSettingsAction())
                .ShouldReturn()
                .Json()
                .WithJsonSerializerSettings(s =>
                    s.WithMaxDepth(2));
        }

        [Fact]
        public void WithMaxDepthShouldThrowExceptionWithIncorrectValue()
        {
            Test.AssertException<JsonResultAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.JsonWithSettingsAction())
                        .ShouldReturn()
                        .Json()
                        .WithJsonSerializerSettings(s =>
                            s.WithMaxDepth(int.MaxValue));
                }, 
                "When calling JsonWithSettingsAction action in MvcController expected JSON result serializer settings to have 2147483647 max depth, but in fact found 2.");
        }

        [Fact]
        public void WithNullMaxDepthShouldThrowExceptionWithIncorrectValue()
        {
            Test.AssertException<JsonResultAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.JsonWithSettingsAction())
                        .ShouldReturn()
                        .Json()
                        .WithJsonSerializerSettings(s =>
                            s.WithMaxDepth(null));
                },
                "When calling JsonWithSettingsAction action in MvcController expected JSON result serializer settings to have no max depth, but in fact found 2.");
        }

        [Fact]
        public void WithMaxDepthShouldThrowExceptionWithNullValue()
        {
            Test.AssertException<JsonResultAssertionException>(
                () =>
                {
                    var jsonSerializerSettings = TestObjectFactory.GetJsonSerializerSettings();
                    jsonSerializerSettings.MaxDepth = null;

                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.JsonWithSpecificSettingsAction(jsonSerializerSettings))
                        .ShouldReturn()
                        .Json()
                        .WithJsonSerializerSettings(s =>
                            s.WithMaxDepth(int.MaxValue));
                },
                "When calling JsonWithSpecificSettingsAction action in MvcController expected JSON result serializer settings to have 2147483647 max depth, but in fact found none.");
        }

        [Fact]
        public void WithMaxDepthShouldValidateOnlyTheProperty()
        {
            var jsonSerializerSettings = TestObjectFactory.GetJsonSerializerSettings();
            jsonSerializerSettings.MaxDepth = int.MaxValue;
            jsonSerializerSettings.Formatting = Formatting.None;

            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.JsonWithSpecificSettingsAction(jsonSerializerSettings))
                .ShouldReturn()
                .Json()
                .WithJsonSerializerSettings(s =>
                    s.WithMaxDepth(int.MaxValue));
        }

        [Fact]
        public void WithMissingMemberHandlingShouldNotThrowExceptionWithCorrectValue()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.JsonWithSettingsAction())
                .ShouldReturn()
                .Json()
                .WithJsonSerializerSettings(s =>
                    s.WithMissingMemberHandling(MissingMemberHandling.Ignore));
        }

        [Fact]
        public void WithMissingMemberHandlingShouldThrowExceptionWithIncorrectValue()
        {
            Test.AssertException<JsonResultAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.JsonWithSettingsAction())
                        .ShouldReturn()
                        .Json()
                        .WithJsonSerializerSettings(s =>
                            s.WithMissingMemberHandling(MissingMemberHandling.Error));
                }, 
                "When calling JsonWithSettingsAction action in MvcController expected JSON result serializer settings to have Error missing member handling, but in fact found Ignore.");
        }

        [Fact]
        public void WithMissingMemberHandlingShouldValidateOnlyTheProperty()
        {
            var jsonSerializerSettings = TestObjectFactory.GetJsonSerializerSettings();
            jsonSerializerSettings.MaxDepth = int.MaxValue;
            jsonSerializerSettings.MissingMemberHandling = MissingMemberHandling.Error;

            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.JsonWithSpecificSettingsAction(jsonSerializerSettings))
                .ShouldReturn()
                .Json()
                .WithJsonSerializerSettings(s =>
                    s.WithMissingMemberHandling(MissingMemberHandling.Error));
        }

        [Fact]
        public void WithNullValueHandlingShouldNotThrowExceptionWithCorrectValue()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.JsonWithSettingsAction())
                .ShouldReturn()
                .Json()
                .WithJsonSerializerSettings(s =>
                    s.WithNullValueHandling(NullValueHandling.Ignore));
        }

        [Fact]
        public void WithNullValueHandlingShouldThrowExceptionWithIncorrectValue()
        {
            Test.AssertException<JsonResultAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.JsonWithSettingsAction())
                        .ShouldReturn()
                        .Json()
                        .WithJsonSerializerSettings(s =>
                            s.WithNullValueHandling(NullValueHandling.Include));
                }, 
                "When calling JsonWithSettingsAction action in MvcController expected JSON result serializer settings to have Include null value handling, but in fact found Ignore.");
        }

        [Fact]
        public void WithNullValueHandlingShouldValidateOnlyTheProperty()
        {
            var jsonSerializerSettings = TestObjectFactory.GetJsonSerializerSettings();
            jsonSerializerSettings.MaxDepth = int.MaxValue;
            jsonSerializerSettings.NullValueHandling = NullValueHandling.Include;

            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.JsonWithSpecificSettingsAction(jsonSerializerSettings))
                .ShouldReturn()
                .Json()
                .WithJsonSerializerSettings(s =>
                    s.WithNullValueHandling(NullValueHandling.Include));
        }

        [Fact]
        public void WithObjectCreationHandlingShouldNotThrowExceptionWithCorrectValue()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.JsonWithSettingsAction())
                .ShouldReturn()
                .Json()
                .WithJsonSerializerSettings(s =>
                    s.WithObjectCreationHandling(ObjectCreationHandling.Replace));
        }

        [Fact]
        public void WithObjectCreationHandlingShouldThrowExceptionWithIncorrectValue()
        {
            Test.AssertException<JsonResultAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.JsonWithSettingsAction())
                        .ShouldReturn()
                        .Json()
                        .WithJsonSerializerSettings(s =>
                            s.WithObjectCreationHandling(ObjectCreationHandling.Auto));
                }, 
                "When calling JsonWithSettingsAction action in MvcController expected JSON result serializer settings to have Auto object creation handling, but in fact found Replace.");
        }

        [Fact]
        public void WithObjectCreationHandlingShouldValidateOnlyTheProperty()
        {
            var jsonSerializerSettings = TestObjectFactory.GetJsonSerializerSettings();
            jsonSerializerSettings.MaxDepth = int.MaxValue;
            jsonSerializerSettings.ObjectCreationHandling = ObjectCreationHandling.Replace;

            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.JsonWithSpecificSettingsAction(jsonSerializerSettings))
                .ShouldReturn()
                .Json()
                .WithJsonSerializerSettings(s =>
                    s.WithObjectCreationHandling(ObjectCreationHandling.Replace));
        }

        [Fact]
        public void WithPreserveReferencesHandlingShouldNotThrowExceptionWithCorrectValue()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.JsonWithSettingsAction())
                .ShouldReturn()
                .Json()
                .WithJsonSerializerSettings(s =>
                    s.WithPreserveReferencesHandling(PreserveReferencesHandling.Arrays));
        }

        [Fact]
        public void WithPreserveReferencesHandlingShouldThrowExceptionWithIncorrectValue()
        {
            Test.AssertException<JsonResultAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.JsonWithSettingsAction())
                        .ShouldReturn()
                        .Json()
                        .WithJsonSerializerSettings(s =>
                            s.WithPreserveReferencesHandling(PreserveReferencesHandling.Objects));
                }, 
                "When calling JsonWithSettingsAction action in MvcController expected JSON result serializer settings to have Objects preserve references handling, but in fact found Arrays.");
        }

        [Fact]
        public void WithPreserveReferencesHandlingShouldValidateOnlyTheProperty()
        {
            var jsonSerializerSettings = TestObjectFactory.GetJsonSerializerSettings();
            jsonSerializerSettings.MaxDepth = int.MaxValue;
            jsonSerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.Arrays;

            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.JsonWithSpecificSettingsAction(jsonSerializerSettings))
                .ShouldReturn()
                .Json()
                .WithJsonSerializerSettings(s =>
                    s.WithPreserveReferencesHandling(PreserveReferencesHandling.Arrays));
        }

        [Fact]
        public void WithReferenceLoopHandlingShouldNotThrowExceptionWithCorrectValue()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.JsonWithSettingsAction())
                .ShouldReturn()
                .Json()
                .WithJsonSerializerSettings(s =>
                    s.WithReferenceLoopHandling(ReferenceLoopHandling.Serialize));
        }

        [Fact]
        public void WithReferenceLoopHandlingShouldThrowExceptionWithIncorrectValue()
        {
            Test.AssertException<JsonResultAssertionException>(
                () =>
                {
                    MyMvc
                       .Controller<MvcController>()
                       .Calling(c => c.JsonWithSettingsAction())
                       .ShouldReturn()
                       .Json()
                       .WithJsonSerializerSettings(s =>
                           s.WithReferenceLoopHandling(ReferenceLoopHandling.Ignore));
                }, 
                "When calling JsonWithSettingsAction action in MvcController expected JSON result serializer settings to have Ignore reference loop handling, but in fact found Serialize.");
        }

        [Fact]
        public void WithReferenceLoopHandlingShouldValidateOnlyTheProperty()
        {
            var jsonSerializerSettings = TestObjectFactory.GetJsonSerializerSettings();
            jsonSerializerSettings.MaxDepth = int.MaxValue;
            jsonSerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.JsonWithSpecificSettingsAction(jsonSerializerSettings))
                .ShouldReturn()
                .Json()
                .WithJsonSerializerSettings(s =>
                    s.WithReferenceLoopHandling(ReferenceLoopHandling.Ignore));
        }

        [Fact]
        public void WithTypeNameAssemblyFormatShouldNotThrowExceptionWithCorrectValue()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.JsonWithSettingsAction())
                .ShouldReturn()
                .Json()
                .WithJsonSerializerSettings(s =>
                    s.WithTypeNameAssemblyFormat(FormatterAssemblyStyle.Simple));
        }

        [Fact]
        public void WithTypeNameAssemblyFormatShouldThrowExceptionWithIncorrectValue()
        {
            Test.AssertException<JsonResultAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.JsonWithSettingsAction())
                        .ShouldReturn()
                        .Json()
                        .WithJsonSerializerSettings(s =>
                            s.WithTypeNameAssemblyFormat(FormatterAssemblyStyle.Full));
                }, 
                "When calling JsonWithSettingsAction action in MvcController expected JSON result serializer settings to have Full type name assembly format, but in fact found Simple.");
        }

        [Fact]
        public void WithTypeNameAssemblyFormatShouldValidateOnlyTheProperty()
        {
            var jsonSerializerSettings = TestObjectFactory.GetJsonSerializerSettings();
            jsonSerializerSettings.MaxDepth = int.MaxValue;
            jsonSerializerSettings.TypeNameAssemblyFormat = FormatterAssemblyStyle.Simple;

            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.JsonWithSpecificSettingsAction(jsonSerializerSettings))
                .ShouldReturn()
                .Json()
                .WithJsonSerializerSettings(s =>
                    s.WithTypeNameAssemblyFormat(FormatterAssemblyStyle.Simple));
        }

        [Fact]
        public void WithTypeNameHandlingShouldNotThrowExceptionWithCorrectValue()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.JsonWithSettingsAction())
                .ShouldReturn()
                .Json()
                .WithJsonSerializerSettings(s =>
                    s.WithTypeNameHandling(TypeNameHandling.None));
        }

        [Fact]
        public void WithTypeNameHandlingShouldThrowExceptionWithIncorrectValue()
        {
            Test.AssertException<JsonResultAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.JsonWithSettingsAction())
                        .ShouldReturn()
                        .Json()
                        .WithJsonSerializerSettings(s =>
                            s.WithTypeNameHandling(TypeNameHandling.Auto));
                }, 
                "When calling JsonWithSettingsAction action in MvcController expected JSON result serializer settings to have Auto type name handling, but in fact found None.");
        }

        [Fact]
        public void WithTypeNameHandlingShouldValidateOnlyTheProperty()
        {
            var jsonSerializerSettings = TestObjectFactory.GetJsonSerializerSettings();
            jsonSerializerSettings.MaxDepth = int.MaxValue;
            jsonSerializerSettings.TypeNameHandling = TypeNameHandling.Arrays;

            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.JsonWithSpecificSettingsAction(jsonSerializerSettings))
                .ShouldReturn()
                .Json()
                .WithJsonSerializerSettings(s =>
                    s.WithTypeNameHandling(TypeNameHandling.Arrays));
        }

        [Fact]
        public void AndAlsoShouldNotThrowExceptionWithCorrectValues()
        {
            MyMvc
                .Controller<MvcController>()
                .Calling(c => c.JsonWithSettingsAction())
                .ShouldReturn()
                .Json()
                .WithJsonSerializerSettings(s => s
                    .WithFormatting(Formatting.Indented)
                    .AndAlso()
                    .WithMaxDepth(2)
                    .AndAlso()
                    .WithConstructorHandling(ConstructorHandling.AllowNonPublicDefaultConstructor));
        }

        [Fact]
        public void AndAlsoShouldThrowExceptionWithIncorrectValue()
        {
            Test.AssertException<JsonResultAssertionException>(
                () =>
                {
                    MyMvc
                        .Controller<MvcController>()
                        .Calling(c => c.JsonWithSettingsAction())
                        .ShouldReturn()
                        .Json()
                        .WithJsonSerializerSettings(s => s
                            .WithFormatting(Formatting.Indented)
                            .AndAlso()
                            .WithMaxDepth(2)
                            .AndAlso()
                            .WithConstructorHandling(ConstructorHandling.Default));
                }, 
                "When calling JsonWithSettingsAction action in MvcController expected JSON result serializer settings to have Default constructor handling, but in fact found AllowNonPublicDefaultConstructor.");
        }
    }
}
