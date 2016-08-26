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
        
        protected override void PrepareComponentContext()
        {
            var controllerContext = this.TestContext.ControllerContext;
            this.controllerContextAction?.Invoke(controllerContext);
        }

        protected override void PrepareComponent()
        {
            var controllerPropertyActivators = this.Services.GetServices<IControllerPropertyActivator>();

            controllerPropertyActivators.ForEach(a => a.Activate(this.TestContext.ControllerContext, this.TestContext.Component));

            this.TestContext.ComponentPreparationDelegate?.Invoke();

            this.controllerSetupAction?.Invoke(this.TestContext.ComponentAs<TController>());
        }
    }
}
