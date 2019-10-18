namespace MyTested.AspNetCore.Mvc.Builders.Data
{
    using System.Collections.Generic;
    using Contracts.Data;
    using Microsoft.AspNetCore.Http;

    /// <summary>
    /// Used for building <see cref="ISession"/>.
    /// </summary>
    public class WithoutSessionBuilder : BaseSessionBuilder, IAndWithoutSessionTestBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WithoutSessionBuilder"/> class.
        /// </summary>
        /// <param name="session"><see cref="ISession"/> to built.</param>
        public WithoutSessionBuilder(ISession session) 
            : base(session)
        {
        }

        /// <inheritdoc />
        public IAndWithoutSessionTestBuilder WithoutEntry(string key)
        {
            this.Session.Remove(key);
            return this;
        }

        /// <inheritdoc />
        public IAndWithoutSessionTestBuilder WithoutAllEntries()
        {
            this.Session.Clear();
            return this;
        }

        /// <inheritdoc />
        public IAndWithoutSessionTestBuilder WithoutEntries(IEnumerable<string> keys)
        {
            this.RemoveKeysFromSession(keys);
            return this;
        }

        /// <inheritdoc />
        public IAndWithoutSessionTestBuilder WithoutEntries(params string[] keys)
        {
            this.RemoveKeysFromSession(keys);
            return this;
        }

        /// <inheritdoc />
        public IWithoutSessionBuilder AndAlso() => this;

        private void RemoveKeysFromSession(IEnumerable<string> keys)
        {
            foreach (var key in keys)
            {
                this.Session.Remove(key);
            }
        }
    }
}
