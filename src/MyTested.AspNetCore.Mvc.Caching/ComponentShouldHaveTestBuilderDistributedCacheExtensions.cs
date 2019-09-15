namespace MyTested.AspNetCore.Mvc
{
    using Builders.Contracts.Base;
    using Builders.Contracts.Data;
    using Builders.Base;
    using System;
    using Utilities.Validators;
    using MyTested.AspNetCore.Mvc.Internal.TestContexts;
    using MyTested.AspNetCore.Mvc.Builders.Data;

    public static class ComponentShouldHaveTestBuilderDistributedCacheExtensions
    {
        /* /// <summary>
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
                    DistributedCacheTestBuilder.MemoryCacheName);
            }

            return actualBuilder.Builder;
        }

        /// <summary>
        /// Tests whether the component sets specific <see cref="Microsoft.Extensions.Caching.Distributed.IDistributedCache"/> entries.
        /// </summary>
        /// <typeparam name="TBuilder">Class representing ASP.NET Core MVC test builder.</typeparam>
        /// <param name="builder">Instance of <see cref="IBaseTestBuilderWithComponentShouldHaveTestBuilder{TBuilder}"/> type.</param>
        /// <param name="withNumberOfEntries">Expected number of <see cref="Microsoft.Extensions.Caching.Distributed.IDistributedCache"/> entries.
        /// <returns>The same component should have test builder.</returns>
        public static TBuilder DistributedCache<TBuilder>(
            this IBaseTestBuilderWithComponentShouldHaveTestBuilder<TBuilder> builder,
            int? withNumberOfEntries = null)
            where TBuilder : IBaseTestBuilder
        {
            var actualBuilder = (BaseTestBuilderWithComponentBuilder<TBuilder>)builder;

             DataProviderValidator.ValidateDataProviderNumberOfEntries(
                actualBuilder.TestContext,
                DistributedCacheTestBuilder.MemoryCacheName,
                withNumberOfEntries,
                actualBuilder.TestContext.GetDistributedCacheMock().Count);

            return actualBuilder.Builder;
        }*/

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
