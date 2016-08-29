namespace MyTested.AspNetCore.Mvc.Internal.ViewComponents
{
    using Microsoft.AspNetCore.Mvc.ViewComponents;
    using TestContexts;
    using Utilities.Validators;

    public class ViewComponentContextMock : ViewComponentContext
    {
        private HttpTestContext testContext;
        
        public ViewComponentContextMock(HttpTestContext testContext)
        {
            this.TestContext = testContext;
        }

        private ViewComponentContextMock(HttpTestContext testContext, ViewComponentContext viewComponentContext)
            : this (testContext)
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

        public static ViewComponentContextMock FromViewComponentContext(HttpTestContext testContext, ViewComponentContext viewComponentContext)
        {
            CommonValidator.CheckForNullReference(testContext, nameof(HttpTestContext));
            CommonValidator.CheckForNullReference(viewComponentContext, nameof(ViewComponentContext));

            viewComponentContext.ViewComponentDescriptor = viewComponentContext.ViewComponentDescriptor ?? new ViewComponentDescriptor();
            viewComponentContext.ViewContext = viewComponentContext.ViewContext ?? new ViewContextMock(testContext);

            return new ViewComponentContextMock(testContext, viewComponentContext);
        }

        private void PrepareViewComponentContext(HttpTestContext testContext)
        {
        }
    }
}
