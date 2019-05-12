namespace MyTested.AspNetCore.Mvc
{
    using Builders.ActionResults.Redirect;
    using Builders.Base;
    using Builders.Contracts.ActionResults.Redirect;
    using Exceptions;
    using Internal.Contracts.ActionResults;
    using Microsoft.AspNetCore.Mvc;
    using Utilities.Extensions;
    using Utilities.Validators;

    /// <summary>
    /// Contains extension methods for <see cref="IRedirectTestBuilder"/>.
    /// </summary>
    public static class RedirectTestBuilderExtensions
    {
        /// <summary>
        /// Tests whether the <see cref="RedirectToActionResult"/>
        /// has specific action name.
        /// </summary>
        /// <param name="redirectTestBuilder">
        /// Instance of <see cref="IRedirectTestBuilder"/> type.
        /// </param>
        /// <param name="actionName">Expected action name.</param>
        /// <returns>The same <see cref="IAndRedirectTestBuilder"/>.</returns>
        public static IAndRedirectTestBuilder ToAction(
            this IRedirectTestBuilder redirectTestBuilder,
            string actionName)
        {
            var actualBuilder = GetRedirectTestBuilder<RedirectToActionResult>(redirectTestBuilder, "action name");

            RouteActionResultValidator.ValidateActionName(
                actualBuilder.ActionResult,
                actionName,
                actualBuilder.ThrowNewFailedValidationException);

            return actualBuilder;
        }

        /// <summary>
        /// Tests whether the <see cref="RedirectToActionResult"/>
        /// result has specific controller name.
        /// </summary>
        /// <param name="redirectTestBuilder">
        /// Instance of <see cref="IRedirectTestBuilder"/> type.
        /// </param>
        /// <param name="controllerName">Expected controller name.</param>
        /// <returns>The same <see cref="IAndRedirectTestBuilder"/>.</returns>
        public static IAndRedirectTestBuilder ToController(
            this IRedirectTestBuilder redirectTestBuilder,
            string controllerName)
        {
            var actualBuilder = GetRedirectTestBuilder<RedirectToActionResult>(redirectTestBuilder, "controller name");

            RouteActionResultValidator.ValidateControllerName(
                actualBuilder.ActionResult,
                controllerName,
                actualBuilder.ThrowNewFailedValidationException);

            return actualBuilder;
        }

        /// <summary>
        /// Tests whether the <see cref="RedirectToPageResult"/>
        /// result has specific page name.
        /// </summary>
        /// <param name="redirectTestBuilder">
        /// Instance of <see cref="IRedirectTestBuilder"/> type.
        /// </param>
        /// <param name="pageName">Expected page name.</param>
        /// <returns>The same <see cref="IAndRedirectTestBuilder"/>.</returns>
        public static IAndRedirectTestBuilder ToPage(
            this IRedirectTestBuilder redirectTestBuilder,
            string pageName)
        {
            var actualBuilder = GetRedirectTestBuilder<RedirectToPageResult>(redirectTestBuilder, "page name");

            var actualPageName = actualBuilder.ActionResult.PageName;

            if (pageName != actualPageName)
            {
                actualBuilder.ThrowNewFailedValidationException(
                    "to have",
                    $"'{pageName}' page name",
                    $"instead received {actualPageName.GetErrorMessageName()}");
            }

            return actualBuilder.ResultTestBuilder;
        }

        /// <summary>
        /// Tests whether the <see cref="RedirectToPageResult"/>
        /// result has specific page handler.
        /// </summary>
        /// <param name="redirectTestBuilder">
        /// Instance of <see cref="IRedirectTestBuilder"/> type.
        /// </param>
        /// <param name="pageHandler">Expected page handler.</param>
        /// <returns>The same <see cref="IAndRedirectTestBuilder"/>.</returns>
        public static IAndRedirectTestBuilder WithPageHandler(
            this IRedirectTestBuilder redirectTestBuilder,
            string pageHandler)
        {
            var actualBuilder = GetRedirectTestBuilder<RedirectToPageResult>(redirectTestBuilder, "page handler");

            var actualPageHandler = actualBuilder.ActionResult.PageHandler;

            if (pageHandler != actualPageHandler)
            {
                actualBuilder.ThrowNewFailedValidationException(
                    "page handler",
                    $"to be {pageHandler.GetErrorMessageName()}",
                    $"instead received {actualPageHandler.GetErrorMessageName()}");
            }

            return actualBuilder.ResultTestBuilder;
        }

        /// <summary>
        /// Tests whether the <see cref="RedirectToPageResult"/>
        /// result has specific URL protocol.
        /// </summary>
        /// <param name="redirectTestBuilder">
        /// Instance of <see cref="IRedirectTestBuilder"/> type.
        /// </param>
        /// <param name="protocol">Expected URL protocol.</param>
        /// <returns>The same <see cref="IAndRedirectTestBuilder"/>.</returns>
        public static IAndRedirectTestBuilder WithProtocol(
            this IRedirectTestBuilder redirectTestBuilder,
            string protocol)
        {
            var actualBuilder = GetRedirectTestBuilder<RedirectToPageResult>(redirectTestBuilder, "protocol");

            var actualProtocol = actualBuilder.ActionResult.Protocol;

            if (protocol != actualProtocol)
            {
                actualBuilder.ThrowNewFailedValidationException(
                    "protocol",
                    $"to be {protocol.GetErrorMessageName()}",
                    $"instead received {actualProtocol.GetErrorMessageName()}");
            }

            return actualBuilder.ResultTestBuilder;
        }

        /// <summary>
        /// Tests whether the <see cref="RedirectToPageResult"/>
        /// result has specific URL host.
        /// </summary>
        /// <param name="redirectTestBuilder">
        /// Instance of <see cref="IRedirectTestBuilder"/> type.
        /// </param>
        /// <param name="host">Expected URL host.</param>
        /// <returns>The same <see cref="IAndRedirectTestBuilder"/>.</returns>
        public static IAndRedirectTestBuilder WithHost(
            this IRedirectTestBuilder redirectTestBuilder,
            string host)
        {
            var actualBuilder = GetRedirectTestBuilder<RedirectToPageResult>(redirectTestBuilder, "host");

            var actualHost = actualBuilder.ActionResult.Host;

            if (host != actualHost)
            {
                actualBuilder.ThrowNewFailedValidationException(
                    "host",
                    $"to be {host.GetErrorMessageName()}",
                    $"instead received {actualHost.GetErrorMessageName()}");
            }

            return actualBuilder.ResultTestBuilder;
        }

        /// <summary>
        /// Tests whether the <see cref="RedirectToActionResult"/> or <see cref="RedirectToRouteResult"/>
        /// contains specific route value of the given type.
        /// </summary>
        /// <param name="redirectTestBuilder">
        /// Instance of <see cref="IRedirectTestBuilder"/> type.
        /// </param>
        /// <returns>The same <see cref="IAndRedirectTestBuilder"/>.</returns>
        public static IAndRedirectTestBuilder ContainingRouteValueOfType<TRouteValue>(
            this IRedirectTestBuilder redirectTestBuilder)
            => redirectTestBuilder
                .ContainingRouteValueOfType<IAndRedirectTestBuilder, TRouteValue>();

        /// <summary>
        /// Tests whether the <see cref="RedirectToActionResult"/> or <see cref="RedirectToRouteResult"/>
        /// contains specific route value of the given type with the provided key.
        /// </summary>
        /// <param name="redirectTestBuilder">
        /// Instance of <see cref="IRedirectTestBuilder"/> type.
        /// </param>
        /// <param name="key">Expected route key.</param>
        /// <returns>The same <see cref="IAndRedirectTestBuilder"/>.</returns>
        public static IAndRedirectTestBuilder ContainingRouteValueOfType<TRouteValue>(
            this IRedirectTestBuilder redirectTestBuilder,
            string key)
            => redirectTestBuilder
                .ContainingRouteValueOfType<IAndRedirectTestBuilder, TRouteValue>(key);

        /// <summary>
        /// Tests whether the <see cref="RedirectToActionResult"/> or <see cref="RedirectToRouteResult"/>
        /// has the same <see cref="IUrlHelper"/> type as the provided one.
        /// </summary>
        /// <param name="redirectTestBuilder">
        /// Instance of <see cref="IRedirectTestBuilder"/> type.
        /// </param>
        /// <returns>The same <see cref="IAndRedirectTestBuilder"/>.</returns>
        public static IAndRedirectTestBuilder WithUrlHelperOfType<TUrlHelper>(
            this IRedirectTestBuilder redirectTestBuilder)
            where TUrlHelper : IUrlHelper
            => redirectTestBuilder
                .WithUrlHelperOfType<IAndRedirectTestBuilder, TUrlHelper>();

        /// <summary>
        /// Tests whether the <see cref="RedirectToActionResult"/> or <see cref="RedirectToRouteResult"/>
        /// has the same fragment as the provided one.
        /// </summary>
        /// <param name="redirectTestBuilder">
        /// Instance of <see cref="IRedirectTestBuilder"/> type.
        /// </param>
        /// <param name="fragment">Expected fragment.</param>
        /// <returns>The same <see cref="IAndRedirectTestBuilder"/>.</returns>
        public static IAndRedirectTestBuilder WithFragment(
            this IRedirectTestBuilder redirectTestBuilder,
            string fragment)
        {
            var actualBuilder = (IBaseTestBuilderWithRedirectResultInternal<IAndRedirectTestBuilder>)redirectTestBuilder;

            RuntimeBinderValidator.ValidateBinding(() =>
            {
                var actualFragment = actualBuilder
                    .TestContext
                    .MethodResult
                    .AsDynamic()
                    .Fragment;

                if (fragment != actualFragment)
                {
                    actualBuilder.ThrowNewFailedValidationException(
                        "fragment",
                        $"to be {fragment.GetErrorMessageName()}",
                        $"instead received {actualFragment.GetErrorMessageName()}");
                }
            });

            return actualBuilder.ResultTestBuilder;
        }

        private static RedirectTestBuilder<TRedirectResult> GetRedirectTestBuilder<TRedirectResult>(
            IRedirectTestBuilder redirectTestBuilder,
            string containment)
            where TRedirectResult : ActionResult
        {
            var actualRedirectTestBuilder = redirectTestBuilder as RedirectTestBuilder<TRedirectResult>;

            if (actualRedirectTestBuilder == null)
            {
                var redirectTestBuilderBase = (BaseTestBuilderWithComponent)redirectTestBuilder;

                throw new RedirectResultAssertionException(string.Format(
                    "{0} redirect result to contain {1}, but such could not be found.",
                    redirectTestBuilderBase.TestContext.ExceptionMessagePrefix,
                    containment));
            }

            return actualRedirectTestBuilder;
        }
    }
}
