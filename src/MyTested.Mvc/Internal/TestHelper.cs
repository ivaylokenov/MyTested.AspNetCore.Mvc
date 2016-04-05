namespace MyTested.Mvc.Internal
{
    using System;
    using Application;
    using Http;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Http.Features;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Infrastructure;
    using Microsoft.AspNetCore.Mvc.Internal;
    using Microsoft.AspNetCore.Session;
    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.Extensions.DependencyInjection;

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

        public static void SetMockedSession(HttpContext httpContext)
        {
            var sessionStore = httpContext.RequestServices.GetService<ISessionStore>();
            if (sessionStore != null)
            {
                if (httpContext.Features.Get<ISessionFeature>() == null)
                {
                    ISession mockedSession;
                    if (sessionStore is MockedSessionStore)
                    {
                        mockedSession = new MockedSession();
                    }
                    else
                    {
                        mockedSession = sessionStore.Create(Guid.NewGuid().ToString(), TimeSpan.Zero, () => true, true);
                    }

                    httpContext.Features.Set<ISessionFeature>(new MockedSessionFeature
                    {
                        Session = mockedSession
                    });
                }
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

        public static void SetActionContextToAccessor(ActionContext actionContext)
        {
            var actionContextAccessor = TestServiceProvider.GetService<IActionContextAccessor>();
            if (actionContextAccessor != null)
            {
                actionContextAccessor.ActionContext = actionContext;
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
