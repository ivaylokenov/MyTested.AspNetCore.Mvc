namespace MyTested.AspNetCore.Mvc
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Builders.Attributes;
    using Builders.Contracts.Attributes;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Routing;
    using Utilities.Extensions;

    using SystemHttpMethod = System.Net.Http.HttpMethod;

    /// <summary>
    /// Contains extension methods for <see cref="IActionAttributesTestBuilder"/>.
    /// </summary>
    public static class ActionAttributesTestBuilderExtensions
    {
        /// <summary>
        /// Tests whether the action attributes contain <see cref="ActionNameAttribute"/>.
        /// </summary>
        /// <param name="actionAttributesTestBuilder">
        /// Instance of <see cref="IActionAttributesTestBuilder"/> type.
        /// </param>
        /// <param name="actionName">Expected overridden name of the action.</param>
        /// <returns>The same <see cref="IAndActionAttributesTestBuilder"/>.</returns>
        public static IAndActionAttributesTestBuilder ChangingActionNameTo(
            this IActionAttributesTestBuilder actionAttributesTestBuilder,
            string actionName)
        {
            var actualBuilder = (BaseAttributesTestBuilder<IAndActionAttributesTestBuilder>)actionAttributesTestBuilder;

            actualBuilder.ContainingAttributeOfType<ActionNameAttribute>();

            actualBuilder.Validations.Add(attrs =>
            {
                var actionNameAttribute = actualBuilder.GetAttributeOfType<ActionNameAttribute>(attrs);
                var actualActionName = actionNameAttribute.Name;

                if (actionName != actualActionName)
                {
                    actualBuilder.ThrowNewAttributeAssertionException(
                        $"{actionNameAttribute.GetName()} with '{actionName}' name",
                        $"in fact found '{actualActionName}'");
                }
            });

            return actualBuilder.AttributesTestBuilder;
        }

        /// <summary>
        /// Tests whether the action attributes contain <see cref="NonActionAttribute"/>.
        /// </summary>
        /// <param name="actionAttributesTestBuilder">
        /// Instance of <see cref="IActionAttributesTestBuilder"/> type.
        /// </param>
        /// <returns>The same <see cref="IAndActionAttributesTestBuilder"/>.</returns>
        public static IAndActionAttributesTestBuilder DisablingActionCall(
            this IActionAttributesTestBuilder actionAttributesTestBuilder)
            => actionAttributesTestBuilder
                .ContainingAttributeOfType<NonActionAttribute>();

        /// <summary>
        /// Tests whether the action attributes restrict the request to a specific
        /// HTTP method (<see cref="AcceptVerbsAttribute"/> or the specific
        /// <see cref="HttpGetAttribute"/>, <see cref="HttpPostAttribute"/>, etc.).
        /// </summary>
        /// <param name="actionAttributesTestBuilder">
        /// Instance of <see cref="IActionAttributesTestBuilder"/> type.
        /// </param>
        /// <typeparam name="THttpMethod">Attribute of type <see cref="IActionHttpMethodProvider"/>.</typeparam>
        /// <returns>The same <see cref="IAndActionAttributesTestBuilder"/>.</returns>
        public static IAndActionAttributesTestBuilder RestrictingForHttpMethod<THttpMethod>(
            this IActionAttributesTestBuilder actionAttributesTestBuilder)
            where THttpMethod : Attribute, IActionHttpMethodProvider, new()
            => actionAttributesTestBuilder
                .RestrictingForHttpMethods(new THttpMethod().HttpMethods);

        /// <summary>
        /// Tests whether the action attributes restrict the request to a specific
        /// HTTP method (<see cref="AcceptVerbsAttribute"/> or the specific
        /// <see cref="HttpGetAttribute"/>, <see cref="HttpPostAttribute"/>, etc.).
        /// </summary>
        /// <param name="actionAttributesTestBuilder">
        /// Instance of <see cref="IActionAttributesTestBuilder"/> type.
        /// </param>
        /// <param name="httpMethod">HTTP method provided as string.</param>
        /// <returns>The same <see cref="IAndActionAttributesTestBuilder"/>.</returns>
        public static IAndActionAttributesTestBuilder RestrictingForHttpMethod(
            this IActionAttributesTestBuilder actionAttributesTestBuilder, 
            string httpMethod)
            => actionAttributesTestBuilder
                .RestrictingForHttpMethod(new SystemHttpMethod(httpMethod));

        /// <summary>
        /// Tests whether the action attributes restrict the request to a specific HTTP method
        /// (<see cref="AcceptVerbsAttribute"/> or the specific
        /// <see cref="HttpGetAttribute"/>, <see cref="HttpPostAttribute"/>, etc.).
        /// </summary>
        /// <param name="actionAttributesTestBuilder">
        /// Instance of <see cref="IActionAttributesTestBuilder"/> type.
        /// </param>
        /// <param name="httpMethod">HTTP method provided as <see cref="System.Net.Http.HttpMethod"/> class.</param>
        /// <returns>The same <see cref="IAndActionAttributesTestBuilder"/>.</returns>
        public static IAndActionAttributesTestBuilder RestrictingForHttpMethod(
            this IActionAttributesTestBuilder actionAttributesTestBuilder,
            SystemHttpMethod httpMethod)
            => actionAttributesTestBuilder
                .RestrictingForHttpMethods(new List<SystemHttpMethod> { httpMethod });

        /// <summary>
        /// Tests whether the action attributes restrict the request to a specific HTTP methods
        /// (<see cref="AcceptVerbsAttribute"/> or the specific
        /// <see cref="HttpGetAttribute"/>, <see cref="HttpPostAttribute"/>, etc.).
        /// </summary>
        /// <param name="actionAttributesTestBuilder">
        /// Instance of <see cref="IActionAttributesTestBuilder"/> type.
        /// </param>
        /// <param name="httpMethods">HTTP methods provided as collection of strings.</param>
        /// <returns>The same <see cref="IAndActionAttributesTestBuilder"/>.</returns>
        public static IAndActionAttributesTestBuilder RestrictingForHttpMethods(
            this IActionAttributesTestBuilder actionAttributesTestBuilder, 
            IEnumerable<string> httpMethods)
            => actionAttributesTestBuilder
                .RestrictingForHttpMethods(httpMethods.Select(m => new SystemHttpMethod(m)));

        /// <summary>
        /// Tests whether the action attributes restrict the request to a specific HTTP methods
        /// (<see cref="AcceptVerbsAttribute"/> or the specific
        /// <see cref="HttpGetAttribute"/>, <see cref="HttpPostAttribute"/>, etc.).
        /// </summary>
        /// <param name="actionAttributesTestBuilder">
        /// Instance of <see cref="IActionAttributesTestBuilder"/> type.
        /// </param>
        /// <param name="httpMethods">HTTP methods provided as string parameters.</param>
        /// <returns>The same <see cref="IAndActionAttributesTestBuilder"/>.</returns>
        public static IAndActionAttributesTestBuilder RestrictingForHttpMethods(
            this IActionAttributesTestBuilder actionAttributesTestBuilder, 
            params string[] httpMethods)
            => actionAttributesTestBuilder.RestrictingForHttpMethods(httpMethods.AsEnumerable());

        /// <summary>
        /// Tests whether the action attributes restrict the request to a specific HTTP methods
        /// (<see cref="AcceptVerbsAttribute"/> or the specific
        /// <see cref="HttpGetAttribute"/>, <see cref="HttpPostAttribute"/>, etc.).
        /// </summary>
        /// <param name="actionAttributesTestBuilder">
        /// Instance of <see cref="IActionAttributesTestBuilder"/> type.
        /// </param>
        /// <param name="httpMethods">HTTP methods provided as collection of <see cref="System.Net.Http.HttpMethod"/> classes.</param>
        /// <returns>The same <see cref="IAndActionAttributesTestBuilder"/>.</returns>
        public static IAndActionAttributesTestBuilder RestrictingForHttpMethods(
            this IActionAttributesTestBuilder actionAttributesTestBuilder,
            IEnumerable<SystemHttpMethod> httpMethods)
        {
            var actualBuilder = (BaseAttributesTestBuilder<IAndActionAttributesTestBuilder>)actionAttributesTestBuilder;

            actualBuilder.Validations.Add(attrs =>
            {
                var totalAllowedHttpMethods = attrs.OfType<IActionHttpMethodProvider>().SelectMany(a => a.HttpMethods);

                httpMethods.ForEach(httpMethod =>
                {
                    var method = httpMethod.Method;

                    if (!totalAllowedHttpMethods.Contains(method))
                    {
                        actualBuilder.ThrowNewAttributeAssertionException(
                            $"attribute restricting requests for HTTP '{method}' method",
                            "in fact none was found");
                    }
                });
            });

            return actualBuilder.AttributesTestBuilder;
        }

        /// <summary>
        /// Tests whether the action attributes restrict the request to a specific HTTP methods
        /// (<see cref="AcceptVerbsAttribute"/> or the specific
        /// <see cref="HttpGetAttribute"/>, <see cref="HttpPostAttribute"/>, etc.).
        /// </summary>
        /// <param name="actionAttributesTestBuilder">
        /// Instance of <see cref="IActionAttributesTestBuilder"/> type.
        /// </param>
        /// <param name="httpMethods">HTTP methods provided as parameters of <see cref="System.Net.Http.HttpMethod"/> class.</param>
        /// <returns>The same <see cref="IAndActionAttributesTestBuilder"/>.</returns>
        public static IAndActionAttributesTestBuilder RestrictingForHttpMethods(
            this IActionAttributesTestBuilder actionAttributesTestBuilder, 
            params SystemHttpMethod[] httpMethods)
            => actionAttributesTestBuilder
                .RestrictingForHttpMethods(httpMethods.AsEnumerable());
    }
}
