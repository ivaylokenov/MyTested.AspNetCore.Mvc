namespace MyTested.AspNetCore.Mvc.Internal.ViewComponents
{
    using System.IO;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    using Microsoft.AspNetCore.Routing;
    using Microsoft.Extensions.DependencyInjection;
    using TestContexts;
    using Utilities.Validators;
    using Microsoft.AspNetCore.Mvc.ViewFeatures.Internal;

    public class ViewContextMock : ViewContext
    {
        private HttpTestContext testContext;
        
        private ViewContextMock(HttpTestContext testContext, ViewContext viewContext)
        {
            this.PrepareViewContext(testContext, viewContext);
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

            viewContext.ActionDescriptor = viewContext.ActionDescriptor ?? ActionDescriptorMock.Default;
            viewContext.FormContext = viewContext.FormContext ?? new FormContext();
            viewContext.View = viewContext.View ?? NullView.Instance;
            viewContext.Writer = viewContext.Writer ?? TextWriter.Null;

            PrepareDataProviders(testContext, viewContext);
            ApplyHtmlHelperOptions(testContext, viewContext);
           
            return new ViewContextMock(testContext, viewContext);
        }

        private static void PrepareDataProviders(HttpTestContext testContext, ViewContext viewContext)
        {
            viewContext.ViewData = viewContext.ViewData ?? ViewDataDictionaryMock.FromViewContext(viewContext);
            viewContext.TempData = viewContext.TempData 
                ?? testContext
                .HttpContext
                .RequestServices
                .GetService<ITempDataDictionaryFactory>()
                ?.GetTempData(testContext.HttpContext);
        }

        private static void ApplyHtmlHelperOptions(HttpTestContext testContext, ViewContext viewContext)
        {
            var htmlHelperOptions = testContext.HttpContext.RequestServices.GetService<MvcViewOptions>()?.HtmlHelperOptions;
            if (htmlHelperOptions != null)
            {
                if (string.IsNullOrEmpty(viewContext.ValidationMessageElement))
                {
                    viewContext.ValidationMessageElement = htmlHelperOptions.ValidationMessageElement;
                }

                if (string.IsNullOrEmpty(viewContext.ValidationSummaryMessageElement))
                {
                    viewContext.ValidationSummaryMessageElement = htmlHelperOptions.ValidationSummaryMessageElement;
                }
            }
        }

        private void PrepareViewContext(HttpTestContext testContext, ViewContext viewContext)
        {
            this.TestContext = testContext;
            this.HttpContext = testContext.HttpContext;
            this.RouteData = testContext.RouteData ?? new RouteData();
            this.ActionDescriptor = viewContext.ActionDescriptor;
            this.ClientValidationEnabled = viewContext.ClientValidationEnabled;
            this.ExecutingFilePath = viewContext.ExecutingFilePath;
            this.FormContext = viewContext.FormContext;
            this.Html5DateRenderingMode = viewContext.Html5DateRenderingMode;
            this.TempData = viewContext.TempData;
            this.ValidationMessageElement = viewContext.ValidationMessageElement;
            this.ValidationSummaryMessageElement = viewContext.ValidationSummaryMessageElement;
            this.View = viewContext.View;
            this.ViewData = viewContext.ViewData;
            this.Writer = viewContext.Writer;
        }
    }
}
