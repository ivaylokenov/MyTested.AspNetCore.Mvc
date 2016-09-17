namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Uri
{
    /// <summary>
    /// Used for building <see cref="System.Uri"/> location.
    /// </summary>
    public interface IUriBuilder
    {
        /// <summary>
        /// Sets the <see cref="System.Uri.Host"/> of the built <see cref="System.Uri"/>.
        /// </summary>
        /// <param name="host">Host part of <see cref="System.Uri"/>.</param>
        /// <returns>The same <see cref="IAndUriBuilder"/>.</returns>
        IAndUriBuilder WithHost(string host);

        /// <summary>
        /// Sets the <see cref="System.Uri.Port"/> of the built <see cref="System.Uri"/>.
        /// </summary>
        /// <param name="port">Port part of <see cref="System.Uri"/>.</param>
        /// <returns>The same <see cref="IAndUriBuilder"/>.</returns>
        IAndUriBuilder WithPort(int port);

        /// <summary>
        /// Sets the <see cref="System.Uri.AbsolutePath"/> of the built <see cref="System.Uri"/>.
        /// </summary>
        /// <param name="absolutePath">Absolute path part of <see cref="System.Uri"/>.</param>
        /// <returns>The same <see cref="IAndUriBuilder"/>.</returns>
        IAndUriBuilder WithAbsolutePath(string absolutePath);

        /// <summary>
        /// Sets the <see cref="System.Uri.Scheme"/> of the built <see cref="System.Uri"/>.
        /// </summary>
        /// <param name="scheme">Scheme part of <see cref="System.Uri"/>.</param>
        /// <returns>The same <see cref="IAndUriBuilder"/>.</returns>
        IAndUriBuilder WithScheme(string scheme);

        /// <summary>
        /// Sets the <see cref="System.Uri.Query"/> of the built <see cref="System.Uri"/>.
        /// </summary>
        /// <param name="query">Query part of <see cref="System.Uri"/>.</param>
        /// <returns>The same <see cref="IAndUriBuilder"/>.</returns>
        IAndUriBuilder WithQuery(string query);

        /// <summary>
        /// Sets the <see cref="System.Uri.Fragment"/> of the built <see cref="System.Uri"/>.
        /// </summary>
        /// <param name="fragment">Document fragment part of <see cref="System.Uri"/>.</param>
        /// <returns>The same <see cref="IAndUriBuilder"/>.</returns>
        IAndUriBuilder WithFragment(string fragment);
    }
}
