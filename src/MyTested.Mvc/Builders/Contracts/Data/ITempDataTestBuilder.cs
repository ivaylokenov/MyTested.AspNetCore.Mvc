namespace MyTested.Mvc.Builders.Contracts.Data
{
    using System.Collections.Generic;

    public interface ITempDataTestBuilder
    {
        IAndTempDataTestBuilder ContainingEntryWithKey(string key);

        IAndTempDataTestBuilder ContainingEntryWithValue<TEntry>(TEntry value);

        IAndTempDataTestBuilder ContainingEntryOfType<TEntry>();

        IAndTempDataTestBuilder ContainingEntryOfType<TEntry>(string key);

        IAndTempDataTestBuilder ContainingEntry(string key, object value);

        IAndTempDataTestBuilder ContainingEntries(object entries);

        IAndTempDataTestBuilder ContainingEntries(IDictionary<string, object> entries);
    }
}
