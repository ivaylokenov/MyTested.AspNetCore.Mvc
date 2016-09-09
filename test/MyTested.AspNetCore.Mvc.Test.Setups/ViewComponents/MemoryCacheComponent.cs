namespace MyTested.AspNetCore.Mvc.Test.Setups.ViewComponents
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Memory;

    public class MemoryCacheComponent : ViewComponent
    {
        private readonly IMemoryCache memoryCache;

        public MemoryCacheComponent(IMemoryCache memoryCache)
        {
            this.memoryCache = memoryCache;
        }

        public IViewComponentResult Invoke()
        {
            var cacheValue = this.memoryCache.Get<string>("test");
            if (cacheValue == "value")
            {
                return this.View();
            }

            return this.Content("No cache");
        }
    }
}
