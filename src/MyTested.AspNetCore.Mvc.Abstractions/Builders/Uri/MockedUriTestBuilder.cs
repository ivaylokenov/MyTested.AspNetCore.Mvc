namespace MyTested.AspNetCore.Mvc.Builders.Uri
{
    using System;
    using System.Collections.Generic;
    using Contracts.Uri;
    using Internal;

    /// <summary>
    /// Used for testing <see cref="Uri"/>.
    /// </summary>
    public class MockedUriTestBuilder : MockedUriBuilder, IAndUriTestBuilder
    {
        private readonly ICollection<Func<MockedUri, Uri, bool>> validations;

        /// <summary>
        /// Initializes a new instance of the <see cref="MockedUriTestBuilder"/> class.
        /// </summary>
        public MockedUriTestBuilder()
        {
            this.validations = new List<Func<MockedUri, Uri, bool>>();
        }

        /// <inheritdoc />
        public new IAndUriTestBuilder WithHost(string host)
        {
            this.validations.Add((expected, actual) => expected.Host == actual.Host);
            base.WithHost(host);

            return this;
        }

        /// <inheritdoc />
        public new IAndUriTestBuilder WithPort(int port)
        {
            this.validations.Add((expected, actual) => expected.Port == actual.Port);
            base.WithPort(port);

            return this;
        }

        /// <inheritdoc />
        public new IAndUriTestBuilder WithAbsolutePath(string absolutePath)
        {
            this.validations.Add((expected, actual) =>
            {
                if (actual.IsAbsoluteUri)
                {
                    return expected.AbsolutePath == actual.AbsolutePath;
                }

                return expected.AbsolutePath == actual.OriginalString;
            });

            base.WithAbsolutePath(absolutePath);

            return this;
        }

        /// <inheritdoc />
        public new IAndUriTestBuilder WithScheme(string scheme)
        {
            this.validations.Add((expected, actual) => expected.Scheme == actual.Scheme);
            base.WithScheme(scheme);

            return this;
        }

        /// <inheritdoc />
        public new IAndUriTestBuilder WithQuery(string query)
        {
            this.validations.Add((expected, actual) => expected.Query == actual.Query);
            base.WithQuery(query);

            return this;
        }

        /// <inheritdoc />
        public new IAndUriTestBuilder WithFragment(string fragment)
        {
            this.validations.Add((expected, actual) => expected.Fragment == actual.Fragment);
            base.WithFragment(fragment);

            return this;
        }

        /// <inheritdoc />
        public IAndUriTestBuilder Passing(Action<Uri> assertions)
        {
            this.validations.Add((expected, actual) => 
            {
                assertions(actual);
                return true;
            });

            return this;
        }

        /// <inheritdoc />
        public IAndUriTestBuilder Passing(Func<Uri, bool> predicate)
        {
            this.validations.Add((expected, actual) => predicate(actual));
            return this;
        }

        /// <inheritdoc />
        public new IUriTestBuilder AndAlso() => this;

        internal ICollection<Func<MockedUri, Uri, bool>> GetMockedUriValidations() => this.validations;
    }
}
