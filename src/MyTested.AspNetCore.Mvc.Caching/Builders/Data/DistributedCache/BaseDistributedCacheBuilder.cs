namespace MyTested.AspNetCore.Mvc.Builders.Data
{
    using System;
    using Microsoft.Extensions.Caching.Distributed;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Used for building mocked <see cref="IDistributedCache"/>.
    /// </summary>
    public abstract class BaseDistributedCacheBuilder
    {
        /// <summary>
        /// Abstract <see cref="BaseDistributedCacheBuilder"/> class.
        /// </summary>
        /// <param name="services"><see cref="IServiceProvider"/> providing the current <see cref="IDistributedCache"/>.</param>
        public BaseDistributedCacheBuilder(IServiceProvider services)
            => this.DistributedCache = services.GetRequiredService<IDistributedCache>();

        /// <summary>
        /// Gets the mocked <see cref="IDistributedCache"/>.
        /// </summary>
        /// <value>Built <see cref="IDistributedCache"/>.</value>
        protected IDistributedCache DistributedCache { get; private set; }
    }
}
