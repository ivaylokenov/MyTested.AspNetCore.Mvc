namespace MyTested.AspNetCore.Mvc.Internal.EntityFrameworkCore
{
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore.Migrations;

    public class MigratorMock : IMigrator
    {
        public string GenerateScript(
            string fromMigration = null, 
            string toMigration = null, 
            MigrationsSqlGenerationOptions options = 
            MigrationsSqlGenerationOptions.Default)
            => string.Empty;

        public void Migrate(string targetMigration = null)
        {
            // intentionally left empty
        }

        public Task MigrateAsync(
            string targetMigration = null,
            CancellationToken cancellationToken = default(CancellationToken))
            => Task.CompletedTask;
    }
}
