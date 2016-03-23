namespace MyTested.Mvc.Builders.Contracts.Data
{
    using System.Collections.Generic;

    public interface ISessionBuilder
    {
        IAndSessionBuilder WithId(string sessionId);

        IAndSessionBuilder WithEntry(string key, byte[] value);

        IAndSessionBuilder WithEntry(string key, string value);

        IAndSessionBuilder WithEntry(string key, int value);
        
        IAndSessionBuilder WithEntries(object entries);

        IAndSessionBuilder WithEntries(IDictionary<string, byte[]> entries);

        IAndSessionBuilder WithEntries(IDictionary<string, string> entries);
        
        IAndSessionBuilder WithEntries(IDictionary<string, int> entries);
    }
}
