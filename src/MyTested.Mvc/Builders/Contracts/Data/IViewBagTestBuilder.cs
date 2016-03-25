namespace MyTested.Mvc.Builders.Contracts.Data
{
    using System.Collections.Generic;

    public interface IViewBagTestBuilder
    {
        IAndViewBagTestBuilder ContainingEntryWithKey(string key);

        IAndViewBagTestBuilder ContainingEntryWithValue<TEntry>(TEntry value);

        IAndViewBagTestBuilder ContainingEntryOfType<TEntry>();

        IAndViewBagTestBuilder ContainingEntryOfType<TEntry>(string key);

        IAndViewBagTestBuilder ContainingEntry(string key, object value);

        IAndViewBagTestBuilder ContainingEntries(object entries);

        IAndViewBagTestBuilder ContainingEntries(IDictionary<string, object> entries);
    }
}
