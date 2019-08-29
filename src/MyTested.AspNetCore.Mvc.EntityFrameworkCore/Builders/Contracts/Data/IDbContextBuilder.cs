namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Data
{
    using System;
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Used for building <see cref="DbContext"/>.
    /// </summary>
    public interface IDbContextBuilder
    {
        /// <summary>
        /// Sets initial values to the registered <see cref="DbContext"/>.
        /// </summary>
        /// <param name="entities">Initial values to add to the registered <see cref="DbContext"/>.</param>
        /// <returns>The same <see cref="DbContext"/> builder.</returns>
        IAndDbContextBuilder WithEntities(IEnumerable<object> entities);

        /// <summary>
        /// Remove values from the registered <see cref="DbContext"/>.
        /// </summary>
        /// <param name="entities">Values to remove from the registered <see cref="DbContext"/>.</param>
        /// <returns>The same <see cref="DbContext"/> builder.</returns>
        IAndDbContextBuilder WithoutEntities(IEnumerable<object> entities);

        /// <summary>
        /// Sets initial values to the provided <see cref="DbContext"/>.
        /// </summary>
        /// <typeparam name="TDbContext">Type of <see cref="DbContext"/> to set up.</typeparam>
        /// <param name="entities">Initial values to add to the provided <see cref="DbContext"/>.</param>
        /// <returns>The same <see cref="DbContext"/> builder.</returns>
        IAndDbContextBuilder WithEntities<TDbContext>(IEnumerable<object> entities)
            where TDbContext : DbContext;

        /// <summary>
        /// Remove values from the registered <see cref="DbContext"/>.
        /// </summary>
        /// <typeparam name="TDbContext">Type of <see cref="DbContext"/> to set up.</typeparam>
        /// <param name="entities">Values to remove from the registered <see cref="DbContext"/>.</param>
        /// <returns>The same <see cref="DbContext"/> builder.</returns>
        IAndDbContextBuilder WithoutEntities<TDbContext>(IEnumerable<object> entities)
            where TDbContext : DbContext;

        /// <summary>
        /// Sets initial values to the registered <see cref="DbContext"/>.
        /// </summary>
        /// <param name="entities">Initial values to add to the registered <see cref="DbContext"/>.</param>
        /// <returns>The same <see cref="DbContext"/> builder.</returns>
        IAndDbContextBuilder WithEntities(params object[] entities);

        /// <summary>
        /// Remove values from the registered <see cref="DbContext"/>.
        /// </summary>
        /// <param name="entities">Values to remove from the registered <see cref="DbContext"/>.</param>
        /// <returns>The same <see cref="DbContext"/> builder.</returns>
        IAndDbContextBuilder WithoutEntities(params object[] entities);

        /// <summary>
        /// Sets initial values to the provided <see cref="DbContext"/>.
        /// </summary>
        /// <typeparam name="TDbContext">Type of <see cref="DbContext"/> to set up.</typeparam>
        /// <param name="entities">Initial values to add to the provided <see cref="DbContext"/>.</param>
        /// <returns>The same <see cref="DbContext"/> builder.</returns>
        IAndDbContextBuilder WithEntities<TDbContext>(params object[] entities)
            where TDbContext : DbContext;

        /// <summary>
        /// Remove values from the provided <see cref="DbContext"/>.
        /// </summary>
        /// <typeparam name="TDbContext">Type of <see cref="DbContext"/> to set up.</typeparam>
        /// <param name="entities">Values to remove from the provided <see cref="DbContext"/>.</param>
        /// <returns>The same <see cref="DbContext"/> builder.</returns>
        IAndDbContextBuilder WithoutEntities<TDbContext>(params object[] entities)
            where TDbContext : DbContext;

        /// <summary>
        /// Sets initial values to the registered <see cref="DbContext"/>.
        /// </summary>
        /// <param name="dbContextSetup">Action setting the <see cref="DbContext"/>.</param>
        /// <returns>The same <see cref="DbContext"/> builder.</returns>
        IAndDbContextBuilder WithEntities(Action<DbContext> dbContextSetup);

        /// <summary>
        /// Remove values from the registered <see cref="DbContext"/>.
        /// </summary>
        /// <param name="dbContextSetup">Action setting the <see cref="DbContext"/>.</param>
        /// <returns>The same <see cref="DbContext"/> builder.</returns>
        IAndDbContextBuilder WithoutEntities(Action<DbContext> dbContextSetup);

        /// <summary>
        /// Sets initial values to the provided <see cref="DbContext"/>.
        /// </summary>
        /// <typeparam name="TDbContext">Type of <see cref="DbContext"/> to set up.</typeparam>
        /// <param name="dbContextSetup">Action setting the <see cref="DbContext"/>.</param>
        /// <returns>The same <see cref="DbContext"/> builder.</returns>
        IAndDbContextBuilder WithEntities<TDbContext>(Action<TDbContext> dbContextSetup)
            where TDbContext : DbContext;

        /// <summary>
        /// Remove values from the provided <see cref="DbContext"/>.
        /// </summary>
        /// <typeparam name="TDbContext">Type of <see cref="DbContext"/> to set up.</typeparam>
        /// <param name="dbContextSetup">Action setting the <see cref="DbContext"/>.</param>
        /// <returns>The same <see cref="DbContext"/> builder.</returns>
        IAndDbContextBuilder WithoutEntities<TDbContext>(Action<TDbContext> dbContextSetup)
            where TDbContext : DbContext;

        /// <summary>
        /// Wipe the whole database data, returning it to a clean state.
        /// </summary>
        /// <returns>The same <see cref="DbContext"/> builder.</returns>
        IAndDbContextBuilder WipeDatabase();

        /// <summary>
        /// Sets initial values to the provided <see cref="DbContext"/> entity.
        /// </summary>
        /// <typeparam name="TEntity">Type of entity to set up.</typeparam>
        /// <param name="entitySetup">Action setting the <see cref="DbContext"/> entity.</param>
        /// <returns>The same <see cref="DbContext"/> builder.</returns>
        IAndDbContextBuilder WithSet<TEntity>(Action<DbSet<TEntity>> entitySetup)
            where TEntity : class;

        /// <summary>
        /// Remove values from the provided <see cref="DbContext"/> entity.
        /// </summary>
        /// <typeparam name="TEntity">Type of entity to remove.</typeparam>
        /// <param name="entitySetup">Action setting the <see cref="DbContext"/> entity.</param>
        /// <returns>The same <see cref="DbContext"/> builder.</returns>
        IAndDbContextBuilder WithoutSet<TEntity>(Action<DbSet<TEntity>> entitySetup)
            where TEntity : class;

        /// <summary>
        /// Sets initial values to the provided <see cref="DbContext"/> entity.
        /// </summary>
        /// <typeparam name="TDbContext">Type of <see cref="DbContext"/> to set up.</typeparam>
        /// <typeparam name="TEntity">Type of entity to set up.</typeparam>
        /// <param name="entitySetup">Action setting the <see cref="DbContext"/> entity.</param>
        /// <returns>The same <see cref="DbContext"/> builder.</returns>
        IAndDbContextBuilder WithSet<TDbContext, TEntity>(Action<DbSet<TEntity>> entitySetup)
            where TDbContext : DbContext
            where TEntity : class;

        /// <summary>
        /// Remove values from the provided <see cref="DbContext"/> entity.
        /// </summary>
        /// <typeparam name="TDbContext">Type of <see cref="DbContext"/> to remove values from.</typeparam>
        /// <typeparam name="TEntity">Type of entity to remove.</typeparam>
        /// <param name="entitySetup">Action setting the <see cref="DbContext"/> entity.</param>
        /// <returns>The same <see cref="DbContext"/> builder.</returns>
        IAndDbContextBuilder WithoutSet<TDbContext, TEntity>(Action<DbSet<TEntity>> entitySetup)
            where TDbContext : DbContext
            where TEntity : class;
    }
}
