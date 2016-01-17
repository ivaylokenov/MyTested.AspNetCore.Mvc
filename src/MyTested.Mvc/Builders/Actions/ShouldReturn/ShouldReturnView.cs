namespace MyTested.Mvc.Builders.Actions.ShouldReturn
{
    using ActionResults.View;
    using Contracts.ActionResults.View;
    using Exceptions;
    using Microsoft.AspNet.Mvc;
    using Utilities;
    /// <summary>
    /// Class containing methods for testing ViewResult or PartialViewResult.
    /// </summary>
    /// <typeparam name="TActionResult">Result from invoked action in ASP.NET MVC 6 controller.</typeparam>
    public partial class ShouldReturnTestBuilder<TActionResult>
    {
        /// <summary>
        /// Tests whether action result is ViewResult with default view name.
        /// </summary>
        /// <returns>View test builder.</returns>
        public IViewTestBuilder View()
        {
            return this.View(null);
        }

        /// <summary>
        /// Tests whether action result is ViewResult with the specified view name.
        /// </summary>
        /// <param name="viewName">Expected view name.</param>
        /// <returns>View test builder.</returns>
        public IViewTestBuilder View(string viewName)
        {
            var viewType = "view";
            var viewResult = this.GetReturnObject<ViewResult>();
            var actualViewName = viewResult.ViewName;
            if (viewName != actualViewName)
            {
                this.ThrowNewViewResultAssertionException(viewType, viewName, actualViewName);
            }

            return new ViewTestBuilder<ViewResult>(
                this.Controller,
                this.ActionName,
                this.CaughtException,
                viewResult,
                viewType);
        }

        /// <summary>
        /// Tests whether action result is PartialViewResult with default view name.
        /// </summary>
        /// <returns>View test builder.</returns>
        public IViewTestBuilder PartialView()
        {
            return this.PartialView(null);
        }

        /// <summary>
        /// Tests whether action result is PartialViewResult with the specified view name.
        /// </summary>
        /// <param name="viewName">Expected partial view name.</param>
        /// <returns>View test builder.</returns>
        public IViewTestBuilder PartialView(string viewName)
        {
            var viewType = "partial view";
            var viewResult = this.GetReturnObject<PartialViewResult>();
            var actualViewName = viewResult.ViewName;
            if (viewName != actualViewName)
            {
                this.ThrowNewViewResultAssertionException(viewType, viewName, actualViewName);
            }

            return new ViewTestBuilder<PartialViewResult>(
                this.Controller,
                this.ActionName,
                this.CaughtException,
                viewResult,
                viewType);
        }
        
        private void ThrowNewViewResultAssertionException(string viewType, string expectedViewName, string actualViewName)
        {
            throw new ViewResultAssertionException(string.Format(
                    "When calling {0} action in {1} expected {2} result to be {3}, but instead received {4}.",
                    this.ActionName,
                    this.Controller,
                    viewType,
                    ViewTestHelper.GetFriendlyViewName(expectedViewName),
                    ViewTestHelper.GetFriendlyViewName(actualViewName)));
        }
    }
}
