namespace MyTested.Mvc.Builders.Contracts.Data
{
    public interface IAndMemoryCacheBuilder : IMemoryCacheBuilder
    {
        IMemoryCacheBuilder AndAlso();
    }
}
