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
    public class WithDbContextTestBuilder : BaseTestBuilderWithComponent, IAndWithDbContextTestBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WithDbContextTestBuilder"/> class.
        /// </summary>
        /// <param name="testContext"><see cref="ComponentTestContext"/> containing data about the currently executed assertion chain.</param>
        public WithDbContextTestBuilder(ComponentTestContext testContext)
            : base(testContext)
        {
        }

        /// <inheritdoc />
        public IAndWithDbContextTestBuilder WithEntities(Action<DbContext> assertions) 
            => this.WithEntities<DbContext>(assertions);

        /// <inheritdoc />
        public IAndWithDbContextTestBuilder WithEntities(Func<DbContext, bool> predicate) 
            => this.WithEntities<DbContext>(predicate);

        /// <inheritdoc />
        public IAndWithDbContextTestBuilder WithEntities<TDbContext>(Func<TDbContext, bool> predicate) where TDbContext : class
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
        public IAndWithDbContextTestBuilder WithEntities<TDbContext>(Action<TDbContext> assertions) where TDbContext : class
        {
            CommonValidator.CheckForNullReference(assertions, nameof(assertions));

            assertions(this.TestContext.GetDbContext<TDbContext>());

            return this;
        }

        /// <inheritdoc />
        public IAndWithDbContextTestBuilder WithSet<TEntity>(Action<DbSet<TEntity>> assertions)
            where TEntity : class 
            => this.WithSet<DbContext, TEntity>(assertions);

        /// <inheritdoc />
        public IAndWithDbContextTestBuilder WithSet<TEntity>(Func<DbSet<TEntity>, bool> predicate)
            where TEntity : class 
            => this.WithSet<DbContext, TEntity>(predicate);

        /// <inheritdoc />
        public IAndWithDbContextTestBuilder WithSet<TDbContext, TEntity>(Action<DbSet<TEntity>> assertions)
            where TDbContext : class
            where TEntity : class
        {
            CommonValidator.CheckForNullReference(assertions, nameof(assertions));

            assertions(this.TestContext.GetBaseDbContext<TDbContext>().Set<TEntity>());

            return this;
        }

        /// <inheritdoc />
        public IAndWithDbContextTestBuilder WithSet<TDbContext, TEntity>(Func<DbSet<TEntity>, bool> predicate)
            where TDbContext : class
            where TEntity : class
        {
            CommonValidator.CheckForNullReference(predicate, nameof(predicate));

            if (!predicate(this.TestContext.GetBaseDbContext<TDbContext>().Set<TEntity>()))
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
        public IWithDbContextTestBuilder AndAlso() => this;
    }
}
