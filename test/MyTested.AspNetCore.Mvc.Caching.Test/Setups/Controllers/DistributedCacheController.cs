namespace MyTested.AspNetCore.Mvc.Test.Setups.Controllers
{
    using System;
    using System.Collections.Generic;
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

        public IActionResult ValidDistributedCacheEntryAction([FromServices] IDistributedCache cache)
        {
            if (!(cache is IDistributedCacheMock distributedCache))
            {
                return this.InternalServerError();
            }

            if (distributedCache.Count != 1)
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

        public IActionResult GetCount([FromServices] IDistributedCache cache)
        {
            return this.Ok((cache as IDistributedCacheMock).GetCacheAsDictionary().Count);
        }

        public IActionResult GetAllEntities([FromServices] IDistributedCache cache)
        {
            return this.Ok((new SortedDictionary<string, byte[]>((cache as IDistributedCacheMock).GetCacheAsDictionary())));
        }

        private IActionResult InternalServerError() => this.StatusCode(500);
    }
}
