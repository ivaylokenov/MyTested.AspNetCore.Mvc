namespace MyTested.AspNetCore.Mvc.Internal.Routing
{
    using System.Collections.Generic;
    using System.Reflection;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Microsoft.AspNetCore.Routing;
    using TestContexts;
    using Utilities.Extensions;

    /// <summary>
    /// Contains information about a resolved route from the ASP.NET Web API internal pipeline.
    /// </summary>
    public class ResolvedRouteContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ResolvedRouteContext"/> class.
        /// </summary>
        /// <param name="controllerType">Resolved controller type for the current route.</param>
        /// <param name="controllerName">Resolved controller name for the current route.</param>
        /// <param name="action">Resolved action name for the current route.</param>
        /// <param name="routeValues">Resolved dictionary of the action arguments for the current route.</param>
        /// <param name="routeData">Resolved HttpMessageHandler for the current route.</param>
        /// <param name="modelState">Resolved model state validation for the current route.</param>
        public ResolvedRouteContext(
            TypeInfo controllerType,
            string controllerName,
            string action,
            IDictionary<string, object> routeValues,
            RouteData routeData,
            ModelStateDictionary modelState)
        {
            this.IsResolved = true;
            this.ControllerType = controllerType;
            this.ControllerName = controllerName;
            this.Action = action;
            this.ActionArguments = routeValues.ToDetailedValues();
            this.RouteData = routeData;
            this.ModelState = modelState;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResolvedRouteContext"/> class.
        /// </summary>
        /// <param name="unresolvedError">Received error during the route resolving.</param>
        public ResolvedRouteContext(string unresolvedError)
        {
            this.IsResolved = false;
            this.UnresolvedError = unresolvedError;
        }

        /// <summary>
        /// Gets a value indicating whether the current route is successfully resolved.
        /// </summary>
        /// <value>True or false.</value>
        public bool IsResolved { get; private set; }

        /// <summary>
        /// Gets route error in case of unsuccessful resolving.
        /// </summary>
        /// <value>The error as string or null, if the route was resolved successfully.</value>
        public string UnresolvedError { get; private set; }

        /// <summary>
        /// Gets the resolved controller type for the current route.
        /// </summary>
        /// <value>Type of the controller.</value>
        public TypeInfo ControllerType { get; private set; }

        /// <summary>
        /// Gets the resolved controller name for the current route.
        /// </summary>
        /// <value>Name of the controller.</value>
        public string ControllerName { get; private set; }

        /// <summary>
        /// Gets the resolved action name for the current route.
        /// </summary>
        /// <value>The action name as string.</value>
        public string Action { get; private set; }

        /// <summary>
        /// Gets the resolved action arguments for the current route.
        /// </summary>
        /// <value>Dictionary of action arguments.</value>
        public IDictionary<string, MethodArgumentTestContext> ActionArguments { get; private set; }

        /// <summary>
        /// Gets the resolved route data for the current route.
        /// </summary>
        /// <value>Instance of RouteData.</value>
        public RouteData RouteData { get; private set; }
        
        /// <summary>
        /// Gets the resolved model state validation for the current route.
        /// </summary>
        /// <value>Instance of ModelStateDictionary.</value>
        public ModelStateDictionary ModelState { get; private set; }
    }
}
