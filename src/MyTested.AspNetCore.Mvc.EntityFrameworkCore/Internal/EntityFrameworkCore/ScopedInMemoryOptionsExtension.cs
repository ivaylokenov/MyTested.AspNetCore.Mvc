namespace MyTested.AspNetCore.Mvc.Internal.EntityFrameworkCore
{
    using Microsoft.EntityFrameworkCore.Migrations;
    using Microsoft.EntityFrameworkCore.InMemory.Infrastructure.Internal;
    using Microsoft.EntityFrameworkCore.InMemory.Storage.Internal;
    using Microsoft.Extensions.DependencyInjection;

    public class ScopedInMemoryOptionsExtension : InMemoryOptionsExtension
    {
        public override bool ApplyServices(IServiceCollection services)
        {
            services
                .AddScoped<IMigrator, MigratorMock>()
                .ReplaceLifetime<IInMemorySingletonOptions>(ServiceLifetime.Scoped)
                .ReplaceLifetime<IInMemoryStoreCache>(ServiceLifetime.Scoped)
                .ReplaceLifetime<IInMemoryTableFactory>(ServiceLifetime.Scoped);

            return true;
        }
    }
}
