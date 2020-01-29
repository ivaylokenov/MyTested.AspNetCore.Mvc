namespace MyTested.AspNetCore.Mvc.Internal.ViewComponents
{
    using System.IO;
    using Actions;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    using Microsoft.AspNetCore.Routing;
    using Microsoft.Extensions.DependencyInjection;
    using TestContexts;
    using Utilities.Validators;

    public class ViewContextMock : ViewContext
    {
        private ViewContextMock(ViewContext viewContext)
        {
            this.PrepareViewContext(viewContext);
        }
        
        public static ViewContext Default(HttpTestContext testContext)
            => FromViewContext(testContext, new ViewContext());

        public static ViewContext FromActionContext(HttpTestContext testContext, ActionContext actionContext)
            => FromViewContext(testContext, new ViewContext
            {
                HttpContext = actionContext.HttpContext,
                RouteData = actionContext.RouteData,
                ActionDescriptor = actionContext.ActionDescriptor,
            });

        public static ViewContext FromViewContext(HttpTestContext testContext, ViewContext viewContext)
        {
            CommonValidator.CheckForNullReference(testContext, nameof(HttpTestContext));
            CommonValidator.CheckForNullReference(viewContext, nameof(ViewContext));

            viewContext.HttpContext = viewContext.HttpContext ?? testContext.HttpContext;
            viewContext.RouteData = viewContext.RouteData ?? testContext.RouteData ?? new RouteData();
            viewContext.ActionDescriptor = viewContext.ActionDescriptor ?? ActionDescriptorMock.Default;
            viewContext.FormContext = viewContext.FormContext ?? new FormContext();
            viewContext.View = viewContext.View ?? NullView.Instance;
            viewContext.Writer = viewContext.Writer ?? TextWriter.Null;

            PrepareDataProviders(testContext, viewContext);
            ApplyHtmlHelperOptions(testContext, viewContext);
           
            return new ViewContextMock(viewContext);
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

        private void PrepareViewContext(ViewContext viewContext)
        {
            this.HttpContext = viewContext.HttpContext;
            this.RouteData = viewContext.RouteData;
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
