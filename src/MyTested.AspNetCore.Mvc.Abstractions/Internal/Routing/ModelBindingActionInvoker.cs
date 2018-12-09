namespace MyTested.AspNetCore.Mvc.Internal.Routing
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Threading.Tasks;
    using Contracts;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Microsoft.AspNetCore.Mvc.Infrastructure;
    using Microsoft.AspNetCore.Mvc.Internal;
    using Microsoft.Extensions.Logging;

    public class ModelBindingActionInvoker : ResourceInvoker, IModelBindingActionInvoker
    {
        private readonly ControllerActionInvokerCacheEntry cacheEntry;
        private readonly ControllerContext controllerContext;
        private readonly Dictionary<string, object> arguments;

        public ModelBindingActionInvoker(
            ILogger logger,
            DiagnosticListener diagnosticListener,
            IActionResultTypeMapper mapper,
            ControllerContext controllerContext,
            ControllerActionInvokerCacheEntry cacheEntry,
            IFilterMetadata[] filters)
            : base(diagnosticListener, logger, mapper, controllerContext, filters, controllerContext.ValueProviderFactories)
        {
            if (cacheEntry == null)
            {
                throw new ArgumentNullException(nameof(cacheEntry));
            }

            this.cacheEntry = cacheEntry;
            this.controllerContext = controllerContext;

            this.arguments = new Dictionary<string, object>();
        }

        public IDictionary<string, object> BoundActionArguments => this.arguments;
        
        protected override async Task InvokeInnerFilterAsync()
        {
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
            // intentionally does nothing
        }
    }
}
