namespace MyTested.Mvc.Builders.Contracts.Data
{
    public interface IAndTempDataBuilder : ITempDataBuilder
    {
        ITempDataBuilder AndAlso();
    }
}
