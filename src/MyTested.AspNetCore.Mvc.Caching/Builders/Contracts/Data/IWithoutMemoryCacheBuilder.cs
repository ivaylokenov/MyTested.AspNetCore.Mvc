namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Data
{
    using System.Collections.Generic;

    public interface IWithoutMemoryCacheBuilder
    {
        IAndWithoutMemoryCacheBuilder WithoutEntry(object key);

        IAndWithoutMemoryCacheBuilder WithoutEntries(IEnumerable<object> keys);

        IAndWithoutMemoryCacheBuilder ClearCache();
    }
}
