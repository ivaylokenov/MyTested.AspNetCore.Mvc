namespace MyTested.Mvc.Internal.Logging
{
    using System;
    using Microsoft.Extensions.Logging;

    public class MockedLogger : ILogger
    {
        public IDisposable BeginScopeImpl(object state)
        {
            return new MockedDisposable();
        }

        public void Log(LogLevel logLevel, int eventId, object state, Exception exception, Func<object, Exception, string> formatter)
        {
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return false;
        }
    }
}
