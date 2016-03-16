namespace MyTested.Mvc.Builders.Contracts.Data
{
    public interface IMemoryCacheTestBuilder
    {
        IAndMemoryCacheTestBuilder ContainingEntry(object key, object value);
    }
}
