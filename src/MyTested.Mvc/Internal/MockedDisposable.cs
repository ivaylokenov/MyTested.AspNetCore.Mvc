namespace MyTested.Mvc.Internal
{
    using System;

    /// <summary>
    /// Mocked IDisposable object.
    /// </summary>
    public class MockedDisposable : IDisposable
    {
        /// <summary>
        /// Does nothing.
        /// </summary>
        public void Dispose()
        {
            // intentionally does nothing
        }
    }
}
