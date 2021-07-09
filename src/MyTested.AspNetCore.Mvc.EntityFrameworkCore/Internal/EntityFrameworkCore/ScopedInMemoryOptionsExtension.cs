namespace MyTested.AspNetCore.Mvc.Internal.EntityFrameworkCore
{
    using Microsoft.EntityFrameworkCore.Migrations;
    using Microsoft.EntityFrameworkCore.InMemory.Infrastructure.Internal;
    using Microsoft.EntityFrameworkCore.InMemory.Storage.Internal;
    using Microsoft.Extensions.DependencyInjection;

#pragma warning disable EF1001 // Internal EF Core API usage.
    public class ScopedInMemoryOptionsExtension : InMemoryOptionsExtension
    {
        public override void ApplyServices(IServiceCollection services)
            => services
                .AddScoped<IMigrator, MigratorMock>()
                .ReplaceLifetime<IInMemorySingletonOptions>(ServiceLifetime.Scoped)
                .ReplaceLifetime<IInMemoryStoreCache>(ServiceLifetime.Scoped)
                .ReplaceLifetime<IInMemoryTableFactory>(ServiceLifetime.Scoped);
    }
#pragma warning restore EF1001 // Internal EF Core API usage.
}
