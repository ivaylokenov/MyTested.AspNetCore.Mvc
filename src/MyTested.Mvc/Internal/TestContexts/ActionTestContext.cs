namespace MyTested.Mvc.Internal.TestContexts
{
    using System;
    using System.Linq.Expressions;

    /// <summary>
    /// Contains information about invoked action.
    /// </summary>
    /// <typeparam name="TActionResult">The action return type.</typeparam>
    public class ActionTestContext<TActionResult>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ActionTestContext{TActionResult}"/> class.
        /// </summary>
        /// <param name="actionName">Name of the action.</param>
        /// <param name="actionCall"><see cref="LambdaExpression"/> representing the action call.</param>
        /// <param name="actionResult">Action return value.</param>
        /// <param name="caughtException">Caught exception during action execution.</param>
        public ActionTestContext(string actionName, LambdaExpression actionCall, TActionResult actionResult, Exception caughtException)
        {
            this.ActionName = actionName;
            this.ActionCall = actionCall;
            this.ActionResult = actionResult;
            this.CaughtException = caughtException;
        }

        /// <summary>
        /// Gets the name of the action.
        /// </summary>
        /// <value>The action name as string.</value>
        public string ActionName { get; internal set; }
        
        public LambdaExpression ActionCall { get; internal set; }

        /// <summary>
        /// Gets the return value of the action.
        /// </summary>
        /// <value>The action result as TActionResult.</value>
        public TActionResult ActionResult { get; internal set; }

        /// <summary>
        /// Gets the caught exception during the action execution.
        /// </summary>
        /// <value>Action execution exception.</value>
        public Exception CaughtException { get; internal set; }
    }
}
