namespace MyTested.AspNetCore.Mvc
{
    using System;
    using System.Collections.Generic;
    using MyTested.AspNetCore.Mvc.Builders.Base;
    using MyTested.AspNetCore.Mvc.Builders.Contracts.Base;
    using MyTested.AspNetCore.Mvc.Builders.Contracts.Data;
    using MyTested.AspNetCore.Mvc.Builders.Data.DistributedCache.WithoutDistributedCache;

    public static class ComponentBuilderDistributedCacheWithoutExtensions
    {
        public static TBuilder WithoutDistributedCache<TBuilder>(
            this IBaseTestBuilderWithComponentBuilder<TBuilder> builder)
            where TBuilder : IBaseTestBuilder
            => builder.WithoutDistributedCache(cache => cache.WithoutAllEntries());
  
        public static TBuilder WithoutDistributedCache<TBuilder>(
            this IBaseTestBuilderWithComponentBuilder<TBuilder> builder,
            string key)
            where TBuilder : IBaseTestBuilder
            => builder.WithoutDistributedCache(cache => cache.WithoutEntry(key));
  
        public static TBuilder WithoutDistributedCache<TBuilder>(
            this IBaseTestBuilderWithComponentBuilder<TBuilder> builder,
            IEnumerable<string> keys)
            where TBuilder : IBaseTestBuilder
            => builder.WithoutDistributedCache(cache => cache.WithoutEntries(keys));

        public static TBuilder WithoutDistributedCache<TBuilder>(
            this IBaseTestBuilderWithComponentBuilder<TBuilder> builder,
            params string[] keys)
            where TBuilder : IBaseTestBuilder
            => builder.WithoutDistributedCache(cache => cache.WithoutEntries(keys));

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
