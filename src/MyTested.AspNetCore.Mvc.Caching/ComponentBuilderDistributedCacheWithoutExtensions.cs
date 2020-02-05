namespace MyTested.AspNetCore.Mvc
{
    using System;
    using System.Collections.Generic;
    using Builders.Base;
    using Builders.Contracts.Base;
    using Builders.Contracts.Data;
    using Builders.Data.DistributedCache.WithoutDistributedCache;

    /// <summary>
    /// Contains <see cref="Microsoft.Extensions.Caching.Distributed.IDistributedCache"/> extension methods for <see cref="IBaseTestBuilderWithComponentBuilder{TBuilder}"/>.
    /// </summary>
    public static class ComponentBuilderDistributedCacheWithoutExtensions
    {
        /// <summary>
        /// Clear all entities from <see cref="Microsoft.Extensions.Caching.Distributed.IDistributedCache"/> service.
        /// </summary>
        /// <typeparam name="TBuilder">Class representing ASP.NET Core MVC test builder.</typeparam>
        /// <param name="builder">Instance of <see cref="IBaseTestBuilderWithComponentBuilder{TBuilder}"/> type.</param>
        /// <returns>The same component builder.</returns>
        public static TBuilder WithoutDistributedCache<TBuilder>(
            this IBaseTestBuilderWithComponentBuilder<TBuilder> builder)
            where TBuilder : IBaseTestBuilder
            => builder
                .WithoutDistributedCache(cache => cache
                .WithoutAllEntries());

        /// <summary>
        /// Remove given entity with key from <see cref="Microsoft.Extensions.Caching.Distributed.IDistributedCache"/> service.
        /// </summary>
        /// <typeparam name="TBuilder">Class representing ASP.NET Core MVC test builder.</typeparam>
        /// <param name="builder">Instance of <see cref="IBaseTestBuilderWithComponentBuilder{TBuilder}"/> type.</param>
        /// <param name="key">Key of the entity that will be removed.</param>
        /// <returns>The same component builder.</returns>
        public static TBuilder WithoutDistributedCache<TBuilder>(
            this IBaseTestBuilderWithComponentBuilder<TBuilder> builder,
            string key)
            where TBuilder : IBaseTestBuilder
            => builder
                .WithoutDistributedCache(cache => cache
                .WithoutEntry(key));

        /// <summary>
        /// Remove given entities from <see cref="Microsoft.Extensions.Caching.Distributed.IDistributedCache"/> service.
        /// </summary>
        /// <typeparam name="TBuilder">Class representing ASP.NET Core MVC test builder.</typeparam>
        /// <param name="builder">Instance of <see cref="IBaseTestBuilderWithComponentBuilder{TBuilder}"/> type.</param>
        /// <param name="keys">Keys of the entities that will be removed.</param>
        /// <returns>The same component builder.</returns>
        public static TBuilder WithoutDistributedCache<TBuilder>(
            this IBaseTestBuilderWithComponentBuilder<TBuilder> builder,
            IEnumerable<string> keys)
            where TBuilder : IBaseTestBuilder
            => builder
                .WithoutDistributedCache(cache => cache
                .WithoutEntries(keys));

        /// <summary>
        /// Remove given entities from <see cref="Microsoft.Extensions.Caching.Distributed.IDistributedCache"/> service.
        /// </summary>
        /// <typeparam name="TBuilder">Class representing ASP.NET Core MVC test builder.</typeparam>
        /// <param name="builder">Instance of <see cref="IBaseTestBuilderWithComponentBuilder{TBuilder}"/> type.</param>
        /// <param name="keys">Keys of the entities that will be removed.</param>
        /// <returns>The same component builder.</returns>
        public static TBuilder WithoutDistributedCache<TBuilder>(
            this IBaseTestBuilderWithComponentBuilder<TBuilder> builder,
            params string[] keys)
            where TBuilder : IBaseTestBuilder
            => builder
                .WithoutDistributedCache(cache => cache
                .WithoutEntries(keys));

        /// <summary>
        /// Remove entity or entities from <see cref="Microsoft.Extensions.Caching.Distributed.IDistributedCache"/> service.
        /// </summary>
        /// <typeparam name="TBuilder">Class representing ASP.NET Core MVC test builder.</typeparam>
        /// <param name="builder">Instance of <see cref="IBaseTestBuilderWithComponentBuilder{TBuilder}"/> type.</param>
        /// <param name="distributedCacheBuilder">Action setting the <see cref="Microsoft.Extensions.Caching.Distributed.IDistributedCache"/> values by using <see cref="IWithoutDistributedCacheBuilder"/>.</param>
        /// <returns>The same component builder.</returns>
        public static TBuilder WithoutDistributedCache<TBuilder>(
            this IBaseTestBuilderWithComponentBuilder<TBuilder> builder,
            Action<IWithoutDistributedCacheBuilder> distributedCacheBuilder)
            where TBuilder : IBaseTestBuilder
        {
            var actualBuilder = (BaseTestBuilderWithComponentBuilder<TBuilder>)builder;
            distributedCacheBuilder(new WithoutDistributedCacheBuilder(actualBuilder.TestContext.HttpContext.RequestServices));

            return actualBuilder.Builder;
        }
    }
}
