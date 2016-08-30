namespace MyTested.AspNetCore.Mvc.Internal.TestContexts
{
    using System;
    using System.Linq.Expressions;

    /// <summary>
    /// Contains information about invoked method.
    /// </summary>
    /// <typeparam name="TResult">The method return type.</typeparam>
    public class InvocationTestContext<TResult>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvocationTestContext{TActionResult}"/> class.
        /// </summary>
        /// <param name="methodName">Name of the method.</param>
        /// <param name="methodCall"><see cref="LambdaExpression"/> representing the method call.</param>
        /// <param name="methodResult">Method return value.</param>
        /// <param name="caughtException">Caught exception during method execution.</param>
        public InvocationTestContext(string methodName, LambdaExpression methodCall, TResult methodResult, Exception caughtException)
        {
            this.MethodName = methodName;
            this.MethodCall = methodCall;
            this.MethodResult = methodResult;
            this.CaughtException = caughtException;
        }

        /// <summary>
        /// Gets the name of the method.
        /// </summary>
        /// <value>The method name as string.</value>
        public string MethodName { get; set; }
        
        public LambdaExpression MethodCall { get; set; }

        /// <summary>
        /// Gets or sets the return value of the method.
        /// </summary>
        /// <value>The method result as TResult.</value>
        public TResult MethodResult { get; set; }

        /// <summary>
        /// Gets or sets the caught exception during the method execution.
        /// </summary>
        /// <value>Method execution exception.</value>
        public Exception CaughtException { get; set; }
    }
}
