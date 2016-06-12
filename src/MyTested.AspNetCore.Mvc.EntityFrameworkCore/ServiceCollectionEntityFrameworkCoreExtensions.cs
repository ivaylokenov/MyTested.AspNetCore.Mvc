namespace MyTested.AspNetCore.Mvc
{
    using System;
    using System.Linq;
    using System.Reflection;
    using Internal;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Infrastructure;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Contains Entity Framework Core extension methods for <see cref="IServiceCollection"/>.
    /// </summary>
    public static class ServiceCollectionEntityFrameworkCoreExtensions
    {
        private static readonly Type BaseDbContextType = typeof(DbContext);
        private static readonly Type BaseDbContextOptionsType = typeof(DbContextOptions);

        private static readonly MethodInfo ReplaceDatabaseMethodInfo =
            typeof(ServiceCollectionEntityFrameworkCoreExtensions)
                .GetTypeInfo()
                .GetDeclaredMethod(nameof(ReplaceDatabase));

        /// <summary>
        /// Replaces the registered <see cref="DbContext"/> with an in memory scoped implementation.
        /// </summary>
        /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/> type.</param>
        public static void ReplaceDbContext(this IServiceCollection serviceCollection)
        {
            var existingDbContextOptionsService =
                serviceCollection.FirstOrDefault(s => BaseDbContextOptionsType.IsAssignableFrom(s.ServiceType));

            if (existingDbContextOptionsService != null)
            {
                serviceCollection.Remove(existingDbContextOptionsService.ServiceType);
            }

            var existingDbContext =
                serviceCollection.FirstOrDefault(s => BaseDbContextType.IsAssignableFrom(s.ImplementationType));

            if (existingDbContext != null)
            {
                if (existingDbContext.Lifetime != ServiceLifetime.Scoped)
                {
                    serviceCollection.Replace(
                        existingDbContext.ServiceType,
                        existingDbContext.ImplementationFactory,
                        ServiceLifetime.Scoped);
                }

                var genericMethod = ReplaceDatabaseMethodInfo.MakeGenericMethod(existingDbContext.ImplementationType);
                genericMethod.Invoke(null, new object[] { serviceCollection });
            }
        }

        private static void ReplaceDatabase<TDbContext>(IServiceCollection serviceCollection)
            where TDbContext : DbContext
        {
            serviceCollection.AddDbContext<TDbContext>(opts =>
            {
                opts.UseInMemoryDatabase();

                ((IDbContextOptionsBuilderInfrastructure)opts).AddOrUpdateExtension(new ScopedInMemoryOptionsExtension());
            });
        }
    }
}
