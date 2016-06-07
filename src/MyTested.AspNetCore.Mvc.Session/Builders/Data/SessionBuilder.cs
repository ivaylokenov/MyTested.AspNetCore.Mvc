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
    public class SessionBuilder : IAndSessionBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SessionBuilder"/> class.
        /// </summary>
        /// <param name="session"><see cref="ISession"/> to built.</param>
        public SessionBuilder(ISession session)
        {
            this.Session = session;
        }

        /// <summary>
        /// Gets the <see cref="ISession"/>.
        /// </summary>
        /// <value>The built <see cref="ISession"/>.</value>
        protected ISession Session { get; private set; }

        /// <inheritdoc />
        public IAndSessionBuilder WithId(string sessionId)
        {
            var mockedSession = this.Session as IMockedSession;
            if (mockedSession == null)
            {
                throw new InvalidOperationException("Setting session Id requires the registered ISession service to implement IMockedSession.");
            }

            mockedSession.Id = sessionId;
            return this;
        }

        /// <inheritdoc />
        public IAndSessionBuilder WithEntry(string key, byte[] value)
        {
            this.Session.Set(key, value);
            return this;
        }

        /// <inheritdoc />
        public IAndSessionBuilder WithEntry(string key, string value)
        {
            this.Session.SetString(key, value);
            return this;
        }

        /// <inheritdoc />
        public IAndSessionBuilder WithEntry(string key, int value)
        {
            this.Session.SetInt32(key, value);
            return this;
        }

        /// <inheritdoc />
        public IAndSessionBuilder WithEntries(object entries)
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
        public IAndSessionBuilder WithEntries(IDictionary<string, byte[]> entries)
        {
            entries.ForEach(e => this.WithEntry(e.Key, e.Value));
            return this;
        }

        /// <inheritdoc />
        public IAndSessionBuilder WithEntries(IDictionary<string, string> entries)
        {
            entries.ForEach(e => this.WithEntry(e.Key, e.Value));
            return this;
        }

        /// <inheritdoc />
        public IAndSessionBuilder WithEntries(IDictionary<string, int> entries)
        {
            entries.ForEach(e => this.WithEntry(e.Key, e.Value));
            return this;
        }

        /// <inheritdoc />
        public ISessionBuilder AndAlso() => this;
    }
}
