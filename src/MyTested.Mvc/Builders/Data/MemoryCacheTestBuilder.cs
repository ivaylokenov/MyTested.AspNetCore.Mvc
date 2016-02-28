namespace MyTested.Mvc.Builders.Data
{
    using Contracts.Data;
    using Microsoft.Extensions.Caching.Memory;    
    
    public class MemoryCacheTestBuilder : IAndMemoryCacheTestBuilder
    {
        private IMemoryCache memoryCache;

        public MemoryCacheTestBuilder(IMemoryCache memoryCache)
        {
            this.memoryCache = memoryCache;
        }

        public IMemoryCacheTestBuilder AndAlso()
        {
            return this;
        }
    }
}
