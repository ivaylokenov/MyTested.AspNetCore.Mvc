﻿namespace MyTested.AspNetCore.Mvc.Test.Setups.Common
{
    using Microsoft.EntityFrameworkCore;

    public class CustomDbContext : DbContext, ICustomDbContext
    {
        public CustomDbContext(DbContextOptions<CustomDbContext> options)
            : base(options)
        {
        }

        public DbSet<CustomModel> Models { get; set; }
    }
}
