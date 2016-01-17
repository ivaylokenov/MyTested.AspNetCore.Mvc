namespace MyTested.Mvc.Builders.Actions.ShouldReturn
{
    using System;
    using ActionResults.View;
    using Contracts.ActionResults.View;
    using Microsoft.AspNet.Mvc;
    using Utilities;

    /// <summary>
    /// Class containing methods for testing ViewComponentResult.
    /// </summary>
    /// <typeparam name="TActionResult">Result from invoked action in ASP.NET MVC 6 controller.</typeparam>
    public partial class ShouldReturnTestBuilder<TActionResult>
    {
        /// <summary>
        /// Tests whether action result is ViewComponentResult.
        /// </summary>
        /// <returns>View component test builder.</returns>
        public IViewComponentTestBuilder ViewComponent()
        {
            this.ValidateActionReturnType<ViewComponentResult>();
            return new ViewComponentTestBuilder(
                this.Controller,
                this.ActionName,
                this.CaughtException,
                this.ActionResult as ViewComponentResult);
        }

        /// <summary>
        /// Tests whether action result is ViewComponentResult with the specified view component name.
        /// </summary>
        /// <param name="viewComponentName">Expected view component name.</param>
        /// <returns>View component test builder.</returns>
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

            return new ViewComponentTestBuilder(
                this.Controller,
                this.ActionName,
                this.CaughtException,
                viewComponentResult);
        }

        /// <summary>
        /// Tests whether action result is ViewComponentResult with the specified view component type.
        /// </summary>
        /// <param name="viewComponentType">Expected view component type.</param>
        /// <returns>View component test builder.</returns>
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

            return new ViewComponentTestBuilder(
                this.Controller,
                this.ActionName,
                this.CaughtException,
                viewComponentResult);
        }

        /// <summary>
        /// Tests whether action result is ViewComponentResult with the specified view component type.
        /// </summary>
        /// <typeparam name="TViewComponentType">Expected view component type.</typeparam>
        /// <returns>View component test builder.</returns>
        public IViewComponentTestBuilder ViewComponent<TViewComponentType>()
        {
            return this.ViewComponent(typeof(TViewComponentType));
        }
    }
}
