namespace MyTested.Mvc.Builders.Uris
{
    using System;
    using System.Collections.Generic;
    using Contracts.Uris;
    using Internal;

    /// <summary>
    /// Used for testing <see cref="Uri"/>.
    /// </summary>
    public class MockedUriTestBuilder : MockedUriBuilder
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
        public override IAndUriTestBuilder WithHost(string host)
        {
            this.validations.Add((expected, actual) => expected.Host == actual.Host);
            return base.WithHost(host);
        }

        /// <inheritdoc />
        public override IAndUriTestBuilder WithPort(int port)
        {
            this.validations.Add((expected, actual) => expected.Port == actual.Port);
            return base.WithPort(port);
        }

        /// <inheritdoc />
        public override IAndUriTestBuilder WithAbsolutePath(string absolutePath)
        {
            this.validations.Add((expected, actual) =>
            {
                if (actual.IsAbsoluteUri)
                {
                    return expected.AbsolutePath == actual.AbsolutePath;
                }

                return expected.AbsolutePath == actual.OriginalString;
            });

            return base.WithAbsolutePath(absolutePath);
        }

        /// <inheritdoc />
        public override IAndUriTestBuilder WithScheme(string scheme)
        {
            this.validations.Add((expected, actual) => expected.Scheme == actual.Scheme);
            return base.WithScheme(scheme);
        }

        /// <inheritdoc />
        public override IAndUriTestBuilder WithQuery(string query)
        {
            this.validations.Add((expected, actual) => expected.Query == actual.Query);
            return base.WithQuery(query);
        }

        /// <inheritdoc />
        public override IAndUriTestBuilder WithFragment(string fragment)
        {
            this.validations.Add((expected, actual) => expected.Fragment == actual.Fragment);
            return base.WithFragment(fragment);
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

        internal ICollection<Func<MockedUri, Uri, bool>> GetMockedUriValidations() => this.validations;
    }
}
