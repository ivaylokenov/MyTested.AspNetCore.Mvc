namespace MyTested.AspNetCore.Mvc.Builders.Data
{
    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.Extensions.DependencyInjection;
    using System;

    public abstract class MemoryCacheBaseBuilder
    {
        public MemoryCacheBaseBuilder(IServiceProvider services)
        {
            this.MemoryCache = services.GetRequiredService<IMemoryCache>();
        }

        protected IMemoryCache MemoryCache { get; private set; }
    }
}
