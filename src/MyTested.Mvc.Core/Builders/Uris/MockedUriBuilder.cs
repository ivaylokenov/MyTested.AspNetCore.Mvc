namespace MyTested.Mvc.Builders.Uris
{
    using System;
    using Contracts.Uris;
    using Internal;

    using SystemUriBuilder = System.UriBuilder;

    /// <summary>
    /// Used for building <see cref="Uri"/>.
    /// </summary>
    public class MockedUriBuilder : IAndUriTestBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MockedUriBuilder"/> class.
        /// </summary>
        public MockedUriBuilder()
        {
            this.MockedUri = new MockedUri();
        }

        /// <summary>
        /// Gets the built mocked <see cref="Uri"/> instance.
        /// </summary>
        /// <value>Mocked <see cref="Uri"/>.</value>
        protected MockedUri MockedUri { get; private set; }

        /// <inheritdoc />
        public virtual IAndUriTestBuilder WithHost(string host)
        {
            this.MockedUri.Host = host;
            return this;
        }

        /// <inheritdoc />
        public virtual IAndUriTestBuilder WithPort(int port)
        {
            this.MockedUri.Port = port;
            return this;
        }

        /// <inheritdoc />
        public virtual IAndUriTestBuilder WithAbsolutePath(string absolutePath)
        {
            this.MockedUri.AbsolutePath = absolutePath;
            return this;
        }

        /// <inheritdoc />
        public virtual IAndUriTestBuilder WithScheme(string scheme)
        {
            this.MockedUri.Scheme = scheme;
            return this;
        }

        /// <inheritdoc />
        public virtual IAndUriTestBuilder WithQuery(string query)
        {
            if (!query.StartsWith("?"))
            {
                throw new ArgumentException("Query string must start with the '?' symbol.");
            }

            this.MockedUri.Query = query;
            return this;
        }

        /// <inheritdoc />
        public virtual IAndUriTestBuilder WithFragment(string fragment)
        {
            this.MockedUri.Fragment = fragment;
            return this;
        }

        /// <inheritdoc />
        public IUriTestBuilder AndAlso() => this;

        internal MockedUri GetMockedUri() => this.MockedUri;

        internal Uri GetUri()
        {
            var uriBuilder = new SystemUriBuilder(
                this.MockedUri.Scheme,
                this.MockedUri.Host,
                this.MockedUri.Port ?? 80,
                this.MockedUri.AbsolutePath,
                this.MockedUri.Query);

            return uriBuilder.Uri;
        }
    }
}
