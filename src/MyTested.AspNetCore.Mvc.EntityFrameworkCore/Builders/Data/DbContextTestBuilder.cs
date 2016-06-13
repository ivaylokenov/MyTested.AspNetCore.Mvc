namespace MyTested.AspNetCore.Mvc.Builders.Data
{
    using System;
    using Base;
    using Contracts.Data;
    using Exceptions;
    using Internal.TestContexts;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
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
        public void WithEntries<TDbContext>(Func<TDbContext, bool> predicate) where TDbContext : DbContext
        {
            CommonValidator.CheckForNullReference(predicate, nameof(predicate));

            if (!predicate(this.GetDbContext<TDbContext>()))
            {
                throw new DataProviderAssertionException(string.Format(
                    "When calling {0} action in {1} expected the {2} entries to pass the given predicate, but it failed.",
                    this.TestContext.ActionName,
                    this.TestContext.Controller.GetName(),
                    typeof(TDbContext).ToFriendlyTypeName()));
            }
        }

        /// <inheritdoc />
        public void WithEntries<TDbContext>(Action<TDbContext> assertions) where TDbContext : DbContext
        {
            CommonValidator.CheckForNullReference(assertions, nameof(assertions));

            assertions(this.GetDbContext<TDbContext>());
        }

        private TDbContext GetDbContext<TDbContext>()
        {
            ServiceValidator.ValidateScopedServiceLifetime<TDbContext>(nameof(WithEntries));

            return this.TestContext
                .HttpContext
                .RequestServices
                .GetRequiredService<TDbContext>();
        }
    }
}
