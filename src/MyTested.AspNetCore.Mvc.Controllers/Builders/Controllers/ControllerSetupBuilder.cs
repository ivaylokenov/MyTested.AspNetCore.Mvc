namespace MyTested.AspNetCore.Mvc.Builders.Controllers
{
    using System;
    using System.Reflection;
    using Contracts.Controllers;
    using Internal.Controllers;
    using Microsoft.AspNetCore.Mvc;
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
            this.TestContext.ComponentContextPreparationDelegate += controllerContextSetup;
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
            this.TestContext.ComponentContextPreparationDelegate += actionContextSetup;
            return this;
        }
        
        protected override void PrepareComponentContext()
        {
            var controllerContext = this.TestContext.ComponentContext;
            controllerContext.ActionDescriptor.ControllerTypeInfo = typeof(TController).GetTypeInfo();
            this.TestContext.ComponentContextPreparationDelegate?.Invoke(controllerContext);
        }
    }
}
