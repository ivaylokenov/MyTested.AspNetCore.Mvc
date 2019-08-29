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
    public class DbContextBuilder : BaseTestBuilder, IAndDbContextBuilder
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
        public IAndDbContextBuilder WithEntities(IEnumerable<object> entities)
            => this.WithEntities(dbContext => dbContext.AddRange(entities));

        /// <inheritdoc />
        public IAndDbContextBuilder WithEntities<TDbContext>(IEnumerable<object> entities)
            where TDbContext : DbContext
            => this.WithEntities<TDbContext>(dbContext => dbContext.AddRange(entities));

        /// <inheritdoc />
        public IAndDbContextBuilder WithEntities(params object[] entities)
            => this.WithEntities(entities.AsEnumerable());

        /// <inheritdoc />
        public IAndDbContextBuilder WithEntities<TDbContext>(params object[] entities)
            where TDbContext : DbContext
            => this.WithEntities<TDbContext>(entities.AsEnumerable());

        /// <inheritdoc />
        public IAndDbContextBuilder WithEntities(Action<DbContext> dbContextSetup)
            => this.WithEntities<DbContext>(dbContextSetup);

        /// <inheritdoc />
        public IAndDbContextBuilder WithEntities<TDbContext>(Action<TDbContext> dbContextSetup)
            where TDbContext : DbContext
        {
            CommonValidator.CheckForNullReference(dbContextSetup, nameof(dbContextSetup));

            var dbContext = this.TestContext.GetDbContext<TDbContext>();
            dbContextSetup(dbContext);
            dbContext.SaveChanges();

            return this;
        }

        /// <inheritdoc />
        public IAndDbContextBuilder WithSet<TEntity>(Action<DbSet<TEntity>> entitySetup)
            where TEntity : class
            => this.WithSet<DbContext, TEntity>(entitySetup);

        /// <inheritdoc />
        public IAndDbContextBuilder WithSet<TDbContext, TEntity>(Action<DbSet<TEntity>> entitySetup)
            where TDbContext : DbContext
            where TEntity : class
        {
            CommonValidator.CheckForNullReference(entitySetup, nameof(entitySetup));

            var dbContext = this.TestContext.GetDbContext<TDbContext>();
            entitySetup(dbContext.Set<TEntity>());
            dbContext.SaveChanges();

            return this;
        }

        /// <inheritdoc />
        public IDbContextBuilder AndAlso() => this;

        /// <inheritdoc />
        public IAndDbContextBuilder WithoutEntities(IEnumerable<object> entities)
            => this.WithoutEntities(dbContext => dbContext.RemoveRange(entities));
        
        /// <inheritdoc />
        public IAndDbContextBuilder WithoutEntities<TDbContext>(IEnumerable<object> entities)
            where TDbContext : DbContext
            => this.WithoutEntities<TDbContext>(dbContext => dbContext.RemoveRange(entities));

        /// <inheritdoc />
        public IAndDbContextBuilder WithoutEntities(params object[] entities)
            => this.WithoutEntities(entities.AsEnumerable());

        /// <inheritdoc />
        public IAndDbContextBuilder WithoutEntities<TDbContext>(params object[] entities)
            where TDbContext : DbContext
            => this.WithoutEntities<TDbContext>(entities.AsEnumerable());

        /// <inheritdoc />
        public IAndDbContextBuilder WithoutEntities(Action<DbContext> dbContextSetup)
            => this.WithoutEntities<DbContext>(dbContextSetup);

        /// <inheritdoc />
        public IAndDbContextBuilder WipeDatabase()
            => this.WithEntities<DbContext>(dbContext => dbContext.Database.EnsureDeleted());

        /// <inheritdoc />
        public IAndDbContextBuilder WithoutEntities<TDbContext>(Action<TDbContext> dbContextSetup) where TDbContext : DbContext
        {
            CommonValidator.CheckForNullReference(dbContextSetup, nameof(dbContextSetup));

            var dbContext = this.TestContext.GetDbContext<TDbContext>();
            dbContextSetup(dbContext);
            dbContext.SaveChanges();

            return this;
        }

        /// <inheritdoc />
        public IAndDbContextBuilder WithoutSet<TEntity>(Action<DbSet<TEntity>> entitySetup)
            where TEntity : class
            => this.WithoutSet<DbContext, TEntity>(entitySetup);

        /// <inheritdoc />
        public IAndDbContextBuilder WithoutSet<TDbContext, TEntity>(Action<DbSet<TEntity>> entitySetup)
            where TDbContext : DbContext
            where TEntity : class
        {
            CommonValidator.CheckForNullReference(entitySetup, nameof(entitySetup));

            var dbContext = this.TestContext.GetDbContext<TDbContext>();
            entitySetup(dbContext.Set<TEntity>());
            dbContext.SaveChanges();

            return this;
        }
    }
}
