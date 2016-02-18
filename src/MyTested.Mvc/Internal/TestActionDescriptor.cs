namespace MyTested.Mvc.Internal
{
    using System;
    using System.Reflection;

    /// <summary>
    /// Contains information about invoked action.
    /// </summary>
    /// <typeparam name="TActionResult">The action return type.</typeparam>
    public class TestActionDescriptor<TActionResult>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestActionDescriptor{TActionResult}" /> class.
        /// </summary>
        /// <param name="actionName">Name of the action.</param>
        /// <param name="actionResult">Action return value.</param>
        /// <param name="caughtException">Caught exception during action execution.</param>
        public TestActionDescriptor(string actionName, MethodInfo action, TActionResult actionResult, Exception caughtException)
        {
            this.ActionName = actionName;
            this.Action = action;
            this.ActionResult = actionResult;
            this.CaughtException = caughtException;
        }

        /// <summary>
        /// Gets the name of the action.
        /// </summary>
        /// <value>The action name as string.</value>
        public string ActionName { get; internal set; }
        
        public MethodInfo Action { get; internal set; }

        /// <summary>
        /// Gets the return value of the action.
        /// </summary>
        /// <value>The action result as TActionResult.</value>
        public TActionResult ActionResult { get; internal set; }

        /// <summary>
        /// Gets or sets the caught exception during the action execution.
        /// </summary>
        /// <value>Action execution exception.</value>
        public Exception CaughtException { get; internal set; }
    }
}
