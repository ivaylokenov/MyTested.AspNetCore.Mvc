namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Data
{
    using System;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Used for testing <see cref="DbContext"/>.
    /// </summary>
    public interface IDbContextTestBuilder
    {
        /// <summary>
        /// Tests whether <see cref="DbContext"/> entries pass the given assertions.
        /// </summary>
        /// <typeparam name="TDbContext">Type of <see cref="DbContext"/>.</typeparam>
        /// <param name="assertions">Action containing all assertions on the <see cref="DbContext"/> entities.</param>
        void WithEntries<TDbContext>(Action<TDbContext> assertions)
               where TDbContext : DbContext;

        /// <summary>
        /// Tests whether <see cref="DbContext"/> entries pass the given predicate.
        /// </summary>
        /// <typeparam name="TDbContext">Type of <see cref="DbContext"/>.</typeparam>
        /// <param name="predicate">Predicate testing the <see cref="DbContext"/> entities.</param>
        void WithEntries<TDbContext>(Func<TDbContext, bool> predicate)
               where TDbContext : DbContext;
    }
}
