namespace MyTested.Mvc.Builders.Contracts.Data
{
    public interface ISessionBuilder
    {
        IAndSessionBuilder WithStringEntry(string key, string value);
    }
}
