namespace MyTested.AspNetCore.Mvc.Internal.ViewComponents
{
    using Microsoft.AspNetCore.Mvc.Abstractions;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    using Microsoft.AspNetCore.Routing;
    using TestContexts;
    using Utilities.Validators;

    public class ViewContextMock : ViewContext
    {
        private HttpTestContext testContext;

        public ViewContextMock(HttpTestContext testContext)
        {
            this.PrepareViewContext(testContext);
        }

        private ViewContextMock(HttpTestContext testContext, ViewContext viewContext)
            : this(testContext)
        {
        }

        private HttpTestContext TestContext
        {
            get
            {
                return this.testContext;
            }

            set
            {
                CommonValidator.CheckForNullReference(value, nameof(TestContext));
                this.testContext = value;
            }
        }

        public static ViewContextMock FromViewContext(HttpTestContext testContext, ViewContext viewContext)
        {
            CommonValidator.CheckForNullReference(testContext, nameof(HttpTestContext));
            CommonValidator.CheckForNullReference(viewContext, nameof(ViewContext));

            viewContext.ActionDescriptor = viewContext.ActionDescriptor ?? new ActionDescriptor();
            viewContext.FormContext = viewContext.FormContext ?? new FormContext();

            return new ViewContextMock(testContext, viewContext);
        }

        private void PrepareViewContext(HttpTestContext testContext)
        {
            this.TestContext = testContext;
            this.HttpContext = testContext.HttpContext;
            this.RouteData = testContext.RouteData ?? new RouteData();
        }
    }
}
