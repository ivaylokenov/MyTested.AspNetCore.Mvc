namespace MyTested.AspNetCore.Mvc.Internal.EntityFrameworkCore
{
    using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
    using Microsoft.EntityFrameworkCore.Migrations;
    using Microsoft.EntityFrameworkCore.Storage.Internal;
    using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;
    using Microsoft.Extensions.DependencyInjection;

    public class ScopedInMemoryOptionsExtension : InMemoryOptionsExtension
    {
        public override void ApplyServices(IServiceCollection services)
            => services
                .AddScoped<IMigrator, MigratorMock>()
                .ReplaceLifetime<IInMemoryStoreSource>(ServiceLifetime.Scoped)
                .ReplaceLifetime<InMemoryValueGeneratorCache>(ServiceLifetime.Scoped);
    }
}
