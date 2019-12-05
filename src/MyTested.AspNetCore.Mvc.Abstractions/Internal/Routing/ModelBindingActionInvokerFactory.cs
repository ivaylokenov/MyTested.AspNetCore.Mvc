namespace MyTested.AspNetCore.Mvc.Internal.Routing
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;
    using Actions;
    using Contracts;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Abstractions;
    using Microsoft.AspNetCore.Mvc.Infrastructure;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using Utilities;
    using Utilities.Extensions;

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
            IActionResultTypeMapper mapper)
            : this (modelBindingActionInvokerCache, optionsAccessor, loggerFactory, diagnosticListener, mapper, null)
        {
        }

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

            var cacheEntry = cacheResult.Item1; // cacheEntry
            var filters = cacheResult.Item2; // filters

            dynamic exposedCacheEntry = new ExposedObject(cacheEntry);

            Action<ControllerContext, object> controllerReleaser 
                = (context, instance) => (instance as IDisposable)?.Dispose();

            Func<ControllerContext, object, Dictionary<string, object>, Task> controllerBinderDelegateFunc 
                = (context, instance, arguments) => Task.CompletedTask;

            var controllerBinderDelegateType = WebFramework.Internals.ControllerBinderDelegate;

            var controllerBinderDelegate = typeof(DelegateExtensions)
                .GetMethod(nameof(DelegateExtensions.ConvertTo))
                .MakeGenericMethod(controllerBinderDelegateType)
                .Invoke(null, new object[] { controllerBinderDelegateFunc });

            var cacheEntryObject = cacheEntry as object;

            var cacheEntryMock = cacheEntryObject
                .GetType()
                .GetConstructors(BindingFlags.NonPublic | BindingFlags.Instance)
                .First()
                .Invoke(new object[]
                {
                    exposedCacheEntry.CachedFilters,
                    exposedCacheEntry.ControllerFactory,
                    controllerReleaser,
                    controllerBinderDelegate,
                    exposedCacheEntry.ObjectMethodExecutor,
                    exposedCacheEntry.ActionMethodExecutor
                });

            var fullExecution = actionContext
                .HttpContext
                .Features
                .Get<RouteTestingFeature>()
                ?.FullExecution
                ?? false;

            return new ModelBindingActionInvoker(
                this.logger,
                this.diagnosticListener,
                this.actionContextAccessor,
                this.mapper,
                controllerContext,
                exposedCacheEntry,
                cacheEntryMock,
                filters,
                fullExecution); 
        }
    }
}
