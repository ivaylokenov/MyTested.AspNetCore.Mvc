namespace MyTested.AspNetCore.Mvc.Internal
{
    using System;
    using System.Collections.Generic;
    using Http;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Http.Features;
    using Plugins;
    using Services;

    public static class TestHelper
    {
        public static Action GlobalTestCleanup { get; set; }
        
        public static ISet<IHttpFeatureRegistrationPlugin> HttpFeatureRegistrationPlugins { get; }
            = new HashSet<IHttpFeatureRegistrationPlugin>();
        
        public static MockedHttpContext CreateMockedHttpContext()
        {
            var httpContextFactory = TestServiceProvider.GetService<IHttpContextFactory>();
            var httpContext = httpContextFactory != null
                ? MockedHttpContext.From(httpContextFactory.Create(new FeatureCollection()))
                : new MockedHttpContext();

            SetHttpContextToAccessor(httpContext);

            return httpContext;
        }

        public static void ApplyHttpFeatures(HttpContext httpContext)
        {
            foreach (var httpFeatureRegistrationPlugin in HttpFeatureRegistrationPlugins)
            {
                httpFeatureRegistrationPlugin.HttpFeatureRegistrationDelegate(httpContext);
            }
        }

        public static void SetHttpContextToAccessor(HttpContext httpContext)
        {
            var httpContextAccessor = TestServiceProvider.GetService<IHttpContextAccessor>();
            if (httpContextAccessor != null)
            {
                httpContextAccessor.HttpContext = httpContext;
            }
        }

        public static void ExecuteTestCleanup() => GlobalTestCleanup?.Invoke();
    }
}
