namespace MyTested.AspNetCore.Mvc.Test.Setups.Controllers
{
    using Internal.Contracts;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Distributed;

    public class DistributedCacheController : Controller
    {
        public IActionResult ValidDistributedCacheAction([FromServices] IDistributedCache cache)
        {
            if (!(cache is IDistributedCacheMock))
            {
                return this.InternalServerError();
            }

            return this.Ok();
        }

        public IActionResult ValidDistributedCacheEntriesAction([FromServices] IDistributedCache cache)
        {
            if (!(cache is IDistributedCacheMock distributedCache))
            {
                return this.InternalServerError();
            }

            if (distributedCache.Count < 3)
            {
                return this.InternalServerError();
            }

            return this.Ok();
        }


        private IActionResult InternalServerError() => this.StatusCode(500);
    }
}
