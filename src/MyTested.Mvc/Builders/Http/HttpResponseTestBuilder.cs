namespace MyTested.Mvc.Builders.Http
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
    /// Used for testing the HTTP response.
    /// </summary>
    public class HttpResponseTestBuilder : BaseTestBuilderWithInvokedAction,
        IAndHttpResponseTestBuilder
    {
        private static Encoding defaultEncoding = Encoding.UTF8;

        private HttpResponse httpResponse;
        private bool contentTypeTested;

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpResponseTestBuilder" /> class.
        /// </summary>
        /// <param name="controller">Controller on which the action will be tested.</param>
        /// <param name="actionName">Name of the tested action.</param>
        /// <param name="caughtException">Caught exception during the action execution.</param>
        /// <param name="httpResponse">HTTP response after the tested action.</param>
        public HttpResponseTestBuilder(ControllerTestContext testContext)
            : base(testContext)
        {
            this.httpResponse = testContext.HttpResponse;
        }

        /// <summary>
        /// Tests whether HTTP response message body has the same contents as the provided Stream.
        /// </summary>
        /// <param name="body">Expected stream body.</param>
        /// <returns>The same HTTP response message test builder.</returns>
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

        /// <summary>
        /// Tests whether HTTP response message body has the same type as the provided one. Body is parsed from the configured formatters and provided content type. Uses UTF8 encoding.
        /// </summary>
        /// <typeparam name="TBody">Expected type of body to test.</typeparam>
        /// <param name="contentType">Expected content type as string.</param>
        /// <returns>The same HTTP response message test builder.</returns>
        public IAndHttpResponseTestBuilder WithBodyOfType<TBody>(string contentType)
            => this.WithBodyOfType<TBody>(contentType, defaultEncoding);

        /// <summary>
        /// Tests whether HTTP response message body has the same type as the provided one. Body is parsed from the configured formatters and provided content type.
        /// </summary>
        /// <typeparam name="TBody">Expected type of body to test.</typeparam>
        /// <param name="contentType">Expected content type as string.</param>
        /// <param name="encoding">Encoding of the body.</param>
        /// <returns>The same HTTP response message test builder.</returns>
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

        /// <summary>
        /// Tests whether HTTP response message body has deeply equal value as the provided one. Body is parsed from the configured formatters and provided content type. Uses UTF8 encoding.
        /// </summary>
        /// <typeparam name="TBody">Expected type of body to test.</typeparam>
        /// <param name="body">Expected body object.</param>
        /// <param name="contentType">Expected content type as string.</param>
        /// <returns>The same HTTP response message test builder.</returns>
        public IAndHttpResponseTestBuilder WithBody<TBody>(TBody body, string contentType)
            => this.WithBody(body, contentType, defaultEncoding);

        /// <summary>
        /// Tests whether HTTP response message body has deeply equal value as the provided one. Body is parsed from the configured formatters and provided content type.
        /// </summary>
        /// <typeparam name="TBody">Expected type of body to test.</typeparam>
        /// <param name="body">Expected body object.</param>
        /// <param name="contentType">Expected content type as string.</param>
        /// <param name="encoding">Encoding of the body.</param>
        /// <returns>The same HTTP response message test builder.</returns>
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

        /// <summary>
        /// Tests whether HTTP response message body is equal to the provided string. Body is parsed from the configured formatters and 'text/plain' content type. Uses UTF8 encoding.
        /// </summary>
        /// <param name="body">Expected body as string.</param>
        /// <returns>The same HTTP response message test builder.</returns>
        public IAndHttpResponseTestBuilder WithStringBody(string body)
            => this.WithStringBody(body, defaultEncoding);

        /// <summary>
        /// Tests whether HTTP response message body is equal to the provided string. Body is parsed from the configured formatters and 'text/plain' content type.
        /// </summary>
        /// <param name="body">Expected body as string.</param>
        /// <param name="encoding">Encoding of the body.</param>
        /// <returns>The same HTTP response message test builder.</returns>
        public IAndHttpResponseTestBuilder WithStringBody(string body, Encoding encoding)
            => this.WithBody(body, ContentType.TextPlain, encoding);

        /// <summary>
        /// Tests whether HTTP response message body is equal to the provided JSON string. Body is parsed from the configured formatters and 'application/json' content type. Uses UTF8 encoding.
        /// </summary>
        /// <param name="jsonBody">Expected body as JSON string.</param>
        /// <returns>The same HTTP response message test builder.</returns>
        public IAndHttpResponseTestBuilder WithJsonBody(string jsonBody)
            => this.WithJsonBody(jsonBody, defaultEncoding);

        /// <summary>
        /// Tests whether HTTP response message body is equal to the provided JSON string. Body is parsed from the configured formatters and 'application/json' content type.
        /// </summary>
        /// <param name="jsonBody">Expected body as JSON string.</param>
        /// <param name="encoding">Encoding of the body.</param>
        /// <returns>The same HTTP response message test builder.</returns>
        public IAndHttpResponseTestBuilder WithJsonBody(string jsonBody, Encoding encoding)
             => this
                .WithContentType(ContentType.ApplicationJson)
                .WithStringBody(jsonBody, encoding);

        /// <summary>
        /// Tests whether HTTP response message body is deeply equal to the provided object. Body is parsed from the configured formatters and 'application/json' content type. Uses UTF8 encoding.
        /// </summary>
        /// <typeparam name="TBody">Expected type of body to test.</typeparam>
        /// <param name="jsonBody">Expected body as object.</param>
        /// <returns>The same HTTP response message test builder.</returns>
        public IAndHttpResponseTestBuilder WithJsonBody<TBody>(TBody jsonBody)
            => this.WithJsonBody(jsonBody, defaultEncoding);

        /// <summary>
        /// Tests whether HTTP response message body is equal to the provided JSON string. Body is parsed from the configured formatters and 'application/json' content type.
        /// </summary>
        /// <typeparam name="TBody">Expected type of body to test.</typeparam>
        /// <param name="jsonBody">Expected body as object.</param>
        /// <param name="encoding">Encoding of the body.</param>
        /// <returns>The same HTTP response message test builder.</returns>
        public IAndHttpResponseTestBuilder WithJsonBody<TBody>(TBody jsonBody, Encoding encoding)
            => this.WithBody(jsonBody, ContentType.ApplicationJson, encoding);

        /// <summary>
        /// Tests whether HTTP response message content length is the same as the provided one.
        /// </summary>
        /// <param name="contentLenght">Expected content length.</param>
        /// <returns>The same HTTP response message test builder.</returns>
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

        /// <summary>
        /// Tests whether HTTP response message content type is the same as the provided one.
        /// </summary>
        /// <param name="contentType">Expected content type.</param>
        /// <returns>The same HTTP response message test builder.</returns>
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

        /// <summary>
        /// Tests whether HTTP response message contains cookie by using test builder.
        /// </summary>
        /// <param name="cookieBuilder">Action of response cookie test builder.</param>
        /// <returns>The same HTTP response message test builder.</returns>
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

        /// <summary>
        /// Tests whether HTTP response message contains cookie with the same name as the provided one.
        /// </summary>
        /// <param name="key">Expected cookie name.</param>
        /// <returns>The same HTTP response message test builder.</returns>
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

        /// <summary>
        /// Tests whether HTTP response message contains cookie with the same name and value as the provided ones.
        /// </summary>
        /// <param name="key">Expected cookie name.</param>
        /// <param name="value">Expected cookie value.</param>
        /// <returns>The same HTTP response message test builder.</returns>
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

        /// <summary>
        /// Tests whether HTTP response message contains cookie with the same name, value and options as the provided ones.
        /// </summary>
        /// <param name="key">Expected cookie name.</param>
        /// <param name="value">Expected cookie value.</param>
        /// <param name="options">Expected cookie options.</param>
        /// <returns>The same HTTP response message test builder.</returns>
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

        public IAndHttpResponseTestBuilder ContainingCookies(object cookies)
            => this.ContainingCookies(cookies.ToStringValueDictionary());

        /// <summary>
        /// Tests whether HTTP response message contains the provided dictionary of cookies.
        /// </summary>
        /// <param name="cookies">Dictionary of cookies.</param>
        /// <returns>The same HTTP response message test builder.</returns>
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

        /// <summary>
        /// Tests whether HTTP response message contains header with the same name as the provided one.
        /// </summary>
        /// <param name="name">Expected header name.</param>
        /// <returns>The same HTTP response message test builder.</returns>
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

        /// <summary>
        /// Tests whether HTTP response message contains header with the same name and value as the provided ones.
        /// </summary>
        /// <param name="name">Expected header name.</param>
        /// <param name="value">Expected header value.</param>
        /// <returns>The same HTTP response message test builder.</returns>
        public IAndHttpResponseTestBuilder ContainingHeader(string name, string value)
        {
            this.ContainingHeader(name);
            var headerValues = this.httpResponse.Headers[name];

            this.ValidateHeaderValues(name, value, headerValues);

            return this;
        }

        /// <summary>
        /// Tests whether HTTP response message contains header with the same name and values as the provided ones.
        /// </summary>
        /// <param name="name">Expected header name.</param>
        /// <param name="value">Expected header values.</param>
        /// <returns>The same HTTP response message test builder.</returns>
        public IAndHttpResponseTestBuilder ContainingHeader(string name, IEnumerable<string> values)
            => this.ContainingHeader(name, new StringValues(values.ToArray()));

        /// <summary>
        /// Tests whether HTTP response message contains header with the same name and values as the provided ones.
        /// </summary>
        /// <param name="name">Expected header name.</param>
        /// <param name="values">Expected header values.</param>
        /// <returns>The same HTTP response message test builder.</returns>
        public IAndHttpResponseTestBuilder ContainingHeader(string name, params string[] values)
            => this.ContainingHeader(name, values.AsEnumerable());

        /// <summary>
        /// Tests whether HTTP response message contains header with the same name and values as the provided ones.
        /// </summary>
        /// <param name="name">Expected header name.</param>
        /// <param name="values">Expected header values.</param>
        /// <returns>The same HTTP response message test builder.</returns>
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

        /// <summary>
        /// Tests whether HTTP response message contains the same headers as the provided ones.
        /// </summary>
        /// <param name="headers">Dictionary of headers.</param>
        /// <returns>The same HTTP response message test builder.</returns>
        public IAndHttpResponseTestBuilder ContainingHeaders(IHeaderDictionary headers)
        {
            this.ValidateHeadersCount(headers.Count);
            headers.ForEach(h => this.ContainingHeader(h.Key, h.Value));
            return this;
        }

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

        public IAndHttpResponseTestBuilder ContainingHeaders(IDictionary<string, string> headers)
        {
            this.ValidateHeadersCount(headers.Count);
            headers.ForEach(h => this.ContainingHeader(h.Key, h.Value));
            return this;
        }

        /// <summary>
        /// Tests whether HTTP response message contains the same headers as the provided ones.
        /// </summary>
        /// <param name="headers">Dictionary of headers.</param>
        /// <returns>The same HTTP response message test builder.</returns>
        public IAndHttpResponseTestBuilder ContainingHeaders(IDictionary<string, StringValues> headers)
        {
            this.ValidateHeadersCount(headers.Count);
            headers.ForEach(h => this.ContainingHeader(h.Key, h.Value));
            return this;
        }

        /// <summary>
        /// Tests whether HTTP response message contains the same headers as the provided ones.
        /// </summary>
        /// <param name="headers">Dictionary of headers.</param>
        /// <returns>The same HTTP response message test builder.</returns>
        public IAndHttpResponseTestBuilder ContainingHeaders(IDictionary<string, IEnumerable<string>> headers)
        {
            this.ValidateHeadersCount(headers.Count);
            headers.ForEach(h => this.ContainingHeader(h.Key, h.Value));
            return this;
        }

        /// <summary>
        /// Tests whether HTTP response message status code is the same as the provided one.
        /// </summary>
        /// <param name="statusCode">Expected status code.</param>
        /// <returns>The same HTTP response message test builder.</returns>
        public IAndHttpResponseTestBuilder WithStatusCode(HttpStatusCode statusCode)
        {
            HttpStatusCodeValidator.ValidateHttpStatusCode(
                statusCode,
                this.httpResponse.StatusCode,
                this.ThrowNewHttpResponseAssertionException);

            return this;
        }

        /// <summary>
        /// Tests whether HTTP response message status code is the same as the provided HttpStatusCode.
        /// </summary>
        /// <param name="statusCode">Expected status code.</param>
        /// <returns>The same HTTP response message test builder.</returns>
        public IAndHttpResponseTestBuilder WithStatusCode(int statusCode)
            => this.WithStatusCode((HttpStatusCode)statusCode);

        /// <summary>
        /// AndAlso method for better readability when chaining HTTP response message tests.
        /// </summary>
        /// <returns>The same HTTP response message test builder.</returns>
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
                "When calling {0} action in {1} expected HTTP response {2} {3}, but {4}.",
                this.ActionName,
                this.Controller.GetName(),
                propertyName,
                expectedValue,
                actualValue));
        }
    }
}
