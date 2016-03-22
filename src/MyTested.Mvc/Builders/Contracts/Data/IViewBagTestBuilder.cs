namespace MyTested.Mvc.Builders.Contracts.Data
{
    public interface IViewBagTestBuilder
    {
        IAndViewBagTestBuilder ContainingEntry(string key, object value);
    }
}
