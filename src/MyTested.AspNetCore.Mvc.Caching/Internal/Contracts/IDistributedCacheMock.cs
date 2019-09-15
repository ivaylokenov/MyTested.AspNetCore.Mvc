namespace MyTested.AspNetCore.Mvc.Internal.Contracts
{
    using Microsoft.Extensions.Caching.Distributed;

    public interface IDistributedCacheMock : IDistributedCache
    {
        int Count { get; }
    }
}
