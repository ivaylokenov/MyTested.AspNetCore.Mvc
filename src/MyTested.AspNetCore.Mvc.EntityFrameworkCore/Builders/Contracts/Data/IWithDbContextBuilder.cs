namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Data
{
    using System;
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Used for building <see cref="DbContext"/>.
    /// </summary>
    public interface IWithDbContextBuilder
    {
        /// <summary>
        /// Sets initial values to the registered <see cref="DbContext"/>.
        /// </summary>
        /// <param name="entities">Initial values to add to the registered <see cref="DbContext"/>.</param>
        /// <returns>The same <see cref="DbContext"/> builder.</returns>
        IAndWithDbContextBuilder WithEntities(IEnumerable<object> entities);

        /// <summary>
        /// Sets initial values to the provided <see cref="DbContext"/>.
        /// </summary>
        /// <typeparam name="TDbContext">Type of <see cref="DbContext"/> to set up.</typeparam>
        /// <param name="entities">Initial values to add to the provided <see cref="DbContext"/>.</param>
        /// <returns>The same <see cref="DbContext"/> builder.</returns>
        IAndWithDbContextBuilder WithEntities<TDbContext>(IEnumerable<object> entities)
            where TDbContext : DbContext;

        /// <summary>
        /// Sets initial values to the registered <see cref="DbContext"/>.
        /// </summary>
        /// <param name="entities">Initial values to add to the registered <see cref="DbContext"/>.</param>
        /// <returns>The same <see cref="DbContext"/> builder.</returns>
        IAndWithDbContextBuilder WithEntities(params object[] entities);

        /// <summary>
        /// Sets initial values to the provided <see cref="DbContext"/>.
        /// </summary>
        /// <typeparam name="TDbContext">Type of <see cref="DbContext"/> to set up.</typeparam>
        /// <param name="entities">Initial values to add to the provided <see cref="DbContext"/>.</param>
        /// <returns>The same <see cref="DbContext"/> builder.</returns>
        IAndWithDbContextBuilder WithEntities<TDbContext>(params object[] entities)
            where TDbContext : DbContext;

        /// <summary>
        /// Sets initial values to the registered <see cref="DbContext"/>.
        /// </summary>
        /// <param name="dbContextSetup">Action setting the <see cref="DbContext"/>.</param>
        /// <returns>The same <see cref="DbContext"/> builder.</returns>
        IAndWithDbContextBuilder WithEntities(Action<DbContext> dbContextSetup);

        /// <summary>
        /// Sets initial values to the provided <see cref="DbContext"/>.
        /// </summary>
        /// <typeparam name="TDbContext">Type of <see cref="DbContext"/> to set up.</typeparam>
        /// <param name="dbContextSetup">Action setting the <see cref="DbContext"/>.</param>
        /// <returns>The same <see cref="DbContext"/> builder.</returns>
        IAndWithDbContextBuilder WithEntities<TDbContext>(Action<TDbContext> dbContextSetup)
            where TDbContext : DbContext;

        /// <summary>
        /// Sets initial values to the provided <see cref="DbContext"/> entity.
        /// </summary>
        /// <typeparam name="TEntity">Type of entity to set up.</typeparam>
        /// <param name="entitySetup">Action setting the <see cref="DbContext"/> entity.</param>
        /// <returns>The same <see cref="DbContext"/> builder.</returns>
        IAndWithDbContextBuilder WithSet<TEntity>(Action<DbSet<TEntity>> entitySetup)
            where TEntity : class;

        /// <summary>
        /// Sets initial values to the provided <see cref="DbContext"/> entity.
        /// </summary>
        /// <typeparam name="TDbContext">Type of <see cref="DbContext"/> to set up.</typeparam>
        /// <typeparam name="TEntity">Type of entity to set up.</typeparam>
        /// <param name="entitySetup">Action setting the <see cref="DbContext"/> entity.</param>
        /// <returns>The same <see cref="DbContext"/> builder.</returns>
        IAndWithDbContextBuilder WithSet<TDbContext, TEntity>(Action<DbSet<TEntity>> entitySetup)
            where TDbContext : DbContext
            where TEntity : class;
    }
}
