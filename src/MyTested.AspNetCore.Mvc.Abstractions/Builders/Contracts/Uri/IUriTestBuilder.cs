namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Uri
{
    using System;

    /// <summary>
    /// Used for testing <see cref="Uri"/> location.
    /// </summary>
    public interface IUriTestBuilder
    {
        /// <summary>
        /// Tests whether the <see cref="Uri.Host"/> has the same value as the provided one.
        /// </summary>
        /// <param name="host">Host part of <see cref="Uri"/>.</param>
        /// <returns>The same <see cref="IAndUriTestBuilder"/>.</returns>
        IAndUriTestBuilder WithHost(string host);

        /// <summary>
        /// Tests whether the <see cref="Uri.Port"/> has the same value as the provided one.
        /// </summary>
        /// <param name="port">Port part of <see cref="Uri"/>.</param>
        /// <returns>The same <see cref="IAndUriTestBuilder"/>.</returns>
        IAndUriTestBuilder WithPort(int port);

        /// <summary>
        /// Tests whether the <see cref="Uri.AbsolutePath"/> has the same value as the provided one.
        /// </summary>
        /// <param name="absolutePath">Absolute path part of <see cref="Uri"/>.</param>
        /// <returns>The same <see cref="IAndUriTestBuilder"/>.</returns>
        IAndUriTestBuilder WithAbsolutePath(string absolutePath);

        /// <summary>
        /// Tests whether the <see cref="Uri.Scheme"/> has the same value as the provided one.
        /// </summary>
        /// <param name="scheme">Scheme part of <see cref="Uri"/>.</param>
        /// <returns>The same <see cref="IAndUriTestBuilder"/>.</returns>
        IAndUriTestBuilder WithScheme(string scheme);

        /// <summary>
        /// Tests whether the <see cref="Uri.Query"/> has the same value as the provided one.
        /// </summary>
        /// <param name="query">Query part of <see cref="Uri"/>.</param>
        /// <returns>The same <see cref="IAndUriTestBuilder"/>.</returns>
        IAndUriTestBuilder WithQuery(string query);

        /// <summary>
        /// Tests whether the <see cref="Uri.Fragment"/> has the same value as the provided one.
        /// </summary>
        /// <param name="fragment">Document fragment part of <see cref="Uri"/>.</param>
        /// <returns>The same <see cref="IAndUriTestBuilder"/>.</returns>
        IAndUriTestBuilder WithFragment(string fragment);

        /// <summary>
        /// Tests whether the <see cref="Uri"/> passes the given assertions. 
        /// </summary>
        /// <param name="assertions">Action containing all assertions on the <see cref="Uri"/>.</param>
        /// <returns>The same <see cref="IAndUriTestBuilder"/>.</returns>
        IAndUriTestBuilder Passing(Action<Uri> assertions);

        /// <summary>
        /// Tests whether the <see cref="Uri"/> passes the given predicate. 
        /// </summary>
        /// <param name="predicate">Predicate testing the <see cref="Uri"/>.</param>
        /// <returns>The same <see cref="IAndUriTestBuilder"/>.</returns>
        IAndUriTestBuilder Passing(Func<Uri, bool> predicate);
    }
}
