namespace MyTested.Mvc.Builders.Contracts.ActionResults.Created
{
    using Microsoft.AspNet.Mvc;
    using Models;
    using Uris;
    using System;
    using System.Linq.Expressions;

    /// <summary>
    /// Used for testing created results.
    /// </summary>
    public interface ICreatedTestBuilder : IBaseResponseModelTestBuilder
    {
        /// <summary>
        /// Tests whether created result has specific location provided by string.
        /// </summary>
        /// <param name="location">Expected location as string.</param>
        /// <returns>The same created test builder.</returns>
        IAndCreatedTestBuilder AtLocation(string location);

        /// <summary>
        /// Tests whether created result has specific location provided by URI.
        /// </summary>
        /// <param name="location">Expected location as URI.</param>
        /// <returns>The same created test builder.</returns>
        IAndCreatedTestBuilder AtLocation(Uri location);

        /// <summary>
        /// Tests whether created result has specific location provided by builder.
        /// </summary>
        /// <param name="uriTestBuilder">Builder for expected URI.</param>
        /// <returns>The same created test builder.</returns>
        IAndCreatedTestBuilder AtLocation(Action<IUriTestBuilder> uriTestBuilder);

        /// <summary>
        /// Tests whether created result returns created at specific action.
        /// </summary>
        /// <typeparam name="TController">Type of expected controller.</typeparam>
        /// <param name="actionCall">Method call expression indicating the expected action.</param>
        /// <returns>The same created test builder.</returns>
        IAndCreatedTestBuilder At<TController>(Expression<Func<TController, object>> actionCall)
            where TController : Controller;

        /// <summary>
        /// Tests whether created result returns created at specific action.
        /// </summary>
        /// <typeparam name="TController">Type of expected controller.</typeparam>
        /// <param name="actionCall">Method call expression indicating the expected action.</param>
        /// <returns>The same created test builder.</returns>
        IAndCreatedTestBuilder At<TController>(Expression<Action<TController>> actionCall)
            where TController : Controller;
    }
}
