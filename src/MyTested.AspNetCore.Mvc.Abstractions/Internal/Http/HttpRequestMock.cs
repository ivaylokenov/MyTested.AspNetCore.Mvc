namespace MyTested.AspNetCore.Mvc.Internal.Http
{
    using System.Collections.Generic;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Http.Features;
    using Microsoft.AspNetCore.Http.Internal;
    using Microsoft.Extensions.Primitives;
    using Utilities.Validators;

    /// <summary>
    /// Mock of HTTP request object.
    /// </summary>
    public class HttpRequestMock : HttpRequest
    {
        private readonly HttpContext httpContext;
        private readonly IHeaderDictionary headerDictionary;
        private readonly FormFileCollection formFiles;
        private readonly Dictionary<string, string> cookieValues;
        private readonly Dictionary<string, StringValues> formValues;
        private readonly Dictionary<string, StringValues> queryValues;

        private IFormFeature formFeature;

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpRequestMock"/> class.
        /// </summary>
        public HttpRequestMock(HttpContext httpContext)
        {
            CommonValidator.CheckForNullReference(httpContext, nameof(this.HttpContext));

            this.httpContext = httpContext;
            this.headerDictionary = new HeaderDictionary();
            this.formFiles = new FormFileCollection();
            this.cookieValues = new Dictionary<string, string>();
            this.formValues = new Dictionary<string, StringValues>();
            this.queryValues = new Dictionary<string, StringValues>();
        }

        /// <summary>
        /// Gets or set the RequestBody Stream.
        /// </summary>
        /// <value>The HTTP request body stream.</value>
        public override Stream Body { get; set; }

        /// <summary>
        /// Gets or sets the Content-Length header.
        /// </summary>
        /// <value>The HTTP request content length.</value>
        public override long? ContentLength { get; set; }

        /// <summary>
        /// Gets or sets the Content-Type header.
        /// </summary>
        /// <value>The HTTP request content type.</value>
        public override string ContentType { get; set; }

        /// <summary>
        /// Gets the collection of Cookies for this request.
        /// </summary>
        /// <value>The HTTP request cookie collection.</value>
        public override IRequestCookieCollection Cookies { get; set; }

        /// <summary>
        /// Gets or sets the request body as a form.
        /// </summary>
        /// <value>The HTTP request form collection.</value>
        public override IFormCollection Form { get; set; }

        /// <summary>
        /// Checks the content-type header for form types.
        /// </summary>
        /// <value>True of False.</value>
        public override bool HasFormContentType => this.formFeature.HasFormContentType;

        /// <summary>
        /// Gets the request headers.
        /// </summary>
        /// <value>The HTTP request header collection.</value>
        public override IHeaderDictionary Headers => this.headerDictionary;

        /// <summary>
        /// Gets or set the Host header. May include the port.
        /// </summary>
        /// <value>The HTTP request host.</value>
        public override HostString Host { get; set; }

        /// <summary>
        /// Gets the <see cref="Microsoft.AspNetCore.Http.HttpContext"/> for this request.
        /// </summary>
        /// <value>The HTTP request HTTP context.</value>
        public override HttpContext HttpContext => this.httpContext;

        /// <summary>
        /// Returns true if the Scheme is 'https'.
        /// </summary>
        /// <value>True or False.</value>
        public override bool IsHttps { get; set; }

        /// <summary>
        /// Gets or set the HTTP method.
        /// </summary>
        /// <value>The HTTP request method.</value>
        public override string Method { get; set; }

        /// <summary>
        /// Gets or set the request path.
        /// </summary>
        /// <value>The HTTP request path.</value>
        public override PathString Path { get; set; }

        /// <summary>
        /// Gets or set the path base.
        /// </summary>
        /// <value>The HTTP request path base.</value>
        public override PathString PathBase { get; set; }

        /// <summary>
        /// Gets or set the request protocol.
        /// </summary>
        /// <value>The HTTP request protocol.</value>
        public override string Protocol { get; set; }

        /// <summary>
        /// Gets or set the query value collection.
        /// </summary>
        /// <value>The HTTP request query collection.</value>
        public override IQueryCollection Query { get; set; }

        /// <summary>
        /// Gets or set the query string.
        /// </summary>
        /// <value>The HTTP request query string.</value>
        public override QueryString QueryString { get; set; }

        /// <summary>
        /// Gets or set the HTTP request scheme.
        /// </summary>
        /// <value>The HTTP request scheme.</value>
        public override string Scheme { get; set; }

        /// <summary>
        /// Reads the request body if it is a form.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token for the asynchronous operation.</param>
        /// <returns>Task of form collection.</returns>
        public override Task<IFormCollection> ReadFormAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.formFeature.ReadFormAsync(cancellationToken);
        }

        /// <summary>
        /// Adds cookie value to the mocked HTTP request.
        /// </summary>
        /// <param name="name">Name of the cookie.</param>
        /// <param name="value">Value of the cookie.</param>
        public void AddCookie(string name, string value)
        {
            this.cookieValues[name] = value;
        }

        /// <summary>
        /// Adds form field value to the mocked HTTP request.
        /// </summary>
        /// <param name="name">Name of the form field.</param>
        /// <param name="value">Value of the form field.</param>
        public void AddFormField(string name, string value)
        {
            this.formValues.Add(name, value);
        }

        /// <summary>
        /// Adds form file value to the mocked HTTP request.
        /// </summary>
        /// <param name="file">Form file to add.</param>
        public void AddFormFile(IFormFile file)
        {
            this.formFiles.Add(file);
        }

        /// <summary>
        /// Adds query string value to the mocked HTTP request.
        /// </summary>
        /// <param name="name">Name of the query.</param>
        /// <param name="value">Value of the query.</param>
        public void AddQueryValue(string name, string value)
        {
            this.queryValues.Add(name, value);
        }

        /// <summary>
        /// Initializes and prepares the mocked HTTP request.
        /// </summary>
        /// <returns>Instance of HttpRequest.</returns>
        public HttpRequest Initialize()
        {
            this.Form = new FormCollection(this.formValues, this.formFiles);
            this.Cookies = new RequestCookieCollection(this.cookieValues);
            this.Query = new QueryCollection(this.queryValues);
            this.formFeature = new FormFeature(this.Form);

            return this;
        }
    }
}
