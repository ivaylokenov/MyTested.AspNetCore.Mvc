namespace MyTested.Mvc.Builders.Actions.ShouldReturn
{
    using ActionResults.View;
    using Contracts.ActionResults.View;
    using Exceptions;
    using Internal;
    using Microsoft.AspNetCore.Mvc;
    using Utilities.Extensions;

    /// <summary>
    /// Class containing methods for testing <see cref="ViewResult"/> or <see cref="PartialViewResult"/>.
    /// </summary>
    public partial class ShouldReturnTestBuilder<TActionResult>
    {
        /// <inheritdoc />
        public IViewTestBuilder View()
        {
            return this.View(null);
        }

        /// <inheritdoc />
        public IViewTestBuilder View(string viewName)
        {
            var viewType = "view";
            var viewResult = this.GetReturnObject<ViewResult>();
            var actualViewName = viewResult.ViewName;
            if (viewName != actualViewName)
            {
                this.ThrowNewViewResultAssertionException(viewType, viewName, actualViewName);
            }

            return new ViewTestBuilder<ViewResult>(this.TestContext, viewType);
        }

        /// <inheritdoc />
        public IViewTestBuilder View<TModel>(TModel model)
        {
            return View(null, model);
        }

        /// <inheritdoc />
        public IViewTestBuilder View<TModel>(string viewName, TModel model)
        {
            var viewTestBuilder = this.View(viewName);
            viewTestBuilder.WithModel(model);
            return viewTestBuilder;
        }

        /// <inheritdoc />
        public IViewTestBuilder PartialView()
        {
            return this.PartialView(null);
        }

        /// <inheritdoc />
        public IViewTestBuilder PartialView(string viewName)
        {
            var viewType = "partial view";
            var viewResult = this.GetReturnObject<PartialViewResult>();
            var actualViewName = viewResult.ViewName;
            if (viewName != actualViewName)
            {
                this.ThrowNewViewResultAssertionException(viewType, viewName, actualViewName);
            }

            return new ViewTestBuilder<PartialViewResult>(this.TestContext, viewType);
        }

        /// <inheritdoc />
        public IViewTestBuilder PartialView<TModel>(TModel model)
        {
            return PartialView(null, model);
        }

        /// <inheritdoc />
        public IViewTestBuilder PartialView<TModel>(string viewName, TModel model)
        {
            var viewTestBuilder = this.PartialView(viewName);
            viewTestBuilder.WithModel(model);
            return viewTestBuilder;
        }

        private void ThrowNewViewResultAssertionException(string viewType, string expectedViewName, string actualViewName)
        {
            throw new ViewResultAssertionException(string.Format(
                    "When calling {0} action in {1} expected {2} result to be {3}, but instead received {4}.",
                    this.ActionName,
                    this.Controller.GetName(),
                    viewType,
                    ViewTestHelper.GetFriendlyViewName(expectedViewName),
                    ViewTestHelper.GetFriendlyViewName(actualViewName)));
        }
    }
}
