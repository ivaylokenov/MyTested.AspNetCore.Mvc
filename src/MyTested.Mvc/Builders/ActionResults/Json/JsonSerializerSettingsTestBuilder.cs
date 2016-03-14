namespace MyTested.Mvc.Builders.ActionResults.Json
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Runtime.Serialization.Formatters;
    using Base;
    using Contracts.ActionResults.Json;
    using Exceptions;
    using Internal.TestContexts;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;
    using Utilities;
    using Utilities.Extensions;
    using Utilities.Validators;

    /// <summary>
    /// Used for testing JSON serializer settings in a JSON result.
    /// </summary>
    public class JsonSerializerSettingsTestBuilder : BaseTestBuilderWithAction, IAndJsonSerializerSettingsTestBuilder
    {
        private readonly JsonSerializerSettings jsonSerializerSettings;
        private readonly ICollection<Action<JsonSerializerSettings, JsonSerializerSettings>> validations;

        /// <summary>
        /// Initializes a new instance of the <see cref="JsonSerializerSettingsTestBuilder" /> class.
        /// </summary>
        /// <param name="controller">Controller on which the action will be tested.</param>
        /// <param name="actionName">Name of the tested action.</param>
        public JsonSerializerSettingsTestBuilder(ControllerTestContext testContext)
            : base(testContext)
        {
            this.jsonSerializerSettings = new JsonSerializerSettings();
            this.validations = new List<Action<JsonSerializerSettings, JsonSerializerSettings>>();
        }

        public IAndJsonSerializerSettingsTestBuilder WithCheckAdditionalContent(bool checkAdditionalContent)
        {
            this.jsonSerializerSettings.CheckAdditionalContent = checkAdditionalContent;
            this.validations.Add((expected, actual) =>
            {
                if (expected.CheckAdditionalContent != actual.CheckAdditionalContent)
                {
                    this.ThrowNewJsonResultAssertionException(
                        $"{(expected.CheckAdditionalContent ? "enabled" : "disabled")} checking for additional content",
                        $"in fact it was {(expected.CheckAdditionalContent ? "enabled" : "disabled")}");
                }
            });

            return this;
        }

        /// <summary>
        /// Tests the Culture property in a JSON serializer settings object.
        /// </summary>
        /// <param name="culture">Expected Culture.</param>
        /// <returns>The same JSON serializer settings test builder.</returns>
        public IAndJsonSerializerSettingsTestBuilder WithCulture(CultureInfo culture)
        {
            this.jsonSerializerSettings.Culture = culture;
            this.validations.Add((expected, actual) =>
            {
                if (expected.Culture.DisplayName != actual.Culture.DisplayName)
                {
                    this.ThrowNewJsonResultAssertionException(
                        $"'{expected.Culture.DisplayName}' culture",
                        $"in fact found '{actual.Culture.DisplayName}'");
                }
            });

            return this;
        }

        /// <summary>
        /// Tests the ContractResolver property in a JSON serializer settings object.
        /// </summary>
        /// <param name="contractResolver">Expected ContractResolver.</param>
        /// <returns>The same JSON serializer settings test builder.</returns>
        public IAndJsonSerializerSettingsTestBuilder WithContractResolver(IContractResolver contractResolver)
        {
            this.jsonSerializerSettings.ContractResolver = contractResolver;
            this.validations.Add((expected, actual) =>
            {
                if (expected.ContractResolver != actual.ContractResolver)
                {
                    this.ThrowNewJsonResultAssertionException(
                        $"the same contract resolver as the provided one",
                        $"in fact it was different");
                }
            });

            return this;
        }

        /// <summary>
        /// Tests the ContractResolver property in a JSON serializer settings object by using generic type.
        /// </summary>
        /// <typeparam name="TContractResolver">Expected ContractResolver type.</typeparam>
        /// <returns>The same JSON serializer settings test builder.</returns>
        public IAndJsonSerializerSettingsTestBuilder WithContractResolverOfType<TContractResolver>()
            where TContractResolver : IContractResolver => this.WithContractResolverOfType(typeof(TContractResolver));

        /// <summary>
        /// Tests the ContractResolver property in a JSON serializer settings object by using a provided type.
        /// </summary>
        /// <param name="contractResolverType">Expected ContractResolver type.</param>
        /// <returns>The same JSON serializer settings test builder.</returns>
        public IAndJsonSerializerSettingsTestBuilder WithContractResolverOfType(Type contractResolverType)
        {
            this.validations.Add((expected, actual) =>
            {
                if (Reflection.AreDifferentTypes(contractResolverType, actual.ContractResolver?.GetType()))
                {
                    this.ThrowNewJsonResultAssertionException(
                        $"{contractResolverType.ToFriendlyTypeName()}",
                        $"in fact found {actual.ContractResolver.GetName()}");
                }
            });

            return this;
        }

        public IAndJsonSerializerSettingsTestBuilder ContainingConverter(JsonConverter jsonConverter)
        {
            this.validations.Add((expected, actual) =>
            {
                if (!actual.Converters.Contains(jsonConverter))
                {
                    this.ThrowNewJsonResultAssertionException(
                        $"the provided converter",
                        "such was not found");
                }
            });

            return this;
        }

        public IAndJsonSerializerSettingsTestBuilder ContainingConverterOfType<TJsonConverter>()
            where TJsonConverter : JsonConverter
        {
            this.validations.Add((expected, actual) =>
            {
                var expectedJsonConverterType = typeof(TJsonConverter);
                if (!actual.Converters.All(c => Reflection.AreDifferentTypes(c.GetType(), expectedJsonConverterType)))
                {
                    this.ThrowNewJsonResultAssertionException(
                        $"converter of {expectedJsonConverterType.Name} type",
                        "such was not found");
                }
            });

            return this;
        }

        public IAndJsonSerializerSettingsTestBuilder ContainingConverters(IEnumerable<JsonConverter> jsonConverters)
        {
            this.validations.Add((expected, actual) =>
            {
                var actualJsonConverters = SortJsonConverterNames(actual.Converters);
                var expectedJsonConverters = SortJsonConverterNames(jsonConverters);

                if (actualJsonConverters.Count != expectedJsonConverters.Count)
                {
                    this.ThrowNewJsonResultAssertionException(
                        $"{expectedJsonConverters.Count} {(expectedJsonConverters.Count != 1 ? "converters" : "converter")}",
                        $"instead found {actualJsonConverters.Count}");
                }

                for (int i = 0; i < actualJsonConverters.Count; i++)
                {
                    var actualJsonConverter = actualJsonConverters[i];
                    var expectedJsonConverter = expectedJsonConverters[i];
                    if (actualJsonConverter != expectedJsonConverter)
                    {
                        this.ThrowNewJsonResultAssertionException(
                            $"converter of {expectedJsonConverter} type",
                            "none was found");
                    }
                }
            });

            return this;
        }

        public IAndJsonSerializerSettingsTestBuilder ContainingConverters(params JsonConverter[] jsonConverters)
            => this.ContainingConverters(jsonConverters.AsEnumerable());

        /// <summary>
        /// Tests the ConstructorHandling property in a JSON serializer settings object.
        /// </summary>
        /// <param name="constructorHandling">Expected ConstructorHandling.</param>
        /// <returns>The same JSON serializer settings test builder.</returns>
        public IAndJsonSerializerSettingsTestBuilder WithConstructorHandling(ConstructorHandling constructorHandling)
        {
            this.jsonSerializerSettings.ConstructorHandling = constructorHandling;
            this.validations.Add((expected, actual) =>
            {
                if (!CommonValidator.CheckEquality(expected.ConstructorHandling, actual.ConstructorHandling))
                {
                    this.ThrowNewJsonResultAssertionException(
                        $"{expected.ConstructorHandling} constructor handling",
                        $"in fact found {actual.ConstructorHandling}");
                }
            });

            return this;
        }

        /// <summary>
        /// Tests the DateFormatHandling property in a JSON serializer settings object.
        /// </summary>
        /// <param name="dateFormatHandling">Expected DateFormatHandling.</param>
        /// <returns>The same JSON serializer settings test builder.</returns>
        public IAndJsonSerializerSettingsTestBuilder WithDateFormatHandling(DateFormatHandling dateFormatHandling)
        {
            this.jsonSerializerSettings.DateFormatHandling = dateFormatHandling;
            this.validations.Add((expected, actual) =>
            {
                if (!CommonValidator.CheckEquality(expected.DateFormatHandling, actual.DateFormatHandling))
                {
                    this.ThrowNewJsonResultAssertionException(
                        $"{expected.DateFormatHandling} date format handling",
                        $"in fact found {actual.DateFormatHandling}");
                }
            });

            return this;
        }

        public IAndJsonSerializerSettingsTestBuilder WithDateFormatString(string dateFormatString)
        {
            this.jsonSerializerSettings.DateFormatString = dateFormatString;
            this.validations.Add((expected, actual) =>
            {
                if (expected.DateFormatString != actual.DateFormatString)
                {
                    this.ThrowNewJsonResultAssertionException(
                        $"'{expected.DateFormatString}' date format string",
                        $"in fact found '{actual.DateFormatString}'");
                }
            });

            return this;
        }

        /// <summary>
        /// Tests the DateParseHandling property in a JSON serializer settings object.
        /// </summary>
        /// <param name="dateParseHandling">Expected DateParseHandling.</param>
        /// <returns>The same JSON serializer settings test builder.</returns>
        public IAndJsonSerializerSettingsTestBuilder WithDateParseHandling(DateParseHandling dateParseHandling)
        {
            this.jsonSerializerSettings.DateParseHandling = dateParseHandling;
            this.validations.Add((expected, actual) =>
            {
                if (!CommonValidator.CheckEquality(expected.DateParseHandling, actual.DateParseHandling))
                {
                    this.ThrowNewJsonResultAssertionException(
                        $"{expected.DateParseHandling} date parse handling",
                        $"in fact found {actual.DateParseHandling}");
                }
            });

            return this;
        }

        /// <summary>
        /// Tests the WithDateTimeZoneHandling property in a JSON serializer settings object.
        /// </summary>
        /// <param name="dateTimeZoneHandling">Expected WithDateTimeZoneHandling.</param>
        /// <returns>The same JSON serializer settings test builder.</returns>
        public IAndJsonSerializerSettingsTestBuilder WithDateTimeZoneHandling(DateTimeZoneHandling dateTimeZoneHandling)
        {
            this.jsonSerializerSettings.DateTimeZoneHandling = dateTimeZoneHandling;
            this.validations.Add((expected, actual) =>
            {
                if (!CommonValidator.CheckEquality(expected.DateTimeZoneHandling, actual.DateTimeZoneHandling))
                {
                    this.ThrowNewJsonResultAssertionException(
                        $"{expected.DateTimeZoneHandling} date time zone handling",
                        $"in fact found {actual.DateTimeZoneHandling}");
                }
            });

            return this;
        }

        /// <summary>
        /// Tests the DefaultValueHandling property in a JSON serializer settings object.
        /// </summary>
        /// <param name="defaultValueHandling">Expected DefaultValueHandling.</param>
        /// <returns>The same JSON serializer settings test builder.</returns>
        public IAndJsonSerializerSettingsTestBuilder WithDefaultValueHandling(DefaultValueHandling defaultValueHandling)
        {
            this.jsonSerializerSettings.DefaultValueHandling = defaultValueHandling;
            this.validations.Add((expected, actual) =>
            {
                if (!CommonValidator.CheckEquality(expected.DefaultValueHandling, actual.DefaultValueHandling))
                {
                    this.ThrowNewJsonResultAssertionException(
                        $"{expected.DefaultValueHandling} default value handling",
                        $"in fact found {actual.DefaultValueHandling}");
                }
            });

            return this;
        }

        public IAndJsonSerializerSettingsTestBuilder WithEqualityComparer(IEqualityComparer equalityComparer)
        {
            this.jsonSerializerSettings.EqualityComparer = equalityComparer;
            this.validations.Add((expected, actual) =>
            {
                if (expected.EqualityComparer != actual.EqualityComparer)
                {
                    this.ThrowNewJsonResultAssertionException(
                        $"the same equality comparer as the provided one",
                        $"in fact it was different");
                }
            });

            return this;
        }

        public IAndJsonSerializerSettingsTestBuilder WithEqualityComparerOfType<TEqualityComparer>()
            where TEqualityComparer : IEqualityComparer => this.WithEqualityComparerOfType(typeof(TEqualityComparer));

        public IAndJsonSerializerSettingsTestBuilder WithEqualityComparerOfType(Type equalityComparerType)
        {
            this.validations.Add((expected, actual) =>
            {
                if (Reflection.AreDifferentTypes(equalityComparerType, actual.EqualityComparer?.GetType()))
                {
                    this.ThrowNewJsonResultAssertionException(
                        $"{equalityComparerType.ToFriendlyTypeName()}",
                        $"in fact found {actual.EqualityComparer.GetName()}");
                }
            });

            return this;
        }

        public IAndJsonSerializerSettingsTestBuilder WithFloatFormatHandling(FloatFormatHandling floatFormatHandling)
        {
            this.jsonSerializerSettings.FloatFormatHandling = floatFormatHandling;
            this.validations.Add((expected, actual) =>
            {
                if (!CommonValidator.CheckEquality(expected.FloatFormatHandling, actual.FloatFormatHandling))
                {
                    this.ThrowNewJsonResultAssertionException(
                        $"{expected.FloatFormatHandling} float format handling",
                        $"in fact found {actual.FloatFormatHandling}");
                }
            });

            return this;
        }

        public IAndJsonSerializerSettingsTestBuilder WithFloatParseHandling(FloatParseHandling floatParseHandling)
        {
            this.jsonSerializerSettings.FloatParseHandling = floatParseHandling;
            this.validations.Add((expected, actual) =>
            {
                if (!CommonValidator.CheckEquality(expected.FloatParseHandling, actual.FloatParseHandling))
                {
                    this.ThrowNewJsonResultAssertionException(
                        $"{expected.FloatParseHandling} float parse handling",
                        $"in fact found {actual.FloatParseHandling}");
                }
            });

            return this;
        }

        /// <summary>
        /// Tests the Formatting property in a JSON serializer settings object.
        /// </summary>
        /// <param name="formatting">Expected Formatting.</param>
        /// <returns>The same JSON serializer settings test builder.</returns>
        public IAndJsonSerializerSettingsTestBuilder WithFormatting(Formatting formatting)
        {
            this.jsonSerializerSettings.Formatting = formatting;
            this.validations.Add((expected, actual) =>
            {
                if (!CommonValidator.CheckEquality(expected.Formatting, actual.Formatting))
                {
                    this.ThrowNewJsonResultAssertionException(
                        $"{expected.Formatting} formatting",
                        $"in fact found {actual.Formatting}");
                }
            });

            return this;
        }

        /// <summary>
        /// Tests the MaxDepth property in a JSON serializer settings object.
        /// </summary>
        /// <param name="maxDepth">Expected max depth.</param>
        /// <returns>The same JSON serializer settings test builder.</returns>
        public IAndJsonSerializerSettingsTestBuilder WithMaxDepth(int? maxDepth)
        {
            this.jsonSerializerSettings.MaxDepth = maxDepth;
            this.validations.Add((expected, actual) =>
            {
                var expectedMaxDepth = expected.MaxDepth;
                var actualMaxDepth = actual.MaxDepth;

                if (!CommonValidator.CheckEquality(expectedMaxDepth, actualMaxDepth))
                {
                    this.ThrowNewJsonResultAssertionException(
                        $"{expectedMaxDepth.GetErrorMessageName(false, "no")} max depth",
                        $"in fact found {actualMaxDepth.GetErrorMessageName(false, "none")}");
                }
            });

            return this;
        }

        public IAndJsonSerializerSettingsTestBuilder WithMetadataPropertyHandling(MetadataPropertyHandling metadataPropertyHandling)
        {
            this.jsonSerializerSettings.MetadataPropertyHandling = metadataPropertyHandling;
            this.validations.Add((expected, actual) =>
            {
                if (!CommonValidator.CheckEquality(expected.MetadataPropertyHandling, actual.MetadataPropertyHandling))
                {
                    this.ThrowNewJsonResultAssertionException(
                        $"{expected.MetadataPropertyHandling} metadata property handling",
                        $"in fact found {actual.MetadataPropertyHandling}");
                }
            });

            return this;
        }

        /// <summary>
        /// Tests the MissingMemberHandling property in a JSON serializer settings object.
        /// </summary>
        /// <param name="missingMemberHandling">Expected MissingMemberHandling.</param>
        /// <returns>The same JSON serializer settings test builder.</returns>
        public IAndJsonSerializerSettingsTestBuilder WithMissingMemberHandling(MissingMemberHandling missingMemberHandling)
        {
            this.jsonSerializerSettings.MissingMemberHandling = missingMemberHandling;
            this.validations.Add((expected, actual) =>
            {
                if (!CommonValidator.CheckEquality(expected.MissingMemberHandling, actual.MissingMemberHandling))
                {
                    this.ThrowNewJsonResultAssertionException(
                        $"{expected.MissingMemberHandling} missing member handling",
                        $"in fact found {actual.MissingMemberHandling}");
                }
            });

            return this;
        }

        /// <summary>
        /// Tests the NullValueHandling property in a JSON serializer settings object.
        /// </summary>
        /// <param name="nullValueHandling">Expected NullValueHandling.</param>
        /// <returns>The same JSON serializer settings test builder.</returns>
        public IAndJsonSerializerSettingsTestBuilder WithNullValueHandling(NullValueHandling nullValueHandling)
        {
            this.jsonSerializerSettings.NullValueHandling = nullValueHandling;
            this.validations.Add((expected, actual) =>
            {
                if (!CommonValidator.CheckEquality(expected.NullValueHandling, actual.NullValueHandling))
                {
                    this.ThrowNewJsonResultAssertionException(
                        $"{expected.NullValueHandling} null value handling",
                        $"in fact found {actual.NullValueHandling}");
                }
            });

            return this;
        }

        /// <summary>
        /// Tests the ObjectCreationHandling property in a JSON serializer settings object.
        /// </summary>
        /// <param name="objectCreationHandling">Expected ObjectCreationHandling.</param>
        /// <returns>The same JSON serializer settings test builder.</returns>
        public IAndJsonSerializerSettingsTestBuilder WithObjectCreationHandling(ObjectCreationHandling objectCreationHandling)
        {
            this.jsonSerializerSettings.ObjectCreationHandling = objectCreationHandling;
            this.validations.Add((expected, actual) =>
            {
                if (!CommonValidator.CheckEquality(expected.ObjectCreationHandling, actual.ObjectCreationHandling))
                {
                    this.ThrowNewJsonResultAssertionException(
                        $"{expected.ObjectCreationHandling} object creation handling",
                        $"in fact found {actual.ObjectCreationHandling}");
                }
            });

            return this;
        }

        /// <summary>
        /// Tests the PreserveReferencesHandling property in a JSON serializer settings object.
        /// </summary>
        /// <param name="preserveReferencesHandling">Expected PreserveReferencesHandling.</param>
        /// <returns>The same JSON serializer settings test builder.</returns>
        public IAndJsonSerializerSettingsTestBuilder WithPreserveReferencesHandling(PreserveReferencesHandling preserveReferencesHandling)
        {
            this.jsonSerializerSettings.PreserveReferencesHandling = preserveReferencesHandling;
            this.validations.Add((expected, actual) =>
            {
                if (!CommonValidator.CheckEquality(
                        expected.PreserveReferencesHandling,
                        actual.PreserveReferencesHandling))
                {
                    this.ThrowNewJsonResultAssertionException(
                        $"{expected.PreserveReferencesHandling} preserve references handling",
                        $"in fact found {actual.PreserveReferencesHandling}");
                }
            });

            return this;
        }

        /// <summary>
        /// Tests the ReferenceLoopHandling property in a JSON serializer settings object.
        /// </summary>
        /// <param name="referenceLoopHandling">Expected ReferenceLoopHandling.</param>
        /// <returns>The same JSON serializer settings test builder.</returns>
        public IAndJsonSerializerSettingsTestBuilder WithReferenceLoopHandling(ReferenceLoopHandling referenceLoopHandling)
        {
            this.jsonSerializerSettings.ReferenceLoopHandling = referenceLoopHandling;
            this.validations.Add((expected, actual) =>
            {
                if (!CommonValidator.CheckEquality(expected.ReferenceLoopHandling, actual.ReferenceLoopHandling))
                {
                    this.ThrowNewJsonResultAssertionException(
                        $"{expected.ReferenceLoopHandling} reference loop handling",
                        $"in fact found {actual.ReferenceLoopHandling}");
                }
            });

            return this;
        }

        public IAndJsonSerializerSettingsTestBuilder WithReferenceResolver(IReferenceResolver referenceResolver)
        {
            this.jsonSerializerSettings.ReferenceResolverProvider = () => referenceResolver;
            this.validations.Add((expected, actual) =>
            {
                if (expected.ReferenceResolverProvider() != actual.ReferenceResolverProvider())
                {
                    this.ThrowNewJsonResultAssertionException(
                        $"the same reference resolver as the provided one",
                        $"in fact it was different");
                }
            });

            return this;
        }

        public IAndJsonSerializerSettingsTestBuilder WithReferenceResolverOfType<TReferenceResolver>()
            where TReferenceResolver : IReferenceResolver => this.WithReferenceResolverOfType(typeof(TReferenceResolver));

        public IAndJsonSerializerSettingsTestBuilder WithReferenceResolverOfType(Type referenceResolverType)
        {
            this.validations.Add((expected, actual) =>
            {
                var actualReferenceResolverType = actual.ReferenceResolverProvider()?.GetType();
                if (Reflection.AreDifferentTypes(referenceResolverType, actualReferenceResolverType))
                {
                    this.ThrowNewJsonResultAssertionException(
                        $"{referenceResolverType.ToFriendlyTypeName()}",
                        $"in fact found {actualReferenceResolverType.GetName()}");
                }
            });

            return this;
        }

        public IAndJsonSerializerSettingsTestBuilder WithStringEscapeHandling(StringEscapeHandling stringEscapeHandling)
        {
            this.jsonSerializerSettings.StringEscapeHandling = stringEscapeHandling;
            this.validations.Add((expected, actual) =>
            {
                if (!CommonValidator.CheckEquality(expected.StringEscapeHandling, actual.StringEscapeHandling))
                {
                    this.ThrowNewJsonResultAssertionException(
                        $"{expected.StringEscapeHandling} string escape handling",
                        $"in fact found {actual.StringEscapeHandling}");
                }
            });

            return this;
        }

        public IAndJsonSerializerSettingsTestBuilder WithTraceWriter(ITraceWriter traceWriter)
        {
            this.jsonSerializerSettings.TraceWriter = traceWriter;
            this.validations.Add((expected, actual) =>
            {
                if (expected.TraceWriter != actual.TraceWriter)
                {
                    this.ThrowNewJsonResultAssertionException(
                        $"the same trace writer as the provided one",
                        $"in fact it was different");
                }
            });

            return this;
        }

        public IAndJsonSerializerSettingsTestBuilder WithTraceWriterOfType<TTraceWriter>()
            where TTraceWriter : ITraceWriter => this.WithTraceWriterOfType(typeof(TTraceWriter));

        public IAndJsonSerializerSettingsTestBuilder WithTraceWriterOfType(Type traceWriterType)
        {
            this.validations.Add((expected, actual) =>
            {
                var actualTraceWriterType = actual.TraceWriter?.GetType();
                if (Reflection.AreDifferentTypes(traceWriterType, actualTraceWriterType))
                {
                    this.ThrowNewJsonResultAssertionException(
                        $"{traceWriterType.ToFriendlyTypeName()}",
                        $"in fact found {actualTraceWriterType.GetName()}");
                }
            });

            return this;
        }

        /// <summary>
        /// Tests the FormatterAssemblyStyle property in a JSON serializer settings object.
        /// </summary>
        /// <param name="typeNameAssemblyFormat">Expected FormatterAssemblyStyle.</param>
        /// <returns>The same JSON serializer settings test builder.</returns>
        public IAndJsonSerializerSettingsTestBuilder WithTypeNameAssemblyFormat(FormatterAssemblyStyle typeNameAssemblyFormat)
        {
            this.jsonSerializerSettings.TypeNameAssemblyFormat = typeNameAssemblyFormat;
            this.validations.Add((expected, actual) =>
            {
                if (!CommonValidator.CheckEquality(expected.TypeNameAssemblyFormat, actual.TypeNameAssemblyFormat))
                {
                    this.ThrowNewJsonResultAssertionException(
                        $"{expected.TypeNameAssemblyFormat} type name assembly format",
                        $"in fact found {actual.TypeNameAssemblyFormat}");
                }
            });

            return this;
        }

        /// <summary>
        /// Tests the TypeNameHandling property in a JSON serializer settings object.
        /// </summary>
        /// <param name="typeNameHandling">Expected TypeNameHandling.</param>
        /// <returns>The same JSON serializer settings test builder.</returns>
        public IAndJsonSerializerSettingsTestBuilder WithTypeNameHandling(TypeNameHandling typeNameHandling)
        {
            this.jsonSerializerSettings.TypeNameHandling = typeNameHandling;
            this.validations.Add((expected, actual) =>
            {
                if (!CommonValidator.CheckEquality(expected.TypeNameHandling, actual.TypeNameHandling))
                {
                    this.ThrowNewJsonResultAssertionException(
                        $"{expected.TypeNameHandling} type name handling",
                        $"in fact found {actual.TypeNameHandling}");
                }
            });
            return this;
        }

        /// <summary>
        /// AndAlso method for better readability when chaining JSON serializer settings test builder.
        /// </summary>
        /// <returns>JSON serializer settings test builder.</returns>
        public IJsonSerializerSettingsTestBuilder AndAlso() => this;

        internal JsonSerializerSettings GetJsonSerializerSettings() => this.jsonSerializerSettings;

        internal ICollection<Action<JsonSerializerSettings, JsonSerializerSettings>> GetJsonSerializerSettingsValidations()
            => this.validations;
        
        private static IList<string> SortJsonConverterNames(IEnumerable<JsonConverter> jsonConverters)
        {
            return jsonConverters
                .Select(of => of.GetType().ToFriendlyTypeName())
                .OrderBy(oft => oft)
                .ToList();
        }

        private void ThrowNewJsonResultAssertionException(string expectedValue, string actualValue)
        {
            throw new JsonResultAssertionException(string.Format(
                        "When calling {0} action in {1} expected JSON result serializer settings to have {2}, but {3}.",
                        this.ActionName,
                        this.Controller.GetName(),
                        expectedValue,
                        actualValue));
        }
    }
}
