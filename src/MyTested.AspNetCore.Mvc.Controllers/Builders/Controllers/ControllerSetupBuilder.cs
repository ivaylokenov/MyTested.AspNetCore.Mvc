namespace MyTested.AspNetCore.Mvc.Builders.Controllers
{
    using System;
    using System.Reflection;
    using Contracts.Controllers;
    using Internal.Controllers;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.DependencyInjection;
    using Utilities.Extensions;
    using Utilities.Validators;
    using Microsoft.AspNetCore.Mvc.Controllers;
    using Microsoft.AspNetCore.Mvc.Internal;

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

        protected override TController TryCreateComponentWithFactory()
        {
            try
            {
                return this.Services
                    .GetService<IControllerFactory>()
                    ?.CreateController(this.TestContext.ComponentContext) as TController;
            }
            catch
            {
                return null;
            }
        }

        protected override void ActivateComponent()
        {
            this.Services
                .GetServices<IControllerPropertyActivator>()
                ?.ForEach(a => a.Activate(this.TestContext.ComponentContext, this.TestContext.Component));
        }
    }
}
