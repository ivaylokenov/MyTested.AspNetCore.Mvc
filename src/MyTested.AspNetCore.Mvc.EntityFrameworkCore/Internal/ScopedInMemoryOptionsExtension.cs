namespace MyTested.AspNetCore.Mvc.Internal
{
    using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
    using Microsoft.EntityFrameworkCore.Storage.Internal;
    using Microsoft.Extensions.DependencyInjection;

    public class ScopedInMemoryOptionsExtension : InMemoryOptionsExtension
    {
        public override void ApplyServices(IServiceCollection services)
        {
            services.Replace<IInMemoryStore, InMemoryStore>(ServiceLifetime.Scoped);
        }
    }
}
