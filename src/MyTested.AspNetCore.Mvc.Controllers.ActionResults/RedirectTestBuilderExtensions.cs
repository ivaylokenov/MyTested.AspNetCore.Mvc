namespace MyTested.AspNetCore.Mvc
{
    using Builders.ActionResults.Redirect;
    using Builders.Base;
    using Builders.Contracts.ActionResults.Redirect;
    using Exceptions;
    using Microsoft.AspNetCore.Mvc;
    using Utilities.Validators;

    /// <summary>
    /// Contains extension methods for <see cref="IRedirectTestBuilder"/>.
    /// </summary>
    public static class RedirectTestBuilderExtensions
    {
        private const string ControllerName = "controller name";
        private const string ActionName = "action name";

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
            var actualBuilder = GetRedirectTestBuilder<RedirectToActionResult>(redirectTestBuilder, ActionName);
            
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
            var actualBuilder = GetRedirectTestBuilder<RedirectToActionResult>(redirectTestBuilder, ControllerName);

            RouteActionResultValidator.ValidateControllerName(
                actualBuilder.ActionResult,
                controllerName,
                actualBuilder.ThrowNewFailedValidationException);

            return actualBuilder;
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
                    "{0} redirect result to contain {1}, but it could not be found.",
                    redirectTestBuilderBase.TestContext.ExceptionMessagePrefix,
                    containment));
            }

            return actualRedirectTestBuilder;
        }
    }
}
