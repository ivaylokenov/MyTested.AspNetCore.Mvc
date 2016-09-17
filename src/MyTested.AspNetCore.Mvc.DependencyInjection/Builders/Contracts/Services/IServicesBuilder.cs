namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Services
{
    using System;

    /// <summary>
    /// Used for building component service dependencies.
    /// </summary>
    public interface IServicesBuilder
    {
        /// <summary>
        /// Tries to resolve constructor service dependency of the given type.
        /// </summary>
        /// <typeparam name="TService">Type of service dependency to resolve.</typeparam>
        /// <param name="service">Instance of service dependency to inject into the component constructor.</param>
        /// <returns>The same <see cref="IServicesBuilder"/>.</returns>
        IAndServicesBuilder With<TService>(TService service)
            where TService : class;

        /// <summary>
        /// Sets null value to the constructor service dependency of the given type.
        /// </summary>
        /// <typeparam name="TService">Type of service dependency.</typeparam>
        /// <returns>The same <see cref="IServicesBuilder"/>.</returns>
        IAndServicesBuilder WithNo<TService>()
            where TService : class;

        /// <summary>
        /// Configures a service with scoped lifetime by using the provided <see cref="Action"/> delegate.
        /// </summary>
        /// <typeparam name="TService">Type of service to configure.</typeparam>
        /// <param name="scopedServiceSetup">Action configuring the service before running the test case.</param>
        /// <returns>The same <see cref="IServicesBuilder"/>.</returns>
        IAndServicesBuilder WithSetupFor<TService>(Action<TService> scopedServiceSetup)
            where TService : class;
    }
}
