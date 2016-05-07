namespace MyTested.Mvc.Internal.Routes
{
    using System;
    using System.Collections.Generic;
    using TestContexts;
    using Utilities.Extensions;

    /// <summary>
    /// Contains route information from parsed expression.
    /// </summary>
    public class ExpressionParsedRouteContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExpressionParsedRouteContext"/> class.
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
            this.ActionArguments = routeValues.ToDetailedValues();
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
        /// Gets the action arguments from the parsed expression.
        /// </summary>
        /// <value>Dictionary of action arguments.</value>
        public IDictionary<string, MethodArgumentTestContext> ActionArguments { get; private set; }
    }
}
