namespace MyTested.AspNetCore.Mvc.Internal.Logging
{
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Mocked ILoggerFactory.
    /// </summary>
    public class MockedLoggerFactory : ILoggerFactory
    {
        private static MockedLoggerFactory defaultMockedLoggerFactory = new MockedLoggerFactory();

        /// <summary>
        /// Created mocked logger factory.
        /// </summary>
        /// <returns>Mocked logger factory.</returns>
        public static MockedLoggerFactory Create() => defaultMockedLoggerFactory;

        /// <summary>
        /// Does nothing. Used for testing purposes.
        /// </summary>
        /// <param name="provider">Not used provider parameter. Used for testing with ILogger service.</param>
        public void AddProvider(ILoggerProvider provider)
        {
            // intentionally does nothing
        }

        /// <summary>
        /// Creates new mocked logger.
        /// </summary>
        /// <param name="categoryName">Not used category name parameter. Used for testing with ILogger service.</param>
        /// <returns>Mocked ILogger.</returns>
        public ILogger CreateLogger(string categoryName)
        {
            return MockedLogger.Instance;
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
