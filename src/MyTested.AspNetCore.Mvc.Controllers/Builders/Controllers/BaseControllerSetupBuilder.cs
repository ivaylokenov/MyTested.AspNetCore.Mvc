namespace MyTested.AspNetCore.Mvc.Builders.Controllers
{
    using System;
    using System.Reflection;
    using Internal.Controllers;
    using Microsoft.AspNetCore.Mvc;
    using Utilities.Validators;

    /// <content>
    /// Used for building the controller which will be tested.
    /// </content>
    public abstract partial class BaseControllerBuilder<TController, TBuilder>
    {
        /// <inheritdoc />
        public TBuilder WithControllerContext(ControllerContext controllerContext)
            => this.WithActionContext(controllerContext);

        /// <inheritdoc />
        public TBuilder WithControllerContext(Action<ControllerContext> controllerContextSetup)
        {
            this.TestContext.ComponentContextPreparationDelegate += controllerContextSetup;
            return this.Builder;
        }

        /// <inheritdoc />
        public TBuilder WithActionContext(ActionContext actionContext)
        {
            CommonValidator.CheckForNullReference(actionContext, nameof(ActionContext));
            this.TestContext.ComponentContext = ControllerContextMock.FromActionContext(this.TestContext, actionContext);
            return this.Builder;
        }

        /// <inheritdoc />
        public TBuilder WithActionContext(Action<ActionContext> actionContextSetup)
        {
            this.TestContext.ComponentContextPreparationDelegate += actionContextSetup;
            return this.Builder;
        }
        
        protected override void PrepareComponentContext()
        {
            var controllerContext = this.TestContext.ComponentContext;
            controllerContext.ActionDescriptor.ControllerTypeInfo = typeof(TController).GetTypeInfo();
            this.TestContext.ComponentContextPreparationDelegate?.Invoke(controllerContext);
        }
    }
}
