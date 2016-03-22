namespace MyTested.Mvc.Builders.Contracts.Data
{
    using System.Collections.Generic;

    public interface ISessionBuilder
    {
        IAndSessionBuilder WithId(string sessionId);

        IAndSessionBuilder WithEntry(string key, byte[] value);

        IAndSessionBuilder WithStringEntry(string key, string value);

        IAndSessionBuilder WithIntegerEntry(string key, int value);
        
        IAndSessionBuilder WithEntries(object entries);

        IAndSessionBuilder WithEntries(IDictionary<string, byte[]> entries);

        IAndSessionBuilder WithStringEntries(IDictionary<string, string> entries);
        
        IAndSessionBuilder WithIntegerEntries(IDictionary<string, int> entries);
    }
}
