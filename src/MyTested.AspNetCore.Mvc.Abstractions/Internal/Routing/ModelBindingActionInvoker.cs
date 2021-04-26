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
    using TestContexts;
    using Utilities.Extensions;
    using Utilities.Validators;

    public class ModelBindingActionInvoker : IModelBindingActionInvoker
    {
        private static readonly Type RouteActionResultMockType = typeof(RouteActionResultMock);

        private readonly ControllerContext controllerContext;
        private readonly dynamic cacheEntry;
        private readonly object invoker;

        public ModelBindingActionInvoker(
            ILogger logger,
            DiagnosticListener diagnosticListener,
            IActionContextAccessor actionContextAccessor,
            IActionResultTypeMapper mapper,
            ControllerContext controllerContext,
            dynamic cacheEntry,
            IFilterMetadata[] filters,
            bool fullExecution)
        {
            CommonValidator.CheckForNullReference(cacheEntry, nameof(cacheEntry));
            CommonValidator.CheckForNullReference(controllerContext, nameof(controllerContext));

            this.controllerContext = controllerContext;
            this.cacheEntry = cacheEntry;

            filters = this.PrepareFilters(filters, fullExecution);

            var invokerType = WebFramework.Internals.ControllerActionInvoker;
            var constructor = invokerType.GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic)[0];
            this.invoker = constructor.Invoke(new object[] { logger, diagnosticListener, actionContextAccessor, mapper, controllerContext, cacheEntry, filters });
        }

        public IDictionary<string, object> BoundActionArguments { get; private set; }

        public async Task InvokeAsync()
        {
            var exposedInvoker = this.invoker.Exposed();

            // Invoke the filter pipeline and execute the pipeline.
            await exposedInvoker.InvokeAsync();

            if (exposedInvoker._result.GetType() != RouteActionResultMockType)
            {
                // Filters short-circuited the action pipeline. Do not set model bound arguments.
                return;
            }

            var executionTestContext = this.controllerContext
                .HttpContext
                .Features
                .Get<ExecutionTestContext>();

            this.BoundActionArguments = executionTestContext.ActionArguments;
        }

        private IFilterMetadata[] PrepareFilters(IFilterMetadata[] filters, bool fullExecution)
        {
            var routeTestingActionFilter = new RouteTestingActionFilter();

            if (!fullExecution)
            {
                return new[] { routeTestingActionFilter };
            }
            else
            {
                return new List<IFilterMetadata>(filters) { routeTestingActionFilter }.ToArray();
            }
        }
    }
}
