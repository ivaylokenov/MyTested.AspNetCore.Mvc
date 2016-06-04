namespace MyTested.Mvc
{
    using System;
    using Builders.Actions.ShouldHave;
    using Builders.Contracts.Actions;
    using Builders.Contracts.And;
    using Builders.Contracts.Data;
    using Builders.Data;

    /// <summary>
    /// Contains <see cref="Microsoft.Extensions.Caching.Memory.IMemoryCache"/> extension methods for <see cref="IShouldHaveTestBuilder{TActionResult}"/>.
    /// </summary>
    public static class ShouldHaveTestBuilderCachingExtensions
    {
        /// <summary>
        /// Tests whether the action does not set any <see cref="Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary"/> entries.
        /// </summary>
        /// <typeparam name="TActionResult">Type of action result type.</typeparam>
        /// <param name="shouldHaveTestBuilder">Instance of <see cref="IShouldHaveTestBuilder{TActionResult}"/> type.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder{TActionResult}"/> type.</returns>
        public static IAndTestBuilder<TActionResult> NoMemoryCache<TActionResult>(this IShouldHaveTestBuilder<TActionResult> shouldHaveTestBuilder)
        {
            var actualShouldHaveTestBuilder = (ShouldHaveTestBuilder<TActionResult>)shouldHaveTestBuilder;

            if (actualShouldHaveTestBuilder.TestContext.GetMockedMemoryCache().Count > 0)
            {
                actualShouldHaveTestBuilder.ThrowNewDataProviderAssertionExceptionWithNoEntries(MemoryCacheTestBuilder.MemoryCacheName);
            }

            return actualShouldHaveTestBuilder.NewAndTestBuilder();
        }

        /// <summary>
        /// Tests whether the action sets entries in the <see cref="Microsoft.Extensions.Caching.Memory.IMemoryCache"/>.
        /// </summary>
        /// <typeparam name="TActionResult">Type of action result type.</typeparam>
        /// <param name="shouldHaveTestBuilder">Instance of <see cref="IShouldHaveTestBuilder{TActionResult}"/> type.</param>
        /// <param name="withNumberOfEntries">Expected number of <see cref="Microsoft.Extensions.Caching.Memory.IMemoryCache"/> entries.
        /// If default null is provided, the test builder checks only if any entries are found.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder{TActionResult}"/> type.</returns>
        public static IAndTestBuilder<TActionResult> MemoryCache<TActionResult>(
            this IShouldHaveTestBuilder<TActionResult> shouldHaveTestBuilder,
            int? withNumberOfEntries = null)
        {
            var actualShouldHaveTestBuilder = (ShouldHaveTestBuilder<TActionResult>)shouldHaveTestBuilder;

            actualShouldHaveTestBuilder.ValidateDataProviderNumberOfEntries(
                MemoryCacheTestBuilder.MemoryCacheName,
                withNumberOfEntries,
                actualShouldHaveTestBuilder.TestContext.GetMockedMemoryCache().Count);

            return actualShouldHaveTestBuilder.NewAndTestBuilder();
        }

        /// <summary>
        /// Tests whether the action sets specific <see cref="Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary"/> entries.
        /// </summary>
        /// <typeparam name="TActionResult">Type of action result type.</typeparam>
        /// <param name="shouldHaveTestBuilder">Instance of <see cref="IShouldHaveTestBuilder{TActionResult}"/> type.</param>
        /// <param name="memoryCacheTestBuilder">Builder for testing specific <see cref="Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary"/> entries.</param>
        /// <returns>Test builder of <see cref="IAndTestBuilder{TActionResult}"/> type.</returns>
        public static IAndTestBuilder<TActionResult> MemoryCache<TActionResult>(
            this IShouldHaveTestBuilder<TActionResult> shouldHaveTestBuilder,
            Action<IMemoryCacheTestBuilder> memoryCacheTestBuilder)
        {
            var actualShouldHaveTestBuilder = (ShouldHaveTestBuilder<TActionResult>)shouldHaveTestBuilder;

            memoryCacheTestBuilder(new MemoryCacheTestBuilder(actualShouldHaveTestBuilder.TestContext));

            return actualShouldHaveTestBuilder.NewAndTestBuilder();
        }
    }
}
