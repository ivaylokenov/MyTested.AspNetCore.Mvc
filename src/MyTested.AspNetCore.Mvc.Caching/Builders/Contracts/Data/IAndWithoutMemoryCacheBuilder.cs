namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Data
{
    public interface IAndWithoutMemoryCacheBuilder : IWithoutMemoryCacheBuilder
    {
        IWithoutMemoryCacheBuilder AndAlso();
    }
}
