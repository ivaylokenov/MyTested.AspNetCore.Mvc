namespace MyTested.AspNetCore.Mvc.Builders.Uri
{
    using System;
    using Contracts.Uri;
    using Internal;

    using SystemUriBuilder = System.UriBuilder;

    /// <summary>
    /// Used for building <see cref="Uri"/>.
    /// </summary>
    public class UriMockBuilder : IAndUriBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UriMockBuilder"/> class.
        /// </summary>
        public UriMockBuilder()
        {
            this.UriMock = new UriMock();
        }

        /// <summary>
        /// Gets the built <see cref="Uri"/> instance.
        /// </summary>
        /// <value>Mock of <see cref="Uri"/>.</value>
        protected UriMock UriMock { get; private set; }

        /// <inheritdoc />
        public IAndUriBuilder WithHost(string host)
        {
            this.UriMock.Host = host;
            return this;
        }

        /// <inheritdoc />
        public IAndUriBuilder WithPort(int port)
        {
            this.UriMock.Port = port;
            return this;
        }

        /// <inheritdoc />
        public IAndUriBuilder WithAbsolutePath(string absolutePath)
        {
            this.UriMock.AbsolutePath = absolutePath;
            return this;
        }

        /// <inheritdoc />
        public IAndUriBuilder WithScheme(string scheme)
        {
            this.UriMock.Scheme = scheme;
            return this;
        }

        /// <inheritdoc />
        public IAndUriBuilder WithQuery(string query)
        {
            if (!query.StartsWith("?"))
            {
                throw new ArgumentException("Query string must start with the '?' symbol.");
            }

            this.UriMock.Query = query;
            return this;
        }

        /// <inheritdoc />
        public IAndUriBuilder WithFragment(string fragment)
        {
            this.UriMock.Fragment = fragment;
            return this;
        }

        /// <inheritdoc />
        public IUriBuilder AndAlso() => this;

        public UriMock GetUriMock() => this.UriMock;

        public Uri GetUri()
        {
            var uriBuilder = new SystemUriBuilder(
                this.UriMock.Scheme,
                this.UriMock.Host,
                this.UriMock.Port ?? 80,
                this.UriMock.AbsolutePath,
                this.UriMock.Query);

            return uriBuilder.Uri;
        }
    }
}
