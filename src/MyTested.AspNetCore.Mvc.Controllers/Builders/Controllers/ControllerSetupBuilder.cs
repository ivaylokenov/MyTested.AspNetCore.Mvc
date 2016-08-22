namespace MyTested.AspNetCore.Mvc.Builders.Controllers
{
    using System;
    using System.Linq;
    using Contracts.Controllers;
    using Exceptions;
    using Internal;
    using Internal.Controllers;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Internal;
    using Microsoft.Extensions.DependencyInjection;
    using Utilities;
    using Utilities.Extensions;
    using Utilities.Validators;

    /// <content>
    /// Used for building the controller which will be tested.
    /// </content>
    public partial class ControllerBuilder<TController>
    {
        /// <inheritdoc />
        public IAndControllerBuilder<TController> WithControllerContext(ControllerContext controllerContext)
        {
            return this.WithActionContext(controllerContext);
        }

        /// <inheritdoc />
        public IAndControllerBuilder<TController> WithControllerContext(Action<ControllerContext> controllerContextSetup)
        {
            this.controllerContextAction += controllerContextSetup;
            return this;
        }

        /// <inheritdoc />
        public IAndControllerBuilder<TController> WithActionContext(ActionContext actionContext)
        {
            CommonValidator.CheckForNullReference(actionContext, nameof(ActionContext));
            this.TestContext.ControllerContext = MockedControllerContext.FromActionContext(this.TestContext, actionContext);
            return this;
        }

        /// <inheritdoc />
        public IAndControllerBuilder<TController> WithActionContext(Action<ActionContext> actionContextSetup)
        {
            this.controllerContextAction += actionContextSetup;
            return this;
        }

        /// <inheritdoc />
        public IAndControllerBuilder<TController> WithSetup(Action<TController> controllerSetup)
        {
            this.controllerSetupAction += controllerSetup;
            return this;
        }

        protected override void BuildComponentIfNotExists()
        {
            if (!this.isPreparedForTesting)
            {
                this.PrepareControllerContext();
            }

            var controller = this.TestContext.Component;
            if (controller == null)
            {
                var explicitDependenciesAreSet = this.TestContext.AggregatedServices.Any();
                if (explicitDependenciesAreSet)
                {
                    // custom dependencies are set, try create instance with them
                    controller = Reflection.TryCreateInstance<TController>(this.TestContext.AggregatedServices);
                }
                else
                {
                    // no custom dependencies are set, try create instance with the global services
                    controller = ControllerTestHelper.TryCreateInstance<TController>();
                }

                if (controller == null && !explicitDependenciesAreSet)
                {
                    // no controller at this point, try to create one with default constructor
                    controller = Reflection.TryFastCreateInstance<TController>();
                }

                if (controller == null)
                {
                    var friendlyServiceNames = this.TestContext.AggregatedServices
                        .Keys
                        .Select(k => k.ToFriendlyTypeName());

                    var joinedFriendlyServices = string.Join(", ", friendlyServiceNames);

                    throw new UnresolvedServicesException(string.Format(
                        "{0} could not be instantiated because it contains no constructor taking {1} parameters.",
                        typeof(TController).ToFriendlyTypeName(),
                        this.TestContext.AggregatedServices.Count == 0 ? "no" : $"{joinedFriendlyServices} as"));
                }

                this.TestContext.ComponentConstruction = () => controller;
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
            this.controllerContextAction?.Invoke(controllerContext);
        }

        private void PrepareController()
        {
            var controllerPropertyActivators = this.Services.GetServices<IControllerPropertyActivator>();

            controllerPropertyActivators.ForEach(a => a.Activate(this.TestContext.ControllerContext, this.TestContext.Component));

            this.TestContext.ComponentPreparationAction?.Invoke();

            this.controllerSetupAction?.Invoke(this.TestContext.ComponentAs<TController>());
        }
    }
}
