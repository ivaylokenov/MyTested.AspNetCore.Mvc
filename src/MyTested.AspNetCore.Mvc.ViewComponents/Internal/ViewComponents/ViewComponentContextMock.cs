namespace MyTested.AspNetCore.Mvc.Internal.ViewComponents
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewComponents;
    using System.Collections.Generic;
    using System.Text.Encodings.Web;
    using TestContexts;
    using Utilities.Validators;

    public class ViewComponentContextMock : ViewComponentContext
    {
        private HttpTestContext testContext;

        private ViewComponentContextMock(HttpTestContext testContext, ViewComponentContext viewComponentContext) 
            => this.PrepareViewComponentContext(testContext, viewComponentContext);

        private HttpTestContext TestContext
        {
            get => this.testContext;

            set
            {
                CommonValidator.CheckForNullReference(value, nameof(TestContext));
                this.testContext = value;
            }
        }

        public static ViewComponentContextMock FromViewContext(HttpTestContext testContext, ViewContext viewContext)
            => FromViewComponentContext(testContext, new ViewComponentContext { ViewContext = viewContext });

        public static ViewComponentContextMock FromViewComponentContext(HttpTestContext testContext, ViewComponentContext viewComponentContext)
        {
            CommonValidator.CheckForNullReference(testContext, nameof(HttpTestContext));
            CommonValidator.CheckForNullReference(viewComponentContext, nameof(ViewComponentContext));

            viewComponentContext.ViewComponentDescriptor = viewComponentContext.ViewComponentDescriptor ?? new ViewComponentDescriptor();
            viewComponentContext.ViewContext = ViewContextMock.FromViewContext(testContext, viewComponentContext.ViewContext ?? new ViewContext());
            viewComponentContext.Arguments = viewComponentContext.Arguments ?? new Dictionary<string, object>();
            viewComponentContext.HtmlEncoder = viewComponentContext.HtmlEncoder ?? HtmlEncoder.Default;

            return new ViewComponentContextMock(testContext, viewComponentContext);
        }

        private void PrepareViewComponentContext(HttpTestContext testContext, ViewComponentContext viewComponentContext)
        {
            this.TestContext = testContext;
            this.Arguments = viewComponentContext.Arguments;
            this.HtmlEncoder = viewComponentContext.HtmlEncoder;
            this.ViewComponentDescriptor = viewComponentContext.ViewComponentDescriptor;
            this.ViewContext = viewComponentContext.ViewContext;

            TestHelper.SetActionContextToAccessor(this.ViewContext);
        }
    }
}
