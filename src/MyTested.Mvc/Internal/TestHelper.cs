namespace MyTested.Mvc.Internal
{
    using Application;
    using Http;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Http.Features;
    using Microsoft.AspNetCore.Mvc.Internal;
    using Microsoft.Extensions.Caching.Memory;

    public static class TestHelper
    {
        /// <summary>
        /// Tries to create instance of the provided type. Returns null if not successful.
        /// </summary>
        /// <typeparam name="TInstance">Type to create.</typeparam>
        /// <returns>Instance of TInstance type.</returns>
        public static TInstance TryCreateInstance<TInstance>()
            where TInstance : class
        {
            var instance = TestServiceProvider.GetService<TInstance>();
            if (instance != null)
            {
                return instance;
            }

            try
            {
                var typeActivatorCache = TestServiceProvider.GetRequiredService<ITypeActivatorCache>();
                return typeActivatorCache.CreateInstance<TInstance>(TestServiceProvider.Global, typeof(TInstance));
            }
            catch
            {
                return null;
            }
        }

        public static MockedHttpContext CreateMockedHttpContext()
        {
            var httpContextFactory = TestServiceProvider.GetService<IHttpContextFactory>();
            var httpContext = httpContextFactory != null
                ? MockedHttpContext.From(httpContextFactory.Create(new FeatureCollection()))
                : new MockedHttpContext();

            SetHttpContextToAccessor(httpContext);

            return httpContext;
        }

        public static void SetHttpContextToAccessor(HttpContext httpContext)
        {
            var httpContextAccessor = TestServiceProvider.GetService<IHttpContextAccessor>();
            if (httpContextAccessor != null)
            {
                httpContextAccessor.HttpContext = httpContext;
            }
        }

        public static void ClearMemoryCache()
        {
            var memoryCache = TestServiceProvider.GetService<IMemoryCache>();
            if (memoryCache != null)
            {
                memoryCache.Dispose();
            }
        }
    }
}
