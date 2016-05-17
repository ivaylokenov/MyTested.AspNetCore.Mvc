namespace MyTested.Mvc.Internal.Routes
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using Contracts;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Abstractions;
    using Microsoft.AspNetCore.Mvc.Controllers;
    using Microsoft.AspNetCore.Mvc.Formatters;
    using Microsoft.AspNetCore.Mvc.Internal;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;

    public class ModelBindingActionInvokerFactory : IModelBindingActionInvokerFactory
    {
        private readonly IControllerActionArgumentBinder argumentBinder;
        private readonly IControllerFactory controllerFactory;
        private readonly ControllerActionInvokerCache controllerActionInvokerCache;
        private readonly IReadOnlyList<IInputFormatter> inputFormatters;
        private readonly IReadOnlyList<IModelValidatorProvider> modelValidatorProviders;
        private readonly IReadOnlyList<IValueProviderFactory> valueProviderFactories;
        private readonly int maxModelValidationErrors;
        private readonly ILogger logger;
        private readonly DiagnosticSource diagnosticSource;

        public ModelBindingActionInvokerFactory(
            IControllerFactory controllerFactory,
            ControllerActionInvokerCache controllerActionInvokerCache,
            IControllerActionArgumentBinder argumentBinder,
            IOptions<MvcOptions> optionsAccessor,
            ILoggerFactory loggerFactory,
            DiagnosticSource diagnosticSource)
        {
            this.controllerFactory = controllerFactory;
            this.argumentBinder = argumentBinder;
            this.controllerActionInvokerCache = controllerActionInvokerCache;
            this.inputFormatters = optionsAccessor.Value.InputFormatters.ToArray();
            this.modelValidatorProviders = optionsAccessor.Value.ModelValidatorProviders.ToArray();
            this.valueProviderFactories = optionsAccessor.Value.ValueProviderFactories.ToArray();
            this.maxModelValidationErrors = optionsAccessor.Value.MaxModelValidationErrors;
            this.logger = loggerFactory.CreateLogger<ControllerActionInvoker>();
            this.diagnosticSource = diagnosticSource;
        }

        public IActionInvoker CreateModelBindingActionInvoker(
            ActionContext actionContext,
            ControllerActionDescriptor controllerActionDescriptor)
        {
            return new ModelBindingActionInvoker(
                actionContext,
                this.controllerActionInvokerCache,
                this.controllerFactory,
                controllerActionDescriptor,
                this.inputFormatters,
                this.argumentBinder,
                this.modelValidatorProviders,
                this.valueProviderFactories,
                this.logger,
                this.diagnosticSource,
                this.maxModelValidationErrors);
        }
    }
}
