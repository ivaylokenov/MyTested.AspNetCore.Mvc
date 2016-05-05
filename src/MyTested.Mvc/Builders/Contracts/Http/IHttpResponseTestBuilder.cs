namespace MyTested.Mvc.Builders.Contracts.Http
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Text;
    using Base;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Primitives;

    /// <summary>
    /// Used for testing <see cref="HttpResponse"/>.
    /// </summary>
    public interface IHttpResponseTestBuilder : IBaseTestBuilderWithInvokedAction
    {
        /// <summary>
        /// Tests whether <see cref="HttpResponse.Body"/> has the same contents as the provided <see cref="Stream"/>.
        /// </summary>
        /// <param name="body">Expected stream body.</param>
        /// <returns>The same <see cref="IAndHttpResponseTestBuilder"/> test builder.</returns>
        IAndHttpResponseTestBuilder WithBody(Stream body);

        /// <summary>
        /// Tests whether <see cref="HttpResponse.Body"/> has the same type as the provided one. Body is parsed from the configured formatters and provided content type. Uses <see cref="Encoding.UTF8"/> encoding.
        /// </summary>
        /// <typeparam name="TBody">Expected type of body to test.</typeparam>
        /// <param name="contentType">Expected content type as string.</param>
        /// <returns>The same <see cref="IAndHttpResponseTestBuilder"/> test builder.</returns>
        IAndHttpResponseTestBuilder WithBodyOfType<TBody>(string contentType);
        
        /// <summary>
        /// Tests whether <see cref="HttpResponse.Body"/> has the same type as the provided one. Body is parsed from the configured formatters and provided content type.
        /// </summary>
        /// <typeparam name="TBody">Expected type of body to test.</typeparam>
        /// <param name="contentType">Expected content type as string.</param>
        /// <param name="encoding"><see cref="Encoding"/> of the body.</param>
        /// <returns>The same <see cref="IAndHttpResponseTestBuilder"/> test builder.</returns>
        IAndHttpResponseTestBuilder WithBodyOfType<TBody>(string contentType, Encoding encoding);

        /// <summary>
        /// Tests whether <see cref="HttpResponse.Body"/> has deeply equal value as the provided one. Body is parsed from the configured formatters and provided content type. Uses <see cref="Encoding.UTF8"/> encoding.
        /// </summary>
        /// <typeparam name="TBody">Expected type of body to test.</typeparam>
        /// <param name="body">Expected body object.</param>
        /// <param name="contentType">Expected content type as string.</param>
        /// <returns>The same <see cref="IAndHttpResponseTestBuilder"/> test builder.</returns>
        IAndHttpResponseTestBuilder WithBody<TBody>(TBody body, string contentType);

        /// <summary>
        /// Tests whether <see cref="HttpResponse.Body"/> has deeply equal value as the provided one. Body is parsed from the configured formatters and provided content type.
        /// </summary>
        /// <typeparam name="TBody">Expected type of body to test.</typeparam>
        /// <param name="body">Expected body object.</param>
        /// <param name="contentType">Expected content type as string.</param>
        /// <param name="encoding"><see cref="Encoding"/> of the body.</param>
        /// <returns>The same <see cref="IAndHttpResponseTestBuilder"/> test builder.</returns>
        IAndHttpResponseTestBuilder WithBody<TBody>(TBody body, string contentType, Encoding encoding);

        /// <summary>
        /// Tests whether <see cref="HttpResponse.Body"/> is equal to the provided string. Body is parsed from the configured formatters and 'text/plain' content type. Uses <see cref="Encoding.UTF8"/> encoding.
        /// </summary>
        /// <param name="body">Expected body as string.</param>
        /// <returns>The same <see cref="IAndHttpResponseTestBuilder"/> test builder.</returns>
        IAndHttpResponseTestBuilder WithStringBody(string body);

        /// <summary>
        /// Tests whether <see cref="HttpResponse.Body"/> is equal to the provided string. Body is parsed from the configured formatters and 'text/plain' content type.
        /// </summary>
        /// <param name="body">Expected body as string.</param>
        /// <param name="encoding"><see cref="Encoding"/> of the body.</param>
        /// <returns>The same <see cref="IAndHttpResponseTestBuilder"/> test builder.</returns>
        IAndHttpResponseTestBuilder WithStringBody(string body, Encoding encoding);

        /// <summary>
        /// Tests whether <see cref="HttpResponse.Body"/> is equal to the provided JSON string. Body is parsed from the configured formatters and 'application/json' content type. Uses <see cref="Encoding.UTF8"/> encoding.
        /// </summary>
        /// <param name="jsonBody">Expected body as JSON string.</param>
        /// <returns>The same <see cref="IAndHttpResponseTestBuilder"/> test builder.</returns>
        IAndHttpResponseTestBuilder WithJsonBody(string jsonBody);

        /// <summary>
        /// Tests whether <see cref="HttpResponse.Body"/> is equal to the provided JSON string. Body is parsed from the configured formatters and 'application/json' content type.
        /// </summary>
        /// <param name="jsonBody">Expected body as JSON string.</param>
        /// <param name="encoding"><see cref="Encoding"/> of the body.</param>
        /// <returns>The same <see cref="IAndHttpResponseTestBuilder"/> test builder.</returns>
        IAndHttpResponseTestBuilder WithJsonBody(string jsonBody, Encoding encoding);

        /// <summary>
        /// Tests whether <see cref="HttpResponse.Body"/> is deeply equal to the provided object. Body is parsed from the configured formatters and 'application/json' content type. Uses <see cref="Encoding.UTF8"/> encoding.
        /// </summary>
        /// <typeparam name="TBody">Expected type of body to test.</typeparam>
        /// <param name="jsonBody">Expected body as object.</param>
        /// <returns>The same <see cref="IAndHttpResponseTestBuilder"/> test builder.</returns>
        IAndHttpResponseTestBuilder WithJsonBody<TBody>(TBody jsonBody);

        /// <summary>
        /// Tests whether <see cref="HttpResponse.Body"/> is equal to the provided JSON string. Body is parsed from the configured formatters and 'application/json' content type.
        /// </summary>
        /// <typeparam name="TBody">Expected type of body to test.</typeparam>
        /// <param name="jsonBody">Expected body as object.</param>
        /// <param name="encoding"><see cref="Encoding"/> of the body.</param>
        /// <returns>The same <see cref="IAndHttpResponseTestBuilder"/> test builder.</returns>
        IAndHttpResponseTestBuilder WithJsonBody<TBody>(TBody jsonBody, Encoding encoding);

        /// <summary>
        /// Tests whether <see cref="HttpResponse.ContentLength"/> is the same as the provided one.
        /// </summary>
        /// <param name="contentLenght">Expected content length.</param>
        /// <returns>The same <see cref="IAndHttpResponseTestBuilder"/> test builder.</returns>
        IAndHttpResponseTestBuilder WithContentLength(long? contentLenght);

        /// <summary>
        /// Tests whether <see cref="HttpResponse.ContentType"/> is the same as the provided one.
        /// </summary>
        /// <param name="contentType">Expected content type.</param>
        /// <returns>The same <see cref="IAndHttpResponseTestBuilder"/> test builder.</returns>
        IAndHttpResponseTestBuilder WithContentType(string contentType);

        /// <summary>
        /// Tests whether <see cref="HttpResponse.Cookies"/> contains cookie with the same name as the provided one.
        /// </summary>
        /// <param name="name">Expected cookie name.</param>
        /// <returns>The same <see cref="IAndHttpResponseTestBuilder"/> test builder.</returns>
        IAndHttpResponseTestBuilder ContainingCookie(string name);

        /// <summary>
        /// Tests whether <see cref="HttpResponse.Cookies"/> contains cookie with the same name and value as the provided ones.
        /// </summary>
        /// <param name="name">Expected cookie name.</param>
        /// <param name="value">Expected cookie value.</param>
        /// <returns>The same <see cref="IAndHttpResponseTestBuilder"/> test builder.</returns>
        IAndHttpResponseTestBuilder ContainingCookie(string name, string value);

        /// <summary>
        /// Tests whether <see cref="HttpResponse.Cookies"/> contains cookie with the same name, value and options as the provided ones.
        /// </summary>
        /// <param name="name">Expected cookie name.</param>
        /// <param name="value">Expected cookie value.</param>
        /// <param name="options">Expected cookie options.</param>
        /// <returns>The same <see cref="IAndHttpResponseTestBuilder"/> test builder.</returns>
        IAndHttpResponseTestBuilder ContainingCookie(string name, string value, CookieOptions options);

        /// <summary>
        /// Tests whether <see cref="HttpResponse.Cookies"/> contains cookie by using test builder.
        /// </summary>
        /// <param name="cookieBuilder">Action of response cookie test builder.</param>
        /// <returns>The same <see cref="IAndHttpResponseTestBuilder"/> test builder.</returns>
        IAndHttpResponseTestBuilder ContainingCookie(Action<IResponseCookieTestBuilder> cookieBuilder);

        /// <summary>
        /// Tests whether <see cref="HttpResponse.Cookies"/> contains the provided dictionary of cookies.
        /// </summary>
        /// <param name="cookies">Dictionary of cookies.</param>
        /// <returns>The same <see cref="IAndHttpResponseTestBuilder"/> test builder.</returns>
        IAndHttpResponseTestBuilder ContainingCookies(IDictionary<string, string> cookies);

        /// <summary>
        /// Tests whether <see cref="HttpResponse.Cookies"/> contains the provided dictionary of cookies.
        /// </summary>
        /// <param name="cookies">Anonymous object of cookies.</param>
        /// <returns>The same <see cref="IAndHttpResponseTestBuilder"/> test builder.</returns>
        IAndHttpResponseTestBuilder ContainingCookies(object cookies);

        /// <summary>
        /// Tests whether <see cref="HttpResponse.Headers"/> contains header with the same name as the provided one.
        /// </summary>
        /// <param name="name">Expected header name.</param>
        /// <returns>The same <see cref="IAndHttpResponseTestBuilder"/> test builder.</returns>
        IAndHttpResponseTestBuilder ContainingHeader(string name);

        /// <summary>
        /// Tests whether <see cref="HttpResponse.Headers"/> contains header with the same name and value as the provided ones.
        /// </summary>
        /// <param name="name">Expected header name.</param>
        /// <param name="value">Expected header value.</param>
        /// <returns>The same <see cref="IAndHttpResponseTestBuilder"/> test builder.</returns>
        IAndHttpResponseTestBuilder ContainingHeader(string name, string value);

        /// <summary>
        /// Tests whether <see cref="HttpResponse.Headers"/> contains header with the same name and values as the provided ones.
        /// </summary>
        /// <param name="name">Expected header name.</param>
        /// <param name="values">Expected header values.</param>
        /// <returns>The same <see cref="IAndHttpResponseTestBuilder"/> test builder.</returns>
        IAndHttpResponseTestBuilder ContainingHeader(string name, IEnumerable<string> values);

        /// <summary>
        /// Tests whether <see cref="HttpResponse.Headers"/> contains header with the same name and values as the provided ones.
        /// </summary>
        /// <param name="name">Expected header name.</param>
        /// <param name="values">Expected header values.</param>
        /// <returns>The same <see cref="IAndHttpResponseTestBuilder"/> test builder.</returns>
        IAndHttpResponseTestBuilder ContainingHeader(string name, params string[] values);

        /// <summary>
        /// Tests whether <see cref="HttpResponse.Headers"/> contains header with the same name and values as the provided ones.
        /// </summary>
        /// <param name="name">Expected header name.</param>
        /// <param name="values">Expected header values.</param>
        /// <returns>The same <see cref="IAndHttpResponseTestBuilder"/> test builder.</returns>
        IAndHttpResponseTestBuilder ContainingHeader(string name, StringValues values);

        /// <summary>
        /// Tests whether <see cref="HttpResponse.Headers"/> contains the same headers as the provided ones.
        /// </summary>
        /// <param name="headers">Dictionary of headers.</param>
        /// <returns>The same <see cref="IAndHttpResponseTestBuilder"/> test builder.</returns>
        IAndHttpResponseTestBuilder ContainingHeaders(IDictionary<string, string> headers);

        /// <summary>
        /// Tests whether <see cref="HttpResponse.Headers"/> contains the same headers as the provided ones.
        /// </summary>
        /// <param name="headers">Dictionary of headers.</param>
        /// <returns>The same <see cref="IAndHttpResponseTestBuilder"/> test builder.</returns>
        IAndHttpResponseTestBuilder ContainingHeaders(IDictionary<string, IEnumerable<string>> headers);

        /// <summary>
        /// Tests whether <see cref="HttpResponse.Headers"/> contains the same headers as the provided ones.
        /// </summary>
        /// <param name="headers">Dictionary of headers.</param>
        /// <returns>The same <see cref="IAndHttpResponseTestBuilder"/> test builder.</returns>
        IAndHttpResponseTestBuilder ContainingHeaders(IDictionary<string, StringValues> headers);

        /// <summary>
        /// Tests whether <see cref="HttpResponse.Headers"/> contains the same headers as the provided ones.
        /// </summary>
        /// <param name="headers">Dictionary of headers.</param>
        /// <returns>The same <see cref="IAndHttpResponseTestBuilder"/> test builder.</returns>
        IAndHttpResponseTestBuilder ContainingHeaders(IHeaderDictionary headers);

        /// <summary>
        /// Tests whether <see cref="HttpResponse.Headers"/> contains the same headers as the provided ones.
        /// </summary>
        /// <param name="headers">Anonymous object of headers.</param>
        /// <returns>The same <see cref="IAndHttpResponseTestBuilder"/> test builder.</returns>
        IAndHttpResponseTestBuilder ContainingHeaders(object headers);
        
        /// <summary>
        /// Tests whether <see cref="HttpResponse.StatusCode"/> status code is the same as the provided one.
        /// </summary>
        /// <param name="statusCode">Expected status code as integer.</param>
        /// <returns>The same <see cref="IAndHttpResponseTestBuilder"/> test builder.</returns>
        IAndHttpResponseTestBuilder WithStatusCode(int statusCode);

        /// <summary>
        /// Tests whether <see cref="HttpResponse.StatusCode"/> status code is the same as the provided <see cref="HttpStatusCode"/>.
        /// </summary>
        /// <param name="statusCode">Expected status code as <see cref="HttpStatusCode"/>.</param>
        /// <returns>The same <see cref="IAndHttpResponseTestBuilder"/> test builder.</returns>
        IAndHttpResponseTestBuilder WithStatusCode(HttpStatusCode statusCode);
    }
}
