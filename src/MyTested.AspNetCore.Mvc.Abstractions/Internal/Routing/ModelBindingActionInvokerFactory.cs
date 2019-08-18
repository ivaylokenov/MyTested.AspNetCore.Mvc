namespace MyTested.AspNetCore.Mvc.Internal.Routing
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using Contracts;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Abstractions;
    using Microsoft.AspNetCore.Mvc.Infrastructure;
    using Microsoft.AspNetCore.Mvc.Internal;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;

    public class ModelBindingActionInvokerFactory : IModelBindingActionInvokerFactory
    {
        private readonly ControllerActionInvokerCache controllerActionInvokerCache;
        private readonly IReadOnlyList<IValueProviderFactory> valueProviderFactories;
        private readonly int maxModelValidationErrors;
        private readonly ILogger logger;
        private readonly DiagnosticListener diagnosticListener;
        private readonly IActionResultTypeMapper mapper;

        public ModelBindingActionInvokerFactory(
            ControllerActionInvokerCache controllerActionInvokerCache,
            IOptions<MvcOptions> optionsAccessor,
            ILoggerFactory loggerFactory,
            DiagnosticListener diagnosticListener,
            IActionResultTypeMapper mapper)
        {
            this.controllerActionInvokerCache = controllerActionInvokerCache;
            this.valueProviderFactories = optionsAccessor.Value.ValueProviderFactories.ToArray();
            this.maxModelValidationErrors = optionsAccessor.Value.MaxModelValidationErrors;
            this.logger = loggerFactory.CreateLogger<ControllerActionInvoker>();
            this.diagnosticListener = diagnosticListener;
            this.mapper = mapper;
        }

        public IActionInvoker CreateModelBindingActionInvoker(ActionContext actionContext)
        {
            var controllerContext = new ControllerContext(actionContext)
            {
                ValueProviderFactories = new CopyOnWriteList<IValueProviderFactory>(this.valueProviderFactories)
            };

            controllerContext.ModelState.MaxAllowedErrors = this.maxModelValidationErrors;

            var cacheResult = this.controllerActionInvokerCache.GetCachedResult(controllerContext);

            return new ModelBindingActionInvoker(
                this.logger,
                this.diagnosticListener,
                this.mapper,
                controllerContext,
                cacheResult.cacheEntry,
                cacheResult.filters);
        }
    }
}
