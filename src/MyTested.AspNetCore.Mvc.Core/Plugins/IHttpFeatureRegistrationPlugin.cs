namespace MyTested.AspNetCore.Mvc.Plugins
{
    using System;
    using Microsoft.AspNetCore.Http;

    public interface IHttpFeatureRegistrationPlugin
    {
        Action<HttpContext> HttpFeatureRegistrationDelegate { get; }
    }
}
