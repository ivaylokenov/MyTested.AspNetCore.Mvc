namespace MyTested.Mvc.Builders.Contracts.Uris
{
    /// <summary>
    /// Used for testing URI location in a created result.
    /// </summary>
    public interface IUriTestBuilder
    {
        /// <summary>
        /// Tests whether the URI has the same host as the provided one.
        /// </summary>
        /// <param name="host">Host part of URI.</param>
        /// <returns>The same URI test builder.</returns>
        IAndUriTestBuilder WithHost(string host);

        /// <summary>
        /// Tests whether the URI has the same port as the provided one.
        /// </summary>
        /// <param name="port">Port part of URI.</param>
        /// <returns>The same URI test builder.</returns>
        IAndUriTestBuilder WithPort(int port);

        /// <summary>
        /// Tests whether the URI has the same absolute path as the provided one.
        /// </summary>
        /// <param name="absolutePath">Absolute path part of URI.</param>
        /// <returns>The same URI test builder.</returns>
        IAndUriTestBuilder WithAbsolutePath(string absolutePath);

        /// <summary>
        /// Tests whether the URI has the same scheme as the provided one.
        /// </summary>
        /// <param name="scheme">Scheme part of URI.</param>
        /// <returns>The same URI test builder.</returns>
        IAndUriTestBuilder WithScheme(string scheme);

        /// <summary>
        /// Tests whether the URI has the same query as the provided one.
        /// </summary>
        /// <param name="query">Query part of URI.</param>
        /// <returns>The same URI test builder.</returns>
        IAndUriTestBuilder WithQuery(string query);

        /// <summary>
        /// Tests whether the URI has the same fragment as the provided one.
        /// </summary>
        /// <param name="fragment">Document fragment part of URI.</param>
        /// <returns>The same URI test builder.</returns>
        IAndUriTestBuilder WithFragment(string fragment);
    }
}
