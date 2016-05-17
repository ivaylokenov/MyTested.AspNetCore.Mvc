namespace MyTested.Mvc
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Internal;
    using Internal.Caching;
    using Internal.Http;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Http.Internal;
    using Microsoft.AspNetCore.Mvc.Infrastructure;
    using Microsoft.AspNetCore.Mvc.Internal;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    using Microsoft.AspNetCore.Session;
    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Utilities.Extensions;
    using Utilities.Validators;

    /// <summary>
    /// Provides useful <see cref="IServiceCollection"/> extensions for testing purposes.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds <see cref="IHttpContextAccessor"/> with singleton scope to the service collection.
        /// </summary>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        public static void AddHttpContextAccessor(this IServiceCollection serviceCollection)
        {
            CommonValidator.CheckForNullReference(serviceCollection, nameof(IServiceCollection));
            serviceCollection.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        /// <summary>
        /// Adds <see cref="IActionContextAccessor"/> with singleton scope to the service collection.
        /// </summary>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        public static void AddActionContextAccessor(this IServiceCollection serviceCollection)
        {
            CommonValidator.CheckForNullReference(serviceCollection, nameof(serviceCollection));
            serviceCollection.TryAddSingleton<IActionContextAccessor, ActionContextAccessor>();
        }

        /// <summary>
        /// Adds the provided controller types as services to the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        /// <param name="controllerTypes">Controller types to add.</param>
        public static void AddMvcControllersAsServices(this IServiceCollection serviceCollection, params Type[] controllerTypes)
        {
            serviceCollection.AddMvcControllersAsServices(controllerTypes.AsEnumerable());
        }

        /// <summary>
        /// Adds the provided controller types as services to the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        /// <param name="controllerTypes">Controller types to add.</param>
        public static void AddMvcControllersAsServices(this IServiceCollection serviceCollection, IEnumerable<Type> controllerTypes)
        {
            // TODO: Application parts
            // ControllersAsServices.AddControllersAsServices(serviceCollection, controllerTypes);
        }

        /// <summary>
        /// Adds the provided controller type assemblies as services to the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        /// <param name="controllerAssemblies">Assemblies containing controller types.</param>
        public static void AddMvcControllersAsServices(this IServiceCollection serviceCollection, params Assembly[] controllerAssemblies)
        {
            serviceCollection.AddMvcControllersAsServices(controllerAssemblies.AsEnumerable());
        }

        /// <summary>
        /// Adds the provided controller type assemblies as services to the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        /// <param name="controllerAssemblies">Assemblies containing controller types.</param>
        public static void AddMvcControllersAsServices(this IServiceCollection serviceCollection, IEnumerable<Assembly> controllerAssemblies)
        {
            // TODO: Application parts
            // ControllersAsServices.AddControllersAsServices(serviceCollection, controllerAssemblies);
        }

        /// <summary>
        /// Replaces the default <see cref="ITempDataProvider"/> with a mocked implementation.
        /// </summary>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        public static void ReplaceTempDataProvider(this IServiceCollection serviceCollection)
        {
            serviceCollection.TryReplaceSingleton<ITempDataProvider, MockedTempDataProvider>();
        }

        /// <summary>
        /// Replaces the default <see cref="IMemoryCache"/> with a mocked implementation.
        /// </summary>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        public static void ReplaceMemoryCache(this IServiceCollection serviceCollection)
        {
            serviceCollection.TryReplace<IMemoryCache, MockedMemoryCache>(ServiceLifetime.Transient);
        }

        /// <summary>
        /// Replaces the default <see cref="ISessionStore"/> with a mocked implementation.
        /// </summary>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        public static void ReplaceSession(this IServiceCollection serviceCollection)
        {
            serviceCollection.TryReplaceTransient<ISessionStore, MockedSessionStore>();
        }

        /// <summary>
        /// Tries to remove а service from the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        /// <param name="service">Type of the service which will be removed.</param>
        public static void TryRemove(this IServiceCollection serviceCollection, Type service)
        {
            CommonValidator.CheckForNullReference(service, nameof(service));
            RemoveServices(serviceCollection, s => s.ServiceType == service);
        }

        /// <summary>
        /// Tries to remove a service from the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        /// <param name="service">Type of the service which will be removed.</param>
        /// <param name="implementationType">Service implementation type which will be removed.</param>
        public static void TryRemove(this IServiceCollection serviceCollection, Type service, Type implementationType)
        {
            CommonValidator.CheckForNullReference(service, nameof(service));
            RemoveServices(serviceCollection, s => s.ServiceType == service && s.ImplementationType == implementationType);
        }

        /// <summary>
        /// Tries to remove a service from the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <typeparam name="TServive">Type of the service which will be removed.</typeparam>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        public static void TryRemove<TServive>(this IServiceCollection serviceCollection)
            where TServive : class
        {
            serviceCollection.TryRemove(typeof(TServive));
        }

        /// <summary>
        /// Tries to remove a service from the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <typeparam name="TServive">Type of the service which will be removed.</typeparam>
        /// <typeparam name="TImplementation">Service implementation type which will be removed.</typeparam>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        public static void TryRemove<TServive, TImplementation>(this IServiceCollection serviceCollection)
            where TServive : class
            where TImplementation : class, TServive
        {
            serviceCollection.TryRemove(typeof(TServive), typeof(TImplementation));
        }

        /// <summary>
        /// Tries to remove a transient service from the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        /// <param name="service">Type of the service which will be removed.</param>
        public static void TryRemoveTransient(this IServiceCollection serviceCollection, Type service)
        {
            CommonValidator.CheckForNullReference(service, nameof(service));
            RemoveServices(serviceCollection, s => s.ServiceType == service && s.Lifetime == ServiceLifetime.Transient);
        }

        /// <summary>
        /// Tries to remove a transient service from the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        /// <param name="service">Type of the service which will be removed.</param>
        /// <param name="implementationType">Service implementation type which will be removed.</param>
        public static void TryRemoveTransient(this IServiceCollection serviceCollection, Type service, Type implementationType)
        {
            CommonValidator.CheckForNullReference(service, nameof(service));
            CommonValidator.CheckForNullReference(implementationType, nameof(implementationType));
            RemoveServices(serviceCollection, s => s.ServiceType == service && s.ImplementationType == implementationType && s.Lifetime == ServiceLifetime.Transient);
        }

        /// <summary>
        /// Tries to remove a transient service from the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <typeparam name="TServive">Type of the service which will be removed.</typeparam>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        public static void TryRemoveTransient<TServive>(this IServiceCollection serviceCollection)
            where TServive : class
        {
            serviceCollection.TryRemoveTransient(typeof(TServive));
        }

        /// <summary>
        /// Tries to remove a transient service from the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <typeparam name="TServive">Type of the service which will be removed.</typeparam>
        /// <typeparam name="TImplementation">Service implementation type which will be removed.</typeparam>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        public static void TryRemoveTransient<TServive, TImplementation>(this IServiceCollection serviceCollection)
            where TServive : class
            where TImplementation : class, TServive
        {
            serviceCollection.TryRemoveTransient(typeof(TServive), typeof(TImplementation));
        }

        /// <summary>
        /// Tries to remove a singleton service from the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        /// <param name="service">Type of the service which will be removed.</param>
        public static void TryRemoveSingleton(this IServiceCollection serviceCollection, Type service)
        {
            CommonValidator.CheckForNullReference(service, nameof(service));
            RemoveServices(serviceCollection, s => s.ServiceType == service && s.Lifetime == ServiceLifetime.Singleton);
        }

        /// <summary>
        /// Tries to remove a singleton service from the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        /// <param name="service">Type of the service which will be removed.</param>
        /// <param name="implementationType">Service implementation type which will be removed.</param>
        public static void TryRemoveSingleton(this IServiceCollection serviceCollection, Type service, Type implementationType)
        {
            CommonValidator.CheckForNullReference(service, nameof(service));
            CommonValidator.CheckForNullReference(implementationType, nameof(implementationType));
            RemoveServices(serviceCollection, s => s.ServiceType == service && s.ImplementationType == implementationType && s.Lifetime == ServiceLifetime.Singleton);
        }

        /// <summary>
        /// Tries to remove a singleton service from the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <typeparam name="TServive">Type of the service which will be removed.</typeparam>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        public static void TryRemoveSingleton<TServive>(this IServiceCollection serviceCollection)
            where TServive : class
        {
            serviceCollection.TryRemoveSingleton(typeof(TServive));
        }

        /// <summary>
        /// Tries to remove a singleton service from the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <typeparam name="TServive">Type of the service which will be removed.</typeparam>
        /// <typeparam name="TImplementation">Service implementation type which will be removed.</typeparam>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        public static void TryRemoveSingleton<TServive, TImplementation>(this IServiceCollection serviceCollection)
            where TServive : class
            where TImplementation : class, TServive
        {
            serviceCollection.TryRemoveSingleton(typeof(TServive), typeof(TImplementation));
        }

        /// <summary>
        /// Tries to remove a scoped service from the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        /// <param name="service">Type of the service which will be removed.</param>
        public static void TryRemoveScoped(this IServiceCollection serviceCollection, Type service)
        {
            CommonValidator.CheckForNullReference(service, nameof(service));
            RemoveServices(serviceCollection, s => s.ServiceType == service && s.Lifetime == ServiceLifetime.Scoped);
        }

        /// <summary>
        /// Tries to remove a scoped service from the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        /// <param name="service">Type of the service which will be removed.</param>
        /// <param name="implementationType">Service implementation type which will be removed.</param>
        public static void TryRemoveScoped(this IServiceCollection serviceCollection, Type service, Type implementationType)
        {
            CommonValidator.CheckForNullReference(service, nameof(service));
            CommonValidator.CheckForNullReference(implementationType, nameof(implementationType));
            RemoveServices(serviceCollection, s => s.ServiceType == service && s.ImplementationType == implementationType && s.Lifetime == ServiceLifetime.Scoped);
        }

        /// <summary>
        /// Tries to remove a scoped service from the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <typeparam name="TServive">Type of the service which will be removed.</typeparam>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        public static void TryRemoveScoped<TServive>(this IServiceCollection serviceCollection)
            where TServive : class
        {
            serviceCollection.TryRemoveScoped(typeof(TServive));
        }

        /// <summary>
        /// Tries to remove a scoped service from the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <typeparam name="TServive">Type of the service which will be removed.</typeparam>
        /// <typeparam name="TImplementation">Service implementation type which will be removed.</typeparam>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        public static void TryRemoveScoped<TServive, TImplementation>(this IServiceCollection serviceCollection)
            where TServive : class
            where TImplementation : class, TServive
        {
            serviceCollection.TryRemoveScoped(typeof(TServive), typeof(TImplementation));
        }

        /// <summary>
        /// Tries to replace a service in the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        /// <param name="service">Type of the service which will be replaced.</param>
        /// <param name="implementationType">Service implementation type which will be used for replacement.</param>
        /// <param name="lifetime">The <see cref="ServiceLifetime"/> which will be applied on the replaced service.</param>
        public static void TryReplace(this IServiceCollection serviceCollection, Type service, Type implementationType, ServiceLifetime lifetime)
        {
            serviceCollection.TryRemove(service);
            serviceCollection.TryAdd(ServiceDescriptor.Describe(service, implementationType, lifetime));
        }

        /// <summary>
        /// Tries to replace a service in the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        /// <param name="service">Type of the service which will be replaced.</param>
        /// <param name="implementationFactory">Service implementation factory which will be used for replacement.</param>
        /// <param name="lifetime">The <see cref="ServiceLifetime"/> which will be applied on the replaced service.</param>
        public static void TryReplace(this IServiceCollection serviceCollection, Type service, Func<IServiceProvider, object> implementationFactory, ServiceLifetime lifetime)
        {
            serviceCollection.TryRemove(service);
            serviceCollection.TryAdd(ServiceDescriptor.Describe(service, implementationFactory, lifetime));
        }

        /// <summary>
        /// Tries to replace a service in the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <typeparam name="TService">Type of the service which will be replaced.</typeparam>
        /// <typeparam name="TImplementation">Service implementation type which will be used for replacement.</typeparam>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        /// <param name="lifetime">The <see cref="ServiceLifetime"/> which will be applied on the replaced service.</param>
        public static void TryReplace<TService, TImplementation>(this IServiceCollection serviceCollection, ServiceLifetime lifetime)
            where TService : class
            where TImplementation : class, TService
        {
            serviceCollection.TryReplace(typeof(TService), typeof(TImplementation), lifetime);
        }

        /// <summary>
        /// Tries to replace a transient service in the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        /// <param name="service">Type of the service which will be replaced.</param>
        /// <param name="implementationType">Service implementation type which will be used for replacement.</param>
        public static void TryReplaceTransient(this IServiceCollection serviceCollection, Type service, Type implementationType)
        {
            serviceCollection.TryRemoveTransient(service);
            serviceCollection.AddTransient(service, implementationType);
        }

        /// <summary>
        /// Tries to replace a transient service in the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        /// <param name="service">Type of the service which will be replaced.</param>
        /// <param name="implementationFactory">Service implementation factory which will be used for replacement.</param>
        public static void TryReplaceTransient(this IServiceCollection serviceCollection, Type service, Func<IServiceProvider, object> implementationFactory)
        {
            serviceCollection.TryRemove(service);
            serviceCollection.AddTransient(service, implementationFactory);
        }

        /// <summary>
        /// Tries to replace a transient service in the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <typeparam name="TService">Type of the service which will be replaced.</typeparam>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        /// <param name="implementationFactory">Service implementation factory which will be used for replacement.</param>
        public static void TryReplaceTransient<TService>(this IServiceCollection serviceCollection, Func<IServiceProvider, object> implementationFactory)
            where TService : class
        {
            serviceCollection.TryReplaceTransient(typeof(TService), implementationFactory);
        }

        /// <summary>
        /// Tries to replace a transient service in the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <typeparam name="TService">Type of the service which will be replaced.</typeparam>
        /// <typeparam name="TImplementation">Service implementation type which will be used for replacement.</typeparam>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        public static void TryReplaceTransient<TService, TImplementation>(this IServiceCollection serviceCollection)
            where TService : class
            where TImplementation : class, TService
        {
            serviceCollection.TryReplaceTransient(typeof(TService), typeof(TImplementation));
        }

        /// <summary>
        /// Tries to replace a singleton service in the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        /// <param name="service">Type of the service which will be replaced.</param>
        /// <param name="implementationType">Service implementation type which will be used for replacement.</param>
        public static void TryReplaceSingleton(this IServiceCollection serviceCollection, Type service, Type implementationType)
        {
            serviceCollection.TryRemoveSingleton(service);
            serviceCollection.AddSingleton(service, implementationType);
        }

        /// <summary>
        /// Tries to replace a singleton service in the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        /// <param name="service">Type of the service which will be replaced.</param>
        /// <param name="implementationFactory">Service implementation factory which will be used for replacement.</param>
        public static void TryReplaceSingleton(this IServiceCollection serviceCollection, Type service, Func<IServiceProvider, object> implementationFactory)
        {
            serviceCollection.TryRemoveSingleton(service);
            serviceCollection.AddSingleton(service, implementationFactory);
        }

        /// <summary>
        /// Tries to replace a singleton service in the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        /// <param name="service">Type of the service which will be replaced.</param>
        /// <param name="implementationInstance">Service implementation instance which will be used for replacement.</param>
        public static void TryReplaceSingleton(this IServiceCollection serviceCollection, Type service, object implementationInstance)
        {
            serviceCollection.TryRemoveSingleton(service);
            serviceCollection.AddSingleton(service, implementationInstance);
        }

        /// <summary>
        /// Tries to replace a singleton service in the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <typeparam name="TService">Type of the service which will be replaced.</typeparam>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        /// <param name="implementationFactory">Service implementation factory which will be used for replacement.</param>
        public static void TryReplaceSingleton<TService>(this IServiceCollection serviceCollection, Func<IServiceProvider, object> implementationFactory)
            where TService : class
        {
            serviceCollection.TryReplaceSingleton(typeof(TService), implementationFactory);
        }

        /// <summary>
        /// Tries to replace a singleton service in the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <typeparam name="TService">Type of the service which will be replaced.</typeparam>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        /// <param name="implementationInstance">Service implementation instance which will be used for replacement.</param>
        public static void TryReplaceSingleton<TService>(this IServiceCollection serviceCollection, object implementationInstance)
            where TService : class
        {
            serviceCollection.TryReplaceSingleton(typeof(TService), implementationInstance);
        }

        /// <summary>
        /// Tries to replace a singleton service in the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <typeparam name="TService">Type of the service which will be replaced.</typeparam>
        /// <typeparam name="TImplementation">Service implementation type which will be used for replacement.</typeparam>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        public static void TryReplaceSingleton<TService, TImplementation>(this IServiceCollection serviceCollection)
            where TService : class
            where TImplementation : class, TService
        {
            serviceCollection.TryReplaceSingleton(typeof(TService), typeof(TImplementation));
        }

        /// <summary>
        /// Tries to replace a scoped service in the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        /// <param name="service">Type of the service which will be replaced.</param>
        /// <param name="implementationType">Service implementation type which will be used for replacement.</param>
        public static void TryReplaceScoped(this IServiceCollection serviceCollection, Type service, Type implementationType)
        {
            serviceCollection.TryRemoveScoped(service);
            serviceCollection.AddScoped(service, implementationType);
        }

        /// <summary>
        /// Tries to replace a scoped service in the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        /// <param name="service">Type of the service which will be replaced.</param>
        /// <param name="implementationFactory">Service implementation factory which will be used for replacement.</param>
        public static void TryReplaceScoped(this IServiceCollection serviceCollection, Type service, Func<IServiceProvider, object> implementationFactory)
        {
            serviceCollection.TryRemoveScoped(service);
            serviceCollection.AddScoped(service, implementationFactory);
        }

        /// <summary>
        /// Tries to replace a scoped service in the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <typeparam name="TService">Type of the service which will be replaced.</typeparam>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        /// <param name="implementationFactory">Service implementation factory which will be used for replacement.</param>
        public static void TryReplaceScoped<TService>(this IServiceCollection serviceCollection, Func<IServiceProvider, object> implementationFactory)
            where TService : class
        {
            serviceCollection.TryReplaceScoped(typeof(TService), implementationFactory);
        }

        /// <summary>
        /// Tries to replace a scoped service in the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <typeparam name="TService">Type of the service which will be replaced.</typeparam>
        /// <typeparam name="TImplementation">Service implementation type which will be used for replacement.</typeparam>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        public static void TryReplaceScoped<TService, TImplementation>(this IServiceCollection serviceCollection)
            where TService : class
            where TImplementation : class, TService
        {
            serviceCollection.TryReplaceScoped(typeof(TService), typeof(TImplementation));
        }

        /// <summary>
        /// Tries to replace multiple services in the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        /// <param name="descriptor"><see cref="ServiceDescriptor"/> providing the services.</param>
        public static void TryReplaceEnumerable(this IServiceCollection serviceCollection, ServiceDescriptor descriptor)
        {
            CommonValidator.CheckForNullReference(descriptor, nameof(descriptor));
            RemoveServices(serviceCollection, s => s.ServiceType == descriptor.ServiceType && s.Lifetime == descriptor.Lifetime);
            serviceCollection.TryAddEnumerable(descriptor);
        }

        /// <summary>
        /// Tries to replace multiple services in the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        /// <param name="descriptors"><see cref="ServiceDescriptor"/> providing the services.</param>
        public static void TryReplaceEnumerable(this IServiceCollection serviceCollection, IEnumerable<ServiceDescriptor> descriptors)
        {
            CommonValidator.CheckForNullReference(descriptors, nameof(descriptors));
            descriptors.ForEach(d => serviceCollection.TryReplaceEnumerable(d));
        }

        private static void RemoveServices(IServiceCollection serviceCollection, Func<ServiceDescriptor, bool> predicate)
        {
            CommonValidator.CheckForNullReference(serviceCollection, nameof(IServiceCollection));

            serviceCollection
                .Where(predicate)
                .ToArray()
                .ForEach(s => serviceCollection.Remove(s));
        }
    }
}
