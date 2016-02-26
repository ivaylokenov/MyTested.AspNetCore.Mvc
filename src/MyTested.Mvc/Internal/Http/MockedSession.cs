namespace MyTested.Mvc.Internal.Http
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http.Features;
    using Microsoft.AspNetCore.Mvc.Internal;

    public class MockedSession : ISession
    {
        private readonly IDictionary<string, byte[]> session;

        public MockedSession()
        {
            this.session = new Dictionary<string, byte[]>();
        }

        public IEnumerable<string> Keys => this.session.Keys;

        public void Clear() => this.session.Clear();

        public Task CommitAsync() => TaskCache.CompletedTask;

        public Task LoadAsync() => TaskCache.CompletedTask;

        public void Remove(string key)
        {
            if (this.session.ContainsKey(key))
            {
                this.session.Remove(key);
            }
        }

        public void Set(string key, byte[] value)
        {
            this.session[key] = value;
        }

        public bool TryGetValue(string key, out byte[] value)
        {
            if (this.session.ContainsKey(key))
            {
                value = this.session[key];
                return true;
            }

            value = null;
            return false;
        }
    }
}
