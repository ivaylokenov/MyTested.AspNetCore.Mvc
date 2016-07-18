namespace MyTested.AspNetCore.Mvc.Internal.Logging
{
    using System;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Mocked <see cref="ILogger"/>.
    /// </summary>
    public class MockedLogger : ILogger
    {
        public static MockedLogger Instance { get; } = new MockedLogger();

        /// <summary>
        /// Returns empty <see cref="IDisposable"/> object.
        /// </summary>
        /// <typeparam name="TState">Not used type argument. Used for testing with <see cref="ILogger"/> service.</typeparam>
        /// <param name="state">Not used parameter. Used for testing with <see cref="ILogger"/> service.</param>
        /// <returns>Disposable object.</returns>
        public IDisposable BeginScope<TState>(TState state)
        {
            return MockedDisposable.Instance;
        }

        /// <summary>
        /// Does nothing. Always returns false.
        /// </summary>
        /// <param name="logLevel">Not used parameter. Used for testing with <see cref="ILogger"/> service.</param>
        /// <returns>Always false.</returns>
        public bool IsEnabled(LogLevel logLevel)
        {
            return false;
        }

        /// <summary>
        /// Does nothing. Used for testing purposes.
        /// </summary>
        /// <typeparam name="TState">Type of log state. Not used.</typeparam>
        /// <param name="logLevel">Not used log level parameter. Used for testing with <see cref="ILogger"/> service.</param>
        /// <param name="eventId">Not used event ID parameter. Used for testing with <see cref="ILogger"/> service.</param>
        /// <param name="state">Not used state parameter. Used for testing with <see cref="ILogger"/> service.</param>
        /// <param name="exception">Not used exception parameter. Used for testing with <see cref="ILogger"/> service.</param>
        /// <param name="formatter">Not used formatter parameter. Used for testing with <see cref="ILogger"/> service.</param>
        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            // intentionally does nothing
        }
    }
}
