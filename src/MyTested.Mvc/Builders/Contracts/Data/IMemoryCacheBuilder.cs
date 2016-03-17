namespace MyTested.Mvc.Builders.Contracts.Data
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Extensions.Caching.Memory;

    public interface IMemoryCacheBuilder
    {
        IAndMemoryCacheBuilder WithEntry(object key, object value);

        IAndMemoryCacheBuilder WithEntry(object key, object value, MemoryCacheEntryOptions options);

        IAndMemoryCacheBuilder WithEntry(Action<IMemoryCacheEntryTestBuilder> memoryCacheEntryBuilder);

        IAndMemoryCacheBuilder WithEntries(IDictionary<object, object> entries);
    }
}
