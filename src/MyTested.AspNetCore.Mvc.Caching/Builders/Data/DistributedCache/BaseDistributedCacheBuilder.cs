namespace MyTested.AspNetCore.Mvc.Builders.Data
{
    using System;
    using Microsoft.Extensions.Caching.Distributed;
    using Microsoft.Extensions.DependencyInjection;

    public abstract class BaseDistributedCacheBuilder
    {
        public BaseDistributedCacheBuilder(IServiceProvider services)
            => this.DistributedCache = services.GetRequiredService<IDistributedCache>();

        /// <summary>
        /// Gets the mocked <see cref="IDistributedCache"/>.
        /// </summary>
        /// <value>Built <see cref="IDistributedCache"/>.</value>
        protected IDistributedCache DistributedCache { get; private set; }
    }
}
