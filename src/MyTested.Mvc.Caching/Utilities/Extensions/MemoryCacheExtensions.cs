namespace MyTested.Mvc.Utilities.Extensions
{
    using System;
    using Internal.Contracts;
    using Microsoft.Extensions.Caching.Memory;

    public static class MemoryCacheExtensions
    {
        public static IMockedMemoryCache AsMockedMemoryCache(this IMemoryCache memoryCache)
        {
            var mockedMemoryCache = memoryCache as IMockedMemoryCache;
            if (mockedMemoryCache == null)
            {
                throw new InvalidOperationException("This test requires the registered IMemoryCache service to implement IMockedMemoryCache.");
            }

            return mockedMemoryCache;
        }
    }
}
