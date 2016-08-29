namespace MyTested.AspNetCore.Mvc.Internal
{
    using System;

    /// <summary>
    /// Mock of <see cref="IDisposable"/> object.
    /// </summary>
    public class DisposableMock : IDisposable
    {
        private static DisposableMock defaultDisposableMock = new DisposableMock();

        public static DisposableMock Instance => defaultDisposableMock;

        /// <summary>
        /// Does nothing.
        /// </summary>
        public void Dispose()
        {
            // intentionally does nothing
        }
    }
}
