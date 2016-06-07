namespace MyTested.AspNetCore.Mvc.Builders.Contracts.ActionResults.Json
{
    using System;
    using System.Net;
    using Base;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Net.Http.Headers;
    using Newtonsoft.Json;

    /// <summary>
    /// Used for testing <see cref="JsonResult"/>.
    /// </summary>
    public interface IJsonTestBuilder : IBaseTestBuilderWithResponseModel,
        IBaseTestBuilderWithActionResult<JsonResult>
    {
        /// <summary>
        /// Tests whether <see cref="JsonResult"/> has the same status code as the provided one.
        /// </summary>
        /// <param name="statusCode">Status code as integer.</param>
        /// <returns>The same <see cref="IAndJsonTestBuilder"/>.</returns>
        IAndJsonTestBuilder WithStatusCode(int statusCode);

        /// <summary>
        /// Tests whether <see cref="JsonResult"/> has the same status code as the provided <see cref="HttpStatusCode"/>.
        /// </summary>
        /// <param name="statusCode"><see cref="HttpStatusCode"/> enumeration.</param>
        /// <returns>The same <see cref="IAndJsonTestBuilder"/>.</returns>
        IAndJsonTestBuilder WithStatusCode(HttpStatusCode statusCode);
        
        /// <summary>
        /// Tests whether <see cref="JsonResult"/> has the same content type as the provided string.
        /// </summary>
        /// <param name="contentType">ContentType type as string.</param>
        /// <returns>The same <see cref="IAndJsonTestBuilder"/>.</returns>
        IAndJsonTestBuilder WithContentType(string contentType);

        /// <summary>
        /// Tests whether <see cref="JsonResult"/> has the same content type as the provided <see cref="MediaTypeHeaderValue"/>.
        /// </summary>
        /// <param name="contentType">Content type as <see cref="MediaTypeHeaderValue"/>.</param>
        /// <returns>The same <see cref="IAndJsonTestBuilder"/>.</returns>
        IAndJsonTestBuilder WithContentType(MediaTypeHeaderValue contentType);

        /// <summary>
        /// Tests whether <see cref="JsonResult"/> has the default <see cref="JsonSerializerSettings"/>.
        /// </summary>
        /// <returns>The same <see cref="IAndJsonTestBuilder"/>.</returns>
        IAndJsonTestBuilder WithDefaulJsonSerializerSettings();

        /// <summary>
        /// Tests whether <see cref="JsonResult"/> has the provided <see cref="JsonSerializerSettings"/>.
        /// </summary>
        /// <param name="jsonSerializerSettings">Expected <see cref="JsonSerializerSettings"/> to test with.</param>
        /// <returns>The same <see cref="IAndJsonTestBuilder"/>.</returns>
        IAndJsonTestBuilder WithJsonSerializerSettings(JsonSerializerSettings jsonSerializerSettings);

        /// <summary>
        /// Tests whether <see cref="JsonResult"/> has <see cref="JsonSerializerSettings"/> by using builder.
        /// </summary>
        /// <param name="jsonSerializerSettingsBuilder">Builder for creating <see cref="JsonSerializerSettings"/>.</param>
        /// <returns>The same <see cref="IAndJsonTestBuilder"/>.</returns>
        IAndJsonTestBuilder WithJsonSerializerSettings(
            Action<IJsonSerializerSettingsTestBuilder> jsonSerializerSettingsBuilder);
    }
}
