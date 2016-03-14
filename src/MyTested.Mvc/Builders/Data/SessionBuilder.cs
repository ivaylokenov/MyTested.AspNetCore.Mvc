namespace MyTested.Mvc.Builders.Data
{
    using System;
    using System.Collections.Generic;
    using Contracts.Data;
    using Internal.Contracts;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Http.Features;
    using Microsoft.AspNetCore.Routing;
    using Utilities.Extensions;

    public class SessionBuilder : IAndSessionBuilder
    {
        public SessionBuilder(ISession session)
        {
            this.Session = session;
        }

        protected ISession Session { get; private set; }

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

        public IAndSessionBuilder WithEntry(string key, byte[] value)
        {
            this.Session.Set(key, value);
            return this;
        }

        public IAndSessionBuilder WithStringEntry(string key, string value)
        {
            this.Session.SetString(key, value);
            return this;
        }

        public IAndSessionBuilder WithIntegerEntry(string key, int value)
        {
            this.Session.SetInt32(key, value);
            return this;
        }

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
                    this.WithStringEntry(e.Key, (string)e.Value);
                }
                else if (typeOfValue == typeof(int))
                {
                    this.WithIntegerEntry(e.Key, (int)e.Value);
                }
            });

            return this;
        }

        public IAndSessionBuilder WithEntries(IDictionary<string, byte[]> entries)
        {
            entries.ForEach(e => this.WithEntry(e.Key, e.Value));
            return this;
        }
        
        public IAndSessionBuilder WithStringEntries(IDictionary<string, string> entries)
        {
            entries.ForEach(e => this.WithStringEntry(e.Key, e.Value));
            return this;
        }
        
        public IAndSessionBuilder WithIntegerEntries(IDictionary<string, int> entries)
        {
            entries.ForEach(e => this.WithIntegerEntry(e.Key, e.Value));
            return this;
        }

        public ISessionBuilder AndAlso() => this;
    }
}
