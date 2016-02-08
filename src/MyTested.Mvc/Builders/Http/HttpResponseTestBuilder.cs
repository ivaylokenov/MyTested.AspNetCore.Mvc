namespace MyTested.Mvc.Builders.Http
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.IO;
    using System.Net;
    using Base;
    using Contracts.Http;
    using Utilities.Extensions;
    using Exceptions;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Primitives;
    using Utilities.Validators;
    using Microsoft.Net.Http.Headers;
    using Utilities;

    /// <summary>
    /// Used for testing the HTTP response.
    /// </summary>
    public class HttpResponseTestBuilder : BaseTestBuilderWithInvokedAction,
        IAndHttpResponseTestBuilder
    {
        private HttpResponse httpResponse;

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpResponseTestBuilder" /> class.
        /// </summary>
        /// <param name="controller">Controller on which the action will be tested.</param>
        /// <param name="actionName">Name of the tested action.</param>
        /// <param name="caughtException">Caught exception during the action execution.</param>
        /// <param name="httpResponse">HTTP response after the tested action.</param>
        public HttpResponseTestBuilder(
            Controller controller,
            string actionName,
            Exception caughtException,
            HttpResponse httpResponse)
            : base(controller, actionName, caughtException)
        {
            this.httpResponse = httpResponse;
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
                    "to have contents as the provided one",
                    "instead received different result");
            }

            return this;
        }

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
                    $"to be {contentLenght.GetErrorMessageName()}",
                    $"instead received {actualContentLength.GetErrorMessageName()}");
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
            throw new NotImplementedException();
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
                    $"to contain cookie with the provided options - '{expectedCookie.ToString()}'",
                    $"in fact they were different - '{cookie.ToString()}'");
            }

            return this;
        }
        
        /// <summary>
        /// Tests whether HTTP response message contains the provided dictionary of cookies.
        /// </summary>
        /// <param name="cookies">Dictionary of cookies.</param>
        /// <returns>The same HTTP response message test builder.</returns>
        public IAndHttpResponseTestBuilder ContainingCookies(IDictionary<string, string> cookies)
        {
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
        {
            return this.ContainingHeader(name, new StringValues(values.ToArray()));
        }

        /// <summary>
        /// Tests whether HTTP response message contains header with the same name and values as the provided ones.
        /// </summary>
        /// <param name="name">Expected header name.</param>
        /// <param name="values">Expected header values.</param>
        /// <returns>The same HTTP response message test builder.</returns>
        public IAndHttpResponseTestBuilder ContainingHeader(string name, params string[] values)
        {
            return this.ContainingHeader(name, values.AsEnumerable());
        }

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
                    $"to have header with '{name}' name and {values.Count} {(values.Count != 1 ? "values" : "value")}",
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
            if (!headerValues.Contains(expectedValue))
            {
                this.ThrowNewHttpResponseAssertionException(
                    "headers",
                    $"to contain header with '{name}' name and '{expectedValue}' value",
                    $"the {(headerValues.Count != 1 ? "the values were" : "the value was")} '{headerValues}'");
            }
        }

        private void ValidateHeadersCount(int expectedCount)
        {
            var actualHeadersCount = this.httpResponse.Headers.Count;
            if (expectedCount != this.httpResponse.Headers.Count)
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
                "When calling {0} action in {1} expected HTTP response result {2} {3}, but {4}.",
                this.ActionName,
                this.Controller.GetName(),
                propertyName,
                expectedValue,
                actualValue));
        }
    }
}
