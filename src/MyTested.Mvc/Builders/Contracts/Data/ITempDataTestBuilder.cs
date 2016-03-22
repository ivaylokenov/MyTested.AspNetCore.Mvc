namespace MyTested.Mvc.Builders.Contracts.Data
{
    public interface ITempDataTestBuilder
    {
        IAndTempDataTestBuilder ContainingEntry(string key, object value);
    }
}
