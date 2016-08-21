namespace MyTested.AspNetCore.Mvc.Internal
{
    using System;
    using System.Collections.Generic;
    using Http;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Http.Features;
    using Plugins;
    using Services;
    using TestContexts;
    using Utilities;

    public static class TestHelper
    {
        public static Action GlobalTestCleanup { get; set; }
        
        public static ISet<IHttpFeatureRegistrationPlugin> HttpFeatureRegistrationPlugins { get; }
            = new HashSet<IHttpFeatureRegistrationPlugin>();

        public static ISet<IShouldPassForPlugin> ShouldPassForPlugins { get; }
            = new HashSet<IShouldPassForPlugin>();
        
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

        public static TComponent TryGetShouldPassForValue<TComponent>(ComponentTestContext testContext)
            where TComponent : class
        {
            var result = testContext.ComponentAs<TComponent>()
                ?? testContext.MethodResultAs<TComponent>()
                ?? testContext.ModelAs<TComponent>();

            foreach (var shouldPassForPlugin in ShouldPassForPlugins)
            {
                if (result != null)
                {
                    return result;
                }

                result = shouldPassForPlugin.TryGetValue(typeof(TComponent), testContext) as TComponent;
            }

            throw new InvalidOperationException($"{typeof(TComponent).ToFriendlyTypeName()} could not be resolved for the 'ShouldPassForThe<TComponent>' method call.");
        }

        public static void ExecuteTestCleanup() => GlobalTestCleanup?.Invoke();
    }
}
