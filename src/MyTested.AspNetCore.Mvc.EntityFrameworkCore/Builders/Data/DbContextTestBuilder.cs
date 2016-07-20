namespace MyTested.AspNetCore.Mvc.Builders.Data
{
    using System;
    using Base;
    using Contracts.Data;
    using Exceptions;
    using Internal.TestContexts;
    using Microsoft.EntityFrameworkCore;
    using Utilities;
    using Utilities.Extensions;
    using Utilities.Validators;

    /// <summary>
    /// Used for testing <see cref="DbContext"/>.
    /// </summary>
    public class DbContextTestBuilder : BaseTestBuilderWithInvokedAction, IDbContextTestBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DbContextTestBuilder"/> class.
        /// </summary>
        /// <param name="testContext"><see cref="ControllerTestContext"/> containing data about the currently executed assertion chain.</param>
        public DbContextTestBuilder(ControllerTestContext testContext)
            : base(testContext)
        {
        }

        /// <inheritdoc />
        public void WithEntities<TDbContext>(Func<TDbContext, bool> predicate) where TDbContext : DbContext
        {
            CommonValidator.CheckForNullReference(predicate, nameof(predicate));

            if (!predicate(this.TestContext.GetDbContext<TDbContext>()))
            {
                throw new DataProviderAssertionException(string.Format(
                    "When calling {0} action in {1} expected the {2} entities to pass the given predicate, but it failed.",
                    this.TestContext.MethodName,
                    this.TestContext.Component.GetName(),
                    typeof(TDbContext).ToFriendlyTypeName()));
            }
        }

        /// <inheritdoc />
        public void WithEntities<TDbContext>(Action<TDbContext> assertions) where TDbContext : DbContext
        {
            CommonValidator.CheckForNullReference(assertions, nameof(assertions));

            assertions(this.TestContext.GetDbContext<TDbContext>());
        }

        /// <inheritdoc />
        public void WithSet<TDbContext, TEntity>(Action<DbSet<TEntity>> assertions)
            where TDbContext : DbContext
            where TEntity : class
        {
            CommonValidator.CheckForNullReference(assertions, nameof(assertions));

            assertions(this.TestContext.GetDbContext<TDbContext>().Set<TEntity>());
        }

        /// <inheritdoc />
        public void WithSet<TDbContext, TEntity>(Func<DbSet<TEntity>, bool> predicate)
            where TDbContext : DbContext
            where TEntity : class
        {
            CommonValidator.CheckForNullReference(predicate, nameof(predicate));

            if (!predicate(this.TestContext.GetDbContext<TDbContext>().Set<TEntity>()))
            {
                throw new DataProviderAssertionException(string.Format(
                    "When calling {0} action in {1} expected the {2} set of {3} to pass the given predicate, but it failed.",
                    this.TestContext.MethodName,
                    this.TestContext.Component.GetName(),
                    typeof(TDbContext).ToFriendlyTypeName(),
                    typeof(TEntity).ToFriendlyTypeName()));
            }
        }
    }
}
