namespace MyTested.AspNetCore.Mvc.Test.Setups.Common
{
    using System.Collections;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Http;

    public class CustomRequestCookieCollection : IRequestCookieCollection
    {
        private IDictionary<string, string> store;

        public CustomRequestCookieCollection() 
            => this.store = new Dictionary<string, string>();

        public string this[string key]
        {
            get => this.store[key];
            set => this.store[key] = value;
        }

        public int Count => this.store.Count;

        public ICollection<string> Keys => this.store.Keys;

        public bool ContainsKey(string key) => this.store.ContainsKey(key);

        public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
            => this.store.GetEnumerator();

        public bool TryGetValue(string key, out string value)
            => this.store.TryGetValue(key, out value);

        IEnumerator IEnumerable.GetEnumerator() => this.store.GetEnumerator();
    }
}
