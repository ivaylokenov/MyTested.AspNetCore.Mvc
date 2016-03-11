namespace MyTested.Mvc.Builders.Controllers
{
    using Authentication;
    using Contracts.Authentication;
    using Contracts.Controllers;
    using Contracts.Http;
    using Http;
    using Internal.Application;
    using Internal.Routes;
    using Microsoft.AspNetCore.Http;
    using System;
    using System.Linq.Expressions;
    using Utilities.Validators;

    /// <summary>
    /// Used for building the controller which will be tested.
    /// </summary>
    /// <typeparam name="TController">Class inheriting ASP.NET MVC controller.</typeparam>
    public partial class ControllerBuilder<TController>
    {
        /// <summary>
        /// Sets the HTTP context for the current test case. If no request services are set on the provided context, the globally configured ones are initialized.
        /// </summary>
        /// <param name="httpContext">Instance of HttpContext.</param>
        /// <returns>The same controller builder.</returns>
        public IAndControllerBuilder<TController> WithHttpContext(HttpContext httpContext)
        {
            CommonValidator.CheckForNullReference(httpContext, nameof(HttpContext));
            this.TestContext.HttpContext = httpContext;
            return this;
        }

        public IAndControllerBuilder<TController> WithHttpContext(Action<HttpContext> httpContextSetup)
        {
            httpContextSetup(this.TestContext.HttpContext);
            return this;
        }

        /// <summary>
        /// Adds HTTP request to the tested controller.
        /// </summary>
        /// <param name="httpRequest">Instance of HttpRequest.</param>
        /// <returns>The same controller builder.</returns>
        public IAndControllerBuilder<TController> WithHttpRequest(HttpRequest httpRequest)
        {
            CommonValidator.CheckForNullReference(httpRequest, nameof(HttpRequest));
            this.HttpContext.CustomRequest = httpRequest;
            return this;
        }

        /// <summary>
        /// Adds HTTP request to the tested controller by using builder.
        /// </summary>
        /// <param name="httpRequestBuilder">HTTP request builder.</param>
        /// <returns>The same controller builder.</returns>
        public IAndControllerBuilder<TController> WithHttpRequest(Action<IHttpRequestBuilder> httpRequestBuilder)
        {
            var newHttpRequestBuilder = new HttpRequestBuilder();
            httpRequestBuilder(newHttpRequestBuilder);
            newHttpRequestBuilder.ApplyTo(this.HttpRequest);
            return this;
        }

        /// <summary>
        /// Sets default authenticated user to the built controller with "TestId" identifier and "TestUser" username.
        /// </summary>
        /// <returns>The same controller builder.</returns>
        public IAndControllerBuilder<TController> WithAuthenticatedUser()
        {
            this.HttpContext.User = ClaimsPrincipalBuilder.CreateDefaultAuthenticated();
            return this;
        }

        /// <summary>
        /// Sets custom authenticated user using the provided user builder.
        /// </summary>
        /// <param name="userBuilder">User builder to create mocked user object.</param>
        /// <returns>The same controller builder.</returns>
        public IAndControllerBuilder<TController> WithAuthenticatedUser(Action<IAndClaimsPrincipalBuilder> userBuilder)
        {
            var newUserBuilder = new ClaimsPrincipalBuilder();
            userBuilder(newUserBuilder);
            this.HttpContext.User = newUserBuilder.GetClaimsPrincipal();
            return this;
        }
        
        public IAndControllerBuilder<TController> WithResolvedRouteData()
        {
            return this.WithResolvedRouteData(null);
        }

        public IAndControllerBuilder<TController> WithResolvedRouteData(object additionalRouteValues)
        {
            this.resolveRouteValues = true;
            this.additionalRouteValues = additionalRouteValues;
            return this;
        }
        
        private void SetRouteData(LambdaExpression actionCall)
        {
            if (this.resolveRouteValues)
            {
                this.TestContext.RouteData = RouteExpressionParser.ResolveRouteData(TestApplication.Router, actionCall);
                RouteExpressionParser.ApplyAdditionaRouteValues(
                    this.additionalRouteValues,
                    this.TestContext.RouteData.Values);
            }
        }
    }
}
