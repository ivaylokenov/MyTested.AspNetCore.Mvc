namespace MyTested.AspNetCore.Mvc.Builders.Data
{
    using Contracts.Data;
    using Microsoft.AspNetCore.Http;

    public class WithoutSessionBuilder : SessionBaseBuilder, IAndWithoutSessionBuilder
    {
        public WithoutSessionBuilder(ISession session) : base(session)
        {
        }

        public IAndWithoutSessionBuilder WithoutEntry(string key)
        {
            this.Session.Remove(key);
            return this;
        }

        public IAndWithoutSessionBuilder ClearSession()
        {
            this.Session.Clear();
            return this;
        }

        public IWithoutSessionBuilder AndAlso() => this;
    }
}
