namespace MyTested.Mvc.Builders.Contracts.Http
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using Microsoft.AspNet.Http;
    using Microsoft.Extensions.Primitives;
    using Uris;

    /// <summary>
    /// Used for building HTTP request message.
    /// </summary>
    public interface IHttpRequestBuilder
    {
        /// <summary>
        /// Adds body to the built HTTP request.
        /// </summary>
        /// <param name="stream">Body as stream.</param>
        /// <returns>The same HTTP request builder.</returns>
        IAndHttpRequestBuilder WithBody(Stream stream);

        /// <summary>
        /// Adds content length to the built HTTP request.
        /// </summary>
        /// <param name="contentLength">Content length as nullable long.</param>
        /// <returns>The same HTTP request builder.</returns>
        IAndHttpRequestBuilder WithContentLength(long? contentLength);

        /// <summary>
        /// Adds content type to the built HTTP request.
        /// </summary>
        /// <param name="contentType">Content type as string.</param>
        /// <returns>The same HTTP request builder.</returns>
        IAndHttpRequestBuilder WithContentType(string contentType);

        /// <summary>
        /// Adds cookie to the built HTTP request.
        /// </summary>
        /// <param name="name">Cookie name.</param>
        /// <param name="value">Cookie value.</param>
        /// <returns>The same HTTP request builder.</returns>
        IAndHttpRequestBuilder WithCookie(string name, string value);

        /// <summary>
        /// Adds cookies to the built HTTP request.
        /// </summary>
        /// <param name="cookies">Dictionary of name-value cookies.</param>
        /// <returns>The same HTTP request builder.</returns>
        IAndHttpRequestBuilder WithCookies(IDictionary<string, string> cookies);

        /// <summary>
        /// Adds cookies to the built HTTP request.
        /// </summary>
        /// <param name="cookies">Request cookie collection.</param>
        /// <returns>The same HTTP request builder.</returns>
        IAndHttpRequestBuilder WithCookies(IRequestCookieCollection cookies);

        /// <summary>
        /// Adds form field to the built HTTP request.
        /// </summary>
        /// <param name="name">Name of the form field.</param>
        /// <param name="value">Value of the form field.</param>
        /// <returns>The same HTTP request builder.</returns>
        IAndHttpRequestBuilder WithFormField(string name, string value);

        /// <summary>
        /// Adds form field to the built HTTP request.
        /// </summary>
        /// <param name="name">Name of the form field.</param>
        /// <param name="values">Collection of values for the form field.</param>
        /// <returns>The same HTTP request builder.</returns>
        IAndHttpRequestBuilder WithFormField(string name, IEnumerable<string> values);
        
        /// <summary>
        /// Adds form field to the built HTTP request.
        /// </summary>
        /// <param name="name">Name of the form field.</param>
        /// <param name="values">Collection of values for the form field.</param>
        /// <returns>The same HTTP request builder.</returns>
        IAndHttpRequestBuilder WithFormField(string name, params string[] values);

        /// <summary>
        /// Adds form field to the built HTTP request.
        /// </summary>
        /// <param name="name">Name of the form field.</param>
        /// <param name="values">Collection of string values for the form field.</param>
        /// <returns>The same HTTP request builder.</returns>
        IAndHttpRequestBuilder WithFormField(string name, StringValues values);

        /// <summary>
        /// Adds form fields to the built HTTP request.
        /// </summary>
        /// <param name="formValues">Dictionary of form fields to add.</param>
        /// <returns>The same HTTP request builder.</returns>
        IAndHttpRequestBuilder WithFormFields(IDictionary<string, IEnumerable<string>> formValues);

        /// <summary>
        /// Adds form fields to the built HTTP request.
        /// </summary>
        /// <param name="formValues">Dictionary of form fields to add.</param>
        /// <returns>The same HTTP request builder.</returns>
        IAndHttpRequestBuilder WithFormFields(IDictionary<string, StringValues> formValues);

        /// <summary>
        /// Adds form file to the built HTTP request.
        /// </summary>
        /// <param name="formFile">Form file to add.</param>
        /// <returns>The same HTTP request builder.</returns>
        IAndHttpRequestBuilder WithFormFile(IFormFile formFile);

        /// <summary>
        /// Adds form files to the built HTTP request.
        /// </summary>
        /// <param name="formFiles">Collection of form files.</param>
        /// <returns>The same HTTP request builder.</returns>
        IAndHttpRequestBuilder WithFormFiles(IEnumerable<IFormFile> formFiles);

        /// <summary>
        /// Adds form files to the built HTTP request.
        /// </summary>
        /// <param name="formFiles">Form file parameters.</param>
        /// <returns>The same HTTP request builder.</returns>
        IAndHttpRequestBuilder WithFormFiles(params IFormFile[] formFiles);

        /// <summary>
        /// Adds form values and files to the built HTTP request.
        /// </summary>
        /// <param name="formCollection">Form collection to add.</param>
        /// <returns>The same HTTP request builder.</returns>
        IAndHttpRequestBuilder WithForm(IFormCollection formCollection);

        /// <summary>
        /// Adds header to the built HTTP request.
        /// </summary>
        /// <param name="name">Name of the header.</param>
        /// <param name="value">Value of the header.</param>
        /// <returns>The same HTTP request builder.</returns>
        IAndHttpRequestBuilder WithHeader(string name, string value);

        /// <summary>
        /// Adds header to the built HTTP request.
        /// </summary>
        /// <param name="name">Name of the header.</param>
        /// <param name="values">Collection of values for the header.</param>
        /// <returns>The same HTTP request builder.</returns>
        IAndHttpRequestBuilder WithHeader(string name, IEnumerable<string> values);

        /// <summary>
        /// Adds header to the built HTTP request.
        /// </summary>
        /// <param name="name">Name of the header.</param>
        /// <param name="values">Collection of values for the header.</param>
        /// <returns>The same HTTP request builder.</returns>
        IAndHttpRequestBuilder WithHeader(string name, params string[] values);

        /// <summary>
        /// Adds header to the built HTTP request.
        /// </summary>
        /// <param name="name">Name of the header.</param>
        /// <param name="values">Collection of string values for the header.</param>
        /// <returns>The same HTTP request builder.</returns>
        IAndHttpRequestBuilder WithHeader(string name, StringValues values);

        /// <summary>
        /// Adds collection of headers to the built HTTP request.
        /// </summary>
        /// <param name="headers">Dictionary of headers to add.</param>
        /// <returns>The same HTTP request builder.</returns>
        IAndHttpRequestBuilder WithHeaders(IDictionary<string, IEnumerable<string>> headers);

        /// <summary>
        /// Adds collection of headers to the built HTTP request.
        /// </summary>
        /// <param name="headers">Dictionary of headers to add.</param>
        /// <returns>The same HTTP request builder.</returns>
        IAndHttpRequestBuilder WithHeaders(IDictionary<string, StringValues> headers);

        /// <summary>
        /// Adds collection of headers to the built HTTP request.
        /// </summary>
        /// <param name="headers">Dictionary of headers to add.</param>
        /// <returns>The same HTTP request builder.</returns>
        IAndHttpRequestBuilder WithHeaders(IHeaderDictionary headers);

        /// <summary>
        /// Adds host to the built HTTP request.
        /// </summary>
        /// <param name="host">Host as string.</param>
        /// <returns>The same HTTP request builder.</returns>
        IAndHttpRequestBuilder WithHost(string host);

        /// <summary>
        /// Adds host to the built HTTP request.
        /// </summary>
        /// <param name="host">Host as HostString.</param>
        /// <returns>The same HTTP request builder.</returns>
        IAndHttpRequestBuilder WithHost(HostString host);

        /// <summary>
        /// Adds method to the built HTTP request.
        /// </summary>
        /// <param name="method">HTTP method represented by string.</param>
        /// <returns>The same HTTP request builder.</returns>
        IAndHttpRequestBuilder WithMethod(string method);

        /// <summary>
        /// Adds path to the built HTTP request.
        /// </summary>
        /// <param name="path">Path as string.</param>
        /// <returns>The same HTTP request builder.</returns>
        IAndHttpRequestBuilder WithPath(string path);

        /// <summary>
        /// Adds path to the built HTTP request.
        /// </summary>
        /// <param name="path">Path as PathString.</param>
        /// <returns>The same HTTP request builder.</returns>
        IAndHttpRequestBuilder WithPath(PathString path);

        /// <summary>
        /// Adds path base to the built HTTP request.
        /// </summary>
        /// <param name="pathBase">Path base as string.</param>
        /// <returns>The same HTTP request builder.</returns>
        IAndHttpRequestBuilder WithPathBase(string pathBase);

        /// <summary>
        /// Adds path base to the built HTTP request.
        /// </summary>
        /// <param name="pathBase">Path base as PathString.</param>
        /// <returns>The same HTTP request builder.</returns>
        IAndHttpRequestBuilder WithPathBase(PathString pathBase);

        /// <summary>
        /// Adds protocol to the built HTTP request.
        /// </summary>
        /// <param name="protocol">Protocol as string.</param>
        /// <returns>The same HTTP request builder.</returns>
        IAndHttpRequestBuilder WithProtocol(string protocol);

        /// <summary>
        /// Adds query value to the built HTTP request.
        /// </summary>
        /// <param name="name">Name of the query.</param>
        /// <param name="value">Value of the query.</param>
        /// <returns>The same HTTP request builder.</returns>
        IAndHttpRequestBuilder WithQuery(string name, string value);

        /// <summary>
        /// Adds query value to the built HTTP request.
        /// </summary>
        /// <param name="name">Name of the query.</param>
        /// <param name="values">Collection of values for the query.</param>
        /// <returns>The same HTTP request builder.</returns>
        IAndHttpRequestBuilder WithQuery(string name, IEnumerable<string> values);

        /// <summary>
        /// Adds query value to the built HTTP request.
        /// </summary>
        /// <param name="name">Name of the query.</param>
        /// <param name="values">Collection of values for the query.</param>
        /// <returns>The same HTTP request builder.</returns>
        IAndHttpRequestBuilder WithQuery(string name, params string[] values);

        /// <summary>
        /// Adds query value to the built HTTP request.
        /// </summary>
        /// <param name="name">Name of the query.</param>
        /// <param name="values">Collection of string values for the query.</param>
        /// <returns>The same HTTP request builder.</returns>
        IAndHttpRequestBuilder WithQuery(string name, StringValues values);

        /// <summary>
        /// Adds collection of query values to the built HTTP request.
        /// </summary>
        /// <param name="query">Dictionary of query values to add.</param>
        /// <returns>The same HTTP request builder.</returns>
        IAndHttpRequestBuilder WithQuery(IDictionary<string, IEnumerable<string>> query);

        /// <summary>
        /// Adds collection of query values to the built HTTP request.
        /// </summary>
        /// <param name="query">Dictionary of query values to add.</param>
        /// <returns>The same HTTP request builder.</returns>
        IAndHttpRequestBuilder WithQuery(IDictionary<string, StringValues> query);

        /// <summary>
        /// Adds collection of query values to the built HTTP request.
        /// </summary>
        /// <param name="headers">Dictionary of query values to add.</param>
        /// <returns>The same HTTP request builder.</returns>
        IAndHttpRequestBuilder WithQuery(IQueryCollection query);

        /// <summary>
        /// Adds query string to the built HTTP request.
        /// </summary>
        /// <param name="pathBase">Path base as string.</param>
        /// <returns>The same HTTP request builder.</returns>
        IAndHttpRequestBuilder WithQueryString(string queryString);

        /// <summary>
        /// Adds query string to the built HTTP request.
        /// </summary>
        /// <param name="pathBase">Query string as QueryString.</param>
        /// <returns>The same HTTP request builder.</returns>
        IAndHttpRequestBuilder WithQueryString(QueryString queryString);

        /// <summary>
        /// Adds scheme to the built HTTP request.
        /// </summary>
        /// <param name="scheme">Scheme to add.</param>
        /// <returns>The same HTTP request builder.</returns>
        IAndHttpRequestBuilder WithScheme(string scheme);

        /// <summary>
        /// Adds HTTPS scheme to the built HTTP request.
        /// </summary>
        /// <returns>The same HTTP request builder.</returns>
        IAndHttpRequestBuilder WithHttps();

        /// <summary>
        /// Adds all URI components to the built HTTP request by extracting them from the provided location.
        /// </summary>
        /// <param name="location">Location as string.</param>
        /// <returns>The same HTTP request builder.</returns>
        IAndHttpRequestBuilder WithLocation(string location);

        /// <summary>
        /// Adds all URI components to the built HTTP request by extracting them from the provided location.
        /// </summary>
        /// <param name="location">Location as URI.</param>
        /// <returns>The same HTTP request builder.</returns>
        IAndHttpRequestBuilder WithLocation(Uri location);

        /// <summary>
        /// Adds all URI components to the built HTTP request by extracting them from the provided location.
        /// </summary>
        /// <param name="uriBuilder">Builder for the URI.</param>
        /// <returns>The same HTTP request builder.</returns>
        IAndHttpRequestBuilder WithLocation(Action<IUriTestBuilder> uriBuilder);
    }
}
