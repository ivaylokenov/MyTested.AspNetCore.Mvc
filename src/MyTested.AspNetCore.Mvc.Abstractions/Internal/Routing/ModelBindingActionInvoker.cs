namespace MyTested.AspNetCore.Mvc.Internal.Routing
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Threading.Tasks;
    using Contracts;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Microsoft.AspNetCore.Mvc.Infrastructure;
    using Microsoft.AspNetCore.Mvc.Internal;
    using Microsoft.Extensions.Logging;
    using Utilities.Validators;

    public class ModelBindingActionInvoker : ResourceInvoker, IModelBindingActionInvoker
    {
        private readonly ControllerActionInvokerCacheEntry cacheEntry;
        private readonly ControllerContext controllerContext;

        private Dictionary<string, object> arguments;

        public ModelBindingActionInvoker(
            ILogger logger,
            DiagnosticListener diagnosticListener,
            IActionResultTypeMapper mapper,
            ControllerContext controllerContext,
            ControllerActionInvokerCacheEntry cacheEntry,
            IFilterMetadata[] filters)
            : base(diagnosticListener, logger, mapper, controllerContext, filters, controllerContext.ValueProviderFactories)
        {
            CommonValidator.CheckForNullReference(cacheEntry, nameof(cacheEntry));

            this.cacheEntry = cacheEntry;
            this.controllerContext = controllerContext;
        }

        public IDictionary<string, object> BoundActionArguments => this.arguments;
        
        protected override async Task InvokeInnerFilterAsync()
        {
            // Not initialized in the constructor because filters may
            // short-circuit the request and tests will fail with wrong exception message.
            this.arguments = new Dictionary<string, object>();

            var actionDescriptor = this.controllerContext.ActionDescriptor;
            if (actionDescriptor.BoundProperties.Count == 0 &&
                actionDescriptor.Parameters.Count == 0)
            {
                return;
            }

            await this.cacheEntry.ControllerBinderDelegate(this.controllerContext, _instance, this.arguments);
        }

        protected override void ReleaseResources()
        {
            // Intentionally does nothing.
        }
    }
}
