namespace MyTested.AspNetCore.Mvc.Builders.Data
{
    using System;
    using Base;
    using Contracts.Data;
    using Exceptions;
    using Internal.TestContexts;
    using Microsoft.EntityFrameworkCore;
    using Utilities;
    using Utilities.Validators;

    /// <summary>
    /// Used for testing <see cref="DbContext"/>.
    /// </summary>
    public class DbContextTestBuilder : BaseTestBuilderWithComponent, IAndDbContextTestBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DbContextTestBuilder"/> class.
        /// </summary>
        /// <param name="testContext"><see cref="ComponentTestContext"/> containing data about the currently executed assertion chain.</param>
        public DbContextTestBuilder(ComponentTestContext testContext)
            : base(testContext)
        {
        }

        /// <inheritdoc />
        public IAndDbContextTestBuilder WithEntities(Action<DbContext> assertions)
        {
            return this.WithEntities<DbContext>(assertions);
        }

        /// <inheritdoc />
        public IAndDbContextTestBuilder WithEntities(Func<DbContext, bool> predicate)
        {
            return this.WithEntities<DbContext>(predicate);
        }

        /// <inheritdoc />
        public IAndDbContextTestBuilder WithEntities<TDbContext>(Func<TDbContext, bool> predicate) where TDbContext : DbContext
        {
            CommonValidator.CheckForNullReference(predicate, nameof(predicate));

            if (!predicate(this.TestContext.GetDbContext<TDbContext>()))
            {
                throw new DataProviderAssertionException(string.Format(
                    "{0} the {1} entities to pass the given predicate, but it failed.",
                    this.TestContext.ExceptionMessagePrefix,
                    typeof(TDbContext).ToFriendlyTypeName()));
            }

            return this;
        }

        /// <inheritdoc />
        public IAndDbContextTestBuilder WithEntities<TDbContext>(Action<TDbContext> assertions) where TDbContext : DbContext
        {
            CommonValidator.CheckForNullReference(assertions, nameof(assertions));

            assertions(this.TestContext.GetDbContext<TDbContext>());

            return this;
        }

        /// <inheritdoc />
        public IAndDbContextTestBuilder WithSet<TEntity>(Action<DbSet<TEntity>> assertions)
            where TEntity : class
        {
            return this.WithSet<DbContext, TEntity>(assertions);
        }

        /// <inheritdoc />
        public IAndDbContextTestBuilder WithSet<TEntity>(Func<DbSet<TEntity>, bool> predicate)
            where TEntity : class
        {
            return this.WithSet<DbContext, TEntity>(predicate);
        }

        /// <inheritdoc />
        public IAndDbContextTestBuilder WithSet<TDbContext, TEntity>(Action<DbSet<TEntity>> assertions)
            where TDbContext : DbContext
            where TEntity : class
        {
            CommonValidator.CheckForNullReference(assertions, nameof(assertions));

            assertions(this.TestContext.GetDbContext<TDbContext>().Set<TEntity>());

            return this;
        }

        /// <inheritdoc />
        public IAndDbContextTestBuilder WithSet<TDbContext, TEntity>(Func<DbSet<TEntity>, bool> predicate)
            where TDbContext : DbContext
            where TEntity : class
        {
            CommonValidator.CheckForNullReference(predicate, nameof(predicate));

            if (!predicate(this.TestContext.GetDbContext<TDbContext>().Set<TEntity>()))
            {
                throw new DataProviderAssertionException(string.Format(
                    "{0} the {1} set of {2} to pass the given predicate, but it failed.",
                    this.TestContext.ExceptionMessagePrefix,
                    typeof(TDbContext).ToFriendlyTypeName(),
                    typeof(TEntity).ToFriendlyTypeName()));
            }

            return this;
        }

        /// <inheritdoc />
        public IDbContextTestBuilder AndAlso() => this;
    }
}
