namespace MyTested.Mvc.Builders.Contracts.Data
{
    public interface IViewDataTestBuilder
    {
        IAndViewDataTestBuilder ContainingEntry(string key, object value);
    }
}
