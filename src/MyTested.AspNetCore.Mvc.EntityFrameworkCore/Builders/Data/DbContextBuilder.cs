namespace MyTested.AspNetCore.Mvc.Builders.Data
{
    using System;
    using Base;
    using Contracts.Data;
    using Internal.TestContexts;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
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
        public void WithSetup<TDbContext>(Action<TDbContext> dbContextSetup)
            where TDbContext : DbContext
        {
            CommonValidator.CheckForNullReference(dbContextSetup, nameof(dbContextSetup));
            ServiceValidator.ValidateScopedServiceLifetime<TDbContext>(nameof(WithSetup));
            
            var dbContext = this.TestContext
                .HttpContext
                .RequestServices
                .GetRequiredService<TDbContext>();

            dbContextSetup(dbContext);
            dbContext.SaveChanges();
        }
    }
}
