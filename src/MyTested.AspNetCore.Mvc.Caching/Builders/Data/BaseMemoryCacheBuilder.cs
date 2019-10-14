namespace MyTested.AspNetCore.Mvc.Builders.Data
{
    using System;
    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Used for building mocked <see cref="IMemoryCache"/>.
    /// </summary>
    public abstract class BaseMemoryCacheBuilder
    {
        /// <summary>
        /// Abstract <see cref="BaseMemoryCacheBuilder"/> class.
        /// </summary>
        /// <param name="services"><see cref="IServiceProvider"/> providing the current <see cref="IMemoryCache"/>.</param>
        public BaseMemoryCacheBuilder(IServiceProvider services)
            => this.MemoryCache = services.GetRequiredService<IMemoryCache>();

        protected IMemoryCache MemoryCache { get; private set; }
    }
}
