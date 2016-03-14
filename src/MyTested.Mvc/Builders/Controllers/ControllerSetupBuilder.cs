namespace MyTested.Mvc.Builders.Controllers
{
    using System;
    using System.Linq;
    using Contracts.Controllers;
    using Data;
    using Exceptions;
    using Internal;
    using Internal.Controllers;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Controllers;
    using Microsoft.Extensions.DependencyInjection;
    using Utilities;
    using Utilities.Extensions;
    using Utilities.Validators;

    /// <summary>
    /// Used for building the controller which will be tested.
    /// </summary>
    /// <typeparam name="TController">Class representing ASP.NET MVC controller.</typeparam>
    public partial class ControllerBuilder<TController>
    {
        public IAndControllerBuilder<TController> WithControllerContext(ControllerContext controllerContext)
        {
            return this.WithActionContext(controllerContext);
        }

        public IAndControllerBuilder<TController> WithControllerContext(Action<ControllerContext> controllerContextSetup)
        {
            this.controllerContextAction += controllerContextSetup;
            return this;
        }

        public IAndControllerBuilder<TController> WithActionContext(ActionContext actionContext)
        {
            CommonValidator.CheckForNullReference(actionContext, nameof(ActionContext));
            this.TestContext.ControllerContext = MockedControllerContext.FromActionContext(this.TestContext, actionContext);
            return this;
        }

        public IAndControllerBuilder<TController> WithActionContext(Action<ActionContext> actionContextSetup)
        {
            this.controllerContextAction += actionContextSetup;
            return this;
        }

        /// <summary>
        /// Sets custom properties to the controller using action delegate.
        /// </summary>
        /// <param name="controllerSetup">Action delegate to use for controller setup.</param>
        /// <returns>The same controller test builder.</returns>
        public IAndControllerBuilder<TController> WithSetup(Action<TController> controllerSetup)
        {
            this.controllerSetupAction += controllerSetup;
            return this;
        }

        private void BuildControllerIfNotExists()
        {
            if (!this.isPreparedForTesting)
            {
                this.PrepareControllerContext();
            }

            var controller = this.TestContext.Controller;
            if (controller == null)
            {
                var explicitDependenciesAreSet = this.aggregatedDependencies.Any();
                if (explicitDependenciesAreSet)
                {
                    // custom dependencies are set, try create instance with them
                    controller = Reflection.TryCreateInstance<TController>(this.aggregatedDependencies);
                }
                else
                {
                    // no custom dependencies are set, try create instance with the global services
                    controller = TestHelper.TryCreateInstance<TController>();
                }

                if (controller == null && !explicitDependenciesAreSet)
                {
                    // no controller at this point, try to create one with default constructor
                    controller = Reflection.TryFastCreateInstance<TController>();
                }

                if (controller == null)
                {
                    var friendlyDependenciesNames = this.aggregatedDependencies
                        .Keys
                        .Select(k => k.ToFriendlyTypeName());

                    var joinedFriendlyDependencies = string.Join(", ", friendlyDependenciesNames);

                    throw new UnresolvedDependenciesException(string.Format(
                        "{0} could not be instantiated because it contains no constructor taking {1} parameters.",
                        typeof(TController).ToFriendlyTypeName(),
                        this.aggregatedDependencies.Count == 0 ? "no" : $"{joinedFriendlyDependencies} as"));
                }

                this.TestContext.ControllerConstruction = () => controller;
            }

            if (!this.isPreparedForTesting)
            {
                this.PrepareController();
                this.isPreparedForTesting = true;
            }
        }

        private void PrepareControllerContext()
        {
            var controllerContext = this.TestContext.ControllerContext;

            if (this.controllerContextAction != null)
            {
                this.controllerContextAction(controllerContext);
            }
        }

        private void PrepareController()
        {
            var controllerPropertyActivators = this.Services.GetServices<IControllerPropertyActivator>();

            controllerPropertyActivators.ForEach(a => a.Activate(this.TestContext.ControllerContext, this.TestContext.Controller));

            if (this.tempDataBuilderAction != null)
            {
                this.tempDataBuilderAction(new TempDataBuilder(this.TestContext.TempData));
            }

            if (this.controllerSetupAction != null)
            {
                this.controllerSetupAction(this.TestContext.ControllerAs<TController>());
            }
        }
    }
}
