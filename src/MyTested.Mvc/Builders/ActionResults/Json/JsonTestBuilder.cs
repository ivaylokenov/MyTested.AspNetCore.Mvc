namespace MyTested.Mvc.Builders.ActionResults.Json
{
    using System;
    using System.Net;
    using Base;
    using Contracts.ActionResults.Json;
    using Exceptions;
    using Internal;
    using Internal.Extensions;
    using Microsoft.AspNet.Mvc;
    using Microsoft.Extensions.Options;
    using Microsoft.Net.Http.Headers;
    using Newtonsoft.Json;
    using Utilities.Validators;

    /// <summary>
    /// Used for testing JSON results.
    /// </summary>
    public class JsonTestBuilder : BaseTestBuilderWithResponseModel<JsonResult>, IAndJsonTestBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="JsonTestBuilder" /> class.
        /// </summary>
        /// <param name="controller">Controller on which the action will be tested.</param>
        /// <param name="actionName">Name of the tested action.</param>
        /// <param name="caughtException">Caught exception during the action execution.</param>
        /// <param name="jsonResult">Result from the tested action.</param>
        public JsonTestBuilder(
            Controller controller,
            string actionName,
            Exception caughtException,
            JsonResult jsonResult)
            : base(controller, actionName, caughtException, jsonResult)
        {
        }

        /// <summary>
        /// Tests whether JSON result has the same status code as the provided one.
        /// </summary>
        /// <param name="statusCode">Status code.</param>
        /// <returns>The same JSON test builder.</returns>
        public IAndJsonTestBuilder WithStatusCode(int statusCode)
        {
            return this.WithStatusCode((HttpStatusCode)statusCode);
        }

        /// <summary>
        /// Tests whether JSON result has the same status code as the provided HttpStatusCode.
        /// </summary>
        /// <param name="statusCode">HttpStatusCode enumeration.</param>
        /// <returns>The same JSON test builder.</returns>
        public IAndJsonTestBuilder WithStatusCode(HttpStatusCode statusCode)
        {
            HttpStatusCodeValidator.ValidateHttpStatusCode(
                statusCode,
                this.ActionResult.StatusCode,
                this.ThrowNewJsonResultAssertionException);

            return this;
        }

        /// <summary>
        /// Tests whether JSON result has the same content type as the provided string.
        /// </summary>
        /// <param name="contentType">ContentType type as string.</param>
        /// <returns>The same JSON test builder.</returns>
        public IAndJsonTestBuilder WithContentType(string contentType)
        {
            return this.WithContentType(new MediaTypeHeaderValue(contentType));
        }

        /// <summary>
        /// Tests whether JSON result has the same content type as the provided MediaTypeHeaderValue.
        /// </summary>
        /// <param name="contentType">Content type as MediaTypeHeaderValue.</param>
        /// <returns>The same JSON test builder.</returns>
        public IAndJsonTestBuilder WithContentType(MediaTypeHeaderValue contentType)
        {
            ContentTypeValidator.ValidateContentType(
                contentType,
                this.ActionResult.ContentType,
                this.ThrowNewJsonResultAssertionException);

            return this;
        }

        /// <summary>
        /// Tests whether JSON result has the default JSON serializer settings.
        /// </summary>
        /// <returns>The same JSON test builder.</returns>
        public IAndJsonTestBuilder WithDefaulJsonSerializerSettings()
        {
            return this.WithJsonSerializerSettings(s => this.PopulateFullJsonSerializerSettingsTestBuilder(s));
        }

        /// <summary>
        /// Tests whether JSON result has the provided JSON serializer settings.
        /// </summary>
        /// <param name="jsonSerializerSettings">Expected JSON serializer settings to test with.</param>
        /// <returns>The same JSON test builder.</returns>
        public IAndJsonTestBuilder WithJsonSerializerSettings(JsonSerializerSettings jsonSerializerSettings)
        {
            return this.WithJsonSerializerSettings(s => this.PopulateFullJsonSerializerSettingsTestBuilder(s, jsonSerializerSettings));
        }

        /// <summary>
        /// Tests whether JSON result has JSON serializer settings by using builder.
        /// </summary>
        /// <param name="jsonSerializerSettingsBuilder">Builder for creating JSON serializer settings.</param>
        /// <returns>The same JSON test builder.</returns>
        public IAndJsonTestBuilder WithJsonSerializerSettings(
            Action<IJsonSerializerSettingsTestBuilder> jsonSerializerSettingsBuilder)
        {
            var actualJsonSerializerSettings = this.ActionResult.SerializerSettings
                ?? this.GetServiceDefaultSerializerSettings()
                ?? new JsonSerializerSettings();

            var newJsonSerializerSettingsTestBuilder = new JsonSerializerSettingsTestBuilder(this.Controller, this.ActionName);
            jsonSerializerSettingsBuilder(newJsonSerializerSettingsTestBuilder);
            var expectedJsonSerializerSettings = newJsonSerializerSettingsTestBuilder.GetJsonSerializerSettings();

            var validations = newJsonSerializerSettingsTestBuilder.GetJsonSerializerSettingsValidations();
            validations.ForEach(v => v(expectedJsonSerializerSettings, actualJsonSerializerSettings));

            return this;
        }

        /// <summary>
        /// AndAlso method for better readability when chaining JSON result tests.
        /// </summary>
        /// <returns>JSON result test builder.</returns>
        public IJsonTestBuilder AndAlso()
        {
            return this;
        }

        protected override void ThrowNewFailedValidationException(string propertyName, string expectedValue, string actualValue)
            => this.ThrowNewJsonResultAssertionException(propertyName, expectedValue, actualValue);

        private JsonSerializerSettings GetServiceDefaultSerializerSettings()
        {
            return TestServiceProvider.GetService<IOptions<MvcJsonOptions>>()?.Value?.SerializerSettings;
        }

        private void PopulateFullJsonSerializerSettingsTestBuilder(
            IJsonSerializerSettingsTestBuilder jsonSerializerSettingsTestBuilder,
            JsonSerializerSettings jsonSerializerSettings = null)
        {
            var contractResolver = jsonSerializerSettings != null ? jsonSerializerSettings.ContractResolver : null;
            jsonSerializerSettings = jsonSerializerSettings
                ?? this.GetServiceDefaultSerializerSettings()
                ?? new JsonSerializerSettings();

            jsonSerializerSettingsTestBuilder
                .WithCulture(jsonSerializerSettings.Culture)
                .WithContractResolver(contractResolver)
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
                .WithTypeNameAssemblyFormat(jsonSerializerSettings.TypeNameAssemblyFormat)
                .WithTypeNameHandling(jsonSerializerSettings.TypeNameHandling);
        }

        private void ThrowNewJsonResultAssertionException(string propertyName, string expectedValue, string actualValue)
        {
            throw new JsonResultAssertionException(string.Format(
                    "When calling {0} action in {1} expected JSON result {2} {3}, but {4}.",
                    this.ActionName,
                    this.Controller.GetName(),
                    propertyName,
                    expectedValue,
                    actualValue));
        }
    }
}
