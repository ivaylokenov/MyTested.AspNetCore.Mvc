namespace MyTested.Mvc.Builders.Uris
{
    using System;
    using System.Collections.Generic;
    using Contracts.Uris;
    using Internal;

    /// <summary>
    /// Used for testing URI location in a created result.
    /// </summary>
    public class MockedUriTestBuilder : MockedUriBuilder
    {
        private readonly ICollection<Func<MockedUri, Uri, bool>> validations;

        /// <summary>
        /// Initializes a new instance of the <see cref="MockedUriTestBuilder" /> class.
        /// </summary>
        public MockedUriTestBuilder()
        {
            this.validations = new List<Func<MockedUri, Uri, bool>>();
        }

        /// <summary>
        /// Tests whether the URI has the same host as the provided one.
        /// </summary>
        /// <param name="host">Host part of URI.</param>
        /// <returns>The same URI test builder.</returns>
        public override IAndUriTestBuilder WithHost(string host)
        {
            this.validations.Add((expected, actual) => expected.Host == actual.Host);
            return base.WithHost(host);
        }

        /// <summary>
        /// Tests whether the URI has the same port as the provided one.
        /// </summary>
        /// <param name="port">Port part of URI.</param>
        /// <returns>The same URI test builder.</returns>
        public override IAndUriTestBuilder WithPort(int port)
        {
            this.validations.Add((expected, actual) => expected.Port == actual.Port);
            return base.WithPort(port);
        }

        /// <summary>
        /// Tests whether the URI has the same absolute path as the provided one.
        /// </summary>
        /// <param name="absolutePath">Absolute path part of URI.</param>
        /// <returns>The same URI test builder.</returns>
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

        /// <summary>
        /// Tests whether the URI has the same scheme as the provided one.
        /// </summary>
        /// <param name="scheme">Scheme part of URI.</param>
        /// <returns>The same URI test builder.</returns>
        public override IAndUriTestBuilder WithScheme(string scheme)
        {
            this.validations.Add((expected, actual) => expected.Scheme == actual.Scheme);
            return base.WithScheme(scheme);
        }

        /// <summary>
        /// Tests whether the URI has the same query as the provided one.
        /// </summary>
        /// <param name="query">Query part of URI.</param>
        /// <returns>The same URI test builder.</returns>
        public override IAndUriTestBuilder WithQuery(string query)
        {
            this.validations.Add((expected, actual) => expected.Query == actual.Query);
            return base.WithQuery(query);
        }

        /// <summary>
        /// Tests whether the URI has the same fragment as the provided one.
        /// </summary>
        /// <param name="fragment">Document fragment part of URI.</param>
        /// <returns>The same URI test builder.</returns>
        public override IAndUriTestBuilder WithFragment(string fragment)
        {
            this.validations.Add((expected, actual) => expected.Fragment == actual.Fragment);
            return base.WithFragment(fragment);
        }

        internal ICollection<Func<MockedUri, Uri, bool>> GetMockedUriValidations()
        {
            return this.validations;
        }
    }
}
