namespace MyTested.AspNetCore.Mvc.Internal.Routing
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Reflection;
    using System.Threading.Tasks;
    using Contracts;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Microsoft.AspNetCore.Mvc.Infrastructure;
    using Microsoft.Extensions.Logging;
    using Utilities.Extensions;
    using Utilities.Validators;

    public class ModelBindingActionInvoker : IModelBindingActionInvoker
    {
        private static readonly Type RouteActionResultMockType = typeof(RouteActionResultMock);

        private readonly ControllerContext controllerContext;
        private readonly dynamic cacheEntry;
        private readonly dynamic cacheEntryMock;
        private readonly object invoker;

        private Dictionary<string, object> arguments;

        public ModelBindingActionInvoker(
            ILogger logger,
            DiagnosticListener diagnosticListener,
            IActionContextAccessor actionContextAccessor,
            IActionResultTypeMapper mapper,
            ControllerContext controllerContext,
            dynamic cacheEntry,
            dynamic cacheEntryMock,
            IFilterMetadata[] filters)
        {
            CommonValidator.CheckForNullReference(cacheEntry, nameof(cacheEntry));
            CommonValidator.CheckForNullReference(cacheEntryMock, nameof(cacheEntryMock));
            CommonValidator.CheckForNullReference(controllerContext, nameof(controllerContext));

            this.controllerContext = controllerContext;

            this.cacheEntry = cacheEntry;
            this.cacheEntryMock = cacheEntryMock;

            var invokerType = WebFramework.Internals.ControllerActionInvoker;
            var constructor = invokerType.GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic)[0];
            this.invoker = constructor.Invoke(new object[] { logger, diagnosticListener, actionContextAccessor, mapper, controllerContext, cacheEntryMock, filters });
        }

        public IDictionary<string, object> BoundActionArguments => this.arguments;

        public async Task InvokeAsync()
        {
            var exposedInvoker = this.invoker.Exposed();

            // Invoke the filter pipeline and execute the fake action mock.
            await exposedInvoker.InvokeAsync();

            if (exposedInvoker._result.GetType() != RouteActionResultMockType)
            {
                // Filters short-circuited the action pipeline. Do not perform model binding.
                return;
            }

            // Not initialized in the constructor because filters may
            // short-circuit the request and tests will fail with wrong exception message.
            this.arguments = new Dictionary<string, object>();

            var actionDescriptor = this.controllerContext.ActionDescriptor;
            if (actionDescriptor.BoundProperties.Count == 0 &&
                actionDescriptor.Parameters.Count == 0)
            {
                // No parameters on the real action. Do not perform model binding.
                return;
            }

            // Invokes the model binding on the real action.
            var controllerInstance = this.cacheEntry.ControllerFactory.Invoke(this.controllerContext);

            await this.cacheEntry.ControllerBinderDelegate.DynamicInvoke(this.controllerContext, controllerInstance, this.arguments);
        }
    }
}
