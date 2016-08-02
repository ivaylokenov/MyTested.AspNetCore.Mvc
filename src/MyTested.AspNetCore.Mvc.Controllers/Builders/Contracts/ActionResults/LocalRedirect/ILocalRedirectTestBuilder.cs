namespace MyTested.AspNetCore.Mvc.Builders.Contracts.ActionResults.LocalRedirect
{
    using System;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Base;
    using Microsoft.AspNetCore.Mvc;
    using Uris;

    /// <summary>
    /// Used for testing <see cref="LocalRedirectResult"/>.
    /// </summary>
    public interface ILocalRedirectTestBuilder : IBaseTestBuilderWithActionResult<LocalRedirectResult>
    {
        /// <summary>
        /// Tests whether <see cref="LocalRedirectResult"/> is permanent.
        /// </summary>
        /// <returns>The same <see cref="IAndLocalRedirectTestBuilder"/>.</returns>
        IAndLocalRedirectTestBuilder Permanent();

        /// <summary>
        /// Tests whether <see cref="LocalRedirectResult"/> has specific URL provided as string.
        /// </summary>
        /// <param name="localUrl">Expected URL as string.</param>
        /// <returns>The same <see cref="IAndLocalRedirectTestBuilder"/>.</returns>
        IAndLocalRedirectTestBuilder ToUrl(string localUrl);

        /// <summary>
        /// Tests whether <see cref="LocalRedirectResult"/> URL passes the given assertions.
        /// </summary>
        /// <param name="assertions">Action containing all assertions on the URL.</param>
        /// <returns>The same <see cref="IAndLocalRedirectTestBuilder"/>.</returns>
        IAndLocalRedirectTestBuilder ToUrlPassing(Action<string> assertions);

        /// <summary>
        /// Tests whether <see cref="LocalRedirectResult"/> URL passes the given predicate.
        /// </summary>
        /// <param name="predicate">Predicate testing the URL.</param>
        /// <returns>The same <see cref="IAndLocalRedirectTestBuilder"/>.</returns>
        IAndLocalRedirectTestBuilder ToUrlPassing(Func<string, bool> predicate);

        /// <summary>
        /// Tests whether <see cref="LocalRedirectResult"/> has specific URL provided as <see cref="Uri"/>.
        /// </summary>
        /// <param name="localUrl">Expected URL as <see cref="Uri"/>.</param>
        /// <returns>The same <see cref="IAndLocalRedirectTestBuilder"/>.</returns>
        IAndLocalRedirectTestBuilder ToUrl(Uri localUrl);

        /// <summary>
        /// Tests whether <see cref="LocalRedirectResult"/> has specific location provided by builder.
        /// </summary>
        /// <param name="uriTestBuilder">Builder for expected URL.</param>
        /// <returns>The same <see cref="IAndLocalRedirectTestBuilder"/>.</returns>
        IAndLocalRedirectTestBuilder ToUrl(Action<IUriTestBuilder> uriTestBuilder);

        /// <summary>
        /// Tests whether <see cref="LocalRedirectResult"/> has the same <see cref="IUrlHelper"/> as the provided one.
        /// </summary>
        /// <param name="urlHelper">URL helper of type <see cref="IUrlHelper"/>.</param>
        /// <returns>The same <see cref="IAndLocalRedirectTestBuilder"/>.</returns>
        IAndLocalRedirectTestBuilder WithUrlHelper(IUrlHelper urlHelper);

        /// <summary>
        /// Tests whether <see cref="LocalRedirectResult"/> has the same <see cref="IUrlHelper"/> type as the provided one.
        /// </summary>
        /// <typeparam name="TUrlHelper">URL helper of type <see cref="IUrlHelper"/>.</typeparam>
        /// <returns>The same <see cref="IAndLocalRedirectTestBuilder"/>.</returns>
        IAndLocalRedirectTestBuilder WithUrlHelperOfType<TUrlHelper>()
            where TUrlHelper : IUrlHelper;

        /// <summary>
        /// Tests whether <see cref="LocalRedirectResult"/> redirects to specific action.
        /// </summary>
        /// <typeparam name="TController">Type of expected redirect controller.</typeparam>
        /// <param name="actionCall">Method call expression indicating the expected redirect action.</param>
        /// <returns>The same <see cref="IAndLocalRedirectTestBuilder"/>.</returns>
        IAndLocalRedirectTestBuilder To<TController>(Expression<Action<TController>> actionCall)
            where TController : class;

        /// <summary>
        /// Tests whether <see cref="LocalRedirectResult"/> redirects to specific asynchronous action.
        /// </summary>
        /// <typeparam name="TController">Type of expected redirect controller.</typeparam>
        /// <param name="actionCall">Method call expression indicating the expected asynchronous action.</param>
        /// <returns>The same <see cref="IAndLocalRedirectTestBuilder"/>.</returns>
        IAndLocalRedirectTestBuilder To<TController>(Expression<Func<TController, Task>> actionCall)
            where TController : class;
    }
}
