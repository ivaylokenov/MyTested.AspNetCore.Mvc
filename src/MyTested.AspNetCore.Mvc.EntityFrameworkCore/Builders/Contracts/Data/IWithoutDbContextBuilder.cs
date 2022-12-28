namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Data
{
    using System;
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;

    public interface IWithoutDbContextBuilder
    {
        /// <summary>
        /// Remove entity by providing its primary key from the registered <see cref="DbContext"/>.
        /// </summary>
        /// <typeparam name="TEntity">Entity model type for which the provided key is related to.</typeparam>
        /// <param name="key">Primary key of entity to remove from the registered <see cref="DbContext"/>.</param>
        /// <returns>The same <see cref="DbContext"/> builder.</returns>
        IAndWithoutDbContextBuilder WithoutEntityByKey<TEntity>(object key)
            where TEntity : class;

        /// <summary>
        /// Remove entity by providing its primary key from the registered <see cref="DbContext"/>.
        /// </summary>
        /// <typeparam name="TDbContext">Type of <see cref="DbContext"/> to set up.</typeparam>
        /// <typeparam name="TEntity">Entity model type for which the provided key is related to.</typeparam>
        /// <param name="key">Primary key of entity to remove from the registered <see cref="DbContext"/>.</param>
        /// <returns>The same <see cref="DbContext"/> builder.</returns>
        IAndWithoutDbContextBuilder WithoutEntityByKey<TDbContext, TEntity>(object key)
            where TDbContext : class;

        /// <summary>
        /// Remove entities by providing their primary keys from the registered <see cref="DbContext"/>.
        /// </summary>
        /// <typeparam name="TEntity">Entity model type for which the provided key is related to.</typeparam>
        /// <param name="keys">Primary keys for entities to remove from the registered <see cref="DbContext"/>.</param>
        /// <returns>The same <see cref="DbContext"/> builder.</returns>
        IAndWithoutDbContextBuilder WithoutEntitiesByKeys<TEntity>(IEnumerable<object> keys)
            where TEntity : class;

        /// <summary>
        /// Remove entities by providing their primary keys from the registered <see cref="DbContext"/>.
        /// </summary>
        /// <typeparam name="TDbContext">Type of <see cref="DbContext"/> to set up.</typeparam>
        /// <typeparam name="TEntity">Entity model type for which the provided keys are related to.</typeparam>
        /// <param name="keys">Primary keys for entities to remove from the registered <see cref="DbContext"/>.</param>
        /// <returns>The same <see cref="DbContext"/> builder.</returns>
        IAndWithoutDbContextBuilder WithoutEntitiesByKeys<TDbContext, TEntity>(IEnumerable<object> keys)
            where TDbContext : class;

        /// <summary>
        /// Remove entity from the registered <see cref="DbContext"/>.
        /// </summary>
        /// <param name="entity">Entity to remove from the registered <see cref="DbContext"/>.</param>
        /// <returns>The same <see cref="DbContext"/> builder.</returns>
        IAndWithoutDbContextBuilder WithoutEntity(object entity);
            
        /// <summary>
        /// Remove entity from the registered <see cref="DbContext"/>.
        /// </summary>
        /// <typeparam name="TDbContext">Type of <see cref="DbContext"/> to set up.</typeparam>
        /// <param name="entity">Entity to remove from the registered <see cref="DbContext"/>.</param>
        /// <returns>The same <see cref="DbContext"/> builder.</returns>
        IAndWithoutDbContextBuilder WithoutEntity<TDbContext>(object entity)
            where TDbContext : class;

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
            where TDbContext : class;

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
            where TDbContext : class;

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
            where TDbContext : class;

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
            where TDbContext : class
            where TEntity : class;

        /// <summary>
        /// Wipes the whole database data, returning it to a clean state.
        /// </summary>
        /// <returns>The same <see cref="DbContext"/> builder.</returns>
        IAndWithoutDbContextBuilder WithoutAllEntities();
    }
}
