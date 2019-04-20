namespace MyTested.AspNetCore.Mvc.Builders.ActionResults.Json
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
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
    /// Used for testing <see cref="JsonSerializerSettings"/> in a <see cref="Microsoft.AspNetCore.Mvc.JsonResult"/>.
    /// </summary>
    public class JsonSerializerSettingsTestBuilder : BaseTestBuilderWithAction, IAndJsonSerializerSettingsTestBuilder
    {
        private readonly JsonSerializerSettings jsonSerializerSettings;
        private readonly ICollection<Action<JsonSerializerSettings, JsonSerializerSettings>> validations;

        /// <summary>
        /// Initializes a new instance of the <see cref="JsonSerializerSettingsTestBuilder"/> class.
        /// </summary>
        /// <param name="testContext"><see cref="ControllerTestContext"/> containing data about the currently executed assertion chain.</param>
        public JsonSerializerSettingsTestBuilder(ControllerTestContext testContext)
            : base(testContext)
        {
            this.jsonSerializerSettings = new JsonSerializerSettings();
            this.validations = new List<Action<JsonSerializerSettings, JsonSerializerSettings>>();
        }

        /// <inheritdoc />
        public IAndJsonSerializerSettingsTestBuilder WithCheckAdditionalContent(bool checkAdditionalContent)
        {
            this.jsonSerializerSettings.CheckAdditionalContent = checkAdditionalContent;
            this.validations.Add((expected, actual) =>
            {
                if (expected.CheckAdditionalContent != actual.CheckAdditionalContent)
                {
                    this.ThrowNewJsonResultAssertionException(
                        $"{(expected.CheckAdditionalContent ? "enabled" : "disabled")} checking for additional content",
                        $"in fact it was {(actual.CheckAdditionalContent ? "enabled" : "disabled")}");
                }
            });

            return this;
        }

        /// <inheritdoc />
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

        /// <inheritdoc />
        public IAndJsonSerializerSettingsTestBuilder WithContractResolver(IContractResolver contractResolver)
        {
            this.jsonSerializerSettings.ContractResolver = contractResolver;
            this.validations.Add((expected, actual) =>
            {
                if (expected.ContractResolver != actual.ContractResolver)
                {
                    this.ThrowNewJsonResultAssertionException(
                        "the same contract resolver as the provided one",
                        "in fact it was different");
                }
            });

            return this;
        }

        /// <inheritdoc />
        public IAndJsonSerializerSettingsTestBuilder WithContractResolverOfType<TContractResolver>()
            where TContractResolver : IContractResolver => this.WithContractResolverOfType(typeof(TContractResolver));

        /// <inheritdoc />
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

        /// <inheritdoc />
        public IAndJsonSerializerSettingsTestBuilder ContainingConverter(JsonConverter jsonConverter)
        {
            this.validations.Add((expected, actual) =>
            {
                if (!actual.Converters.Contains(jsonConverter))
                {
                    this.ThrowNewJsonResultAssertionException(
                        "the provided converter",
                        "such was not found");
                }
            });

            return this;
        }

        /// <inheritdoc />
        public IAndJsonSerializerSettingsTestBuilder ContainingConverterOfType<TJsonConverter>()
            where TJsonConverter : JsonConverter
        {
            this.validations.Add((expected, actual) =>
            {
                var expectedJsonConverterType = typeof(TJsonConverter);
                if (actual.Converters.All(c => Reflection.AreDifferentTypes(c.GetType(), expectedJsonConverterType)))
                {
                    this.ThrowNewJsonResultAssertionException(
                        $"converter of {expectedJsonConverterType.Name} type",
                        "such was not found");
                }
            });

            return this;
        }

        /// <inheritdoc />
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

        /// <inheritdoc />
        public IAndJsonSerializerSettingsTestBuilder ContainingConverters(params JsonConverter[] jsonConverters)
            => this.ContainingConverters(jsonConverters.AsEnumerable());

        /// <inheritdoc />
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

        /// <inheritdoc />
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

        /// <inheritdoc />
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

        /// <inheritdoc />
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

        /// <inheritdoc />
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

        /// <inheritdoc />
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

        /// <inheritdoc />
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

        /// <inheritdoc />
        public IAndJsonSerializerSettingsTestBuilder WithEqualityComparerOfType<TEqualityComparer>()
            where TEqualityComparer : IEqualityComparer => this.WithEqualityComparerOfType(typeof(TEqualityComparer));

        /// <inheritdoc />
        public IAndJsonSerializerSettingsTestBuilder WithEqualityComparerOfType(Type equalityComparerType)
        {
            this.validations.Add((expected, actual) =>
            {
                if (Reflection.AreDifferentTypes(equalityComparerType, actual.EqualityComparer?.GetType()))
                {
                    this.ThrowNewJsonResultAssertionException(
                        $"equality comparer of {equalityComparerType.ToFriendlyTypeName()} type",
                        $"in fact found {actual.EqualityComparer.GetName()}");
                }
            });

            return this;
        }

        /// <inheritdoc />
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

        /// <inheritdoc />
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

        /// <inheritdoc />
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

        /// <inheritdoc />
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

        /// <inheritdoc />
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

        /// <inheritdoc />
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

        /// <inheritdoc />
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

        /// <inheritdoc />
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

        /// <inheritdoc />
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

        /// <inheritdoc />
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

        /// <inheritdoc />
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

        /// <inheritdoc />
        public IAndJsonSerializerSettingsTestBuilder WithReferenceResolverOfType<TReferenceResolver>()
            where TReferenceResolver : IReferenceResolver => this.WithReferenceResolverOfType(typeof(TReferenceResolver));

        /// <inheritdoc />
        public IAndJsonSerializerSettingsTestBuilder WithReferenceResolverOfType(Type referenceResolverType)
        {
            this.validations.Add((expected, actual) =>
            {
                var actualReferenceResolverType = actual.ReferenceResolverProvider()?.GetType();
                if (Reflection.AreDifferentTypes(referenceResolverType, actualReferenceResolverType))
                {
                    this.ThrowNewJsonResultAssertionException(
                        $"reference resolver of {referenceResolverType.ToFriendlyTypeName()} type",
                        $"in fact found {actualReferenceResolverType.ToFriendlyTypeName()}");
                }
            });

            return this;
        }

        /// <inheritdoc />
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

        /// <inheritdoc />
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

        /// <inheritdoc />
        public IAndJsonSerializerSettingsTestBuilder WithTraceWriterOfType<TTraceWriter>()
            where TTraceWriter : ITraceWriter => this.WithTraceWriterOfType(typeof(TTraceWriter));

        /// <inheritdoc />
        public IAndJsonSerializerSettingsTestBuilder WithTraceWriterOfType(Type traceWriterType)
        {
            this.validations.Add((expected, actual) =>
            {
                var actualTraceWriterType = actual.TraceWriter?.GetType();
                if (Reflection.AreDifferentTypes(traceWriterType, actualTraceWriterType))
                {
                    this.ThrowNewJsonResultAssertionException(
                        $"trace writer of {traceWriterType.ToFriendlyTypeName()} type",
                        $"in fact found {actualTraceWriterType.ToFriendlyTypeName()}");
                }
            });

            return this;
        }

        /// <inheritdoc />
        public IAndJsonSerializerSettingsTestBuilder WithTypeNameAssemblyFormatHandling(
            TypeNameAssemblyFormatHandling typeNameAssemblyFormatHandling)
        {
            this.jsonSerializerSettings.TypeNameAssemblyFormatHandling = typeNameAssemblyFormatHandling;
            this.validations.Add((expected, actual) =>
            {
                if (!CommonValidator.CheckEquality(expected.TypeNameAssemblyFormatHandling, actual.TypeNameAssemblyFormatHandling))
                {
                    this.ThrowNewJsonResultAssertionException(
                        $"{expected.TypeNameAssemblyFormatHandling} type name assembly format handling",
                        $"in fact found {actual.TypeNameAssemblyFormatHandling}");
                }
            });

            return this;
        }

        /// <inheritdoc />
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

        /// <inheritdoc />
        public IJsonSerializerSettingsTestBuilder AndAlso() => this;

        internal JsonSerializerSettings GetJsonSerializerSettings() => this.jsonSerializerSettings;

        internal ICollection<Action<JsonSerializerSettings, JsonSerializerSettings>> GetJsonSerializerSettingsValidations()
            => this.validations;
        
        private static IList<string> SortJsonConverterNames(IEnumerable<JsonConverter> jsonConverters) 
            => jsonConverters
                .Select(of => of.GetType().ToFriendlyTypeName())
                .OrderBy(oft => oft)
                .ToList();

        private void ThrowNewJsonResultAssertionException(string expectedValue, string actualValue) 
            => throw new JsonResultAssertionException(string.Format(
                "When calling {0} action in {1} expected JSON result serializer settings to have {2}, but {3}.",
                this.ActionName,
                this.Controller.GetName(),
                expectedValue,
                actualValue));
    }
}
