namespace MyTested.Mvc.Builders.ActionResults.Json
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Runtime.Serialization.Formatters;
    using Base;
    using Contracts.ActionResults.Json;
    using Exceptions;
    using Internal.Extensions;
    using Microsoft.AspNet.Mvc;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;
    using Utilities;
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
        public JsonSerializerSettingsTestBuilder(
            Controller controller,
            string actionName)
            : base(controller, actionName)
        {
            this.jsonSerializerSettings = new JsonSerializerSettings();
            this.validations = new List<Action<JsonSerializerSettings, JsonSerializerSettings>>();
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
            where TContractResolver : IContractResolver
        {
            return this.WithContractResolverOfType(typeof(TContractResolver));
        }

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
                        $"{contractResolverType.Name}",
                        $"in fact found {actual.ContractResolver.GetName()}");
                }
            });

            return this;
        }

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
                if (!CommonValidator.CheckEquality(expected.MaxDepth, actual.MaxDepth))
                {
                    this.ThrowNewJsonResultAssertionException(
                        $"{(expected.MaxDepth != null ? expected.MaxDepth.ToString() : "no")} max depth",
                        $"in fact found {(actual.MaxDepth != null ? actual.MaxDepth.ToString() : "none")}");
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
        public IJsonSerializerSettingsTestBuilder AndAlso()
        {
            return this;
        }

        internal JsonSerializerSettings GetJsonSerializerSettings()
        {
            return this.jsonSerializerSettings;
        }

        internal ICollection<Action<JsonSerializerSettings, JsonSerializerSettings>> GetJsonSerializerSettingsValidations()
        {
            return this.validations;
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
