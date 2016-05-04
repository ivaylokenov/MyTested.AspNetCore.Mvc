namespace MyTested.Mvc.Builders.Http
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using Contracts.Http;
    using Contracts.Uris;
    using Exceptions;
    using Internal.Formatters;
    using Internal.Http;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Primitives;
    using Uris;
    using Utilities.Extensions;
    using Utilities.Validators;

    /// <summary>
    /// Used for building HTTP request message.
    /// </summary>
    public class HttpRequestBuilder : IAndHttpRequestBuilder
    {
        private static Encoding defaultEncoding = Encoding.UTF8;

        private readonly MockedHttpRequest request;

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpRequestBuilder" /> class.
        /// </summary>
        public HttpRequestBuilder()
        {
            this.request = new MockedHttpRequest
            {
                Scheme = HttpScheme.Http,
                Path = "/"
            };
        }

        /// <summary>
        /// Adds body to the built HTTP request.
        /// </summary>
        /// <param name="stream">Body as stream.</param>
        /// <returns>The same HTTP request builder.</returns>
        public IAndHttpRequestBuilder WithBody(Stream body)
        {
            this.request.Body = body;
            return this;
        }

        /// <summary>
        /// Adds body to the built HTTP request by trying to format the provided object based on the content type and the configured formatters. Uses UTF8 encoding.
        /// </summary>
        /// <typeparam name="TBody">Type of body.</typeparam>
        /// <param name="body">Body as object.</param>
        /// <param name="contentType">Content type to use for formatting.</param>
        /// <returns>The same HTTP request builder.</returns>
        public IAndHttpRequestBuilder WithBody<TBody>(TBody body, string contentType)
            => this.WithBody(body, contentType, defaultEncoding);

        /// <summary>
        /// Adds body to the built HTTP request by trying to format the provided object based on the content type and the configured formatters.
        /// </summary>
        /// <typeparam name="TBody">Type of body.</typeparam>
        /// <param name="body">Body as object.</param>
        /// <param name="contentType">Content type to use for formatting.</param>
        /// <param name="encoding">Encoding to use for the body.</param>
        /// <returns>The same HTTP request builder.</returns>
        public IAndHttpRequestBuilder WithBody<TBody>(TBody body, string contentType, Encoding encoding)
        {
            var stream = FormattersHelper.WriteAsStringToStream(body, contentType, encoding);

            return this
                .WithContentType(this.request.ContentType ?? contentType)
                .WithContentLength(this.request.ContentLength ?? stream.Length)
                .WithBody(stream);
        }

        /// <summary>
        /// Adds string body to the built HTTP request.
        /// </summary>
        /// <param name="body">Body as string. If no content type is set on the request, 'text/plain' will be used. Uses UTF8 encoding.</param>
        /// <returns>The same HTTP request builder.</returns>
        public IAndHttpRequestBuilder WithStringBody(string body)
            => this.WithStringBody(body, defaultEncoding);

        /// <summary>
        /// Adds string body to the built HTTP request. If no content type is set on the request, 'text/plain' will be used.
        /// </summary>
        /// <param name="stream">Body as string.</param>
        /// <param name="encoding">Encoding to use for the body.</param>
        /// <returns>The same HTTP request builder.</returns>
        public IAndHttpRequestBuilder WithStringBody(string body, Encoding encoding)
            => this.WithBody(body, ContentType.TextPlain, encoding);

        /// <summary>
        /// Adds JSON body to the built HTTP request. Sets 'application/json' to the content type. Uses UTF8 encoding.
        /// </summary>
        /// <param name="jsonBody">JSON body as string.</param>
        /// <returns>The same HTTP request builder.</returns>
        public IAndHttpRequestBuilder WithJsonBody(string jsonBody)
            => this.WithJsonBody(jsonBody, defaultEncoding);

        /// <summary>
        /// Adds JSON body to the built HTTP request. If no content type is set on the request, 'application/json' will be used.
        /// </summary>
        /// <param name="jsonBody">JSON body as string.</param>
        /// <param name="encoding">Encoding to use for the body.</param>
        /// <returns>The same HTTP request builder.</returns>
        public IAndHttpRequestBuilder WithJsonBody(string jsonBody, Encoding encoding)
        {
            return this
                .WithContentType(this.request.ContentType ?? ContentType.ApplicationJson)
                .WithStringBody(jsonBody, encoding);
        }

        /// <summary>
        /// Adds JSON body to the built HTTP request. If no content type is set on the request, 'application/json' will be used. Uses UTF8 encoding.
        /// </summary>
        /// <param name="jsonBody">Object to serialize using the built-in JSON formatters in ASP.NET Core MVC.</param>
        /// <returns>The same HTTP request builder.</returns>
        public IAndHttpRequestBuilder WithJsonBody(object jsonBody)
            => this.WithJsonBody(jsonBody, defaultEncoding);

        /// <summary>
        /// Adds JSON body to the built HTTP request. If no content type is set on the request, 'application/json' will be used.
        /// </summary>
        /// <param name="jsonBody">Object to serialize using the built-in JSON formatters in ASP.NET Core MVC.</param>
        /// <param name="encoding">Encoding to use for the body.</param>
        /// <returns>The same HTTP request builder.</returns>
        public IAndHttpRequestBuilder WithJsonBody(object jsonBody, Encoding encoding)
            => this.WithBody(jsonBody, ContentType.ApplicationJson, encoding);

        /// <summary>
        /// Adds content length to the built HTTP request.
        /// </summary>
        /// <param name="contentLength">Content length as null-able long.</param>
        /// <returns>The same HTTP request builder.</returns>
        public IAndHttpRequestBuilder WithContentLength(long? contentLength)
        {
            this.request.ContentLength = contentLength;
            return this;
        }

        /// <summary>
        /// Adds content type to the built HTTP request.
        /// </summary>
        /// <param name="contentType">Content type as string.</param>
        /// <returns>The same HTTP request builder.</returns>
        public IAndHttpRequestBuilder WithContentType(string contentType)
        {
            this.request.ContentType = contentType;
            return this;
        }

        /// <summary>
        /// Adds cookie to the built HTTP request.
        /// </summary>
        /// <param name="name">Cookie name.</param>
        /// <param name="value">Cookie value.</param>
        /// <returns>The same HTTP request builder.</returns>
        public IAndHttpRequestBuilder WithCookie(string name, string value)
        {
            this.request.AddCookie(name, value);
            return this;
        }

        public IAndHttpRequestBuilder WithCookies(object cookies)
            => this.WithCookies(cookies.ToStringValueDictionary());

        /// <summary>
        /// Adds cookies to the built HTTP request.
        /// </summary>
        /// <param name="cookies">Dictionary of name-value cookies.</param>
        /// <returns>The same HTTP request builder.</returns>
        public IAndHttpRequestBuilder WithCookies(IDictionary<string, string> cookies)
        {
            cookies.ForEach(c => this.WithCookie(c.Key, c.Value));
            return this;
        }

        /// <summary>
        /// Adds cookies to the built HTTP request.
        /// </summary>
        /// <param name="cookies">Request cookie collection.</param>
        /// <returns>The same HTTP request builder.</returns>
        public IAndHttpRequestBuilder WithCookies(IRequestCookieCollection cookies)
        {
            cookies.ForEach(c => this.WithCookie(c.Key, c.Value));
            return this;
        }

        /// <summary>
        /// Adds form field to the built HTTP request.
        /// </summary>
        /// <param name="name">Name of the form field.</param>
        /// <param name="value">Value of the form field.</param>
        /// <returns>The same HTTP request builder.</returns>
        public IAndHttpRequestBuilder WithFormField(string name, string value)
        {
            this.request.AddFormField(name, value);
            return this;
        }

        /// <summary>
        /// Adds form field to the built HTTP request.
        /// </summary>
        /// <param name="name">Name of the form field.</param>
        /// <param name="values">Collection of values for the form field.</param>
        /// <returns>The same HTTP request builder.</returns>
        public IAndHttpRequestBuilder WithFormField(string name, IEnumerable<string> values)
            => this.WithFormField(name, new StringValues(values.ToArray()));

        /// <summary>
        /// Adds form field to the built HTTP request.
        /// </summary>
        /// <param name="name">Name of the form field.</param>
        /// <param name="values">Collection of values for the form field.</param>
        /// <returns>The same HTTP request builder.</returns>
        public IAndHttpRequestBuilder WithFormField(string name, params string[] values)
            => this.WithFormField(name, values.AsEnumerable());

        /// <summary>
        /// Adds form field to the built HTTP request.
        /// </summary>
        /// <param name="name">Name of the form field.</param>
        /// <param name="values">Collection of string values for the form field.</param>
        /// <returns>The same HTTP request builder.</returns>
        public IAndHttpRequestBuilder WithFormField(string name, StringValues values)
        {
            this.request.AddFormField(name, values);
            return this;
        }

        public IAndHttpRequestBuilder WithFormFields(object formValues)
            => this.WithFormFields(formValues.ToStringValueDictionary());

        public IAndHttpRequestBuilder WithFormFields(IDictionary<string, string> formValues)
        {
            formValues.ForEach(h => this.WithFormField(h.Key, h.Value));
            return this;
        }

        /// <summary>
        /// Adds form fields to the built HTTP request.
        /// </summary>
        /// <param name="formValues">Dictionary of form fields to add.</param>
        /// <returns>The same HTTP request builder.</returns>
        public IAndHttpRequestBuilder WithFormFields(IDictionary<string, IEnumerable<string>> formValues)
        {
            formValues.ForEach(h => this.WithFormField(h.Key, h.Value));
            return this;
        }

        /// <summary>
        /// Adds form fields to the built HTTP request.
        /// </summary>
        /// <param name="formValues">Dictionary of form fields to add.</param>
        /// <returns>The same HTTP request builder.</returns>
        public IAndHttpRequestBuilder WithFormFields(IDictionary<string, StringValues> formValues)
        {
            formValues.ForEach(h => this.WithFormField(h.Key, h.Value));
            return this;
        }

        /// <summary>
        /// Adds form file to the built HTTP request.
        /// </summary>
        /// <param name="formFile">Form file to add.</param>
        /// <returns>The same HTTP request builder.</returns>
        public IAndHttpRequestBuilder WithFormFile(IFormFile formFile)
        {
            this.request.AddFormFile(formFile);
            return this;
        }

        /// <summary>
        /// Adds form files to the built HTTP request.
        /// </summary>
        /// <param name="formFiles">Collection of form files.</param>
        /// <returns>The same HTTP request builder.</returns>
        public IAndHttpRequestBuilder WithFormFiles(IEnumerable<IFormFile> formFiles)
        {
            formFiles.ForEach(f => this.WithFormFile(f));
            return this;
        }

        /// <summary>
        /// Adds form files to the built HTTP request.
        /// </summary>
        /// <param name="formFiles">Form file parameters.</param>
        /// <returns>The same HTTP request builder.</returns>
        public IAndHttpRequestBuilder WithFormFiles(params IFormFile[] formFiles)
            => this.WithFormFiles(formFiles.AsEnumerable());

        /// <summary>
        /// Adds form values and files to the built HTTP request.
        /// </summary>
        /// <param name="formCollection">Form collection to add.</param>
        /// <returns>The same HTTP request builder.</returns>
        public IAndHttpRequestBuilder WithForm(IFormCollection formCollection)
        {
            formCollection.ForEach(h => this.WithFormField(h.Key, h.Value));
            formCollection.Files.ForEach(f => this.WithFormFile(f));
            return this;
        }

        /// <summary>
        /// Adds header to the built HTTP request.
        /// </summary>
        /// <param name="name">Name of the header.</param>
        /// <param name="value">Value of the header.</param>
        /// <returns>The same HTTP request builder.</returns>
        public IAndHttpRequestBuilder WithHeader(string name, string value)
        {
            this.request.Headers.Add(name, value);
            return this;
        }

        /// <summary>
        /// Adds header to the built HTTP request.
        /// </summary>
        /// <param name="name">Name of the header.</param>
        /// <param name="values">Collection of values for the header.</param>
        /// <returns>The same HTTP request builder.</returns>
        public IAndHttpRequestBuilder WithHeader(string name, IEnumerable<string> values)
            => this.WithHeader(name, new StringValues(values.ToArray()));

        /// <summary>
        /// Adds header to the built HTTP request.
        /// </summary>
        /// <param name="name">Name of the header.</param>
        /// <param name="values">Collection of values for the header.</param>
        /// <returns>The same HTTP request builder.</returns>
        public IAndHttpRequestBuilder WithHeader(string name, params string[] values)
            => this.WithHeader(name, values.AsEnumerable());

        /// <summary>
        /// Adds header to the built HTTP request.
        /// </summary>
        /// <param name="name">Name of the header.</param>
        /// <param name="values">Collection of string values for the header.</param>
        /// <returns>The same HTTP request builder.</returns>
        public IAndHttpRequestBuilder WithHeader(string name, StringValues values)
        {
            this.request.Headers.Add(name, values);
            return this;
        }

        public IAndHttpRequestBuilder WithHeaders(object headers)
            => this.WithHeaders(headers.ToStringValueDictionary());

        public IAndHttpRequestBuilder WithHeaders(IDictionary<string, string> headers)
        {
            headers.ForEach(h => this.WithHeader(h.Key, h.Value));
            return this;
        }

        /// <summary>
        /// Adds collection of headers to the built HTTP request.
        /// </summary>
        /// <param name="headers">Dictionary of headers to add.</param>
        /// <returns>The same HTTP request builder.</returns>
        public IAndHttpRequestBuilder WithHeaders(IDictionary<string, IEnumerable<string>> headers)
        {
            headers.ForEach(h => this.WithHeader(h.Key, h.Value));
            return this;
        }

        /// <summary>
        /// Adds collection of headers to the built HTTP request.
        /// </summary>
        /// <param name="headers">Dictionary of headers to add.</param>
        /// <returns>The same HTTP request builder.</returns>
        public IAndHttpRequestBuilder WithHeaders(IDictionary<string, StringValues> headers)
        {
            headers.ForEach(h => this.WithHeader(h.Key, h.Value));
            return this;
        }

        /// <summary>
        /// Adds collection of headers to the built HTTP request.
        /// </summary>
        /// <param name="headers">Dictionary of headers to add.</param>
        /// <returns>The same HTTP request builder.</returns>
        public IAndHttpRequestBuilder WithHeaders(IHeaderDictionary headers)
        {
            headers.ForEach(h => this.WithHeader(h.Key, h.Value));
            return this;
        }

        /// <summary>
        /// Adds host to the built HTTP request.
        /// </summary>
        /// <param name="host">Host as string.</param>
        /// <returns>The same HTTP request builder.</returns>
        public IAndHttpRequestBuilder WithHost(string host)
            => this.WithHost(new HostString(host));

        /// <summary>
        /// Adds host to the built HTTP request.
        /// </summary>
        /// <param name="host">Host as HostString.</param>
        /// <returns>The same HTTP request builder.</returns>
        public IAndHttpRequestBuilder WithHost(HostString host)
        {
            this.request.Host = host;
            return this;
        }

        /// <summary>
        /// Adds method to the built HTTP request.
        /// </summary>
        /// <param name="method">HTTP method represented by string.</param>
        /// <returns>The same HTTP request builder.</returns>
        public IAndHttpRequestBuilder WithMethod(string method)
        {
            this.request.Method = method;
            return this;
        }

        /// <summary>
        /// Adds path to the built HTTP request.
        /// </summary>
        /// <param name="path">Path as string.</param>
        /// <returns>The same HTTP request builder.</returns>
        public IAndHttpRequestBuilder WithPath(string path)
            => this.WithPath(new PathString(path));

        /// <summary>
        /// Adds path to the built HTTP request.
        /// </summary>
        /// <param name="path">Path as PathString.</param>
        /// <returns>The same HTTP request builder.</returns>
        public IAndHttpRequestBuilder WithPath(PathString path)
        {
            this.request.Path = path;
            return this;
        }

        /// <summary>
        /// Adds path base to the built HTTP request.
        /// </summary>
        /// <param name="pathBase">Path base as string.</param>
        /// <returns>The same HTTP request builder.</returns>
        public IAndHttpRequestBuilder WithPathBase(string pathBase)
            => this.WithPathBase(new PathString(pathBase));

        /// <summary>
        /// Adds path base to the built HTTP request.
        /// </summary>
        /// <param name="pathBase">Path base as PathString.</param>
        /// <returns>The same HTTP request builder.</returns>
        public IAndHttpRequestBuilder WithPathBase(PathString pathBase)
        {
            this.request.PathBase = pathBase;
            return this;
        }

        /// <summary>
        /// Adds protocol to the built HTTP request.
        /// </summary>
        /// <param name="protocol">Protocol as string.</param>
        /// <returns>The same HTTP request builder.</returns>
        public IAndHttpRequestBuilder WithProtocol(string protocol)
        {
            this.request.Protocol = protocol;
            return this;
        }

        /// <summary>
        /// Adds query value to the built HTTP request.
        /// </summary>
        /// <param name="name">Name of the query.</param>
        /// <param name="value">Value of the query.</param>
        /// <returns>The same HTTP request builder.</returns>
        public IAndHttpRequestBuilder WithQuery(string name, string value)
        {
            this.request.AddQueryValue(name, value);
            return this;
        }

        /// <summary>
        /// Adds query value to the built HTTP request.
        /// </summary>
        /// <param name="name">Name of the query.</param>
        /// <param name="values">Collection of values for the query.</param>
        /// <returns>The same HTTP request builder.</returns>
        public IAndHttpRequestBuilder WithQuery(string name, IEnumerable<string> values)
            => this.WithQuery(name, new StringValues(values.ToArray()));

        /// <summary>
        /// Adds query value to the built HTTP request.
        /// </summary>
        /// <param name="name">Name of the query.</param>
        /// <param name="values">Collection of values for the query.</param>
        /// <returns>The same HTTP request builder.</returns>
        public IAndHttpRequestBuilder WithQuery(string name, params string[] values)
            => this.WithQuery(name, values.AsEnumerable());

        /// <summary>
        /// Adds query value to the built HTTP request.
        /// </summary>
        /// <param name="name">Name of the query.</param>
        /// <param name="values">Collection of string values for the query.</param>
        /// <returns>The same HTTP request builder.</returns>
        public IAndHttpRequestBuilder WithQuery(string name, StringValues values)
        {
            this.request.AddQueryValue(name, values);
            return this;
        }

        public IAndHttpRequestBuilder WithQuery(object query)
            => this.WithQuery(query.ToStringValueDictionary());

        public IAndHttpRequestBuilder WithQuery(IDictionary<string, string> query)
        {
            query.ForEach(h => this.WithQuery(h.Key, h.Value));
            return this;
        }

        /// <summary>
        /// Adds collection of query values to the built HTTP request.
        /// </summary>
        /// <param name="query">Dictionary of query values to add.</param>
        /// <returns>The same HTTP request builder.</returns>
        public IAndHttpRequestBuilder WithQuery(IDictionary<string, IEnumerable<string>> query)
        {
            query.ForEach(h => this.WithQuery(h.Key, h.Value));
            return this;
        }

        /// <summary>
        /// Adds collection of query values to the built HTTP request.
        /// </summary>
        /// <param name="query">Dictionary of query values to add.</param>
        /// <returns>The same HTTP request builder.</returns>
        public IAndHttpRequestBuilder WithQuery(IDictionary<string, StringValues> query)
        {
            query.ForEach(h => this.WithQuery(h.Key, h.Value));
            return this;
        }

        /// <summary>
        /// Adds collection of query values to the built HTTP request.
        /// </summary>
        /// <param name="query">Dictionary of query values to add.</param>
        /// <returns>The same HTTP request builder.</returns>
        public IAndHttpRequestBuilder WithQuery(IQueryCollection query)
        {
            query.ForEach(h => this.WithQuery(h.Key, h.Value));
            return this;
        }

        /// <summary>
        /// Adds query string to the built HTTP request.
        /// </summary>
        /// <param name="queryString">Path base as string.</param>
        /// <returns>The same HTTP request builder.</returns>
        public IAndHttpRequestBuilder WithQueryString(string queryString)
            => this.WithQueryString(new QueryString(queryString));

        /// <summary>
        /// Adds query string to the built HTTP request.
        /// </summary>
        /// <param name="queryString">Query string as QueryString.</param>
        /// <returns>The same HTTP request builder.</returns>
        public IAndHttpRequestBuilder WithQueryString(QueryString queryString)
        {
            this.request.QueryString = queryString;
            return this;
        }

        /// <summary>
        /// Adds scheme to the built HTTP request.
        /// </summary>
        /// <param name="scheme">Scheme to add.</param>
        /// <returns>The same HTTP request builder.</returns>
        public IAndHttpRequestBuilder WithScheme(string scheme)
        {
            this.request.Scheme = scheme;
            return this;
        }

        /// <summary>
        /// Adds HTTPS scheme to the built HTTP request.
        /// </summary>
        /// <returns>The same HTTP request builder.</returns>
        public IAndHttpRequestBuilder WithHttps()
            => this.WithScheme("https");

        /// <summary>
        /// Adds all URI components to the built HTTP request by extracting them from the provided location.
        /// </summary>
        /// <param name="location">Location as string.</param>
        /// <returns>The same HTTP request builder.</returns>
        public IAndHttpRequestBuilder WithLocation(string location)
        {
            var uri = LocationValidator.ValidateAndGetWellFormedUriString(
                location,
                this.ThrowNewInvalidHttpRequestMessageException);

            return this.WithLocation(uri);
        }

        /// <summary>
        /// Adds all URI components to the built HTTP request by extracting them from the provided location.
        /// </summary>
        /// <param name="location">Location as URI.</param>
        /// <returns>The same HTTP request builder.</returns>
        public IAndHttpRequestBuilder WithLocation(Uri location)
        {
            Uri uri;
            if (location.IsAbsoluteUri)
            {
                uri = new Uri(location.OriginalString, UriKind.Absolute);
            }
            else
            {
                uri = new Uri(new Uri("http://localhost"), location);
            }

            if (location.IsAbsoluteUri)
            {
                this
                    .WithHost(HostString.FromUriComponent(uri))
                    .WithScheme(uri.Scheme);
            }

            return this
                .WithPathBase(PathString.FromUriComponent(uri))
                .WithPath(PathString.FromUriComponent(uri.AbsolutePath))
                .WithQueryString(QueryString.FromUriComponent(uri.Query));
        }

        /// <summary>
        /// Adds all URI components to the built HTTP request by extracting them from the provided location.
        /// </summary>
        /// <param name="uriBuilder">Builder for the URI.</param>
        /// <returns>The same HTTP request builder.</returns>
        public IAndHttpRequestBuilder WithLocation(Action<IUriTestBuilder> uriBuilder)
        {
            var mockedUriBuilder = new MockedUriBuilder();
            uriBuilder(mockedUriBuilder);
            var uri = mockedUriBuilder.GetUri();
            return this.WithLocation(uri);
        }

        /// <summary>
        /// AndAlso method for better readability when building HTTP request.
        /// </summary>
        /// <returns>The same HTTP request builder.</returns>
        public IHttpRequestBuilder AndAlso() => this;

        internal void ApplyTo(HttpRequest httpRequest)
        {
            this.request.Initialize();

            httpRequest.Body = this.request.Body;
            httpRequest.ContentLength = this.request.ContentLength;
            httpRequest.ContentType = this.request.ContentType;
            httpRequest.Method = this.request.Method;
            httpRequest.Protocol = this.request.Protocol;
            httpRequest.Scheme = this.request.Scheme;

            if (this.request.Host.HasValue)
            {
                httpRequest.Host = this.request.Host;
            }

            if (this.request.Path.HasValue)
            {
                httpRequest.Path = this.request.Path;
            }

            if (this.request.PathBase.HasValue)
            {
                httpRequest.PathBase = this.request.PathBase;
            }

            if (this.request.QueryString.HasValue)
            {
                httpRequest.QueryString = this.request.QueryString;
            }

            if (this.request.Cookies.Any())
            {
                httpRequest.Cookies = this.request.Cookies;
            }

            if (this.request.Form.Any())
            {
                httpRequest.Form = this.request.Form;
            }

            if (this.request.Query.Any())
            {
                httpRequest.Query = this.request.Query;
            }

            this.request.Headers.ForEach(h => httpRequest.Headers.Add(h));
        }

        private void ThrowNewInvalidHttpRequestMessageException(string propertyName, string expectedValue, string actualValue)
        {
            throw new InvalidHttpRequestException(string.Format(
                "When building HttpRequest expected {0} {1}, but {2}.",
                propertyName,
                expectedValue,
                actualValue));
        }
    }
}
