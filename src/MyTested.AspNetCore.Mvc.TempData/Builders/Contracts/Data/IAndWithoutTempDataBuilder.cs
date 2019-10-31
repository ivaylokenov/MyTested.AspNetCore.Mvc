namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Data
{
    public interface IAndWithoutTempDataBuilder : IWithoutTempDataBuilder
    {
        IWithoutTempDataBuilder AndAlso();
    }
}
