namespace MyTested.Mvc.Test.Setups.Common
{
    using System;
    using Microsoft.AspNetCore.Http.Features;
    using Microsoft.AspNetCore.Session;

    public class CustomSessionStore : ISessionStore
    {
        public bool IsAvailable => true;

        public ISession Create(string sessionKey, TimeSpan idleTimeout, Func<bool> tryEstablishSession, bool isNewSessionKey)
        {
            return new CustomSession();
        }
    }
}
