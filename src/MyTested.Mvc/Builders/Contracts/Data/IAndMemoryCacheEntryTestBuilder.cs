namespace MyTested.Mvc.Builders.Contracts.Data
{
    public interface IAndMemoryCacheEntryTestBuilder : IMemoryCacheEntryTestBuilder
    {
        IMemoryCacheEntryTestBuilder AndAlso();
    }
}
