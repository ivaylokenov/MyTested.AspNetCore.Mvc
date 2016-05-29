namespace MyTested.Mvc.Builders.Contracts.Uris
{
    /// <summary>
    /// Used for testing <see cref="System.Uri"/> location..
    /// </summary>
    public interface IUriTestBuilder
    {
        /// <summary>
        /// Tests whether the <see cref="System.Uri.Host"/> has the same value as the provided one.
        /// </summary>
        /// <param name="host">Host part of <see cref="System.Uri"/>.</param>
        /// <returns>The same <see cref="IAndUriTestBuilder"/>.</returns>
        IAndUriTestBuilder WithHost(string host);

        /// <summary>
        /// Tests whether the <see cref="System.Uri.Port"/> has the same value as the provided one.
        /// </summary>
        /// <param name="port">Port part of <see cref="System.Uri"/>.</param>
        /// <returns>The same <see cref="IAndUriTestBuilder"/>.</returns>
        IAndUriTestBuilder WithPort(int port);

        /// <summary>
        /// Tests whether the <see cref="System.Uri.AbsolutePath"/> has the same value as the provided one.
        /// </summary>
        /// <param name="absolutePath">Absolute path part of <see cref="System.Uri"/>.</param>
        /// <returns>The same <see cref="IAndUriTestBuilder"/>.</returns>
        IAndUriTestBuilder WithAbsolutePath(string absolutePath);

        /// <summary>
        /// Tests whether the <see cref="System.Uri.Scheme"/> has the same value as the provided one.
        /// </summary>
        /// <param name="scheme">Scheme part of <see cref="System.Uri"/>.</param>
        /// <returns>The same <see cref="IAndUriTestBuilder"/>.</returns>
        IAndUriTestBuilder WithScheme(string scheme);

        /// <summary>
        /// Tests whether the <see cref="System.Uri.Query"/> has the same value as the provided one.
        /// </summary>
        /// <param name="query">Query part of <see cref="System.Uri"/>.</param>
        /// <returns>The same <see cref="IAndUriTestBuilder"/>.</returns>
        IAndUriTestBuilder WithQuery(string query);

        /// <summary>
        /// Tests whether the <see cref="System.Uri.Fragment"/> has the same value as the provided one.
        /// </summary>
        /// <param name="fragment">Document fragment part of <see cref="System.Uri"/>.</param>
        /// <returns>The same <see cref="IAndUriTestBuilder"/>.</returns>
        IAndUriTestBuilder WithFragment(string fragment);
    }
}
