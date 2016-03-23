namespace MyTested.Mvc.Builders.Contracts.Data
{
    using System.Collections.Generic;

    public interface ITempDataBuilder
    {
        IAndTempDataBuilder WithEntry(string key, object value);

        IAndTempDataBuilder WithEntries(IDictionary<string, object> entries);

        IAndTempDataBuilder WithEntries(object entries);
    }
}
