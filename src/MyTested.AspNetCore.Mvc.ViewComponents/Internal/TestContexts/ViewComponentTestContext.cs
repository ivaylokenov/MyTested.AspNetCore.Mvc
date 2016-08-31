namespace MyTested.AspNetCore.Mvc.Internal.TestContexts
{
    using System;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewComponents;
    using Utilities.Extensions;
    using Utilities.Validators;
    using ViewComponents;

    public class ViewComponentTestContext : ActionTestContext<ViewContext>
    {
        private ViewComponentContext viewComponentContext;

        public override string ExceptionMessagePrefix => $"When invoking {this.Component.GetName()} expected";

        protected override ViewContext DefaultComponentContext
            => ViewContextMock.Default(this);

        public ViewComponentContext ViewComponentContext
        {
            get
            {
                if (this.viewComponentContext == null)
                {
                    this.viewComponentContext = ViewComponentContextMock.FromViewContext(this, this.ComponentContext);
                }

                return this.viewComponentContext;
            }

            set
            {
                CommonValidator.CheckForNullReference(value, nameof(ViewComponentContext));
                this.viewComponentContext = value;
            }
        }

        public Action<ViewComponentContext> ViewComponentContextPreparationDelegate { get; set; }
    }
}
