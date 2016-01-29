namespace MyTested.Mvc.Builders.Base
{
    using System;
    using System.Collections.Generic;
    using And;
    using Contracts.Base;
    using Microsoft.AspNet.Http;
    using Microsoft.AspNet.Mvc;

    /// <summary>
    /// Base class for test builders with caught exception.
    /// </summary>
    public abstract class BaseTestBuilderWithInvokedAction
        : BaseTestBuilderWithAction, IBaseTestBuilderWithInvokedAction
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseTestBuilderWithInvokedAction" /> class.
        /// </summary>
        /// <param name="controller">Controller on which the action will be tested.</param>
        /// <param name="actionName">Name of the tested action.</param>
        /// <param name="caughtException">Caught exception during the action execution.</param>
        /// <param name="actionAttributes">Collected action attributes from the method call.</param>
        protected BaseTestBuilderWithInvokedAction(
            Controller controller,
            string actionName,
            Exception caughtException,
            IEnumerable<object> actionAttributes = null)
            : base(controller, actionName, actionAttributes)
        {
            this.CaughtException = caughtException;
        }

        internal Exception CaughtException { get; private set; }

        /// <summary>
        /// Gets the thrown exception in the tested action.
        /// </summary>
        /// <returns>The exception instance or null, if no exception was caught.</returns>
        public Exception AndProvideTheCaughtException() => this.CaughtException;
        
        /// <summary>
        /// Gets the HTTP response after the tested action is executed.
        /// </summary>
        /// <returns>The HTTP response.</returns>
        public HttpResponse AndProvideTheHttpResponse() => this.Controller.Response;

        /// <summary>
        /// Creates new AndProvideTestBuilder.
        /// </summary>
        /// <returns>Base test builder.</returns>
        protected IBaseTestBuilderWithInvokedAction NewAndProvideTestBuilder()
            => new AndProvideTestBuilder(this.Controller, this.ActionName, this.CaughtException);
    }
}
