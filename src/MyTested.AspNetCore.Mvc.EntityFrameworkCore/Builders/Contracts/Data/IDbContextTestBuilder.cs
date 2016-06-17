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
        /// Tests whether <see cref="DbContext"/> entities pass the given assertions.
        /// </summary>
        /// <typeparam name="TDbContext">Type of <see cref="DbContext"/>.</typeparam>
        /// <param name="assertions">Action containing all assertions on the <see cref="DbContext"/> entities.</param>
        void WithEntities<TDbContext>(Action<TDbContext> assertions)
               where TDbContext : DbContext;

        /// <summary>
        /// Tests whether <see cref="DbContext"/> entities pass the given predicate.
        /// </summary>
        /// <typeparam name="TDbContext">Type of <see cref="DbContext"/>.</typeparam>
        /// <param name="predicate">Predicate testing the <see cref="DbContext"/> entities.</param>
        void WithEntities<TDbContext>(Func<TDbContext, bool> predicate)
               where TDbContext : DbContext;

        /// <summary>
        /// Tests whether <see cref="DbContext"/> entity <see cref="DbSet{TEntity}"/> passes the given assertions.
        /// </summary>
        /// <typeparam name="TDbContext">Type of <see cref="DbContext"/>.</typeparam>
        /// <typeparam name="TEntity">Type of entity set.</typeparam>
        /// <param name="assertions">Action containing all assertions on the <see cref="DbContext"/> entity set.</param>
        void WithSet<TDbContext, TEntity>(Action<DbSet<TEntity>> assertions)
            where TDbContext : DbContext
            where TEntity : class;

        /// <summary>
        /// Tests whether <see cref="DbContext"/> entity <see cref="DbSet{TEntity}"/> passes the given predicate.
        /// </summary>
        /// <typeparam name="TDbContext">Type of <see cref="DbContext"/>.</typeparam>
        /// <typeparam name="TEntity">Type of entity set.</typeparam>
        /// <param name="predicate">Predicate testing the <see cref="DbContext"/> entity set.</param>
        void WithSet<TDbContext, TEntity>(Func<DbSet<TEntity>, bool> predicate)
            where TDbContext : DbContext
            where TEntity : class;
    }
}
