namespace MyTested.Mvc.Builders.Contracts.Data
{
    public interface IAndTempDataTestBuilder : ITempDataTestBuilder
    {
        ITempDataTestBuilder AndAlso();
    }
}
