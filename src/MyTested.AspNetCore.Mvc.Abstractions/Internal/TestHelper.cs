namespace MyTested.AspNetCore.Mvc.Internal
{
    using System;
    using System.Collections.Generic;
    using Http;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Http.Features;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Infrastructure;
    using Plugins;
    using Services;
    using TestContexts;
    using Utilities;

    public static class TestHelper
    {
        public static Action GlobalTestCleanup { get; set; }

        public static IFeatureCollection DefaultHttpFeatures { get; set; } = new FeatureCollection();

        public static ISet<IHttpFeatureRegistrationPlugin> HttpFeatureRegistrationPlugins { get; }
            = new HashSet<IHttpFeatureRegistrationPlugin>();

        public static ISet<IShouldPassForPlugin> ShouldPassForPlugins { get; }
            = new HashSet<IShouldPassForPlugin>();
        
        public static HttpContextMock CreateHttpContextMock()
        {
            var httpContextFactory = TestServiceProvider.GetService<IHttpContextFactory>();
            var httpContext = httpContextFactory != null
                ? HttpContextMock.From(httpContextFactory
                    .Create(new FeatureCollection(DefaultHttpFeatures)))
                : new HttpContextMock();

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
                var typeActivatorCacheType = WebFramework.Internals.TypeActivatorCache;
                var openGenericCreateInstance = typeActivatorCacheType.GetMethod("CreateInstance");
                var closedGenericCreateInstance = openGenericCreateInstance.MakeGenericMethod(typeof(TInstance));
                var typeActivatorCache = TestServiceProvider.GetService(typeActivatorCacheType);
                var createInstanceDelegate = (Func<IServiceProvider, Type, TInstance>)closedGenericCreateInstance.CreateDelegate(typeof(Func<IServiceProvider, Type, TInstance>), typeActivatorCache);

                return createInstanceDelegate(TestServiceProvider.Current, typeof(TInstance));
            }
            catch
            {
                return null;
            }
        }

        public static TComponent TryGetShouldPassForValue<TComponent>(ComponentTestContext testContext)
            where TComponent : class
        {
            var result = testContext.ComponentAs<TComponent>()
                ?? testContext.MethodResultAs<TComponent>()
                ?? testContext.ModelAs<TComponent>();
            
            if (result != null)
            {
                return result;
            }

            foreach (var shouldPassForPlugin in ShouldPassForPlugins)
            {
                result = shouldPassForPlugin.TryGetValue(typeof(TComponent), testContext) as TComponent;

                if (result != null)
                {
                    return result;
                }
            }

            throw new InvalidOperationException($"{typeof(TComponent).ToFriendlyTypeName()} could not be resolved for the 'ShouldPassForThe<TComponent>' method call.");
        }

        public static void ExecuteTestCleanup() => GlobalTestCleanup?.Invoke();

        /// <summary>
        /// Gets formatted friendly name.
        /// </summary>
        /// <param name="name">Name to format.</param>
        /// <returns>Formatted string.</returns>
        public static string GetFriendlyName(string name) 
            => name == null ? "the default one" : $"'{name}'";
    }
}
