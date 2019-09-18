namespace MyTested.AspNetCore.Mvc.Builders.Data
{
    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.Extensions.DependencyInjection;
    using System;

    /// <summary>
    /// Used for building mocked <see cref="IMemoryCache"/>.
    /// </summary>
    public abstract class MemoryCacheBaseBuilder
    {
        /// <summary>
        /// Abstract <see cref="MemoryCacheBaseBuilder"/> class.
        /// </summary>
        /// <param name="services"><see cref="IServiceProvider"/> providing the current <see cref="IMemoryCache"/>.</param>
        public MemoryCacheBaseBuilder(IServiceProvider services)
        {
            this.MemoryCache = services.GetRequiredService<IMemoryCache>();
        }

        protected IMemoryCache MemoryCache { get; private set; }
    }
}
