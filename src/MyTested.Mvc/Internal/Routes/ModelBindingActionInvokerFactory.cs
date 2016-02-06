namespace MyTested.Mvc.Internal.Routes
{
    using Contracts;
    using Microsoft.AspNet.Mvc;
    using Microsoft.AspNet.Mvc.Abstractions;
    using Microsoft.AspNet.Mvc.Controllers;
    using Microsoft.AspNet.Mvc.Filters;
    using Microsoft.AspNet.Mvc.Formatters;
    using Microsoft.AspNet.Mvc.ModelBinding;
    using Microsoft.AspNet.Mvc.ModelBinding.Validation;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;

    public class ModelBindingActionInvokerFactory : IModelBindingActionInvokerFactory
    {
        private readonly IControllerActionArgumentBinder argumentBinder;
        private readonly IControllerFactory controllerFactory;
        private readonly IFilterProvider[] filterProviders;
        private readonly IReadOnlyList<IInputFormatter> inputFormatters;
        private readonly IReadOnlyList<IModelBinder> modelBinders;
        private readonly IReadOnlyList<IModelValidatorProvider> modelValidatorProviders;
        private readonly IReadOnlyList<IValueProviderFactory> valueProviderFactories;
        private readonly int maxModelValidationErrors;
        private readonly ILogger logger;
        private readonly DiagnosticSource diagnosticSource;

        public ModelBindingActionInvokerFactory(
            IControllerFactory controllerFactory,
            IEnumerable<IFilterProvider> filterProviders,
            IControllerActionArgumentBinder argumentBinder,
            IOptions<MvcOptions> optionsAccessor,
            ILoggerFactory loggerFactory,
            DiagnosticSource diagnosticSource)
        {
            this.controllerFactory = controllerFactory;
            this.argumentBinder = argumentBinder;
            this.filterProviders = filterProviders.OrderBy(item => item.Order).ToArray();
            this.inputFormatters = optionsAccessor.Value.InputFormatters.ToArray();
            this.modelBinders = optionsAccessor.Value.ModelBinders.ToArray();
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
                this.filterProviders,
                this.controllerFactory,
                controllerActionDescriptor,
                this.inputFormatters,
                this.argumentBinder,
                this.modelBinders,
                this.modelValidatorProviders,
                this.valueProviderFactories,
                this.logger,
                this.diagnosticSource,
                this.maxModelValidationErrors);
        }
    }
}
