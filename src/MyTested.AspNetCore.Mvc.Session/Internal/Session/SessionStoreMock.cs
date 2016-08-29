namespace MyTested.AspNetCore.Mvc.Internal.Session
{
    using System;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Session;

    public class SessionStoreMock : ISessionStore
    {
        public ISession Create(string sessionKey, TimeSpan idleTimeout, Func<bool> tryEstablishSession, bool isNewSessionKey)
        {
            return new SessionMock();
        }
    }
}
