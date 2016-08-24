namespace MyTested.AspNetCore.Mvc.Builders.Uri
{
    using System;
    using Contracts.Uri;
    using Internal;

    using SystemUriBuilder = System.UriBuilder;

    /// <summary>
    /// Used for building <see cref="Uri"/>.
    /// </summary>
    public class MockedUriBuilder : IAndUriBuilder
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
        public IAndUriBuilder WithHost(string host)
        {
            this.MockedUri.Host = host;
            return this;
        }

        /// <inheritdoc />
        public IAndUriBuilder WithPort(int port)
        {
            this.MockedUri.Port = port;
            return this;
        }

        /// <inheritdoc />
        public IAndUriBuilder WithAbsolutePath(string absolutePath)
        {
            this.MockedUri.AbsolutePath = absolutePath;
            return this;
        }

        /// <inheritdoc />
        public IAndUriBuilder WithScheme(string scheme)
        {
            this.MockedUri.Scheme = scheme;
            return this;
        }

        /// <inheritdoc />
        public IAndUriBuilder WithQuery(string query)
        {
            if (!query.StartsWith("?"))
            {
                throw new ArgumentException("Query string must start with the '?' symbol.");
            }

            this.MockedUri.Query = query;
            return this;
        }

        /// <inheritdoc />
        public IAndUriBuilder WithFragment(string fragment)
        {
            this.MockedUri.Fragment = fragment;
            return this;
        }

        /// <inheritdoc />
        public IUriBuilder AndAlso() => this;

        public MockedUri GetMockedUri() => this.MockedUri;

        public Uri GetUri()
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
