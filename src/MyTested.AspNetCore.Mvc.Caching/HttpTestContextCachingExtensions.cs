namespace MyTested.AspNetCore.Mvc
{
    using Internal.Contracts;
    using Internal.TestContexts;
    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.Extensions.DependencyInjection;
    using Utilities.Extensions;

    public static class HttpTestContextCachingExtensions
    {
        public static IMemoryCache GetMemoryCache(this HttpTestContext testContext)
            => testContext.HttpContext.RequestServices.GetRequiredService<IMemoryCache>();

        internal static IMockedMemoryCache GetMockedMemoryCache(this HttpTestContext testContext)
            => testContext.GetMemoryCache().AsMockedMemoryCache();
    }
}
