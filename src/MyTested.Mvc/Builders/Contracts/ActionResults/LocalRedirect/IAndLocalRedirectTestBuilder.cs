namespace MyTested.Mvc.Builders.Contracts.ActionResults.LocalRedirect
{
    public interface IAndLocalRedirectTestBuilder : ILocalRedirectTestBuilder
    {
        ILocalRedirectTestBuilder AndAlso();
    }
}
