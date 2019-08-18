namespace MyTested.AspNetCore.Mvc.Builders.Data
{
    using Contracts.Data;
    using Internal.TestContexts;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Routing;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Utilities.Validators;

    /// <summary>
    /// Used for testing <see cref="ISession"/>.
    /// </summary>
    public class SessionTestBuilder : BaseDataProviderTestBuilder, IAndSessionTestBuilder
    {
        internal const string SessionName = "session";

        /// <summary>
        /// Initializes a new instance of the <see cref="SessionTestBuilder"/> class.
        /// </summary>
        /// <param name="testContext"><see cref="ComponentTestContext"/> containing data about the currently executed assertion chain.</param>
        public SessionTestBuilder(ComponentTestContext testContext)
            : base(testContext, SessionName)
        {
        }

        /// <inheritdoc />
        public IAndSessionTestBuilder ContainingEntryWithKey(string key)
        {
            this.ValidateContainingEntryWithKey(key);
            return this;
        }

        /// <inheritdoc />
        public IAndSessionTestBuilder ContainingEntryWithValue(byte[] value)
        {
            this.ValidateContainingEntryWithValue(value);
            return this;
        }

        /// <inheritdoc />
        public IAndSessionTestBuilder ContainingEntryWithValue(string value)
        {
            this.ValidateContainingEntryWithValue(ConvertStringToByteArray(value));
            return this;
        }

        /// <inheritdoc />
        public IAndSessionTestBuilder ContainingEntryWithValue(int value)
        {
            this.ValidateContainingEntryWithValue(ConvertIntegerToByteArray(value));
            return this;
        }

        /// <inheritdoc />
        public IAndSessionTestBuilder ContainingEntry(string key, byte[] value)
        {
            this.ValidateContainingEntry(key, value);
            return this;
        }

        /// <inheritdoc />
        public IAndSessionTestBuilder ContainingEntry(string key, string value)
        {
            this.ValidateContainingEntry(key, ConvertStringToByteArray(value));
            return this;
        }

        /// <inheritdoc />
        public IAndSessionTestBuilder ContainingEntry(string key, int value)
        {
            this.ValidateContainingEntry(key, ConvertIntegerToByteArray(value));
            return this;
        }

        /// <inheritdoc />
        public IAndSessionTestBuilder ContainingEntries(object entries)
            => this.ContainingEntries(new RouteValueDictionary(entries));

        /// <inheritdoc />
        public IAndSessionTestBuilder ContainingEntries(IDictionary<string, byte[]> entries)
            => this.ContainingEntries<byte[]>(entries);

        /// <inheritdoc />
        public IAndSessionTestBuilder ContainingEntries(IDictionary<string, object> entries)
            => this.ContainingEntries<object>(entries, true);

        /// <inheritdoc />
        public IAndSessionTestBuilder ContainingEntries(IDictionary<string, string> entries)
            => this.ContainingEntries<string>(entries);

        /// <inheritdoc />
        public IAndSessionTestBuilder ContainingEntries(IDictionary<string, int> entries)
            => this.ContainingEntries<int>(entries);

        /// <inheritdoc />
        public ISessionTestBuilder AndAlso() => this;

        /// <summary>
        /// When overridden in derived class provides a way to built the data provider as <see cref="IDictionary{TKey,TValue}"/>.
        /// </summary>
        /// <returns>Data provider as <see cref="IDictionary{TKey,TValue}"/></returns>
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

        private static byte[] ConvertStringToByteArray(string value) => Encoding.UTF8.GetBytes(value);

        private static byte[] ConvertIntegerToByteArray(int value)
            => new[]
            {
                (byte)(value >> 24),
                (byte)(0xFF & (value >> 16)),
                (byte)(0xFF & (value >> 8)),
                (byte)(0xFF & value)
            };

        private static object ConvertToByteEntry<T>(KeyValuePair<string, T> entry)
        {
            var typeOfValue = entry.Value?.GetType();
            object value = entry.Value;

            if (typeOfValue == typeof(string))
            {
                return ConvertStringToByteArray(entry.Value as string);
            }

            if (typeOfValue == typeof(int))
            {
                return ConvertIntegerToByteArray((entry.Value as int?).Value);
            }

            return value;
        }

        private IAndSessionTestBuilder ContainingEntries<T>(IDictionary<string, T> entries, bool includeCountCheck = false)
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
