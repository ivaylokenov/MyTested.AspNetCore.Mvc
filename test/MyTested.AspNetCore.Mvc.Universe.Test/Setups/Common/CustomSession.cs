namespace MyTested.AspNetCore.Mvc.Test.Setups.Common
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;

    public class CustomSession : ISession
    {
        public string Id => "Test";

        public bool IsAvailable => true;

        public IEnumerable<string> Keys => Enumerable.Empty<string>();

        public void Clear()
        {
        }

        public Task CommitAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return Task.CompletedTask;
        }

        public Task LoadAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return Task.CompletedTask;
        }

        public void Remove(string key)
        {
        }

        public void Set(string key, byte[] value)
        {
        }

        public bool TryGetValue(string key, out byte[] value)
        {
            value = null;
            return false;
        }
    }
}
