namespace MyTested.AspNetCore.Mvc.Internal.TestContexts
{
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;
    using Utilities.Validators;

    public static class HttpTestContextOptionsExtensions
    {
        public static TOptions GetOptions<TOptions>(this HttpTestContext httpTestContext)
            where TOptions : class, new()
        {
            ServiceValidator.ValidateScopedServiceLifetime(typeof(IOptions<>), nameof(GetOptions));

            return httpTestContext
                .HttpContext
                .RequestServices
                .GetRequiredService<IOptions<TOptions>>()
                .Value;
        }
    }
}
