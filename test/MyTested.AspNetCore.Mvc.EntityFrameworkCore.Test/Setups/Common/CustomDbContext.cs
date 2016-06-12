namespace MyTested.AspNetCore.Mvc.EntityFrameworkCore.Test.Setups.Common
{
    using Microsoft.EntityFrameworkCore;

    public class CustomDbContext : DbContext
    {
        public CustomDbContext(DbContextOptions<CustomDbContext> options)
            : base(options)
        {
        }

        public DbSet<CustomModel> Models { get; set; }
    }
}
