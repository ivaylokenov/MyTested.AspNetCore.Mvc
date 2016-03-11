namespace MyTested.Mvc.Builders.Base
{
    using System.Collections.Generic;
    using Contracts.Base;
    using Microsoft.AspNetCore.Http;
    using Utilities.Validators;
    using Internal.TestContexts;

    /// <summary>
    /// Base class for all test builder.
    /// </summary>
    public abstract class BaseTestBuilder : IBaseTestBuilder
    {
        private ControllerTestContext testContext;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseTestBuilder" /> class.
        /// </summary>
        /// <param name="testContext"></param>
        protected BaseTestBuilder(ControllerTestContext testContext)
        {
            this.TestContext = testContext;
        }

        /// <summary>
        /// Gets the controller on which the action will be tested.
        /// </summary>
        /// <value>Controller on which the action will be tested.</value>
        internal object Controller => this.TestContext.Controller;

        internal IEnumerable<object> ControllerLevelAttributes => this.TestContext.ControllerAttributes;

        protected ControllerTestContext TestContext
        {
            get
            {
                return this.testContext;
            }
            private set
            {
                CommonValidator.CheckForNullReference(value, nameof(TestContext));
                CommonValidator.CheckForNullReference(value.Controller, nameof(Controller));
                this.testContext = value;
            }
        }

        /// <summary>
        /// Gets the controller on which the action is tested.
        /// </summary>
        /// <returns>ASP.NET MVC controller on which the action is tested.</returns>
        public object AndProvideTheController() => this.Controller;

        /// <summary>
        /// Gets the HTTP request message with which the action will be tested.
        /// </summary>
        /// <returns>HttpRequest from the tested controller.</returns>
        public HttpRequest AndProvideTheHttpRequest() => this.TestContext.HttpContext.Request;

        /// <summary>
        /// Gets the HTTP context with which the action will be tested.
        /// </summary>
        /// <returns>HttpContext from the tested controller.</returns>
        public HttpContext AndProvideTheHttpContext() => this.TestContext.HttpContext;

        /// <summary>
        /// Gets the attributes on the tested controller.
        /// </summary>
        /// <returns>IEnumerable of object representing the attributes or null, if no attributes were collected on the controller.</returns>
        public IEnumerable<object> AndProvideTheControllerAttributes() => this.ControllerLevelAttributes;
    }
}
