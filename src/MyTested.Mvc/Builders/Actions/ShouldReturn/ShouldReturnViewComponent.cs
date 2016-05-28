namespace MyTested.Mvc.Builders.Actions.ShouldReturn
{
    using System;
    using ActionResults.View;
    using Contracts.ActionResults.View;
    using Microsoft.AspNetCore.Mvc;
    using Utilities;

    /// <content>
    /// Class containing methods for testing <see cref="ViewComponentResult"/>.
    /// </content>
    public partial class ShouldReturnTestBuilder<TActionResult>
    {
        /// <inheritdoc />
        public IViewComponentTestBuilder ViewComponent()
        {
            this.TestContext.ActionResult = this.GetReturnObject<ViewComponentResult>();
            return new ViewComponentTestBuilder(this.TestContext);
        }

        /// <inheritdoc />
        public IViewComponentTestBuilder ViewComponent(string viewComponentName)
        {
            var viewComponentResult = this.GetReturnObject<ViewComponentResult>();
            var actualViewComponentName = viewComponentResult.ViewComponentName;

            if (viewComponentName != actualViewComponentName)
            {
                this.ThrowNewViewResultAssertionException(
                    "view component",
                    viewComponentName,
                    actualViewComponentName);
            }

            this.TestContext.ActionResult = viewComponentResult;
            return new ViewComponentTestBuilder(this.TestContext);
        }

        /// <inheritdoc />
        public IViewComponentTestBuilder ViewComponent(Type viewComponentType)
        {
            var viewComponentResult = this.GetReturnObject<ViewComponentResult>();
            var actualViewComponentType = viewComponentResult.ViewComponentType;

            if (viewComponentType != actualViewComponentType)
            {
                this.ThrowNewViewResultAssertionException(
                    "view component",
                    viewComponentType.ToFriendlyTypeName(),
                    actualViewComponentType.ToFriendlyTypeName());
            }

            this.TestContext.ActionResult = viewComponentResult;
            return new ViewComponentTestBuilder(this.TestContext);
        }

        /// <inheritdoc />
        public IViewComponentTestBuilder ViewComponent<TViewComponentType>()
        {
            return this.ViewComponent(typeof(TViewComponentType));
        }
    }
}
