namespace MyTested.Mvc.Builders.Contracts.Data
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Extensions.Caching.Memory;

    public interface IMemoryCacheTestBuilder
    {
        IAndMemoryCacheTestBuilder ContainingEntryWithKey(object key);

        IAndMemoryCacheTestBuilder ContainingEntryWithValue<TEntry>(TEntry value);

        IAndMemoryCacheTestBuilder ContainingEntryOfType<TEntry>();

        IAndMemoryCacheTestBuilder ContainingEntry(object key, object value);
        
        IAndMemoryCacheTestBuilder ContainingEntryOfType<TEntry>(object key);

        IAndMemoryCacheTestBuilder ContainingEntry(object key, object value, MemoryCacheEntryOptions options);

        IAndMemoryCacheTestBuilder ContainingEntry(Action<IMemoryCacheEntryTestBuilder> memoryCacheEntryTestBuilder);

        IAndMemoryCacheTestBuilder ContainingEntries(IDictionary<object, object> entries);
    }
}
