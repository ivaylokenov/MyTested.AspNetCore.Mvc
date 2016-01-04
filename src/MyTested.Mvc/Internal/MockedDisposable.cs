namespace MyTested.Mvc.Internal
{
    using System;

    public class MockedDisposable : IDisposable
    {
        public void Dispose()
        {
            // intentionally does nothing
        }
    }
}
