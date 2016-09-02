namespace MyTested.AspNetCore.Mvc.Builders.ViewComponents
{
    using System;
    using System.Reflection;
    using Contracts.ViewComponents;
    using Internal.Contracts;
    using Internal.ViewComponents;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewComponents;
    using Microsoft.AspNetCore.Mvc.ViewEngines;
    using Microsoft.Extensions.DependencyInjection;
    using Utilities.Extensions;
    using Utilities.Validators;

    /// <summary>
    /// Used for building the view component which will be tested.
    /// </summary>
    public partial class ViewComponentBuilder<TViewComponent>
    {
        /// <inheritdoc />
        public IAndViewComponentBuilder<TViewComponent> WithViewComponentContext(
            ViewComponentContext viewComponentContext)
        {
            CommonValidator.CheckForNullReference(viewComponentContext, nameof(ViewComponentContext));
            this.TestContext.ViewComponentContext = ViewComponentContextMock.FromViewComponentContext(this.TestContext, viewComponentContext);
            return this;
        }

        /// <inheritdoc />
        public IAndViewComponentBuilder<TViewComponent> WithViewComponentContext(
            Action<ViewComponentContext> viewComponentContextSetup)
        {
            this.TestContext.ViewComponentContextPreparationDelegate += viewComponentContextSetup;
            return this;
        }
        
        /// <inheritdoc />
        public IAndViewComponentBuilder<TViewComponent> WithViewContext(
            ViewContext viewContext)
        {
            CommonValidator.CheckForNullReference(viewContext, nameof(ViewContext));
            this.TestContext.ComponentContext = ViewContextMock.FromViewContext(this.TestContext, viewContext);
            return this;
        }
        
        /// <inheritdoc />
        public IAndViewComponentBuilder<TViewComponent> WithViewContext(
            Action<ViewContext> viewContextSetup)
        {
            this.TestContext.ComponentContextPreparationDelegate += viewContextSetup;
            return this;
        }

        /// <inheritdoc />
        public IAndViewComponentBuilder<TViewComponent> WithActionContext(
            ActionContext actionContext)
        {
            CommonValidator.CheckForNullReference(actionContext, nameof(ActionContext));
            this.TestContext.ComponentContext = ViewContextMock.FromActionContext(this.TestContext, actionContext);
            return this;
        }

        /// <inheritdoc />
        public IAndViewComponentBuilder<TViewComponent> WithActionContext(
            Action<ActionContext> actionContextSetup)
        {
            this.TestContext.ComponentContextPreparationDelegate += actionContextSetup;
            return this;
        }
        
        protected override void PrepareComponentContext()
        {
            var viewContext = this.TestContext.ComponentContext;
            this.TestContext.ComponentContextPreparationDelegate?.Invoke(viewContext);

            var viewComponentContext = this.TestContext.ViewComponentContext;
            viewComponentContext.ViewComponentDescriptor.TypeInfo = typeof(TViewComponent).GetTypeInfo();
            this.TestContext.ViewComponentContextPreparationDelegate?.Invoke(viewComponentContext);
        }

        protected override TViewComponent TryCreateComponentWithFactory()
        {
            try
            {
                return this.Services
                    .GetService<IViewComponentFactory>()
                    ?.CreateViewComponent(this.TestContext.ViewComponentContext) as TViewComponent;
            }
            catch
            {
                return null;
            }
        }

        protected override void ActivateComponent()
        {
            this.Services
                .GetServices<IViewComponentPropertyActivator>()
                ?.ForEach(a => a.Activate(this.TestContext.ViewComponentContext, this.TestContext.Component));
        }
    }
}
