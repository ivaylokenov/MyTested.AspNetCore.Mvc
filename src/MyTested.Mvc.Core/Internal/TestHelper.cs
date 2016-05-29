namespace MyTested.Mvc.Internal
{
    using System;
    using System.Collections.Generic;
    using Application;
    using Http;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Http.Features;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Infrastructure;
    using Microsoft.AspNetCore.Mvc.Internal;

    public static class TestHelper
    {
        public static Action GlobalTestCleanup { get; set; }
        
        internal static ISet<IHttpFeatureRegistrationPlugin> HttpFeatureRegistrationPlugins { get; }
            = new HashSet<IHttpFeatureRegistrationPlugin>();

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
                return typeActivatorCache.CreateInstance<TInstance>(TestServiceProvider.Current, typeof(TInstance));
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

        public static void SetActionContextToAccessor(ActionContext actionContext)
        {
            var actionContextAccessor = TestServiceProvider.GetService<IActionContextAccessor>();
            if (actionContextAccessor != null)
            {
                actionContextAccessor.ActionContext = actionContext;
            }
        }

        public static void ExecuteTestCleanup() => GlobalTestCleanup?.Invoke();
    }
}
