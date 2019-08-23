namespace MyTested.AspNetCore.Mvc.Internal.Routing
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Reflection;
    using System.Threading.Tasks;
    using Contracts;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Microsoft.AspNetCore.Mvc.Infrastructure;
    using Microsoft.Extensions.Logging;
    using Utilities.Validators;

    public class ModelBindingActionInvoker : IModelBindingActionInvoker
    {
        private readonly dynamic cacheEntry;
        private readonly ControllerContext controllerContext;
        private readonly object invoker;

        private Dictionary<string, object> arguments;

        public ModelBindingActionInvoker(
            ILogger logger,
            DiagnosticListener diagnosticListener,
            IActionContextAccessor actionContextAccessor,
            IActionResultTypeMapper mapper,
            ControllerContext controllerContext,
            dynamic cacheEntry,
            IFilterMetadata[] filters)
        {
            CommonValidator.CheckForNullReference(cacheEntry, nameof(cacheEntry));
            
            this.cacheEntry = cacheEntry;
            this.controllerContext = controllerContext;

            var invokerType = WebFramework.Internals.ControllerActionInvoker;
            var constructor = invokerType.GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic)[0];
            this.invoker = constructor.Invoke(new object[] { logger, diagnosticListener, actionContextAccessor, mapper, controllerContext, cacheEntry, filters });
        }

        public IDictionary<string, object> BoundActionArguments => this.arguments;

        public async Task InvokeAsync()
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

            await (Task)this.invoker.GetType().GetMethod(nameof(this.InvokeAsync)).Invoke(this.invoker, null);

            var controllerInstance = this.cacheEntry.ControllerFactory(this.controllerContext);

            await this.cacheEntry.ControllerBinderDelegate(this.controllerContext, controllerInstance, this.arguments);
        }
    }
}
