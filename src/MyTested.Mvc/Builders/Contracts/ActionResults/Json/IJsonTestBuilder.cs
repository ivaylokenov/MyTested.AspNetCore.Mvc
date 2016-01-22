namespace MyTested.Mvc.Builders.Contracts.ActionResults.Json
{
    using System;
    using System.Net;
    using Base;
    using Microsoft.Net.Http.Headers;
    using Newtonsoft.Json;

    /// <summary>
    /// Used for testing JSON results.
    /// </summary>
    public interface IJsonTestBuilder : IBaseTestBuilderWithResponseModel
    {
        /// <summary>
        /// Tests whether JSON result has the same status code as the provided one.
        /// </summary>
        /// <param name="statusCode">Status code.</param>
        /// <returns>The same JSON test builder.</returns>
        IAndJsonTestBuilder WithStatusCode(int statusCode);

        /// <summary>
        /// Tests whether JSON result has the same status code as the provided HttpStatusCode.
        /// </summary>
        /// <param name="statusCode">HttpStatusCode enumeration.</param>
        /// <returns>The same JSON test builder.</returns>
        IAndJsonTestBuilder WithStatusCode(HttpStatusCode statusCode);
        
        /// <summary>
        /// Tests whether JSON result has the same content type as the provided string.
        /// </summary>
        /// <param name="contentType">ContentType type as string.</param>
        /// <returns>The same JSON test builder.</returns>
        IAndJsonTestBuilder WithContentType(string contentType);
        
        /// <summary>
        /// Tests whether JSON result has the same content type as the provided MediaTypeHeaderValue.
        /// </summary>
        /// <param name="contentType">Content type as MediaTypeHeaderValue.</param>
        /// <returns>The same JSON test builder.</returns>
        IAndJsonTestBuilder WithContentType(MediaTypeHeaderValue contentType);

        /// <summary>
        /// Tests whether JSON result has the default JSON serializer settings.
        /// </summary>
        /// <returns>The same JSON test builder.</returns>
        IAndJsonTestBuilder WithDefaulJsonSerializerSettings();

        /// <summary>
        /// Tests whether JSON result has the provided JSON serializer settings.
        /// </summary>
        /// <param name="jsonSerializerSettings">Expected JSON serializer settings to test with.</param>
        /// <returns>The same JSON test builder.</returns>
        IAndJsonTestBuilder WithJsonSerializerSettings(JsonSerializerSettings jsonSerializerSettings);

        /// <summary>
        /// Tests whether JSON result has JSON serializer settings by using builder.
        /// </summary>
        /// <param name="jsonSerializerSettingsBuilder">Builder for creating JSON serializer settings.</param>
        /// <returns>The same JSON test builder.</returns>
        IAndJsonTestBuilder WithJsonSerializerSettings(
            Action<IJsonSerializerSettingsTestBuilder> jsonSerializerSettingsBuilder);
    }
}
