namespace MyTested.Mvc.Builders.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using Contracts.Data;
    using Internal.TestContexts;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Routing;

    public class SessionTestBuilder : BaseDataProviderTestBuilder, IAndSessionTestBuilder
    {
        internal const string SessionName = "session";

        public SessionTestBuilder(ControllerTestContext testContext)
            : base(testContext, SessionName)
        {
        }

        public IAndSessionTestBuilder ContainingEntryWithKey(string key)
        {
            this.ValidateContainingEntryWithKey(key);
            return this;
        }

        public IAndSessionTestBuilder ContainingEntry(string key, byte[] value)
        {
            this.ValidateContainingEntry(key, value);
            return this;
        }

        public IAndSessionTestBuilder ContainingStringEntry(string key, string value)
        {
            this.ValidateContainingEntry(key, value);
            return this;
        }

        public IAndSessionTestBuilder ContainingIntegerEntry(string key, int value)
        {
            var bytes = new byte[]
            {
                (byte)(value >> 24),
                (byte)(0xFF & (value >> 16)),
                (byte)(0xFF & (value >> 8)),
                (byte)(0xFF & value)
            };

            this.ValidateContainingEntry(key, bytes);
            return this;
        }

        public IAndSessionTestBuilder ContainingEntries(object entries)
            => this.ContainingEntries(new RouteValueDictionary(entries));

        public IAndSessionTestBuilder ContainingEntries(IDictionary<string, byte[]> entries)
        {
            return this.ContainingEntries<byte[]>(entries);
        }

        public IAndSessionTestBuilder ContainingEntries(IDictionary<string, object> entries)
        {
            return this.ContainingEntries<object>(entries);
        }

        public IAndSessionTestBuilder ContainingStringEntries(IDictionary<string, string> entries)
        {
            return this.ContainingEntries(entries);
        }

        public IAndSessionTestBuilder ContainingIntegerEntries(IDictionary<string, int> entries)
        {
            return this.ContainingEntries(entries);
        }

        public ISessionTestBuilder AndAlso() => this;

        protected override IDictionary<string, object> GetDataProvider()
        {
            var result = new Dictionary<string, object>();
            var session = this.TestContext.HttpContext.Session;

            foreach (var key in session.Keys)
            {
                result.Add(key, session.Get(key));
            }

            return result;
        }

        private IAndSessionTestBuilder ContainingEntries<T>(IDictionary<string, T> entries)
        {
            this.ValidateContainingEntries(entries.ToDictionary(e => e.Key, e => (object)e.Value));
            return this;
        }
    }
}
