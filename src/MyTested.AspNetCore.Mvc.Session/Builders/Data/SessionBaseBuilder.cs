namespace MyTested.AspNetCore.Mvc.Builders.Data
{
    using Microsoft.AspNetCore.Http;

    public abstract class SessionBaseBuilder
    {
        /// <summary>
        /// Gets the <see cref="ISession"/>.
        /// </summary>
        /// <value>The built <see cref="ISession"/>.</value>
        protected ISession Session { get; }

        public SessionBaseBuilder(ISession session) => this.Session = session;
    }
}
