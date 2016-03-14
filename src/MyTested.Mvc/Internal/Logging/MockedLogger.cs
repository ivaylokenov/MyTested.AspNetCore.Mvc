namespace MyTested.Mvc.Internal.Logging
{
    using System;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Mocked ILogger.
    /// </summary>
    public class MockedLogger : ILogger
    {
        private static MockedLogger defaultMockedLogger = new MockedLogger();

        public static MockedLogger Instance => defaultMockedLogger; 

        /// <summary>
        /// Returns empty disposable object.
        /// </summary>
        /// <param name="state">Not used parameter. Used for testing with ILogger service.</param>
        /// <returns>Disposable object.</returns>
        public IDisposable BeginScopeImpl(object state)
        {
            return MockedDisposable.Instance;
        }

        /// <summary>
        /// Does nothing. Used for testing purposes.
        /// </summary>
        /// <param name="logLevel">Not used log level parameter. Used for testing with ILogger service.</param>
        /// <param name="eventId">Not used event ID parameter. Used for testing with ILogger service.</param>
        /// <param name="state">Not used state parameter. Used for testing with ILogger service.</param>
        /// <param name="exception">Not used exception parameter. Used for testing with ILogger service.</param>
        /// <param name="formatter">Not used formatter parameter. Used for testing with ILogger service.</param>
        public void Log(LogLevel logLevel, int eventId, object state, Exception exception, Func<object, Exception, string> formatter)
        {
            // intentionally does nothing
        }

        /// <summary>
        /// Does nothing. Always returns false.
        /// </summary>
        /// <param name="logLevel">Not used parameter. Used for testing with ILogger service.</param>
        /// <returns>Always false.</returns>
        public bool IsEnabled(LogLevel logLevel)
        {
            return false;
        }

        /// <summary>
        /// Does nothing. Used for testing purposes.
        /// </summary>
        /// <typeparam name="TState">Type of log state. Not used.</typeparam>
        /// <param name="logLevel">Not used log level parameter. Used for testing with ILogger service.</param>
        /// <param name="eventId">Not used event ID parameter. Used for testing with ILogger service.</param>
        /// <param name="state">Not used state parameter. Used for testing with ILogger service.</param>
        /// <param name="exception">Not used exception parameter. Used for testing with ILogger service.</param>
        /// <param name="formatter">Not used formatter parameter. Used for testing with ILogger service.</param>
        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            // intentionally does nothing
        }
    }
}
