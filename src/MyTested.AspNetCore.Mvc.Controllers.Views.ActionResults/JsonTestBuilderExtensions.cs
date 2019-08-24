namespace MyTested.AspNetCore.Mvc
{
    using Builders.Contracts.ActionResults.Json;
    using Newtonsoft.Json;
    using System;
    using Builders.ActionResults.Json;
    using Internal.TestContexts;
    using Utilities.Extensions;

    /// <summary>
    /// Contains extension methods for <see cref="IJsonTestBuilder" />.
    /// </summary>
    public static class JsonTestBuilderExtensions
    {
        /// <summary>
        /// Tests whether the <see cref="Microsoft.AspNetCore.Mvc.JsonResult"/>
        /// has the default <see cref="JsonSerializerSettings"/>.
        /// </summary>
        /// <param name="jsonTestBuilder">
        /// Instance of <see cref="IJsonTestBuilder"/> type.
        /// </param>
        /// <returns>The same <see cref="IAndJsonTestBuilder"/>.</returns>
        public static IAndJsonTestBuilder WithDefaultJsonSerializerSettings(
            this IJsonTestBuilder jsonTestBuilder)
            => jsonTestBuilder
                .WithJsonSerializerSettings(s => jsonTestBuilder
                    .PopulateFullJsonSerializerSettingsTestBuilder(s));

        /// <summary>
        /// Tests whether the <see cref="Microsoft.AspNetCore.Mvc.JsonResult"/>
        /// has the provided <see cref="JsonSerializerSettings"/>.
        /// </summary>
        /// <param name="jsonTestBuilder">
        /// Instance of <see cref="IJsonTestBuilder"/> type.
        /// </param>
        /// <param name="jsonSerializerSettings">Expected <see cref="JsonSerializerSettings"/> to test with.</param>
        /// <returns>The same <see cref="IAndJsonTestBuilder"/>.</returns>
        public static IAndJsonTestBuilder WithJsonSerializerSettings(
            this IJsonTestBuilder jsonTestBuilder,
            JsonSerializerSettings jsonSerializerSettings)
            => jsonTestBuilder
                .WithJsonSerializerSettings(s => jsonTestBuilder
                    .PopulateFullJsonSerializerSettingsTestBuilder(s, jsonSerializerSettings));

        /// <summary>
        /// Tests whether the <see cref="Microsoft.AspNetCore.Mvc.JsonResult"/>
        /// has <see cref="JsonSerializerSettings"/> by using builder.
        /// </summary>
        /// <param name="jsonTestBuilder">
        /// Instance of <see cref="IJsonTestBuilder"/> type.
        /// </param>
        /// <param name="jsonSerializerSettingsBuilder">Builder for testing <see cref="JsonSerializerSettings"/>.</param>
        /// <returns>The same <see cref="IAndJsonTestBuilder"/>.</returns>
        public static IAndJsonTestBuilder WithJsonSerializerSettings(
            this IJsonTestBuilder jsonTestBuilder,
            Action<IJsonSerializerSettingsTestBuilder> jsonSerializerSettingsBuilder)
        {
            var actualBuilder = (JsonTestBuilder)jsonTestBuilder;

            var actualJsonSerializerSettings = actualBuilder.GetJsonResult().SerializerSettings as JsonSerializerSettings
                ?? actualBuilder.GetServiceDefaultSerializerSettings()
                ?? new JsonSerializerSettings();

            var newJsonSerializerSettingsTestBuilder = new JsonSerializerSettingsTestBuilder(
                actualBuilder.TestContext as ControllerTestContext);

            jsonSerializerSettingsBuilder(newJsonSerializerSettingsTestBuilder);
            var expectedJsonSerializerSettings = newJsonSerializerSettingsTestBuilder.GetJsonSerializerSettings();

            var validations = newJsonSerializerSettingsTestBuilder.GetJsonSerializerSettingsValidations();
            validations.ForEach(v => v(expectedJsonSerializerSettings, actualJsonSerializerSettings));

            return actualBuilder;
        }

        private static void PopulateFullJsonSerializerSettingsTestBuilder(
            this IJsonTestBuilder jsonTestBuilder,
            IJsonSerializerSettingsTestBuilder jsonSerializerSettingsTestBuilder,
            JsonSerializerSettings jsonSerializerSettings = null)
        {
            var actualBuilder = (JsonTestBuilder)jsonTestBuilder;

            jsonSerializerSettings = jsonSerializerSettings
                 ?? actualBuilder.GetServiceDefaultSerializerSettings()
                 ?? new JsonSerializerSettings();

            jsonSerializerSettingsTestBuilder
                .WithCulture(jsonSerializerSettings.Culture)
                .WithContractResolverOfType(jsonSerializerSettings.ContractResolver?.GetType())
                .WithConstructorHandling(jsonSerializerSettings.ConstructorHandling)
                .WithDateFormatHandling(jsonSerializerSettings.DateFormatHandling)
                .WithDateParseHandling(jsonSerializerSettings.DateParseHandling)
                .WithDateTimeZoneHandling(jsonSerializerSettings.DateTimeZoneHandling)
                .WithDefaultValueHandling(jsonSerializerSettings.DefaultValueHandling)
                .WithFormatting(jsonSerializerSettings.Formatting)
                .WithMaxDepth(jsonSerializerSettings.MaxDepth)
                .WithMissingMemberHandling(jsonSerializerSettings.MissingMemberHandling)
                .WithNullValueHandling(jsonSerializerSettings.NullValueHandling)
                .WithObjectCreationHandling(jsonSerializerSettings.ObjectCreationHandling)
                .WithPreserveReferencesHandling(jsonSerializerSettings.PreserveReferencesHandling)
                .WithReferenceLoopHandling(jsonSerializerSettings.ReferenceLoopHandling)
                .WithTypeNameAssemblyFormatHandling(jsonSerializerSettings.TypeNameAssemblyFormatHandling)
                .WithTypeNameHandling(jsonSerializerSettings.TypeNameHandling);
        }
    }
}
