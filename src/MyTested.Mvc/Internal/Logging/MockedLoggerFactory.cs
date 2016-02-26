namespace MyTested.Mvc.Internal.Logging
{
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Mocked ILoggerFactory.
    /// </summary>
    public class MockedLoggerFactory : ILoggerFactory
    {
        /// <summary>
        /// Created mocked logger factory.
        /// </summary>
        /// <returns>Mocked logger factory.</returns>
        public static MockedLoggerFactory Create()
        {
            return new MockedLoggerFactory();
        }

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
            return new MockedLogger();
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
