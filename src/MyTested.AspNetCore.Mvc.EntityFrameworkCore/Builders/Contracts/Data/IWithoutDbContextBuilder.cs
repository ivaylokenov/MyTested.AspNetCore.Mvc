namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Data
{
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;

    public interface IWithoutDbContextBuilder
    {
        /// <summary>
        /// Remove values from the registered <see cref="DbContext"/>.
        /// </summary>
        /// <param name="entities">Values to remove from the registered <see cref="DbContext"/>.</param>
        /// <returns>The same <see cref="DbContext"/> builder.</returns>
        IAndWithoutDbContextBuilder WithoutEntities(IEnumerable<object> entities);

        /// <summary>
        /// Remove values from the registered <see cref="DbContext"/>.
        /// </summary>
        /// <typeparam name="TDbContext">Type of <see cref="DbContext"/> to set up.</typeparam>
        /// <param name="entities">Values to remove from the registered <see cref="DbContext"/>.</param>
        /// <returns>The same <see cref="DbContext"/> builder.</returns>
        IAndWithoutDbContextBuilder WithoutEntities<TDbContext>(IEnumerable<object> entities)
            where TDbContext : DbContext;

        /// <summary>
        /// Remove values from the registered <see cref="DbContext"/>.
        /// </summary>
        /// <param name="entities">Values to remove from the registered <see cref="DbContext"/>.</param>
        /// <returns>The same <see cref="DbContext"/> builder.</returns>
        IAndWithoutDbContextBuilder WithoutEntities(params object[] entities);

        /// <summary>
        /// Remove values from the provided <see cref="DbContext"/>.
        /// </summary>
        /// <typeparam name="TDbContext">Type of <see cref="DbContext"/> to set up.</typeparam>
        /// <param name="entities">Values to remove from the provided <see cref="DbContext"/>.</param>
        /// <returns>The same <see cref="DbContext"/> builder.</returns>
        IAndWithoutDbContextBuilder WithoutEntities<TDbContext>(params object[] entities)
            where TDbContext : DbContext;

        /// <summary>
        /// Remove values from the registered <see cref="DbContext"/>.
        /// </summary>
        /// <param name="dbContextSetup">Action setting the <see cref="DbContext"/>.</param>
        /// <returns>The same <see cref="DbContext"/> builder.</returns>
        IAndWithoutDbContextBuilder WithoutEntities(Action<DbContext> dbContextSetup);

        /// <summary>
        /// Remove values from the provided <see cref="DbContext"/>.
        /// </summary>
        /// <typeparam name="TDbContext">Type of <see cref="DbContext"/> to set up.</typeparam>
        /// <param name="dbContextSetup">Action setting the <see cref="DbContext"/>.</param>
        /// <returns>The same <see cref="DbContext"/> builder.</returns>
        IAndWithoutDbContextBuilder WithoutEntities<TDbContext>(Action<TDbContext> dbContextSetup)
            where TDbContext : DbContext;

        /// <summary>
        /// Remove values from the provided <see cref="DbContext"/> entity.
        /// </summary>
        /// <typeparam name="TEntity">Type of entity to remove.</typeparam>
        /// <param name="entitySetup">Action setting the <see cref="DbContext"/> entity.</param>
        /// <returns>The same <see cref="DbContext"/> builder.</returns>
        IAndWithoutDbContextBuilder WithoutSet<TEntity>(Action<DbSet<TEntity>> entitySetup)
            where TEntity : class;

        /// <summary>
        /// Remove values from the provided <see cref="DbContext"/> entity.
        /// </summary>
        /// <typeparam name="TDbContext">Type of <see cref="DbContext"/> to remove values from.</typeparam>
        /// <typeparam name="TEntity">Type of entity to remove.</typeparam>
        /// <param name="entitySetup">Action setting the <see cref="DbContext"/> entity.</param>
        /// <returns>The same <see cref="DbContext"/> builder.</returns>
        IAndWithoutDbContextBuilder WithoutSet<TDbContext, TEntity>(Action<DbSet<TEntity>> entitySetup)
            where TDbContext : DbContext
            where TEntity : class;

        /// <summary>
        /// Wipe the whole database data, returning it to a clean state.
        /// </summary>
        /// <returns>The same <see cref="DbContext"/> builder.</returns>
        IAndWithoutDbContextBuilder WipeDatabase();
    }
}
