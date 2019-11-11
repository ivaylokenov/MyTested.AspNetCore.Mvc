namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Data
{
    public interface IAndWithoutDistributedCache : IWithoutDistributedCache
    {
        IWithoutDistributedCache AndAlso();
    }
}
