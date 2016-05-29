namespace MyTested.Mvc.Internal
{
    using System;

    /// <summary>
    /// Mocked IDisposable object.
    /// </summary>
    public class MockedDisposable : IDisposable
    {
        private static MockedDisposable defaultMockedDisposable = new MockedDisposable();

        public static MockedDisposable Instance => defaultMockedDisposable;

        /// <summary>
        /// Does nothing.
        /// </summary>
        public void Dispose()
        {
            // intentionally does nothing
        }
    }
}
