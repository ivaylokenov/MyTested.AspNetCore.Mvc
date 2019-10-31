using System.Collections.Generic;

namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Data
{
    public interface IWithoutTempDataBuilder
    {
        IAndWithoutTempDataBuilder WithEntry(string key, object value);

        IAndWithoutTempDataBuilder WithEntries(IDictionary<string, object> entries);

        IAndWithoutTempDataBuilder WithEntries(object entries);

        IAndWithoutTempDataBuilder WithoutAllEntries();
    }
}
