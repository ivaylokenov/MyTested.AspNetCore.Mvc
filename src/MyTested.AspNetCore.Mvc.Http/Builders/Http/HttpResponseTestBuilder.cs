namespace MyTested.AspNetCore.Mvc.Builders.Http
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Text;
    using Base;
    using Contracts.Http;
    using Exceptions;
    using Internal.Formatters;
    using Internal.TestContexts;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Primitives;
    using Microsoft.Net.Http.Headers;
    using Utilities;
    using Utilities.Extensions;
    using Utilities.Validators;

    /// <summary>
    /// Used for testing <see cref="HttpResponse"/>.
    /// </summary>
    public class HttpResponseTestBuilder : BaseTestBuilderWithComponent,
        IAndHttpResponseTestBuilder
    {
        private static Encoding defaultEncoding = Encoding.UTF8;

        private HttpResponse httpResponse;
        private bool contentTypeTested;

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpResponseTestBuilder"/> class.
        /// </summary>
        /// <param name="testContext"><see cref="ComponentTestContext"/> containing data about the currently executed assertion chain.</param>
        public HttpResponseTestBuilder(ComponentTestContext testContext)
            : base(testContext)
        {
            this.httpResponse = testContext.HttpResponse;
        }

        /// <inheritdoc />
        public IAndHttpResponseTestBuilder WithBody(Stream body)
        {
            var expectedContents = body.ToByteArray();
            var actualContents = this.httpResponse.Body.ToByteArray();
            if (!expectedContents.SequenceEqual(actualContents))
            {
                this.ThrowNewHttpResponseAssertionException(
                    "body",
                    "to have contents as the provided ones",
                    "instead received different result");
            }

            return this;
        }

        /// <inheritdoc />
        public IAndHttpResponseTestBuilder WithBodyOfType<TBody>(string contentType)
            => this.WithBodyOfType<TBody>(contentType, defaultEncoding);

        /// <inheritdoc />
        public IAndHttpResponseTestBuilder WithBodyOfType<TBody>(string contentType, Encoding encoding)
        {
            try
            {
                FormattersHelper.ReadFromStream<TBody>(this.httpResponse.Body, contentType, encoding);
            }
            catch (InvalidDataException)
            {
                this.ThrowNewHttpResponseAssertionException(
                    "body",
                    $"to be of {typeof(TBody).ToFriendlyTypeName()} type when using '{contentType}'",
                    $"in fact it was not");
            }

            return this.WithContentType(contentType);
        }

        /// <inheritdoc />
        public IAndHttpResponseTestBuilder WithBody<TBody>(TBody body, string contentType)
            => this.WithBody(body, contentType, defaultEncoding);

        /// <inheritdoc />
        public IAndHttpResponseTestBuilder WithBody<TBody>(TBody body, string contentType, Encoding encoding)
        {
            var parsedBody = FormattersHelper.ReadFromStream<TBody>(this.httpResponse.Body, contentType, encoding);

            if (Reflection.AreNotDeeplyEqual(body, parsedBody))
            {
                this.ThrowNewHttpResponseAssertionException(
                    "body",
                    "to be the given object",
                    "in fact it was different");
            }

            return this.WithContentType(contentType);
        }

        /// <inheritdoc />
        public IAndHttpResponseTestBuilder WithStringBody(string body)
            => this.WithStringBody(body, defaultEncoding);

        /// <inheritdoc />
        public IAndHttpResponseTestBuilder WithStringBody(string body, Encoding encoding)
            => this.WithBody(body, ContentType.TextPlain, encoding);

        /// <inheritdoc />
        public IAndHttpResponseTestBuilder WithJsonBody(string jsonBody)
            => this.WithJsonBody(jsonBody, defaultEncoding);

        /// <inheritdoc />
        public IAndHttpResponseTestBuilder WithJsonBody(string jsonBody, Encoding encoding)
             => this
                .WithContentType(ContentType.ApplicationJson)
                .WithStringBody(jsonBody, encoding);

        /// <inheritdoc />
        public IAndHttpResponseTestBuilder WithJsonBody<TBody>(TBody jsonBody)
            => this.WithJsonBody(jsonBody, defaultEncoding);

        /// <inheritdoc />
        public IAndHttpResponseTestBuilder WithJsonBody<TBody>(TBody jsonBody, Encoding encoding)
            => this.WithBody(jsonBody, ContentType.ApplicationJson, encoding);

        /// <inheritdoc />
        public IAndHttpResponseTestBuilder WithContentLength(long? contentLenght)
        {
            var actualContentLength = this.httpResponse.ContentLength;
            if (contentLenght != actualContentLength)
            {
                this.ThrowNewHttpResponseAssertionException(
                    "content length",
                    $"to be {contentLenght.GetErrorMessageName(includeQuotes: false)}",
                    $"instead received {actualContentLength.GetErrorMessageName(includeQuotes: false)}");
            }

            return this;
        }

        /// <inheritdoc />
        public IAndHttpResponseTestBuilder WithContentType(string contentType)
        {
            if (this.contentTypeTested)
            {
                return this;
            }

            this.contentTypeTested = true;

            var actualContentType = this.httpResponse.ContentType;
            if (contentType != this.httpResponse.ContentType)
            {
                this.ThrowNewHttpResponseAssertionException(
                    "content type",
                    $"to be {contentType.GetErrorMessageName()}",
                    $"instead received {actualContentType.GetErrorMessageName()}");
            }

            return this;
        }

        /// <inheritdoc />
        public IAndHttpResponseTestBuilder ContainingCookie(Action<IResponseCookieTestBuilder> cookieBuilder)
        {
            var newResponseCookieTestBuilder = new ResponseCookieTestBuilder();
            cookieBuilder(newResponseCookieTestBuilder);
            var expectedCookie = newResponseCookieTestBuilder.GetResponseCookie();

            var expectedCookieName = expectedCookie.Name;
            this.ContainingCookie(expectedCookieName);
            var actualCookie = this.GetCookieByKey(expectedCookieName);

            var validations = newResponseCookieTestBuilder.GetResponseCookieValidations();

            if (validations.Any(v => !v(expectedCookie, actualCookie)))
            {
                this.ThrowNewHttpResponseAssertionException(
                    "cookies",
                    $"to contain cookie with '{expectedCookieName}' name and '{expectedCookie}' value",
                    $"the value was '{actualCookie}'");
            }

            return this;
        }

        /// <inheritdoc />
        public IAndHttpResponseTestBuilder ContainingCookie(string key)
        {
            var cookie = this.GetCookieByKey(key);
            if (cookie == null)
            {
                this.ThrowNewHttpResponseAssertionException(
                    "cookies",
                    $"to contain cookie with '{key}' name",
                    "such was not found");
            }

            return this;
        }

        /// <inheritdoc />
        public IAndHttpResponseTestBuilder ContainingCookie(string key, string value)
        {
            this.ContainingCookie(key);
            var cookie = this.GetCookieByKey(key);

            if (cookie.Value != value)
            {
                this.ThrowNewHttpResponseAssertionException(
                    "cookies",
                    $"to contain cookie with '{key}' name and '{value}' value",
                    $"the value was '{cookie.Value}'");
            }

            return this;
        }

        /// <inheritdoc />
        public IAndHttpResponseTestBuilder ContainingCookie(string key, string value, CookieOptions options)
        {
            this.ContainingCookie(key, value);
            var cookie = this.GetCookieByKey(key);
            var cookieOptions = this.GetCookieOptions(cookie);

            if (Reflection.AreNotDeeplyEqual(options, cookieOptions))
            {
                var expectedCookie = new SetCookieHeaderValue(
                    Uri.EscapeDataString(key),
                    Uri.EscapeDataString(value))
                {
                    Domain = options.Domain,
                    Path = options.Path,
                    Expires = options.Expires,
                    Secure = options.Secure,
                    HttpOnly = options.HttpOnly,
                };

                this.ThrowNewHttpResponseAssertionException(
                    "cookies",
                    $"to contain cookie with the provided options - '{expectedCookie}'",
                    $"in fact they were different - '{cookie}'");
            }

            return this;
        }

        /// <inheritdoc />
        public IAndHttpResponseTestBuilder ContainingCookies(object cookies)
            => this.ContainingCookies(cookies.ToStringValueDictionary());

        /// <inheritdoc />
        public IAndHttpResponseTestBuilder ContainingCookies(IDictionary<string, string> cookies)
        {
            var expectedCookiesCount = cookies.Count;
            var actualCookiesCount = this.GetAllCookies().Count;
            if (expectedCookiesCount != actualCookiesCount)
            {
                this.ThrowNewHttpResponseAssertionException(
                    "cookies",
                    $"to have {expectedCookiesCount} {(expectedCookiesCount != 1 ? "items" : "item")}",
                    $"instead found {actualCookiesCount}");
            }

            cookies.ForEach(c => this.ContainingCookie(c.Key, c.Value));
            return this;
        }

        /// <inheritdoc />
        public IAndHttpResponseTestBuilder ContainingHeader(string name)
        {
            if (!this.httpResponse.Headers.ContainsKey(name))
            {
                this.ThrowNewHttpResponseAssertionException(
                    "headers",
                    $"to contain header with '{name}' name",
                    "such was not found");
            }

            return this;
        }

        /// <inheritdoc />
        public IAndHttpResponseTestBuilder ContainingHeader(string name, string value)
        {
            this.ContainingHeader(name);
            var headerValues = this.httpResponse.Headers[name];

            this.ValidateHeaderValues(name, value, headerValues);

            return this;
        }

        /// <inheritdoc />
        public IAndHttpResponseTestBuilder ContainingHeader(string name, IEnumerable<string> values)
            => this.ContainingHeader(name, new StringValues(values.ToArray()));

        /// <inheritdoc />
        public IAndHttpResponseTestBuilder ContainingHeader(string name, params string[] values)
            => this.ContainingHeader(name, values.AsEnumerable());

        /// <inheritdoc />
        public IAndHttpResponseTestBuilder ContainingHeader(string name, StringValues values)
        {
            this.ContainingHeader(name);
            var headerValues = this.httpResponse.Headers[name];
            if (values.Count != headerValues.Count)
            {
                this.ThrowNewHttpResponseAssertionException(
                    "headers",
                    $"to contain header with '{name}' name and {values.Count} {(values.Count != 1 ? "values" : "value")}",
                    $"instead found {headerValues.Count}");
            }

            values.ForEach(v => this.ValidateHeaderValues(name, v, headerValues));
            return this;
        }

        /// <inheritdoc />
        public IAndHttpResponseTestBuilder ContainingHeaders(IHeaderDictionary headers)
        {
            this.ValidateHeadersCount(headers.Count);
            headers.ForEach(h => this.ContainingHeader(h.Key, h.Value));
            return this;
        }

        /// <inheritdoc />
        public IAndHttpResponseTestBuilder ContainingHeaders(object headers)
        {
            var headerDictionary = headers.ToObjectValueDictionary();

            headerDictionary.ForEach(h =>
            {
                var headerAsString = h.Value as string;
                if (headerAsString != null)
                {
                    this.ContainingHeader(h.Key, headerAsString);
                    return;
                }

                var headerAsEnumerable = h.Value as IEnumerable<string>;
                if (headerAsEnumerable != null)
                {
                    this.ContainingHeader(h.Key, new StringValues(headerAsEnumerable.ToArray()));
                }
            });

            return this;
        }

        /// <inheritdoc />
        public IAndHttpResponseTestBuilder ContainingHeaders(IDictionary<string, string> headers)
        {
            this.ValidateHeadersCount(headers.Count);
            headers.ForEach(h => this.ContainingHeader(h.Key, h.Value));
            return this;
        }

        /// <inheritdoc />
        public IAndHttpResponseTestBuilder ContainingHeaders(IDictionary<string, StringValues> headers)
        {
            this.ValidateHeadersCount(headers.Count);
            headers.ForEach(h => this.ContainingHeader(h.Key, h.Value));
            return this;
        }

        /// <inheritdoc />
        public IAndHttpResponseTestBuilder ContainingHeaders(IDictionary<string, IEnumerable<string>> headers)
        {
            this.ValidateHeadersCount(headers.Count);
            headers.ForEach(h => this.ContainingHeader(h.Key, h.Value));
            return this;
        }

        /// <inheritdoc />
        public IAndHttpResponseTestBuilder WithStatusCode(HttpStatusCode statusCode)
        {
            HttpStatusCodeValidator.ValidateHttpStatusCode(
                statusCode,
                this.httpResponse.StatusCode,
                this.ThrowNewHttpResponseAssertionException);

            return this;
        }

        /// <inheritdoc />
        public IAndHttpResponseTestBuilder WithStatusCode(int statusCode)
            => this.WithStatusCode((HttpStatusCode)statusCode);

        /// <inheritdoc />
        public IHttpResponseTestBuilder AndAlso() => this;

        private IList<SetCookieHeaderValue> GetAllCookies()
        {
            this.ContainingHeader(HttpHeader.SetCookie);
            var cookieHeader = this.httpResponse.Headers[HttpHeader.SetCookie];

            if (!cookieHeader.Any())
            {
                this.ThrowNewHttpResponseAssertionException(
                    "to have",
                    "set cookies",
                    "none were found");
            }

            IList<SetCookieHeaderValue> setCookieHeaderValue;
            if (!SetCookieHeaderValue.TryParseList(cookieHeader, out setCookieHeaderValue))
            {
                this.ThrowNewHttpResponseAssertionException(
                    "to have",
                    "valid cookie values",
                    "some of them were invalid");
            }

            return setCookieHeaderValue;
        }

        private SetCookieHeaderValue GetCookieByKey(string key) => this.GetAllCookies().FirstOrDefault(c => c.Name == key);

        private CookieOptions GetCookieOptions(SetCookieHeaderValue cookie)
            => new CookieOptions
            {
                Domain = cookie.Domain,
                Expires = cookie.Expires,
                HttpOnly = cookie.HttpOnly,
                Path = cookie.Path,
                Secure = cookie.Secure
            };

        private void ValidateHeaderValues(string name, string expectedValue, StringValues headerValues)
        {
            if (!headerValues.Contains(expectedValue) && string.Join(",", headerValues) != expectedValue)
            {
                this.ThrowNewHttpResponseAssertionException(
                    "headers",
                    $"to contain header with '{name}' name and '{expectedValue}' value",
                    $"the {(headerValues.Count != 1 ? "values were" : "value was")} '{headerValues}'");
            }
        }

        private void ValidateHeadersCount(int expectedCount)
        {
            var actualHeadersCount = this.httpResponse.Headers.Count;
            if (expectedCount != actualHeadersCount)
            {
                this.ThrowNewHttpResponseAssertionException(
                    "headers",
                    $"to have {expectedCount} {(expectedCount != 1 ? "items" : "item")}",
                    $"instead found {actualHeadersCount}");
            }
        }

        private void ThrowNewHttpResponseAssertionException(string propertyName, string expectedValue, string actualValue)
        {
            throw new HttpResponseAssertionException(string.Format(
                "{0} HTTP response {1} {2}, but {3}.",
                this.TestContext.ExceptionMessagePrefix,
                propertyName,
                expectedValue,
                actualValue));
        }
    }
}
