namespace MyTested.AspNetCore.Mvc
{
    using System;
    using System.Linq;
    using System.Reflection;
    using Internal.EntityFrameworkCore;
    using Internal.Services;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Infrastructure;
    using Microsoft.Extensions.DependencyInjection;
    using Utilities.Extensions;

    /// <summary>
    /// Contains Entity Framework Core extension methods for <see cref="IServiceCollection"/>.
    /// </summary>
    public static class ServiceCollectionEntityFrameworkCoreExtensions
    {
        private static readonly Type BaseDbContextType = typeof(DbContext);
        private static readonly Type BaseDbContextOptionsType = typeof(DbContextOptions);

        private static readonly MethodInfo AddScopedDatabaseMethodInfo =
            typeof(ServiceCollectionEntityFrameworkCoreExtensions)
                .GetTypeInfo()
                .GetDeclaredMethod(nameof(AddScopedDatabase));

        /// <summary>
        /// Replaces the registered <see cref="DbContext"/> with an in memory scoped implementation.
        /// </summary>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        /// <returns>The same <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection ReplaceDbContext(this IServiceCollection serviceCollection)
        {
            // Remove all DbContextOptions services.
            serviceCollection
                .Where(s => BaseDbContextOptionsType.IsAssignableFrom(s.ServiceType))
                .ToArray()
                .ForEach(existingDbContextOptionsService => serviceCollection
                    .Remove(existingDbContextOptionsService.ServiceType));

            // Remove all base DbContext services.
            serviceCollection
                .Where(s => BaseDbContextType == s.ServiceType)
                .ToArray()
                .ForEach(existingDbContextService => serviceCollection
                    .Remove(existingDbContextService));

            // Replace the database services with in memory scoped ones.
            serviceCollection
                .Where(s => BaseDbContextType.IsAssignableFrom(s.ImplementationType))
                .ToArray()
                .ForEach(existingDbContextService =>
                {
                    if (existingDbContextService.Lifetime != ServiceLifetime.Scoped)
                    {
                        serviceCollection.Replace(
                            existingDbContextService.ServiceType,
                            existingDbContextService.ImplementationFactory,
                            ServiceLifetime.Scoped);
                    }

                    var genericMethod = AddScopedDatabaseMethodInfo.MakeGenericMethod(existingDbContextService.ImplementationType);
                    genericMethod.Invoke(null, new object[] { serviceCollection });
                });
            
            TestServiceProvider.SaveServiceLifetime(BaseDbContextType, ServiceLifetime.Scoped);

            return serviceCollection;
        }

        private static void AddScopedDatabase<TDbContext>(IServiceCollection serviceCollection)
            where TDbContext : DbContext
        {
            serviceCollection.AddScoped<DbContext>(s => s.GetRequiredService<TDbContext>());
            serviceCollection.AddDbContext<TDbContext>(opts =>
            {
                opts.UseInMemoryDatabase(Guid.NewGuid().ToString());

                ((IDbContextOptionsBuilderInfrastructure)opts).AddOrUpdateExtension(new ScopedInMemoryOptionsExtension());
            });
        }
    }
}
