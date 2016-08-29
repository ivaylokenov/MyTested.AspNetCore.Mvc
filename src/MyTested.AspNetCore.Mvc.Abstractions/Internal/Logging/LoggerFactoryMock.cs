namespace MyTested.AspNetCore.Mvc.Internal.Logging
{
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Mock of <see cref="ILoggerFactory"/>.
    /// </summary>
    public class LoggerFactoryMock : ILoggerFactory
    {
        private static LoggerFactoryMock defaultLoggerFactoryMock = new LoggerFactoryMock();

        /// <summary>
        /// Created mocked logger factory.
        /// </summary>
        /// <returns>Mock of logger factory.</returns>
        public static LoggerFactoryMock Create() => defaultLoggerFactoryMock;

        /// <summary>
        /// Does nothing. Used for testing purposes.
        /// </summary>
        /// <param name="provider">Not used provider parameter. Used for testing with <see cref="ILogger"/> service.</param>
        public void AddProvider(ILoggerProvider provider)
        {
            // intentionally does nothing
        }

        /// <summary>
        /// Creates new mock of logger.
        /// </summary>
        /// <param name="categoryName">Not used category name parameter. This method is used for testing with <see cref="ILogger"/> service.</param>
        /// <returns>Mock of <see cref="ILogger"/>.</returns>
        public ILogger CreateLogger(string categoryName)
        {
            return LoggerMock.Instance;
        }

        /// <summary>
        /// Does nothing. Used for testing with ILogger service.
        /// </summary>
        public void Dispose()
        {
            // intentionally does nothing
        }
    }
}
