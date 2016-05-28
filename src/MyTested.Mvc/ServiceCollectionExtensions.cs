namespace MyTested.Mvc
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Internal;
    using Internal.Application;
    using Internal.Caching;
    using Internal.Http;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc.Infrastructure;
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
            serviceCollection.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        /// <summary>
        /// Adds <see cref="IActionContextAccessor"/> with singleton scope to the service collection.
        /// </summary>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        public static void AddActionContextAccessor(this IServiceCollection serviceCollection)
        {
            CommonValidator.CheckForNullReference(serviceCollection, nameof(serviceCollection));
            serviceCollection.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
        }
        
        /// <summary>
        /// Replaces the default <see cref="ITempDataProvider"/> with a mocked implementation.
        /// </summary>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        public static void ReplaceTempDataProvider(this IServiceCollection serviceCollection)
        {
            serviceCollection.ReplaceSingleton<ITempDataProvider, MockedTempDataProvider>();
        }

        /// <summary>
        /// Replaces the default <see cref="IMemoryCache"/> with a mocked implementation.
        /// </summary>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        public static void ReplaceMemoryCache(this IServiceCollection serviceCollection)
        {
            serviceCollection.Replace<IMemoryCache, MockedMemoryCache>(ServiceLifetime.Transient);
        }

        /// <summary>
        /// Replaces the default <see cref="ISessionStore"/> with a mocked implementation.
        /// </summary>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        public static void ReplaceSession(this IServiceCollection serviceCollection)
        {
            serviceCollection.ReplaceTransient<ISessionStore, MockedSessionStore>();
        }

        /// <summary>
        /// Tries to remove а service from the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        /// <param name="service">Type of the service which will be removed.</param>
        public static void Remove(this IServiceCollection serviceCollection, Type service)
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
        public static void Remove(this IServiceCollection serviceCollection, Type service, Type implementationType)
        {
            CommonValidator.CheckForNullReference(service, nameof(service));
            RemoveServices(serviceCollection, s => s.ServiceType == service && s.ImplementationType == implementationType);
        }

        /// <summary>
        /// Tries to remove a service from the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <typeparam name="TServive">Type of the service which will be removed.</typeparam>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        public static void Remove<TServive>(this IServiceCollection serviceCollection)
            where TServive : class
        {
            serviceCollection.Remove(typeof(TServive));
        }

        /// <summary>
        /// Tries to remove a service from the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <typeparam name="TServive">Type of the service which will be removed.</typeparam>
        /// <typeparam name="TImplementation">Service implementation type which will be removed.</typeparam>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        public static void Remove<TServive, TImplementation>(this IServiceCollection serviceCollection)
            where TServive : class
            where TImplementation : class, TServive
        {
            serviceCollection.Remove(typeof(TServive), typeof(TImplementation));
        }

        /// <summary>
        /// Tries to remove a transient service from the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        /// <param name="service">Type of the service which will be removed.</param>
        public static void RemoveTransient(this IServiceCollection serviceCollection, Type service)
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
        public static void RemoveTransient(this IServiceCollection serviceCollection, Type service, Type implementationType)
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
        public static void RemoveTransient<TServive>(this IServiceCollection serviceCollection)
            where TServive : class
        {
            serviceCollection.RemoveTransient(typeof(TServive));
        }

        /// <summary>
        /// Tries to remove a transient service from the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <typeparam name="TServive">Type of the service which will be removed.</typeparam>
        /// <typeparam name="TImplementation">Service implementation type which will be removed.</typeparam>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        public static void RemoveTransient<TServive, TImplementation>(this IServiceCollection serviceCollection)
            where TServive : class
            where TImplementation : class, TServive
        {
            serviceCollection.RemoveTransient(typeof(TServive), typeof(TImplementation));
        }

        /// <summary>
        /// Tries to remove a singleton service from the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        /// <param name="service">Type of the service which will be removed.</param>
        public static void RemoveSingleton(this IServiceCollection serviceCollection, Type service)
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
        public static void RemoveSingleton(this IServiceCollection serviceCollection, Type service, Type implementationType)
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
        public static void RemoveSingleton<TServive>(this IServiceCollection serviceCollection)
            where TServive : class
        {
            serviceCollection.RemoveSingleton(typeof(TServive));
        }

        /// <summary>
        /// Tries to remove a singleton service from the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <typeparam name="TServive">Type of the service which will be removed.</typeparam>
        /// <typeparam name="TImplementation">Service implementation type which will be removed.</typeparam>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        public static void RemoveSingleton<TServive, TImplementation>(this IServiceCollection serviceCollection)
            where TServive : class
            where TImplementation : class, TServive
        {
            serviceCollection.RemoveSingleton(typeof(TServive), typeof(TImplementation));
        }

        /// <summary>
        /// Tries to remove a scoped service from the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        /// <param name="service">Type of the service which will be removed.</param>
        public static void RemoveScoped(this IServiceCollection serviceCollection, Type service)
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
        public static void RemoveScoped(this IServiceCollection serviceCollection, Type service, Type implementationType)
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
        public static void RemoveScoped<TServive>(this IServiceCollection serviceCollection)
            where TServive : class
        {
            serviceCollection.RemoveScoped(typeof(TServive));
        }

        /// <summary>
        /// Tries to remove a scoped service from the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <typeparam name="TServive">Type of the service which will be removed.</typeparam>
        /// <typeparam name="TImplementation">Service implementation type which will be removed.</typeparam>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        public static void RemoveScoped<TServive, TImplementation>(this IServiceCollection serviceCollection)
            where TServive : class
            where TImplementation : class, TServive
        {
            serviceCollection.RemoveScoped(typeof(TServive), typeof(TImplementation));
        }

        /// <summary>
        /// Tries to replace a service in the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        /// <param name="service">Type of the service which will be replaced.</param>
        /// <param name="implementationType">Service implementation type which will be used for replacement.</param>
        /// <param name="lifetime">The <see cref="ServiceLifetime"/> which will be applied on the replaced service.</param>
        public static void Replace(this IServiceCollection serviceCollection, Type service, Type implementationType, ServiceLifetime lifetime)
        {
            serviceCollection.Remove(service);
            serviceCollection.Add(ServiceDescriptor.Describe(service, implementationType, lifetime));
            TestServiceProvider.SaveServiceLifetime(service, lifetime);
        }

        /// <summary>
        /// Tries to replace a service in the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        /// <param name="service">Type of the service which will be replaced.</param>
        /// <param name="implementationFactory">Service implementation factory which will be used for replacement.</param>
        /// <param name="lifetime">The <see cref="ServiceLifetime"/> which will be applied on the replaced service.</param>
        public static void Replace(this IServiceCollection serviceCollection, Type service, Func<IServiceProvider, object> implementationFactory, ServiceLifetime lifetime)
        {
            serviceCollection.Remove(service);
            serviceCollection.Add(ServiceDescriptor.Describe(service, implementationFactory, lifetime));
            TestServiceProvider.SaveServiceLifetime(service, lifetime);
        }

        /// <summary>
        /// Tries to replace a service in the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <typeparam name="TService">Type of the service which will be replaced.</typeparam>
        /// <typeparam name="TImplementation">Service implementation type which will be used for replacement.</typeparam>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        /// <param name="lifetime">The <see cref="ServiceLifetime"/> which will be applied on the replaced service.</param>
        public static void Replace<TService, TImplementation>(this IServiceCollection serviceCollection, ServiceLifetime lifetime)
            where TService : class
            where TImplementation : class, TService
        {
            serviceCollection.Replace(typeof(TService), typeof(TImplementation), lifetime);
        }

        /// <summary>
        /// Tries to replace a transient service in the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        /// <param name="service">Type of the service which will be replaced.</param>
        /// <param name="implementationType">Service implementation type which will be used for replacement.</param>
        public static void ReplaceTransient(this IServiceCollection serviceCollection, Type service, Type implementationType)
        {
            serviceCollection.RemoveTransient(service);
            serviceCollection.AddTransient(service, implementationType);
        }

        /// <summary>
        /// Tries to replace a transient service in the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        /// <param name="service">Type of the service which will be replaced.</param>
        /// <param name="implementationFactory">Service implementation factory which will be used for replacement.</param>
        public static void ReplaceTransient(this IServiceCollection serviceCollection, Type service, Func<IServiceProvider, object> implementationFactory)
        {
            serviceCollection.RemoveTransient(service);
            serviceCollection.AddTransient(service, implementationFactory);
        }

        /// <summary>
        /// Tries to replace a transient service in the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <typeparam name="TService">Type of the service which will be replaced.</typeparam>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        /// <param name="implementationFactory">Service implementation factory which will be used for replacement.</param>
        public static void ReplaceTransient<TService>(this IServiceCollection serviceCollection, Func<IServiceProvider, object> implementationFactory)
            where TService : class
        {
            serviceCollection.ReplaceTransient(typeof(TService), implementationFactory);
        }

        /// <summary>
        /// Tries to replace a transient service in the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <typeparam name="TService">Type of the service which will be replaced.</typeparam>
        /// <typeparam name="TImplementation">Service implementation type which will be used for replacement.</typeparam>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        public static void ReplaceTransient<TService, TImplementation>(this IServiceCollection serviceCollection)
            where TService : class
            where TImplementation : class, TService
        {
            serviceCollection.ReplaceTransient(typeof(TService), typeof(TImplementation));
        }

        /// <summary>
        /// Tries to replace a singleton service in the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        /// <param name="service">Type of the service which will be replaced.</param>
        /// <param name="implementationType">Service implementation type which will be used for replacement.</param>
        public static void ReplaceSingleton(this IServiceCollection serviceCollection, Type service, Type implementationType)
        {
            serviceCollection.RemoveSingleton(service);
            serviceCollection.AddSingleton(service, implementationType);
        }

        /// <summary>
        /// Tries to replace a singleton service in the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        /// <param name="service">Type of the service which will be replaced.</param>
        /// <param name="implementationFactory">Service implementation factory which will be used for replacement.</param>
        public static void ReplaceSingleton(this IServiceCollection serviceCollection, Type service, Func<IServiceProvider, object> implementationFactory)
        {
            serviceCollection.RemoveSingleton(service);
            serviceCollection.AddSingleton(service, implementationFactory);
        }

        /// <summary>
        /// Tries to replace a singleton service in the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        /// <param name="service">Type of the service which will be replaced.</param>
        /// <param name="implementationInstance">Service implementation instance which will be used for replacement.</param>
        public static void ReplaceSingleton(this IServiceCollection serviceCollection, Type service, object implementationInstance)
        {
            serviceCollection.RemoveSingleton(service);
            serviceCollection.AddSingleton(service, implementationInstance);
        }

        /// <summary>
        /// Tries to replace a singleton service in the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <typeparam name="TService">Type of the service which will be replaced.</typeparam>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        /// <param name="implementationFactory">Service implementation factory which will be used for replacement.</param>
        public static void ReplaceSingleton<TService>(this IServiceCollection serviceCollection, Func<IServiceProvider, object> implementationFactory)
            where TService : class
        {
            serviceCollection.ReplaceSingleton(typeof(TService), implementationFactory);
        }

        /// <summary>
        /// Tries to replace a singleton service in the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <typeparam name="TService">Type of the service which will be replaced.</typeparam>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        /// <param name="implementationInstance">Service implementation instance which will be used for replacement.</param>
        public static void ReplaceSingleton<TService>(this IServiceCollection serviceCollection, object implementationInstance)
            where TService : class
        {
            serviceCollection.ReplaceSingleton(typeof(TService), implementationInstance);
        }

        /// <summary>
        /// Tries to replace a singleton service in the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <typeparam name="TService">Type of the service which will be replaced.</typeparam>
        /// <typeparam name="TImplementation">Service implementation type which will be used for replacement.</typeparam>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        public static void ReplaceSingleton<TService, TImplementation>(this IServiceCollection serviceCollection)
            where TService : class
            where TImplementation : class, TService
        {
            serviceCollection.ReplaceSingleton(typeof(TService), typeof(TImplementation));
        }

        /// <summary>
        /// Tries to replace a scoped service in the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        /// <param name="service">Type of the service which will be replaced.</param>
        /// <param name="implementationType">Service implementation type which will be used for replacement.</param>
        public static void ReplaceScoped(this IServiceCollection serviceCollection, Type service, Type implementationType)
        {
            serviceCollection.RemoveScoped(service);
            serviceCollection.AddScoped(service, implementationType);
        }

        /// <summary>
        /// Tries to replace a scoped service in the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        /// <param name="service">Type of the service which will be replaced.</param>
        /// <param name="implementationFactory">Service implementation factory which will be used for replacement.</param>
        public static void ReplaceScoped(this IServiceCollection serviceCollection, Type service, Func<IServiceProvider, object> implementationFactory)
        {
            serviceCollection.RemoveScoped(service);
            serviceCollection.AddScoped(service, implementationFactory);
        }

        /// <summary>
        /// Tries to replace a scoped service in the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <typeparam name="TService">Type of the service which will be replaced.</typeparam>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        /// <param name="implementationFactory">Service implementation factory which will be used for replacement.</param>
        public static void ReplaceScoped<TService>(this IServiceCollection serviceCollection, Func<IServiceProvider, object> implementationFactory)
            where TService : class
        {
            serviceCollection.ReplaceScoped(typeof(TService), implementationFactory);
        }

        /// <summary>
        /// Tries to replace a scoped service in the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <typeparam name="TService">Type of the service which will be replaced.</typeparam>
        /// <typeparam name="TImplementation">Service implementation type which will be used for replacement.</typeparam>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        public static void ReplaceScoped<TService, TImplementation>(this IServiceCollection serviceCollection)
            where TService : class
            where TImplementation : class, TService
        {
            serviceCollection.ReplaceScoped(typeof(TService), typeof(TImplementation));
        }

        /// <summary>
        /// Tries to replace multiple services in the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        /// <param name="descriptor"><see cref="ServiceDescriptor"/> providing the services.</param>
        public static void ReplaceEnumerable(this IServiceCollection serviceCollection, ServiceDescriptor descriptor)
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
        public static void ReplaceEnumerable(this IServiceCollection serviceCollection, IEnumerable<ServiceDescriptor> descriptors)
        {
            CommonValidator.CheckForNullReference(descriptors, nameof(descriptors));
            descriptors.ForEach(serviceCollection.ReplaceEnumerable);
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
