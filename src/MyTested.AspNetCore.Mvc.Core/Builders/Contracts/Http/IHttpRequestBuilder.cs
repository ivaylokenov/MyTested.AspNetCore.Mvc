namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Http
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Primitives;
    using Uris;
    using Authentication;

    /// <summary>
    /// Used for building <see cref="HttpRequest"/>.
    /// </summary>
    public interface IHttpRequestBuilder
    {
        /// <summary>
        /// Adds body to the <see cref="HttpRequest.Body"/>.
        /// </summary>
        /// <param name="body">Body as <see cref="Stream"/>.</param>
        /// <returns>The same <see cref="IAndHttpRequestBuilder"/>.</returns>
        IAndHttpRequestBuilder WithBody(Stream body);

        /// <summary>
        /// Adds body to the <see cref="HttpRequest.Body"/> by trying to format the provided object based
        /// on the content type and the configured formatters. Uses <see cref="Encoding.UTF8"/> encoding.
        /// </summary>
        /// <typeparam name="TBody">Type of body.</typeparam>
        /// <param name="body">Body as object.</param>
        /// <param name="contentType">Content type to use for formatting.</param>
        /// <returns>The same <see cref="IAndHttpRequestBuilder"/>.</returns>
        IAndHttpRequestBuilder WithBody<TBody>(TBody body, string contentType);

        /// <summary>
        /// Adds body to the <see cref="HttpRequest.Body"/> by trying to format the provided object based
        /// on the content type and the configured formatters.
        /// </summary>
        /// <typeparam name="TBody">Type of body.</typeparam>
        /// <param name="body">Body as object.</param>
        /// <param name="contentType">Content type to use for formatting.</param>
        /// <param name="encoding"><see cref="Encoding"/> to use for the body.</param>
        /// <returns>The same <see cref="IAndHttpRequestBuilder"/>.</returns>
        IAndHttpRequestBuilder WithBody<TBody>(TBody body, string contentType, Encoding encoding);

        /// <summary>
        /// Adds string body to the <see cref="HttpRequest.Body"/>. If no content type is set on the request,
        /// 'text/plain' will be used. Uses <see cref="Encoding.UTF8"/> encoding.
        /// </summary>
        /// <param name="body">Body as string.</param>
        /// <returns>The same <see cref="IAndHttpRequestBuilder"/>.</returns>
        IAndHttpRequestBuilder WithStringBody(string body);

        /// <summary>
        /// Adds string body to the <see cref="HttpRequest.Body"/>. If no content type is
        /// set on the request, 'text/plain' will be used.
        /// </summary>
        /// <param name="body">Body as string.</param>
        /// <param name="encoding"><see cref="Encoding"/> to use for the body.</param>
        /// <returns>The same <see cref="IAndHttpRequestBuilder"/>.</returns>
        IAndHttpRequestBuilder WithStringBody(string body, Encoding encoding);

        /// <summary>
        /// Adds JSON body to the <see cref="HttpRequest.Body"/>. Sets 'application/json' to the content type. Uses <see cref="Encoding.UTF8"/> encoding.
        /// </summary>
        /// <param name="jsonBody">JSON body as string.</param>
        /// <returns>The same <see cref="IAndHttpRequestBuilder"/>.</returns>
        IAndHttpRequestBuilder WithJsonBody(string jsonBody);

        /// <summary>
        /// Adds JSON body to the <see cref="HttpRequest.Body"/>. If no content type is set on the request, 'application/json' will be used.
        /// </summary>
        /// <param name="jsonBody">JSON body as string.</param>
        /// <param name="encoding"><see cref="Encoding"/> to use for the body.</param>
        /// <returns>The same <see cref="IAndHttpRequestBuilder"/>.</returns>
        IAndHttpRequestBuilder WithJsonBody(string jsonBody, Encoding encoding);

        /// <summary>
        /// Adds JSON body to the <see cref="HttpRequest.Body"/>. If no content type is set on the request, 'application/json' will be used. Uses <see cref="Encoding.UTF8"/> encoding.
        /// </summary>
        /// <param name="jsonBody">Object to serialize using the built-in JSON formatters in ASP.NET Core MVC.</param>
        /// <returns>The same <see cref="IAndHttpRequestBuilder"/>.</returns>
        IAndHttpRequestBuilder WithJsonBody(object jsonBody);

        /// <summary>
        /// Adds JSON body to the <see cref="HttpRequest.Body"/>. If no content type is set on the request, 'application/json' will be used.
        /// </summary>
        /// <param name="jsonBody">Object to serialize using the built-in JSON formatters in ASP.NET Core MVC.</param>
        /// <param name="encoding"><see cref="Encoding"/> to use for the body.</param>
        /// <returns>The same <see cref="IAndHttpRequestBuilder"/>.</returns>
        IAndHttpRequestBuilder WithJsonBody(object jsonBody, Encoding encoding);

        /// <summary>
        /// Adds content length to the <see cref="HttpRequest.ContentLength"/>.
        /// </summary>
        /// <param name="contentLength">Content length as nullable long.</param>
        /// <returns>The same <see cref="IAndHttpRequestBuilder"/>.</returns>
        IAndHttpRequestBuilder WithContentLength(long? contentLength);

        /// <summary>
        /// Adds content type to the <see cref="HttpRequest.ContentType"/>.
        /// </summary>
        /// <param name="contentType">Content type as string.</param>
        /// <returns>The same <see cref="IAndHttpRequestBuilder"/>.</returns>
        IAndHttpRequestBuilder WithContentType(string contentType);

        /// <summary>
        /// Adds cookie to the <see cref="HttpRequest.Cookies"/>.
        /// </summary>
        /// <param name="name">Cookie name.</param>
        /// <param name="value">Cookie value.</param>
        /// <returns>The same <see cref="IAndHttpRequestBuilder"/>.</returns>
        IAndHttpRequestBuilder WithCookie(string name, string value);

        /// <summary>
        /// Adds cookies to the <see cref="HttpRequest.Cookies"/>.
        /// </summary>
        /// <param name="cookies">Anonymous object of name-value cookies.</param>
        /// <returns>The same <see cref="IAndHttpRequestBuilder"/>.</returns>
        IAndHttpRequestBuilder WithCookies(object cookies);

        /// <summary>
        /// Adds cookies to the <see cref="HttpRequest.Cookies"/>.
        /// </summary>
        /// <param name="cookies">Dictionary of name-value cookies.</param>
        /// <returns>The same <see cref="IAndHttpRequestBuilder"/>.</returns>
        IAndHttpRequestBuilder WithCookies(IDictionary<string, string> cookies);

        /// <summary>
        /// Adds cookies to the <see cref="HttpRequest.Cookies"/>.
        /// </summary>
        /// <param name="cookies">Request cookie collection.</param>
        /// <returns>The same <see cref="IAndHttpRequestBuilder"/>.</returns>
        IAndHttpRequestBuilder WithCookies(IRequestCookieCollection cookies);

        /// <summary>
        /// Adds form field to the <see cref="HttpRequest.Form"/>.
        /// </summary>
        /// <param name="name">Name of the form field.</param>
        /// <param name="value">Value of the form field.</param>
        /// <returns>The same <see cref="IAndHttpRequestBuilder"/>.</returns>
        IAndHttpRequestBuilder WithFormField(string name, string value);

        /// <summary>
        /// Adds form field to the <see cref="HttpRequest.Form"/>.
        /// </summary>
        /// <param name="name">Name of the form field.</param>
        /// <param name="values">Collection of values for the form field.</param>
        /// <returns>The same <see cref="IAndHttpRequestBuilder"/>.</returns>
        IAndHttpRequestBuilder WithFormField(string name, IEnumerable<string> values);

        /// <summary>
        /// Adds form field to the <see cref="HttpRequest.Form"/>.
        /// </summary>
        /// <param name="name">Name of the form field.</param>
        /// <param name="values">Collection of values for the form field.</param>
        /// <returns>The same <see cref="IAndHttpRequestBuilder"/>.</returns>
        IAndHttpRequestBuilder WithFormField(string name, params string[] values);

        /// <summary>
        /// Adds form field to the <see cref="HttpRequest.Form"/>.
        /// </summary>
        /// <param name="name">Name of the form field.</param>
        /// <param name="values">Collection of string values for the form field.</param>
        /// <returns>The same <see cref="IAndHttpRequestBuilder"/>.</returns>
        IAndHttpRequestBuilder WithFormField(string name, StringValues values);

        /// <summary>
        /// Adds form fields to the <see cref="HttpRequest.Form"/>.
        /// </summary>
        /// <param name="formValues">Anonymous object of form fields to add.</param>
        /// <returns>The same <see cref="IAndHttpRequestBuilder"/>.</returns>
        IAndHttpRequestBuilder WithFormFields(object formValues);

        /// <summary>
        /// Adds form fields to the <see cref="HttpRequest.Form"/>.
        /// </summary>
        /// <param name="formValues">Dictionary of form fields to add.</param>
        /// <returns>The same <see cref="IAndHttpRequestBuilder"/>.</returns>
        IAndHttpRequestBuilder WithFormFields(IDictionary<string, string> formValues);

        /// <summary>
        /// Adds form fields to the <see cref="HttpRequest.Form"/>.
        /// </summary>
        /// <param name="formValues">Dictionary of form fields to add.</param>
        /// <returns>The same <see cref="IAndHttpRequestBuilder"/>.</returns>
        IAndHttpRequestBuilder WithFormFields(IDictionary<string, IEnumerable<string>> formValues);

        /// <summary>
        /// Adds form fields to the <see cref="HttpRequest.Form"/>.
        /// </summary>
        /// <param name="formValues">Dictionary of form fields to add.</param>
        /// <returns>The same <see cref="IAndHttpRequestBuilder"/>.</returns>
        IAndHttpRequestBuilder WithFormFields(IDictionary<string, StringValues> formValues);

        /// <summary>
        /// Adds form file to the <see cref="HttpRequest.Form"/>.
        /// </summary>
        /// <param name="formFile">Form file to add.</param>
        /// <returns>The same <see cref="IAndHttpRequestBuilder"/>.</returns>
        IAndHttpRequestBuilder WithFormFile(IFormFile formFile);

        /// <summary>
        /// Adds form files to the <see cref="HttpRequest.Form"/>.
        /// </summary>
        /// <param name="formFiles">Collection of form files.</param>
        /// <returns>The same <see cref="IAndHttpRequestBuilder"/>.</returns>
        IAndHttpRequestBuilder WithFormFiles(IEnumerable<IFormFile> formFiles);

        /// <summary>
        /// Adds form files to the <see cref="HttpRequest.Form"/>.
        /// </summary>
        /// <param name="formFiles">Form file parameters.</param>
        /// <returns>The same <see cref="IAndHttpRequestBuilder"/>.</returns>
        IAndHttpRequestBuilder WithFormFiles(params IFormFile[] formFiles);

        /// <summary>
        /// Adds form values and files to the <see cref="HttpRequest.Form"/>.
        /// </summary>
        /// <param name="formCollection">Form collection to add.</param>
        /// <returns>The same <see cref="IAndHttpRequestBuilder"/>.</returns>
        IAndHttpRequestBuilder WithForm(IFormCollection formCollection);

        /// <summary>
        /// Adds header to the <see cref="HttpRequest.Headers"/>.
        /// </summary>
        /// <param name="name">Name of the header.</param>
        /// <param name="value">Value of the header.</param>
        /// <returns>The same <see cref="IAndHttpRequestBuilder"/>.</returns>
        IAndHttpRequestBuilder WithHeader(string name, string value);

        /// <summary>
        /// Adds header to the <see cref="HttpRequest.Headers"/>.
        /// </summary>
        /// <param name="name">Name of the header.</param>
        /// <param name="values">Collection of values for the header.</param>
        /// <returns>The same <see cref="IAndHttpRequestBuilder"/>.</returns>
        IAndHttpRequestBuilder WithHeader(string name, IEnumerable<string> values);

        /// <summary>
        /// Adds header to the <see cref="HttpRequest.Headers"/>.
        /// </summary>
        /// <param name="name">Name of the header.</param>
        /// <param name="values">Collection of values for the header.</param>
        /// <returns>The same <see cref="IAndHttpRequestBuilder"/>.</returns>
        IAndHttpRequestBuilder WithHeader(string name, params string[] values);

        /// <summary>
        /// Adds header to the <see cref="HttpRequest.Headers"/>.
        /// </summary>
        /// <param name="name">Name of the header.</param>
        /// <param name="values">Collection of string values for the header.</param>
        /// <returns>The same <see cref="IAndHttpRequestBuilder"/>.</returns>
        IAndHttpRequestBuilder WithHeader(string name, StringValues values);

        /// <summary>
        /// Adds collection of headers to the <see cref="HttpRequest.Headers"/>.
        /// </summary>
        /// <param name="headers">Anonymous object of headers to add.</param>
        /// <returns>The same <see cref="IAndHttpRequestBuilder"/>.</returns>
        IAndHttpRequestBuilder WithHeaders(object headers);

        /// <summary>
        /// Adds collection of headers to the <see cref="HttpRequest.Headers"/>.
        /// </summary>
        /// <param name="headers">Dictionary of headers to add.</param>
        /// <returns>The same <see cref="IAndHttpRequestBuilder"/>.</returns>
        IAndHttpRequestBuilder WithHeaders(IDictionary<string, string> headers);

        /// <summary>
        /// Adds collection of headers to the <see cref="HttpRequest.Headers"/>.
        /// </summary>
        /// <param name="headers">Dictionary of headers to add.</param>
        /// <returns>The same <see cref="IAndHttpRequestBuilder"/>.</returns>
        IAndHttpRequestBuilder WithHeaders(IDictionary<string, IEnumerable<string>> headers);

        /// <summary>
        /// Adds collection of headers to the <see cref="HttpRequest.Headers"/>.
        /// </summary>
        /// <param name="headers">Dictionary of headers to add.</param>
        /// <returns>The same <see cref="IAndHttpRequestBuilder"/>.</returns>
        IAndHttpRequestBuilder WithHeaders(IDictionary<string, StringValues> headers);

        /// <summary>
        /// Adds collection of headers to the <see cref="HttpRequest.Headers"/>.
        /// </summary>
        /// <param name="headers">Dictionary of headers to add.</param>
        /// <returns>The same <see cref="IAndHttpRequestBuilder"/>.</returns>
        IAndHttpRequestBuilder WithHeaders(IHeaderDictionary headers);

        /// <summary>
        /// Adds host to the <see cref="HttpRequest.Host"/>.
        /// </summary>
        /// <param name="host">Host as string.</param>
        /// <returns>The same <see cref="IAndHttpRequestBuilder"/>.</returns>
        IAndHttpRequestBuilder WithHost(string host);

        /// <summary>
        /// Adds host to the <see cref="HttpRequest.Host"/>.
        /// </summary>
        /// <param name="host">Host as <see cref="HostString"/>.</param>
        /// <returns>The same <see cref="IAndHttpRequestBuilder"/>.</returns>
        IAndHttpRequestBuilder WithHost(HostString host);

        /// <summary>
        /// Adds method to the <see cref="HttpRequest.Method"/>.
        /// </summary>
        /// <param name="method">HTTP method represented by string.</param>
        /// <returns>The same <see cref="IAndHttpRequestBuilder"/>.</returns>
        IAndHttpRequestBuilder WithMethod(string method);

        /// <summary>
        /// Adds path to the <see cref="HttpRequest.Path"/>.
        /// </summary>
        /// <param name="path">Path as string.</param>
        /// <returns>The same <see cref="IAndHttpRequestBuilder"/>.</returns>
        IAndHttpRequestBuilder WithPath(string path);

        /// <summary>
        /// Adds path to the <see cref="HttpRequest.Path"/>.
        /// </summary>
        /// <param name="path">Path as <see cref="PathString"/>.</param>
        /// <returns>The same <see cref="IAndHttpRequestBuilder"/>.</returns>
        IAndHttpRequestBuilder WithPath(PathString path);

        /// <summary>
        /// Adds path base to the <see cref="HttpRequest.PathBase"/>.
        /// </summary>
        /// <param name="pathBase">Path base as string.</param>
        /// <returns>The same <see cref="IAndHttpRequestBuilder"/>.</returns>
        IAndHttpRequestBuilder WithPathBase(string pathBase);

        /// <summary>
        /// Adds path base to the <see cref="HttpRequest.PathBase"/>.
        /// </summary>
        /// <param name="pathBase">Path base as <see cref="PathString"/>.</param>
        /// <returns>The same <see cref="IAndHttpRequestBuilder"/>.</returns>
        IAndHttpRequestBuilder WithPathBase(PathString pathBase);

        /// <summary>
        /// Adds protocol to the <see cref="HttpRequest.Protocol"/>.
        /// </summary>
        /// <param name="protocol">Protocol as string.</param>
        /// <returns>The same <see cref="IAndHttpRequestBuilder"/>.</returns>
        IAndHttpRequestBuilder WithProtocol(string protocol);

        /// <summary>
        /// Adds query value to the <see cref="HttpRequest.Query"/>.
        /// </summary>
        /// <param name="name">Name of the query.</param>
        /// <param name="value">Value of the query.</param>
        /// <returns>The same <see cref="IAndHttpRequestBuilder"/>.</returns>
        IAndHttpRequestBuilder WithQuery(string name, string value);

        /// <summary>
        /// Adds collection of query values to the <see cref="HttpRequest.Query"/>.
        /// </summary>
        /// <param name="query">Anonymous object of query values to add.</param>
        /// <returns>The same <see cref="IAndHttpRequestBuilder"/>.</returns>
        IAndHttpRequestBuilder WithQuery(object query);

        /// <summary>
        /// Adds collection of query values to the <see cref="HttpRequest.Query"/>.
        /// </summary>
        /// <param name="query">Dictionary of query values to add.</param>
        /// <returns>The same <see cref="IAndHttpRequestBuilder"/>.</returns>
        IAndHttpRequestBuilder WithQuery(IDictionary<string, string> query);

        /// <summary>
        /// Adds query value to the <see cref="HttpRequest.Query"/>.
        /// </summary>
        /// <param name="name">Name of the query.</param>
        /// <param name="values">Collection of values for the query.</param>
        /// <returns>The same <see cref="IAndHttpRequestBuilder"/>.</returns>
        IAndHttpRequestBuilder WithQuery(string name, IEnumerable<string> values);

        /// <summary>
        /// Adds query value to the <see cref="HttpRequest.Query"/>.
        /// </summary>
        /// <param name="name">Name of the query.</param>
        /// <param name="values">Collection of values for the query.</param>
        /// <returns>The same <see cref="IAndHttpRequestBuilder"/>.</returns>
        IAndHttpRequestBuilder WithQuery(string name, params string[] values);

        /// <summary>
        /// Adds query value to the <see cref="HttpRequest.Query"/>.
        /// </summary>
        /// <param name="name">Name of the query.</param>
        /// <param name="values">Collection of string values for the query.</param>
        /// <returns>The same <see cref="IAndHttpRequestBuilder"/>.</returns>
        IAndHttpRequestBuilder WithQuery(string name, StringValues values);

        /// <summary>
        /// Adds collection of query values to the <see cref="HttpRequest.Query"/>.
        /// </summary>
        /// <param name="query">Dictionary of query values to add.</param>
        /// <returns>The same <see cref="IAndHttpRequestBuilder"/>.</returns>
        IAndHttpRequestBuilder WithQuery(IDictionary<string, IEnumerable<string>> query);

        /// <summary>
        /// Adds collection of query values to the <see cref="HttpRequest.Query"/>.
        /// </summary>
        /// <param name="query">Dictionary of query values to add.</param>
        /// <returns>The same <see cref="IAndHttpRequestBuilder"/>.</returns>
        IAndHttpRequestBuilder WithQuery(IDictionary<string, StringValues> query);

        /// <summary>
        /// Adds collection of query values to the <see cref="HttpRequest.Query"/>.
        /// </summary>
        /// <param name="query">Dictionary of query values to add.</param>
        /// <returns>The same <see cref="IAndHttpRequestBuilder"/>.</returns>
        IAndHttpRequestBuilder WithQuery(IQueryCollection query);

        /// <summary>
        /// Adds query string to the <see cref="HttpRequest.Query"/>.
        /// </summary>
        /// <param name="queryString">Path base as string.</param>
        /// <returns>The same <see cref="IAndHttpRequestBuilder"/>.</returns>
        IAndHttpRequestBuilder WithQueryString(string queryString);

        /// <summary>
        /// Adds query string to the <see cref="HttpRequest.Query"/>.
        /// </summary>
        /// <param name="queryString">Query string as <see cref="QueryString"/>.</param>
        /// <returns>The same <see cref="IAndHttpRequestBuilder"/>.</returns>
        IAndHttpRequestBuilder WithQueryString(QueryString queryString);

        /// <summary>
        /// Adds scheme to the <see cref="HttpRequest.Scheme"/>.
        /// </summary>
        /// <param name="scheme">Scheme to add.</param>
        /// <returns>The same <see cref="IAndHttpRequestBuilder"/>.</returns>
        IAndHttpRequestBuilder WithScheme(string scheme);

        /// <summary>
        /// Adds HTTPS scheme to the <see cref="HttpRequest.Scheme"/>.
        /// </summary>
        /// <returns>The same <see cref="IAndHttpRequestBuilder"/>.</returns>
        IAndHttpRequestBuilder WithHttps();

        /// <summary>
        /// Adds all URI components to the <see cref="HttpRequest"/> by extracting them from the provided location.
        /// </summary>
        /// <param name="location">Location as string.</param>
        /// <returns>The same <see cref="IAndHttpRequestBuilder"/>.</returns>
        IAndHttpRequestBuilder WithLocation(string location);

        /// <summary>
        /// Adds all URI components to the <see cref="HttpRequest"/> by extracting them from the provided location.
        /// </summary>
        /// <param name="location">Location as <see cref="Uri"/>.</param>
        /// <returns>The same <see cref="IAndHttpRequestBuilder"/>.</returns>
        IAndHttpRequestBuilder WithLocation(Uri location);

        /// <summary>
        /// Adds all URI components to the <see cref="HttpRequest"/> by extracting them from the provided location.
        /// </summary>
        /// <param name="uriBuilder">Builder for the URI.</param>
        /// <returns>The same <see cref="IAndHttpRequestBuilder"/>.</returns>
        IAndHttpRequestBuilder WithLocation(Action<IUriTestBuilder> uriBuilder);

        /// <summary>
        /// Sets default authenticated <see cref="HttpContext.User"/> to the built request with "TestId" identifier and "TestUser" username.
        /// </summary>
        /// <returns>The same <see cref="IAndHttpRequestBuilder"/>.</returns>
        IAndHttpRequestBuilder WithAuthenticatedUser();

        /// <summary>
        /// Sets custom authenticated <see cref="HttpContext.User"/> to the built request using the provided user builder.
        /// </summary>
        /// <param name="userBuilder">Action setting the <see cref="HttpContext.User"/> by using <see cref="IClaimsPrincipalBuilder"/>.</param>
        /// <returns>The same <see cref="IAndHttpRequestBuilder"/>.</returns>
        IAndHttpRequestBuilder WithAuthenticatedUser(Action<IClaimsPrincipalBuilder> userBuilder);
    }
}
