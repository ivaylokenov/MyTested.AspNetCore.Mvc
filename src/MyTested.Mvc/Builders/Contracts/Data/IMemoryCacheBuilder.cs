namespace MyTested.Mvc.Builders.Contracts.Data
{
    public interface IMemoryCacheBuilder
    {
        IAndMemoryCacheBuilder WithEntry(object key, object value);
    }
}
