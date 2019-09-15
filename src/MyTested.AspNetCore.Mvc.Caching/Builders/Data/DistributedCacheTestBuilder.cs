namespace MyTested.AspNetCore.Mvc.Builders.Data
{
    using System.Collections.Generic;
    using Microsoft.Extensions.Caching.Distributed;
    using Builders.Contracts.Data;
    using Internal.TestContexts;

    public class DistributedCacheTestBuilder : IAndDistributedCacheTestBuilder
    {
        public DistributedCacheTestBuilder(ComponentTestContext testContext)
        {

        }

        public IDistributedCacheTestBuilder AndAlso()
        {
            throw new System.NotImplementedException();
        }

        public IAndDistributedCacheTestBuilder ContainingEntries(IDictionary<string, byte[]> entries)
        {
            throw new System.NotImplementedException();
        }

        public IAndDistributedCacheTestBuilder ContainingEntry(string key, byte[] value)
        {
            throw new System.NotImplementedException();
        }

        public IAndDistributedCacheTestBuilder ContainingEntry(string key, byte[] value, DistributedCacheEntryOptions options)
        {
            throw new System.NotImplementedException();
        }

        public IAndDistributedCacheTestBuilder ContainingEntryWithKey(string key)
        {
            throw new System.NotImplementedException();
        }

        public IAndDistributedCacheTestBuilder ContainingEntryWithValue(byte[] value)
        {
            throw new System.NotImplementedException();
        }
    }
}
