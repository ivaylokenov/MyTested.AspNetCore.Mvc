namespace MyTested.AspNetCore.Mvc.Builders.Data
{
    using Microsoft.EntityFrameworkCore;
    using MyTested.AspNetCore.Mvc.Builders.Base;
    using MyTested.AspNetCore.Mvc.Builders.Contracts.Data;
    using MyTested.AspNetCore.Mvc.Internal.TestContexts;
    using MyTested.AspNetCore.Mvc.Utilities.Validators;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Used for building <see cref="DbContext"/>.
    /// </summary>
    public class WithoutDbContextBuilder : BaseTestBuilder, IAndWithoutDbContextBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WithoutDbContextBuilder"/> class.
        /// </summary>
        /// <param name="testContext"><see cref="HttpTestContext"/> containing data about the currently executed assertion chain.</param>
        public WithoutDbContextBuilder(HttpTestContext testContext)
            : base(testContext)
        {
        }

        /// <inheritdoc />
        public IAndWithoutDbContextBuilder WithoutEntities(IEnumerable<object> entities)
            => this.WithoutEntities(dbContext => dbContext.RemoveRange(entities));

        /// <inheritdoc />
        public IAndWithoutDbContextBuilder WithoutEntities<TDbContext>(IEnumerable<object> entities)
            where TDbContext : DbContext
            => this.WithoutEntities<TDbContext>(dbContext => dbContext.RemoveRange(entities));

        /// <inheritdoc />
        public IAndWithoutDbContextBuilder WithoutEntities(params object[] entities)
            => this.WithoutEntities(entities.AsEnumerable());

        /// <inheritdoc />
        public IAndWithoutDbContextBuilder WithoutEntities<TDbContext>(params object[] entities)
            where TDbContext : DbContext
            => this.WithoutEntities<TDbContext>(entities.AsEnumerable());

        /// <inheritdoc />
        public IAndWithoutDbContextBuilder WithoutEntities(Action<DbContext> dbContextSetup)
            => this.WithoutEntities<DbContext>(dbContextSetup);

        /// <inheritdoc />
        public IAndWithoutDbContextBuilder WipeDatabase()
            => this.WithoutEntities<DbContext>(dbContext => dbContext.Database.EnsureDeleted());

        /// <inheritdoc />
        public IAndWithoutDbContextBuilder WithoutEntities<TDbContext>(Action<TDbContext> dbContextSetup) where TDbContext : DbContext
        {
            CommonValidator.CheckForNullReference(dbContextSetup, nameof(dbContextSetup));

            var dbContext = this.TestContext.GetDbContext<TDbContext>();
            dbContextSetup(dbContext);

            try
            {
                dbContext.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Intentional silent fail.
            }

            return this;
        }

        /// <inheritdoc />
        public IAndWithoutDbContextBuilder WithoutSet<TEntity>(Action<DbSet<TEntity>> entitySetup)
            where TEntity : class
            => this.WithoutSet<DbContext, TEntity>(entitySetup);

        /// <inheritdoc />
        public IAndWithoutDbContextBuilder WithoutSet<TDbContext, TEntity>(Action<DbSet<TEntity>> entitySetup)
            where TDbContext : DbContext
            where TEntity : class
        {
            CommonValidator.CheckForNullReference(entitySetup, nameof(entitySetup));

            var dbContext = this.TestContext.GetDbContext<TDbContext>();
            entitySetup(dbContext.Set<TEntity>());

            try
            {
                dbContext.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Intentional silent fail.
            }

            return this;
        }

        /// <inheritdoc />
        public IWithoutDbContextBuilder AndAlso() => this;
    }
}
