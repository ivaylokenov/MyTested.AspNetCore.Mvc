namespace MyTested.AspNetCore.Mvc.Builders.Data
{
    using System;
    using Base;
    using Contracts.Data;
    using Internal.TestContexts;
    using Microsoft.EntityFrameworkCore;
    using Utilities.Validators;

    /// <summary>
    /// Used for building <see cref="DbContext"/>.
    /// </summary>
    public class DbContextBuilder : BaseTestBuilder, IDbContextBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DbContextBuilder"/> class.
        /// </summary>
        /// <param name="testContext"><see cref="HttpTestContext"/> containing data about the currently executed assertion chain.</param>
        public DbContextBuilder(HttpTestContext testContext)
            : base(testContext)
        {
        }

        /// <inheritdoc />
        public void WithEntities(Action<DbContext> dbContextSetup)
        {
            WithEntities<DbContext>(dbContextSetup);
        }

        /// <inheritdoc />
        public void WithEntities<TDbContext>(Action<TDbContext> dbContextSetup)
            where TDbContext : DbContext
        {
            CommonValidator.CheckForNullReference(dbContextSetup, nameof(dbContextSetup));

            var dbContext = this.TestContext.GetDbContext<TDbContext>();
            dbContextSetup(dbContext);
            dbContext.SaveChanges();
        }

        /// <inheritdoc />
        public void WithSet<TDbContext, TEntity>(Action<DbSet<TEntity>> entitySetup)
            where TDbContext : DbContext
            where TEntity : class
        {
            CommonValidator.CheckForNullReference(entitySetup, nameof(entitySetup));

            var dbContext = this.TestContext.GetDbContext<TDbContext>();
            entitySetup(dbContext.Set<TEntity>());
            dbContext.SaveChanges();
        }
    }
}
