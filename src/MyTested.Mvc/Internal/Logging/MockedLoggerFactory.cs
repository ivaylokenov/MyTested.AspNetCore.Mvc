namespace MyTested.Mvc.Internal.Logging
{
    using Microsoft.Extensions.Logging;

    public class MockedLoggerFactory : ILoggerFactory
    {
        public static MockedLoggerFactory Create()
        {
            return new MockedLoggerFactory(); // TODO: make singleton
        }

        public void AddProvider(ILoggerProvider provider)
        {
        }

        public ILogger CreateLogger(string categoryName)
        {
            return new MockedLogger();
        }

        public void Dispose()
        {
        }
    }
}
