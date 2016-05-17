namespace MyTested.Mvc.Test.Setups.Common
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc.Internal;

    public class CustomSession : ISession
    {
        public string Id => "Test";

        public IEnumerable<string> Keys => Enumerable.Empty<string>();

        public void Clear()
        {
        }

        public Task CommitAsync()
        {
            return TaskCache.CompletedTask;
        }

        public Task LoadAsync()
        {
            return TaskCache.CompletedTask;
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
