namespace MyTested.AspNetCore.Mvc
{
    using System;
    using Builders.ActionResults.Created;
    using Builders.Base;
    using Builders.Contracts.ActionResults.Created;
    using Builders.Contracts.Uri;
    using Exceptions;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Formatters;
    using Utilities.Validators;

    /// <summary>
    /// Contains extension methods for <see cref="ICreatedTestBuilder"/>.
    /// </summary>
    public static class CreatedTestBuilderExtensions
    {
        private const string Location = "location";
        private const string ControllerName = "controller name";
        private const string ActionName = "action name";

        /// <summary>
        /// Tests whether the <see cref="CreatedResult"/>
        /// has specific location provided by string.
        /// </summary>
        /// <param name="createdTestBuilder">
        /// Instance of <see cref="ICreatedTestBuilder"/> type.
        /// </param>
        /// <param name="location">Expected location as string.</param>
        /// <returns>The same <see cref="IAndCreatedTestBuilder"/>.</returns>
        public static IAndCreatedTestBuilder AtLocation(
            this ICreatedTestBuilder createdTestBuilder,
            string location)
        {
            var actualBuilder = GetCreatedTestBuilder<CreatedResult>(createdTestBuilder, Location);

            var uri = LocationValidator.ValidateAndGetWellFormedUriString(
                location, 
                actualBuilder.ThrowNewFailedValidationException);

            return actualBuilder.AtLocation(uri);
        }

        /// <summary>
        /// Tests whether the <see cref="CreatedResult"/>
        /// location passes the given assertions.
        /// </summary>
        /// <param name="createdTestBuilder">
        /// Instance of <see cref="ICreatedTestBuilder"/> type.
        /// </param>
        /// <param name="assertions">Action containing all assertions for the location.</param>
        /// <returns>The same <see cref="IAndCreatedTestBuilder"/>.</returns>
        public static IAndCreatedTestBuilder AtLocationPassing(
            this ICreatedTestBuilder createdTestBuilder, 
            Action<string> assertions)
        {
            var actualBuilder = GetCreatedTestBuilder<CreatedResult>(createdTestBuilder, Location);
            
            assertions(actualBuilder.ActionResult.Location);

            return actualBuilder;
        }

        /// <summary>
        /// Tests whether the <see cref="CreatedResult"/>
        /// location passes the given predicate.
        /// </summary>
        /// <param name="createdTestBuilder">
        /// Instance of <see cref="ICreatedTestBuilder"/> type.
        /// </param>
        /// <param name="predicate">Predicate testing the location.</param>
        /// <returns>The same <see cref="IAndCreatedTestBuilder"/>.</returns>
        public static IAndCreatedTestBuilder AtLocationPassing(
            this ICreatedTestBuilder createdTestBuilder, 
            Func<string, bool> predicate)
        {
            var actualBuilder = GetCreatedTestBuilder<CreatedResult>(createdTestBuilder, Location);
            
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
        /// Tests whether the <see cref="CreatedResult"/>
        /// has specific location provided by <see cref="Uri"/>.
        /// </summary>
        /// <param name="createdTestBuilder">
        /// Instance of <see cref="ICreatedTestBuilder"/> type.
        /// </param>
        /// <param name="location">Expected location as <see cref="Uri"/>.</param>
        /// <returns>The same <see cref="IAndCreatedTestBuilder"/>.</returns>
        public static IAndCreatedTestBuilder AtLocation(
            this ICreatedTestBuilder createdTestBuilder, 
            Uri location)
        {
            var actualBuilder = GetCreatedTestBuilder<CreatedResult>(createdTestBuilder, Location);

            LocationValidator.ValidateUri(
                actualBuilder.ActionResult,
                location.OriginalString,
                actualBuilder.ThrowNewFailedValidationException);

            return actualBuilder;
        }

        /// <summary>
        /// Tests whether the <see cref="CreatedResult"/>
        /// has specific location provided by builder.
        /// </summary>
        /// <param name="createdTestBuilder">
        /// Instance of <see cref="ICreatedTestBuilder"/> type.
        /// </param>
        /// <param name="uriTestBuilder">Builder for expected URI.</param>
        /// <returns>The same <see cref="IAndCreatedTestBuilder"/>.</returns>
        public static IAndCreatedTestBuilder AtLocation(
            this ICreatedTestBuilder createdTestBuilder, 
            Action<IUriTestBuilder> uriTestBuilder)
        {
            var actualBuilder = GetCreatedTestBuilder<CreatedResult>(createdTestBuilder, Location);

            LocationValidator.ValidateLocation(
                actualBuilder.ActionResult,
                uriTestBuilder,
                actualBuilder.ThrowNewFailedValidationException);

            return actualBuilder;
        }

        /// <summary>
        /// Tests whether the <see cref="CreatedAtActionResult"/>
        /// has specific action name.
        /// </summary>
        /// <param name="createdTestBuilder">
        /// Instance of <see cref="ICreatedTestBuilder"/> type.
        /// </param>
        /// <param name="actionName">Expected action name.</param>
        /// <returns>The same <see cref="IAndCreatedTestBuilder"/>.</returns>
        public static IAndCreatedTestBuilder AtAction(
            this ICreatedTestBuilder createdTestBuilder, 
            string actionName)
        {
            var actualBuilder = GetCreatedTestBuilder<CreatedAtActionResult>(
                createdTestBuilder,
                ActionName);
            
            RouteActionResultValidator.ValidateActionName(
                actualBuilder.ActionResult,
                actionName,
                actualBuilder.ThrowNewFailedValidationException);

            return actualBuilder;
        }

        /// <summary>
        /// Tests whether the <see cref="CreatedAtActionResult"/>
        /// has specific controller name.
        /// </summary>
        /// <param name="createdTestBuilder">
        /// Instance of <see cref="ICreatedTestBuilder"/> type.
        /// </param>
        /// <param name="controllerName">Expected controller name.</param>
        /// <returns>The same <see cref="IAndCreatedTestBuilder"/>.</returns>
        public static IAndCreatedTestBuilder AtController(
            this ICreatedTestBuilder createdTestBuilder, 
            string controllerName)
        {
            var actualBuilder = GetCreatedTestBuilder<CreatedAtActionResult>(
                createdTestBuilder,
                ControllerName);
            
            RouteActionResultValidator.ValidateControllerName(
                actualBuilder.ActionResult,
                controllerName,
                actualBuilder.ThrowNewFailedValidationException);

            return actualBuilder;
        }

        /// <summary>
        /// Tests whether the created result
        /// contains <see cref="IOutputFormatter"/> of the provided type.
        /// </summary>
        /// <param name="createdTestBuilder">
        /// Instance of <see cref="ICreatedTestBuilder"/> type.
        /// </param>
        /// <returns>The same <see cref="IAndCreatedTestBuilder"/>.</returns>
        public static IAndCreatedTestBuilder ContainingOutputFormatterOfType<TOutputFormatter>(
            this ICreatedTestBuilder createdTestBuilder)
            where TOutputFormatter : IOutputFormatter
            => createdTestBuilder
                .ContainingOutputFormatterOfType<IAndCreatedTestBuilder, TOutputFormatter>();

        /// <summary>
        /// Tests whether the <see cref="CreatedAtActionResult"/> or <see cref="CreatedAtRouteResult"/>
        /// contains specific route value of the given type.
        /// </summary>
        /// <param name="createdTestBuilder">
        /// Instance of <see cref="ICreatedTestBuilder"/> type.
        /// </param>
        /// <returns>The same <see cref="IAndCreatedTestBuilder"/>.</returns>
        public static IAndCreatedTestBuilder ContainingRouteValueOfType<TRouteValue>(
            this ICreatedTestBuilder createdTestBuilder)
            => createdTestBuilder
                .ContainingRouteValueOfType<IAndCreatedTestBuilder, TRouteValue>();

        /// <summary>
        /// Tests whether the <see cref="CreatedAtActionResult"/> or <see cref="CreatedAtRouteResult"/>
        /// contains specific route value of the given type with the provided key.
        /// </summary>
        /// <param name="createdTestBuilder">
        /// Instance of <see cref="ICreatedTestBuilder"/> type.
        /// </param>
        /// <param name="key">Expected route key.</param>
        /// <returns>The same <see cref="IAndCreatedTestBuilder"/>.</returns>
        public static IAndCreatedTestBuilder ContainingRouteValueOfType<TRouteValue>(
            this ICreatedTestBuilder createdTestBuilder,
            string key)
            => createdTestBuilder
                .ContainingRouteValueOfType<IAndCreatedTestBuilder, TRouteValue>(key);

        /// <summary>
        /// Tests whether the <see cref="CreatedAtActionResult"/> or <see cref="CreatedAtRouteResult"/>
        /// has the same <see cref="IUrlHelper"/> type as the provided one.
        /// </summary>
        /// <param name="createdTestBuilder">
        /// Instance of <see cref="ICreatedTestBuilder"/> type.
        /// </param>
        /// <returns>The same <see cref="IAndCreatedTestBuilder"/>.</returns>
        public static IAndCreatedTestBuilder WithUrlHelperOfType<TUrlHelper>(
            this ICreatedTestBuilder createdTestBuilder)
            where TUrlHelper : IUrlHelper
            => createdTestBuilder
                .WithUrlHelperOfType<IAndCreatedTestBuilder, TUrlHelper>();

        private static CreatedTestBuilder<TCreatedResult> GetCreatedTestBuilder<TCreatedResult>(
            ICreatedTestBuilder createdTestBuilder,
            string containment)
            where TCreatedResult : ObjectResult
        {
            var actualCreatedTestBuilder = createdTestBuilder as CreatedTestBuilder<TCreatedResult>;

            if (actualCreatedTestBuilder == null)
            {
                var createdTestBuilderBase = (BaseTestBuilderWithComponent)createdTestBuilder;

                throw new CreatedResultAssertionException(string.Format(
                    "{0} created result to contain {1}, but it could not be found.",
                    createdTestBuilderBase.TestContext.ExceptionMessagePrefix,
                    containment));
            }

            return actualCreatedTestBuilder;
        }
    }
}
