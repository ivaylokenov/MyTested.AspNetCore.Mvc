namespace MyTested.Mvc.Builders.Contracts.Data
{
    public interface IAndSessionBuilder : ISessionBuilder
    {
        ISessionBuilder AndAlso();
    }
}
