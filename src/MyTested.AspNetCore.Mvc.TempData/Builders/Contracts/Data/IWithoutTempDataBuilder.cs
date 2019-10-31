using System.Collections.Generic;

namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Data
{
    public interface IWithoutTempDataBuilder
    {
        IAndWithoutTempDataBuilder WithoutEntry(string key);

        IAndWithoutTempDataBuilder WithoutEntries(IEnumerable<string> entriesKeys);

        IAndWithoutTempDataBuilder WithoutAllEntries();
    }
}
