namespace MyTested.AspNetCore.Mvc.Builders.Data
{
    using System;
    using System.Collections.Generic;
    using Contracts.Data;
    using Internal.Contracts;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Routing;
    using Utilities.Extensions;

    /// <summary>
    /// Used for building <see cref="ISession"/>.
    /// </summary>
    public class WithSessionBuilder : BaseSessionBuilder, IAndWithSessionBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WithSessionBuilder"/> class.
        /// </summary>
        /// <param name="session"><see cref="ISession"/> to built.</param>
        public WithSessionBuilder(ISession session) : base(session) { }

        /// <inheritdoc />
        public IAndWithSessionBuilder WithId(string sessionId)
        {
            if (!(this.Session is ISessionMock mockedSession))
            {
                throw new InvalidOperationException("Setting session Id requires the registered ISession service to implement ISessionMock.");
            }

            mockedSession.Id = sessionId;
            return this;
        }

        /// <inheritdoc />
        public IAndWithSessionBuilder WithEntry(string key, byte[] value)
        {
            this.Session.Set(key, value);
            return this;
        }

        /// <inheritdoc />
        public IAndWithSessionBuilder WithEntry(string key, string value)
        {
            this.Session.SetString(key, value);
            return this;
        }

        /// <inheritdoc />
        public IAndWithSessionBuilder WithEntry(string key, int value)
        {
            this.Session.SetInt32(key, value);
            return this;
        }

        /// <inheritdoc />
        public IAndWithSessionBuilder WithEntries(object entries)
        {
            var entriesAsDictionary = new RouteValueDictionary(entries);
            entriesAsDictionary.ForEach(e =>
            {
                var typeOfValue = e.Value?.GetType();
                if (typeOfValue == typeof(byte[]))
                {
                    this.WithEntry(e.Key, (byte[])e.Value);
                }
                else if (typeOfValue == typeof(string))
                {
                    this.WithEntry(e.Key, (string)e.Value);
                }
                else if (typeOfValue == typeof(int))
                {
                    this.WithEntry(e.Key, (int)e.Value);
                }
            });

            return this;
        }

        /// <inheritdoc />
        public IAndWithSessionBuilder WithEntries(IDictionary<string, byte[]> entries)
        {
            entries.ForEach(e => this.WithEntry(e.Key, e.Value));
            return this;
        }

        /// <inheritdoc />
        public IAndWithSessionBuilder WithEntries(IDictionary<string, string> entries)
        {
            entries.ForEach(e => this.WithEntry(e.Key, e.Value));
            return this;
        }

        /// <inheritdoc />
        public IAndWithSessionBuilder WithEntries(IDictionary<string, int> entries)
        {
            entries.ForEach(e => this.WithEntry(e.Key, e.Value));
            return this;
        }

        /// <inheritdoc />
        public IWithSessionBuilder AndAlso() => this;
    }
}
