namespace MyTested.AspNetCore.Mvc.Utilities.Validators
{
    using System;
    using Internal.Application;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Validator class containing application services validation logic.
    /// </summary>
    public static class ServiceValidator
    {
        /// <summary>
        /// Validates whether application services are registered.
        /// </summary>
        public static void ValidateServices()
        {
            CommonValidator.CheckForNullReference(
                TestServiceProvider.Global,
                "'StartsFrom' method should be called before running this test case. MyMvc must be configured and services");
        }

        /// <summary>
        /// Validates whether service exists.
        /// </summary>
        /// <typeparam name="TService">Type of service to validate.</typeparam>
        /// <param name="service">Service object to validate.</param>
        public static void ValidateServiceExists<TService>(TService service)
        {
            CommonValidator.CheckForNullReference(
                service,
                $"{typeof(TService).Name} could not be resolved from the services provider. Before running this test case, the service should be registered in the 'StartsFrom' method and");
        }

        /// <summary>
        /// Validates whether service exists and has scoped <see cref="ServiceLifetime"/>.
        /// </summary>
        /// <typeparam name="TService">Type of service to validate.</typeparam>
        /// <param name="methodName">Name of the calling method.</param>
        public static void ValidateScopedServiceLifetime<TService>(string methodName)
            where TService : class
        {
            ValidateScopedServiceLifetime(typeof(TService), methodName);
        }

        /// <summary>
        /// Validates whether service exists and has scoped <see cref="ServiceLifetime"/>.
        /// </summary>
        /// <param name="service">Type of service to validate.</param>
        /// <param name="methodName">Name of the calling method.</param>
        public static void ValidateScopedServiceLifetime(Type service, string methodName)
        {
            var serviceLifetime = TestServiceProvider.GetServiceLifetime(service);
            if (serviceLifetime != ServiceLifetime.Scoped)
            {
                throw new InvalidOperationException($"The '{methodName}' method can be used only for services with scoped lifetime.");
            }
        }

        /// <summary>
        /// Validates whether formatter exists.
        /// </summary>
        /// <param name="formatter">Formatter to validate.</param>
        /// <param name="contentType">Content type for which a formatter was not resolved.</param>
        public static void ValidateFormatterExists(object formatter, string contentType)
        {
            CommonValidator.CheckForNullReference(
                formatter,
                $"Formatter able to process '{contentType}' could not be resolved from the services provider. Before running this test case, the formatter should be registered in the 'StartsFrom' method and");
        }
    }
}
