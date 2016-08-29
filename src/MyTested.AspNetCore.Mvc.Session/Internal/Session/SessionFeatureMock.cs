namespace MyTested.AspNetCore.Mvc.Internal.Session
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Http.Features;

    public class SessionFeatureMock : ISessionFeature
    {
        public ISession Session { get; set; }
    }
}
