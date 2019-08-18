namespace MyTested.AspNetCore.Mvc.Test.Setups.Common
{
    using Microsoft.EntityFrameworkCore;

    public class AnotherDbContext : DbContext
    {
        public AnotherDbContext(DbContextOptions<AnotherDbContext> options)
            : base(options)
        {
        }

        public DbSet<AnotherModel> OtherModels { get; set; }
    }
}
