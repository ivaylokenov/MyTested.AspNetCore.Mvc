namespace MyTested.AspNetCore.Mvc
{
    using System;
    using Builders.Base;
    using Builders.Contracts.Base;
    using Builders.Contracts.Data.DistributedCache;
    using Builders.Data.DistributedCache;

    /// <summary>
    /// Contains <see cref="Microsoft.Extensions.Caching.Distributed.IDistributedCache"/> extension methods for <see cref="IBaseTestBuilderWithComponentBuilder{TBuilder}"/>.
    /// </summary>
    public static class ComponentBuilderDistributedCacheExtensions
    {
        /// <summary>
        /// Sets initial values to the <see cref="Microsoft.Extensions.Caching.Distributed.IDistributedCache"/> service.
        /// </summary>
        /// <typeparam name="TBuilder">Class representing ASP.NET Core MVC test builder.</typeparam>
        /// <param name="builder">Instance of <see cref="IBaseTestBuilderWithComponentBuilder{TBuilder}"/> type.</param>
        /// <param name="distributedCacheBuilder">Action setting the <see cref="Microsoft.Extensions.Caching.Distributed.IDistributedCache"/> values by using <see cref="IDistributedCacheBuilder"/>.</param>
        /// <returns>The same component builder.</returns>
        public static TBuilder WithDistributedCache<TBuilder>(
            this IBaseTestBuilderWithComponentBuilder<TBuilder> builder,
            Action<IDistributedCacheBuilder> distributedCacheBuilder)
            where TBuilder : IBaseTestBuilder
        {
            var actualBuilder = (BaseTestBuilderWithComponentBuilder<TBuilder>)builder;

            distributedCacheBuilder(new DistributedCacheBuilder(actualBuilder.TestContext.HttpContext.RequestServices));

            return actualBuilder.Builder;
        }
    }
}
