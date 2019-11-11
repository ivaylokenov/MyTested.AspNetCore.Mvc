namespace MyTested.AspNetCore.Mvc.Builders.Data.DistributedCache.WithoutDistributedCache
{
    using System;
    using Microsoft.Extensions.Caching.Distributed;

    public class WithoutDistributedCacheBuilder : BaseDistributedCacheBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WithoutDistributedCacheBuilder"/> class.
        /// </summary>
        /// <param name="services"><see cref="IServiceProvider"/> providing the current <see cref="IDistributedCache"/>.</param>
        public WithoutDistributedCacheBuilder(IServiceProvider services)
            : base(services)
        {
        }
    }
}
