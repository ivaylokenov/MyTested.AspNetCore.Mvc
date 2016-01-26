namespace MyTested.Mvc.Builders.Http
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using Contracts.Http;
    using Contracts.Uris;
    using Internal.Http;
    using Uris;
    using Utilities.Validators;
    using Microsoft.Extensions.Primitives;
    using System.Linq;
    using Internal.Extensions;
    using Microsoft.AspNet.Http;

    /// <summary>
    /// Used for building HTTP request message.
    /// </summary>
    public class HttpRequestBuilder : IAndHttpRequestBuilder
    {
        private readonly MockedHttpRequest request;

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpRequestBuilder" /> class.
        /// </summary>
        public HttpRequestBuilder()
        {
            this.request = new MockedHttpRequest();
        }

        /// <summary>
        /// Adds body to the built HTTP request.
        /// </summary>
        /// <param name="stream">Body as stream.</param>
        /// <returns>The same HTTP request builder.</returns>
        public IAndHttpRequestBuilder WithBody(Stream stream)
        {
            this.request.Body = stream;
            return this;
        }

        /// <summary>
        /// Adds content length to the built HTTP request.
        /// </summary>
        /// <param name="contentLength">Content length as nullable long.</param>
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
        {
            return this.WithHeader(name, new StringValues(values.ToArray()));
        }

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
        /// Adds method to the built HTTP request.
        /// </summary>
        /// <param name="method">HTTP method represented by string.</param>
        /// <returns>The same HTTP request builder.</returns>
        public IAndHttpRequestBuilder WithMethod(string method)
        {
            this.request.Method = method;
            return this;
        }

        ///// <summary>
        ///// Adds request location to the built HTTP request message.
        ///// </summary>
        ///// <param name="location">Expected location as string.</param>
        ///// <returns>The same HTTP request message builder.</returns>
        //public IAndHttpRequestBuilder WithRequestUri(string location)
        //{
        //    this.request.RequestUri = LocationValidator.ValidateAndGetWellFormedUriString(
        //        location,
        //        this.ThrowNewInvalidHttpRequestMessageException);

        //    return this;
        //}

        ///// <summary>
        ///// Adds request location to the built HTTP request message.
        ///// </summary>
        ///// <param name="location">Expected location as URI.</param>
        ///// <returns>The same HTTP request message builder.</returns>
        //public IAndHttpRequestBuilder WithRequestUri(Uri location)
        //{
        //    this.request.RequestUri = location;
        //    return this;
        //}

        ///// <summary>
        ///// Adds request location provided by a builder to the HTTP request message.
        ///// </summary>
        ///// <param name="uriTestBuilder">Builder for expected URI.</param>
        ///// <returns>The same HTTP request message builder.</returns>
        //public IAndHttpRequestBuilder WithRequestUri(Action<IUriTestBuilder> uriTestBuilder)
        //{
        //    var mockedUriBuilder = new MockedUriBuilder();
        //    uriTestBuilder(mockedUriBuilder);
        //    this.request.RequestUri = mockedUriBuilder.GetUri();
        //    return this;
        //}

        ///// <summary>
        ///// Adds HTTP version to the built HTTP request message.
        ///// </summary>
        ///// <param name="version">HTTP version provided by string.</param>
        ///// <returns>The same HTTP request message builder.</returns>
        //public IAndHttpRequestBuilder WithVersion(string version)
        //{
        //    var parsedVersion = VersionValidator.TryParse(version, this.ThrowNewInvalidHttpRequestMessageException);
        //    return this.WithVersion(parsedVersion);
        //}

        ///// <summary>
        ///// Adds HTTP version to the built HTTP request message.
        ///// </summary>
        ///// <param name="major">Major number in the provided version.</param>
        ///// <param name="minor">Minor number in the provided version.</param>
        ///// <returns>The same HTTP request message builder.</returns>
        //public IAndHttpRequestBuilder WithVersion(int major, int minor)
        //{
        //    return this.WithVersion(new Version(major, minor));
        //}

        ///// <summary>
        ///// Adds HTTP version to the built HTTP request message.
        ///// </summary>
        ///// <param name="version">HTTP version provided by Version type.</param>
        ///// <returns>The same HTTP request message builder.</returns>
        //public IAndHttpRequestBuilder WithVersion(Version version)
        //{
        //    this.request.Version = version;
        //    return this;
        //}

        ///// <summary>
        ///// AndAlso method for better readability when building HTTP request message.
        ///// </summary>
        ///// <returns>The same HTTP request message builder.</returns>
        //public IHttpRequestMessageBuilder AndAlso()
        //{
        //    return this;
        //}

        //internal HttpRequestMessage GetHttpRequestMessage()
        //{
        //    return this.request;
        //}

        //private void ThrowNewInvalidHttpRequestMessageException(string propertyName, string expectedValue, string actualValue)
        //{
        //    throw new InvalidHttpRequestMessageException(string.Format(
        //        "When building HttpRequestMessage expected {0} to be {1}, but instead received {2}.",
        //        propertyName,
        //        expectedValue,
        //        actualValue));
        //}
    }
}
