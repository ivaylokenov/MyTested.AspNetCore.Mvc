namespace MyTested.Mvc
{
    using System;
    using Microsoft.AspNetCore.Http;

    public interface IHttpFeatureRegistrationPlugin
    {
        Action<HttpContext> HttpFeatureRegistrationDelegate { get; }
    }
}
