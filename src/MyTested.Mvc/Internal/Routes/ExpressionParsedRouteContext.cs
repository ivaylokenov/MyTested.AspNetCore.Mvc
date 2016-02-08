namespace MyTested.Mvc.Internal.Routes
{
    using Utilities.Extensions;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Contains route information from parsed expression.
    /// </summary>
    public class ExpressionParsedRouteContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExpressionParsedRouteContext" /> class.
        /// </summary>
        /// <param name="controllerType">Controller name.</param>
        /// <param name="action">Action name.</param>
        /// <param name="routeValues">Route values.</param>
        public ExpressionParsedRouteContext(
            Type controllerType,
            string controllerName,
            string action,
            IDictionary<string, object> routeValues)
        {
            this.ControllerType = controllerType;
            this.ControllerName = controllerName;
            this.Action = action;
            this.RouteValues = routeValues.ToDetailedValues();
        }

        /// <summary>
        /// Gets the controller type from the parsed expression.
        /// </summary>
        /// <value>The controller type.</value>
        public Type ControllerType { get; private set; }

        /// <summary>
        /// Gets the controller name from the parsed expression.
        /// </summary>
        /// <value>The controller name.</value>
        public string ControllerName { get; private set; }

        /// <summary>
        /// Gets the action name from the parsed expression.
        /// </summary>
        /// <value>The action type.</value>
        public string Action { get; private set; }

        /// <summary>
        /// Gets the route values from the parsed expression.
        /// </summary>
        /// <value>Dictionary of route values.</value>
        public IDictionary<string, MethodArgumentContext> RouteValues { get; private set; }
    }
}
