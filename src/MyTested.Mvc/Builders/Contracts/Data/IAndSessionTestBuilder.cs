namespace MyTested.Mvc.Builders.Contracts.Data
{
    public interface IAndSessionTestBuilder : ISessionTestBuilder
    {
        ISessionTestBuilder AndAlso();
    }
}
