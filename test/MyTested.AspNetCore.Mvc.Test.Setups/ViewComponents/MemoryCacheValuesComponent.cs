namespace MyTested.AspNetCore.Mvc.Test.Setups.ViewComponents
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.Extensions.DependencyInjection;
    using System;

    public class MemoryCacheValuesComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var memoryCache = this.HttpContext.RequestServices.GetService<IMemoryCache>();
            memoryCache.Set("test", "value", new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = new DateTimeOffset(new DateTime(2016, 1, 1, 1, 1, 1, DateTimeKind.Utc)),
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1),
                Priority = CacheItemPriority.High,
                SlidingExpiration = TimeSpan.FromMinutes(5)
            });

            memoryCache.Set("another", "anotherValue");

            return this.View();
        }
    }
}
