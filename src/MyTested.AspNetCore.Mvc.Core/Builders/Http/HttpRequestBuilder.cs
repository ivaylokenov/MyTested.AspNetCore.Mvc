namespace MyTested.AspNetCore.Mvc.Builders.Http
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
    /// Used for building <see cref="HttpRequest"/>.
    /// </summary>
    public class HttpRequestBuilder : IAndHttpRequestBuilder
    {
        private static Encoding defaultEncoding = Encoding.UTF8;

        private readonly MockedHttpRequest request;

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpRequestBuilder"/> class.
        /// </summary>
        public HttpRequestBuilder()
        {
            this.request = new MockedHttpRequest
            {
                Scheme = HttpScheme.Http,
                Path = "/"
            };
        }

        /// <inheritdoc />
        public IAndHttpRequestBuilder WithBody(Stream body)
        {
            this.request.Body = body;
            return this;
        }

        /// <inheritdoc />
        public IAndHttpRequestBuilder WithBody<TBody>(TBody body, string contentType)
            => this.WithBody(body, contentType, defaultEncoding);

        /// <inheritdoc />
        public IAndHttpRequestBuilder WithBody<TBody>(TBody body, string contentType, Encoding encoding)
        {
            var stream = FormattersHelper.WriteAsStringToStream(body, contentType, encoding);

            return this
                .WithContentType(this.request.ContentType ?? contentType)
                .WithContentLength(this.request.ContentLength ?? stream.Length)
                .WithBody(stream);
        }

        /// <inheritdoc />
        public IAndHttpRequestBuilder WithStringBody(string body)
            => this.WithStringBody(body, defaultEncoding);

        /// <inheritdoc />
        public IAndHttpRequestBuilder WithStringBody(string body, Encoding encoding)
            => this.WithBody(body, ContentType.TextPlain, encoding);

        /// <inheritdoc />
        public IAndHttpRequestBuilder WithJsonBody(string jsonBody)
            => this.WithJsonBody(jsonBody, defaultEncoding);

        /// <inheritdoc />
        public IAndHttpRequestBuilder WithJsonBody(string jsonBody, Encoding encoding)
        {
            return this
                .WithContentType(this.request.ContentType ?? ContentType.ApplicationJson)
                .WithStringBody(jsonBody, encoding);
        }

        /// <inheritdoc />
        public IAndHttpRequestBuilder WithJsonBody(object jsonBody)
            => this.WithJsonBody(jsonBody, defaultEncoding);

        /// <inheritdoc />
        public IAndHttpRequestBuilder WithJsonBody(object jsonBody, Encoding encoding)
            => this.WithBody(jsonBody, ContentType.ApplicationJson, encoding);

        /// <inheritdoc />
        public IAndHttpRequestBuilder WithContentLength(long? contentLength)
        {
            this.request.ContentLength = contentLength;
            return this;
        }

        /// <inheritdoc />
        public IAndHttpRequestBuilder WithContentType(string contentType)
        {
            this.request.ContentType = contentType;
            return this;
        }

        /// <inheritdoc />
        public IAndHttpRequestBuilder WithCookie(string name, string value)
        {
            this.request.AddCookie(name, value);
            return this;
        }

        /// <inheritdoc />
        public IAndHttpRequestBuilder WithCookies(object cookies)
            => this.WithCookies(cookies.ToStringValueDictionary());

        /// <inheritdoc />
        public IAndHttpRequestBuilder WithCookies(IDictionary<string, string> cookies)
        {
            cookies.ForEach(c => this.WithCookie(c.Key, c.Value));
            return this;
        }

        /// <inheritdoc />
        public IAndHttpRequestBuilder WithCookies(IRequestCookieCollection cookies)
        {
            cookies.ForEach(c => this.WithCookie(c.Key, c.Value));
            return this;
        }

        /// <inheritdoc />
        public IAndHttpRequestBuilder WithFormField(string name, string value)
        {
            this.request.AddFormField(name, value);
            return this;
        }

        /// <inheritdoc />
        public IAndHttpRequestBuilder WithFormField(string name, IEnumerable<string> values)
            => this.WithFormField(name, new StringValues(values.ToArray()));

        /// <inheritdoc />
        public IAndHttpRequestBuilder WithFormField(string name, params string[] values)
            => this.WithFormField(name, values.AsEnumerable());

        /// <inheritdoc />
        public IAndHttpRequestBuilder WithFormField(string name, StringValues values)
        {
            this.request.AddFormField(name, values);
            return this;
        }

        /// <inheritdoc />
        public IAndHttpRequestBuilder WithFormFields(object formValues)
            => this.WithFormFields(formValues.ToStringValueDictionary());

        /// <inheritdoc />
        public IAndHttpRequestBuilder WithFormFields(IDictionary<string, string> formValues)
        {
            formValues.ForEach(h => this.WithFormField(h.Key, h.Value));
            return this;
        }

        /// <inheritdoc />
        public IAndHttpRequestBuilder WithFormFields(IDictionary<string, IEnumerable<string>> formValues)
        {
            formValues.ForEach(h => this.WithFormField(h.Key, h.Value));
            return this;
        }

        /// <inheritdoc />
        public IAndHttpRequestBuilder WithFormFields(IDictionary<string, StringValues> formValues)
        {
            formValues.ForEach(h => this.WithFormField(h.Key, h.Value));
            return this;
        }

        /// <inheritdoc />
        public IAndHttpRequestBuilder WithFormFile(IFormFile formFile)
        {
            this.request.AddFormFile(formFile);
            return this;
        }

        /// <inheritdoc />
        public IAndHttpRequestBuilder WithFormFiles(IEnumerable<IFormFile> formFiles)
        {
            formFiles.ForEach(f => this.WithFormFile(f));
            return this;
        }

        /// <inheritdoc />
        public IAndHttpRequestBuilder WithFormFiles(params IFormFile[] formFiles)
            => this.WithFormFiles(formFiles.AsEnumerable());

        /// <inheritdoc />
        public IAndHttpRequestBuilder WithForm(IFormCollection formCollection)
        {
            formCollection.ForEach(h => this.WithFormField(h.Key, h.Value));
            formCollection.Files.ForEach(f => this.WithFormFile(f));
            return this;
        }

        /// <inheritdoc />
        public IAndHttpRequestBuilder WithHeader(string name, string value)
        {
            this.request.Headers.Add(name, value);
            return this;
        }

        /// <inheritdoc />
        public IAndHttpRequestBuilder WithHeader(string name, IEnumerable<string> values)
            => this.WithHeader(name, new StringValues(values.ToArray()));

        /// <inheritdoc />
        public IAndHttpRequestBuilder WithHeader(string name, params string[] values)
            => this.WithHeader(name, values.AsEnumerable());

        /// <inheritdoc />
        public IAndHttpRequestBuilder WithHeader(string name, StringValues values)
        {
            this.request.Headers.Add(name, values);
            return this;
        }

        /// <inheritdoc />
        public IAndHttpRequestBuilder WithHeaders(object headers)
            => this.WithHeaders(headers.ToStringValueDictionary());

        /// <inheritdoc />
        public IAndHttpRequestBuilder WithHeaders(IDictionary<string, string> headers)
        {
            headers.ForEach(h => this.WithHeader(h.Key, h.Value));
            return this;
        }

        /// <inheritdoc />
        public IAndHttpRequestBuilder WithHeaders(IDictionary<string, IEnumerable<string>> headers)
        {
            headers.ForEach(h => this.WithHeader(h.Key, h.Value));
            return this;
        }

        /// <inheritdoc />
        public IAndHttpRequestBuilder WithHeaders(IDictionary<string, StringValues> headers)
        {
            headers.ForEach(h => this.WithHeader(h.Key, h.Value));
            return this;
        }

        /// <inheritdoc />
        public IAndHttpRequestBuilder WithHeaders(IHeaderDictionary headers)
        {
            headers.ForEach(h => this.WithHeader(h.Key, h.Value));
            return this;
        }

        /// <inheritdoc />
        public IAndHttpRequestBuilder WithHost(string host)
            => this.WithHost(new HostString(host));

        /// <inheritdoc />
        public IAndHttpRequestBuilder WithHost(HostString host)
        {
            this.request.Host = host;
            return this;
        }

        /// <inheritdoc />
        public IAndHttpRequestBuilder WithMethod(string method)
        {
            this.request.Method = method;
            return this;
        }

        /// <inheritdoc />
        public IAndHttpRequestBuilder WithPath(string path)
            => this.WithPath(new PathString(path));

        /// <inheritdoc />
        public IAndHttpRequestBuilder WithPath(PathString path)
        {
            this.request.Path = path;
            return this;
        }

        /// <inheritdoc />
        public IAndHttpRequestBuilder WithPathBase(string pathBase)
            => this.WithPathBase(new PathString(pathBase));

        /// <inheritdoc />
        public IAndHttpRequestBuilder WithPathBase(PathString pathBase)
        {
            this.request.PathBase = pathBase;
            return this;
        }

        /// <inheritdoc />
        public IAndHttpRequestBuilder WithProtocol(string protocol)
        {
            this.request.Protocol = protocol;
            return this;
        }

        /// <inheritdoc />
        public IAndHttpRequestBuilder WithQuery(string name, string value)
        {
            this.request.AddQueryValue(name, value);
            return this;
        }

        /// <inheritdoc />
        public IAndHttpRequestBuilder WithQuery(string name, IEnumerable<string> values)
            => this.WithQuery(name, new StringValues(values.ToArray()));

        /// <inheritdoc />
        public IAndHttpRequestBuilder WithQuery(string name, params string[] values)
            => this.WithQuery(name, values.AsEnumerable());

        /// <inheritdoc />
        public IAndHttpRequestBuilder WithQuery(string name, StringValues values)
        {
            this.request.AddQueryValue(name, values);
            return this;
        }

        /// <inheritdoc />
        public IAndHttpRequestBuilder WithQuery(object query)
            => this.WithQuery(query.ToStringValueDictionary());

        /// <inheritdoc />
        public IAndHttpRequestBuilder WithQuery(IDictionary<string, string> query)
        {
            query.ForEach(h => this.WithQuery(h.Key, h.Value));
            return this;
        }

        /// <inheritdoc />
        public IAndHttpRequestBuilder WithQuery(IDictionary<string, IEnumerable<string>> query)
        {
            query.ForEach(h => this.WithQuery(h.Key, h.Value));
            return this;
        }

        /// <inheritdoc />
        public IAndHttpRequestBuilder WithQuery(IDictionary<string, StringValues> query)
        {
            query.ForEach(h => this.WithQuery(h.Key, h.Value));
            return this;
        }

        /// <inheritdoc />
        public IAndHttpRequestBuilder WithQuery(IQueryCollection query)
        {
            query.ForEach(h => this.WithQuery(h.Key, h.Value));
            return this;
        }

        /// <inheritdoc />
        public IAndHttpRequestBuilder WithQueryString(string queryString)
            => this.WithQueryString(new QueryString(queryString));

        /// <inheritdoc />
        public IAndHttpRequestBuilder WithQueryString(QueryString queryString)
        {
            this.request.QueryString = queryString;
            return this;
        }

        /// <inheritdoc />
        public IAndHttpRequestBuilder WithScheme(string scheme)
        {
            this.request.Scheme = scheme;
            return this;
        }

        /// <inheritdoc />
        public IAndHttpRequestBuilder WithHttps()
            => this.WithScheme(HttpScheme.Https);

        /// <inheritdoc />
        public IAndHttpRequestBuilder WithLocation(string location)
        {
            var uri = LocationValidator.ValidateAndGetWellFormedUriString(
                location,
                this.ThrowNewInvalidHttpRequestMessageException);

            return this.WithLocation(uri);
        }

        /// <inheritdoc />
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

        /// <inheritdoc />
        public IAndHttpRequestBuilder WithLocation(Action<IUriTestBuilder> uriBuilder)
        {
            var mockedUriBuilder = new MockedUriBuilder();
            uriBuilder(mockedUriBuilder);
            var uri = mockedUriBuilder.GetUri();
            return this.WithLocation(uri);
        }

        /// <inheritdoc />
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
