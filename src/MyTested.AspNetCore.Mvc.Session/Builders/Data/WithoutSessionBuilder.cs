namespace MyTested.AspNetCore.Mvc.Builders.Data
{
    using Contracts.Data;
    using Microsoft.AspNetCore.Http;

    /// <summary>
    /// Used for building <see cref="ISession"/>.
    /// </summary>
    public class WithoutSessionBuilder : SessionBaseBuilder, IAndWithoutSessionBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WithoutSessionBuilder"/> class.
        /// </summary>
        /// <param name="session"><see cref="ISession"/> to built.</param>
        public WithoutSessionBuilder(ISession session) : base(session)
        {
        }

        /// <inheritdoc />
        public IAndWithoutSessionBuilder WithoutEntry(string key)
        {
            this.Session.Remove(key);
            return this;
        }

        /// <inheritdoc />
        public IAndWithoutSessionBuilder ClearSession()
        {
            this.Session.Clear();
            return this;
        }

        /// <inheritdoc />
        public IWithoutSessionBuilder AndAlso() => this;
    }
}
