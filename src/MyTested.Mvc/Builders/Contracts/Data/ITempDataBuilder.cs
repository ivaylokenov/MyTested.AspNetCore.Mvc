namespace MyTested.Mvc.Builders.Contracts.Data
{
    public interface ITempDataBuilder
    {
        IAndTempDataBuilder WithEntry(string key, object value);
    }
}
