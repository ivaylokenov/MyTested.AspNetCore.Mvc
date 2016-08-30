namespace MyTested.AspNetCore.Mvc.Builders.Controllers
{
    using System;
    using Contracts.Controllers;
    using Internal.Controllers;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Internal;
    using Microsoft.Extensions.DependencyInjection;
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
            this.TestContext.ComponentContext = ControllerContextMock.FromActionContext(this.TestContext, actionContext);
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
            var controllerContext = this.TestContext.ComponentContext;
            this.controllerContextAction?.Invoke(controllerContext);
        }

        protected override void PrepareComponent()
        {
            this.Services
                .GetServices<IControllerPropertyActivator>()
                ?.ForEach(a => a.Activate(this.TestContext.ComponentContext, this.TestContext.Component));

            this.TestContext.ComponentPreparationDelegate?.Invoke();

            this.controllerSetupAction?.Invoke(this.TestContext.ComponentAs<TController>());
        }
    }
}
