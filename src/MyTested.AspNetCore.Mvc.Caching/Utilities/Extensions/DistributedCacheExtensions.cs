namespace MyTested.AspNetCore.Mvc.Utilities.Extensions
{
    using System;
    using Microsoft.Extensions.Caching.Distributed;
    using Internal.Contracts;

    public static class DistributedCacheExtensions
    {
        public static IDistributedCacheMock AsDistributedCacheMock(this IDistributedCache distributedCache)
        {
            if(!(distributedCache is IDistributedCacheMock distributedCacheMock))
            {
                throw new InvalidOperationException("This test requires the registered IDistributedCache service to implement IDistributedCacheMock.");
            }

            return distributedCacheMock;
        }
    }
}
