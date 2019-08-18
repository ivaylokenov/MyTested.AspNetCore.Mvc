namespace MyTested.AspNetCore.Mvc.Internal
{
    using System;

    /// <summary>
    /// Mock of <see cref="IDisposable"/> object.
    /// </summary>
    public class DisposableMock : IDisposable
    {
        public static DisposableMock Instance => new DisposableMock();

        /// <summary>
        /// Does nothing.
        /// </summary>
        public void Dispose()
        {
            // Intentionally does nothing.
        }
    }
}
