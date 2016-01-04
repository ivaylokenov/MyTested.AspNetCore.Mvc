namespace MyTested.Mvc.Internal.Http
{
    using System;
    using Microsoft.AspNet.Http.Features;
    using Microsoft.AspNet.Http.Internal;

    public class MockedHttpResponse : DefaultHttpResponse
    {
        public MockedHttpResponse(DefaultHttpContext context, IFeatureCollection features)
            : base (context, features)
        {
        }

        public override void RegisterForDispose(IDisposable disposable)
        {
            // TODO: think what to do here?
        }
    }
}
