namespace MyTested.Mvc.Builders.Contracts.Http
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using Base;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Primitives;

    /// <summary>
    /// Used for testing the HTTP response.
    /// </summary>
    public interface IHttpResponseTestBuilder : IBaseTestBuilderWithInvokedAction
    {
        /// <summary>
        /// Tests whether HTTP response message body has the same contents as the provided Stream.
        /// </summary>
        /// <param name="body">Expected stream body.</param>
        /// <returns>The same HTTP response message test builder.</returns>
        IAndHttpResponseTestBuilder WithBody(Stream body);

        /// <summary>
        /// Tests whether HTTP response message content length is the same as the provided one.
        /// </summary>
        /// <param name="contentLenght">Expected content length.</param>
        /// <returns>The same HTTP response message test builder.</returns>
        IAndHttpResponseTestBuilder WithContentLength(long? contentLenght);

        /// <summary>
        /// Tests whether HTTP response message content type is the same as the provided one.
        /// </summary>
        /// <param name="contentType">Expected content type.</param>
        /// <returns>The same HTTP response message test builder.</returns>
        IAndHttpResponseTestBuilder WithContentType(string contentType);

        /// <summary>
        /// Tests whether HTTP response message contains cookie with the same name as the provided one.
        /// </summary>
        /// <param name="name">Expected cookie name.</param>
        /// <returns>The same HTTP response message test builder.</returns>
        IAndHttpResponseTestBuilder ContainingCookie(string name);

        /// <summary>
        /// Tests whether HTTP response message contains cookie with the same name and value as the provided ones.
        /// </summary>
        /// <param name="name">Expected cookie name.</param>
        /// <param name="value">Expected cookie value.</param>
        /// <returns>The same HTTP response message test builder.</returns>
        IAndHttpResponseTestBuilder ContainingCookie(string name, string value);

        /// <summary>
        /// Tests whether HTTP response message contains cookie with the same name, value and options as the provided ones.
        /// </summary>
        /// <param name="name">Expected cookie name.</param>
        /// <param name="value">Expected cookie value.</param>
        /// <param name="options">Expected cookie options.</param>
        /// <returns>The same HTTP response message test builder.</returns>
        IAndHttpResponseTestBuilder ContainingCookie(string name, string value, CookieOptions options);

        /// <summary>
        /// Tests whether HTTP response message contains cookie by using test builder.
        /// </summary>
        /// <param name="cookieBuilder">Action of response cookie test builder.</param>
        /// <returns>The same HTTP response message test builder.</returns>
        IAndHttpResponseTestBuilder ContainingCookie(Action<IResponseCookieTestBuilder> cookieBuilder);

        /// <summary>
        /// Tests whether HTTP response message contains the provided dictionary of cookies.
        /// </summary>
        /// <param name="cookies">Dictionary of cookies.</param>
        /// <returns>The same HTTP response message test builder.</returns>
        IAndHttpResponseTestBuilder ContainingCookies(IDictionary<string, string> cookies);

        /// <summary>
        /// Tests whether HTTP response message contains header with the same name as the provided one.
        /// </summary>
        /// <param name="name">Expected header name.</param>
        /// <returns>The same HTTP response message test builder.</returns>
        IAndHttpResponseTestBuilder ContainingHeader(string name);

        /// <summary>
        /// Tests whether HTTP response message contains header with the same name and value as the provided ones.
        /// </summary>
        /// <param name="name">Expected header name.</param>
        /// <param name="value">Expected header value.</param>
        /// <returns>The same HTTP response message test builder.</returns>
        IAndHttpResponseTestBuilder ContainingHeader(string name, string value);

        /// <summary>
        /// Tests whether HTTP response message contains header with the same name and values as the provided ones.
        /// </summary>
        /// <param name="name">Expected header name.</param>
        /// <param name="values">Expected header values.</param>
        /// <returns>The same HTTP response message test builder.</returns>
        IAndHttpResponseTestBuilder ContainingHeader(string name, IEnumerable<string> values);

        /// <summary>
        /// Tests whether HTTP response message contains header with the same name and values as the provided ones.
        /// </summary>
        /// <param name="name">Expected header name.</param>
        /// <param name="values">Expected header values.</param>
        /// <returns>The same HTTP response message test builder.</returns>
        IAndHttpResponseTestBuilder ContainingHeader(string name, params string[] values);

        /// <summary>
        /// Tests whether HTTP response message contains header with the same name and values as the provided ones.
        /// </summary>
        /// <param name="name">Expected header name.</param>
        /// <param name="values">Expected header values.</param>
        /// <returns>The same HTTP response message test builder.</returns>
        IAndHttpResponseTestBuilder ContainingHeader(string name, StringValues values);

        /// <summary>
        /// Tests whether HTTP response message contains the same headers as the provided ones.
        /// </summary>
        /// <param name="headers">Dictionary of headers.</param>
        /// <returns>The same HTTP response message test builder.</returns>
        IAndHttpResponseTestBuilder ContainingHeaders(IDictionary<string, IEnumerable<string>> headers);

        /// <summary>
        /// Tests whether HTTP response message contains the same headers as the provided ones.
        /// </summary>
        /// <param name="headers">Dictionary of headers.</param>
        /// <returns>The same HTTP response message test builder.</returns>
        IAndHttpResponseTestBuilder ContainingHeaders(IDictionary<string, StringValues> headers);

        /// <summary>
        /// Tests whether HTTP response message contains the same headers as the provided ones.
        /// </summary>
        /// <param name="headers">Dictionary of headers.</param>
        /// <returns>The same HTTP response message test builder.</returns>
        IAndHttpResponseTestBuilder ContainingHeaders(IHeaderDictionary headers);

        /// <summary>
        /// Tests whether HTTP response message status code is the same as the provided one.
        /// </summary>
        /// <param name="statusCode">Expected status code.</param>
        /// <returns>The same HTTP response message test builder.</returns>
        IAndHttpResponseTestBuilder WithStatusCode(int statusCode);

        /// <summary>
        /// Tests whether HTTP response message status code is the same as the provided HttpStatusCode.
        /// </summary>
        /// <param name="statusCode">Expected status code.</param>
        /// <returns>The same HTTP response message test builder.</returns>
        IAndHttpResponseTestBuilder WithStatusCode(HttpStatusCode statusCode);
    }
}
