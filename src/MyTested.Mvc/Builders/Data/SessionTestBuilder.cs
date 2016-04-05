namespace MyTested.Mvc.Builders.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Contracts.Data;
    using Internal.TestContexts;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Routing;
    using Utilities.Validators;

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

        public IAndSessionTestBuilder ContainingEntryWithValue(byte[] value)
        {
            this.ValidateContainingEntryWithValue(value);
            return this;
        }

        public IAndSessionTestBuilder ContainingEntryWithValue(string value)
        {
            this.ValidateContainingEntryWithValue(ConvertStringToByteArray(value));
            return this;
        }

        public IAndSessionTestBuilder ContainingEntryWithValue(int value)
        {
            this.ValidateContainingEntryWithValue(ConvertIntegerToByteArray(value));
            return this;
        }

        public IAndSessionTestBuilder ContainingEntry(string key, byte[] value)
        {
            this.ValidateContainingEntry(key, value);
            return this;
        }

        public IAndSessionTestBuilder ContainingEntry(string key, string value)
        {
            this.ValidateContainingEntry(key, ConvertStringToByteArray(value));
            return this;
        }

        public IAndSessionTestBuilder ContainingEntry(string key, int value)
        {
            this.ValidateContainingEntry(key, ConvertIntegerToByteArray(value));
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
            return this.ContainingEntries<object>(entries, true);
        }

        public IAndSessionTestBuilder ContainingEntries(IDictionary<string, string> entries)
        {
            return this.ContainingEntries<string>(entries);
        }

        public IAndSessionTestBuilder ContainingEntries(IDictionary<string, int> entries)
        {
            return this.ContainingEntries<int>(entries);
        }

        public ISessionTestBuilder AndAlso() => this;

        protected override IDictionary<string, object> GetDataProvider()
        {
            var result = new Dictionary<string, object>();
            var session = this.HttpContext.Session;

            foreach (var key in session.Keys)
            {
                result.Add(key, session.Get(key));
            }

            return result;
        }

        private static byte[] ConvertStringToByteArray(string value)
        {
            return Encoding.UTF8.GetBytes(value);
        }

        private static byte[] ConvertIntegerToByteArray(int value)
        {
            return new byte[]
            {
                (byte)(value >> 24),
                (byte)(0xFF & (value >> 16)),
                (byte)(0xFF & (value >> 8)),
                (byte)(0xFF & value)
            };
        }

        private static object ConvertToByteEntry<T>(KeyValuePair<string, T> entry)
        {
            var typeOfValue = entry.Value?.GetType();
            object value = entry.Value;

            if (typeOfValue == typeof(string))
            {
                return ConvertStringToByteArray(entry.Value as string);
            }
            else if (typeOfValue == typeof(int))
            {
                return ConvertIntegerToByteArray((entry.Value as int?).Value);
            }

            return value;
        }

        private IAndSessionTestBuilder ContainingEntries<Т>(IDictionary<string, Т> entries, bool includeCountCheck = false)
        {
            DictionaryValidator.ValidateValues(
                this.DataProviderName,
                this.DataProvider,
                entries.ToDictionary(e => e.Key, ConvertToByteEntry),
                this.ThrowNewDataProviderAssertionException,
                includeCountCheck);

            return this;
        }
    }
}
