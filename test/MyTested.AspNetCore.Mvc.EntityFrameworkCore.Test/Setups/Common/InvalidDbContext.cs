namespace MyTested.AspNetCore.Mvc.Test.Setups.Common
{
    using Microsoft.EntityFrameworkCore;

    public class InvalidDbContext : IInvalidDbContext
    {
        public DbSet<CustomModel> Models { get; set; }
    }
}
