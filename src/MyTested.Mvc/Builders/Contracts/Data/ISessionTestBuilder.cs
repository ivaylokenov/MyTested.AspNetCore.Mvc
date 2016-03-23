namespace MyTested.Mvc.Builders.Contracts.Data
{
    using System.Collections.Generic;

    public interface ISessionTestBuilder
    {
        IAndSessionTestBuilder ContainingEntryWithKey(string key);

        IAndSessionTestBuilder ContainingEntryWithValue(byte[] value);

        IAndSessionTestBuilder ContainingEntryWithValue(string value);

        IAndSessionTestBuilder ContainingEntryWithValue(int value);

        IAndSessionTestBuilder ContainingEntry(string key, byte[] value);

        IAndSessionTestBuilder ContainingEntry(string key, string value);

        IAndSessionTestBuilder ContainingEntry(string key, int value);

        IAndSessionTestBuilder ContainingEntries(object entries);
        
        IAndSessionTestBuilder ContainingEntries(IDictionary<string, byte[]> entries);

        IAndSessionTestBuilder ContainingEntries(IDictionary<string, object> entries);
        
        IAndSessionTestBuilder ContainingEntries(IDictionary<string, string> entries);
        
        IAndSessionTestBuilder ContainingEntries(IDictionary<string, int> entries);
    }
}
