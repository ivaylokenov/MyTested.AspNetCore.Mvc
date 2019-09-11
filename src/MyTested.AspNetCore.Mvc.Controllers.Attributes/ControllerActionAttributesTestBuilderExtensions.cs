namespace MyTested.AspNetCore.Mvc
{
    using Builders.Attributes;
    using Builders.Contracts.Attributes;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Utilities.Extensions;
    using Utilities.Validators;

    /// <summary>
    /// Contains extension methods for <see cref="IControllerActionAttributesTestBuilder{TAttributesTestBuilder}"/>.
    /// </summary>
    public static class ControllerActionAttributesTestBuilderExtensions
    {
        /// <summary>
        /// Tests whether the collected attributes contain <see cref="RouteAttribute"/>.
        /// </summary>
        /// <param name="controllerActionAttributesTestBuilder">
        /// Instance of <see cref="IControllerActionAttributesTestBuilder{TAttributesTestBuilder}"/> type.
        /// </param>
        /// <param name="template">Expected overridden route template of the controller.</param>
        /// <param name="withName">Optional expected route name.</param>
        /// <param name="withOrder">Optional expected route order.</param>
        /// <returns>The same attributes test builder.</returns>
        public static TAttributesTestBuilder ChangingRouteTo<TAttributesTestBuilder>(
            this IControllerActionAttributesTestBuilder<TAttributesTestBuilder> controllerActionAttributesTestBuilder,
            string template,
            string withName = null,
            int? withOrder = null)
            where TAttributesTestBuilder : IControllerActionAttributesTestBuilder<TAttributesTestBuilder>
            => controllerActionAttributesTestBuilder
                .ChangingRouteTo(route => route
                    .WithTemplate(template)
                    .WithName(withName)
                    .WithOrder(withOrder ?? 0));

        /// <summary>
        /// Tests whether the collected attributes contain <see cref="RouteAttribute"/>.
        /// </summary>
        /// <param name="controllerActionAttributesTestBuilder">
        /// Instance of <see cref="IControllerActionAttributesTestBuilder{TAttributesTestBuilder}"/> type.
        /// </param>
        /// <param name="routeAttributeBuilder">Expected <see cref="RouteAttribute"/> builder.</param>
        /// <returns>The same attributes test builder.</returns>
        public static TAttributesTestBuilder ChangingRouteTo<TAttributesTestBuilder>(
            this IControllerActionAttributesTestBuilder<TAttributesTestBuilder> controllerActionAttributesTestBuilder,
            Action<IRouteAttributeTestBuilder> routeAttributeBuilder)
            where TAttributesTestBuilder : IControllerActionAttributesTestBuilder<TAttributesTestBuilder>
        {
            var actualBuilder = (BaseAttributesTestBuilder<TAttributesTestBuilder>)controllerActionAttributesTestBuilder;

            actualBuilder.ContainingAttributeOfType<RouteAttribute>();

            actualBuilder.Validations.Add(attrs =>
            {
                var newRouteAttributeTestBuilder = new RouteAttributeTestBuilder(
                    actualBuilder.TestContext,
                    actualBuilder.ThrowNewAttributeAssertionException);

                routeAttributeBuilder(newRouteAttributeTestBuilder);

                var expectedRouteAttribute = newRouteAttributeTestBuilder.GetAttribute();
                var actualRouteAttribute = actualBuilder.GetAttributeOfType<RouteAttribute>(attrs);

                var validations = newRouteAttributeTestBuilder.GetAttributeValidations();
                validations.ForEach(v => v(expectedRouteAttribute, actualRouteAttribute));
            });

            return actualBuilder.AttributesTestBuilder;
        }

        /// <summary>
        /// Tests whether the collected attributes contain <see cref="AreaAttribute"/>.
        /// </summary>
        /// <param name="controllerActionAttributesTestBuilder">
        /// Instance of <see cref="IControllerActionAttributesTestBuilder{TAttributesTestBuilder}"/> type.
        /// </param>
        /// <param name="areaName">Expected area name.</param>
        /// <returns>The same attributes test builder.</returns>
        public static TAttributesTestBuilder SpecifyingArea<TAttributesTestBuilder>(
            this IControllerActionAttributesTestBuilder<TAttributesTestBuilder> controllerActionAttributesTestBuilder,
            string areaName)
            where TAttributesTestBuilder : IControllerActionAttributesTestBuilder<TAttributesTestBuilder>
        {
            var actualBuilder = (BaseAttributesTestBuilder<TAttributesTestBuilder>)controllerActionAttributesTestBuilder;

            actualBuilder.ContainingAttributeOfType<AreaAttribute>();

            actualBuilder.Validations.Add(attrs =>
            {
                var areaAttribute = actualBuilder.GetAttributeOfType<AreaAttribute>(attrs);
                var actualAreaName = areaAttribute.RouteValue;

                if (areaName != actualAreaName)
                {
                    actualBuilder.ThrowNewAttributeAssertionException(
                        $"'{areaName}' area",
                        $"in fact found '{actualAreaName}'");
                }
            });

            return actualBuilder.AttributesTestBuilder;
        }

        /// <summary>
        /// Tests whether the collected attributes contain <see cref="ConsumesAttribute"/>.
        /// </summary>
        /// <param name="controllerActionAttributesTestBuilder">
        /// Instance of <see cref="IControllerActionAttributesTestBuilder{TAttributesTestBuilder}"/> type.
        /// </param>
        /// <param name="ofContentType">Expected content type.</param>
        /// <returns>The same attributes test builder.</returns>
        public static TAttributesTestBuilder SpecifyingConsumption<TAttributesTestBuilder>(
            this IControllerActionAttributesTestBuilder<TAttributesTestBuilder> controllerActionAttributesTestBuilder,
            string ofContentType)
            where TAttributesTestBuilder : IControllerActionAttributesTestBuilder<TAttributesTestBuilder>
        {
            var actualBuilder = (BaseAttributesTestBuilder<TAttributesTestBuilder>)controllerActionAttributesTestBuilder;

            actualBuilder.ContainingAttributeOfType<ConsumesAttribute>();

            actualBuilder.Validations.Add(attrs =>
            {
                var consumesAttribute = actualBuilder.GetAttributeOfType<ConsumesAttribute>(attrs);

                ContentTypeValidator.ValidateAttributeContainingOfContentType(
                    consumesAttribute,
                    ofContentType,
                    actualBuilder.ThrowNewAttributeAssertionException);
            });

            return actualBuilder.AttributesTestBuilder;
        }

        /// <summary>
        /// Tests whether the collected attributes contain <see cref="ConsumesAttribute"/>.
        /// </summary>
        /// <param name="controllerActionAttributesTestBuilder">
        /// Instance of <see cref="IControllerActionAttributesTestBuilder{TAttributesTestBuilder}"/> type.
        /// </param>
        /// <param name="ofContentTypes">Expected content types.</param>
        /// <returns>The same attributes test builder.</returns>
        public static TAttributesTestBuilder SpecifyingConsumption<TAttributesTestBuilder>(
            this IControllerActionAttributesTestBuilder<TAttributesTestBuilder> controllerActionAttributesTestBuilder,
            IEnumerable<string> ofContentTypes)
            where TAttributesTestBuilder : IControllerActionAttributesTestBuilder<TAttributesTestBuilder>
        {
            var actualBuilder = (BaseAttributesTestBuilder<TAttributesTestBuilder>)controllerActionAttributesTestBuilder;

            actualBuilder.ContainingAttributeOfType<ConsumesAttribute>();

            actualBuilder.Validations.Add(attrs =>
            {
                var consumesAttribute = actualBuilder.GetAttributeOfType<ConsumesAttribute>(attrs);

                ContentTypeValidator.ValidateAttributeContentTypes(
                    consumesAttribute,
                    ofContentTypes,
                    actualBuilder.ThrowNewAttributeAssertionException);
            });

            return actualBuilder.AttributesTestBuilder;
        }

        /// <summary>
        /// Tests whether the collected attributes contain <see cref="ConsumesAttribute"/>.
        /// </summary>
        /// <param name="controllerActionAttributesTestBuilder">
        /// Instance of <see cref="IControllerActionAttributesTestBuilder{TAttributesTestBuilder}"/> type.
        /// </param>
        /// <param name="ofContentType">Expected content type.</param>
        /// <param name="withOtherContentTypes">Expected other content types.</param>
        /// <returns>The same attributes test builder.</returns>
        public static TAttributesTestBuilder SpecifyingConsumption<TAttributesTestBuilder>(
            this IControllerActionAttributesTestBuilder<TAttributesTestBuilder> controllerActionAttributesTestBuilder,
            string ofContentType,
            params string[] withOtherContentTypes)
            where TAttributesTestBuilder : IControllerActionAttributesTestBuilder<TAttributesTestBuilder>
            => controllerActionAttributesTestBuilder
                .SpecifyingConsumption(new List<string>(withOtherContentTypes) { ofContentType });

        /// <summary>
        /// Tests whether the collected attributes contain <see cref="ProducesAttribute"/>.
        /// </summary>
        /// <param name="controllerActionAttributesTestBuilder">
        /// Instance of <see cref="IControllerActionAttributesTestBuilder{TAttributesTestBuilder}"/> type.
        /// </param>
        /// <param name="ofContentType">Expected content type.</param>
        /// <returns>The same attributes test builder.</returns>
        public static TAttributesTestBuilder SpecifyingProduction<TAttributesTestBuilder>(
            this IControllerActionAttributesTestBuilder<TAttributesTestBuilder> controllerActionAttributesTestBuilder,
            string ofContentType)
            where TAttributesTestBuilder : IControllerActionAttributesTestBuilder<TAttributesTestBuilder>
        {
            var actualBuilder = (BaseAttributesTestBuilder<TAttributesTestBuilder>)controllerActionAttributesTestBuilder;

            actualBuilder.ContainingAttributeOfType<ProducesAttribute>();

            actualBuilder.Validations.Add(attrs =>
            {
                var producesAttribute = actualBuilder.GetAttributeOfType<ProducesAttribute>(attrs);

                ContentTypeValidator.ValidateAttributeContainingOfContentType(
                    producesAttribute,
                    ofContentType,
                    actualBuilder.ThrowNewAttributeAssertionException);
            });

            return actualBuilder.AttributesTestBuilder;
        }

        /// <summary>
        /// Tests whether the collected attributes contain <see cref="ProducesAttribute"/>.
        /// </summary>
        /// <param name="controllerActionAttributesTestBuilder">
        /// Instance of <see cref="IControllerActionAttributesTestBuilder{TAttributesTestBuilder}"/> type.
        /// </param>
        /// <param name="ofContentTypes">Expected content types.</param>
        /// <returns>The same attributes test builder.</returns>
        public static TAttributesTestBuilder SpecifyingProduction<TAttributesTestBuilder>(
            this IControllerActionAttributesTestBuilder<TAttributesTestBuilder> controllerActionAttributesTestBuilder,
            IEnumerable<string> ofContentTypes)
            where TAttributesTestBuilder : IControllerActionAttributesTestBuilder<TAttributesTestBuilder>
        {
            var actualBuilder = (BaseAttributesTestBuilder<TAttributesTestBuilder>)controllerActionAttributesTestBuilder;

            actualBuilder.ContainingAttributeOfType<ProducesAttribute>();

            actualBuilder.Validations.Add(attrs =>
            {
                var consumesAttribute = actualBuilder.GetAttributeOfType<ProducesAttribute>(attrs);

                ContentTypeValidator.ValidateAttributeContentTypes(
                    consumesAttribute,
                    ofContentTypes,
                    actualBuilder.ThrowNewAttributeAssertionException);
            });

            return actualBuilder.AttributesTestBuilder;
        }

        /// <summary>
        /// Tests whether the collected attributes contain <see cref="ProducesAttribute"/>.
        /// </summary>
        /// <param name="controllerActionAttributesTestBuilder">
        /// Instance of <see cref="IControllerActionAttributesTestBuilder{TAttributesTestBuilder}"/> type.
        /// </param>
        /// <param name="ofContentType">Expected content type.</param>
        /// <param name="withOtherContentTypes">Expected other content types.</param>
        /// <returns>The same attributes test builder.</returns>
        public static TAttributesTestBuilder SpecifyingProduction<TAttributesTestBuilder>(
            this IControllerActionAttributesTestBuilder<TAttributesTestBuilder> controllerActionAttributesTestBuilder,
            string ofContentType,
            params string[] withOtherContentTypes)
            where TAttributesTestBuilder : IControllerActionAttributesTestBuilder<TAttributesTestBuilder>
            => controllerActionAttributesTestBuilder
                .SpecifyingProduction(new List<string>(withOtherContentTypes) { ofContentType });

        /// <summary>
        /// Tests whether the collected attributes contain <see cref="ProducesAttribute"/>.
        /// </summary>
        /// <param name="controllerActionAttributesTestBuilder">
        /// Instance of <see cref="IControllerActionAttributesTestBuilder{TAttributesTestBuilder}"/> type.
        /// </param>
        /// <param name="withType">Expected type.</param>
        /// <returns>The same attributes test builder.</returns>
        public static TAttributesTestBuilder SpecifyingProduction<TAttributesTestBuilder>(
            this IControllerActionAttributesTestBuilder<TAttributesTestBuilder> controllerActionAttributesTestBuilder,
            Type withType)
            where TAttributesTestBuilder : IControllerActionAttributesTestBuilder<TAttributesTestBuilder>
            => controllerActionAttributesTestBuilder
                .SpecifyingProduction(production => production.WithType(withType));

        /// <summary>
        /// Tests whether the collected attributes contain <see cref="ProducesAttribute"/>.
        /// </summary>
        /// <param name="controllerActionAttributesTestBuilder">
        /// Instance of <see cref="IControllerActionAttributesTestBuilder{TAttributesTestBuilder}"/> type.
        /// </param>
        /// <param name="withType">Expected type.</param>
        /// <param name="withContentTypes">Expected content types.</param>
        /// <returns>The same attributes test builder.</returns>
        public static TAttributesTestBuilder SpecifyingProduction<TAttributesTestBuilder>(
            this IControllerActionAttributesTestBuilder<TAttributesTestBuilder> controllerActionAttributesTestBuilder,
            Type withType,
            IEnumerable<string> withContentTypes)
            where TAttributesTestBuilder : IControllerActionAttributesTestBuilder<TAttributesTestBuilder>
            => controllerActionAttributesTestBuilder
                .SpecifyingProduction(production => production
                    .WithType(withType)
                    .WithContentTypes(withContentTypes));

        /// <summary>
        /// Tests whether the collected attributes contain <see cref="ProducesAttribute"/>.
        /// </summary>
        /// <param name="controllerActionAttributesTestBuilder">
        /// Instance of <see cref="IControllerActionAttributesTestBuilder{TAttributesTestBuilder}"/> type.
        /// </param>
        /// <param name="withType">Expected type.</param>
        /// <param name="withContentTypes">Expected content types.</param>
        /// <returns>The same attributes test builder.</returns>
        public static TAttributesTestBuilder SpecifyingProduction<TAttributesTestBuilder>(
            this IControllerActionAttributesTestBuilder<TAttributesTestBuilder> controllerActionAttributesTestBuilder,
            Type withType,
            params string[] withContentTypes)
            where TAttributesTestBuilder : IControllerActionAttributesTestBuilder<TAttributesTestBuilder>
            => controllerActionAttributesTestBuilder
                .SpecifyingProduction(withType, withContentTypes.AsEnumerable());

        /// <summary>
        /// Tests whether the collected attributes contain <see cref="ProducesAttribute"/>.
        /// </summary>
        /// <param name="controllerActionAttributesTestBuilder">
        /// Instance of <see cref="IControllerActionAttributesTestBuilder{TAttributesTestBuilder}"/> type.
        /// </param>
        /// <param name="producesAttributeBuilder">Expected <see cref="ProducesAttribute"/> builder.</param>
        /// <returns>The same attributes test builder.</returns>
        public static TAttributesTestBuilder SpecifyingProduction<TAttributesTestBuilder>(
            this IControllerActionAttributesTestBuilder<TAttributesTestBuilder> controllerActionAttributesTestBuilder,
            Action<IProducesAttributeTestBuilder> producesAttributeBuilder)
            where TAttributesTestBuilder : IControllerActionAttributesTestBuilder<TAttributesTestBuilder>
        {
            var actualBuilder = (BaseAttributesTestBuilder<TAttributesTestBuilder>)controllerActionAttributesTestBuilder;

            actualBuilder.ContainingAttributeOfType<ProducesAttribute>();

            actualBuilder.Validations.Add(attrs =>
            {
                var newProducesAttributeTestBuilder = new ProducesAttributeTestBuilder(
                    actualBuilder.TestContext,
                    actualBuilder.ThrowNewAttributeAssertionException);

                producesAttributeBuilder(newProducesAttributeTestBuilder);

                var expectedProducesAttribute = newProducesAttributeTestBuilder.GetAttribute();
                var actualProducesAttribute = actualBuilder.GetAttributeOfType<ProducesAttribute>(attrs);

                var validations = newProducesAttributeTestBuilder.GetAttributeValidations();
                validations.ForEach(v => v(expectedProducesAttribute, actualProducesAttribute));
            });

            return actualBuilder.AttributesTestBuilder;
        }

        /// <summary>
        /// Tests whether the collected attributes contain <see cref="MiddlewareFilterAttribute"/>.
        /// </summary>
        /// <param name="controllerActionAttributesTestBuilder">
        /// Instance of <see cref="IControllerActionAttributesTestBuilder{TAttributesTestBuilder}"/> type.
        /// </param>
        /// <param name="configurationType">A type which configures a middleware pipeline.</param>
        /// <returns>The same attributes test builder.</returns>
        public static TAttributesTestBuilder SpecifyingMiddleware<TAttributesTestBuilder>(
            this IControllerActionAttributesTestBuilder<TAttributesTestBuilder> controllerActionAttributesTestBuilder,
            Type configurationType)
            where TAttributesTestBuilder : IControllerActionAttributesTestBuilder<TAttributesTestBuilder>
            => controllerActionAttributesTestBuilder
                .SpecifyingMiddleware(middleware => 
                    middleware.WithType(configurationType));

        /// <summary>
        /// Tests whether the collected attributes contain <see cref="MiddlewareFilterAttribute"/>.
        /// </summary>
        /// <param name="controllerActionAttributesTestBuilder">
        /// Instance of <see cref="IControllerActionAttributesTestBuilder{TAttributesTestBuilder}"/> type.
        /// </param>
        /// <param name="middlewareFilterAttributeBuilder">Expected <see cref="MiddlewareFilterAttribute"/> builder.</param>
        /// <returns>The same attributes test builder.</returns>
        public static TAttributesTestBuilder SpecifyingMiddleware<TAttributesTestBuilder>(
            this IControllerActionAttributesTestBuilder<TAttributesTestBuilder> controllerActionAttributesTestBuilder,
            Action<IMiddlewareFilterAttributeTestBuilder> middlewareFilterAttributeBuilder)
            where TAttributesTestBuilder : IControllerActionAttributesTestBuilder<TAttributesTestBuilder>
        {
            var actualBuilder = (BaseAttributesTestBuilder<TAttributesTestBuilder>)controllerActionAttributesTestBuilder;

            actualBuilder.ContainingAttributeOfType<MiddlewareFilterAttribute>();

            actualBuilder.Validations.Add(attrs =>
            {
                var newMiddlewareFilterAttributeBuilder = new MiddlewareFilterAttributeTestBuilder(
                    actualBuilder.TestContext,
                    actualBuilder.ThrowNewAttributeAssertionException);

                middlewareFilterAttributeBuilder(newMiddlewareFilterAttributeBuilder);

                var expectedAttribute = newMiddlewareFilterAttributeBuilder.GetAttribute();
                var actualAttribute = actualBuilder.GetAttributeOfType<MiddlewareFilterAttribute>(attrs);

                var validations = newMiddlewareFilterAttributeBuilder.GetAttributeValidations();
                validations.ForEach(v => v(expectedAttribute, actualAttribute));
            });

            return actualBuilder.AttributesTestBuilder;
        }

        /// <summary>
        /// Tests whether the collected attributes contain <see cref="RequireHttpsAttribute"/>.
        /// </summary>
        /// <param name="controllerActionAttributesTestBuilder">
        /// Instance of <see cref="IControllerActionAttributesTestBuilder{TAttributesTestBuilder}"/> type.
        /// </param>
        /// <param name="withPermanentRedirect">Tests whether a permanent redirect should be used instead of a temporary one.</param>
        /// <returns>The same attributes test builder.</returns>
        public static TAttributesTestBuilder RequiringHttps<TAttributesTestBuilder>(
            this IControllerActionAttributesTestBuilder<TAttributesTestBuilder> controllerActionAttributesTestBuilder,
            bool? withPermanentRedirect = null)
            where TAttributesTestBuilder : IControllerActionAttributesTestBuilder<TAttributesTestBuilder>
        {
            var actualBuilder = (BaseAttributesTestBuilder<TAttributesTestBuilder>)controllerActionAttributesTestBuilder;

            actualBuilder.ContainingAttributeOfType<RequireHttpsAttribute>();

            if (withPermanentRedirect.HasValue)
            {
                actualBuilder.Validations.Add(attrs =>
                {
                    var requireHttpsAttribute = actualBuilder.GetAttributeOfType<RequireHttpsAttribute>(attrs);
                    var actualPermanentValue = requireHttpsAttribute.Permanent;

                    if (withPermanentRedirect != actualPermanentValue)
                    {
                        actualBuilder.ThrowNewAttributeAssertionException(
                            $"{requireHttpsAttribute.GetName()} with {(withPermanentRedirect.Value ? "permanent" : "temporary")} redirect",
                            $"in fact it was a {(actualPermanentValue ? "permanent" : "temporary")} one");
                    }
                });
            }

            return actualBuilder.AttributesTestBuilder;
        }

        /// <summary>
        /// Tests whether the collected attributes contain <see cref="AllowAnonymousAttribute"/>.
        /// </summary>
        /// <param name="controllerActionAttributesTestBuilder">
        /// Instance of <see cref="IControllerActionAttributesTestBuilder{TAttributesTestBuilder}"/> type.
        /// </param>
        /// <returns>The same attributes test builder.</returns>
        public static TAttributesTestBuilder AllowingAnonymousRequests<TAttributesTestBuilder>(
            this IControllerActionAttributesTestBuilder<TAttributesTestBuilder> controllerActionAttributesTestBuilder)
            where TAttributesTestBuilder : IControllerActionAttributesTestBuilder<TAttributesTestBuilder>
            => controllerActionAttributesTestBuilder.ContainingAttributeOfType<AllowAnonymousAttribute>();

        /// <summary>
        /// Tests whether the collected attributes contain <see cref="AuthorizeAttribute"/>.
        /// </summary>
        /// <param name="controllerActionAttributesTestBuilder">
        /// Instance of <see cref="IControllerActionAttributesTestBuilder{TAttributesTestBuilder}"/> type.
        /// </param>
        /// <param name="withAllowedRoles">Optional expected authorized roles.</param>
        /// <returns>The same attributes test builder.</returns>
        public static TAttributesTestBuilder RestrictingForAuthorizedRequests<TAttributesTestBuilder>(
            this IControllerActionAttributesTestBuilder<TAttributesTestBuilder> controllerActionAttributesTestBuilder,
            string withAllowedRoles = null)
            where TAttributesTestBuilder : IControllerActionAttributesTestBuilder<TAttributesTestBuilder>
        {
            var actualBuilder = (BaseAttributesTestBuilder<TAttributesTestBuilder>)controllerActionAttributesTestBuilder;

            actualBuilder.ContainingAttributeOfType<AuthorizeAttribute>();

            var testAllowedRoles = !string.IsNullOrEmpty(withAllowedRoles);
            if (testAllowedRoles)
            {
                actualBuilder.Validations.Add(attrs =>
                {
                    var authorizeAttribute = actualBuilder.GetAttributeOfType<AuthorizeAttribute>(attrs);
                    var actualRoles = authorizeAttribute.Roles;

                    if (withAllowedRoles != actualRoles)
                    {
                        actualBuilder.ThrowNewAttributeAssertionException(
                            $"{authorizeAttribute.GetName()} with allowed '{withAllowedRoles}' roles",
                            $"in fact found '{actualRoles}'");
                    }
                });
            }

            return actualBuilder.AttributesTestBuilder;
        }

        /// <summary>
        /// Tests whether the collected attributes contain <see cref="FormatFilterAttribute"/>.
        /// </summary>
        /// <param name="controllerActionAttributesTestBuilder">
        /// Instance of <see cref="IControllerActionAttributesTestBuilder{TAttributesTestBuilder}"/> type.
        /// </param>
        /// <returns>The same attributes test builder.</returns>
        public static TAttributesTestBuilder AddingFormat<TAttributesTestBuilder>(
            this IControllerActionAttributesTestBuilder<TAttributesTestBuilder> controllerActionAttributesTestBuilder)
            where TAttributesTestBuilder : IControllerActionAttributesTestBuilder<TAttributesTestBuilder>
            => controllerActionAttributesTestBuilder.ContainingAttributeOfType<FormatFilterAttribute>();

        /// <summary>
        /// Tests whether the collected attributes contain <see cref="ResponseCacheAttribute"/>.
        /// </summary>
        /// <param name="controllerActionAttributesTestBuilder">
        /// Instance of <see cref="IControllerActionAttributesTestBuilder{TAttributesTestBuilder}"/> type.
        /// </param>
        /// <returns>The same attributes test builder.</returns>
        public static TAttributesTestBuilder CachingResponse<TAttributesTestBuilder>(
            this IControllerActionAttributesTestBuilder<TAttributesTestBuilder> controllerActionAttributesTestBuilder)
            where TAttributesTestBuilder : IControllerActionAttributesTestBuilder<TAttributesTestBuilder>
            => controllerActionAttributesTestBuilder.ContainingAttributeOfType<ResponseCacheAttribute>();

        /// <summary>
        /// Tests whether the collected attributes contain <see cref="ResponseCacheAttribute"/>.
        /// </summary>
        /// <param name="controllerActionAttributesTestBuilder">
        /// Instance of <see cref="IControllerActionAttributesTestBuilder{TAttributesTestBuilder}"/> type.
        /// </param>
        /// <param name="withDuration">Expected duration.</param>
        /// <returns></returns>
        public static TAttributesTestBuilder CachingResponse<TAttributesTestBuilder>(
            this IControllerActionAttributesTestBuilder<TAttributesTestBuilder> controllerActionAttributesTestBuilder,
            int withDuration)
            where TAttributesTestBuilder : IControllerActionAttributesTestBuilder<TAttributesTestBuilder>
            => controllerActionAttributesTestBuilder
                .CachingResponse(responseCache => responseCache
                    .WithDuration(withDuration));

        /// <summary>
        /// Tests whether the collected attributes contain <see cref="ResponseCacheAttribute"/>.
        /// </summary>
        /// <param name="controllerActionAttributesTestBuilder">
        /// Instance of <see cref="IControllerActionAttributesTestBuilder{TAttributesTestBuilder}"/> type.
        /// </param>
        /// <param name="withCacheProfileName">Expected cache profile name.</param>
        /// <returns>The same attributes test builder.</returns>
        public static TAttributesTestBuilder CachingResponse<TAttributesTestBuilder>(
            this IControllerActionAttributesTestBuilder<TAttributesTestBuilder> controllerActionAttributesTestBuilder,
            string withCacheProfileName)
            where TAttributesTestBuilder : IControllerActionAttributesTestBuilder<TAttributesTestBuilder>
            => controllerActionAttributesTestBuilder
                .CachingResponse(responseCache => responseCache
                    .WithCacheProfileName(withCacheProfileName));

        /// <summary>
        /// Tests whether the collected attributes contain <see cref="ResponseCacheAttribute"/>.
        /// </summary>
        /// <param name="controllerActionAttributesTestBuilder">
        /// Instance of <see cref="IControllerActionAttributesTestBuilder{TAttributesTestBuilder}"/> type.
        /// </param>
        /// <param name="responseCacheAttributeBuilder">Expected <see cref="ResponseCacheAttribute"/> builder.</param>
        /// <returns>The same attributes test builder.</returns>
        public static TAttributesTestBuilder CachingResponse<TAttributesTestBuilder>(
            this IControllerActionAttributesTestBuilder<TAttributesTestBuilder> controllerActionAttributesTestBuilder,
            Action<IResponseCacheAttributeTestBuilder> responseCacheAttributeBuilder)
            where TAttributesTestBuilder : IControllerActionAttributesTestBuilder<TAttributesTestBuilder>
        {
            var actualBuilder = (BaseAttributesTestBuilder<TAttributesTestBuilder>)controllerActionAttributesTestBuilder;

            actualBuilder.ContainingAttributeOfType<ResponseCacheAttribute>();

            actualBuilder.Validations.Add(attrs =>
            {
                var newResponseCacheAttributeTestBuilder = new ResponseCacheAttributeTestBuilder(
                    actualBuilder.TestContext,
                    actualBuilder.ThrowNewAttributeAssertionException);

                responseCacheAttributeBuilder(newResponseCacheAttributeTestBuilder);

                var expectedResponseCacheAttribute = newResponseCacheAttributeTestBuilder.GetAttribute();
                var actualResponseCacheAttribute = actualBuilder.GetAttributeOfType<ResponseCacheAttribute>(attrs);

                var validations = newResponseCacheAttributeTestBuilder.GetAttributeValidations();
                validations.ForEach(v => v(expectedResponseCacheAttribute, actualResponseCacheAttribute));
            });

            return actualBuilder.AttributesTestBuilder;
        }

        /// <summary>
        /// Tests whether the collected attributes contain <see cref="RequestSizeLimitAttribute"/>.
        /// </summary>
        /// <param name="controllerActionAttributesTestBuilder">
        /// Instance of <see cref="IControllerActionAttributesTestBuilder{TAttributesTestBuilder}"/> type.
        /// </param>
        /// <param name="requestFormLimitsAttributeBuilder">Expected <see cref="RequestSizeLimitAttribute"/> builder.</param>
        /// <returns>The same attributes test builder.</returns>
        public static TAttributesTestBuilder SettingRequestFormLimitsTo<TAttributesTestBuilder>(
            this IControllerActionAttributesTestBuilder<TAttributesTestBuilder> controllerActionAttributesTestBuilder,
            Action<IRequestFormLimitsAttributeTestBuilder> requestFormLimitsAttributeBuilder)
            where TAttributesTestBuilder : IControllerActionAttributesTestBuilder<TAttributesTestBuilder>
        {
            var actualBuilder = (BaseAttributesTestBuilder<TAttributesTestBuilder>)controllerActionAttributesTestBuilder;

            actualBuilder.ContainingAttributeOfType<RequestFormLimitsAttribute>();

            actualBuilder.Validations.Add(attrs =>
            {
                var newRequestFormLimitsAttributeTestBuilder = new RequestFormLimitsAttributeTestBuilder(
                    actualBuilder.TestContext,
                    actualBuilder.ThrowNewAttributeAssertionException);

                requestFormLimitsAttributeBuilder(newRequestFormLimitsAttributeTestBuilder);

                var expectedRequestFormLimitsAttribute = newRequestFormLimitsAttributeTestBuilder.GetAttribute();
                var actualRequestFormLimitsAttribute = actualBuilder.GetAttributeOfType<RequestFormLimitsAttribute>(attrs);

                var validations = newRequestFormLimitsAttributeTestBuilder.GetAttributeValidations();
                validations.ForEach(v => v(expectedRequestFormLimitsAttribute, actualRequestFormLimitsAttribute));
            });

            return actualBuilder.AttributesTestBuilder;
        }

        /// <summary>
        /// Tests whether the collected attributes contain <see cref="RequestSizeLimitAttribute"/>.
        /// </summary>
        /// <param name="controllerActionAttributesTestBuilder">
        /// Instance of <see cref="IControllerActionAttributesTestBuilder{TAttributesTestBuilder}"/> type.
        /// </param>
        /// <param name="bytes">Expected request body size limit in bytes.</param>
        /// <returns>The same attributes test builder.</returns>
        public static TAttributesTestBuilder SettingRequestSizeLimitTo<TAttributesTestBuilder>(
            this IControllerActionAttributesTestBuilder<TAttributesTestBuilder> controllerActionAttributesTestBuilder,
            long bytes)
            where TAttributesTestBuilder : IControllerActionAttributesTestBuilder<TAttributesTestBuilder>
        {
            var actualBuilder = (BaseAttributesTestBuilder<TAttributesTestBuilder>)controllerActionAttributesTestBuilder;

            actualBuilder.ContainingAttributeOfType<RequestSizeLimitAttribute>();

            actualBuilder.Validations.Add(attrs =>
            {
                var requestSizeLimitAttribute = actualBuilder.GetAttributeOfType<RequestSizeLimitAttribute>(attrs);
                var actualBytes = requestSizeLimitAttribute.GetFieldValue<long>($"_{nameof(bytes)}");

                if (bytes != actualBytes)
                {
                    actualBuilder.ThrowNewAttributeAssertionException(
                        $"{requestSizeLimitAttribute.GetName()} with request size limit of {bytes} bytes",
                        $"in fact found {actualBytes}");
                }
            });

            return actualBuilder.AttributesTestBuilder;
        }

        /// <summary>
        /// Tests whether the collected attributes contain <see cref="DisableRequestSizeLimitAttribute"/>.
        /// </summary>
        /// <param name="controllerActionAttributesTestBuilder">
        /// Instance of <see cref="IControllerActionAttributesTestBuilder{TAttributesTestBuilder}"/> type.
        /// </param>
        /// <returns>The same attributes test builder.</returns>
        public static TAttributesTestBuilder DisablingRequestSizeLimit<TAttributesTestBuilder>(
            this IControllerActionAttributesTestBuilder<TAttributesTestBuilder> controllerActionAttributesTestBuilder)
            where TAttributesTestBuilder : IControllerActionAttributesTestBuilder<TAttributesTestBuilder>
            => controllerActionAttributesTestBuilder
                .ContainingAttributeOfType<DisableRequestSizeLimitAttribute>();
    }
}
