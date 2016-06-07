namespace MyTested.AspNetCore.Mvc.Internal.Http
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Http.Features;

    public class MockedSessionFeature : ISessionFeature
    {
        public ISession Session { get; set; }
    }
}
