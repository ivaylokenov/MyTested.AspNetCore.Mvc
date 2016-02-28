namespace MyTested.Mvc.Builders.Contracts.Data
{
    public interface IAndMemoryCacheTestBuilder : IMemoryCacheTestBuilder
    {
        IMemoryCacheTestBuilder AndAlso();
    }
}
