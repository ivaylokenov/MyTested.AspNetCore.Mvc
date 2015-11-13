namespace MyTested.Mvc.Common
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Contains information about invoked action.
    /// </summary>
    /// <typeparam name="TActionResult">The action return type.</typeparam>
    public class ActionInfo<TActionResult>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ActionInfo{TActionResult}" /> class.
        /// </summary>
        /// <param name="actionName">Name of the action.</param>
        /// <param name="actionAttributes">Collection of action attributes.</param>
        /// <param name="actionResult">Action return value.</param>
        /// <param name="caughtException">Caught exception during action execution.</param>
        public ActionInfo(string actionName, IEnumerable<object> actionAttributes, TActionResult actionResult, Exception caughtException)
        {
            this.ActionName = actionName;
            this.ActionAttributes = actionAttributes;
            this.ActionResult = actionResult;
            this.CaughtException = caughtException;
        }

        /// <summary>
        /// Gets the name of the action.
        /// </summary>
        /// <value>The action name as string.</value>
        public string ActionName { get; private set; }

        /// <summary>
        /// Gets the action attributes.
        /// </summary>
        /// <value>IEnumerable of objects.</value>
        public IEnumerable<object> ActionAttributes { get; private set; }

        /// <summary>
        /// Gets the return value of the action.
        /// </summary>
        /// <value>The action result as TActionResult.</value>
        public TActionResult ActionResult { get; private set; }

        /// <summary>
        /// Gets or sets the caught exception during the action execution.
        /// </summary>
        /// <value>Action execution exception.</value>
        public Exception CaughtException { get; set; }
    }
}
