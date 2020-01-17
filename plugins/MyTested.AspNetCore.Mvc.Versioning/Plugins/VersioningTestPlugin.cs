namespace MyTested.AspNetCore.Mvc.Plugins
{
    using System;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc.Versioning;

    public class VersioningTestPlugin : IHttpFeatureRegistrationPlugin
    {
        public Action<HttpContext> HttpFeatureRegistrationDelegate
            => httpContext => httpContext
                .Features
                .Set<IApiVersioningFeature>(new ApiVersioningFeature(httpContext));
    }
}
