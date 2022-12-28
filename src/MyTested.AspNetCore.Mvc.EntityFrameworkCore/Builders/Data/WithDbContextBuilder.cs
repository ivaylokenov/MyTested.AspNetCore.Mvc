namespace MyTested.AspNetCore.Mvc.Builders.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Base;
    using Contracts.Data;
    using Internal.TestContexts;
    using Microsoft.EntityFrameworkCore;
    using Utilities.Validators;

    /// <summary>
    /// Used for building <see cref="DbContext"/>.
    /// </summary>
    public class WithDbContextBuilder : BaseTestBuilder, IAndWithDbContextBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WithDbContextBuilder"/> class.
        /// </summary>
        /// <param name="testContext"><see cref="HttpTestContext"/> containing data about the currently executed assertion chain.</param>
        public WithDbContextBuilder(HttpTestContext testContext)
            : base(testContext)
        {
        }

        /// <inheritdoc />
        public IAndWithDbContextBuilder WithEntities(IEnumerable<object> entities)
            => this.WithEntities(dbContext => dbContext.AddRange(entities));

        /// <inheritdoc />
        public IAndWithDbContextBuilder WithEntities<TDbContext>(IEnumerable<object> entities)
            where TDbContext : class
            => this.WithEntities<TDbContext>(dbContext => (dbContext as DbContext).AddRange(entities));

        /// <inheritdoc />
        public IAndWithDbContextBuilder WithEntities(params object[] entities)
            => this.WithEntities(entities.AsEnumerable());

        /// <inheritdoc />
        public IAndWithDbContextBuilder WithEntities<TDbContext>(params object[] entities)
            where TDbContext : class
            => this.WithEntities<TDbContext>(entities.AsEnumerable());

        /// <inheritdoc />
        public IAndWithDbContextBuilder WithEntities(Action<DbContext> dbContextSetup)
            => this.WithEntities<DbContext>(dbContextSetup);

        /// <inheritdoc />
        public IAndWithDbContextBuilder WithEntities<TDbContext>(Action<TDbContext> dbContextSetup)
            where TDbContext : class
        {
            CommonValidator.CheckForNullReference(dbContextSetup, nameof(dbContextSetup));

            var dbContext = this.TestContext.GetDbContext<TDbContext>();
            dbContextSetup(dbContext);

            this.SaveChangesAndCleanUp(dbContext as DbContext);

            return this;
        }

        /// <inheritdoc />
        public IAndWithDbContextBuilder WithSet<TEntity>(Action<DbSet<TEntity>> entitySetup)
            where TEntity : class
            => this.WithSet<DbContext, TEntity>(entitySetup);

        /// <inheritdoc />
        public IAndWithDbContextBuilder WithSet<TDbContext, TEntity>(Action<DbSet<TEntity>> entitySetup)
            where TDbContext : class
            where TEntity : class
        {
            CommonValidator.CheckForNullReference(entitySetup, nameof(entitySetup));

            var dbContext = this.TestContext.GetBaseDbContext<TDbContext>();
            entitySetup(dbContext.Set<TEntity>());

            this.SaveChangesAndCleanUp(dbContext);

            return this;
        }

        /// <inheritdoc />
        public IWithDbContextBuilder AndAlso() => this;

        private void SaveChangesAndCleanUp(DbContext dbContext)
        {
            dbContext.SaveChanges();

            // Clear change tracker entries to clean up for the test execution.
            foreach (var entry in dbContext.ChangeTracker.Entries())
            {
                entry.State = EntityState.Detached;
            }
        }
    }
}
