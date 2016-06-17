namespace MyTested.AspNetCore.Mvc.Internal.TestContexts
{
    using Microsoft.Extensions.DependencyInjection;
    using TestContexts;
    using Utilities.Validators;

    public static class HttpTestContextEntityFrameworkExtensions
    {
        public static TDbContext GetDbContext<TDbContext>(this HttpTestContext httpTestContext)
            where TDbContext : class
        {
            ServiceValidator.ValidateScopedServiceLifetime<TDbContext>(nameof(GetDbContext));

            return httpTestContext
                .HttpContext
                .RequestServices
                .GetRequiredService<TDbContext>();
        }
    }
}
