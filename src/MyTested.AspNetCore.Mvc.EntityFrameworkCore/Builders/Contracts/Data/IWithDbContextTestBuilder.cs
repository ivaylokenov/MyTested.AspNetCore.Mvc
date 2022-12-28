namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Data
{
    using System;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Used for testing <see cref="DbContext"/>.
    /// </summary>
    public interface IWithDbContextTestBuilder
    {
        /// <summary>
        /// Tests whether <see cref="DbContext"/> entities pass the given assertions.
        /// </summary>
        /// <param name="assertions">Action containing all assertions for the <see cref="DbContext"/> entities.</param>
        /// <returns>The same <see cref="IWithDbContextTestBuilder"/>.</returns>
        IAndWithDbContextTestBuilder WithEntities(Action<DbContext> assertions);

        /// <summary>
        /// Tests whether <see cref="DbContext"/> entities pass the given predicate.
        /// </summary>
        /// <param name="predicate">Predicate testing the <see cref="DbContext"/> entities.</param>
        /// <returns>The same <see cref="IWithDbContextTestBuilder"/>.</returns>
        IAndWithDbContextTestBuilder WithEntities(Func<DbContext, bool> predicate);

        /// <summary>
        /// Tests whether <see cref="DbContext"/> entities pass the given assertions.
        /// </summary>
        /// <typeparam name="TDbContext">Type of <see cref="DbContext"/>.</typeparam>
        /// <param name="assertions">Action containing all assertions for the <see cref="DbContext"/> entities.</param>
        IAndWithDbContextTestBuilder WithEntities<TDbContext>(Action<TDbContext> assertions)
            where TDbContext : class;

        /// <summary>
        /// Tests whether <see cref="DbContext"/> entities pass the given predicate.
        /// </summary>
        /// <typeparam name="TDbContext">Type of <see cref="DbContext"/>.</typeparam>
        /// <param name="predicate">Predicate testing the <see cref="DbContext"/> entities.</param>
        IAndWithDbContextTestBuilder WithEntities<TDbContext>(Func<TDbContext, bool> predicate)
            where TDbContext : class;

        /// <summary>
        /// Tests whether <see cref="DbContext"/> entity <see cref="DbSet{TEntity}"/> passes the given assertions.
        /// </summary>
        /// <typeparam name="TEntity">Type of entity set.</typeparam>
        /// <param name="assertions">Action containing all assertions for the <see cref="DbContext"/> entity set.</param>
        IAndWithDbContextTestBuilder WithSet<TEntity>(Action<DbSet<TEntity>> assertions)
            where TEntity : class;

        /// <summary>
        /// Tests whether <see cref="DbContext"/> entity <see cref="DbSet{TEntity}"/> passes the given predicate.
        /// </summary>
        /// <typeparam name="TEntity">Type of entity set.</typeparam>
        /// <param name="predicate">Predicate testing the <see cref="DbContext"/> entity set.</param>
        IAndWithDbContextTestBuilder WithSet<TEntity>(Func<DbSet<TEntity>, bool> predicate)
            where TEntity : class;

        /// <summary>
        /// Tests whether <see cref="DbContext"/> entity <see cref="DbSet{TEntity}"/> passes the given assertions.
        /// </summary>
        /// <typeparam name="TDbContext">Type of <see cref="DbContext"/>.</typeparam>
        /// <typeparam name="TEntity">Type of entity set.</typeparam>
        /// <param name="assertions">Action containing all assertions for the <see cref="DbContext"/> entity set.</param>
        IAndWithDbContextTestBuilder WithSet<TDbContext, TEntity>(Action<DbSet<TEntity>> assertions)
            where TDbContext : class
            where TEntity : class;

        /// <summary>
        /// Tests whether <see cref="DbContext"/> entity <see cref="DbSet{TEntity}"/> passes the given predicate.
        /// </summary>
        /// <typeparam name="TDbContext">Type of <see cref="DbContext"/>.</typeparam>
        /// <typeparam name="TEntity">Type of entity set.</typeparam>
        /// <param name="predicate">Predicate testing the <see cref="DbContext"/> entity set.</param>
        IAndWithDbContextTestBuilder WithSet<TDbContext, TEntity>(Func<DbSet<TEntity>, bool> predicate)
            where TDbContext : class
            where TEntity : class;
    }
}
