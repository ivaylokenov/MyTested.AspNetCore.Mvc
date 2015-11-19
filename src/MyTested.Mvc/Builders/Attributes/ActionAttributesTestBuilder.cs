namespace MyTested.Mvc.Builders.Attributes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Common.Extensions;
    using Contracts.Attributes;
    using Exceptions;
    using Microsoft.AspNet.Mvc;
    using Microsoft.AspNet.Mvc.Infrastructure;
    using Microsoft.AspNet.Authorization;

    /// <summary>
    /// Used for testing action attributes.
    /// </summary>
    public class ActionAttributesTestBuilder : BaseAttributesTestBuilder, IAndActionAttributesTestBuilder
    {
        private readonly string testedActionName;

        /// <summary>
        /// Initializes a new instance of the <see cref="ActionAttributesTestBuilder" /> class.
        /// </summary>
        /// <param name="controller">Controller on which the attributes will be tested.</param>
        /// <param name="actionName">Name of the tested action.</param>
        public ActionAttributesTestBuilder(Controller controller, string actionName)
            : base(controller)
        {
            this.testedActionName = actionName;
        }

        /// <summary>
        /// Checks whether the collected attributes contain the provided attribute type.
        /// </summary>
        /// <typeparam name="TAttribute">Type of expected attribute.</typeparam>
        /// <returns>The same attributes test builder.</returns>
        public IAndActionAttributesTestBuilder ContainingAttributeOfType<TAttribute>()
            where TAttribute : Attribute
        {
            this.ContainingAttributeOfType<TAttribute>(this.ThrowNewAttributeAssertionException);
            return this;
        }

        /// <summary>
        /// Checks whether the collected attributes contain ActionNameAttribute.
        /// </summary>
        /// <param name="actionName">Expected overridden name of the action.</param>
        /// <returns>The same attributes test builder.</returns>
        public IAndActionAttributesTestBuilder ChangingActionNameTo(string actionName)
        {
            this.ContainingAttributeOfType<ActionNameAttribute>();
            this.Validations.Add(attrs =>
            {
                var actionNameAttribute = this.GetAttributeOfType<ActionNameAttribute>(attrs);
                var actualActionName = actionNameAttribute.Name;
                if (actionName != actualActionName)
                {
                    this.ThrowNewAttributeAssertionException(
                        string.Format("{0} with '{1}' name", actionNameAttribute.GetName(), actionName),
                        string.Format("in fact found '{0}'", actualActionName));
                }
            });

            return this;
        }

        /// <summary>
        /// Checks whether the collected attributes contain RouteAttribute.
        /// </summary>
        /// <param name="template">Expected overridden route template of the action.</param>
        /// <param name="withName">Optional expected route name.</param>
        /// <param name="withOrder">Optional expected route order.</param>
        /// <returns>The same attributes test builder.</returns>
        public IAndActionAttributesTestBuilder ChangingRouteTo(
            string template,
            string withName = null,
            int? withOrder = null)
        {
            this.ChangingRouteTo(
                template,
                this.ThrowNewAttributeAssertionException,
                withName,
                withOrder);

            return this;
        }

        /// <summary>
        /// Checks whether the collected attributes contain AllowAnonymousAttribute.
        /// </summary>
        /// <returns>The same attributes test builder.</returns>
        public IAndActionAttributesTestBuilder AllowingAnonymousRequests()
        {
            return this.ContainingAttributeOfType<AllowAnonymousAttribute>();
        }

        /// <summary>
        /// Checks whether the collected attributes contain AuthorizeAttribute.
        /// </summary>
        /// <param name="withAllowedRoles">Optional expected authorized roles.</param>
        /// <returns>The same attributes test builder.</returns>
        public IAndActionAttributesTestBuilder RestrictingForAuthorizedRequests(
            string withAllowedRoles = null)
        {
            this.RestrictingForAuthorizedRequests(
                this.ThrowNewAttributeAssertionException,
                withAllowedRoles);

            return this;
        }

        /// <summary>
        /// Checks whether the collected attributes contain NonActionAttribute.
        /// </summary>
        /// <returns>The same attributes test builder.</returns>
        public IAndActionAttributesTestBuilder DisablingActionCall()
        {
            return this.ContainingAttributeOfType<NonActionAttribute>();
        }

        // TODO: ?
        ///// <summary>
        ///// Checks whether the collected attributes restrict the request to a specific HTTP method (AcceptVerbsAttribute or the specific HttpGetAttribute, HttpPostAttribute, etc.).
        ///// </summary>
        ///// <typeparam name="THttpMethod">Attribute of type IActionHttpMethodProvider.</typeparam>
        ///// <returns>The same attributes test builder.</returns>
        //public IAndActionAttributesTestBuilder RestrictingForRequestsWithMethod<THttpMethod>()
        //    where THttpMethod : Attribute, IActionHttpMethodProvider, new()
        //{
        //    return this.RestrictingForRequestsWithMethods((new THttpMethod()).HttpMethods);
        //}

        // TODO: ?
        ///// <summary>
        ///// Checks whether the collected attributes restrict the request to a specific HTTP method (AcceptVerbsAttribute or the specific HttpGetAttribute, HttpPostAttribute, etc.).
        ///// </summary>
        ///// <param name="httpMethod">HTTP method provided as string.</param>
        ///// <returns>The same attributes test builder.</returns>
        //public IAndActionAttributesTestBuilder RestrictingForRequestsWithMethod(string httpMethod)
        //{
        //    return this.RestrictingForRequestsWithMethod(new HttpMethod(httpMethod));
        //}

        // TODO: ?
        ///// <summary>
        ///// Checks whether the collected attributes restrict the request to a specific HTTP method (AcceptVerbsAttribute or the specific HttpGetAttribute, HttpPostAttribute, etc.).
        ///// </summary>
        ///// <param name="httpMethod">HTTP method provided as HttpMethod class.</param>
        ///// <returns>The same attributes test builder.</returns>
        //public IAndActionAttributesTestBuilder RestrictingForRequestsWithMethod(HttpMethod httpMethod)
        //{
        //    return this.RestrictingForRequestsWithMethods(new List<HttpMethod> { httpMethod });
        //}

            // TODO: ?
        ///// <summary>
        ///// Checks whether the collected attributes restrict the request to a specific HTTP methods (AcceptVerbsAttribute or the specific HttpGetAttribute, HttpPostAttribute, etc.).
        ///// </summary>
        ///// <param name="httpMethods">HTTP methods provided as collection of strings.</param>
        ///// <returns>The same attributes test builder.</returns>
        //public IAndActionAttributesTestBuilder RestrictingForRequestsWithMethods(IEnumerable<string> httpMethods)
        //{
        //    return this.RestrictingForRequestsWithMethods(httpMethods.Select(m => new HttpMethod(m)));
        //}

            // TODO: ?
        ///// <summary>
        ///// Checks whether the collected attributes restrict the request to a specific HTTP methods (AcceptVerbsAttribute or the specific HttpGetAttribute, HttpPostAttribute, etc.).
        ///// </summary>
        ///// <param name="httpMethods">HTTP methods provided as string parameters.</param>
        ///// <returns>The same attributes test builder.</returns>
        //public IAndActionAttributesTestBuilder RestrictingForRequestsWithMethods(params string[] httpMethods)
        //{
        //    return this.RestrictingForRequestsWithMethods(httpMethods.AsEnumerable());
        //}

        ///// <summary>
        ///// Checks whether the collected attributes restrict the request to a specific HTTP methods (AcceptVerbsAttribute or the specific HttpGetAttribute, HttpPostAttribute, etc.).
        ///// </summary>
        ///// <param name="httpMethods">HTTP methods provided as collection of HttpMethod classes.</param>
        ///// <returns>The same attributes test builder.</returns>
        //public IAndActionAttributesTestBuilder RestrictingForRequestsWithMethods(IEnumerable<HttpMethod> httpMethods)
        //{
        //    this.Validations.Add(attrs =>
        //    {
        //        var totalAllowedHttpMethods = attrs.OfType<IActionHttpMethodProvider>().SelectMany(a => a.HttpMethods);

        //        httpMethods.ForEach(httpMethod =>
        //        {
        //            if (!totalAllowedHttpMethods.Contains(httpMethod))
        //            {
        //                this.ThrowNewAttributeAssertionException(
        //                    string.Format("attribute restricting requests for HTTP '{0}' method", httpMethod.Method),
        //                    "in fact none was found");
        //            }
        //        });
        //    });

        //    return this;
        //}

        ///// <summary>
        ///// Checks whether the collected attributes restrict the request to a specific HTTP methods (AcceptVerbsAttribute or the specific HttpGetAttribute, HttpPostAttribute, etc.).
        ///// </summary>
        ///// <param name="httpMethods">HTTP methods provided as parameters of HttpMethod class.</param>
        ///// <returns>The same attributes test builder.</returns>
        //public IAndActionAttributesTestBuilder RestrictingForRequestsWithMethods(params HttpMethod[] httpMethods)
        //{
        //    return this.RestrictingForRequestsWithMethods(httpMethods.AsEnumerable());
        //}

        /// <summary>
        /// AndAlso method for better readability when chaining attribute tests.
        /// </summary>
        /// <returns>The same attributes test builder.</returns>
        public IActionAttributesTestBuilder AndAlso()
        {
            return this;
        }

        private void ThrowNewAttributeAssertionException(string expectedValue, string actualValue)
        {
            throw new AttributeAssertionException(string.Format(
                        "When calling {0} action in {1} expected action to have {2}, but {3}.",
                        this.testedActionName,
                        this.Controller.GetName(),
                        expectedValue,
                        actualValue));
        }
    }
}
