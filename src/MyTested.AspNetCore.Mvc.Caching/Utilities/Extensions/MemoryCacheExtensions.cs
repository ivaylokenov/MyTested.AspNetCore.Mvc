namespace MyTested.AspNetCore.Mvc.Utilities.Extensions
{
    using System;
    using Internal.Contracts;
    using Microsoft.Extensions.Caching.Memory;

    public static class MemoryCacheExtensions
    {
        public static IMemoryCacheMock AsMemoryCacheMock(this IMemoryCache memoryCache)
        {
            var mockedMemoryCache = memoryCache as IMemoryCacheMock;
            if (mockedMemoryCache == null)
            {
                throw new InvalidOperationException("This test requires the registered IMemoryCache service to implement IMemoryCacheMock.");
            }

            return mockedMemoryCache;
        }
    }
}
