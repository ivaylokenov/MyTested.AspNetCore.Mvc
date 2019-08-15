namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.ActionResultsTests.JsonTests
{
    using Exceptions;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;
    using Setups;
    using Setups.Common;
    using Setups.Controllers;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Globalization;
    using Xunit;

    public class JsonSerializerSettingsTestBuilderTests
    {
        [Fact]
        public void WithCheckAdditionalContentShouldNotThrowExceptionWithCorrectValue()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.JsonWithSettingsAction())
                .ShouldReturn()
                .Json(json => json
                    .WithJsonSerializerSettings(s =>
                        s.WithAdditionalContentChecking(false)));
        }

        [Fact]
        public void WithCheckAdditionalContentShouldThrowExceptionWithIncorrectValue()
        {
            Test.AssertException<JsonResultAssertionException>(
                   () =>
                   {
                        MyController<MvcController>
                            .Instance()
                            .Calling(c => c.JsonWithSettingsAction())
                            .ShouldReturn()
                            .Json(json => json
                                .WithJsonSerializerSettings(s =>
                                    s.WithAdditionalContentChecking(true)));
                   },
                   "When calling JsonWithSettingsAction action in MvcController expected JSON result serializer settings to have enabled checking for additional content, but in fact it was disabled.");
        }

        [Fact]
        public void WithCheckAdditionalContentShouldThrowExceptionForDisabledAdditionalChecks()
        {
            var jsonSerializerSettings = TestObjectFactory.GetJsonSerializerSettings();
            jsonSerializerSettings.CheckAdditionalContent = true;

            Test.AssertException<JsonResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.JsonWithSpecificSettingsAction(jsonSerializerSettings))
                        .ShouldReturn()
                        .Json(json => json
                            .WithJsonSerializerSettings(s =>
                                s.WithAdditionalContentChecking(false)));
                },
                "When calling JsonWithSpecificSettingsAction action in MvcController expected JSON result serializer settings to have disabled checking for additional content, but in fact it was enabled.");
        }

        [Fact]
        public void WithCheckAdditionalContentShouldThrowExceptionForEnabledAdditionalChecks()
        {
            var jsonSerializerSettings = TestObjectFactory.GetJsonSerializerSettings();
            jsonSerializerSettings.CheckAdditionalContent = false;

            Test.AssertException<JsonResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.JsonWithSpecificSettingsAction(jsonSerializerSettings))
                        .ShouldReturn()
                        .Json(json => json
                            .WithJsonSerializerSettings(s =>
                                s.WithAdditionalContentChecking(true)));
                },
                "When calling JsonWithSpecificSettingsAction action in MvcController expected JSON result serializer settings to have enabled checking for additional content, but in fact it was disabled.");
        }

        [Fact]
        public void WithCultureShouldNotThrowExceptionWithCorrectValue()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.JsonWithSettingsAction())
                .ShouldReturn()
                .Json(json => json
                    .WithJsonSerializerSettings(s =>
                        s.WithCulture(CultureInfo.InvariantCulture)));
        }

        [Fact]
        public void WithCultureShouldThrowExceptionWithIncorrectValue()
        {
            Test.AssertException<JsonResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.JsonWithSettingsAction())
                        .ShouldReturn()
                        .Json(json => json
                            .WithJsonSerializerSettings(s =>
                                s.WithCulture(CultureInfo.CurrentCulture)));
                },
                "When calling JsonWithSettingsAction action in MvcController expected JSON result serializer settings to have 'English (United States)' culture, but in fact found 'Invariant Language (Invariant Country)'.");
        }

        [Fact]
        public void WithCultureShouldValidateOnlyTheProperty()
        {
            var jsonSerializerSettings = TestObjectFactory.GetJsonSerializerSettings();
            jsonSerializerSettings.Culture = CultureInfo.InvariantCulture;
            jsonSerializerSettings.ConstructorHandling = ConstructorHandling.Default;

            MyController<MvcController>
                .Instance()
                .WithoutValidation()
                .Calling(c => c.JsonWithSpecificSettingsAction(jsonSerializerSettings))
                .ShouldReturn()
                .Json(json => json
                    .WithJsonSerializerSettings(s =>
                        s.WithCulture(CultureInfo.InvariantCulture)));
        }

        [Fact]
        public void WithContractResolverOfTypeShouldNotThrowExceptionWithCorrectValue()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.JsonWithSettingsAction())
                .ShouldReturn()
                .Json(json => json
                    .WithJsonSerializerSettings(s =>
                        s.WithContractResolverOfType<CamelCasePropertyNamesContractResolver>()));
        }

        [Fact]
        public void WithContractResolverOfTypeShouldThrowExceptionWithIncorrectValue()
        {
            Test.AssertException<JsonResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.JsonWithSettingsAction())
                        .ShouldReturn()
                        .Json(json => json
                            .WithJsonSerializerSettings(s =>
                                s.WithContractResolverOfType<DefaultContractResolver>()));
                },
                "When calling JsonWithSettingsAction action in MvcController expected JSON result serializer settings to have DefaultContractResolver, but in fact found CamelCasePropertyNamesContractResolver.");
        }

        [Fact]
        public void WithContractResolverOfTypeShouldValidateTheProperty()
        {
            var jsonSerializerSettings = TestObjectFactory.GetJsonSerializerSettings();
            jsonSerializerSettings.MaxDepth = int.MaxValue;
            jsonSerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            MyController<MvcController>
                .Instance()
                .WithoutValidation()
                .Calling(c => c.JsonWithSpecificSettingsAction(jsonSerializerSettings))
                .ShouldReturn()
                .Json(json => json
                    .WithJsonSerializerSettings(s =>
                        s.WithContractResolverOfType<CamelCasePropertyNamesContractResolver>()));
        }

        [Fact]
        public void WithContractResolverShouldNotThrowExceptionWithCorrectValue()
        {
            var jsonSettings = TestObjectFactory.GetJsonSerializerSettings();

            MyController<MvcController>
                .Instance()
                .WithoutValidation()
                .Calling(c => c.JsonWithSpecificSettingsAction(jsonSettings))
                .ShouldReturn()
                .Json(json => json
                    .WithJsonSerializerSettings(s =>
                        s.WithContractResolver(jsonSettings.ContractResolver)));
        }

        [Fact]
        public void WithoutContractResolverShouldNotThrowException()
        {
            var jsonSerializerSettings = TestObjectFactory.GetJsonSerializerSettings();
            jsonSerializerSettings.ContractResolver = null;

            MyController<MvcController>
                .Instance()
                .Calling(c => c.JsonWithSpecificSettingsAction(jsonSerializerSettings))
                .ShouldReturn()
                .Json(json => json
                    .WithJsonSerializerSettings(s =>
                        s.WithContractResolver(null)));
        }

        [Fact]
        public void WithContractResolverShouldThrowExceptionWithIncorrectValue()
        {
            Test.AssertException<JsonResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.JsonWithSettingsAction())
                        .ShouldReturn()
                        .Json(json => json
                            .WithJsonSerializerSettings(s =>
                                s.WithContractResolver(new DefaultContractResolver())));
                },
                "When calling JsonWithSettingsAction action in MvcController expected JSON result serializer settings to have the same contract resolver as the provided one, but in fact it was different.");
        }

        [Fact]
        public void WithoutContractResolverOfTypeShouldThrowException()
        {
            var jsonSerializerSettings = TestObjectFactory.GetJsonSerializerSettings();
            jsonSerializerSettings.ContractResolver = null;

            Test.AssertException<JsonResultAssertionException>(() =>
            {
                MyController<MvcController>
                    .Instance()
                    .Calling(c => c.JsonWithSpecificSettingsAction(jsonSerializerSettings))
                    .ShouldReturn()
                    .Json(json => json
                        .WithJsonSerializerSettings(s =>
                            s.WithContractResolverOfType(typeof(DefaultContractResolver))));
            },
            "When calling JsonWithSpecificSettingsAction action in MvcController expected JSON result serializer settings to have DefaultContractResolver, but in fact found null.");
        }

        [Fact]
        public void WithDefaultContractResolverOfTypeShouldThrowExceptionWithIncorrectValue()
        {
            var jsonSerializerSettings = TestObjectFactory.GetJsonSerializerSettings();
            jsonSerializerSettings.ContractResolver = new DefaultContractResolver();

            Test.AssertException<JsonResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.JsonWithSpecificSettingsAction(jsonSerializerSettings))
                        .ShouldReturn()
                        .Json(json => json
                            .WithJsonSerializerSettings(s =>
                                s.WithContractResolverOfType(typeof(CamelCasePropertyNamesContractResolver))));
                },
                "When calling JsonWithSpecificSettingsAction action in MvcController expected JSON result serializer settings to have CamelCasePropertyNamesContractResolver, but in fact found DefaultContractResolver.");
        }

        [Fact]
        public void WithCamelCaseContractResolverShouldThrowExceptionWithIncorrectValue()
        {
            Test.AssertException<JsonResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.JsonAction())
                        .ShouldReturn()
                        .Json(json => json
                            .WithJsonSerializerSettings(s =>
                                s.WithContractResolverOfType(typeof(CamelCasePropertyNamesContractResolver))));
                },
                "When calling JsonAction action in MvcController expected JSON result serializer settings to have CamelCasePropertyNamesContractResolver, but in fact found DefaultContractResolver.");
        }

        [Fact]
        public void WithContractResolverShouldValidateOnlyTheProperty()
        {
            var jsonSerializerSettings = TestObjectFactory.GetJsonSerializerSettings();
            var contractResolver = new CamelCasePropertyNamesContractResolver();
            jsonSerializerSettings.MaxDepth = int.MaxValue;
            jsonSerializerSettings.ContractResolver = contractResolver;

            MyController<MvcController>
                .Instance()
                .WithoutValidation()
                .Calling(c => c.JsonWithSpecificSettingsAction(jsonSerializerSettings))
                .ShouldReturn()
                .Json(json => json
                    .WithJsonSerializerSettings(s =>
                        s.WithContractResolver(contractResolver)));
        }

        [Fact]
        public void ContainingConverterShouldNotThrowExceptionWithCorrectValue()
        {
            var jsonSerializerSettings = TestObjectFactory.GetJsonSerializerSettings();
            var jsonConverter = new CustomJsonConverter();
            jsonSerializerSettings.Converters.Add(jsonConverter);

            MyController<MvcController>
                .Instance()
                .WithoutValidation()
                .Calling(c => c.JsonWithSpecificSettingsAction(jsonSerializerSettings))
                .ShouldReturn()
                .Json(json => json
                    .WithJsonSerializerSettings(s =>
                        s.ContainingConverter(jsonConverter)));
        }

        [Fact]
        public void ContainingConverterShouldThrowExceptionWithIncorrectValue()
        {
            var jsonSerializerSettings = TestObjectFactory.GetJsonSerializerSettings();
            var jsonConverter = new CustomJsonConverter();

            Test.AssertException<JsonResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .WithoutValidation()        
                        .Calling(c => c.JsonWithSpecificSettingsAction(jsonSerializerSettings))
                        .ShouldReturn()
                        .Json(json => json
                            .WithJsonSerializerSettings(s =>
                                s.ContainingConverter(jsonConverter)));
                },
                "When calling JsonWithSpecificSettingsAction action in MvcController expected JSON result serializer settings to have the provided converter, but such was not found.");
        }

        [Fact]
        public void ContainingConverterOfTypeShouldNotThrowExceptionWithCorrectValue()
        {
            var jsonSerializerSettings = TestObjectFactory.GetJsonSerializerSettings();
            var jsonConverter = new CustomJsonConverter();
            jsonSerializerSettings.Converters.Add(jsonConverter);

            MyController<MvcController>
                .Instance()
                .WithoutValidation()
                .Calling(c => c.JsonWithSpecificSettingsAction(jsonSerializerSettings))
                .ShouldReturn()
                .Json(json => json
                    .WithJsonSerializerSettings(s =>
                        s.ContainingConverterOfType<CustomJsonConverter>()));
        }

        [Fact]
        public void ContainingConverterOfTypeShouldThrowExceptionWithIncorrectValue()
        {
            var jsonSerializerSettings = TestObjectFactory.GetJsonSerializerSettings();

            Test.AssertException<JsonResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .WithoutValidation()
                        .Calling(c => c.JsonWithSpecificSettingsAction(jsonSerializerSettings))
                        .ShouldReturn()
                        .Json(json => json
                            .WithJsonSerializerSettings(s =>
                                s.ContainingConverterOfType<CustomJsonConverter>()));
                },
                "When calling JsonWithSpecificSettingsAction action in MvcController expected JSON result serializer settings to have converter of CustomJsonConverter type, but such was not found.");
        }

        [Fact]
        public void ContainingConvertersShouldNotThrowExceptionWithCorrectValue()
        {
            var jsonSerializerSettings = TestObjectFactory.GetJsonSerializerSettings();
            var jsonConverter = new CustomJsonConverter();
            jsonSerializerSettings.Converters.Add(jsonConverter);

            MyController<MvcController>
                .Instance()
                .WithoutValidation()
                .Calling(c => c.JsonWithSpecificSettingsAction(jsonSerializerSettings))
                .ShouldReturn()
                .Json(json => json
                    .WithJsonSerializerSettings(s =>
                        s.ContainingConverters(jsonConverter)));
        }

        [Fact]
        public void ContainingConvertersShouldNotThrowExceptionWithCorrectValueAsEnumerable()
        {
            var jsonSerializerSettings = TestObjectFactory.GetJsonSerializerSettings();
            var jsonConverter = new CustomJsonConverter();
            jsonSerializerSettings.Converters.Add(jsonConverter);

            MyController<MvcController>
                .Instance()
                .WithoutValidation()
                .Calling(c => c.JsonWithSpecificSettingsAction(jsonSerializerSettings))
                .ShouldReturn()
                .Json(json => json
                    .WithJsonSerializerSettings(s =>
                        s.ContainingConverters(new List<JsonConverter> { jsonConverter })));
        }

        [Fact]
        public void ContainingConvertersShouldThrowExceptionWithIncorrectValueOfInvalidCount()
        {
            var jsonSerializerSettings = TestObjectFactory.GetJsonSerializerSettings();
            var jsonConverter = new CustomJsonConverter();
            jsonSerializerSettings.Converters.Add(jsonConverter);
            jsonSerializerSettings.Converters.Add(jsonConverter);

            Test.AssertException<JsonResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .WithoutValidation()
                        .Calling(c => c.JsonWithSpecificSettingsAction(jsonSerializerSettings))
                        .ShouldReturn()
                        .Json(json => json
                            .WithJsonSerializerSettings(s =>
                                s.ContainingConverters(jsonConverter)));
                },
                "When calling JsonWithSpecificSettingsAction action in MvcController expected JSON result serializer settings to have 1 converter, but instead found 2.");
        }

        [Fact]
        public void ContainingConvertersShouldThrowExceptionWithIncorrectValueOfInvalidManyCount()
        {
            var jsonSerializerSettings = TestObjectFactory.GetJsonSerializerSettings();
            var jsonConverter = new CustomJsonConverter();
            jsonSerializerSettings.Converters.Add(jsonConverter);
            jsonSerializerSettings.Converters.Add(jsonConverter);

            Test.AssertException<JsonResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .WithoutValidation()
                        .Calling(c => c.JsonWithSpecificSettingsAction(jsonSerializerSettings))
                        .ShouldReturn()
                        .Json(json => json
                            .WithJsonSerializerSettings(s =>
                                s.ContainingConverters(jsonConverter, jsonConverter, jsonConverter)));
                },
                "When calling JsonWithSpecificSettingsAction action in MvcController expected JSON result serializer settings to have 3 converters, but instead found 2.");
        }

        [Fact]
        public void ContainingConvertersShouldThrowExceptionWithIncorrectConverter()
        {
            var jsonSerializerSettings = TestObjectFactory.GetJsonSerializerSettings();
            var jsonConverter = new CustomJsonConverter();
            var otherJsonConverter = new CustomJsonConverter.OtherJsonConverter();
            jsonSerializerSettings.Converters.Add(otherJsonConverter);

            Test.AssertException<JsonResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .WithoutValidation()
                        .Calling(c => c.JsonWithSpecificSettingsAction(jsonSerializerSettings))
                        .ShouldReturn()
                        .Json(json => json
                            .WithJsonSerializerSettings(s =>
                                s.ContainingConverters(jsonConverter)));
                },
                "When calling JsonWithSpecificSettingsAction action in MvcController expected JSON result serializer settings to have converter of CustomJsonConverter type, but none was found.");
        }

        [Fact]
        public void WithConstructorHandlingShouldNotThrowExceptionWithCorrectValue()
        {
            MyController<MvcController>
                .Instance()
                .WithoutValidation()
                .Calling(c => c.JsonWithSettingsAction())
                .ShouldReturn()
                .Json(json => json
                    .WithJsonSerializerSettings(s =>
                        s.WithConstructorHandling(ConstructorHandling.AllowNonPublicDefaultConstructor)));
        }

        [Fact]
        public void WithConstructorHandlingShouldThrowExceptionWithIncorrectValue()
        {
            Test.AssertException<JsonResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .WithoutValidation()
                        .Calling(c => c.JsonWithSettingsAction())
                        .ShouldReturn()
                        .Json(json => json
                            .WithJsonSerializerSettings(s =>
                                s.WithConstructorHandling(ConstructorHandling.Default)));
                },
                "When calling JsonWithSettingsAction action in MvcController expected JSON result serializer settings to have Default constructor handling, but in fact found AllowNonPublicDefaultConstructor.");
        }

        [Fact]
        public void WithConstructorHandlingShouldValidateOnlyTheProperty()
        {
            var jsonSerializerSettings = TestObjectFactory.GetJsonSerializerSettings();
            jsonSerializerSettings.MaxDepth = int.MaxValue;
            jsonSerializerSettings.ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor;

            MyController<MvcController>
                .Instance()
                .WithoutValidation()
                .Calling(c => c.JsonWithSpecificSettingsAction(jsonSerializerSettings))
                .ShouldReturn()
                .Json(json => json
                    .WithJsonSerializerSettings(s =>
                        s.WithConstructorHandling(ConstructorHandling.AllowNonPublicDefaultConstructor)));
        }

        [Fact]
        public void WithDateFormatHandlingShouldNotThrowExceptionWithCorrectValue()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.JsonWithSettingsAction())
                .ShouldReturn()
                .Json(json => json
                    .WithJsonSerializerSettings(s =>
                        s.WithDateFormatHandling(DateFormatHandling.MicrosoftDateFormat)));
        }

        [Fact]
        public void WithDateFormatHandlingShouldThrowExceptionWithIncorrectValue()
        {
            Test.AssertException<JsonResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.JsonWithSettingsAction())
                        .ShouldReturn()
                        .Json(json => json
                            .WithJsonSerializerSettings(s =>
                                s.WithDateFormatHandling(DateFormatHandling.IsoDateFormat)));
                },
                "When calling JsonWithSettingsAction action in MvcController expected JSON result serializer settings to have IsoDateFormat date format handling, but in fact found MicrosoftDateFormat.");
        }

        [Fact]
        public void WithDateFormatStringShouldNotThrowExceptionWithCorrectValue()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.JsonWithSettingsAction())
                .ShouldReturn()
                .Json(json => json
                    .WithJsonSerializerSettings(s =>
                        s.WithDateFormatString("TEST")));
        }

        [Fact]
        public void WithDateFormatStringShouldThrowExceptionWithIncorrectValue()
        {
            Test.AssertException<JsonResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.JsonWithSettingsAction())
                        .ShouldReturn()
                        .Json(json => json
                            .WithJsonSerializerSettings(s =>
                                s.WithDateFormatString("Invalid")));
                },
                "When calling JsonWithSettingsAction action in MvcController expected JSON result serializer settings to have 'Invalid' date format string, but in fact found 'TEST'.");
        }

        [Fact]
        public void WithDateFormatHandlingShouldValidateOnlyTheProperty()
        {
            var jsonSerializerSettings = TestObjectFactory.GetJsonSerializerSettings();
            jsonSerializerSettings.MaxDepth = int.MaxValue;
            jsonSerializerSettings.DateFormatHandling = DateFormatHandling.MicrosoftDateFormat;

            MyController<MvcController>
                .Instance()
                .WithoutValidation()
                .Calling(c => c.JsonWithSpecificSettingsAction(jsonSerializerSettings))
                .ShouldReturn()
                .Json(json => json
                    .WithJsonSerializerSettings(s =>
                        s.WithDateFormatHandling(DateFormatHandling.MicrosoftDateFormat)));
        }

        [Fact]
        public void WithDateParseHandlingShouldNotThrowExceptionWithCorrectValue()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.JsonWithSettingsAction())
                .ShouldReturn()
                .Json(json => json
                    .WithJsonSerializerSettings(s =>
                        s.WithDateParseHandling(DateParseHandling.DateTimeOffset)));
        }

        [Fact]
        public void WithDateParseHandlingShouldThrowExceptionWithIncorrectValue()
        {
            Test.AssertException<JsonResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.JsonWithSettingsAction())
                        .ShouldReturn()
                        .Json(json => json
                            .WithJsonSerializerSettings(s =>
                                s.WithDateParseHandling(DateParseHandling.DateTime)));
                },
                "When calling JsonWithSettingsAction action in MvcController expected JSON result serializer settings to have DateTime date parse handling, but in fact found DateTimeOffset.");
        }

        [Fact]
        public void WithDateParseHandlingShouldValidateOnlyTheProperty()
        {
            var jsonSerializerSettings = TestObjectFactory.GetJsonSerializerSettings();
            jsonSerializerSettings.MaxDepth = int.MaxValue;
            jsonSerializerSettings.DateParseHandling = DateParseHandling.DateTimeOffset;

            MyController<MvcController>
                .Instance()
                .WithoutValidation()
                .Calling(c => c.JsonWithSpecificSettingsAction(jsonSerializerSettings))
                .ShouldReturn()
                .Json(json => json
                    .WithJsonSerializerSettings(s =>
                        s.WithDateParseHandling(DateParseHandling.DateTimeOffset)));
        }

        [Fact]
        public void WithDateTimeZoneHandlingShouldNotThrowExceptionWithCorrectValue()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.JsonWithSettingsAction())
                .ShouldReturn()
                .Json(json => json
                    .WithJsonSerializerSettings(s =>
                        s.WithDateTimeZoneHandling(DateTimeZoneHandling.Utc)));
        }

        [Fact]
        public void WithDateTimeZoneHandlingShouldThrowExceptionWithIncorrectValue()
        {
            Test.AssertException<JsonResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.JsonWithSettingsAction())
                        .ShouldReturn()
                        .Json(json => json
                            .WithJsonSerializerSettings(s =>
                                s.WithDateTimeZoneHandling(DateTimeZoneHandling.Local)));
                },
                "When calling JsonWithSettingsAction action in MvcController expected JSON result serializer settings to have Local date time zone handling, but in fact found Utc.");
        }

        [Fact]
        public void WithDateTimeZoneHandlingShouldValidateOnlyTheProperty()
        {
            var jsonSerializerSettings = TestObjectFactory.GetJsonSerializerSettings();
            jsonSerializerSettings.MaxDepth = int.MaxValue;
            jsonSerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Local;

            MyController<MvcController>
                .Instance()
                .WithoutValidation()
                .Calling(c => c.JsonWithSpecificSettingsAction(jsonSerializerSettings))
                .ShouldReturn()
                .Json(json => json
                    .WithJsonSerializerSettings(s =>
                        s.WithDateTimeZoneHandling(DateTimeZoneHandling.Local)));
        }

        [Fact]
        public void WithDefaultValueHandlingShouldNotThrowExceptionWithCorrectValue()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.JsonWithSettingsAction())
                .ShouldReturn()
                .Json(json => json
                    .WithJsonSerializerSettings(s =>
                        s.WithDefaultValueHandling(DefaultValueHandling.Ignore)));
        }

        [Fact]
        public void WithDefaultValueHandlingShouldThrowExceptionWithIncorrectValue()
        {
            Test.AssertException<JsonResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.JsonWithSettingsAction())
                        .ShouldReturn()
                        .Json(json => json
                            .WithJsonSerializerSettings(s =>
                                s.WithDefaultValueHandling(DefaultValueHandling.Include)));
                },
                "When calling JsonWithSettingsAction action in MvcController expected JSON result serializer settings to have Include default value handling, but in fact found Ignore.");
        }

        [Fact]
        public void WithDefaultValueHandlingShouldValidateOnlyTheProperty()
        {
            var jsonSerializerSettings = TestObjectFactory.GetJsonSerializerSettings();
            jsonSerializerSettings.MaxDepth = int.MaxValue;
            jsonSerializerSettings.DefaultValueHandling = DefaultValueHandling.Ignore;

            MyController<MvcController>
                .Instance()
                .WithoutValidation()
                .Calling(c => c.JsonWithSpecificSettingsAction(jsonSerializerSettings))
                .ShouldReturn()
                .Json(json => json
                    .WithJsonSerializerSettings(s =>
                        s.WithDefaultValueHandling(DefaultValueHandling.Ignore)));
        }

        [Fact]
        public void WithEqualityComparerShouldNotThrowExceptionWithCorrectValue()
        {
            var jsonSettings = TestObjectFactory.GetJsonSerializerSettings();
            var equalityComparer = new CustomEqualityComparer();
            jsonSettings.EqualityComparer = equalityComparer;

            MyController<MvcController>
                .Instance()
                .WithoutValidation()
                .Calling(c => c.JsonWithSpecificSettingsAction(jsonSettings))
                .ShouldReturn()
                .Json(json => json
                    .WithJsonSerializerSettings(s =>
                        s.WithEqualityComparer(equalityComparer)));
        }

        [Fact]
        public void WithEqualityComparerShouldThrowExceptionWithIncorrectValue()
        {
            Test.AssertException<JsonResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.JsonWithSettingsAction())
                        .ShouldReturn()
                        .Json(json => json
                            .WithJsonSerializerSettings(s =>
                                s.WithEqualityComparer(new CustomEqualityComparer())));
                },
                "When calling JsonWithSettingsAction action in MvcController expected JSON result serializer settings to have the same equality comparer as the provided one, but in fact it was different.");
        }

        [Fact]
        public void WithEqualityComparerOfTypeShouldNotThrowExceptionWithCorrectValue()
        {
            var jsonSettings = TestObjectFactory.GetJsonSerializerSettings();

            MyController<MvcController>
                .Instance()
                .WithoutValidation()
                .Calling(c => c.JsonWithSpecificSettingsAction(jsonSettings))
                .ShouldReturn()
                .Json(json => json
                    .WithJsonSerializerSettings(s =>
                        s.WithEqualityComparerOfType<CustomEqualityComparer>()));
        }

        [Fact]
        public void WithEqualityComparerOfTypeShouldThrowExceptionWithIncorrectValue()
        {
            Test.AssertException<JsonResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.JsonWithSettingsAction())
                        .ShouldReturn()
                        .Json(json => json
                            .WithJsonSerializerSettings(s =>
                                s.WithEqualityComparerOfType<IEqualityComparer>()));
                },
                "When calling JsonWithSettingsAction action in MvcController expected JSON result serializer settings to have equality comparer of IEqualityComparer type, but in fact found CustomEqualityComparer.");
        }

        [Fact]
        public void WithEqualityComparerOfTypeWithTypeShouldNotThrowExceptionWithCorrectValue()
        {
            var jsonSettings = TestObjectFactory.GetJsonSerializerSettings();

            MyController<MvcController>
                .Instance()
                .WithoutValidation()
                .Calling(c => c.JsonWithSpecificSettingsAction(jsonSettings))
                .ShouldReturn()
                .Json(json => json
                    .WithJsonSerializerSettings(s =>
                        s.WithEqualityComparerOfType(typeof(CustomEqualityComparer))));
        }

        [Fact]
        public void WithEqualityComparerOfTypeShouldThrowException()
        {
            Test.AssertException<JsonResultAssertionException>(() =>
            {
                MyController<MvcController>
                    .Instance()
                    .Calling(c => c.JsonWithSpecificSettingsAction(new JsonSerializerSettings()))
                    .ShouldReturn()
                    .Json(json => json
                        .WithJsonSerializerSettings(s =>
                            s.WithEqualityComparerOfType(typeof(IEqualityComparer))));
            },
            "When calling JsonWithSpecificSettingsAction action in MvcController expected JSON result serializer settings to have equality comparer of IEqualityComparer type, but in fact found null.");
        }

        [Fact]
        public void WithFloatFormatHandlingShouldNotThrowExceptionWithCorrectValue()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.JsonWithSettingsAction())
                .ShouldReturn()
                .Json(json => json
                    .WithJsonSerializerSettings(s =>
                        s.WithFloatFormatHandling(FloatFormatHandling.String)));
        }

        [Fact]
        public void WithFloatFormatHandlingShouldThrowExceptionWithIncorrectValue()
        {
            Test.AssertException<JsonResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                       .Instance()
                       .Calling(c => c.JsonWithSettingsAction())
                       .ShouldReturn()
                       .Json(json => json
                           .WithJsonSerializerSettings(s =>
                               s.WithFloatFormatHandling(FloatFormatHandling.Symbol)));
                },
                "When calling JsonWithSettingsAction action in MvcController expected JSON result serializer settings to have Symbol float format handling, but in fact found String.");
        }

        [Fact]
        public void WithFloatParseHandlingShouldNotThrowExceptionWithCorrectValue()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.JsonWithSettingsAction())
                .ShouldReturn()
                .Json(json => json
                    .WithJsonSerializerSettings(s =>
                        s.WithFloatParseHandling(FloatParseHandling.Decimal)));
        }

        [Fact]
        public void WithFloatParseHandlingShouldThrowExceptionWithIncorrectValue()
        {
            Test.AssertException<JsonResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                       .Instance()
                       .Calling(c => c.JsonWithSettingsAction())
                       .ShouldReturn()
                       .Json(json => json
                           .WithJsonSerializerSettings(s =>
                               s.WithFloatParseHandling(FloatParseHandling.Double)));
                },
                "When calling JsonWithSettingsAction action in MvcController expected JSON result serializer settings to have Double float parse handling, but in fact found Decimal.");
        }
        
        [Fact]
        public void WithMetadataPropertyHandlingShouldNotThrowExceptionWithCorrectValue()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.JsonWithSettingsAction())
                .ShouldReturn()
                .Json(json => json
                    .WithJsonSerializerSettings(s =>
                        s.WithMetadataPropertyHandling(MetadataPropertyHandling.ReadAhead)));
        }

        [Fact]
        public void WithMetadataPropertyHandlingShouldThrowExceptionWithIncorrectValue()
        {
            Test.AssertException<JsonResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                       .Instance()
                       .Calling(c => c.JsonWithSettingsAction())
                       .ShouldReturn()
                       .Json(json => json
                           .WithJsonSerializerSettings(s =>
                               s.WithMetadataPropertyHandling(MetadataPropertyHandling.Ignore)));
                },
                "When calling JsonWithSettingsAction action in MvcController expected JSON result serializer settings to have Ignore metadata property handling, but in fact found ReadAhead.");
        }
        
        [Fact]
        public void WithReferenceResolverShouldNotThrowExceptionWithCorrectValue()
        {
            var jsonSettings = TestObjectFactory.GetJsonSerializerSettings();
            var referenceResolver = new CustomJsonReferenceResolver();
            jsonSettings.ReferenceResolverProvider = () => referenceResolver;

            MyController<MvcController>
                .Instance()
                .WithoutValidation()
                .Calling(c => c.JsonWithSpecificSettingsAction(jsonSettings))
                .ShouldReturn()
                .Json(json => json
                    .WithJsonSerializerSettings(s =>
                        s.WithReferenceResolver(referenceResolver)));
        }

        [Fact]
        public void WithReferenceResolverShouldThrowExceptionWithIncorrectValue()
        {
            Test.AssertException<JsonResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.JsonWithSettingsAction())
                        .ShouldReturn()
                        .Json(json => json
                            .WithJsonSerializerSettings(s =>
                                s.WithReferenceResolver(new CustomJsonReferenceResolver())));
                },
                "When calling JsonWithSettingsAction action in MvcController expected JSON result serializer settings to have the same reference resolver as the provided one, but in fact it was different.");
        }

        [Fact]
        public void WithReferenceResolverOfTypeShouldNotThrowExceptionWithCorrectValue()
        {
            var jsonSettings = TestObjectFactory.GetJsonSerializerSettings();

            MyController<MvcController>
                .Instance()
                .WithoutValidation()
                .Calling(c => c.JsonWithSpecificSettingsAction(jsonSettings))
                .ShouldReturn()
                .Json(json => json
                    .WithJsonSerializerSettings(s =>
                        s.WithReferenceResolverOfType<CustomJsonReferenceResolver>()));
        }

        [Fact]
        public void WithReferenceResolverOfTypeShouldThrowExceptionWithIncorrectValue()
        {
            Test.AssertException<JsonResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.JsonWithSettingsAction())
                        .ShouldReturn()
                        .Json(json => json
                            .WithJsonSerializerSettings(s =>
                                s.WithReferenceResolverOfType<IReferenceResolver>()));
                },
                "When calling JsonWithSettingsAction action in MvcController expected JSON result serializer settings to have reference resolver of IReferenceResolver type, but in fact found CustomJsonReferenceResolver.");
        }

        [Fact]
        public void WithReferenceResolverOfTypeShouldNotThrowException()
        {
            MyController<MvcController>
                        .Instance()
                        .Calling(c => c.JsonWithSettingsAction())
                        .ShouldReturn()
                        .Json(json => json
                            .WithJsonSerializerSettings(s =>
                                s.WithReferenceResolverOfType(typeof(CustomJsonReferenceResolver))));
        }

        [Fact]
        public void WithReferenceResolverOfTypeWithTypeShouldNotThrowExceptionWithCorrectValue()
        {
            var jsonSettings = TestObjectFactory.GetJsonSerializerSettings();

            MyController<MvcController>
                .Instance()
                .WithoutValidation()
                .Calling(c => c.JsonWithSpecificSettingsAction(jsonSettings))
                .ShouldReturn()
                .Json(json => json
                    .WithJsonSerializerSettings(s =>
                        s.WithReferenceResolverOfType(typeof(CustomJsonReferenceResolver))));
        }

        [Fact]
        public void WithReferenceResolverOfTypeShouldNotThrowExceptionWithNull()
        {
            var jsonSerializerSettings = TestObjectFactory.GetJsonSerializerSettings();
            jsonSerializerSettings.ReferenceResolverProvider = () => null;

            MyController<MvcController>
                .Instance()
                .Calling(c => c.JsonWithSpecificSettingsAction(jsonSerializerSettings))
                .ShouldReturn()
                .Json(json => json
                    .WithJsonSerializerSettings(s =>
                        s.WithReferenceResolverOfType(null)));
        }

        [Fact]
        public void WithStringEscapeHandlingShouldNotThrowExceptionWithCorrectValue()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.JsonWithSettingsAction())
                .ShouldReturn()
                .Json(json => json
                    .WithJsonSerializerSettings(s =>
                        s.WithStringEscapeHandling(StringEscapeHandling.EscapeHtml)));
        }

        [Fact]
        public void WithStringEscapeHandlingShouldThrowExceptionWithIncorrectValue()
        {
            Test.AssertException<JsonResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.JsonWithSettingsAction())
                        .ShouldReturn()
                        .Json(json => json
                            .WithJsonSerializerSettings(s =>
                                s.WithStringEscapeHandling(StringEscapeHandling.EscapeNonAscii)));
                },
                "When calling JsonWithSettingsAction action in MvcController expected JSON result serializer settings to have EscapeNonAscii string escape handling, but in fact found EscapeHtml.");
        }
        
        [Fact]
        public void WithFormattingShouldNotThrowExceptionWithCorrectValue()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.JsonWithSettingsAction())
                .ShouldReturn()
                .Json(json => json
                    .WithJsonSerializerSettings(s =>
                        s.WithFormatting(Formatting.Indented)));
        }

        [Fact]
        public void WithFormattingShouldThrowExceptionWithIncorrectValue()
        {
            Test.AssertException<JsonResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                       .Instance()
                       .Calling(c => c.JsonWithSettingsAction())
                       .ShouldReturn()
                       .Json(json => json
                           .WithJsonSerializerSettings(s =>
                               s.WithFormatting(Formatting.None)));
                },
                "When calling JsonWithSettingsAction action in MvcController expected JSON result serializer settings to have None formatting, but in fact found Indented.");
        }

        [Fact]
        public void WithFormattingShouldValidateOnlyTheProperty()
        {
            var jsonSerializerSettings = TestObjectFactory.GetJsonSerializerSettings();
            jsonSerializerSettings.MaxDepth = int.MaxValue;
            jsonSerializerSettings.Formatting = Formatting.Indented;

            MyController<MvcController>
                .Instance()
                .WithoutValidation()
                .Calling(c => c.JsonWithSpecificSettingsAction(jsonSerializerSettings))
                .ShouldReturn()
                .Json(json => json
                    .WithJsonSerializerSettings(s =>
                        s.WithFormatting(Formatting.Indented)));
        }

        [Fact]
        public void WithMaxDepthShouldNotThrowExceptionWithCorrectValue()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.JsonWithSettingsAction())
                .ShouldReturn()
                .Json(json => json
                    .WithJsonSerializerSettings(s =>
                        s.WithMaxDepth(2)));
        }

        [Fact]
        public void WithMaxDepthShouldThrowExceptionWithIncorrectValue()
        {
            Test.AssertException<JsonResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.JsonWithSettingsAction())
                        .ShouldReturn()
                        .Json(json => json
                            .WithJsonSerializerSettings(s =>
                                s.WithMaxDepth(int.MaxValue)));
                },
                "When calling JsonWithSettingsAction action in MvcController expected JSON result serializer settings to have 2147483647 max depth, but in fact found 2.");
        }

        [Fact]
        public void WithNullMaxDepthShouldThrowExceptionWithIncorrectValue()
        {
            Test.AssertException<JsonResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.JsonWithSettingsAction())
                        .ShouldReturn()
                        .Json(json => json
                            .WithJsonSerializerSettings(s =>
                                s.WithMaxDepth(null)));
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

                    MyController<MvcController>
                        .Instance()
                        .WithoutValidation()
                        .Calling(c => c.JsonWithSpecificSettingsAction(jsonSerializerSettings))
                        .ShouldReturn()
                        .Json(json => json
                            .WithJsonSerializerSettings(s =>
                                s.WithMaxDepth(int.MaxValue)));
                },
                "When calling JsonWithSpecificSettingsAction action in MvcController expected JSON result serializer settings to have 2147483647 max depth, but in fact found none.");
        }

        [Fact]
        public void WithMaxDepthShouldValidateOnlyTheProperty()
        {
            var jsonSerializerSettings = TestObjectFactory.GetJsonSerializerSettings();
            jsonSerializerSettings.MaxDepth = int.MaxValue;
            jsonSerializerSettings.Formatting = Formatting.None;

            MyController<MvcController>
                .Instance()
                .WithoutValidation()
                .Calling(c => c.JsonWithSpecificSettingsAction(jsonSerializerSettings))
                .ShouldReturn()
                .Json(json => json
                    .WithJsonSerializerSettings(s =>
                        s.WithMaxDepth(int.MaxValue)));
        }

        [Fact]
        public void WithMissingMemberHandlingShouldNotThrowExceptionWithCorrectValue()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.JsonWithSettingsAction())
                .ShouldReturn()
                .Json(json => json
                    .WithJsonSerializerSettings(s =>
                        s.WithMissingMemberHandling(MissingMemberHandling.Ignore)));
        }

        [Fact]
        public void WithMissingMemberHandlingShouldThrowExceptionWithIncorrectValue()
        {
            Test.AssertException<JsonResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.JsonWithSettingsAction())
                        .ShouldReturn()
                        .Json(json => json
                            .WithJsonSerializerSettings(s =>
                                s.WithMissingMemberHandling(MissingMemberHandling.Error)));
                },
                "When calling JsonWithSettingsAction action in MvcController expected JSON result serializer settings to have Error missing member handling, but in fact found Ignore.");
        }

        [Fact]
        public void WithMissingMemberHandlingShouldValidateOnlyTheProperty()
        {
            var jsonSerializerSettings = TestObjectFactory.GetJsonSerializerSettings();
            jsonSerializerSettings.MaxDepth = int.MaxValue;
            jsonSerializerSettings.MissingMemberHandling = MissingMemberHandling.Error;

            MyController<MvcController>
                .Instance()
                .WithoutValidation()
                .Calling(c => c.JsonWithSpecificSettingsAction(jsonSerializerSettings))
                .ShouldReturn()
                .Json(json => json
                    .WithJsonSerializerSettings(s =>
                        s.WithMissingMemberHandling(MissingMemberHandling.Error)));
        }

        [Fact]
        public void WithNullValueHandlingShouldNotThrowExceptionWithCorrectValue()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.JsonWithSettingsAction())
                .ShouldReturn()
                .Json(json => json
                    .WithJsonSerializerSettings(s =>
                        s.WithNullValueHandling(NullValueHandling.Ignore)));
        }

        [Fact]
        public void WithNullValueHandlingShouldThrowExceptionWithIncorrectValue()
        {
            Test.AssertException<JsonResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.JsonWithSettingsAction())
                        .ShouldReturn()
                        .Json(json => json
                            .WithJsonSerializerSettings(s =>
                                s.WithNullValueHandling(NullValueHandling.Include)));
                },
                "When calling JsonWithSettingsAction action in MvcController expected JSON result serializer settings to have Include null value handling, but in fact found Ignore.");
        }

        [Fact]
        public void WithNullValueHandlingShouldValidateOnlyTheProperty()
        {
            var jsonSerializerSettings = TestObjectFactory.GetJsonSerializerSettings();
            jsonSerializerSettings.MaxDepth = int.MaxValue;
            jsonSerializerSettings.NullValueHandling = NullValueHandling.Include;

            MyController<MvcController>
                .Instance()
                .WithoutValidation()
                .Calling(c => c.JsonWithSpecificSettingsAction(jsonSerializerSettings))
                .ShouldReturn()
                .Json(json => json
                    .WithJsonSerializerSettings(s =>
                        s.WithNullValueHandling(NullValueHandling.Include)));
        }

        [Fact]
        public void WithObjectCreationHandlingShouldNotThrowExceptionWithCorrectValue()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.JsonWithSettingsAction())
                .ShouldReturn()
                .Json(json => json
                    .WithJsonSerializerSettings(s =>
                        s.WithObjectCreationHandling(ObjectCreationHandling.Replace)));
        }

        [Fact]
        public void WithObjectCreationHandlingShouldThrowExceptionWithIncorrectValue()
        {
            Test.AssertException<JsonResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.JsonWithSettingsAction())
                        .ShouldReturn()
                        .Json(json => json
                            .WithJsonSerializerSettings(s =>
                                s.WithObjectCreationHandling(ObjectCreationHandling.Auto)));
                },
                "When calling JsonWithSettingsAction action in MvcController expected JSON result serializer settings to have Auto object creation handling, but in fact found Replace.");
        }

        [Fact]
        public void WithObjectCreationHandlingShouldValidateOnlyTheProperty()
        {
            var jsonSerializerSettings = TestObjectFactory.GetJsonSerializerSettings();
            jsonSerializerSettings.MaxDepth = int.MaxValue;
            jsonSerializerSettings.ObjectCreationHandling = ObjectCreationHandling.Replace;

            MyController<MvcController>
                .Instance()
                .WithoutValidation()
                .Calling(c => c.JsonWithSpecificSettingsAction(jsonSerializerSettings))
                .ShouldReturn()
                .Json(json => json
                    .WithJsonSerializerSettings(s =>
                        s.WithObjectCreationHandling(ObjectCreationHandling.Replace)));
        }

        [Fact]
        public void WithPreserveReferencesHandlingShouldNotThrowExceptionWithCorrectValue()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.JsonWithSettingsAction())
                .ShouldReturn()
                .Json(json => json
                    .WithJsonSerializerSettings(s =>
                        s.WithPreserveReferencesHandling(PreserveReferencesHandling.Arrays)));
        }

        [Fact]
        public void WithPreserveReferencesHandlingShouldThrowExceptionWithIncorrectValue()
        {
            Test.AssertException<JsonResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.JsonWithSettingsAction())
                        .ShouldReturn()
                        .Json(json => json
                            .WithJsonSerializerSettings(s =>
                                s.WithPreserveReferencesHandling(PreserveReferencesHandling.Objects)));
                },
                "When calling JsonWithSettingsAction action in MvcController expected JSON result serializer settings to have Objects preserve references handling, but in fact found Arrays.");
        }

        [Fact]
        public void WithPreserveReferencesHandlingShouldValidateOnlyTheProperty()
        {
            var jsonSerializerSettings = TestObjectFactory.GetJsonSerializerSettings();
            jsonSerializerSettings.MaxDepth = int.MaxValue;
            jsonSerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.Arrays;

            MyController<MvcController>
                .Instance()
                .WithoutValidation()
                .Calling(c => c.JsonWithSpecificSettingsAction(jsonSerializerSettings))
                .ShouldReturn()
                .Json(json => json
                    .WithJsonSerializerSettings(s =>
                        s.WithPreserveReferencesHandling(PreserveReferencesHandling.Arrays)));
        }

        [Fact]
        public void WithReferenceLoopHandlingShouldNotThrowExceptionWithCorrectValue()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.JsonWithSettingsAction())
                .ShouldReturn()
                .Json(json => json
                    .WithJsonSerializerSettings(s =>
                        s.WithReferenceLoopHandling(ReferenceLoopHandling.Serialize)));
        }

        [Fact]
        public void WithReferenceLoopHandlingShouldThrowExceptionWithIncorrectValue()
        {
            Test.AssertException<JsonResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                       .Instance()
                       .Calling(c => c.JsonWithSettingsAction())
                       .ShouldReturn()
                       .Json(json => json
                           .WithJsonSerializerSettings(s =>
                               s.WithReferenceLoopHandling(ReferenceLoopHandling.Ignore)));
                },
                "When calling JsonWithSettingsAction action in MvcController expected JSON result serializer settings to have Ignore reference loop handling, but in fact found Serialize.");
        }

        [Fact]
        public void WithReferenceLoopHandlingShouldValidateOnlyTheProperty()
        {
            var jsonSerializerSettings = TestObjectFactory.GetJsonSerializerSettings();
            jsonSerializerSettings.MaxDepth = int.MaxValue;
            jsonSerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

            MyController<MvcController>
                .Instance()
                .WithoutValidation()
                .Calling(c => c.JsonWithSpecificSettingsAction(jsonSerializerSettings))
                .ShouldReturn()
                .Json(json => json
                    .WithJsonSerializerSettings(s =>
                        s.WithReferenceLoopHandling(ReferenceLoopHandling.Ignore)));
        }

        [Fact]
        public void WithTypeNameAssemblyFormatShouldNotThrowExceptionWithCorrectValue()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.JsonWithSettingsAction())
                .ShouldReturn()
                .Json(json => json
                    .WithJsonSerializerSettings(s =>
                        s.WithTypeNameAssemblyFormatHandling(TypeNameAssemblyFormatHandling.Simple)));
        }

        [Fact]
        public void WithTypeNameAssemblyFormatShouldThrowExceptionWithIncorrectValue()
        {
            Test.AssertException<JsonResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.JsonWithSettingsAction())
                        .ShouldReturn()
                        .Json(json => json
                            .WithJsonSerializerSettings(s =>
                                s.WithTypeNameAssemblyFormatHandling(TypeNameAssemblyFormatHandling.Full)));
                },
                "When calling JsonWithSettingsAction action in MvcController expected JSON result serializer settings to have Full type name assembly format handling, but in fact found Simple.");
        }

        [Fact]
        public void WithTypeNameAssemblyFormatShouldValidateOnlyTheProperty()
        {
            var jsonSerializerSettings = TestObjectFactory.GetJsonSerializerSettings();
            jsonSerializerSettings.MaxDepth = int.MaxValue;
            jsonSerializerSettings.TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Simple;

            MyController<MvcController>
                .Instance()
                .WithoutValidation()
                .Calling(c => c.JsonWithSpecificSettingsAction(jsonSerializerSettings))
                .ShouldReturn()
                .Json(json => json
                    .WithJsonSerializerSettings(s =>
                        s.WithTypeNameAssemblyFormatHandling(TypeNameAssemblyFormatHandling.Simple)));
        }

        [Fact]
        public void WithTypeNameHandlingShouldNotThrowExceptionWithCorrectValue()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.JsonWithSettingsAction())
                .ShouldReturn()
                .Json(json => json
                    .WithJsonSerializerSettings(s =>
                        s.WithTypeNameHandling(TypeNameHandling.None)));
        }

        [Fact]
        public void WithTypeNameHandlingShouldThrowExceptionWithIncorrectValue()
        {
            Test.AssertException<JsonResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.JsonWithSettingsAction())
                        .ShouldReturn()
                        .Json(json => json
                            .WithJsonSerializerSettings(s =>
                                s.WithTypeNameHandling(TypeNameHandling.Auto)));
                },
                "When calling JsonWithSettingsAction action in MvcController expected JSON result serializer settings to have Auto type name handling, but in fact found None.");
        }

        [Fact]
        public void WithTypeNameHandlingShouldValidateOnlyTheProperty()
        {
            var jsonSerializerSettings = TestObjectFactory.GetJsonSerializerSettings();
            jsonSerializerSettings.MaxDepth = int.MaxValue;
            jsonSerializerSettings.TypeNameHandling = TypeNameHandling.Arrays;

            MyController<MvcController>
                .Instance()
                .WithoutValidation()
                .Calling(c => c.JsonWithSpecificSettingsAction(jsonSerializerSettings))
                .ShouldReturn()
                .Json(json => json
                    .WithJsonSerializerSettings(s =>
                        s.WithTypeNameHandling(TypeNameHandling.Arrays)));
        }

        [Fact]
        public void WithTraceWriterShouldNotThrowExceptionWithCorrectValue()
        {
            var jsonSettings = TestObjectFactory.GetJsonSerializerSettings();
            var traceWriter = new CustomJsonTraceWriter();
            jsonSettings.TraceWriter = traceWriter;

            MyController<MvcController>
                .Instance()
                .WithoutValidation()
                .Calling(c => c.JsonWithSpecificSettingsAction(jsonSettings))
                .ShouldReturn()
                .Json(json => json
                    .WithJsonSerializerSettings(s =>
                        s.WithTraceWriter(traceWriter)));
        }

        [Fact]
        public void WithTraceWriterShouldThrowExceptionWithIncorrectValue()
        {
            Test.AssertException<JsonResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.JsonWithSettingsAction())
                        .ShouldReturn()
                        .Json(json => json
                            .WithJsonSerializerSettings(s =>
                                s.WithTraceWriter(new CustomJsonTraceWriter())));
                },
                "When calling JsonWithSettingsAction action in MvcController expected JSON result serializer settings to have the same trace writer as the provided one, but in fact it was different.");
        }

        [Fact]
        public void WithTraceWriterOfTypeShouldNotThrowException()
        {
            MyController<MvcController>
                        .Instance()
                        .Calling(c => c.JsonWithSettingsAction())
                        .ShouldReturn()
                        .Json(json => json
                            .WithJsonSerializerSettings(s =>
                                s.WithTraceWriterOfType(typeof(CustomJsonTraceWriter))));
        }

        [Fact]
        public void WithTraceWriterOfTypeShouldNotThrowExceptionWithCorrectValue()
        {
            var jsonSettings = TestObjectFactory.GetJsonSerializerSettings();

            MyController<MvcController>
                .Instance()
                .WithoutValidation()
                .Calling(c => c.JsonWithSpecificSettingsAction(jsonSettings))
                .ShouldReturn()
                .Json(json => json
                    .WithJsonSerializerSettings(s =>
                        s.WithTraceWriterOfType<CustomJsonTraceWriter>()));
        }

        [Fact]
        public void WithTraceWriterOfTypeShouldNotThrowExceptionWithDefaultWriter()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.JsonWithSpecificSettingsAction(new JsonSerializerSettings()))
                .ShouldReturn()
                .Json(json => json
                    .WithJsonSerializerSettings(s =>
                        s.WithTraceWriterOfType(null)));
        }

        [Fact]
        public void WithTraceWriterOfTypeShouldThrowExceptionWithDifferentTypes()
        {
            var jsonSerializerSettings = TestObjectFactory.GetJsonSerializerSettings();
            jsonSerializerSettings.TraceWriter = new DiagnosticsTraceWriter();

            Test.AssertException<JsonResultAssertionException>(() =>
            {
                MyController<MvcController>
                    .Instance()
                    .Calling(c => c.JsonWithSpecificSettingsAction(jsonSerializerSettings))
                    .ShouldReturn()
                    .Json(json => json
                        .WithJsonSerializerSettings(s =>
                            s.WithTraceWriterOfType(typeof(CustomJsonTraceWriter))));
            },
            "When calling JsonWithSpecificSettingsAction action in MvcController expected JSON result serializer settings to have trace writer of CustomJsonTraceWriter type, but in fact found DiagnosticsTraceWriter.");
        }

        [Fact]
        public void WithTraceWriterOfTypeShouldThrowExceptionWithIncorrectValue()
        {
            Test.AssertException<JsonResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.JsonWithSettingsAction())
                        .ShouldReturn()
                        .Json(json => json
                            .WithJsonSerializerSettings(s =>
                                s.WithTraceWriterOfType<ITraceWriter>()));
                },
                "When calling JsonWithSettingsAction action in MvcController expected JSON result serializer settings to have trace writer of ITraceWriter type, but in fact found CustomJsonTraceWriter.");
        }

        [Fact]
        public void WithTraceWriterOfTypeWithTypeShouldNotThrowExceptionWithCorrectValue()
        {
            var jsonSettings = TestObjectFactory.GetJsonSerializerSettings();

            MyController<MvcController>
                .Instance()
                .WithoutValidation()
                .Calling(c => c.JsonWithSpecificSettingsAction(jsonSettings))
                .ShouldReturn()
                .Json(json => json
                    .WithJsonSerializerSettings(s =>
                        s.WithTraceWriterOfType(typeof(CustomJsonTraceWriter))));
        }

        [Fact]
        public void WithSerializationBinderShouldNotThrowExceptionWithCorrectValue()
        {
            var jsonSerializerSettings = TestObjectFactory.GetJsonSerializerSettings();
            var defaultSerializationBinder = new DefaultSerializationBinder();
            jsonSerializerSettings.SerializationBinder = defaultSerializationBinder;

            MyController<MvcController>
                .Instance()
                .WithoutValidation()
                .Calling(c => c.JsonWithSpecificSettingsAction(jsonSerializerSettings))
                .ShouldReturn()
                .Json(json => json
                    .WithJsonSerializerSettings(s =>
                        s.WithSerializationBinder(defaultSerializationBinder)));
        }

        [Fact]
        public void WithoutSerializationBinderShouldNotThrowExceptionWithCorrectValue()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.JsonAction())
                .ShouldReturn()
                .Json(json => json
                    .WithJsonSerializerSettings(s =>
                        s.WithSerializationBinder(null)));
        }

        [Fact]
        public void WithSerializationBinderShouldThrowExceptionWithIncorrectValue()
        {
            var defaultSerializationBinder = new DefaultSerializationBinder();

            Test.AssertException<JsonResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.JsonWithSettingsAction())
                        .ShouldReturn()
                        .Json(json => json
                            .WithJsonSerializerSettings(s =>
                                s.WithSerializationBinder(defaultSerializationBinder)));
                },
                "When calling JsonWithSettingsAction action in MvcController expected JSON result serializer settings to have the same serialization binder as the provided one, but in fact it was different.");
        }

        [Fact]
        public void WithoutSerializationBinderOfTypeShouldNotThrowExceptionWithCorrectValue()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.JsonAction())
                .ShouldReturn()
                .Json(json => json
                    .WithJsonSerializerSettings(s =>
                        s.WithSerializationBinderOfType(null)));
        }

        [Fact]
        public void WithSerializationBinderOfTypeShouldNotThrowExceptionWithCorrectValue()
        {
            var jsonSerializerSettings = TestObjectFactory.GetJsonSerializerSettings();
            var defaultSerializationBinder = new DefaultSerializationBinder();
            jsonSerializerSettings.SerializationBinder = defaultSerializationBinder;

            MyController<MvcController>
                .Instance()
                .WithoutValidation()
                .Calling(c => c.JsonWithSpecificSettingsAction(jsonSerializerSettings))
                .ShouldReturn()
                .Json(json => json
                    .WithJsonSerializerSettings(s =>
                        s.WithSerializationBinderOfType(typeof(DefaultSerializationBinder))));
        }

        [Fact]
        public void WithPreviouslySetSerializationBinderShouldThrowException()
        {
            var jsonSerializerSettings = TestObjectFactory.GetJsonSerializerSettings();
            var defaultSerializationBinder = new DefaultSerializationBinder();
            jsonSerializerSettings.SerializationBinder = defaultSerializationBinder;

            Test.AssertException<InvalidOperationException>(() =>
            {
                MyController<MvcController>
                    .Instance()
                    .Calling(c => c.JsonWithSpecificSettingsAction(jsonSerializerSettings))
                    .ShouldReturn()
                    .Json(json => json
                        .WithJsonSerializerSettings(s =>
                            s.WithSerializationBinderOfType(typeof(DefaultSerializationBinder))));
            },
            "Cannot get SerializationBinder because an ISerializationBinder was previously set.");
        }

        [Fact]
        public void WithSerializationBinderOfTypeShouldThrowExceptionWithIncorrectValue()
        {
            Test.AssertException<JsonResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.JsonWithSettingsAction())
                        .ShouldReturn()
                        .Json(json => json
                            .WithJsonSerializerSettings(s =>
                                s.WithSerializationBinderOfType(typeof(DefaultSerializationBinder))));
                },
                "When calling JsonWithSettingsAction action in MvcController expected JSON result serializer settings to have serialization binder of DefaultSerializationBinder type, but in fact found null.");
        }

        [Fact]
        public void WithSerializationBinderOfTypeShouldNotThrowException()
        {
            var jsonSerializerSettings = TestObjectFactory.GetJsonSerializerSettings();
            jsonSerializerSettings.SerializationBinder = new DefaultSerializationBinder();

            MyController<MvcController>
                .Instance()
                .WithoutValidation()
                .Calling(c => c.JsonWithSpecificSettingsAction(jsonSerializerSettings))
                .ShouldReturn()
                .Json(json => json
                    .WithJsonSerializerSettings(s =>
                        s.WithSerializationBinderOfType<DefaultSerializationBinder>()));
        }

        [Fact]
        public void WithSerializationBinderOfTypeShouldThrowException()
        {
            var jsonSerializerSettings = TestObjectFactory.GetJsonSerializerSettings();
            jsonSerializerSettings.SerializationBinder = null;

            Test.AssertException<JsonResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.JsonWithSpecificSettingsAction(jsonSerializerSettings))
                        .ShouldReturn()
                        .Json(json => json
                            .WithJsonSerializerSettings(s =>
                                s.WithSerializationBinderOfType<DefaultSerializationBinder>()));
                },
                "When calling JsonWithSpecificSettingsAction action in MvcController expected JSON result serializer settings to have serialization binder of DefaultSerializationBinder type, but in fact found null.");
        }

        [Fact]
        public void WithPreviouslySetSerializationBinderOfTypeShouldThrowException()
        {
            var jsonSerializerSettings = TestObjectFactory.GetJsonSerializerSettings();
            jsonSerializerSettings.SerializationBinder = new DefaultSerializationBinder();

            Test.AssertException<InvalidOperationException>(() =>
            {
                MyController<MvcController>
                    .Instance()
                    .Calling(c => c.JsonWithSpecificSettingsAction(jsonSerializerSettings))
                    .ShouldReturn()
                    .Json(json => json
                        .WithJsonSerializerSettings(s =>
                            s.WithSerializationBinderOfType<DefaultSerializationBinder>()));
            },
            "Cannot get SerializationBinder because an ISerializationBinder was previously set.");
        }

        [Fact]
        public void AndAlsoShouldNotThrowExceptionWithCorrectValues()
        {
            MyController<MvcController>
                .Instance()
                .Calling(c => c.JsonWithSettingsAction())
                .ShouldReturn()
                .Json(json => json
                    .WithJsonSerializerSettings(s => s
                        .WithFormatting(Formatting.Indented)
                        .AndAlso()
                        .WithMaxDepth(2)
                        .AndAlso()
                        .WithConstructorHandling(ConstructorHandling.AllowNonPublicDefaultConstructor)));
        }

        [Fact]
        public void AndAlsoShouldThrowExceptionWithIncorrectValue()
        {
            Test.AssertException<JsonResultAssertionException>(
                () =>
                {
                    MyController<MvcController>
                        .Instance()
                        .Calling(c => c.JsonWithSettingsAction())
                        .ShouldReturn()
                        .Json(json => json
                            .WithJsonSerializerSettings(s => s
                                .WithFormatting(Formatting.Indented)
                                .AndAlso()
                                .WithMaxDepth(2)
                                .AndAlso()
                                .WithConstructorHandling(ConstructorHandling.Default)));
                },
                "When calling JsonWithSettingsAction action in MvcController expected JSON result serializer settings to have Default constructor handling, but in fact found AllowNonPublicDefaultConstructor.");
        }
    }
}
