namespace MyTested.AspNetCore.Mvc
{
    using System;
    using Builders.ActionResults.Accepted;
    using Builders.Base;
    using Builders.Contracts.ActionResults.Accepted;
    using Builders.Contracts.Uri;
    using Exceptions;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Formatters;
    using Utilities.Validators;

    /// <summary>
    /// Contains extension methods for <see cref="IAcceptedTestBuilder"/>.
    /// </summary>
    public static class AcceptedTestBuilderExtensions
    {
        private const string Location = "location";

        /// <summary>
        /// Tests whether the <see cref="AcceptedResult"/>
        /// has specific location provided by string.
        /// </summary>
        /// <param name="acceptedTestBuilder">
        /// Instance of <see cref="IAcceptedTestBuilder"/> type.
        /// </param>
        /// <param name="location">Expected location as string.</param>
        /// <returns>The same <see cref="IAndAcceptedTestBuilder"/>.</returns>
        public static IAndAcceptedTestBuilder AtLocation(
            this IAcceptedTestBuilder acceptedTestBuilder,
            string location)
        {
            var actualBuilder = GetAcceptedTestBuilder<AcceptedResult>(acceptedTestBuilder, Location);

            var uri = LocationValidator.ValidateAndGetWellFormedUriString(
                location,
                actualBuilder.ThrowNewFailedValidationException);

            return actualBuilder.AtLocation(uri);
        }

        /// <summary>
        /// Tests whether the <see cref="AcceptedResult"/>
        /// location passes the given assertions.
        /// </summary>
        /// <param name="acceptedTestBuilder">
        /// Instance of <see cref="IAcceptedTestBuilder"/> type.
        /// </param>
        /// <param name="assertions">Action containing all assertions for the location.</param>
        /// <returns>The same <see cref="IAndAcceptedTestBuilder"/>.</returns>
        public static IAndAcceptedTestBuilder AtLocationPassing(
            this IAcceptedTestBuilder acceptedTestBuilder,
            Action<string> assertions)
        {
            var actualBuilder = GetAcceptedTestBuilder<AcceptedResult>(acceptedTestBuilder, Location);

            assertions(actualBuilder.ActionResult.Location);

            return actualBuilder;
        }

        /// <summary>
        /// Tests whether the <see cref="AcceptedResult"/>
        /// location passes the given predicate.
        /// </summary>
        /// <param name="acceptedTestBuilder">
        /// Instance of <see cref="IAcceptedTestBuilder"/> type.
        /// </param>
        /// <param name="predicate">Predicate testing the location.</param>
        /// <returns>The same <see cref="IAndAcceptedTestBuilder"/>.</returns>
        public static IAndAcceptedTestBuilder AtLocationPassing(
            this IAcceptedTestBuilder acceptedTestBuilder,
            Func<string, bool> predicate)
        {
            var actualBuilder = GetAcceptedTestBuilder<AcceptedResult>(acceptedTestBuilder, Location);

            var location = actualBuilder.ActionResult.Location;

            if (!predicate(location))
            {
                actualBuilder.ThrowNewFailedValidationException(
                    $"location ('{location}')",
                    "to pass the given predicate",
                    "it failed");
            }

            return actualBuilder;
        }

        /// <summary>
        /// Tests whether the <see cref="AcceptedResult"/>
        /// has specific location provided by <see cref="Uri"/>.
        /// </summary>
        /// <param name="acceptedTestBuilder">
        /// Instance of <see cref="IAcceptedTestBuilder"/> type.
        /// </param>
        /// <param name="location">Expected location as <see cref="Uri"/>.</param>
        /// <returns>The same <see cref="IAndAcceptedTestBuilder"/>.</returns>
        public static IAndAcceptedTestBuilder AtLocation(
            this IAcceptedTestBuilder acceptedTestBuilder,
            Uri location)
        {
            var actualBuilder = GetAcceptedTestBuilder<AcceptedResult>(acceptedTestBuilder, Location);

            LocationValidator.ValidateUri(
                actualBuilder.ActionResult,
                location.OriginalString,
                actualBuilder.ThrowNewFailedValidationException);

            return actualBuilder;
        }

        /// <summary>
        /// Tests whether the <see cref="AcceptedResult"/>
        /// has specific location provided by builder.
        /// </summary>
        /// <param name="acceptedTestBuilder">
        /// Instance of <see cref="IAcceptedTestBuilder"/> type.
        /// </param>
        /// <param name="uriTestBuilder">Builder for expected URI.</param>
        /// <returns>The same <see cref="IAndAcceptedTestBuilder"/>.</returns>
        public static IAndAcceptedTestBuilder AtLocation(
            this IAcceptedTestBuilder acceptedTestBuilder,
            Action<IUriTestBuilder> uriTestBuilder)
        {
            var actualBuilder = GetAcceptedTestBuilder<AcceptedResult>(acceptedTestBuilder, Location);

            LocationValidator.ValidateLocation(
                actualBuilder.ActionResult,
                uriTestBuilder,
                actualBuilder.ThrowNewFailedValidationException);

            return actualBuilder;
        }

        /// <summary>
        /// Tests whether the <see cref="AcceptedAtActionResult"/>
        /// has specific action name.
        /// </summary>
        /// <param name="acceptedTestBuilder">
        /// Instance of <see cref="IAcceptedTestBuilder"/> type.
        /// </param>
        /// <param name="actionName">Expected action name.</param>
        /// <returns>The same <see cref="IAndAcceptedTestBuilder"/>.</returns>
        public static IAndAcceptedTestBuilder AtAction(
            this IAcceptedTestBuilder acceptedTestBuilder,
            string actionName)
        {
            var actualBuilder = GetAcceptedTestBuilder<AcceptedAtActionResult>(
                acceptedTestBuilder,
                "action name");

            RouteActionResultValidator.ValidateActionName(
                actualBuilder.ActionResult,
                actionName,
                actualBuilder.ThrowNewFailedValidationException);

            return actualBuilder;
        }

        /// <summary>
        /// Tests whether the <see cref="AcceptedAtActionResult"/>
        /// has specific controller name.
        /// </summary>
        /// <param name="acceptedTestBuilder">
        /// Instance of <see cref="IAcceptedTestBuilder"/> type.
        /// </param>
        /// <param name="controllerName">Expected controller name.</param>
        /// <returns>The same <see cref="IAndAcceptedTestBuilder"/>.</returns>
        public static IAndAcceptedTestBuilder AtController(
            this IAcceptedTestBuilder acceptedTestBuilder,
            string controllerName)
        {
            var actualBuilder = GetAcceptedTestBuilder<AcceptedAtActionResult>(
                acceptedTestBuilder,
                "controller name");

            RouteActionResultValidator.ValidateControllerName(
                actualBuilder.ActionResult,
                controllerName,
                actualBuilder.ThrowNewFailedValidationException);

            return actualBuilder;
        }

        /// <summary>
        /// Tests whether the accepted result
        /// contains <see cref="IOutputFormatter"/> of the provided type.
        /// </summary>
        /// <param name="acceptedTestBuilder">
        /// Instance of <see cref="IAcceptedTestBuilder"/> type.
        /// </param>
        /// <returns>The same <see cref="IAndAcceptedTestBuilder"/>.</returns>
        public static IAndAcceptedTestBuilder ContainingOutputFormatterOfType<TOutputFormatter>(
            this IAcceptedTestBuilder acceptedTestBuilder)
            where TOutputFormatter : IOutputFormatter
            => acceptedTestBuilder
                .ContainingOutputFormatterOfType<IAndAcceptedTestBuilder, TOutputFormatter>();

        /// <summary>
        /// Tests whether the <see cref="AcceptedAtActionResult"/> or <see cref="AcceptedAtRouteResult"/>
        /// contains specific route value of the given type.
        /// </summary>
        /// <param name="acceptedTestBuilder">
        /// Instance of <see cref="IAcceptedTestBuilder"/> type.
        /// </param>
        /// <returns>The same <see cref="IAndAcceptedTestBuilder"/>.</returns>
        public static IAndAcceptedTestBuilder ContainingRouteValueOfType<TRouteValue>(
            this IAcceptedTestBuilder acceptedTestBuilder)
            => acceptedTestBuilder
                .ContainingRouteValueOfType<IAndAcceptedTestBuilder, TRouteValue>();

        /// <summary>
        /// Tests whether the <see cref="AcceptedAtActionResult"/> or <see cref="AcceptedAtRouteResult"/>
        /// contains specific route value of the given type with the provided key.
        /// </summary>
        /// <param name="acceptedTestBuilder">
        /// Instance of <see cref="IAcceptedTestBuilder"/> type.
        /// </param>
        /// <param name="key">Expected route key.</param>
        /// <returns>The same <see cref="IAndAcceptedTestBuilder"/>.</returns>
        public static IAndAcceptedTestBuilder ContainingRouteValueOfType<TRouteValue>(
            this IAcceptedTestBuilder acceptedTestBuilder,
            string key)
            => acceptedTestBuilder
                .ContainingRouteValueOfType<IAndAcceptedTestBuilder, TRouteValue>(key);

        /// <summary>
        /// Tests whether the <see cref="AcceptedAtActionResult"/> or <see cref="AcceptedAtRouteResult"/>
        /// has the same <see cref="IUrlHelper"/> type as the provided one.
        /// </summary>
        /// <param name="acceptedTestBuilder">
        /// Instance of <see cref="IAcceptedTestBuilder"/> type.
        /// </param>
        /// <returns>The same <see cref="IAndAcceptedTestBuilder"/>.</returns>
        public static IAndAcceptedTestBuilder WithUrlHelperOfType<TUrlHelper>(
            this IAcceptedTestBuilder acceptedTestBuilder)
            where TUrlHelper : IUrlHelper
            => acceptedTestBuilder
                .WithUrlHelperOfType<IAndAcceptedTestBuilder, TUrlHelper>();

        private static AcceptedTestBuilder<TAcceptedResult> GetAcceptedTestBuilder<TAcceptedResult>(
            IAcceptedTestBuilder acceptedTestBuilder,
            string containment)
            where TAcceptedResult : ObjectResult
        {
            var actualAcceptedTestBuilder = acceptedTestBuilder as AcceptedTestBuilder<TAcceptedResult>;

            if (actualAcceptedTestBuilder == null)
            {
                var acceptedTestBuilderBase = (BaseTestBuilderWithComponent)acceptedTestBuilder;

                throw new AcceptedResultAssertionException(string.Format(
                    "{0} accepted result to contain {1}, but such could not be found.",
                    acceptedTestBuilderBase.TestContext.ExceptionMessagePrefix,
                    containment));
            }

            return actualAcceptedTestBuilder;
        }
    }
}
