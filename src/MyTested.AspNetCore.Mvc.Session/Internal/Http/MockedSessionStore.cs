namespace MyTested.AspNetCore.Mvc.Internal.Http
{
    using System;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Session;

    public class MockedSessionStore : ISessionStore
    {
        public ISession Create(string sessionKey, TimeSpan idleTimeout, Func<bool> tryEstablishSession, bool isNewSessionKey)
        {
            return new MockedSession();
        }
    }
}
