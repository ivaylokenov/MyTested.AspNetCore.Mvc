namespace MyTested.Mvc.Builders.Contracts.Http
{
    public interface IAndResponseCookieTestBuilder : IResponseCookieTestBuilder
    {
        IResponseCookieTestBuilder AndAlso();
    }
}
