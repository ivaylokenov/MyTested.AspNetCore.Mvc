namespace MyTested.AspNetCore.Mvc
{
    using Builders.Contracts.Base;
    using Builders.Base;
    using System;
    using Builders.Contracts.Data.DistributedCache;
    using Utilities.Validators;
    using Internal.TestContexts;
    using Builders.Data.DistributedCache;

    public static class ComponentShouldHaveTestBuilderDistributedCacheExtensions
    {
        /// <summary>
        /// Tests whether the component does not set any <see cref="Microsoft.Extensions.Caching.Distributed.IDistributedCache"/> entries.
        /// </summary>
        /// <typeparam name="TBuilder">Class representing ASP.NET Core MVC test builder.</typeparam>
        /// <param name="builder">Instance of <see cref="IBaseTestBuilderWithComponentShouldHaveTestBuilder{TBuilder}"/> type.</param>
        /// <returns>The same component should have test builder.</returns>
        public static TBuilder NoDistributedCache<TBuilder>(this IBaseTestBuilderWithComponentShouldHaveTestBuilder<TBuilder> builder)
            where TBuilder : IBaseTestBuilder
        {
            var actualBuilder = (BaseTestBuilderWithComponentBuilder<TBuilder>)builder;

            if (actualBuilder.TestContext.GetDistributedCacheMock().Count > 0)
            {
                DataProviderValidator.ThrowNewDataProviderAssertionExceptionWithNoEntries(
                    actualBuilder.TestContext,
                    DistributedCacheTestBuilder.DistributedCacheName);
            }

            return actualBuilder.Builder;
        }
        
        /// <summary>
        /// Tests whether the component sets specific <see cref="Microsoft.Extensions.Caching.Distributed.IDistributedCache"/> entries.
        /// </summary>
        /// <typeparam name="TBuilder">Class representing ASP.NET Core MVC test builder.</typeparam>
        /// <param name="builder">Instance of <see cref="IBaseTestBuilderWithComponentShouldHaveTestBuilder{TBuilder}"/> type.</param>
        /// <param name="withNumberOfEntries">Expected number of <see cref="Microsoft.Extensions.Caching.Distributed.IDistributedCache"/> entries.</param>
        /// <returns>The same component should have test builder.</returns>
        public static TBuilder DistributedCache<TBuilder>(
            this IBaseTestBuilderWithComponentShouldHaveTestBuilder<TBuilder> builder,
            int? withNumberOfEntries = null)
            where TBuilder : IBaseTestBuilder
        {
            var actualBuilder = (BaseTestBuilderWithComponentBuilder<TBuilder>)builder;

             DataProviderValidator.ValidateDataProviderNumberOfEntries(
                actualBuilder.TestContext,
                DistributedCacheTestBuilder.DistributedCacheName,
                withNumberOfEntries,
                actualBuilder.TestContext.GetDistributedCacheMock().Count);

            return actualBuilder.Builder;
        }

        /// <summary>
        /// Tests whether the component sets specific <see cref="Microsoft.Extensions.Caching.Distributed.IDistributedCache"/> entries.
        /// </summary>
        /// <typeparam name="TBuilder">Class representing ASP.NET Core MVC test builder.</typeparam>
        /// <param name="builder">Instance of <see cref="IBaseTestBuilderWithComponentShouldHaveTestBuilder{TBuilder}"/> type.</param>
        /// <param name="distributedCacheTestBuilder">Builder for testing specific <see cref="Microsoft.Extensions.Caching.Distributed.IDistributedCache"/> entries.</param>
        /// <returns>The same component should have test builder.</returns>
        public static TBuilder DistributedCache<TBuilder>(
            this IBaseTestBuilderWithComponentShouldHaveTestBuilder<TBuilder> builder,
            Action<IDistributedCacheTestBuilder> distributedCacheTestBuilder)
            where TBuilder : IBaseTestBuilder
        {
            var actualBuilder = (BaseTestBuilderWithComponentBuilder<TBuilder>)builder;

            distributedCacheTestBuilder(new DistributedCacheTestBuilder(actualBuilder.TestContext));

            return actualBuilder.Builder;
        }
    }
}
