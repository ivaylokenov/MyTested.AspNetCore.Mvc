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
        public IAndWithoutDbContextBuilder WithoutEntityByKey<TEntity>(object key)
            where TEntity : class
            => this.WithoutEntityByKey<DbContext, TEntity>(key);

        /// <inheritdoc />
        public IAndWithoutDbContextBuilder WithoutEntityByKey<TDbContext, TEntity>(object key)
            where TDbContext : class
        {
            var dbContext = this.TestContext.GetBaseDbContext<TDbContext>();

            var entity = dbContext.Find(typeof(TEntity), key);
            if (entity == null)
            {
                return this;
            }

            dbContext.Remove(entity);
            dbContext.SaveChanges();

            return this;
        }

        /// <inheritdoc />
        public IAndWithoutDbContextBuilder WithoutEntitiesByKeys<TEntity>(IEnumerable<object> keys)
            where TEntity : class
            => this.WithoutEntitiesByKeys<DbContext, TEntity>(keys);

        /// <inheritdoc />
        public IAndWithoutDbContextBuilder WithoutEntitiesByKeys<TDbContext, TEntity>(IEnumerable<object> keys)
            where TDbContext : class
        {
            var dbContext = this.TestContext.GetBaseDbContext<TDbContext>();

            var entities = keys
                .Select(key => dbContext.Find(typeof(TEntity), key))
                .Where(entity => entity != null);

            if (entities.Any() == false)
            {
                return this;
            }

            dbContext.RemoveRange(entities);
            dbContext.SaveChanges();

            return this;
        }

        /// <inheritdoc />
        public IAndWithoutDbContextBuilder WithoutEntity(object entity)
            => this.WithoutEntity<DbContext>(entity);

        /// <inheritdoc />
        public IAndWithoutDbContextBuilder WithoutEntity<TDbContext>(object entity)
            where TDbContext : class
            => this.WithoutEntities<TDbContext>(entity);

        /// <inheritdoc />
        public IAndWithoutDbContextBuilder WithoutEntities(IEnumerable<object> entities)
            => this.WithoutEntities(dbContext => dbContext.RemoveRange(entities));

        /// <inheritdoc />
        public IAndWithoutDbContextBuilder WithoutEntities<TDbContext>(IEnumerable<object> entities)
            where TDbContext : class
            => this.WithoutEntities<TDbContext>(dbContext => (dbContext as DbContext).RemoveRange(entities));

        /// <inheritdoc />
        public IAndWithoutDbContextBuilder WithoutEntities(params object[] entities)
            => this.WithoutEntities(entities.AsEnumerable());

        /// <inheritdoc />
        public IAndWithoutDbContextBuilder WithoutEntities<TDbContext>(params object[] entities)
            where TDbContext : class
            => this.WithoutEntities<TDbContext>(entities.AsEnumerable());

        /// <inheritdoc />
        public IAndWithoutDbContextBuilder WithoutEntities(Action<DbContext> dbContextSetup)
            => this.WithoutEntities<DbContext>(dbContextSetup);

        /// <inheritdoc />
        public IAndWithoutDbContextBuilder WithoutAllEntities()
            => this.WithoutEntities<DbContext>(dbContext => dbContext.Database.EnsureDeleted());

        /// <inheritdoc />
        public IAndWithoutDbContextBuilder WithoutEntities<TDbContext>(Action<TDbContext> dbContextSetup) where TDbContext : class
        {
            CommonValidator.CheckForNullReference(dbContextSetup, nameof(dbContextSetup));

            var dbContext = this.TestContext.GetDbContext<TDbContext>();

            dbContextSetup(dbContext);

            try
            {
                (dbContext as DbContext).SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Intentional silent fail, when deleting entities that do not exist in the database or have been already deleted.
            }

            return this;
        }

        /// <inheritdoc />
        public IAndWithoutDbContextBuilder WithoutSet<TEntity>(Action<DbSet<TEntity>> entitySetup)
            where TEntity : class
            => this.WithoutSet<DbContext, TEntity>(entitySetup);

        /// <inheritdoc />
        public IAndWithoutDbContextBuilder WithoutSet<TDbContext, TEntity>(Action<DbSet<TEntity>> entitySetup)
            where TDbContext : class
            where TEntity : class
        {
            CommonValidator.CheckForNullReference(entitySetup, nameof(entitySetup));

            var dbContext = this.TestContext.GetBaseDbContext<TDbContext>();

            entitySetup(dbContext.Set<TEntity>());

            try
            {
                dbContext.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Intentional silent fail, when deleting entities that do not exist in the database or have been already deleted.
            }

            return this;
        }

        /// <inheritdoc />
        public IWithoutDbContextBuilder AndAlso() => this;
    }
}
