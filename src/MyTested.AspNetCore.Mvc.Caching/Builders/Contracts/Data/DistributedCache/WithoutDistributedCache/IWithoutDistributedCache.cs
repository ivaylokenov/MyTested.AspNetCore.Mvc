namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Data
{
    using System.Collections.Generic;

    public interface IWithoutDistributedCache
    {
        IAndWithoutDistributedCache WithoutEntry(object key);

        IAndWithoutDistributedCache WithoutEntries(IEnumerable<object> keys);

        IAndWithoutDistributedCache WithoutEntries(params object[] keys);

        IAndWithoutDistributedCache WithoutAllEntries();
    }
}
