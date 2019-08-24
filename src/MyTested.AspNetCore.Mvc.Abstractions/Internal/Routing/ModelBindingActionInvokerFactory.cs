namespace MyTested.AspNetCore.Mvc.Internal.Routing
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using Actions;
    using Contracts;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Abstractions;
    using Microsoft.AspNetCore.Mvc.Infrastructure;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;

    public class ModelBindingActionInvokerFactory : IModelBindingActionInvokerFactory
    {
        private readonly ModelBindingActionInvokerCache modelBindingActionInvokerCache;
        private readonly IReadOnlyList<IValueProviderFactory> valueProviderFactories;
        private readonly int maxModelValidationErrors;
        private readonly ILogger logger;
        private readonly DiagnosticListener diagnosticListener;
        private readonly IActionResultTypeMapper mapper;
        private readonly IActionContextAccessor actionContextAccessor;

        public ModelBindingActionInvokerFactory(
            ModelBindingActionInvokerCache modelBindingActionInvokerCache,
            IOptions<MvcOptions> optionsAccessor,
            ILoggerFactory loggerFactory,
            DiagnosticListener diagnosticListener,
            IActionResultTypeMapper mapper,
            IActionContextAccessor actionContextAccessor)
        {
            this.modelBindingActionInvokerCache = modelBindingActionInvokerCache;
            this.valueProviderFactories = optionsAccessor.Value.ValueProviderFactories.ToArray();
            this.maxModelValidationErrors = optionsAccessor.Value.MaxModelValidationErrors;
            this.logger = loggerFactory.CreateLogger<ModelBindingActionInvoker>();
            this.diagnosticListener = diagnosticListener;
            this.mapper = mapper;
            this.actionContextAccessor = actionContextAccessor ?? ActionContextAccessorMock.Null;
        }

        public IActionInvoker CreateModelBindingActionInvoker(ActionContext actionContext)
        {
            var controllerContext = new ControllerContext(actionContext)
            {
                ValueProviderFactories = new List<IValueProviderFactory>(this.valueProviderFactories)
            };

            controllerContext.ModelState.MaxAllowedErrors = this.maxModelValidationErrors;

            var cacheResult = this.modelBindingActionInvokerCache.GetCachedResult(controllerContext);

            return new ModelBindingActionInvoker(
                this.logger,
                this.diagnosticListener,
                this.actionContextAccessor,
                this.mapper,
                controllerContext,
                cacheResult.cacheEntry,
                cacheResult.filters);
        }
    }
}
