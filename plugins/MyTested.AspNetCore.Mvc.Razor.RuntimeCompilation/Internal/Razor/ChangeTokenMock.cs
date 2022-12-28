namespace MyTested.AspNetCore.Mvc.Internal.Razor
{
    using Microsoft.Extensions.Primitives;
    using System;

    public class ChangeTokenMock : IChangeToken
    {
        public static ChangeTokenMock Instance => new ChangeTokenMock();

        public bool ActiveChangeCallbacks => false;

        public bool HasChanged => false;

        public IDisposable RegisterChangeCallback(Action<object> callback, object state) => DisposableMock.Instance;
    }
}
