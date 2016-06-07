namespace MyTested.AspNetCore.Mvc
{
    using System;
    using Microsoft.AspNetCore.Http;

    public interface IHttpFeatureRegistrationPlugin
    {
        Action<HttpContext> HttpFeatureRegistrationDelegate { get; }
    }
}
