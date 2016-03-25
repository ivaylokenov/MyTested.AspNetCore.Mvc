namespace MyTested.Mvc.Builders.Contracts.Data
{
    using System.Collections.Generic;

    public interface IViewDataTestBuilder
    {
        IAndViewDataTestBuilder ContainingEntryWithKey(string key);

        IAndViewDataTestBuilder ContainingEntryWithValue<TEntry>(TEntry value);

        IAndViewDataTestBuilder ContainingEntryOfType<TEntry>();

        IAndViewDataTestBuilder ContainingEntryOfType<TEntry>(string key);

        IAndViewDataTestBuilder ContainingEntry(string key, object value);

        IAndViewDataTestBuilder ContainingEntries(object entries);

        IAndViewDataTestBuilder ContainingEntries(IDictionary<string, object> entries);
    }
}
