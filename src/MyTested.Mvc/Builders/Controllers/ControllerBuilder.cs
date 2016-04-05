namespace MyTested.Mvc.Builders.Controllers
{
    using System;
    using System.Collections.Generic;
    using Base;
    using Contracts.Controllers;
    using Contracts.Data;
    using Internal.Contracts;
    using Internal.Http;
    using Internal.TestContexts;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.DependencyInjection;
    using Utilities;
    using Utilities.Validators;

    /// <summary>
    /// Used for building the controller which will be tested.
    /// </summary>
    /// <typeparam name="TController">Class representing ASP.NET MVC controller.</typeparam>
    public partial class ControllerBuilder<TController> : BaseTestBuilder, IAndControllerBuilder<TController>
        where TController : class
    {
        private readonly IDictionary<Type, object> aggregatedDependencies;

        private ControllerTestContext testContext;
        private Action<ControllerContext> controllerContextAction;
        private Action<ITempDataBuilder> tempDataBuilderAction;
        private Action<TController> controllerSetupAction;
        private bool isPreparedForTesting;
        private bool enabledValidation;
        private bool resolveRouteValues;
        private object additionalRouteValues;

        /// <summary>
        /// Initializes a new instance of the <see cref="ControllerBuilder{TController}" /> class.
        /// </summary>
        /// <param name="testContext"></param>
        public ControllerBuilder(ControllerTestContext testContext)
            :base (testContext)
        {
            this.TestContext = testContext;

            this.enabledValidation = true;
            this.aggregatedDependencies = new Dictionary<Type, object>();

            this.ValidateControllerType();
        }

        /// <summary>
        /// Gets the ASP.NET MVC controller instance to be tested.
        /// </summary>
        /// <value>Instance of the ASP.NET MVC controller.</value>
        private TController Controller
        {
            get
            {
                this.BuildControllerIfNotExists();
                return this.TestContext.ControllerAs<TController>();
            }
        }

        private new ControllerTestContext TestContext
        {
            get
            {
                return this.testContext;
            }

            set
            {
                CommonValidator.CheckForNullReference(value, nameof(this.TestContext));
                this.testContext = value;
            }
        }

        private new MockedHttpContext HttpContext => this.TestContext.MockedHttpContext;

        private HttpRequest HttpRequest => this.HttpContext.Request;

        private IServiceProvider Services => this.HttpContext.RequestServices;

        /// <summary>
        /// AndAlso method for better readability when building controller instance.
        /// </summary>
        /// <returns>The same controller builder.</returns>
        public IAndControllerBuilder<TController> AndAlso()
        {
            return this;
        }

        /// <summary>
        /// Used for testing controller attributes.
        /// </summary>
        /// <returns>Controller test builder.</returns>
        public IControllerTestBuilder ShouldHave()
        {
            this.BuildControllerIfNotExists();
            return new ControllerTestBuilder(this.TestContext);
        }

        /// <summary>
        /// Gets ASP.NET MVC controller instance to be tested.
        /// </summary>
        /// <returns>Instance of the ASP.NET MVC controller.</returns>
        public TController AndProvideTheController()
        {
            return this.Controller;
        }
        
        private void ValidateControllerType()
        {
            var validControllers = this.Services.GetService<IValidControllersCache>();
            var controllerType = typeof(TController);
            if (!validControllers.IsValid(typeof(TController)))
            {
                throw new InvalidOperationException($"{controllerType.ToFriendlyTypeName()} is not a valid controller type.");
            }
        }
    }
}
