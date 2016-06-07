namespace MyTested.AspNetCore.Mvc.Test.Setups.Common
{
    using System;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Http.Features;

    public class CustomHttpContextFactory : IHttpContextFactory
    {
        public HttpContext Create(IFeatureCollection featureCollection)
        {
            var result = new DefaultHttpContext();
            result.Request.ContentType = ContentType.AudioVorbis;
            return result;
        }

        public void Dispose(HttpContext httpContext)
        {
            throw new NotImplementedException();
        }
    }
}
