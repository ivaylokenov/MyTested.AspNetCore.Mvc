namespace MyTested.Mvc.Internal.Http
{
    using System.Collections.Generic;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.AspNet.Http;
    using Microsoft.AspNet.Http.Features.Internal;
    using Microsoft.AspNet.Http.Internal;
    using Microsoft.Extensions.Primitives;

    public class MockedHttpRequest : HttpRequest
    {
        private readonly IHeaderDictionary headerDictionary;
        private readonly FormFileCollection formFileCollection;
        private readonly Dictionary<string, string> cookieValues;
        private readonly Dictionary<string, StringValues> formValues;
        private readonly Dictionary<string, StringValues> queryValues;

        private IFormFeature formFeature;

        public MockedHttpRequest()
        {
            this.headerDictionary = new HeaderDictionary();
            this.formFileCollection = new FormFileCollection();
            this.cookieValues = new Dictionary<string, string>();
            this.formValues = new Dictionary<string, StringValues>();
            this.queryValues = new Dictionary<string, StringValues>();
        }

        public override Stream Body { get; set; }

        public override long? ContentLength { get; set; }

        public override string ContentType { get; set; }

        public override IRequestCookieCollection Cookies { get; set; }

        public override IFormCollection Form { get; set; }

        public override bool HasFormContentType => this.formFeature.HasFormContentType;

        public override IHeaderDictionary Headers => this.headerDictionary;

        public override HostString Host { get; set; }

        public override HttpContext HttpContext => null;

        public override bool IsHttps { get; set; }

        public override string Method { get; set; }

        public override PathString Path { get; set; }

        public override PathString PathBase { get; set; }

        public override string Protocol { get; set; }

        public override IQueryCollection Query { get; set; }

        public override QueryString QueryString { get; set; }

        public override string Scheme { get; set; }

        public override Task<IFormCollection> ReadFormAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.formFeature.ReadFormAsync(cancellationToken);
        }

        public void AddCookie(string name, string value)
        {
            this.cookieValues[name] = value;
        }

        public HttpRequest Initialize()
        {
            this.Form = new FormCollection(this.formValues, this.formFileCollection);
            this.Cookies = new RequestCookieCollection(this.cookieValues);
            this.Query = new QueryCollection(this.queryValues);
            this.formFeature = new FormFeature(this.Form);

            return this;
        }
    }
}
