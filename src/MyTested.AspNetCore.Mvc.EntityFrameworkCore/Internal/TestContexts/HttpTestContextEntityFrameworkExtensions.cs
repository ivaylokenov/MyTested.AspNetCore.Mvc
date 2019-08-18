namespace MyTested.AspNetCore.Mvc.Internal.TestContexts
{
    using System;
    using System.Linq;
    using Microsoft.Extensions.DependencyInjection;
    using Utilities;
    using Utilities.Validators;

    public static class HttpTestContextEntityFrameworkExtensions
    {
        public static TDbContext GetDbContext<TDbContext>(this HttpTestContext httpTestContext)
            where TDbContext : class
        {
            var dbContextServices = httpTestContext
                .HttpContext
                .RequestServices
                .GetServices<TDbContext>()
                .ToArray();

            if (!dbContextServices.Any())
            {
                throw new InvalidOperationException($"{typeof(TDbContext).ToFriendlyTypeName()} is not registered in the test service provider.");
            }

            if (dbContextServices.Length > 1)
            {
                throw new InvalidOperationException($"Multiple services of type {typeof(TDbContext).ToFriendlyTypeName()} are registered in the test service provider. You should specify the DbContext class explicitly by calling '.WithData(data => data.WithEntities<TDbContext>(dbContextSetupAction)'.");
            }

            ServiceValidator.ValidateScopedServiceLifetime<TDbContext>(nameof(GetDbContext));

            return dbContextServices.First();
        }
    }
}
