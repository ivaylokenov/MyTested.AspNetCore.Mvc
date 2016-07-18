namespace MyTested.AspNetCore.Mvc
{
    using System.Linq;
    using Internal.Caching;
    using Internal.Contracts;
    using Internal.Controllers;
    using Internal.Formatters;
    using Internal.Routes;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Abstractions;
    using Microsoft.AspNetCore.Mvc.Formatters;
    using Microsoft.AspNetCore.Mvc.Infrastructure;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Utilities.Validators;

    /// <summary>
    /// Contains core extension methods for <see cref="IServiceCollection"/>.
    /// </summary>
    public static class ServiceCollectionCoreExtensions
    {
        /// <summary>
        /// Adds <see cref="IHttpContextAccessor"/> with singleton scope to the service collection.
        /// </summary>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        /// <returns>The same <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection AddHttpContextAccessor(this IServiceCollection serviceCollection)
        {
            CommonValidator.CheckForNullReference(serviceCollection, nameof(IServiceCollection));
            return serviceCollection.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        /// <summary>
        /// Adds <see cref="IActionContextAccessor"/> with singleton scope to the service collection.
        /// </summary>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        /// <returns>The same <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection AddActionContextAccessor(this IServiceCollection serviceCollection)
        {
            CommonValidator.CheckForNullReference(serviceCollection, nameof(serviceCollection));
            return serviceCollection.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
        }

        public static IServiceCollection ReplaceMvcCore(this IServiceCollection serviceCollection)
        {
            CommonValidator.CheckForNullReference(serviceCollection, nameof(serviceCollection));

            // testing framework services
            serviceCollection.TryAddSingleton<IValidControllersCache, ValidControllersCache>();
            serviceCollection.TryAddSingleton<IControllerActionDescriptorCache, ControllerActionDescriptorCache>();
            
            // custom MVC options
            serviceCollection.Configure<MvcOptions>(options =>
            {
                // add controller conventions to save all valid controller types
                options.Conventions.Add(new ValidControllersCache());

                // string input formatter helps with HTTP request processing
                var inputFormatters = options.InputFormatters.OfType<TextInputFormatter>();
                if (!inputFormatters.Any(f => f.SupportedMediaTypes.Contains(ContentType.TextPlain)))
                {
                    options.InputFormatters.Add(new StringInputFormatter());
                }
            });

            return serviceCollection;
        }
        
        public static IServiceCollection ReplaceMvcRouting(this IServiceCollection serviceCollection)
        {
            CommonValidator.CheckForNullReference(serviceCollection, nameof(serviceCollection));

            var modelBindingActionInvokerFactoryServiceType = typeof(IModelBindingActionInvokerFactory);

            if (serviceCollection.All(s => s.ServiceType != modelBindingActionInvokerFactoryServiceType))
            {
                serviceCollection.TryAddEnumerable(
                    ServiceDescriptor.Transient<IActionInvokerProvider, ModelBindingActionInvokerProvider>());
                serviceCollection.TryAddSingleton(modelBindingActionInvokerFactoryServiceType, typeof(ModelBindingActionInvokerFactory));
            }

            return serviceCollection;
        }
    }
}
