namespace MyTested.AspNetCore.Mvc.Builders.Data
{
    using Microsoft.AspNetCore.Http;

    public abstract class BaseSessionBuilder
    {
        /// <summary>
        /// Gets the <see cref="ISession"/>.
        /// </summary>
        /// <value>The built <see cref="ISession"/>.</value>
        protected ISession Session { get; }

        /// <summary>
        /// Abstract base <see cref="BaseSessionBuilder"/> class.
        /// </summary>
        /// <param name="session"><see cref="ISession"/> to built.</param>
        public BaseSessionBuilder(ISession session) => this.Session = session;
    }
}
